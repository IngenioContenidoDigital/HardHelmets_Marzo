using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardarStadisticas : MonoBehaviour {

	public string Dato;

	// Use this for initialization
	void Start ()
	{
		PlayerPrefs.SetInt(Dato, PlayerPrefs.GetInt(Dato)+1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
