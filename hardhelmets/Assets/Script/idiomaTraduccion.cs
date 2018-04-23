using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idiomaTraduccion : MonoBehaviour {

	public string idioma;

	public string ingles;
	public string español;
	public string chino;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		idioma = PlayerPrefs.GetString("idioma");

		if(idioma == "ENGLISH")
		{
			GetComponent<UnityEngine.UI.Text>().text = ingles;
		}
		if(idioma == "SPANISH")
		{
			GetComponent<UnityEngine.UI.Text>().text = español;
		}
		if(idioma == "CHINESE")
		{
			GetComponent<UnityEngine.UI.Text>().text = chino;
		}
	}
}
