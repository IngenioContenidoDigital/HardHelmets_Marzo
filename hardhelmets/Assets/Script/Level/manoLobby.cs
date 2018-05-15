using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manoLobby : MonoBehaviour {

	public string nombre;

	public int carta;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		carta = PlayerPrefs.GetInt("Mano"+nombre);

		GetComponent<combinedSkins>().skinsToCombine[0] = carta.ToString();
	}
}
