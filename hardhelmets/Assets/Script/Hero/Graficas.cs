using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityStandardAssets.CinematicEffects;
using UnityStandardAssets.ImageEffects;

public class Graficas : MonoBehaviour {

	public string antiAliasing;
	public string ambientOclusion;
	public int motionBlur;
	public int bloom;
	public int dephOfFlied;
	public int globalFog;
	public int volumetricImage;
	public string sunShaft;



	// Use this for initialization
	void Start ()
	{
		antiAliasing = PlayerPrefs.GetString("antiAliasing");
		ambientOclusion = PlayerPrefs.GetString("ambientOclusion");
		motionBlur = PlayerPrefs.GetInt("motionBlur");
		bloom = PlayerPrefs.GetInt("bloom");
		dephOfFlied = PlayerPrefs.GetInt("dephOfFlied");
		globalFog = PlayerPrefs.GetInt("globalFog");
		volumetricImage = PlayerPrefs.GetInt("volumetricImage");
		sunShaft = PlayerPrefs.GetString("sunShaft");

		if(motionBlur == 1)
		{
			GetComponent<UnityStandardAssets.CinematicEffects.MotionBlur>().enabled = true;
		}else
		{
			GetComponent<UnityStandardAssets.CinematicEffects.MotionBlur>().enabled = false;
		}

		if(bloom == 1)
		{
			GetComponent<UnityStandardAssets.CinematicEffects.Bloom>().enabled = true;
		}else
		{
			GetComponent<UnityStandardAssets.CinematicEffects.Bloom>().enabled = false;
		}

		if(dephOfFlied == 1)
		{
			GetComponent<UnityStandardAssets.CinematicEffects.DepthOfField>().enabled = true;
		}else
		{
			GetComponent<UnityStandardAssets.CinematicEffects.DepthOfField>().enabled = false;
		}

		if(globalFog == 1)
		{
			GetComponent<GlobalFog>().enabled = true;
		}else
		{
			GetComponent<GlobalFog>().enabled = false;
		}

		if(volumetricImage == 1)
		{
			GetComponent<HxVolumetricCamera>().enabled = true;
		}else
		{
			GetComponent<HxVolumetricCamera>().enabled = false;
		}

		/*if(sunShaft == "LOW")
		{
			GetComponent<SunShafts>().SunShaftsResolution.Low;
		}else if(sunShaft == "NORMAL")
		{
			GetComponent<SunShafts>().SunShaftsResolution.Normal;
		}else if(sunShaft == "HIGH")
		{
			GetComponent<SunShafts>().SunShaftsResolution.High;
		}*/
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
