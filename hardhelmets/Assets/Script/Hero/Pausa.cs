using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.ImageEffects;
using UnityEngine.UI;

public class Pausa : MonoBehaviour {

	public string pantalla;

	public GameObject Player;

	public EventSystem eventsystem;
	public GameObject carta1;
	public GameObject blood1;
	public GameObject options1;
	public GameObject si1;

	public GameObject principal;
	public GameObject Opciones;
	public GameObject mesaje;

	public GameObject pad;
	public GameObject teclado;

	//DETECTAR MANDO CONECTADO
	private int Xbox_One_Controller = 0;
	private int PS4_Controller = 0;

	public string mando;

	public bool press;
	public int sumar;

	// Use this for initialization
	void Start ()
	{
		//OPCIONES
		musica = Mathf.RoundToInt(PlayerPrefs.GetFloat("musica"));
		violencia = PlayerPrefs.GetInt("violencia");
	}

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

		//DETECTAR MANDO CONECTADO
		string[] names = Input.GetJoystickNames();

		for(int x = 0; x < names.Length; x++)
		{
			print(names[x].Length);
			if(names[x].Length == 33)
			{
				print("XBOX CONNECTED");
				Xbox_One_Controller = 1;
				PS4_Controller = 0;
				mando = "XBOX";
			}else if(names[x].Length == 19)
			{
				print("PS4 CONNECTED");
				PS4_Controller = 1;
				Xbox_One_Controller = 0;
				mando = "PS4";
			}else
			{
				print("TECLADO");
				PS4_Controller = 0;
				Xbox_One_Controller = 0;
				mando = "TECLADO";
			}
		}
		//OPCIONES
		if(musica == 0)
		{
			musicaT.text = "OFF";
		}
		if(musica == 1)
		{
			musicaT.text = "ON";
		}
		PlayerPrefs.SetFloat("musica",musica);

		if(violencia == 0)
		{
			violenciaT.text = "OFF";
		}
		if(violencia == 1)
		{
			violenciaT.text = "ON";
		}
		PlayerPrefs.SetInt("violencia",violencia);

		if(Input.GetButtonDown("Cancel") && !press)
		{
			Regresar();
			press = true;
		}
		if(press)
		{
			sumar += 1;
			if(sumar >= 2)
			{
				sumar = 0;
				press = false;
			}
		}
	}

	public void Resume()
	{
		pantalla = "";
		Time.timeScale = 1;
		Player.GetComponent<Hero>().SniperCam.GetComponent<Grayscale>().enabled = false;
		eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(carta1);
		gameObject.SetActive(false);
	}
	public void Options()
	{
		pantalla = "opciones";
		principal.SetActive(false);
		Opciones.SetActive(true);
		eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(blood1);
	}
	public int violencia;
	public Text violenciaT;
	public void blood()
	{
		if(violencia == 0)
		{
			violencia = 1;
		}else if(violencia == 1)
		{
			violencia = 0;
		}
	}
	public int musica;
	public Text musicaT;
	public void music()
	{
		if(musica == 0)
		{
			musica = 1;
		}else if(musica == 1)
		{
			musica = 0;
		}
	}
	public void Control()
	{
		pantalla = "control";

		if (mando == "PS4")
		{
			pad.SetActive(true);

		}else if (mando == "XBOX")
		{
			pad.SetActive(true);
		}else
		{
			teclado.SetActive(true);
		}

		eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(null);
	}

	public void Salir()
	{
		pantalla = "salir";

		mesaje.SetActive(true);
		eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(si1);
	}
	public void Si()
	{
		pantalla = "";
		Application.LoadLevel("Load");
		loading.nombre = "menu";
	}
	public void No()
	{
		pantalla = "";
		mesaje.SetActive(false);
		eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(options1);
	}

	public void Regresar ()
	{
		if(pantalla == "opciones")
		{
			pantalla = "";

			Opciones.SetActive(false);
			principal.SetActive(true);

			eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(options1);
		}else if(pantalla == "control")
		{
			pantalla = "opciones";

			pad.SetActive(false);
			teclado.SetActive(false);

			eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(blood1);
		}else if(pantalla == "salir")
		{
			No();
		}else
		{
			Resume();
		}
	}
}
