using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour {

	float musica;

	void Awake ()
	{
		DontDestroyOnLoad(gameObject);

		if(FindObjectsOfType(GetType()).Length > 1)
		{
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		musica = PlayerPrefs.GetFloat("musica");
		GetComponent<AudioSource>().volume = musica;
		if(Application.loadedLevelName == "LevelNetwork0" || Application.loadedLevelName == "LevelNetwork1" || Application.loadedLevelName == "LevelNetwork2")
		{
			Destroy(gameObject);
		}
		if(Application.loadedLevelName == "ComunityMatch0" || Application.loadedLevelName == "ComunityMatch1" || Application.loadedLevelName == "ComunityMatch2")
		{
			Destroy(gameObject);
		}
		if(Application.loadedLevelName == "Load2")
		{
			Destroy(gameObject);
		}
		if(Application.loadedLevelName == "Tutorial")
		{
			Destroy(gameObject);
		}
		if(Application.loadedLevelName == "Practica")
		{
			Destroy(gameObject);
		}
		if(Application.loadedLevelName == "credits")
		{
			Destroy(gameObject);
		}
		if(Application.loadedLevelName == "Collector")
		{
			Destroy(gameObject);
		}
	}
}
