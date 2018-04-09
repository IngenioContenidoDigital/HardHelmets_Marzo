using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Llamas : MonoBehaviour {

	public AudioSource audio1;
	float efectos;

	public AudioClip entra;
	public AudioClip sale;

	public static bool sonar;
	bool activo;

	void Update()
	{
		if(sonar)
		{
			LanzaLlamas();
			sonar = false;
		}
		if(GetComponent<ParticleSystem>().particleCount >= 21 && !activo)
		{
			activo = true;
		}
		if(GetComponent<ParticleSystem>().particleCount <= 20 && activo)
		{
			activo = false;
			LanzaLlamasOff();
		}
	}

	void LanzaLlamas()
	{
		audio1.volume = PlayerPrefs.GetFloat("efects");
		audio1.clip = entra;
		audio1.Play();
	}
	void LanzaLlamasOff()
	{
		audio1.volume = PlayerPrefs.GetFloat("efects");
		audio1.clip = sale;
		audio1.Play();
	}
}
