using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tankSounds : MonoBehaviour {

	//DISPAROS
	public AudioClip acelera;
	public AudioClip desacelera;
	public AudioClip maneja;
	public AudioClip prende;
	public AudioClip apaga;

	//SOURCE
	public AudioSource audio1;

	public AudioSource audioDisparo;

	//VOLUMEN
	float efectos;

	void Start ()
	{
		efectos = PlayerPrefs.GetFloat("efects");
		TurnOn();
	}

	void Update ()
	{
		
	}

	public void Accelerate()
	{
		/*audio1.volume = efectos;
		audio1.clip = acelera;
		audio1.Play();
		audio1.loop = false;*/
	}
	IEnumerator espera ()
	{
		yield return new WaitForSeconds(2);
		drive();
	}
	public void Decelerate()
	{
		audio1.volume = efectos;
		audio1.clip = desacelera;
		audio1.Play();
		audio1.loop = false;
	}

	public void drive()
	{
		audio1.volume = efectos;
		audio1.clip = maneja;
		audio1.Play();
		audio1.loop = true;
	}

	public void TurnOff()
	{
		audio1.volume = efectos;
		audio1.clip = apaga;
		audio1.Play();
		audio1.loop = false;
	}

	public void TurnOn()
	{
		audio1.volume = efectos;
		audio1.clip = prende;
		audio1.Play();
		audio1.loop = false;
	}

	public void canon()
	{
		audioDisparo.volume = efectos;
		audioDisparo.Play();
	}
}
