using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Prototype.NetworkLobby;

public class LobbyButtonSearch2 : MonoBehaviour {

	public GameObject boton;

	public Button este;

	public Navigation navegar;

	public string direccion;

	public string buscar;
	public string buscar2;
	public GameObject buscar3;

	public GameObject lista;


	// Use this for initialization
	void Start ()
	{
		navegar = este.navigation;
	}

	// Update is called once per frame
	void Update ()
	{
		if(boton == null)
		{
			if(lista.GetComponent<regresaLobby>().actual == "servidores")
			{
				boton = buscar3;
			}else
			{
				boton = GameObject.Find(buscar2);
			}
		}else
		{
			if(lista.GetComponent<regresaLobby>().actual != "servidores")
			{
				boton = GameObject.Find(buscar2);
			}

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

			if(boton.GetComponent<LobbyButtonSearch>().boton == null)
			{
				boton.GetComponent<LobbyButtonSearch>().boton = gameObject;
			}
		}
	}
}
