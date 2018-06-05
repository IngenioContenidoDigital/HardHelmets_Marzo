using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sirena : MonoBehaviour {

	public string enemigo;

	public ParticleSystem uno;
	public ParticleSystem dos;
	public AudioSource sonar;

	public int contar;
	public bool reproducir;

	public bool silenciar;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(contar >= 1 && !reproducir)
		{
			reproducir = true;
			silenciar = false;
		}
		if(contar <= 0)
		{
			contar = 0;
			silenciar = true;
			reproducir = false;
		}

		if(reproducir && !sonar.isPlaying)
		{
			uno.Play();
			dos.Play();
			sonar.Play();
		}

		if(silenciar && sonar.isPlaying)
		{
			uno.Stop();
			dos.Stop();
			sonar.Stop();
		}
	}

	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == enemigo)
		{
			contar ++;
		}
	}

	void OnTriggerExit (Collider col)
	{
		if(col.gameObject.tag == enemigo)
		{
			contar --;
		}
	}
}
