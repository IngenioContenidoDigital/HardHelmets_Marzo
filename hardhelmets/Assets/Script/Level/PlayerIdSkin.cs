using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine.Unity.Modules.AttachmentTools;
using UnityEngine.UI;

public class PlayerIdSkin : MonoBehaviour {

	[SpineSkin]
	public List<string> skinsToCombine;

	Spine.Skin combinedSkin;

	public string name;

	public UnityEngine.UI.Text nombre;

	public int nivel;
	public int flags;
	public int bas;

	public UnityEngine.UI.Text level;
	public UnityEngine.UI.Text banderas;
	public UnityEngine.UI.Text bases;

	/*public GameObject avatar;
	public GameObject borde;
	public GameObject fondo;*/

	// Use this for initialization
	void Start ()
	{
		name = PlayerPrefs.GetString("SteamName");
		nivel = PlayerPrefs.GetInt("PlayerLevel");
		flags = PlayerPrefs.GetInt("Banderas");
		bas = PlayerPrefs.GetInt("Bases");

		nombre.text = name;

		level.text = nivel.ToString();
		banderas.text = flags.ToString();
		bases.text = bas.ToString();

		skinsToCombine[0] = PlayerPrefs.GetString("avatar");
		skinsToCombine[1] = PlayerPrefs.GetString("borde");
		skinsToCombine[2] = PlayerPrefs.GetString("fondo");
		skinsToCombine[3] = "rango"+nivel.ToString();
	}
	
	// Update is called once per frame
	void Update ()
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
	}
}
