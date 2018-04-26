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
			if(ingles != null)
			{
				ingles.SetActive(true);
			}
			if(español != null)
			{
				español.SetActive(false);
			}
			if(chino != null)
			{
				chino.SetActive(false);
			}
		}
		if(idioma == "SPANISH")
		{
			if(español != null)
			{
				español.SetActive(true);
			}
			if(ingles != null)
			{
				ingles.SetActive(false);
			}
			if(chino != null)
			{
				chino.SetActive(false);
			}
		}
		if(idioma == "CHINESE")
		{
			if(chino != null)
			{
				chino.SetActive(true);
			}
			if(español != null)
			{
				español.SetActive(false);
			}
			if(ingles != null)
			{
				ingles.SetActive(false);
			}
		}
	}
}
