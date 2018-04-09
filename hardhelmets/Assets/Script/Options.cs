using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour {

	public UnityEngine.UI.Slider efects;
	public UnityEngine.UI.Slider music;
	public UnityEngine.UI.Slider voice;

	public UnityEngine.UI.Toggle blood;

	float efectos;
	float musica;
	float voz;

	float sangre;

	// Use this for initialization
	void Start ()
	{
		if(PlayerPrefs.GetInt("violencia") == 1)
		{
			blood.isOn = true;
		}else
		{
			blood.isOn = false;
		}

		efects.value = PlayerPrefs.GetFloat("efects");
		voice.value = PlayerPrefs.GetFloat("voice");
		music.value = PlayerPrefs.GetFloat("music");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(blood.isOn == true)
		{
			PlayerPrefs.SetInt("violencia",1);
		}else if(blood.isOn == false)
		{
			PlayerPrefs.SetInt("violencia",0);
		}

		PlayerPrefs.SetFloat("efects", efects.value);
		PlayerPrefs.SetFloat("voice", voice.value);
		PlayerPrefs.SetFloat("music", music.value);
	}
}
