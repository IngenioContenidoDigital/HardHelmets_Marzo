using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine.Unity.Modules.AttachmentTools; 

public class Custom : MonoBehaviour {

		[SpineSkin]
		public List<string> skinsToCombine;

		Spine.Skin combinedSkin;


		void Start ()
		{
			//PlayerPrefs.SetString("casco", "");
		}

		void FixedUpdate ()
		{
			skinsToCombine[0] = GetComponent<customTow>().casco;//casco
			skinsToCombine[1] = GetComponent<customTow>().cara;//cara
			skinsToCombine[2] = GetComponent<customTow>().mascara;//cuerpo
			skinsToCombine[3] = GetComponent<customTow>().uniforme;//uniforme
			skinsToCombine[4] = GetComponent<customTow>().cuello;//cuello
			skinsToCombine[5] = GetComponent<customTow>().cinturon;//cinturon
			skinsToCombine[6] = GetComponent<customTow>().chaleco;//chaleco
			skinsToCombine[7] = GetComponent<customTow>().maleta;//maleta

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


