using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Spine.Unity;

public class MenuSolitario : MonoBehaviour {

	public string pantalla;
	public string pantalla2;

	public GameObject menu;

	public GameObject scrollNivel;
	public GameObject scrollFaction;

	public int nivel;
	public string level;
	public string factionBuena;
	public string factionMala;

	public int min;
	public Text minText;
	public GameObject minRank;
	public int max;
	public Text maxText;
	public GameObject maxRank;

	public GameObject bloqueado;
	public GameObject bloqueadoN0;
	public GameObject bloqueadoN1;

	public GameObject enter;

	//---MENUS---------
	public GameObject menu1;
	public GameObject menu2;
	public GameObject menu3;
	public GameObject ocultar;
	public GameObject ocultar2;

	//---COFRE---------
	public int cajas;
	public GameObject boton;
	public Text cantidadcajas;
	public GameObject baul;
	public GameObject baul2;
	public GameObject baulcanvas;
	public GameObject cartasCofre;

	//---CARTAS---------
	public GameObject mano;
	public GameObject cards;
	public GameObject mensajecartas;

	//---EVENTSYSTEM---------
	public EventSystem eventsystem;
	public GameObject cartas1;
	public GameObject adelante;
	public GameObject jugar;
	public GameObject yes;
	public GameObject atras;

	// Use this for initialization
	void Start ()
	{
		//PlayerPrefs.SetInt("caja1", 10);
	}
	
	// Update is called once per frame
	void Update ()
	{
		nivel = scrollNivel.GetComponent<ScrollSnap>().cellIndex;

		if(scrollFaction.GetComponent<ScrollSnap>().cellIndex == 0)
		{
			factionBuena = "";
			factionMala = "b";
		}
		if(scrollFaction.GetComponent<ScrollSnap>().cellIndex == 1)
		{
			factionBuena = "b";
			factionMala = "";
		}
		PlayerPrefs.SetString("factionBuena", factionBuena);
		PlayerPrefs.SetString("factionMala", factionMala);

		if(nivel == 0)
		{
			min = 1;
			max = 10;
			if(PlayerPrefs.GetInt("PlayerLevel") < min)
			{
				bloqueadoN0.SetActive(true);
			}
			level = "2";
		}
		if(nivel == 1)
		{
			min = 10;
			max = 20;
			if(PlayerPrefs.GetInt("PlayerLevel") < min)
			{
				bloqueadoN1.SetActive(true);
			}
			level = "1";
		}

		minText.text = min.ToString();
		minRank.GetComponent<combinedSkins>().skinsToCombine[0] = min.ToString();

		maxText.text = max.ToString();
		maxRank.GetComponent<combinedSkins>().skinsToCombine[0] = max.ToString();

		if(PlayerPrefs.GetInt("PlayerLevel") < min)
		{
			bloqueado.SetActive(true);
		}else
		{
			bloqueado.SetActive(false);
		}
		if(pantalla == "cartas" || pantalla == "3" || pantalla == "cofre" || pantalla == "abrir")
		{
			enter.SetActive(false);
		}else
		{
			if(PlayerPrefs.GetInt("PlayerLevel") >= min)
			{
				enter.SetActive(true);
			}else
			{
				enter.SetActive(false);
			}
		}
		//---COFRE---------
		cajas = PlayerPrefs.GetInt("caja1");
		cantidadcajas.text = "X"+cajas.ToString();
		if(cajas >= 1 && pantalla != "cartas")
		{
			boton.SetActive(true);
		}else
		{
			boton.SetActive(false);
		}

		if(pantalla == "cartas")
		{
			ocultar.SetActive(false);
			ocultar2.SetActive(false);
		}

		if(pantalla != "1")
		{
			ocultar.SetActive(true);
		}else
		{
			ocultar.SetActive(false);
		}

		if(pantalla != "2")
		{
			ocultar2.SetActive(true);
		}else
		{
			ocultar2.SetActive(false);
		}

		if(Input.GetButtonDown("Cancel"))
		{
			regresar();
		}
	}
	public void Cofre ()
	{
		pantalla2 = pantalla;
		pantalla = "cofre";


		baul.SetActive(true);
		baulcanvas.GetComponent<CajaCartas2>().reiniciar();
		baul2.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "loop", false);
		eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(baul2);
	}

	public void CofreAbrir ()
	{
		pantalla = "abrir";
		cartasCofre.SetActive(true);
		baul2.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "cofre", false);
	}

	public void cartas()
	{
		pantalla2 = pantalla;
		pantalla = "cartas";

		menu.GetComponent<Animator>().SetBool("entra", false);
		menu.GetComponent<Animator>().SetBool("sale", true);

		cards.SetActive(true);
		cards.GetComponent<Animator>().SetBool("sale", false);
		cards.GetComponent<Animator>().SetBool("entra", true);
		eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(cartas1);
	}
	public void next()
	{
		if(pantalla == "1")
		{
			pantalla = "2";
			menu1.GetComponent<Animator>().SetBool("entra", false);
			menu1.GetComponent<Animator>().SetBool("sale", true);

			menu2.GetComponent<Animator>().SetBool("sale", false);
			menu2.GetComponent<Animator>().SetBool("entra", true);
			return;
		}
		if(pantalla == "2")
		{
			pantalla = "3";
			//menu2.GetComponent<Animator>().SetBool("entra", false);
			//menu2.GetComponent<Animator>().SetBool("sale", true);

			menu3.GetComponent<Animator>().SetBool("sale", false);
			menu3.GetComponent<Animator>().SetBool("entra", true);
			eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(jugar);
			return;
		}
	}
	public void regresar()
	{
		if(pantalla == "cartas")
		{
			if(mano.GetComponent<Mano>().guardar)
			{
				menu.GetComponent<Animator>().SetBool("sale", false);
				menu.GetComponent<Animator>().SetBool("entra", true);

				cards.GetComponent<Animator>().SetBool("entra", false);
				cards.GetComponent<Animator>().SetBool("sale", true);
				if(pantalla2 == "3")
				{
					eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(jugar);
				}else
				{
					eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(adelante);
				}

				pantalla = pantalla2;
				pantalla2 = "";
			}else
			{
				mensajecartas.SetActive(true);
				StartCoroutine(esconder());
			}
			return;
		}
		if(pantalla == "cofre")
		{
			baul.SetActive(false);

			if(pantalla2 == "3")
			{
				eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(jugar);
			}else
			{
				eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(adelante);
			}

			pantalla = pantalla2;
			pantalla2 = "";

			return;
		}
		if(pantalla == "2")
		{
			pantalla = "1";
			menu1.GetComponent<Animator>().SetBool("sale", false);
			menu1.GetComponent<Animator>().SetBool("entra", true);

			menu2.GetComponent<Animator>().SetBool("entra", false);
			menu2.GetComponent<Animator>().SetBool("sale", true);

			eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(adelante);
			return;
		}
		if(pantalla == "3")
		{
			pantalla = "2";
			//menu2.GetComponent<Animator>().SetBool("sale", false);
			//menu2.GetComponent<Animator>().SetBool("entra", true);

			menu3.GetComponent<Animator>().SetBool("entra", false);
			menu3.GetComponent<Animator>().SetBool("sale", true);

			eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(adelante);
			return;
		}
		if(pantalla == "1")
		{
			mensajesalir.SetActive(true);

			eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(yes);
			return;
		}
	}
	public GameObject mensajesalir;
	public void si()
	{
		Application.LoadLevel("Load");
		loading.nombre = "menu";
	}
	public void no()
	{
		mensajesalir.SetActive(false);
		eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(atras);
	}
	IEnumerator esconder()
	{
		yield return new WaitForSeconds(1);
		mensajecartas.SetActive(false);
	}

	public void Terminar()
	{
		Application.LoadLevel("Load");
		loading.nombre = "ComunityMatch"+level;
	}
}
