using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Prototype.NetworkLobby;

public class LobbyButtonSearch : MonoBehaviour {

	public GameObject padre;

	public GameObject boton;
	public GameObject boton2;

	public Button este;

	public Navigation navegar;

	public string direccion;

	public string buscar;
	public string buscar2;

	public string direccion2;
	public string buscar3;


	// Use this for initialization
	void Start ()
	{
		navegar = este.navigation;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Player == "server")
		{
			boton = GameObject.Find(buscar);
			boton2 = GameObject.Find(buscar3);
		}else
		{
			boton = GameObject.Find(buscar2);
			boton2 = GameObject.Find(buscar3);
		}

		if(padre.GetComponent<ChangeLevel>().set)
		{
			boton.GetComponent<Button>().enabled = false;
		}

		if(boton != null)
		{
			navegar.mode = Navigation.Mode.Explicit;

			if(direccion == "derecha")
			{
				navegar.selectOnRight = boton.GetComponent<Button>();
			}
			if(direccion == "izquierda")
			{
				navegar.selectOnLeft = boton.GetComponent<Button>();
			}
			if(direccion2 == "derecha")
			{
				navegar.selectOnRight = boton2.GetComponent<Button>();
			}
			este.navigation = navegar;
		}
	}
	public string Player;
}
