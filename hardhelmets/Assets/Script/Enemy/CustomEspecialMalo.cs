using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine.Unity.Modules.AttachmentTools;

public class CustomEspecialMalo : MonoBehaviour {

	[SpineSkin]
	public List<string> skinsToCombine;

	Spine.Skin combinedSkin;

	int cara;
	int casco;
	int pantalon;

	void Awake ()
	{
		cara = Random.Range(1,6);
		casco = Random.Range(1,3);
		pantalon = Random.Range(1,3);
	}

	void Start ()
	{
		skinsToCombine[0] = "Cara"+cara.ToString();

		if(casco == 1)
		{
			skinsToCombine[1] = "MaloCasco2";
			if(pantalon == 1)
			{
				skinsToCombine[2] = "MaloPantalon";
				skinsToCombine[3] = "MaloMaleta";
			}else
			{
				skinsToCombine[2] = "MaloPantalon2";
				skinsToCombine[5] = "MaloAbrigo";
				skinsToCombine[6] = "Shemag";
				skinsToCombine[7] = "Radio";
			}
			skinsToCombine[4] = "MaloCinturon";
		}else
		{
			skinsToCombine[1] = "Malocasco3";
			skinsToCombine[2] = "MaloPantalon";
			skinsToCombine[3] = "MaloMaleta";
			skinsToCombine[4] = "MaloCinturon";
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
