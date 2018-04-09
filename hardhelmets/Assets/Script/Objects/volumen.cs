using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class volumen : MonoBehaviour {

	//VOLUMEN
	float efectos;
	//float musica;

	// Use this for initialization
	void Start ()
	{
		efectos = PlayerPrefs.GetFloat("efects");
		//musica = PlayerPrefs.GetFloat("musica");
		GetComponent<AudioSource>().volume = efectos;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
