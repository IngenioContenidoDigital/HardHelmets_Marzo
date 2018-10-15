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
	public bool personajeUI;

	public string[] aleatorio = new string[]{"A","B","C","E"};

	// Use this for initialization
	void Start ()
	{
		string animation = aleatorio[Random.Range(0,aleatorio.Length)];
		if(personajeUI)
		{
			//GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, animation, true);
			if(gameObject.tag == "Player")
			{
				if(PlayerPrefs.GetString("factionBuena") == "")
				{
					malo = "a";
				}
				if(PlayerPrefs.GetString("factionBuena") == "b")
				{
					malo = "b";
				}
				if(PlayerPrefs.GetString("factionBuena") == "c")
				{
					malo = "c";
				}
			}else
			{
				if(PlayerPrefs.GetString("factionMala") == "")
				{
					malo = "a";
				}
				if(PlayerPrefs.GetString("factionMala") == "b")
				{
					malo = "b";
				}
				if(PlayerPrefs.GetString("factionMala") == "c")
				{
					malo = "c";
				}
			}
		}else if(personaje)
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
				}
				if(PlayerPrefs.GetString("factionBuena") == "b")
				{
					malo = "2";
				}
				if(PlayerPrefs.GetString("factionBuena") == "c")
				{
					malo = "3";
				}
			}else
			{
				if(PlayerPrefs.GetString("factionMala") == "")
				{
					malo = "1";
				}
				if(PlayerPrefs.GetString("factionMala") == "b")
				{
					malo = "2";
				}
				if(PlayerPrefs.GetString("factionMala") == "c")
				{
					malo = "3";
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

		if(personajeUI)
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

			if(gameObject.tag == "Player")
			{
				if(PlayerPrefs.GetString("factionBuena") == "")
				{
					malo = "a";
				}
				if(PlayerPrefs.GetString("factionBuena") == "b")
				{
					malo = "b";
				}
				if(PlayerPrefs.GetString("factionBuena") == "c")
				{
					malo = "c";
				}
			}else
			{
				if(PlayerPrefs.GetString("factionMala") == "")
				{
					malo = "a";
				}
				if(PlayerPrefs.GetString("factionMala") == "b")
				{
					malo = "b";
				}
				if(PlayerPrefs.GetString("factionMala") == "c")
				{
					malo = "c";
				}
			}
		}
	}
}
