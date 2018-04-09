using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class volumenMusica : MonoBehaviour {

	float musica;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		musica = PlayerPrefs.GetFloat("musica");
		GetComponent<AudioSource>().volume = musica;
	}
}
