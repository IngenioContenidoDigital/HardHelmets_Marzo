using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Spine.Unity;
using UnityEngine.Networking;
using Steamworks;

public class Menu : MonoBehaviour {

	public Vector3 nextPosition;

	float valor = 0.05f;

	public float h;
	public float v;

	Vector3 inicial;
	Vector3 limites;

	public bool mover;

	bool start;

	public bool iniciar;
	public bool MenuA;

	public bool MenuB;
	public bool baraja;
	public bool barajaRegresa;

	public bool desenfocar;

	//OBJETO A ANIMAR
	public GameObject logo;
	public GameObject any;
	public GameObject anyES;
	public GameObject anyCH;


	public string pantalla;

	//MENU 1
	public GameObject menu1;

	//MENU 2
	public GameObject menu2;
	public UnityEngine.UI.Text descripcion;
	public GameObject baul;
	public GameObject baul2;

	public GameObject hero;

	//ABRIR BAULES
	public GameObject boton;
	int cajas;
	public UnityEngine.UI.Text cajasT;


	//MENU SLECCIONAR CARTAS
	public GameObject Select;
	//MENU PERFIL DE JUGADOR
	public GameObject Profile;
	public GameObject Profilea;
	public GameObject Profileb;
	public GameObject Profilec;
	//MENU CUSTOMIZACION PERSONAJE
	public GameObject customCharacter;
	public bool enfocar;
	public bool enfocarCafeza;
	public bool enfocarCara;
	public bool enfocarMask;
	public bool enfocarCuerpo;
	public bool enfocarMaleta;
	//SKIN INICIAL
	string skinactualcara;
	string skinactualcasco;
	string skinactualmascara;
	string skinactualshemag;
	string skinactualabrigo;
	string skinactualchaleco;
	string skinactualmaleta;

	//BOTON ATRAS
	public GameObject regresar;

	//STEAM
	[Header ("UI Component")]
	public string SteamName;


	//ORDEN DE BOTONES
	//MENU 1
	public GameObject m1;
	public GameObject s1;
	public GameObject o1;
	public GameObject e1;
	//MENU 2
	public GameObject m2;
	public GameObject r1;
	public GameObject c1;
	public GameObject ch1;
	public GameObject ch2;
	public GameObject ch3;
	public GameObject ch4;
	public GameObject ch5;
	public GameObject ch6;
	public GameObject ch7;
	public GameObject atras1;
	public GameObject card1;
	public GameObject t1;
	public GameObject p1;
	public GameObject arriB;
	public GameObject arriF;
	public GameObject pro1;

	public GameObject mensajecartasCapitan;
	public bool capitan;

	void Start ()
	{
		if(SteamManager.Initialized)
		{
			//Display User Name
			SteamName = SteamFriends.GetPersonaName();
			PlayerPrefs.SetString("SteamName", SteamName);
		}
		//Cursor.visible = true;
		if(PlayerPrefs.GetInt("FirstTime") == 0)
		{
			PlayerPrefs.SetString("idioma", "ENGLISH");

			PlayerPrefs.SetInt("Victorias", 0);
			PlayerPrefs.SetInt("Derrotas", 0);
			PlayerPrefs.SetInt("Empates", 0);

			PlayerPrefs.SetInt("PlayerLevel", 1);
			PlayerPrefs.SetFloat("PlayerEX", 0);
			PlayerPrefs.SetInt("Bases", 0);
			PlayerPrefs.SetInt("Banderas", 0);
			//POR PRIMERA VEZ DESBLOQUEA TODO
			PlayerPrefs.SetFloat("voice",1);
			PlayerPrefs.SetFloat("efects",1);
			PlayerPrefs.SetFloat("musica",1);
			PlayerPrefs.SetInt("violencia",1);
			//SETTING DE JUGADOR
			PlayerPrefs.SetString("antiAliasing", "HIGH");
			PlayerPrefs.SetString("ambientOclusion", "HIGH");
			PlayerPrefs.SetInt("motionBlur", 1);
			PlayerPrefs.SetInt("bloom", 1);
			PlayerPrefs.SetInt("dephOfFlied", 1);
			PlayerPrefs.SetInt("globalFog", 1);
			PlayerPrefs.SetInt("volumetricImage", 1);
			PlayerPrefs.SetString("sunShaft", "HIGH");
			//DESBLOQUEA TODAS LAS CARTAS
			PlayerPrefs.SetInt("card1", 1);
			PlayerPrefs.SetInt("card2", 0);
			PlayerPrefs.SetInt("card3", 0);
			PlayerPrefs.SetInt("card4", 0);
			PlayerPrefs.SetInt("card5", 0);
			PlayerPrefs.SetInt("card6", 0);
			PlayerPrefs.SetInt("card7", 0);
			PlayerPrefs.SetInt("card8", 0);
			PlayerPrefs.SetInt("card9", 0);
			PlayerPrefs.SetInt("card10", 1);
			PlayerPrefs.SetInt("card11", 0);
			PlayerPrefs.SetInt("card12", 0);
			PlayerPrefs.SetInt("card13", 0);
			PlayerPrefs.SetInt("card14", 0);
			PlayerPrefs.SetInt("card15", 0);
			PlayerPrefs.SetInt("card16", 0);
			PlayerPrefs.SetInt("card17", 0);
			PlayerPrefs.SetInt("card18", 0);
			PlayerPrefs.SetInt("card19", 0);
			PlayerPrefs.SetInt("card20", 0);
			PlayerPrefs.SetInt("card21", 0);
			PlayerPrefs.SetInt("card22", 0);
			PlayerPrefs.SetInt("card23", 0);
			PlayerPrefs.SetInt("card24", 0);
			//VALOR DE USO DE CARTAS
			PlayerPrefs.SetInt("1costo", 20);//RIFLE
			PlayerPrefs.SetInt("2costo", 30);//SUBMETRA
			PlayerPrefs.SetInt("3costo", 30);//ESCOPETA
			PlayerPrefs.SetInt("4costo", 40);//METRA 
			PlayerPrefs.SetInt("5costo", 70);//SNIPER
			PlayerPrefs.SetInt("6costo", 80);//LANZA LLAMAS
			PlayerPrefs.SetInt("7costo", 30);//ROCKET
			PlayerPrefs.SetInt("8costo", 40);//GRANADAS
			PlayerPrefs.SetInt("9costo", 30);//MUNICIONES
			PlayerPrefs.SetInt("10costo", 20);//FUSILERO
			PlayerPrefs.SetInt("11costo", 30);//SUBMETRALLETO
			PlayerPrefs.SetInt("12costo", 30);//ESCOPETO
			PlayerPrefs.SetInt("13costo", 50);//MEDICO
			PlayerPrefs.SetInt("14costo", 30);//MG
			PlayerPrefs.SetInt("15costo", 40);//MORTERO
			PlayerPrefs.SetInt("16costo", 30);//METRALLETO
			PlayerPrefs.SetInt("17costo", 90);//LLAMERO
			PlayerPrefs.SetInt("18costo", 60);//BAZOOKERO
			PlayerPrefs.SetInt("19costo", 80);//VIKINGO
			PlayerPrefs.SetInt("20costo", 90);//TANK
			PlayerPrefs.SetInt("21costo", 80);//TORRETA BALA
			PlayerPrefs.SetInt("22costo", 100);//TORRETA MISIL
			PlayerPrefs.SetInt("23costo", 70);//CAMPAMENTO
			PlayerPrefs.SetInt("24costo", 70);//MINA
			//CANTIDAD DE CAJAS de cartas
			PlayerPrefs.SetInt("caja1",0);
			PlayerPrefs.SetInt("caja2",0);
			PlayerPrefs.SetInt("caja3",0);
			//SELECCIONA UNA BARAJA BASICA
			PlayerPrefs.SetInt("Mano1", 1);
			PlayerPrefs.SetInt("Mano2", 10);
			PlayerPrefs.SetInt("Mano3", 0);
			PlayerPrefs.SetInt("Mano4", 0);
			PlayerPrefs.SetInt("Mano5", 0);
			PlayerPrefs.SetInt("Mano6", 0);
			PlayerPrefs.SetInt("Mano7", 0);
			PlayerPrefs.SetInt("Mano8", 0);
			PlayerPrefs.SetInt("Mano9", 0);
			PlayerPrefs.SetInt("Mano10", 0);
			//CANTIDAD DE CARTAS EN LA MANO


			//MONEDAS
			PlayerPrefs.SetInt("monedas",0);
			//VALOR DE VENTA DE CARTAS
			PlayerPrefs.SetInt("card1valor", 60);
			PlayerPrefs.SetInt("card2valor", 80);
			PlayerPrefs.SetInt("card3valor", 80);
			PlayerPrefs.SetInt("card4valor", 80);
			PlayerPrefs.SetInt("card5valor", 100);
			PlayerPrefs.SetInt("card6valor", 100);
			PlayerPrefs.SetInt("card7valor", 60);
			PlayerPrefs.SetInt("card8valor", 50);
			PlayerPrefs.SetInt("card9valor", 60);
			PlayerPrefs.SetInt("card10valor", 60);
			PlayerPrefs.SetInt("card11valor", 60);
			PlayerPrefs.SetInt("card12valor", 120);
			PlayerPrefs.SetInt("card13valor", 90);
			PlayerPrefs.SetInt("card14valor", 90);
			PlayerPrefs.SetInt("card15valor", 90);
			PlayerPrefs.SetInt("card16valor", 120);
			PlayerPrefs.SetInt("card17valor", 120);
			PlayerPrefs.SetInt("card18valor", 150);
			PlayerPrefs.SetInt("card19valor", 150);
			PlayerPrefs.SetInt("card20valor", 150);
			PlayerPrefs.SetInt("card21valor", 150);
			PlayerPrefs.SetInt("card22valor", 150);
			PlayerPrefs.SetInt("card23valor", 150);
			PlayerPrefs.SetInt("card24valor", 150);
			/*//CANTIDAD DE CADA CARTA
			PlayerPrefs.SetInt("card1cantidad", 2);
			PlayerPrefs.SetInt("card2cantidad", 1);
			PlayerPrefs.SetInt("card3cantidad", 1);
			PlayerPrefs.SetInt("card4cantidad", 1);
			PlayerPrefs.SetInt("card5cantidad", 1);
			PlayerPrefs.SetInt("card6cantidad", 1);
			PlayerPrefs.SetInt("card7cantidad", 1);
			PlayerPrefs.SetInt("card8cantidad", 1);
			PlayerPrefs.SetInt("card9cantidad", 1);
			PlayerPrefs.SetInt("card10cantidad", 1);
			PlayerPrefs.SetInt("card11cantidad", 1);
			PlayerPrefs.SetInt("card12cantidad", 1);
			PlayerPrefs.SetInt("card13cantidad", 1);
			PlayerPrefs.SetInt("card14cantidad", 1);
			PlayerPrefs.SetInt("card15cantidad", 1);
			PlayerPrefs.SetInt("card16cantidad", 1);
			PlayerPrefs.SetInt("card17cantidad", 1);
			PlayerPrefs.SetInt("card18cantidad", 1);
			PlayerPrefs.SetInt("card19cantidad", 1);
			PlayerPrefs.SetInt("card20cantidad", 1);
			PlayerPrefs.SetInt("card21cantidad", 1);
			PlayerPrefs.SetInt("card22cantidad", 1);
			PlayerPrefs.SetInt("card23cantidad", 1);
			PlayerPrefs.SetInt("card24cantidad", 1);*/

			//AVATAR DE PERSONAJE
			PlayerPrefs.SetString("avatar", "avatar1");
			PlayerPrefs.SetString("borde", "borde1");
			PlayerPrefs.SetString("fondo", "fondo1");
			//SELECCIONA EL SKIN BASICO
			PlayerPrefs.SetString("casco","casco1");
			PlayerPrefs.SetString("cara","cara1");
			//BAULES
			/*PlayerPrefs.SetInt("caja1", 3);
			PlayerPrefs.SetInt("caja2", 2);
			PlayerPrefs.SetInt("caja3", 2);*/
			//DEJA DE SER LA PRIMERA VEZ
			//PlayerPrefs.SetInt("FirstTimeCartas",1);
			PlayerPrefs.SetInt("FirstTime",1);
		}
		idioma = PlayerPrefs.GetString("idioma");

		//OPCIONES
		musica = Mathf.RoundToInt(PlayerPrefs.GetFloat("musica"));
		violencia = PlayerPrefs.GetInt("violencia");

		//GRAPHIC SETTING
		antiAliasing = PlayerPrefs.GetString("antiAliasing");
		ambientOclusion = PlayerPrefs.GetString("ambientOclusion");
		motionBlur = PlayerPrefs.GetInt("motionBlur");
		bloom = PlayerPrefs.GetInt("bloom");
		dephOfFlied = PlayerPrefs.GetInt("dephOfFlied");
		globalFog = PlayerPrefs.GetInt("globalFog");
		volumetricImage = PlayerPrefs.GetInt("volumetricImage");
		sunShaft = PlayerPrefs.GetString("sunShaft");

		//efectos = PlayerPrefs.GetFloat("efects");

		mover = true;
		limites = transform.position;

		pantalla = "inicio";
		skinactualcara = hero.GetComponent<customTow>().carab;
		skinactualcasco = hero.GetComponent<customTow>().cascob;
		skinactualmascara = hero.GetComponent<customTow>().mascarab;
		skinactualshemag = hero.GetComponent<customTow>().cuellob;
		skinactualabrigo = hero.GetComponent<customTow>().uniformeb;
		skinactualchaleco = hero.GetComponent<customTow>().chalecob;
		skinactualmaleta = hero.GetComponent<customTow>().maletab;
	}
	public GameObject selectedObj;
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
		//eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(m1);

		cajas = PlayerPrefs.GetInt("caja1");

		if(cajas >= 1)
		{
			boton.SetActive(true);
		}else
		{
			boton.SetActive(false);
		}
		if(pantalla == "menu2")
		{
			global.SetActive(true);
		}else
		{
			global.SetActive(false);
		}
		if(pantalla == "esconder" || pantalla == "inicio")
		{
			regresar.SetActive(false);
		}else
		{
			regresar.SetActive(true);
		}
		if(pantalla != "cofre")
		{
			baul2.GetComponent<Cofre>().open = false;
		}

		cajasT.text = cajas.ToString();
		if(mover && !MenuA && !MenuB && !baraja && !barajaRegresa)
		{
			h = valor * Input.GetAxis("Mouse X");
			v = valor * Input.GetAxis("Mouse Y");

			nextPosition = new Vector3(transform.position.x+h, transform.position.y+v, transform.position.z);

			transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * 20);

			//LIMITES DE LA CAMARA
			if(transform.position.x >= limites.x+1)
			{
				transform.position = new Vector3(limites.x+1, transform.position.y, transform.position.z);
			}
			if(transform.position.x <= limites.x-1)
			{
				transform.position = new Vector3(limites.x-1, transform.position.y, transform.position.z);
			}
			if(transform.position.y >= limites.y+0.5f)
			{
				transform.position = new Vector3(transform.position.x, limites.y+0.5f, transform.position.z);
			}
			if(transform.position.y <= limites.y-0.5f)
			{
				transform.position = new Vector3(transform.position.x, limites.y-0.5f, transform.position.z);
			}
		}

		if(Input.anyKey && !start)
		{
			menu1.SetActive(true);
			eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(m1);
			StartCoroutine(esperaAnim());
			mover = false;
			iniciar = true;
			start = true;

			S_Iniciar();
		}
		if(Input.GetButtonDown("Cancel"))
		{
			Regresar();
		}

		if(iniciar)
		{
			any.GetComponent<Animator>().SetBool("bajar", true);
			anyES.GetComponent<Animator>().SetBool("bajar", true);
			anyCH.GetComponent<Animator>().SetBool("bajar", true);
			logo.GetComponent<Animator>().SetBool("bajar", true);
			nextPosition = new Vector3(153.6f, 3.6f, transform.position.z);

			transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * 1);
			if(transform.position.x > 153.2f)
			{
				Destroy(any);
				Destroy(anyES);
				Destroy(anyCH);

				if(pantalla == "inicio")
				{
					pantalla = "menu1";
				}

				limites = transform.position;
				mover = true;
				iniciar = false;
			}
		}

		if(MenuA && !MenuB)
		{
			nextPosition = new Vector3(174.3f, 3.6f, transform.position.z);

			transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * 1);
			if(transform.position.x > 174f)
			{
				//pantalla = "menu2";
				limites = transform.position;
				mover = true;
				menu2.SetActive(true);
				menu2.GetComponent<Animator>().SetBool("entra", true);
				if(PlayerPrefs.GetInt("FirstTimeCartas") == 0)//FirstTimeCartas
				{
					pantalla = "capitanaso";
					PlayerPrefs.SetInt("FirstTimeCartas",1);
					mensajecartasCapitan.SetActive(true);
					iniciar = false;
				}else
				{
					capitan = true;
				}
				if(capitan)
				{
					pantalla = "menu2";
					eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(m2);
					StartCoroutine(esperaAnim());
					MenuA = false;
				}
			}
		}

		if(MenuB && !MenuA && !baraja)
		{
			nextPosition = new Vector3(154.3f, 3.6f, transform.position.z);

			transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * 1);
			if(transform.position.x < 154.6f)
			{
				pantalla = "menu1";
				limites = transform.position;
				mover = true;
				menu1.GetComponent<Animator>().SetBool("entra", true);
				eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(m1);
				StartCoroutine(esperaAnim());
				MenuB = false;
			}
		}

		if(baraja && !MenuB && !barajaRegresa)
		{
			nextPosition = new Vector3(190f, 3.6f, transform.position.z);

			transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * 1);
			if(transform.position.x > 189.6f)
			{
				pantalla = "baraja";
				limites = transform.position;
				mover = true;
				Select.SetActive(true);
				Select.GetComponent<Animator>().SetBool("entra", true);
				eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(card1);
				baraja = false;
			}
		}
		if(barajaRegresa && !baraja)
		{
			nextPosition = new Vector3(174.3f, 3.6f, transform.position.z);

			transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * 1);
			if(transform.position.x < 174.6f)
			{
				pantalla = "menu2";
				limites = transform.position;
				mover = true;
				menu2.GetComponent<Animator>().SetBool("sale", false);
				menu2.GetComponent<Animator>().SetBool("entra", true);
				eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(m2);
				barajaRegresa = false;
			}
		}
		if(desenfocar)
		{
			nextPosition = new Vector3(174.3f, 3.6f, 131.9f);

			transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * 1);
			if(transform.position.x < 174.7f && transform.position.z < 132.3f)
			{
				pantalla = "menu2";
				eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(m2);
				limites = transform.position;
				mover = true;
				desenfocar = false;
			}
		}
		//MOVER CAMARA A PERSONAJE
		if(enfocar)
		{
			nextPosition = new Vector3(176, 3.6f, 137);

			transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * 1);
			if(transform.position.x > 175.6f && transform.position.z > 136.6f)
			{
				pantalla = "customization";
				eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(ch1);
				limites = transform.position;
				enfocar = false;
			}
		}
		if(enfocarCafeza)
		{
			enfocar = false;

			nextPosition = new Vector3(176, 4.4f, transform.position.z);

			transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * 2);
			if(transform.position.y > 4.34f && transform.position.y < 4.44f && transform.position.x < 176.4f)
			{
				limites = transform.position;
				enfocarCafeza = false;
			}
		}
		if(enfocarCara)
		{
			enfocar = false;

			nextPosition = new Vector3(176, 4.2f, transform.position.z);

			transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * 2);
			if(transform.position.y > 4.16f && transform.position.y < 4.24f && transform.position.x < 176.4f)
			{
				limites = transform.position;
				enfocarCara = false;
			}
		}
		if(enfocarMask)
		{
			enfocar = false;

			nextPosition = new Vector3(176, 4f, transform.position.z);

			transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * 2);
			if(transform.position.y > 3.96f && transform.position.y < 4.04f && transform.position.x < 176.4f)
			{
				limites = transform.position;
				enfocarMask = false;
			}
		}
		if(enfocarCuerpo)
		{
			enfocar = false;

			nextPosition = new Vector3(176, 3.8f, transform.position.z);

			transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * 2);
			if(transform.position.y > 3.76f && transform.position.y < 3.84f && transform.position.x < 176.4f)
			{
				limites = transform.position;
				enfocarCuerpo = false;
			}
		}
		if(enfocarMaleta)
		{
			enfocar = false;

			nextPosition = new Vector3(178, 3.8f, transform.position.z);

			transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * 2);
			if(transform.position.x > 177.6f)
			{
				limites = transform.position;
				enfocarMaleta = false;
			}
		}
		//RESWET ANIMATIONS
		if(Profile.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("ProfileEntra"))
		{
			Profile.GetComponent<Animator>().SetBool("entra", false);
		}
		if(Profile.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("ProfileSale"))
		{
			Profile.GetComponent<Animator>().SetBool("entra", false);
			Profile.GetComponent<Animator>().SetBool("sale", false);
		}
		if(Profile.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("ProfileIdle"))
		{
			Profile.GetComponent<Animator>().SetBool("sale", false);
		}
		//customCharacter
		if(customCharacter.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("PanelEntra"))
		{
			customCharacter.GetComponent<Animator>().SetBool("entra", false);
		}
		if(customCharacter.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("PanelSale"))
		{
			customCharacter.GetComponent<Animator>().SetBool("entra", false);
			customCharacter.GetComponent<Animator>().SetBool("sale", false);
		}
		if(customCharacter.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("PanelIdle"))
		{
			customCharacter.GetComponent<Animator>().SetBool("sale", false);
		}
		//CAMBIO DE SKINS A PERSONAJE
		skinchange = skincustom+skinactual;

		if(skincustom == "cara")
		{
			skinmax = 5;
			hero.GetComponent<customTow>().cara = "cara"+skinactualcara;
		}
		if(skincustom == "casco")
		{
			skinmax = 5;
			hero.GetComponent<customTow>().casco = "casco"+skinactualcasco;
		}
		if(skincustom == "mascara")
		{
			skinmax = 2;
			hero.GetComponent<customTow>().mascara = "mascara"+skinactualmascara;
		}
		if(skincustom == "shemag")
		{
			skinmax = 2;
			hero.GetComponent<customTow>().cuello = "shemag"+skinactualshemag;
		}
		if(skincustom == "abrigo")
		{
			skinmax = 2;
			hero.GetComponent<customTow>().uniforme = "abrigo"+skinactualabrigo;
		}
		if(skincustom == "chaleco")
		{
			skinmax = 2;
			hero.GetComponent<customTow>().chaleco = "chaleco"+skinactualchaleco;
		}
		if(skincustom == "maleta")
		{
			skinmax = 3;
			hero.GetComponent<customTow>().maleta = "maleta"+skinactualmaleta;
		}
		//OPCIONES
		if(musica == 0)
		{
			if(idioma == "ENGLISH")
			{
				musicaT.text = "OFF";
			}
			if(idioma == "SPANISH")
			{
				musicaT.text = "APAGADO";
			}
			if(idioma == "CHINESE")
			{
				musicaT.text = "关闭";
			}
		}
		if(musica == 1)
		{
			if(idioma == "ENGLISH")
			{
				musicaT.text = "ON";
			}
			if(idioma == "SPANISH")
			{
				musicaT.text = "ENCENDIDO";
			}
			if(idioma == "CHINESE")
			{
				musicaT.text = "上";
			}
		}
		PlayerPrefs.SetFloat("musica",musica);

		if(violencia == 0)
		{
			if(idioma == "ENGLISH")
			{
				violenciaT.text = "OFF";
			}
			if(idioma == "SPANISH")
			{
				violenciaT.text = "APAGADO";
			}
			if(idioma == "CHINESE")
			{
				violenciaT.text = "关闭";
			}
		}
		if(violencia == 1)
		{
			if(idioma == "ENGLISH")
			{
				violenciaT.text = "ON";
			}
			if(idioma == "SPANISH")
			{
				violenciaT.text = "ENCENDIDO";
			}
			if(idioma == "CHINESE")
			{
				violenciaT.text = "上";
			}
		}
		PlayerPrefs.SetInt("violencia",violencia);
		//LENGUAJE
		PlayerPrefs.SetString("idioma",idioma);
		if(idioma == "ENGLISH")
		{
			idiomaT.text = PlayerPrefs.GetString("idioma");
		}
		if(idioma == "SPANISH")
		{
			idiomaT.text= "ESPAÑOL";
		}
		if(idioma == "CHINESE")
		{
			idiomaT.text = "中文";
		}
		//SETTINGS
		PlayerPrefs.SetString("antiAliasing",antiAliasing);
		if(idioma == "ENGLISH")
		{
			antiAliasingT.text = PlayerPrefs.GetString("antiAliasing");
		}
		if(idioma == "SPANISH")
		{
			if(antiAliasing == "LOWEST")
			{
				antiAliasingT.text = "MUY BAJO";
			}else if(antiAliasing == "LOW")
			{
				antiAliasingT.text = "BAJO";
			}else if(antiAliasing == "MEDIUM")
			{
				antiAliasingT.text = "MEDIO";
			}else if(antiAliasing == "HIGH")
			{
				antiAliasingT.text = "ALTO";
			}else if(antiAliasing == "ULTRA")
			{
				antiAliasingT.text = "ULTRA";
			}
		}
		if(idioma == "CHINESE")
		{
			if(antiAliasing == "LOWEST")
			{
				antiAliasingT.text = "最低";
			}else if(antiAliasing == "LOW")
			{
				antiAliasingT.text = "低";
			}else if(antiAliasing == "MEDIUM")
			{
				antiAliasingT.text = "中";
			}else if(antiAliasing == "HIGH")
			{
				antiAliasingT.text = "高";
			}else if(antiAliasing == "ULTRA")
			{
				antiAliasingT.text = "超";
			}
		}
			
		PlayerPrefs.SetString("ambientOclusion",ambientOclusion);
		if(idioma == "ENGLISH")
		{
			ambientOclusionT.text = PlayerPrefs.GetString("ambientOclusion");
		}
		if(idioma == "SPANISH")
		{
			if(ambientOclusion == "LOWEST")
			{
				ambientOclusionT.text = "MUY BAJO";
			}else if(ambientOclusion == "LOW")
			{
				ambientOclusionT.text = "BAJO";
			}else if(ambientOclusion == "MEDIUM")
			{
				ambientOclusionT.text = "MEDIO";
			}else if(ambientOclusion == "HIGH")
			{
				ambientOclusionT.text = "ALTO";
			}else if(ambientOclusion == "ULTRA")
			{
				ambientOclusionT.text = "ULTRA";
			}
		}
		if(idioma == "CHINESE")
		{
			if(ambientOclusion == "LOWEST")
			{
				ambientOclusionT.text = "最低";
			}else if(ambientOclusion == "LOW")
			{
				ambientOclusionT.text = "低";
			}else if(ambientOclusion == "MEDIUM")
			{
				ambientOclusionT.text = "中";
			}else if(ambientOclusion == "HIGH")
			{
				ambientOclusionT.text = "高";
			}else if(ambientOclusion == "ULTRA")
			{
				ambientOclusionT.text = "超";
			}
		}

		if(motionBlur == 0)
		{
			if(idioma == "ENGLISH")
			{
				motionBlurT.text = "OFF";
			}
			if(idioma == "SPANISH")
			{
				motionBlurT.text = "APAGADO";
			}
			if(idioma == "CHINESE")
			{
				motionBlurT.text = "关闭";
			}
		}
		if(motionBlur == 1)
		{
			if(idioma == "ENGLISH")
			{
				motionBlurT.text = "ON";
			}
			if(idioma == "SPANISH")
			{
				motionBlurT.text = "ENCENDIDO";
			}
			if(idioma == "CHINESE")
			{
				motionBlurT.text = "上";
			}
		}
		PlayerPrefs.SetInt("motionBlur",motionBlur);

		if(bloom == 0)
		{
			if(idioma == "ENGLISH")
			{
				bloomT.text = "OFF";
			}
			if(idioma == "SPANISH")
			{
				bloomT.text = "APAGADO";
			}
			if(idioma == "CHINESE")
			{
				bloomT.text = "关闭";
			}
		}
		if(bloom == 1)
		{
			if(idioma == "ENGLISH")
			{
				bloomT.text = "ON";
			}
			if(idioma == "SPANISH")
			{
				bloomT.text = "ENCENDIDO";
			}
			if(idioma == "CHINESE")
			{
				bloomT.text = "上";
			}
		}
		PlayerPrefs.SetInt("bloom",bloom);

		if(dephOfFlied == 0)
		{
			if(idioma == "ENGLISH")
			{
				dephOfFliedT.text = "OFF";
			}
			if(idioma == "SPANISH")
			{
				dephOfFliedT.text = "APAGADO";
			}
			if(idioma == "CHINESE")
			{
				dephOfFliedT.text = "关闭";
			}
		}
		if(dephOfFlied == 1)
		{
			if(idioma == "ENGLISH")
			{
				dephOfFliedT.text = "ON";
			}
			if(idioma == "SPANISH")
			{
				dephOfFliedT.text = "ENCENDIDO";
			}
			if(idioma == "CHINESE")
			{
				dephOfFliedT.text = "上";
			}
		}
		PlayerPrefs.SetInt("dephOfFlied",dephOfFlied);

		if(globalFog == 0)
		{
			if(idioma == "ENGLISH")
			{
				globalFogT.text = "OFF";
			}
			if(idioma == "SPANISH")
			{
				globalFogT.text = "APAGADO";
			}
			if(idioma == "CHINESE")
			{
				globalFogT.text = "关闭";
			}
		}
		if(globalFog == 1)
		{
			if(idioma == "ENGLISH")
			{
				globalFogT.text = "ON";
			}
			if(idioma == "SPANISH")
			{
				globalFogT.text = "ENCENDIDO";
			}
			if(idioma == "CHINESE")
			{
				globalFogT.text = "上";
			}
		}
		PlayerPrefs.SetInt("globalFog",globalFog);

		if(volumetricImage == 0)
		{
			if(idioma == "ENGLISH")
			{
				volumetricImageT.text = "OFF";
			}
			if(idioma == "SPANISH")
			{
				volumetricImageT.text = "APAGADO";
			}
			if(idioma == "CHINESE")
			{
				volumetricImageT.text = "关闭";
			}
		}
		if(volumetricImage == 1)
		{
			if(idioma == "ENGLISH")
			{
				volumetricImageT.text = "ON";
			}
			if(idioma == "SPANISH")
			{
				volumetricImageT.text = "ENCENDIDO";
			}
			if(idioma == "CHINESE")
			{
				volumetricImageT.text = "上";
			}
		}
		PlayerPrefs.SetInt("volumetricImage",volumetricImage);

		PlayerPrefs.SetString("sunShaft",sunShaft);
		if(idioma == "ENGLISH")
		{
			sunShaftT.text = PlayerPrefs.GetString("sunShaft");
		}
		if(idioma == "SPANISH")
		{
			if(sunShaft == "LOW")
			{
				sunShaftT.text = "BAJO";
			}else if(sunShaft == "NORMAL")
			{
				sunShaftT.text = "NORMAL";
			}else if(sunShaft == "HIGH")
			{
				sunShaftT.text = "ALTO";
			}
		}
		if(idioma == "CHINESE")
		{
			if(sunShaft == "LOW")
			{
				sunShaftT.text = "低";
			}else if(sunShaft == "NORMAL")
			{
				sunShaftT.text = "正常";
			}else if(sunShaft == "HIGH")
			{
				sunShaftT.text = "高";
			}
		}

		//FLECHAS CHARACTER CUSTOMIZATION
		if(Icustom && !wait)
		{
			if(Input.GetButtonDown("left") || Input.GetAxis("Horizontal") < 0 || Input.GetAxis("HorizontalUI") < 0)
			{
				skinAtras();
				wait = true;
				S_Select();
				StartCoroutine(momen());
			}
		}
		if(Dcustom && !wait)
		{
			if(Input.GetButtonDown("right") || Input.GetAxis("Horizontal") > 0 || Input.GetAxis("HorizontalUI") > 0)
			{
				skinAdelante();
				wait = true;
				S_Select();
				StartCoroutine(momen());
			}
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
		//CAMBIA MANDO
		if(Input.GetAxis("Vertical") < 0 || Input.GetAxis("Vertical") > 0)
		{
			print(UnityEngine.Input.GetAxis("Vertical").GetType());
		}
	}
	public bool wait;
	IEnumerator momen ()
	{
		yield return new WaitForSeconds(0.2f);
		wait = false;
	}
	//ESPERAR
	IEnumerator esperaAnim ()
	{
		yield return new WaitForSeconds(0.5f);
		menu1.GetComponent<Animator>().SetBool("entra", false);
		menu2.GetComponent<Animator>().SetBool("entra", false);
		menu1.GetComponent<Animator>().SetBool("sale", false);
		menu2.GetComponent<Animator>().SetBool("sale", false);
	}
	//FUNCIONES BOTONES
	public GameObject global;

	public GameObject graficas;

	public GameObject opciones;
	public GameObject teclado;
	public GameObject control;

	//DETECTAR MANDO CONECTADO
	private int Xbox_One_Controller = 0;
	private int PS4_Controller = 0;

	public string mando;
	//MENU 1
	public void StartGame ()
	{
		pantalla = "";
		menu1.GetComponent<Animator>().SetBool("entra", false);
		menu1.GetComponent<Animator>().SetBool("sale", true);
		mover = false;
		MenuA = true;
	}
	public void settings ()
	{
		pantalla = "settings";
		menu1.GetComponent<Animator>().SetBool("entra", false);
		menu1.GetComponent<Animator>().SetBool("sale", true);
		eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(s1);
		graficas.SetActive(true);
		graficas.GetComponent<Animator>().SetBool("sale", false);
		graficas.GetComponent<Animator>().SetBool("entra", true);
	}
	public string antiAliasing;
	public Text antiAliasingT;
	public void Aliasing ()
	{
		if(antiAliasing == "LOWEST")
		{
			antiAliasing = "LOW";
		}else if(antiAliasing == "LOW")
		{
			antiAliasing = "MEDIUM";
		}else if(antiAliasing == "MEDIUM")
		{
			antiAliasing = "HIGH";
		}else if(antiAliasing == "HIGH")
		{
			antiAliasing = "ULTRA";
		}else if(antiAliasing == "ULTRA")
		{
			antiAliasing = "LOWEST";
		}
	}
	public string ambientOclusion;
	public Text ambientOclusionT;
	public void Occlusion ()
	{
		if(ambientOclusion == "LOWEST")
		{
			ambientOclusion = "LOW";
		}else if(ambientOclusion == "LOW")
		{
			ambientOclusion = "MEDIUM";
		}else if(ambientOclusion == "MEDIUM")
		{
			ambientOclusion = "HIGH";
		}else if(ambientOclusion == "HIGH")
		{
			ambientOclusion = "ULTRA";
		}else if(ambientOclusion == "ULTRA")
		{
			ambientOclusion = "LOWEST";
		}
	}
	public int motionBlur;
	public Text motionBlurT;
	public void Blur ()
	{
		if(motionBlur == 0)
		{
			motionBlur = 1;
		}else if(motionBlur == 1)
		{
			motionBlur = 0;
		}
	}
	public int bloom;
	public Text bloomT;
	public void Blomm ()
	{
		if(bloom == 0)
		{
			bloom = 1;
		}else if(bloom == 1)
		{
			bloom = 0;
		}
	}
	public int dephOfFlied;
	public Text dephOfFliedT;
	public void Field ()
	{
		if(dephOfFlied == 0)
		{
			dephOfFlied = 1;
		}else if(dephOfFlied == 1)
		{
			dephOfFlied = 0;
		}
	}
	public int globalFog;
	public Text globalFogT;
	public void Fog ()
	{
		if(globalFog == 0)
		{
			globalFog = 1;
		}else if(globalFog == 1)
		{
			globalFog = 0;
		}
	}
	public int volumetricImage;
	public Text volumetricImageT;
	public void volumetricEffect ()
	{
		if(volumetricImage == 0)
		{
			volumetricImage = 1;
		}else if(volumetricImage == 1)
		{
			volumetricImage = 0;
		}
	}
	public string sunShaft;
	public Text sunShaftT;
	public void Shaft ()
	{
		if(sunShaft == "LOW")
		{
			sunShaft = "NORMAL";
		}else if(sunShaft == "NORMAL")
		{
			sunShaft = "HIGH";
		}else if(sunShaft == "HIGH")
		{
			sunShaft = "LOW";
		}
	}
	public void options ()
	{
		if(idioma == "")
		{
			idioma = "ENGLISH";
		}
		pantalla = "options";
		menu1.GetComponent<Animator>().SetBool("entra", false);
		menu1.GetComponent<Animator>().SetBool("sale", true);
		eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(o1);

		opciones.SetActive(true);
		opciones.GetComponent<Animator>().SetBool("sale", false);
		opciones.GetComponent<Animator>().SetBool("entra", true);
	}
	public string idioma;
	public Text idiomaT;
	public void lenguaje()
	{
		if(idioma == "ENGLISH")
		{
			idioma = "SPANISH";
		}else if(idioma == "SPANISH")
		{
			idioma = "CHINESE";
		}else if(idioma == "CHINESE")
		{
			idioma = "ENGLISH";
		}
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
	public void controles ()
	{
		pantalla = "controles";
		opciones.GetComponent<Animator>().SetBool("entra", false);
		opciones.GetComponent<Animator>().SetBool("sale", true);

		if (mando == "PS4")
		{
			control.SetActive(true);
			control.GetComponent<Animator>().SetBool("sale", false);
			control.GetComponent<Animator>().SetBool("entra", true);

		}else if (mando == "XBOX")
		{
			control.SetActive(true);
			control.GetComponent<Animator>().SetBool("sale", false);
			control.GetComponent<Animator>().SetBool("entra", true);
		}else
		{
			teclado.SetActive(true);
			teclado.GetComponent<Animator>().SetBool("sale", false);
			teclado.GetComponent<Animator>().SetBool("entra", true);
		}
		eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(null);
	}
	public void Credits ()
	{
		Application.LoadLevel("Load");
		loading.nombre = "credits";
	}
	public void Salir ()
	{
		pantalla = "salir";
		mensajes = "salir";
		eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(e1);
		mensaje.SetActive(true);
		mensaje.GetComponent<Animator>().SetBool("entrada", true);

		menu2.GetComponent<Animator>().SetBool("entra", false);
		menu2.GetComponent<Animator>().SetBool("sale", true);
	}
	//MENU 2
	public void MultiplayerText ()
	{
		if(idioma == "ENGLISH")
		{
			descripcion.text = "Face other players in an online match";
		}
		if(idioma == "SPANISH")
		{
			descripcion.text = "Enfréntate a otros jugadores en una partida online";
		}
		if(idioma == "CHINESE")
		{
			descripcion.text = "面对在线比赛中的其他球员";
		}
	}
	public void Multiplayer ()
	{
		//audio1.volume = efectos;
		//audio1.Play();

		pantalla = "online";
		mensajes = "online";
		mensaje.SetActive(true);
		mensaje.GetComponent<Animator>().SetBool("entrada", true);
		eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(r1);

		menu2.GetComponent<Animator>().SetBool("entra", false);
		menu2.GetComponent<Animator>().SetBool("sale", true);
	}
	//PERZONALIZAR CARTAS
	public void CardDeck ()
	{
		pantalla = "";
		mover = false;

		menu2.GetComponent<Animator>().SetBool("entra", false);
		menu2.GetComponent<Animator>().SetBool("sale", true);

		baraja = true;
	}
	//JUGAR COMMUNITY MATCH
	public void comunity ()
	{
		pantalla = "comunity";
		mensajes = "comunity";
		mensaje.SetActive(true);
		mensaje.GetComponent<Animator>().SetBool("entrada", true);
		eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(c1);

		menu2.GetComponent<Animator>().SetBool("entra", false);
		menu2.GetComponent<Animator>().SetBool("sale", true);
	}
	//JUGAR TUTORIAL
	public void tutorial ()
	{
		pantalla = "tutorial";
		mensajes = "tutorial";
		mensaje.SetActive(true);
		mensaje.GetComponent<Animator>().SetBool("entrada", true);
		eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(t1);

		menu2.GetComponent<Animator>().SetBool("entra", false);
		menu2.GetComponent<Animator>().SetBool("sale", true);
	}
	//JUGAR PRACTICA
	public void practica ()
	{
		pantalla = "practica";
		mensajes = "practica";
		mensaje.SetActive(true);
		mensaje.GetComponent<Animator>().SetBool("entrada", true);
		eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(p1);

		menu2.GetComponent<Animator>().SetBool("entra", false);
		menu2.GetComponent<Animator>().SetBool("sale", true);
	}
	//ABRIR COFRE
	public void Cofre ()
	{
		hero.GetComponent<Animator>().SetBool("sale", true);

		menu2.GetComponent<Animator>().SetBool("entra", false);
		menu2.GetComponent<Animator>().SetBool("sale", true);


		if(!baul.activeSelf)
		{
			baul.SetActive(true);
		}else
		{
			baul2.GetComponent<Animator>().SetBool("cancelar", false);
			baul2.GetComponent<Animator>().SetBool("reiniciar", true);
		}
		StartCoroutine(momentoCofre());
	}
	IEnumerator momentoCofre()
	{
		yield return new WaitForSeconds(0.4f);
		pantalla = "cofre";
	}
	//PROFILE
	public void Perfil()
	{
		pantalla = "profile";

		Profile.SetActive(true);

		menu2.GetComponent<Animator>().SetBool("entra", false);
		menu2.GetComponent<Animator>().SetBool("sale", true);

		Profile.GetComponent<Animator>().SetBool("entra", true);
		eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(pro1);
	}

	public Animator primero;
	public Animator segundo;
	public Animator tercero;

	public GameObject panelFondo;
	public GameObject panelBorde;
	public GameObject panelAvatar;

	public void Back()
	{
		pantalla = "fondo";
		StartCoroutine(activaFondo());

		tercero.GetComponent<Animator>().SetBool("sale", true);
		segundo.GetComponent<Animator>().SetBool("sale", true);

		primero.GetComponent<Animator>().SetBool("entra", true);
		eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(arriF);
	}
	IEnumerator activaFondo()
	{
		yield return new WaitForSeconds(0.3f);
		panelFondo.GetComponent<PlayerIDPanel>().zona = true;
	}
	public void Middle()
	{
		pantalla = "borde";
		StartCoroutine(activaBorde());

		primero.GetComponent<Animator>().SetBool("sale", true);
		tercero.GetComponent<Animator>().SetBool("sale", true);

		segundo.GetComponent<Animator>().SetBool("entra", true);
		eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(arriB);
	}
	IEnumerator activaBorde()
	{
		yield return new WaitForSeconds(0.3f);
		panelBorde.GetComponent<PlayerIDPanel>().zona = true;
	}
	public void Front()
	{
		StartCoroutine(activaavatar());

		primero.GetComponent<Animator>().SetBool("sale", true);
		segundo.GetComponent<Animator>().SetBool("sale", true);

		tercero.GetComponent<Animator>().SetBool("entra", true);
	}
	IEnumerator activaavatar()
	{
		pantalla = "avatar";
		yield return new WaitForSeconds(0.3f);
		panelAvatar.GetComponent<PlayerIDPanel>().zona = true;
	}
	//CHARACTER CUSTOMIZATION
	public void CharacterCustomization ()
	{
		pantalla = "";

		mover = false;

		desenfocar = false;
		enfocar = true;

		hero.GetComponent<Animator>().SetBool("sale", false);
		hero.GetComponent<Animator>().SetBool("entra", true);

		menu2.GetComponent<Animator>().SetBool("entra", false);
		menu2.GetComponent<Animator>().SetBool("sale", true);

		customCharacter.SetActive(true);
		customCharacter.GetComponent<Animator>().SetBool("entra", true);
	}
	public void Helmet ()
	{
		eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(atras1);
		pantalla = "helmet";
		skincustom = "casco";

		mover = false;

		enfocarCara = false;
		enfocarMask = false;
		enfocarCuerpo = false;
		enfocarMaleta = false;

		enfocarCafeza = true;
	}
	public void Head ()
	{
		eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(atras1);
		pantalla = "head";
		skincustom = "cara";

		mover = false;

		enfocarCafeza = false;
		enfocarMask = false;
		enfocarCuerpo = false;
		enfocarMaleta = false;

		enfocarCara = true;
	}
	public void masck()
	{
		eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(atras1);
		pantalla = "masck";
		skincustom = "mascara";

		mover = false;

		enfocarCafeza = false;
		enfocarCara = false;
		enfocarCuerpo = false;
		enfocarMaleta = false;

		enfocarMask = true;
	}
	public void neck()
	{
		eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(atras1);
		pantalla = "neck";
		skincustom = "shemag";

		mover = false;

		enfocarCafeza = false;
		enfocarMask = false;
		enfocarCuerpo = false;
		enfocarMaleta = false;

		enfocarCara = true;
	}
	public void vest()
	{
		eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(atras1);
		pantalla = "vest";
		skincustom = "chaleco";

		mover = false;

		enfocarCafeza = false;
		enfocarCara = false;
		enfocarMask = false;
		enfocarMaleta = false;

		enfocarCuerpo = true;
	}
	public void Bag()
	{
		eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(atras1);
		pantalla = "bag";
		skincustom = "maleta";

		mover = false;

		enfocarCafeza = false;
		enfocarCara = false;
		enfocarMask = false;
		enfocarCuerpo = false;

		enfocarMaleta = true;
	}
	public void overcoat()
	{
		eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(atras1);
		pantalla = "overcoat";
		skincustom = "abrigo";

		mover = false;

		enfocarCafeza = false;
		enfocarCara = false;
		enfocarMask = false;
		enfocarMaleta = false;

		enfocarCuerpo = true;
	}

	public string skincustom;

	public string skinchange;

	public int skinmin;
	public int skinmax;
	public int skinactual;

	public bool Dcustom;
	public bool Icustom;

	public void customI()
	{
		Dcustom = false;
		Icustom = true;
	}
	public void customD()
	{
		Icustom = false;
		Dcustom = true;
	}

	public void skinAtras()
	{
		if(skinactual <= skinmin)
		{
			skinactual = skinmax;
		}else
		{
			skinactual -= 1;
		}

		if(skincustom == "cara")
		{
			skinactualcara = skinactual.ToString();
		}
		if(skincustom == "casco")
		{
			skinactualcasco = skinactual.ToString();
		}
		if(skincustom == "mascara")
		{
			skinactualmascara = skinactual.ToString();
		}
		if(skincustom == "shemag")
		{
			skinactualshemag = skinactual.ToString();
		}
		if(skincustom == "abrigo")
		{
			skinactualabrigo = skinactual.ToString();
		}
		if(skincustom == "chaleco")
		{
			skinactualchaleco = skinactual.ToString();
		}
		if(skincustom == "maleta")
		{
			skinactualmaleta = skinactual.ToString();
		}
	}
	public void skinAdelante()
	{
		if(skinactual >= skinmax)
		{
			skinactual = skinmin;
		}else
		{
			skinactual += 1;
		}
		if(skincustom == "cara")
		{
			skinactualcara = skinactual.ToString();
		}
		if(skincustom == "casco")
		{
			skinactualcasco = skinactual.ToString();
		}
		if(skincustom == "mascara")
		{
			skinactualmascara = skinactual.ToString();
		}
		if(skincustom == "shemag")
		{
			skinactualshemag = skinactual.ToString();
		}
		if(skincustom == "abrigo")
		{
			skinactualabrigo = skinactual.ToString();
		}
		if(skincustom == "chaleco")
		{
			skinactualchaleco = skinactual.ToString();
		}
		if(skincustom == "maleta")
		{
			skinactualmaleta = skinactual.ToString();
		}
	}


	//TEXT
	public void CommunityText ()
	{
		if(idioma == "ENGLISH")
		{
			descripcion.text = "Play offline against AI generated players";
		}
		if(idioma == "SPANISH")
		{
			descripcion.text = "Juega una partida en solitario contra jugadores generados aleatoriamente";
		}
		if(idioma == "CHINESE")
		{
			descripcion.text = "与AI生成的玩家离线玩";
		}
	}
	public void CustomizationText ()
	{
		if(idioma == "ENGLISH")
		{
			descripcion.text = "Customize player's appearance";
		}
		if(idioma == "SPANISH")
		{
			descripcion.text = "Personaliza la apariencia de tu jugador";
		}
		if(idioma == "CHINESE")
		{
			descripcion.text = "自定义播放器的外观";
		}
	}
	public void CardDeckText ()
	{
		if(idioma == "ENGLISH")
		{
			descripcion.text = "Prepare the deck of war bonds you'll use in the field.";
		}
		if(idioma == "SPANISH")
		{
			descripcion.text = "Selecciona tus bonos de guerra";
		}
		if(idioma == "CHINESE")
		{
			descripcion.text = "准备你将在现场使用的甲板战争债券";
		}
	}
	public void TutorialText ()
	{
		if(idioma == "ENGLISH")
		{
			descripcion.text = "Get to learn the basics.";
		}
		if(idioma == "SPANISH")
		{
			descripcion.text = "Aprende lo básico para jugar ";
		}
		if(idioma == "CHINESE")
		{
			descripcion.text = "学习基础知识";
		}
	}
	public void PracticeText ()
	{
		if(idioma == "ENGLISH")
		{
			descripcion.text = "Train to improve your skills and gain some ability to command your men";
		}
		if(idioma == "SPANISH")
		{
			descripcion.text = "Mejora tus habilidades para comandar tu ejército";
		}
		if(idioma == "CHINESE")
		{
			descripcion.text = "训练提高你的技能并获得一些指挥你的人的能力";
		}
	}
	public void ProfileText ()
	{
		if(idioma == "ENGLISH")
		{
			descripcion.text = "Customize your player id to show you personality in front of other players";
		}
		if(idioma == "SPANISH")
		{
			descripcion.text = "Personaliza tu identificación de jugador para mostrar tu personalidad a los demás jugadores";
		}
		if(idioma == "CHINESE")
		{
			descripcion.text = "自定义您的播放器ID以在其他播放器前显示您的个性";
		}
	}
	//REGRESAR
	public GameObject mano;
	//MENSAJE EN PANTALLA
	public GameObject mensaje;
	public string mensajes;
	public GameObject mensajecartas;

	public void Regresar ()
	{
		if(pantalla == "menu1")
		{
			mensajes = "salir";
			mensaje.SetActive(true);
			mensaje.GetComponent<Animator>().SetBool("entrada", true);
			eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(e1);
		}
		if(pantalla == "settings")
		{
			pantalla = "menu1";

			graficas.GetComponent<Animator>().SetBool("entra", false);
			graficas.GetComponent<Animator>().SetBool("sale", true);

			menu1.GetComponent<Animator>().SetBool("sale", false);
			menu1.GetComponent<Animator>().SetBool("entra", true);
			eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(m1);
		}
		if(pantalla == "options")
		{
			pantalla = "menu1";

			opciones.GetComponent<Animator>().SetBool("entra", false);
			opciones.GetComponent<Animator>().SetBool("sale", true);

			menu1.GetComponent<Animator>().SetBool("sale", false);
			menu1.GetComponent<Animator>().SetBool("entra", true);
			eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(m1);
		}
		if(pantalla == "controles")
		{
			pantalla = "options";

			control.GetComponent<Animator>().SetBool("entra", false);
			control.GetComponent<Animator>().SetBool("sale", true);

			teclado.GetComponent<Animator>().SetBool("entra", false);
			teclado.GetComponent<Animator>().SetBool("sale", true);

			opciones.GetComponent<Animator>().SetBool("sale", false);
			opciones.GetComponent<Animator>().SetBool("entra", true);
			eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(o1);
		}
		if(pantalla == "menu2")
		{
			pantalla = "";

			baul2.GetComponent<Animator>().SetBool("cerrar", true);
			menu2.GetComponent<Animator>().SetBool("entra", false);
			menu2.GetComponent<Animator>().SetBool("sale", true);

			mover = false;
			MenuB = true;
		}
		if(pantalla == "cofre")
		{
			pantalla = "menu2";
			boton.GetComponent<Button>().enabled = true;

			hero.GetComponent<Animator>().SetBool("sale", false);
			hero.GetComponent<Animator>().SetBool("entra", true);

			baul2.GetComponent<Animator>().SetBool("cancelar", true);
			baul2.GetComponent<Cofre>().open = false;
			menu2.GetComponent<Animator>().SetBool("sale", false);
			menu2.GetComponent<Animator>().SetBool("entra", true);
			eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(m2);

		}
		if(pantalla == "baraja")
		{
			if(mano.GetComponent<Mano>().guardar)
			{
				mover = false;

				barajaRegresa = true;

				Select.GetComponent<Animator>().SetBool("sale", true);
			}else
			{
				mensajecartas.SetActive(true);
				StartCoroutine(esconder());
			}
		}
		if(pantalla == "profile")
		{
			pantalla = "menu2";

			Profilea.GetComponent<Animator>().SetBool("sale", true);
			Profileb.GetComponent<Animator>().SetBool("sale", true);
			Profilec.GetComponent<Animator>().SetBool("sale", true);

			Profile.GetComponent<Animator>().SetBool("sale", true);

			menu2.GetComponent<Animator>().SetBool("sale", false);
			menu2.GetComponent<Animator>().SetBool("entra", true);
			eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(m2);
		}
		if(pantalla == "fondo" || pantalla == "borde" || pantalla == "avatar")
		{
			pantalla = "profile";

			Profilea.GetComponent<Animator>().SetBool("sale", true);
			Profileb.GetComponent<Animator>().SetBool("sale", true);
			Profilec.GetComponent<Animator>().SetBool("sale", true);

			eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(pro1);
		}

		if(pantalla == "customization")
		{
			skincustom = "";
			desenfocar = true;

			enfocarCafeza = false;
			enfocarCara = false;
			enfocarMask = false;
			enfocarCuerpo = false;
			enfocarMaleta = false;

			customCharacter.GetComponent<Animator>().SetBool("sale", true);
			eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(m2);

			menu2.GetComponent<Animator>().SetBool("sale", false);
			menu2.GetComponent<Animator>().SetBool("entra", true);
		}
		if(pantalla == "helmet")
		{
			pantalla = "customization";
			Dcustom = false;
			Icustom = false;
			eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(ch1);
		}
		if(pantalla == "head")
		{
			pantalla = "customization";
			Dcustom = false;
			Icustom = false;
			eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(ch2);
		}
		if(pantalla == "masck")
		{
			pantalla = "customization";
			Dcustom = false;
			Icustom = false;
			eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(ch3);
		}
		if(pantalla == "neck")
		{
			pantalla = "customization";
			Dcustom = false;
			Icustom = false;
			eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(ch4);
		}
		if(pantalla == "vest")
		{
			pantalla = "customization";
			Dcustom = false;
			Icustom = false;
			eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(ch5);
		}
		if(pantalla == "bag")
		{
			pantalla = "customization";
			Dcustom = false;
			Icustom = false;
			eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(ch6);
		}
		if(pantalla == "overcoat")
		{
			pantalla = "customization";
			Dcustom = false;
			Icustom = false;
			eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(ch7);
		}
		if(pantalla == "online" || pantalla == "comunity" || pantalla == "tutorial" || pantalla == "practica")
		{
			pantalla = "menu2";
			mensajes = "";

			mensaje.GetComponent<Animator>().SetBool("salir", true);

			menu2.GetComponent<Animator>().SetBool("sale", false);
			menu2.GetComponent<Animator>().SetBool("entra", true);

			eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(m2);
		}
	}
	IEnumerator esconder()
	{
		yield return new WaitForSeconds(1);
		mensajecartas.SetActive(false);
	}

	public EventSystem eventsystem;

	//SONIDOS
	public AudioSource audio1;

	public AudioClip inicio_S;

	public void S_Iniciar ()
	{
		audio1.clip = inicio_S;
		audio1.Play();
	}

	public AudioClip click_S;
	public void S_Click ()
	{
		audio1.clip = click_S;
		audio1.Play();
	}

	public AudioClip select_S;
	public void S_Select ()
	{
		audio1.clip = select_S;
		audio1.Play();
	}
}
