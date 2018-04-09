﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CampRespawnNetwork : NetworkBehaviour {

	public GameObject menu;

	public int lugar;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!isServer && gameObject.tag == "Player")
		{
			return;
		}else if(isServer && gameObject.tag == "enemy")
		{
			return;
		}

		if(menu == null)
		{
			menu = GameObject.FindGameObjectWithTag("Respawn");
		}else if(menu.GetComponent<campamentos>().camp[0] == null && lugar == 0)
		{
			menu.GetComponent<campamentos>().camp[0] = gameObject;
			lugar = 1;
		}else if(menu.GetComponent<campamentos>().camp[1] == null && lugar == 0)
		{
			menu.GetComponent<campamentos>().camp[1] = gameObject;
			lugar = 2;
		}else if(menu.GetComponent<campamentos>().camp[2] == null && lugar == 0)
		{
			menu.GetComponent<campamentos>().camp[2] = gameObject;
			lugar = 3;
		}
	}

	public void muere ()
	{
		if(lugar == 1)
		{
			menu.GetComponent<campamentos>().camp[0] = null;
		}else if(lugar == 2)
		{
			menu.GetComponent<campamentos>().camp[1] = null;
		}else if(lugar == 3)
		{
			menu.GetComponent<campamentos>().camp[2] = null;
		}

		Destroy(gameObject);
	}
}
