using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine.Unity.Modules.AttachmentTools;

public class customLoading : MonoBehaviour {

	[SpineSkin]
	public List<string> skinsToCombine;

	Spine.Skin combinedSkin;

	int cara;
	int casco;

	public string malo;

	public string verificar;

	public string[] armas = new string[]{"fusil","escopeta","submetra","metra","lansallamas","panzer"};

	void Start ()
	{
		cara = Random.Range(1,4);
		casco = Random.Range(1,3);
	}

	public bool poner;

	void Update ()
	{
		if(gameObject.tag == "Player")
		{
			malo = PlayerPrefs.GetString("factionBuena");
		}else
		{
			malo = PlayerPrefs.GetString("factionMala");
		}
		if(verificar != malo)
		{
			verificar = malo;
			poner = false;
		}

		if(!poner)
		{
			if(malo == "")
			{
				skinsToCombine[0] = "cara"+cara.ToString()+"a";
			}else
			{
				skinsToCombine[0] = "cara"+cara.ToString()+malo;
			}

			skinsToCombine[5] = armas[Random.Range(0,armas.Length)];

			if(casco == 1)
			{
				skinsToCombine[1] = "casco1"+malo;
				skinsToCombine[2] = "maleta1"+malo;
				skinsToCombine[3] = "chaleco1"+malo;
				skinsToCombine[4] = "cuerpo1"+malo;
			}else if(casco == 2)
			{
				skinsToCombine[1] = "casco2"+malo;
				skinsToCombine[2] = "maleta2"+malo;
				skinsToCombine[4] = "cuerpo1"+malo;
			}

			poner = true;
		}

		var skeletonComponent = GetComponent<ISkeletonComponent>();
		if (skeletonComponent == null) return;
		var skeleton = skeletonComponent.Skeleton;
		if (skeleton == null) return;

		combinedSkin = combinedSkin ?? new Spine.Skin("combined");
		combinedSkin.Clear();
		foreach (var skinName in skinsToCombine) {
			var skin = skeleton.Data.FindSkin(skinName);
			if (skin != null) combinedSkin.Append(skin);
		}

		skeleton.SetSkin(combinedSkin);
		skeleton.SetToSetupPose();
		var animationStateComponent = skeletonComponent as IAnimationStateComponent;
		if (animationStateComponent != null) animationStateComponent.AnimationState.Apply(skeleton);
	}
}
