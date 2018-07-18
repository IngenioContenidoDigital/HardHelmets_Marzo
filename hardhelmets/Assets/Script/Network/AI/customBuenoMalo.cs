using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine.Unity.Modules.AttachmentTools; 

public class customBuenoMalo : MonoBehaviour {

	[SpineSkin]
	public List<string> skinsToCombine;

	Spine.Skin combinedSkin;

	public bool activar;

	public string malo;

	public string skin;

	public bool personaje;

	// Use this for initialization
	void Start ()
	{
		if(personaje)
		{
			if(gameObject.tag == "Player")
			{
				malo = PlayerPrefs.GetString("factionBuena");
			}else
			{
				malo = PlayerPrefs.GetString("factionMala");
			}
		}else
		{
			if(gameObject.tag == "Player")
			{
				if(PlayerPrefs.GetString("factionBuena") == "")
				{
					malo = "1";
				}else
				{
					malo = "2";
				}
			}else
			{
				if(PlayerPrefs.GetString("factionMala") == "b")
				{
					malo = "2";
				}else
				{
					malo = "1";
				}
			}
		}

		activar = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		skinsToCombine[0] = skin+malo;
		if(activar)
		{
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
			activar = false;
		}
	}
}
