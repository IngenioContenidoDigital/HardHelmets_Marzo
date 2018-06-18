using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Spine.Unity;

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
	void Start ()
	{
		
	}
	public GameObject si1;
	public GameObject si2;
	public GameObject si3;
	public GameObject si4;

	[SpineAnimation]
	public string entrada2;

	public bool hablar;
	public bool cosiatar;
	// Update is called once per frame
	void Update ()
	{
		if(cosiatar)
		{
			cosiatar = false;
			StartCoroutine(EspEntra());
		}
		mostrar = master.GetComponent<Menu>().mensajes;

		//yield return new WaitForSpineAnimationComplete(animacion.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0));
		//animacion.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "idle", false);hablarcorto
		/*if(GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0, "hablarcorto") || GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0, "hablarmedio") ||  GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0, "hablarlargo"))
		{
			print("hablando");
			//GetComponent<SkeletonGraphic>().SetInteger("habla", 0);
		}*/
		/*if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("entrada"))
		{
			GetComponent<Animator>().SetBool("entrada", false);
			//GetComponent<Animator>().SetBool("salir", false);
		}
		if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("entrada2"))
		{
			GetComponent<Animator>().SetBool("salir", false);
		}*/

		if(GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0).Animation.Name == "hablarcorto" || GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0).Animation.Name == "hablarmedio" || GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0).Animation.Name == "hablarlargo")
		{
			if(!hablar)
			{
				hablar = true;
				StartCoroutine(EspHabla());
			}
		}else
		{
			hablar = false;
		}

		if(see)
		{
			if(mostrar == "salir")
			{
				//GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "hablarcorto", false);
				//GetComponent<Animator>().SetInteger("habla", 1);
				mensajeSalir.SetActive(true);
				eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(si4);
				//EspHabla();
			}
			if(mostrar == "tutorial")
			{
				//GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "hablarcorto", false);
				//GetComponent<Animator>().SetInteger("habla", 1);
				tutorial.SetActive(true);
				eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(si3);
				//EspHabla();
			}
			if(mostrar == "comunity")
			{
				//GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "hablarcorto", false);
				//GetComponent<Animator>().SetInteger("habla", 1);
				comunity.SetActive(true);
				eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(si2);
				//EspHabla();
			}
			if(mostrar == "practica")
			{
				//GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "hablarcorto", false);
				//GetComponent<Animator>().SetInteger("habla", 1);
				practica.SetActive(true);
				//EspHabla();
			}
			if(mostrar == "online")
			{
				//GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "hablarcorto", false);
				//GetComponent<Animator>().SetInteger("habla", 1);
				online.SetActive(true);
				eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(si1);
				//EspHabla();
			}
			see = false;
		}
	}
	IEnumerator EspEntra()
	{
		yield return new WaitForSpineAnimationComplete(GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0));
		GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "hablarcorto", false);
	}
	IEnumerator EspHabla()
	{
		yield return new WaitForSpineAnimationComplete(GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0));
		GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "idle", false);
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
		StopAllCoroutines();
		hide();
		GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "entrada2", false);

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
		StopAllCoroutines();
		hide();
		GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "entrada2", false);
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
