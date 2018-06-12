﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Spine.Unity;

public class CommunityList : MonoBehaviour {

	public EventSystem eventsystem;

	public GameObject cartas1;
	public GameObject cartbutton;
	public GameObject lvButton;
	public GameObject list;
	public GameObject yes;

	//public int cantidad;

	public bool up;

	public float mover;

	public GameObject Selector;

	public bool top;
	public bool down;
	public float mover2;

	public string nivel;

	// Use this for initialization
	void Start ()
	{

	}
	public bool zona;

	public bool soltar;

	public GameObject selectedObj;
	// Update is called once per frame
	void Update ()
	{
		//RESELECCIONAR ELEMENTO DE MENU
		//eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(m1);
		if (eventsystem.GetComponent<EventSystem>().currentSelectedGameObject == null)
		{
			eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(selectedObj);
			//EventSystem.current.SetSelectedGameObject(selectedObj);
		}

		selectedObj = eventsystem.GetComponent<EventSystem>().currentSelectedGameObject;

		//CAMBIE ENTRE CONTROL Y TECLADO
		if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
		{
			eventsystem.GetComponent<StandaloneInputModule>().horizontalAxis = "Horizontal";
			eventsystem.GetComponent<StandaloneInputModule>().verticalAxis = "Vertical";
		}

		if(Input.GetButtonDown("HorizontalUI") || Input.GetButtonDown("VerticalUI"))
		{
			eventsystem.GetComponent<StandaloneInputModule>().horizontalAxis = "HorizontalUI";
			eventsystem.GetComponent<StandaloneInputModule>().verticalAxis = "VerticalUI";
		}

		nivel = GetComponent<changeLevelOffline>().nivel.ToString();

		if(zona && !soltar)
		{
			if(Input.GetButtonDown("up") && CommunityListObject.activo != "primero" || Input.GetAxis("Vertical") > 0 && CommunityListObject.activo != "primero" || Input.GetAxis("VerticalUI") > 0 && CommunityListObject.activo != "primero")
			{
				if(!top)
				{
					Selector.GetComponent<RectTransform>().anchoredPosition = new Vector3(Selector.GetComponent<RectTransform>().anchoredPosition.x,Selector.GetComponent<RectTransform>().anchoredPosition.y+mover2);
				}else
				{
					content.GetComponent<RectTransform>().anchoredPosition = new Vector3(content.GetComponent<RectTransform>().anchoredPosition.x,content.GetComponent<RectTransform>().anchoredPosition.y-mover);
				}
				soltar = true;
				StartCoroutine(momento());
			}
			//ABAJO
			if(Input.GetButtonDown("down") && CommunityListObject.activo != "ultimo" || Input.GetAxis("Vertical") < 0 && CommunityListObject.activo != "ultimo" || Input.GetAxis("VerticalUI") < 0 && CommunityListObject.activo != "ultimo")
			{
				if(!down)
				{
					Selector.GetComponent<RectTransform>().anchoredPosition = new Vector3(Selector.GetComponent<RectTransform>().anchoredPosition.x,Selector.GetComponent<RectTransform>().anchoredPosition.y-mover2);
				}else
				{
					content.GetComponent<RectTransform>().anchoredPosition = new Vector3(content.GetComponent<RectTransform>().anchoredPosition.x,content.GetComponent<RectTransform>().anchoredPosition.y+mover);
				}
				soltar = true;
				StartCoroutine(momento());
			}
		}
		if(pregunta)
		{
			pantalla = "letrero";

			capitan.SetActive(true);
			capitan.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "entrada", false);
			mensaje.SetActive(true);

			nombrea.text = user;
			levela.text = level;
			banderasa.text = banderas;
			torresa.text = torres;

			rango.GetComponent<combinedSkins>().skinsToCombine[1] = skinFondo;
			rango.GetComponent<combinedSkins>().skinsToCombine[2] = skinBorde;
			rango.GetComponent<combinedSkins>().skinsToCombine[3] = skinRango;

			eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(yes);
			StartCoroutine(EspHabla());
			pregunta = false;
		}
		if(Input.GetButtonDown("Cancel"))
		{
			regresar();
		}
	}
	IEnumerator EspHabla()
	{
		yield return new WaitForSpineAnimationComplete(capitan.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0));
		capitan.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "hablarcorto", false);
		StartCoroutine(EspHabla2());
	}
	IEnumerator EspHabla2()
	{
		yield return new WaitForSpineAnimationComplete(capitan.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0));
		capitan.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "idle", false);
	}
	IEnumerator momento()
	{
		yield return new WaitForSeconds(0.2f);
		soltar = false;
	}

	public string pantalla;
	public GameObject cards;
	public void cartas()
	{
		pantalla = "cartas";

		cards.SetActive(true);
		cards.GetComponent<Animator>().SetBool("sale", false);
		cards.GetComponent<Animator>().SetBool("entra", true);
		eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(cartas1);
	}

	public GameObject mano;
	public GameObject mensajecartas;
	public void regresar()
	{
		if(pantalla == "cartas")
		{
			if(mano.GetComponent<Mano>().guardar)
			{
				cards.GetComponent<Animator>().SetBool("entra", false);
				cards.GetComponent<Animator>().SetBool("sale", true);
				eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(cartbutton);
			}else
			{
				mensajecartas.SetActive(true);
				StartCoroutine(esconder());
			}
			pantalla = "";
		}else if(pantalla == "letrero")
		{
			negar();
		}else if(pantalla == "level")
		{
			GetComponent<changeLevelOffline>().azar = false;
			GetComponent<changeLevelOffline>().escenarios.GetComponent<Animator>().SetBool("sale", true);
			eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(cartbutton);
			pantalla = "";
		}else
		{
			Application.LoadLevel("Load");
			loading.nombre = "menu";
		}
	}
	public bool pregunta;
	public GameObject capitan;
	public GameObject mensaje;

	public Text nombrea;
	public Text levela;
	public Text banderasa;
	public Text torresa;
	public GameObject rango;

	public string user;
	public string level;
	public string banderas;
	public string torres;

	public string skinFondo;
	public string skinBorde;
	public string skinRango;

	public void aceptar ()
	{
		//PlayerPrefs.SetString("nameCommunity", nombre);
		//PlayerPrefs.SetInt("levelCommunity", level);
		PlayerPrefs.SetString("FondoCommunity", skinFondo);
		PlayerPrefs.SetString("BordeCommunity", skinBorde);

		Application.LoadLevel("Load2");
		loading2.nombre = "ComunityMatch"+nivel;
	}

	public void negar()
	{
		pantalla = "";
		capitan.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "entrada2", false);
		mensaje.SetActive(false);
		eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(list);
	}
	public void escenario()
	{
		pantalla = "level";
		eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(lvButton);
	}

	IEnumerator esconder()
	{
		yield return new WaitForSeconds(1);
		mensajecartas.SetActive(false);
	}

	public GameObject content;

	public void seleccionado()
	{
		zona = true;
	}
	public void des()
	{
		zona = false;
	}
}
