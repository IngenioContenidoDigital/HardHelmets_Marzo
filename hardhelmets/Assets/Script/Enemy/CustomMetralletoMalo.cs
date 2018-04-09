using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine.Unity.Modules.AttachmentTools; 

public class CustomMetralletoMalo : MonoBehaviour {

	[SpineSkin]
	public List<string> skinsToCombine;

	Spine.Skin combinedSkin;

	int cara;
	int casco;
	int shemag;
	int mascara;
	int balas;

	void Awake ()
	{
		cara = Random.Range(1,6);
		casco = Random.Range(1,3);
		shemag = Random.Range(1,3);
		mascara = Random.Range(1,3);
		balas = Random.Range(1,3);
	}

	void Start ()
	{
		skinsToCombine[0] = "Cara"+cara.ToString();
		if(cara == 4)
		{
			skinsToCombine[1] = "musclesNegro";
		}else
		{
			skinsToCombine[1] = "muscles";
		}
		if(casco == 1)
		{
			skinsToCombine[2] = "MaloCasco2";
		}else
		{
			skinsToCombine[2] = "Malocasco3";
		}
		if(shemag == 1)
		{
			skinsToCombine[6] = "Mascara";
		}
		if(mascara == 1)
		{
			skinsToCombine[7] = "Shemag";
		}
		if(balas == 1)
		{
			skinsToCombine[5] = "Balas";
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
