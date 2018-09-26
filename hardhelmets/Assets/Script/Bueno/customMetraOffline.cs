﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine.Unity.Modules.AttachmentTools; 

public class customMetraOffline : MonoBehaviour {

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
		if(gameObject.tag == "Player")
		{
			malo = PlayerPrefs.GetString("factionBuena");
		}else
		{
			malo = PlayerPrefs.GetString("factionMala");
		}
		activar = true;

		skinsToCombine[7] = "";

		if(malo == "")
		{
			skinsToCombine[0] = "cara"+cara.ToString()+"a";
		}else
		{
			skinsToCombine[0] = "cara"+cara.ToString()+malo;
		}

		if(casco == 1)
		{
			skinsToCombine[1] = "casco6"+malo;
			skinsToCombine[2] = "maleta1"+malo;
			skinsToCombine[3] = "abrigo1"+malo;
			skinsToCombine[4] = "balas1"+malo;
			skinsToCombine[5] = "cuerpo1"+malo;
		}else if(casco == 2)
		{
			skinsToCombine[1] = "casco3"+malo;
			skinsToCombine[2] = "chaleco1"+malo;
			skinsToCombine[3] = "balas1"+malo;
			skinsToCombine[4] = "maleta2"+malo;
			skinsToCombine[5] = "cuerpo1"+malo;
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
			if(GetComponent<AI>().salud <= 50)
			{
				skinsToCombine[7] = "cascado";
				activar = true;
				poner = false;
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
