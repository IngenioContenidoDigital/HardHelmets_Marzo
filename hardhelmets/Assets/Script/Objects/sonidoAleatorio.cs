using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sonidoAleatorio : MonoBehaviour {

	public AudioClip uno;
	public AudioClip dos;
	public AudioClip tres;
	public AudioClip cuatro;

	public AudioSource audio1;

	//VOLUMEN
	float efectos;
	int random;

	void Start ()
	{
		efectos = PlayerPrefs.GetFloat("efects");
		random = Random.Range(1,5);
		if(random == 1)
		{
			primero();
		}
		if(random == 2)
		{
			segundo();
		}
		if(random == 3)
		{
			tercero();
		}
		if(random == 4)
		{
			cuarto();
		}
	}
		
	void primero ()
	{
		audio1.volume = efectos;
		audio1.clip = uno;
		audio1.Play();
	}
	void segundo ()
	{
		audio1.volume = efectos;
		audio1.clip = dos;
		audio1.Play();
	}
	void tercero ()
	{
		audio1.volume = efectos;
		audio1.clip = tres;
		audio1.Play();
	}
	void cuarto ()
	{
		audio1.volume = efectos;
		audio1.clip = cuatro;
		audio1.Play();
	}
}
