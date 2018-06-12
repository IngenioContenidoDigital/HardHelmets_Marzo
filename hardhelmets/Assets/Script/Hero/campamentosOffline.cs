using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityStandardAssets.ImageEffects;
using Spine.Unity;
using UnityEngine.EventSystems;

public class campamentosOffline : MonoBehaviour {

	public GameObject Player;

	public GameObject[] camp;

	public GameObject uno;
	public GameObject dos;
	public GameObject tres;

	public string BaseBuena;
	public GameObject[] Bases;
	public GameObject A1;
	public GameObject B1;

	public GameObject BasePrimaria;

	public GameObject alpha;
	public Sprite alpha1;
	public Sprite alpha2;
	public Sprite alpha3;
	public Sprite alpha4;

	public GameObject alphaEnemy;
	public Sprite alphaEnemy1;
	public Sprite alphaEnemy2;

	public GameObject beta;
	public Sprite beta1;
	public Sprite beta2;
	public Sprite beta3;
	public Sprite beta4;

	public GameObject betaEnemy;
	public Sprite betaEnemy1;
	public Sprite betaEnemy2;

	public string buena;
	public string mala;

	public GameObject Camara;

	public int nace;

	public GameObject Cabeza;


	// Use this for initialization
	void Start ()
	{
		Cabeza = Player.GetComponent<Hero>().cabeza;
	}

	public GameObject selectedObj;
	public EventSystem eventsystem;

	public bool contar;
	public float cuentaatras;
	public Text contador;

	public string idioma;
	// Update is called once per frame
	void Update ()
	{
		idioma = PlayerPrefs.GetString("idioma");

		if(contar)
		{
			Player.GetComponent<Hero>().salud = 0;

			nace = 0;
			BasePrimaria.SetActive(false);
			alpha.SetActive(false);
			beta.SetActive(false);

			uno.SetActive(false);
			dos.SetActive(false);
			tres.SetActive(false);

			alphaEnemy.SetActive(false);
			betaEnemy.SetActive(false);

			if(idioma == "ENGLISH")
			{
				contador.text = "Waiting..."+cuentaatras.ToString("F0");
			}
			if(idioma == "SPANISH")
			{
				contador.text = "Esperando..."+cuentaatras.ToString("F0");
			}
			if(idioma == "CHINESE")
			{
				contador.text = "等候..."+cuentaatras.ToString("F0");
			}
			cuentaatras -= Time.deltaTime;
			if(cuentaatras <= 0)
			{
				BasePrimaria.SetActive(true);

				eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(BasePrimaria);

				contar = false;
			}
		}else
		{
			contador.text = "";

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

			if(Player.tag == "Player")
			{
				buena = "BaseBuena";
				mala = "BaseMala";

				alpha.GetComponent<Image>().sprite = alpha1;
				SpriteState spritestate = new SpriteState();
				spritestate.highlightedSprite = alpha2;
				alpha.GetComponent<Button>().spriteState = spritestate;

				alphaEnemy.GetComponent<Image>().sprite = alphaEnemy2;

			}else
			{
				buena = "BaseMala";
				mala = "BaseBuena";

				alpha.GetComponent<Image>().sprite = alpha3;
				SpriteState spritestate = new SpriteState();
				spritestate.highlightedSprite = alpha4;
				alpha.GetComponent<Button>().spriteState = spritestate;

				alphaEnemy.GetComponent<Image>().sprite = alphaEnemy1;
			}
			if(Camara == null)
			{
				Camara = GameObject.FindGameObjectWithTag("MainCamera");
			}

			if(A1 == null)
			{
				A1 = GameObject.Find("ALPHA");
			}else if(A1.tag == buena)
			{
				Bases[0] = A1;
				alphaEnemy.SetActive(false);
			}else if(A1.tag == mala)
			{
				Bases[0] = null;
				alphaEnemy.SetActive(true);
			}else
			{
				alphaEnemy.SetActive(false);
				Bases[0] = null;
			}

			if(B1 == null)
			{
				B1 = GameObject.Find("BETA");
			}else if(B1.tag == buena)
			{
				Bases[1] = B1;
			}else if(B1.tag == mala)
			{
				betaEnemy.SetActive(true);
			}else
			{
				Bases[1] = null;
			}

			if(Bases[0] != null)
			{
				alpha.SetActive(true);
			}else
			{
				alpha.SetActive(false);
			}

			if(Bases[1] != null)
			{
				beta.SetActive(true);
			}else
			{
				beta.SetActive(false);
			}

			if(camp[0] != null)
			{
				uno.SetActive(true);
			}else
			{
				uno.SetActive(false);
			}
			if(camp[1] != null)
			{
				dos.SetActive(true);
			}else
			{
				dos.SetActive(false);
			}
			if(camp[2] != null)
			{
				tres.SetActive(true);
			}else
			{
				tres.SetActive(false);
			}
		}
	}

	public void Base()
	{
		nace = 0;
		Camara.GetComponent<Cam>().campamento = true;
		Camara.GetComponent<Cam>().campPos = new Vector3(Player.GetComponent<Animaciones>().inicial.x, Player.transform.position.y+12, Player.GetComponent<Animaciones>().inicial.z-100);
	}

	public void primero()
	{
		nace = 1;
		Camara.GetComponent<Cam>().campamento = true;
		Camara.GetComponent<Cam>().campPos = new Vector3(camp[0].transform.position.x, Player.transform.position.y+12, camp[0].transform.position.z-100);//camp[0].transform.position;
	}
	public void segundo()
	{
		nace = 2;
		Camara.GetComponent<Cam>().campamento = true;
		Camara.GetComponent<Cam>().campPos = new Vector3(camp[1].transform.position.x, Player.transform.position.y+12, camp[1].transform.position.z-100);//camp[0].transform.position;
	}
	public void tercero()
	{
		nace = 3;
		Camara.GetComponent<Cam>().campamento = true;
		Camara.GetComponent<Cam>().campPos = new Vector3(camp[2].transform.position.x, Player.transform.position.y+12, camp[2].transform.position.z-100);//camp[0].transform.position;
	}
	public void A()
	{
		nace = 5;
		Camara.GetComponent<Cam>().campamento = true;
		Camara.GetComponent<Cam>().campPos = new Vector3(Bases[0].transform.position.x, Player.transform.position.y+12, Bases[0].transform.position.z-100);//Bases[0].transform.position;
	}
	public void B()
	{
		nace = 6;
		Camara.GetComponent<Cam>().campamento = true;
		Camara.GetComponent<Cam>().campPos = new Vector3(Bases[1].transform.position.x, Player.transform.position.y+12, Bases[1].transform.position.z-100);//Bases[0].transform.position;
	}
	public void nacer()
	{
		if(nace == 0)
		{
			Player.GetComponent<Hero>().salud = Player.GetComponent<Hero>().saludMax;
			Player.GetComponent<Hero>().vivo = true;
			Player.GetComponent<Animator>().SetBool("muerto", false);
			//Player.GetComponent<Hero>().mascara = "Player";
			Player.GetComponent<Hero>().Base.layer = LayerMask.NameToLayer("Vista");
			Player.transform.position = new Vector3(Player.GetComponent<Animaciones>().inicial.x, Player.GetComponent<Animaciones>().inicial.y+10, Player.GetComponent<Animaciones>().inicial.z);//Player.GetComponent<Animaciones>().inicial;
		}else if(nace == 1)
		{
			Player.GetComponent<Hero>().salud = Player.GetComponent<Hero>().saludMax;
			Player.GetComponent<Hero>().vivo = true;
			Player.GetComponent<Animator>().SetBool("muerto", false);
			//Player.GetComponent<Hero>().mascara = "Player";
			Player.GetComponent<Hero>().Base.layer = LayerMask.NameToLayer("Vista");
			Player.transform.position = new Vector3(camp[0].transform.position.x, Player.GetComponent<Animaciones>().inicial.y+10, camp[0].transform.position.z);

			nace = 0;
		}else if(nace == 2)
		{
			Player.GetComponent<Hero>().salud = Player.GetComponent<Hero>().saludMax;
			Player.GetComponent<Hero>().vivo = true;
			Player.GetComponent<Animator>().SetBool("muerto", false);
			//Player.GetComponent<Hero>().mascara = "Player";
			Player.GetComponent<Hero>().Base.layer = LayerMask.NameToLayer("Vista");
			Player.transform.position = new Vector3(camp[1].transform.position.x, Player.GetComponent<Animaciones>().inicial.y+10, camp[1].transform.position.z);

			nace = 0;
		}else if(nace == 3)
		{
			Player.GetComponent<Hero>().salud = Player.GetComponent<Hero>().saludMax;
			Player.GetComponent<Hero>().vivo = true;
			Player.GetComponent<Animator>().SetBool("muerto", false);
			//Player.GetComponent<Hero>().mascara = "Player";
			Player.GetComponent<Hero>().Base.layer = LayerMask.NameToLayer("Vista");
			Player.transform.position = new Vector3(camp[2].transform.position.x, Player.GetComponent<Animaciones>().inicial.y+10, camp[2].transform.position.z);

			nace = 0;
		}else if(nace == 5)
		{
			Player.GetComponent<Hero>().salud = Player.GetComponent<Hero>().saludMax;
			Player.GetComponent<Hero>().vivo = true;
			Player.GetComponent<Animator>().SetBool("muerto", false);
			//Player.GetComponent<Hero>().mascara = "Player";
			Player.GetComponent<Hero>().Base.layer = LayerMask.NameToLayer("Vista");
			Player.transform.position = new Vector3(Bases[0].transform.position.x, Player.GetComponent<Animaciones>().inicial.y+10, Bases[0].transform.position.z-3);

			nace = 0;
		}else if(nace == 6)
		{
			Player.GetComponent<Hero>().salud = Player.GetComponent<Hero>().saludMax;
			Player.GetComponent<Hero>().vivo = true;
			Player.GetComponent<Animator>().SetBool("muerto", false);
			//Player.GetComponent<Hero>().mascara = "Player";
			Player.GetComponent<Hero>().Base.layer = LayerMask.NameToLayer("Vista");
			Player.transform.position = new Vector3(Bases[1].transform.position.x, Player.GetComponent<Animaciones>().inicial.y+10, Bases[1].transform.position.z-3);

			nace = 0;
		}

		BasePrimaria.SetActive(false);
		alpha.SetActive(false);
		beta.SetActive(false);

		uno.SetActive(false);
		dos.SetActive(false);
		tres.SetActive(false);

		alphaEnemy.SetActive(false);
		betaEnemy.SetActive(false);

		Player.GetComponent<Hero>().animacion.SetActive(false);
		Player.GetComponent<CustomFinal>().skinsToCombine[0] = Player.GetComponent<CustomFinal>().casco;
		Player.GetComponent<CustomFinal>().listo = false;
		Cabeza.GetComponent<Cabeza>().tirosCabeza = 0;

		Camara.GetComponent<Cam>().campamento = false;
		Player.GetComponent<Hero>().eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(Player.GetComponent<Hero>().carta1);
		Player.GetComponent<Hero>().SniperCam.GetComponent<Grayscale>().enabled = false;

		Player.GetComponent<Hero>().animacion.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "simple", false);
		Player.GetComponent<Hero>().esconderBarra.SetActive(true);

		nace = 0;
	}
}
