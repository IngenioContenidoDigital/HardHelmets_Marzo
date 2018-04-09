using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class impact : MonoBehaviour {

	int azar;

	//DISPAROS
	public AudioClip sonido1;
	public AudioClip sonido2;
	public AudioClip sonido3;
	public AudioClip sonido4;
	public AudioClip sonido5;
	public AudioClip sonido6;
	public AudioClip sonido7;
	public AudioClip sonido8;
	public AudioClip sonido9;
	public AudioClip sonido10;

	//AUDIO PLAY
	public AudioSource audio1;

	//VOLUMEN
	float efectos;

	// Use this for initialization
	void Start ()
	{
		azar = Random.Range(1,11);

		efectos = PlayerPrefs.GetFloat("efects");

		if(azar == 1)
		{
			audio1.volume = efectos;
			audio1.clip = sonido1;
			audio1.Play();
		}else if(azar == 2)
		{
			audio1.volume = efectos;
			audio1.clip = sonido2;
			audio1.Play();
		}else if(azar == 3)
		{
			audio1.volume = efectos;
			audio1.clip = sonido3;
			audio1.Play();
		}else if(azar == 4)
		{
			audio1.volume = efectos;
			audio1.clip = sonido4;
			audio1.Play();
		}else if(azar == 5)
		{
			audio1.volume = efectos;
			audio1.clip = sonido5;
			audio1.Play();
		}else if(azar == 6)
		{
			audio1.volume = efectos;
			audio1.clip = sonido6;
			audio1.Play();
		}else if(azar == 7)
		{
			audio1.volume = efectos;
			audio1.clip = sonido7;
			audio1.Play();
		}else if(azar == 8)
		{
			audio1.volume = efectos;
			audio1.clip = sonido8;
			audio1.Play();
		}else if(azar == 9)
		{
			audio1.volume = efectos;
			audio1.clip = sonido9;
			audio1.Play();
		}else if(azar == 10)
		{
			audio1.volume = efectos;
			audio1.clip = sonido10;
			audio1.Play();
		}
	}
}
