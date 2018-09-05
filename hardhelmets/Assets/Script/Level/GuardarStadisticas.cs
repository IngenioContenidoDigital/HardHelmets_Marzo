using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardarStadisticas : MonoBehaviour {

	public string Ejercito;
	public string Dato;

	// Use this for initialization
	void Start ()
	{
		if(PlayerPrefs.GetString("factionBuena") == Ejercito)
		{
			Dato = "Victorias";
		}else
		{
			Dato = "Derrotas";
		}

		PlayerPrefs.SetInt(Dato, PlayerPrefs.GetInt(Dato)+1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
