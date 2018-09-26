using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine.Unity.Modules.AttachmentTools; 

public class CustomFusilero : MonoBehaviour {

	[SpineSkin]
	public List<string> skinsToCombine;

	Spine.Skin combinedSkin;

	int cara;
	int casco;

	public string malo;

	void Awake ()
	{
		cara = Random.Range(1,4);
		casco = Random.Range(1,3);
	}

	void Start ()
	{
		if(malo == "")
		{
			skinsToCombine[0] = "cara"+cara.ToString()+"a";
		}else
		{
			skinsToCombine[0] = "cara"+cara.ToString()+malo;
		}

		if(casco == 1)
		{
			skinsToCombine[1] = "casco1"+malo;
			skinsToCombine[2] = "maleta1"+malo;
			skinsToCombine[3] = "chaleco1"+malo;
		}else if(casco == 2)
		{
			skinsToCombine[1] = "casco2"+malo;
			skinsToCombine[2] = "maleta2"+malo;
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
	public bool poner = true;
	bool activar;
	void Update ()
	{
		if(poner)
		{
			if(GetComponent<AINetwork>())
			{
				if(GetComponent<AINetwork>().salud <= 50)
				{
					skinsToCombine[6] = "cascado";
					activar = true;
					poner = false;
				}
			}else if(GetComponent<AIMorteroMaloNetwork>())
			{
				if(GetComponent<AIMorteroMaloNetwork>().salud <= 50)
				{
					skinsToCombine[5] = "cascado";
					activar = true;
					poner = false;
				}
			}else if(GetComponent<AIMorteroNetwork>())
			{
				if(GetComponent<AIMorteroNetwork>().salud <= 50)
				{
					skinsToCombine[5] = "cascado";
					activar = true;
					poner = false;
				}
			}else if(GetComponent<AINetwork>())
			{
				if(GetComponent<AINetwork>().salud <= 50)
				{
					skinsToCombine[6] = "cascado";
					activar = true;
					poner = false;
				}
			}
		}

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


