using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidosRandom : MonoBehaviour {

	public AudioClip[] sonido;
	public AudioSource audio1;

	//VOLUMEN
	float efectos;

	// Use this for initialization
	void Start ()
	{
		efectos = PlayerPrefs.GetFloat("efects");

		audio1.volume = efectos;
		audio1.clip = sonido[Random.Range(0,sonido.Length)];
		audio1.Play();
	}

}
