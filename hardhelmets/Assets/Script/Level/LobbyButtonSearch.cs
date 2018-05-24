using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Prototype.NetworkLobby;

public class LobbyButtonSearch : MonoBehaviour {

	public GameObject padre;

	public GameObject boton;

	public Button este;

	public Navigation navegar;

	public string direccion;

	public string buscar;
	public string buscar2;


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
			print("buscando 1");
			boton = GameObject.Find(buscar);
		}else
		{
			boton = GameObject.Find(buscar2);
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
			este.navigation = navegar;
		}
	}
	public string Player;
}
