using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityStandardAssets.ImageEffects;
using Spine.Unity;

public class campamentos : MonoBehaviour {

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
	public GameObject alphaEnemy;

	public GameObject alpha2;
	public GameObject alphaEnemy2;

	public GameObject beta;
	public GameObject betaEnemy;
	public GameObject beta2;
	public GameObject betaEnemy2;

	public string buena;
	public string mala;

	public GameObject Camara;

	public int nace;

	public GameObject Cabeza;

	public bool contar;
	public float cuentaatras;
	public Text contador;

	// Use this for initialization
	void Start ()
	{
		Cabeza = Player.GetComponent<HeroNetwork>().cabeza;
	}

	public GameObject selectedObj;
	public EventSystem eventsystem;
	// Update is called once per frame
	void Update ()
	{
		if(contar)
		{
			contador.text = "Waiting..."+cuentaatras.ToString("F0");
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
			}else
			{
				buena = "BaseMala";
				mala = "BaseBuena";
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
			}else if(A1.tag == mala)
			{
				Bases[0] = null;
				if(Player.tag == "Player")
				{
					alphaEnemy.SetActive(true);
				}else
				{
					alphaEnemy2.SetActive(true);
				}
			}else
			{
				if(Player.tag == "Player")
				{
					alphaEnemy.SetActive(false);
				}else
				{
					alphaEnemy2.SetActive(false);
				}
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
				Bases[1] = null;
				if(Player.tag == "Player")
				{
					betaEnemy.SetActive(true);
				}else
				{
					betaEnemy2.SetActive(true);
				}
			}else
			{
				if(Player.tag == "Player")
				{
					betaEnemy.SetActive(false);
				}else
				{
					betaEnemy2.SetActive(false);
				}
				Bases[1] = null;
			}

			if(Bases[0] != null)
			{
				if(Player.tag == "Player")
				{
					alpha.SetActive(true);
				}else
				{
					alpha2.SetActive(true);
				}
			}else
			{
				alpha.SetActive(false);
				alpha2.SetActive(false);
			}

			if(Bases[1] != null)
			{
				if(Player.tag == "Player")
				{
					beta.SetActive(true);
				}else
				{
					beta2.SetActive(true);
				}
			}else
			{
				beta.SetActive(false);
				beta2.SetActive(false);
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
		Camara.GetComponent<CamNetwork>().campamento = true;
		Camara.GetComponent<CamNetwork>().campPos = new Vector3(Player.GetComponent<AnimacionesNetwork>().inicial.x, Camara.transform.position.y, Player.GetComponent<AnimacionesNetwork>().inicial.z-40);
	}

	public void primero()
	{
		nace = 1;
		Camara.GetComponent<CamNetwork>().campamento = true;
		Camara.GetComponent<CamNetwork>().campPos = new Vector3(camp[0].transform.position.x, Player.transform.position.y+7, camp[0].transform.position.z-40);//camp[0].transform.position;
	}
	public void segundo()
	{
		nace = 2;
		Camara.GetComponent<CamNetwork>().campamento = true;
		Camara.GetComponent<CamNetwork>().campPos = new Vector3(camp[1].transform.position.x, Player.transform.position.y+7, camp[1].transform.position.z-40);//camp[1].transform.position;
	}
	public void tercero()
	{
		nace = 3;
		Camara.GetComponent<CamNetwork>().campamento = true;
		Camara.GetComponent<CamNetwork>().campPos = new Vector3(camp[2].transform.position.x, Player.transform.position.y+7, camp[2].transform.position.z-40);//camp[2].transform.position;
	}
	public void A()
	{
		nace = 5;
		Camara.GetComponent<CamNetwork>().campamento = true;
		Camara.GetComponent<CamNetwork>().campPos = new Vector3(Bases[0].transform.position.x, Player.transform.position.y+7, Bases[0].transform.position.z-40);//Bases[0].transform.position;
	}
	public void B()
	{
		nace = 6;
		Camara.GetComponent<CamNetwork>().campamento = true;
		Camara.GetComponent<CamNetwork>().campPos = new Vector3(Bases[1].transform.position.x, Player.transform.position.y+7, Bases[1].transform.position.z-40);//Bases[1].transform.position;
	}
	public void nacer()
	{
		if(nace == 0)
		{
			if(Player.tag == "Player")
			{
				Player.GetComponent<HeroNetwork>().salud = Player.GetComponent<HeroNetwork>().saludMax;
				Player.GetComponent<HeroNetwork>().CmdSendSalud(100);
				Player.GetComponent<HeroNetwork>().vivo = true;
				Player.GetComponent<Animator>().SetBool("muerto", false);
				Player.GetComponent<HeroNetwork>().mascara = "Player";
				Player.GetComponent<HeroNetwork>().CmdChangeMascara("Player");
				Player.GetComponent<HeroNetwork>().CmdChangeBase("Vista");
				Player.transform.position = Player.GetComponent<AnimacionesNetwork>().inicial;
				//vidas -= 1;
			}else
			{
				Player.GetComponent<HeroNetwork>().salud = Player.GetComponent<HeroNetwork>().saludMax;
				Player.GetComponent<HeroNetwork>().CmdSendSalud(100);
				Player.GetComponent<HeroNetwork>().vivo = true;
				Player.GetComponent<Animator>().SetBool("muerto", false);
				Player.GetComponent<HeroNetwork>().mascara = "Enemy";
				Player.GetComponent<HeroNetwork>().CmdChangeMascara("Enemy");
				Player.GetComponent<HeroNetwork>().CmdChangeBase("Vista");
				Player.transform.position = Player.GetComponent<AnimacionesNetwork>().inicial;
				//vidas -= 1;
			}
		}else if(nace == 1)
		{
			if(Player.tag == "Player")
			{
				Player.GetComponent<HeroNetwork>().salud = Player.GetComponent<HeroNetwork>().saludMax;
				Player.GetComponent<HeroNetwork>().CmdSendSalud(100);
				Player.GetComponent<HeroNetwork>().vivo = true;
				Player.GetComponent<Animator>().SetBool("muerto", false);
				Player.GetComponent<HeroNetwork>().mascara = "Player";
				Player.GetComponent<HeroNetwork>().CmdChangeMascara("Player");
				Player.GetComponent<HeroNetwork>().CmdChangeBase("Vista");
				Player.transform.position = new Vector3(camp[0].transform.position.x, Player.GetComponent<AnimacionesNetwork>().inicial.y, camp[0].transform.position.z);
				//vidas -= 1;
			}else
			{
				Player.GetComponent<HeroNetwork>().salud = Player.GetComponent<HeroNetwork>().saludMax;
				Player.GetComponent<HeroNetwork>().CmdSendSalud(100);
				Player.GetComponent<HeroNetwork>().vivo = true;
				Player.GetComponent<Animator>().SetBool("muerto", false);
				Player.GetComponent<HeroNetwork>().mascara = "Enemy";
				Player.GetComponent<HeroNetwork>().CmdChangeMascara("Enemy");
				Player.GetComponent<HeroNetwork>().CmdChangeBase("Vista");
				Player.transform.position = new Vector3(camp[0].transform.position.x, Player.GetComponent<AnimacionesNetwork>().inicial.y, camp[0].transform.position.z);
				//vidas -= 1;
			}
		}else if(nace == 2)
		{
			if(Player.tag == "Player")
			{
				Player.GetComponent<HeroNetwork>().salud = Player.GetComponent<HeroNetwork>().saludMax;
				Player.GetComponent<HeroNetwork>().CmdSendSalud(100);
				Player.GetComponent<HeroNetwork>().vivo = true;
				Player.GetComponent<Animator>().SetBool("muerto", false);
				Player.GetComponent<HeroNetwork>().mascara = "Player";
				Player.GetComponent<HeroNetwork>().CmdChangeMascara("Player");
				Player.GetComponent<HeroNetwork>().CmdChangeBase("Vista");
				Player.transform.position = new Vector3(camp[1].transform.position.x, Player.GetComponent<AnimacionesNetwork>().inicial.y, camp[1].transform.position.z);
				//vidas -= 1;
			}else
			{
				Player.GetComponent<HeroNetwork>().salud = Player.GetComponent<HeroNetwork>().saludMax;
				Player.GetComponent<HeroNetwork>().CmdSendSalud(100);
				Player.GetComponent<HeroNetwork>().vivo = true;
				Player.GetComponent<Animator>().SetBool("muerto", false);
				Player.GetComponent<HeroNetwork>().mascara = "Enemy";
				Player.GetComponent<HeroNetwork>().CmdChangeMascara("Enemy");
				Player.GetComponent<HeroNetwork>().CmdChangeBase("Vista");
				Player.transform.position = new Vector3(camp[1].transform.position.x, Player.GetComponent<AnimacionesNetwork>().inicial.y, camp[1].transform.position.z);
				//vidas -= 1;
			}
		}else if(nace == 4)
		{
				if(Player.tag == "Player")
				{
					Player.GetComponent<HeroNetwork>().salud = Player.GetComponent<HeroNetwork>().saludMax;
					Player.GetComponent<HeroNetwork>().CmdSendSalud(100);
					Player.GetComponent<HeroNetwork>().vivo = true;
					Player.GetComponent<Animator>().SetBool("muerto", false);
					Player.GetComponent<HeroNetwork>().mascara = "Player";
					Player.GetComponent<HeroNetwork>().CmdChangeMascara("Player");
					Player.GetComponent<HeroNetwork>().CmdChangeBase("Vista");
					Player.transform.position = new Vector3(camp[2].transform.position.x, Player.GetComponent<AnimacionesNetwork>().inicial.y, camp[2].transform.position.z);
					//vidas -= 1;
				}else
				{
					Player.GetComponent<HeroNetwork>().salud = Player.GetComponent<HeroNetwork>().saludMax;
					Player.GetComponent<HeroNetwork>().CmdSendSalud(100);
					Player.GetComponent<HeroNetwork>().vivo = true;
					Player.GetComponent<Animator>().SetBool("muerto", false);
					Player.GetComponent<HeroNetwork>().mascara = "Enemy";
					Player.GetComponent<HeroNetwork>().CmdChangeMascara("Enemy");
					Player.GetComponent<HeroNetwork>().CmdChangeBase("Vista");
					Player.transform.position = new Vector3(camp[2].transform.position.x, Player.GetComponent<AnimacionesNetwork>().inicial.y, camp[2].transform.position.z);
					//vidas -= 1;
				}
		}else if(nace == 5)
		{
			if(Player.tag == "Player")
			{
				Player.GetComponent<HeroNetwork>().salud = Player.GetComponent<HeroNetwork>().saludMax;
				Player.GetComponent<HeroNetwork>().CmdSendSalud(100);
				Player.GetComponent<HeroNetwork>().vivo = true;
				Player.GetComponent<Animator>().SetBool("muerto", false);
				Player.GetComponent<HeroNetwork>().mascara = "Player";
				Player.GetComponent<HeroNetwork>().CmdChangeMascara("Player");
				Player.GetComponent<HeroNetwork>().CmdChangeBase("Vista");
				Player.transform.position = new Vector3(Bases[0].transform.position.x, Player.GetComponent<AnimacionesNetwork>().inicial.y, Bases[0].transform.position.z);
				//vidas -= 1;
			}else
			{
				Player.GetComponent<HeroNetwork>().salud = Player.GetComponent<HeroNetwork>().saludMax;
				Player.GetComponent<HeroNetwork>().CmdSendSalud(100);
				Player.GetComponent<HeroNetwork>().vivo = true;
				Player.GetComponent<Animator>().SetBool("muerto", false);
				Player.GetComponent<HeroNetwork>().mascara = "Enemy";
				Player.GetComponent<HeroNetwork>().CmdChangeMascara("Enemy");
				Player.GetComponent<HeroNetwork>().CmdChangeBase("Vista");
				Player.transform.position = new Vector3(Bases[0].transform.position.x, Player.GetComponent<AnimacionesNetwork>().inicial.y, Bases[0].transform.position.z);
				//vidas -= 1;
			}
		}else if(nace == 6)
		{
			if(Player.tag == "Player")
			{
				Player.GetComponent<HeroNetwork>().salud = Player.GetComponent<HeroNetwork>().saludMax;
				Player.GetComponent<HeroNetwork>().CmdSendSalud(100);
				Player.GetComponent<HeroNetwork>().vivo = true;
				Player.GetComponent<Animator>().SetBool("muerto", false);
				Player.GetComponent<HeroNetwork>().mascara = "Player";
				Player.GetComponent<HeroNetwork>().CmdChangeMascara("Player");
				Player.GetComponent<HeroNetwork>().CmdChangeBase("Vista");
				Player.transform.position = new Vector3(Bases[1].transform.position.x, Player.GetComponent<AnimacionesNetwork>().inicial.y, Bases[1].transform.position.z);
				//vidas -= 1;
			}else
			{
				Player.GetComponent<HeroNetwork>().salud = Player.GetComponent<HeroNetwork>().saludMax;
				Player.GetComponent<HeroNetwork>().CmdSendSalud(100);
				Player.GetComponent<HeroNetwork>().vivo = true;
				Player.GetComponent<Animator>().SetBool("muerto", false);
				Player.GetComponent<HeroNetwork>().mascara = "Enemy";
				Player.GetComponent<HeroNetwork>().CmdChangeMascara("Enemy");
				Player.GetComponent<HeroNetwork>().CmdChangeBase("Vista");
				Player.transform.position = new Vector3(Bases[1].transform.position.x, Player.GetComponent<AnimacionesNetwork>().inicial.y, Bases[1].transform.position.z);
				//vidas -= 1;
			}
		}

		//Player.GetComponent<HeroNetwork>().animacion.SetActive(false);

		Player.GetComponent<CustomFinalNetwork>().CmdSetCasco(Player.GetComponent<CustomFinalNetwork>().casco);
		Cabeza.GetComponent<CabezaNetwork>().tirosCabeza = 0;

		Camara.GetComponent<CamNetwork>().campamento = false;

		Player.GetComponent<HeroNetwork>().eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(Player.GetComponent<HeroNetwork>().carta1);
		Player.GetComponent<HeroNetwork>().SniperCam.GetComponent<Grayscale>().enabled = false;

		nace = 0;

		BasePrimaria.SetActive(false);
		alpha.SetActive(false);
		beta.SetActive(false);

		uno.SetActive(false);
		dos.SetActive(false);
		tres.SetActive(false);
	}
}
