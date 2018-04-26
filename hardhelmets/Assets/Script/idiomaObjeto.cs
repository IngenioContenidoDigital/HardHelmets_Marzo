using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idiomaObjeto : MonoBehaviour {

	public string idioma;

	public GameObject ingles;
	public GameObject español;
	public GameObject chino;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		idioma = PlayerPrefs.GetString("idioma");

		if(idioma == "ENGLISH")
		{
			ingles.SetActive(true);
			español.SetActive(false);
			chino.SetActive(false);
		}
		if(idioma == "SPANISH")
		{
			español.SetActive(true);
			ingles.SetActive(false);
			chino.SetActive(false);
		}
		if(idioma == "CHINESE")
		{
			chino.SetActive(true);
			español.SetActive(false);
			ingles.SetActive(false);
		}
	}
}
