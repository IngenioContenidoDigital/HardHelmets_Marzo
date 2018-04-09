using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class casquillos : MonoBehaviour {

	public AudioClip sonidos;

	//DISPAROS
	void sound ()
	{
		GetComponent<AudioSource>().clip = sonidos;
		GetComponent<AudioSource>().Play();
	}

	void OnCollisionEnter (Collision col)
	{
		sound();
		Destroy(gameObject, 3.0f);
	}
}
