using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sonidosVikindo : MonoBehaviour {

	public AudioSource audio1;

	//VOLUMEN
	float efectos;

	void Start ()
	{
		efectos = PlayerPrefs.GetFloat("efects");
	}

	public AudioClip[] acabado;

	void acabadoViking()
	{
		audio1.volume = efectos;
		audio1.clip = acabado[Random.Range(0,acabado.Length)];
		audio1.Play();
	}

	public AudioClip[] ataque;

	void ataqueViking()
	{
		audio1.volume = efectos;
		audio1.clip = ataque[Random.Range(0,ataque.Length)];
		audio1.Play();
	}

	public AudioClip[] attack;

	void attackViking()
	{
		audio1.volume = efectos;
		audio1.clip = attack[Random.Range(0,attack.Length)];
		audio1.Play();
	}

	public AudioClip[] camina;

	void caminaViking()
	{
		audio1.volume = efectos;
		audio1.clip = attack[Random.Range(0,attack.Length)];
		audio1.Play();
	}

	public AudioClip[] golpeado;

	void golpeadoViking()
	{
		audio1.volume = efectos;
		audio1.clip = golpeado[Random.Range(0,golpeado.Length)];
		audio1.Play();
	}

	public AudioClip falli;
	void fall()
	{
		audio1.volume = efectos;
		audio1.clip = falli;
		audio1.Play();
	}

	public AudioClip gunmini;
	void minigun()
	{
		audio1.volume = efectos;
		audio1.clip = gunmini;
		audio1.Play();
	} 	
}
