using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class mensajes : MonoBehaviour {

	public GameObject master;

	public string mostrar;
	public bool see;

	public GameObject mensajeSalir;
	public GameObject tutorial;
	public GameObject comunity;
	public GameObject practica;
	public GameObject online;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		mostrar = master.GetComponent<Menu>().mensajes;

		if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("hablarcorto") || GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("hablarmedio") ||  GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("hablarlargo"))
		{
			GetComponent<Animator>().SetInteger("habla", 0);
		}
		if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("entrada"))
		{
			GetComponent<Animator>().SetBool("entrada", false);
			GetComponent<Animator>().SetBool("salir", false);
		}

		if(see)
		{
			if(mostrar == "salir")
			{
				GetComponent<Animator>().SetInteger("habla", 1);
				mensajeSalir.SetActive(true);
			}
			if(mostrar == "tutorial")
			{
				GetComponent<Animator>().SetInteger("habla", 1);
				tutorial.SetActive(true);
			}
			if(mostrar == "comunity")
			{
				GetComponent<Animator>().SetInteger("habla", 1);
				comunity.SetActive(true);
			}
			if(mostrar == "practica")
			{
				GetComponent<Animator>().SetInteger("habla", 1);
				practica.SetActive(true);
			}
			if(mostrar == "online")
			{
				GetComponent<Animator>().SetInteger("habla", 1);
				online.SetActive(true);
			}
			see = false;
		}
	}

	public void show()
	{
		print("see true");
		see = true;
	}

	public void hide()
	{
		mensajeSalir.SetActive(false);
		tutorial.SetActive(false);
		comunity.SetActive(false);
		practica.SetActive(false);
		online.SetActive(false);
	}
	//SELECCIONAR BOTONES
	public EventSystem eventsystem;
	public GameObject start;
	public GameObject match;

	//OPSIONES DE MENSAJE EN PANTALLA
	public void salirSI()
	{
		Application.Quit();
	}
	public void salirNo()
	{
		GetComponent<Animator>().SetBool("salir", true);

		master.GetComponent<Menu>().pantalla = "menu1";

		master.GetComponent<Menu>().menu1.GetComponent<Animator>().SetBool("sale", false);
		master.GetComponent<Menu>().menu1.GetComponent<Animator>().SetBool("entra", true);
		eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(start);
	}

	public void tutorialSI()
	{
		Application.LoadLevel("Load");
		loading.nombre = "Tutorial";
	}
	public void tutorialNo()
	{
		GetComponent<Animator>().SetBool("salir", true);
		master.GetComponent<Menu>().pantalla = "menu2";

		master.GetComponent<Menu>().menu2.GetComponent<Animator>().SetBool("sale", false);
		master.GetComponent<Menu>().menu2.GetComponent<Animator>().SetBool("entra", true);
		eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(match);
	}
	public void practicaSI()
	{
		Application.LoadLevel("Load");
		loading.nombre = "Practica";
	}
	public void comunitySI()
	{
		Application.LoadLevel("Load");
		loading.nombre = "Comunity";
	}
	public void onlineSI()
	{
		Application.LoadLevel("Load");
		loading.nombre = "Lobby";
	}
}
