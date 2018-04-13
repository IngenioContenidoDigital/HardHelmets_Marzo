﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityStandardAssets.CinematicEffects;
using UnityEngine.Networking;
using UnityEngine.EventSystems;
using UnityStandardAssets.ImageEffects;

public class HeroNetwork : NetworkBehaviour{

	public EventSystem eventsystem;
	public GameObject carta1;
	public GameObject muerto1;
	public GameObject pausa1;

	GameObject[] Enemy;

	public GameObject apuntar;

	public GameObject textos;
	public GameObject textos2;
	public GameObject particulasCurar;

	[SyncVar]
	public string nombre;

	[SyncVar(hook = "OnChangeHealth")]
	public float salud;

	[SyncVar]
	public float saludMax;

	[SyncVar]
	public float saludMax2;

	[SyncVar]
	public int level;

	public bool saludSumar;
	[SyncVar]
	public string medic;

	[SyncVar(hook = "FacingCallback")]
	public string _currentDirection = "right";

	public int maxspeed;
	//AMBIENTES
	public bool water;

	//GROUND CHECHER
	public Transform groundCheck;
	float groundRadius = 0.3f;
	public LayerMask whatIsGround;
	public bool grounded = false;
	//Physics hitColliders;

	public Animator animator;

	public GameObject Girar2;

	//ACCIONES DE PERSONAJE
	Vector3 v3;
	public bool caminarI = false;
	public bool caminarD = false;
	public bool caminarU = false;
	public bool caminarA = false;
	public bool cubrirse = false;
	public bool agachado;
	public Vector3 coverPosition;
	bool cubierto = false;
	public float velocidad;

	bool shoot;

	//ARMAS
	public string arma;
	public bool arma2;
	//LANSA LLAMAS
	public bool llamas;
	public bool lansallamas;
	//OBJETOS DE DISPARO BUENO
	public GameObject bulletPref;
	public GameObject bulletPrefFusil;
	public GameObject bulletPrefEscopeta;
	public GameObject bulletPrefSubmetra;
	public GameObject bulletPrefMetra;
	public GameObject bulletPrefLlamas;
	public GameObject fuegoFin;
	public GameObject bulletPrefSniper;
	public GameObject bulletPrefPanzer;
	//OBJETOS DE DISPARO MALO
	public GameObject bulletPrefMalo;
	public GameObject bulletPrefFusilMalo;
	public GameObject bulletPrefEscopetaMalo;
	public GameObject bulletPrefSubmetraMalo;
	public GameObject bulletPrefMetraMalo;
	public GameObject bulletPrefLlamasMalo;
	public GameObject bulletPrefSniperMalo;
	public GameObject bulletPrefPanzerMalo;
	//public GameObject bulletPrefLlamas;

	//DONDE APARECEN
	public Transform bulletSpawn;
	public Transform bulletSpawnFuego;
	public Transform bulletSniperSpawn;
	public Transform FlameSpawn;
	public Transform granadaSpawn;
	public GameObject casquilloPref;
	public GameObject casquilloPrefB;
	public Transform casquilloSpawn;
	public GameObject granadePref;
	public GameObject luz;
	//BALAS POR PISTOLA
	public bool rafaga = true;
	bool Gready = true;
	public int balaPistola = 12;
	public int balaEscopeta = 8;
	public int balaFusil = 5;
	public int balaSubmetra = 30;
	public int balaMetra = 60;
	public int balaLlamas = 300;
	public int balaSniper = 1;
	public int balaPanzer = 1;
	public int balas;//arma activa}
	public int granadas;
	public bool load = false;
	bool autoload = false;
	public bool cargando = false;
	//BALAS TOTALES
	//public static int PistolaTotales = 1;
	public int EscopetaTotales = 1;
	public int FusilTotales = 1;
	public int SubmetraTotales = 1;
	public int MetraTotales = 1;
	public int SniperTotales = 1; //5
	public int PanzerTotales = 1; //5
	public int balasTotales;//arma activa}

	//DISTANCIA
	public bool cuchillo;
	//MUERTES
	[SyncVar(hook = "cambiavivo")]
	public bool vivo = true;

	[SyncVar]//[SyncVar(hook = "cambiavivo2")]
	public string mascara;

	public GameObject Base;

	int muerte;

	//SNIPER
	public bool sniper;
	bool sniperListo;
	public GameObject SniperTexture;
	public GameObject esconderBarra;
	public GameObject SniperCam;

	public bool pressApuntar;
	public bool pressLado;
	public bool pressLado2;
	public bool pressProfundo;
	public bool pressProfundo2;
	public bool pressDisparo;

	public UnityEngine.UI.Image Health;
	//MOUSE CAMERA POSITION
	Vector3 point;

	//EFECTO DISPARO
	bool efectodisparo;
	bool efectodisparosumar;

	//MUERE CON EXPLOCION
	bool explocion;
	public GameObject Huesos;

	public GameObject mira;

	public GameObject menu;

	[SyncVar]
	public GameObject muneco;
	[SyncVar]
	public bool orden;
	[SyncVar]
	public Vector3 ordenLugar;

	public bool fast;

	public bool tirocabeza;
	bool disparoCabeza;
	public GameObject cabeza;

	public GameObject casco1;
	public GameObject casco2;
	public GameObject casco3;
	public GameObject casco4;
	public GameObject casco5;
	public GameObject casco6;
	public GameObject casco1M;
	public GameObject casco2M;
	public GameObject casco3M;
	public GameObject casco4M;
	public GameObject casco5M;
	public GameObject casco6M;

	public bool ready;

	// Use this for initialization
	void Start ()
	{
		level = PlayerPrefs.GetInt("PlayerLevel");
		CmdSendNivel(level);

		nombre = PlayerPrefs.GetString("SteamName");
		CmdSendNombre(nombre);

		if(!isServer)// && gameObject.tag == "enemy")
		{
			saludMax2 = 100+level*4;
			salud = saludMax2;
			saludMax = saludMax2;
			CmdSendLevel(saludMax2);
		}
		if(isServer)// && gameObject.tag == "Player")
		{
			saludMax = 100+level*4;
			salud = saludMax;
		}

		mira.SetActive(false);

		muerte = Random.Range(1,3);
		if(gameObject.tag == "Player")
		{
			_currentDirection = "right";
			transform.rotation = Quaternion.Euler(0,0,0);
		}else
		{
			_currentDirection = "left";
			CmdChangeDirection ("left");
		}
	}

	[Command]
	public void CmdSendLevel (float newLevel)
	{
		RpcSetLevel (newLevel);
	}
	[ClientRpc]
	public void RpcSetLevel (float newLevel)
	{
		//saludMax2 = newLevel;
		if(!isLocalPlayer)
		{
			saludMax2 = newLevel;
		}
	}
	//ACTUALIZA ARMA
	[Command]
	public void CmdSendArma (string newArma)
	{
		RpcSetArma (newArma);
	}
	[ClientRpc]
	public void RpcSetArma (string newArma)
	{
		if(!isLocalPlayer)
		{
			arma = newArma;
		}
	}

	[Command]
	public void CmdSendNivel (int newNivel)
	{
		RpcGetNivel (newNivel);
	}

	[ClientRpc]
	public void RpcGetNivel (int newNivel)
	{
		if(!isLocalPlayer)
		{
			level = newNivel;
		}
	}

	[Command]
	public void CmdSendNombre (string newNombre)
	{
		RpcGetNombre (newNombre);
	}

	[ClientRpc]
	public void RpcGetNombre (string newNombre)
	{
		if(!isLocalPlayer)
		{
			nombre = newNombre;
		}
	}

	void Update()
	{
		if(!isLocalPlayer)
		{
			return;
		}

		nacerZ = bulletSpawn.rotation.z;

		if(!apuntar.GetComponent<apuntarNetwork>().apuntar)
		{
			apuntar.GetComponent<apuntarNetwork>().apuntar = true;
		}

		Health.fillAmount = salud/saludMax;

		if(salud >= saludMax)
		{
			salud = saludMax;
			saludSumar = false;
		}

		//BUSCA OBJETOS
		if(SniperCam == null)
		{
			SniperCam = GameObject.FindGameObjectWithTag("MainCamera");
		}

		if(salud <= saludMax*70/100)
		{
			if(PlayerPrefs.GetInt("violencia") == 1)
			{
				SniperCam.GetComponent<BleedBehavior>().EdgeSharpness = 0.65f;//0.8
				SniperCam.GetComponent<BleedBehavior>().minAlpha = 0.5f+0-salud/100;//0.2
			}
			Health.color = new Color32(255,0,0,255);
		}else
		{
			SniperCam.GetComponent<BleedBehavior>().minAlpha = -0.1809f;
			SniperCam.GetComponent<BleedBehavior>().EdgeSharpness = 0.65f;
			Health.color = new Color32(0,255,0,255);
		}

		// EFECTO DIPARO EN PANTALLA (DESTELLO)
		if(efectodisparo)
		{
			SniperCam.GetComponent<LensAberrations>().chromaticAberration.amount -= 8;
			if(SniperCam.GetComponent<LensAberrations>().chromaticAberration.amount <= -20)
			{
				efectodisparosumar = true;
				efectodisparo = false;
			}
		}else if(efectodisparosumar)
		{
			SniperCam.GetComponent<LensAberrations>().chromaticAberration.amount += 8;
			if(SniperCam.GetComponent<LensAberrations>().chromaticAberration.amount >= 0)
			{
				SniperCam.GetComponent<LensAberrations>().chromaticAberration.amount = 0;
				efectodisparosumar = false;
				efectodisparo = false;
			}
		}else
		{
			//			SniperCam.GetComponent<LensAberrations>().chromaticAberration.amount = 0;
		}

		point = Camera.main.ScreenToViewportPoint(Input.mousePosition);//POSICION DEL MOUSE EN LA CAMARA EMPIEZA ESQUINA INFERIOR IZQUIERDA

		grounded = Physics.CheckSphere(groundCheck.position, groundRadius, whatIsGround);
		animator.SetBool("grounded", grounded);

		if(vivo && ready)
		{
			if(!isServer)
			{
				CmdSendArma(arma);
			}

			menu.SetActive(false);
			if(saludSumar)
			{
				salud += 0.1f;
				disparoCabeza = false;
				CmdSendSalud(salud);
			}

			if(tirocabeza)
			{
				rafaga = true;
				animator.SetBool("walk", false);
				caminarA = false;
				caminarD = false;
				caminarI = false;
				caminarU = false;
				velocidad = 0;
				v3 = Vector3.zero;
				if(salud >= 1)
				{
					animator.SetInteger("cascado", 1);
				}else
				{
					animator.SetBool("headShot", true);
					animator.SetInteger("muerte", 4);
				}

				StartCoroutine(sumar());
				efectodisparo = true;

				disparoCabeza = true;

				tirocabeza = false;
			}

			if(grounded && !cargando && !animator.GetCurrentAnimatorStateInfo(0).IsName("lansallamasShot") && !animator.GetCurrentAnimatorStateInfo(0).IsName("lansallamasrecarga") && !animator.GetCurrentAnimatorStateInfo(0).IsName("paracaidasSCae") && !animator.GetCurrentAnimatorStateInfo(0).IsName("hammer"))
			{
				//SALTO
				if(Input.GetButtonDown("Jump") && !animator.GetBool("cuchillando") && !sniperListo && !agachado && !cubierto && !animator.GetCurrentAnimatorStateInfo(0).IsName("cae"))
				{
					//GetComponent<Rigidbody>().AddForce (Vector2.up * 1000);
					GetComponent<Rigidbody>().AddForce (new Vector3(0,20,0), ForceMode.Impulse);
					animator.SetBool("jump", true);
				}
				//CAMINAR
				//IZQUIERDA CONTROL
				if(Input.GetAxis("lado") < 0 && !animator.GetBool("cuchillando") && !sniperListo && !agachado && !animator.GetCurrentAnimatorStateInfo(0).IsName("backJump") && !animator.GetCurrentAnimatorStateInfo(0).IsName("cae"))
				{
					if(_currentDirection == "right")
					{
						CmdChangeDirection ("left");
						v3 = Vector3.zero;
						caminarD = false;
					}
					if(caminarD && v3 != Vector3.zero)
					{
						v3 = Vector3.zero;
						caminarD = false;
					}
					caminarI = true;
					SniperCam.GetComponent<CamNetwork>().alejar = true;
					pressLado = false;
				}else if(Input.GetAxis("lado") < 0 && !sniperListo && agachado && !fast)
				{
					if(_currentDirection == "right")
					{
						animator.SetBool("jumpback", true);
						animator.SetBool("movimiento", true);
						animator.SetBool("grounded", false);
						GetComponent<Rigidbody>().AddForce (Vector2.up * 900);
						GetComponent<Rigidbody>().AddForce (Vector2.left * 900);
						agachado = false;
					}else
					{
						fast = true;
						animator.SetBool("jumpfront", true);
						GetComponent<Rigidbody>().AddForce (Vector2.left * 1200);
					}
					pressLado = false;
				}
				//IZQUIERDA TECLADO
				if(Input.GetButtonDown("left") && !animator.GetBool("cuchillando") && !sniperListo && !agachado && !animator.GetCurrentAnimatorStateInfo(0).IsName("backJump") && !animator.GetCurrentAnimatorStateInfo(0).IsName("cae"))
				{
					if(_currentDirection == "right")
					{
						CmdChangeDirection ("left");
						v3 = Vector3.zero;
						caminarD = false;
					}
					if(caminarD && v3 != Vector3.zero)
					{
						v3 = Vector3.zero;
						caminarD = false;
					}
					caminarI = true;
					SniperCam.GetComponent<CamNetwork>().alejar = true;
				}else if(Input.GetButtonDown("left") && !sniperListo && agachado && !fast)
				{
					if(_currentDirection == "right")
					{
						animator.SetBool("jumpback", true);
						animator.SetBool("movimiento", true);
						animator.SetBool("grounded", false);
						GetComponent<Rigidbody>().AddForce (Vector2.up * 900);
						GetComponent<Rigidbody>().AddForce (Vector2.left * 900);
						agachado = false;
					}else
					{
						fast = true;
						animator.SetBool("jumpfront", true);
						GetComponent<Rigidbody>().AddForce (Vector2.left * 1200);
					}
				}

				//DERECHA CONTROL
				if(Input.GetAxis("lado") > 0 && !animator.GetBool("cuchillando") && !sniperListo && !agachado && !animator.GetCurrentAnimatorStateInfo(0).IsName("backJump") && !animator.GetCurrentAnimatorStateInfo(0).IsName("cae"))
				{
					if(_currentDirection == "left")
					{
						CmdChangeDirection ("right");
						v3 = Vector3.zero;
						caminarI = false;
					}
					if(caminarI && v3 != Vector3.zero)
					{
						v3 = Vector3.zero;
						caminarI = false;
					}
					caminarD = true;
					SniperCam.GetComponent<CamNetwork>().alejar = true;
					pressLado2 = false;
				}else if(Input.GetAxis("lado") > 0 && !sniperListo && agachado && !fast)
				{
					if(_currentDirection == "right")
					{
						fast = true;
						animator.SetBool("jumpfront", true);
						GetComponent<Rigidbody>().AddForce (Vector2.right * 1200);
					}else
					{
						animator.SetBool("jumpback", true);
						animator.SetBool("movimiento", true);
						animator.SetBool("grounded", false);
						GetComponent<Rigidbody>().AddForce (Vector2.up * 900);
						GetComponent<Rigidbody>().AddForce (Vector2.right * 900);
						agachado = false;
					}
					pressLado2 = false;
				}
				//DERECHA TECLADO
				if(Input.GetButtonDown("right") && !animator.GetBool("cuchillando") && !sniperListo && !agachado && !animator.GetCurrentAnimatorStateInfo(0).IsName("backJump") && !animator.GetCurrentAnimatorStateInfo(0).IsName("cae"))
				{
					if(_currentDirection == "left")
					{
						CmdChangeDirection ("right");
						v3 = Vector3.zero;
						caminarI = false;
					}
					if(caminarI && v3 != Vector3.zero)
					{
						v3 = Vector3.zero;
						caminarI = false;
					}
					caminarD = true;
					SniperCam.GetComponent<CamNetwork>().alejar = true;
				}else if(Input.GetButtonDown("right") && !sniperListo && agachado && !fast)
				{
					if(_currentDirection == "right")
					{
						fast = true;
						animator.SetBool("jumpfront", true);
						GetComponent<Rigidbody>().AddForce (Vector2.right * 1200);
					}else
					{
						animator.SetBool("jumpback", true);
						animator.SetBool("movimiento", true);
						animator.SetBool("grounded", false);
						GetComponent<Rigidbody>().AddForce (Vector2.up * 900);
						GetComponent<Rigidbody>().AddForce (Vector2.right * 900);
						agachado = false;
					}
				}

				//ARRIBA CONTROL
				if(Input.GetAxis("profundo") > 0 && !animator.GetBool("cuchillando") && !sniperListo && !agachado  && !animator.GetCurrentAnimatorStateInfo(0).IsName("backJump") && !animator.GetCurrentAnimatorStateInfo(0).IsName("cae"))
				{
					//caminarA = false;
					//caminarI = false;
					//caminarD = false;
					if(caminarA && v3 != Vector3.zero)
					{
						v3 = Vector3.zero;
						caminarA = false;
					}
					caminarU = true;
					pressProfundo = false;
				}
				//ARRIBA TECLADO
				if(Input.GetButtonDown("up") && !animator.GetBool("cuchillando") && !sniperListo && !agachado  && !animator.GetCurrentAnimatorStateInfo(0).IsName("backJump") && !animator.GetCurrentAnimatorStateInfo(0).IsName("cae"))
				{
					//caminarA = false;
					//caminarI = false;
					//caminarD = false;
					if(caminarA && v3 != Vector3.zero)
					{
						v3 = Vector3.zero;
						caminarA = false;
					}
					caminarU = true;
				}
				//ABAJO CONTROL
				if(Input.GetAxis("profundo") < 0 && !animator.GetBool("cuchillando") && !sniperListo && !agachado  && !animator.GetCurrentAnimatorStateInfo(0).IsName("backJump") && !animator.GetCurrentAnimatorStateInfo(0).IsName("cae"))
				{
					//caminarU = false;
					//caminarD = false;
					//caminarI = false;
					if(caminarU && v3 != Vector3.zero)
					{
						v3 = Vector3.zero;
						caminarU = false;
					}
					caminarA = true;
					pressProfundo2 = false;
				}

				//ABAJO TECLADO
				if(Input.GetButtonDown("down") && !animator.GetBool("cuchillando") && !sniperListo && !agachado  && !animator.GetCurrentAnimatorStateInfo(0).IsName("backJump") && !animator.GetCurrentAnimatorStateInfo(0).IsName("cae"))
				{
					//caminarU = false;
					//caminarD = false;
					//caminarI = false;
					if(caminarU && v3 != Vector3.zero)
					{
						v3 = Vector3.zero;
						caminarU = false;
					}
					caminarA = true;
				}
				//CUBRIRSE
				if(cubrirse && !sniperListo)
				{
					if(Input.GetButtonDown("USAR"))
					{
						agachado = false;
						animator.SetBool("cubrirse", true);
						cubierto = true;
					}
				}
				if(cubierto)
				{
					transform.position = Vector3.Lerp(transform.position, coverPosition, Time.deltaTime * 5);
					StartCoroutine(coverTiempo());
				}

				if(Input.GetButtonDown("USAR"))
				{
					if(!sniperListo && !cubrirse && !caminarA && !caminarD && !caminarI && !caminarU)
					{
						animator.SetBool("cubrirse", true);
						agachado = true;
						fast = false;
						StartCoroutine(cover());
					}else if(!sniperListo)
					{
						animator.SetBool("roll", true);
					}
				}

				//DISPARO
				if(shoot && rafaga)
				{
					Disparo();
					rafaga = false;
				}

				//DISPARO EN CONTROL
				if(Input.GetAxis("DISPARO") > 0 && point.y > 0.22f && rafaga && !animator.GetCurrentAnimatorStateInfo(0).IsName("cae") && !animator.GetCurrentAnimatorStateInfo(0).IsName("roll"))
				{
					fast = false;
					//agachado = false;
					//cubierto = false;
					if(animator.GetBool("cuchillando"))
					{
						animator.SetBool("cuchillo2", true);
					}else if(cuchillo)
					{
						animator.SetBool("cuchillo", true);
						caminarA = false;
						caminarD = false;
						caminarI = false;
						caminarU = false;
						//cuchillo = false;
					}else if(sniperListo)
					{
						disparoSniper();
					}else
					{
						//animator.SetBool("cuchillo", true);
						//if(arma == "metra" && !animator.GetCurrentAnimatorStateInfo(0).IsName("MetraShot") || !animator.GetCurrentAnimatorStateInfo(0).IsName("MetraShotAgachado"))
						//{
						//	Disparo();
						//}else
						//{
						shoot = true;
						//}
					}
					pressDisparo = false;
				}
				//DISPARO TECLADO
				if(Input.GetButtonDown("DISPARO 2") && point.y > 0.22f && rafaga && !animator.GetCurrentAnimatorStateInfo(0).IsName("cae") && !animator.GetCurrentAnimatorStateInfo(0).IsName("roll"))
				{
					fast = false;
					//agachado = false;
					//cubierto = false;
					if(animator.GetBool("cuchillando"))
					{
						animator.SetBool("cuchillo2", true);
					}else if(cuchillo)
					{
						animator.SetBool("cuchillo", true);
						caminarA = false;
						caminarD = false;
						caminarI = false;
						caminarU = false;
						//cuchillo = false;
					}else if(sniperListo)
					{
						disparoSniper();
					}else
					{
						//animator.SetBool("cuchillo", true);
						//if(arma == "metra" && !animator.GetCurrentAnimatorStateInfo(0).IsName("MetraShot") || !animator.GetCurrentAnimatorStateInfo(0).IsName("MetraShotAgachado"))
						//{
						//	Disparo();
						//}else
						//{
							shoot = true;
						//}
					}
				}
				if(Input.GetButtonDown("RECARGA") && !animator.GetBool("cuchillando") && !animator.GetCurrentAnimatorStateInfo(0).IsName("backJump") && !animator.GetCurrentAnimatorStateInfo(0).IsName("cae") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
				{
					rafaga = true;
					autoload = true;
					if(arma2)
					{
						animator.SetInteger("recarga", -1);
					}
					if(lansallamas)
					{
						animator.SetInteger("recarga", 12);
					}
					if(!arma2 && !lansallamas)
					{
						if(arma == "escopeta")
						{
							animator.SetInteger("recarga", 1);
						}
						if(arma == "fusil")
						{
							animator.SetInteger("recarga", 2);
						}
						if(arma == "submetra")
						{
							animator.SetInteger("recarga", 3);
						}
						if(arma == "metra")
						{
							animator.SetInteger("recarga", 3);
						}
						if(arma == "panzer" && balaPanzer <= 0)
						{
							animator.SetInteger("recarga", 7);
						}
						if(arma == "sniper" && balaSniper <= 0)
						{
							animator.SetInteger("recarga", 2);
						}
					}
				}

				//APUNTAR EN CONTROL
				//PRESIONA APUNTAR EN CONTROL
				if(Input.GetAxis("APUNTAR") > 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("cae") && pressApuntar)// && sniper)
				{
					pressApuntar = false;
					if(sniper)
					{
						caminarA = false;
						caminarD = false;
						caminarI = false;
						caminarU = false;

						rafaga = true;
						cuchillo = false;
						sniperListo = true;

						SniperCam.GetComponent<CamNetwork>().sniper = true;

						SniperTexture.SetActive(true);
						esconderBarra.SetActive(false);
						SniperCam.GetComponent<LensAberrations>().distortion.enabled = true;
						animator.SetInteger("disparo",20);
					}else
					{
						animator.SetBool("pose", true);
						fast = false;
					}
				}
				//apuntar en teclado
				if(Input.GetButtonDown("APUNTAR 2") && !animator.GetCurrentAnimatorStateInfo(0).IsName("cae"))// && sniper)
				{
					if(sniper)
					{
						caminarA = false;
						caminarD = false;
						caminarI = false;
						caminarU = false;

						rafaga = true;
						cuchillo = false;
						sniperListo = true;

						SniperCam.GetComponent<CamNetwork>().sniper = true;

						SniperTexture.SetActive(true);
						esconderBarra.SetActive(false);
						SniperCam.GetComponent<LensAberrations>().distortion.enabled = true;
						animator.SetInteger("disparo",20);
					}else
					{
						animator.SetBool("pose", true);
						fast = false;
					}
				}

				if(Input.GetButtonDown("GRANADA") && granadas >= 1 && !sniperListo && Gready && !animator.GetBool("cuchillando") && !animator.GetCurrentAnimatorStateInfo(0).IsName("backJump") && !animator.GetCurrentAnimatorStateInfo(0).IsName("cae"))
				{
					animator.SetBool("granada", true);
					Gready = false;
					agachado = false;
					animator.SetInteger("disparo", 6);
					StartCoroutine(esperaG());
				}

				if(caminarI && !sniperListo && !cargando && !animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
				{
					caminarD = false;
					animator.SetBool("walk", true);
					velocidad += 1f;
					if(velocidad >= maxspeed)
					{
						velocidad = maxspeed;
					}
					v3 += Vector3.left;
					//GetComponent<Rigidbody>().velocity = (Vector2.left * velocidad);//13
				}
				if(caminarD && !sniperListo && !cargando && !animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
				{
					caminarI = false;
					animator.SetBool("walk", true);
					velocidad += 1f;
					if(velocidad >= maxspeed)
					{
						velocidad = maxspeed;
					}
					v3 += Vector3.right;
					//GetComponent<Rigidbody>().velocity = (Vector2.right * velocidad);//13
				}
				if(caminarU && !sniperListo && !cargando && !animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
				{
					caminarA = false;
					animator.SetBool("walk", true);
					velocidad += 1f;
					if(velocidad >= maxspeed)
					{
						velocidad = maxspeed;
					}
					v3 += Vector3.forward;
					//GetComponent<Rigidbody>().velocity = (Vector3.forward * velocidad);//10
				}
				if(caminarA && !sniperListo && !cargando && !animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
				{
					caminarU = false;
					animator.SetBool("walk", true);
					velocidad += 1f;
					if(velocidad >= maxspeed)
					{
						velocidad = maxspeed;
					}
					v3 += Vector3.back;
					//GetComponent<Rigidbody>().velocity = (Vector3.back * velocidad);//10
				}
				if(v3 != Vector3.zero)
				{
					animator.SetBool("cubrirse", false);
					animator.SetBool("cubierto", false);
					agachado = false;
					GetComponent<Rigidbody>().velocity = (velocidad * v3.normalized);
					//transform.Translate(velocidad * v3.normalized * Time.deltaTime);  
				}

			}else if(!cargando && !sniperListo)//DISPARO EN EL AIRE
			{
				if(Input.GetAxis("up") > 0)
				{
					caminarU = true;
				}
				if(Input.GetAxis("down") > 0)
				{
					caminarA = true;
				}
				//DISPARO CON CONTROL
				if(Input.GetAxis("DISPARO") > 0 && rafaga)
				{
					rafaga = false;
					//ARRIBA
					if(caminarU)
					{
						if(arma2 && balaPistola >= 1)
						{
							StartCoroutine(resetArma());
							animator.SetInteger("disparo", 9);
							StartCoroutine(esperaPistolaAire());
						}else if(!arma2)
						{
							if(arma == "escopeta" && balaEscopeta >= 1)
							{
								animator.SetInteger("disparo", 9);
								StartCoroutine(esperaEscopeta());
							}
							if(arma == "fusil" && balaFusil >= 1)
							{
								animator.SetInteger("disparo", 9);
								StartCoroutine(esperaFusil());
							}
							if(arma == "submetra" && balaSubmetra >= 1)
							{
								animator.SetInteger("disparo", 9);
								StartCoroutine(esperaSubmetra());
							}
							if(arma == "metra" && balaMetra >= 1)
							{
								animator.SetInteger("disparo", 9);
								StartCoroutine(esperaMetra());
							}
							if(arma == "panzer" && balaPanzer >= 1)
							{
								animator.SetInteger("disparo", 9);
								StartCoroutine(esperaPanzer());
							}
						}
					}else if(caminarA)
					{
						if(arma2 && balaPistola >= 1)
						{
							StartCoroutine(resetArma());
							animator.SetInteger("disparo", 10);
							StartCoroutine(esperaPistolaAire());
						}else if(!arma2)
						{
							if(arma == "escopeta" && balaEscopeta >= 1)
							{
								animator.SetInteger("disparo", 10);
								StartCoroutine(esperaEscopeta());
							}
							if(arma == "fusil" && balaFusil >= 1)
							{
								animator.SetInteger("disparo", 10);
								StartCoroutine(esperaFusil());
							}
							if(arma == "submetra" && balaSubmetra >= 1)
							{
								animator.SetInteger("disparo", 10);
								StartCoroutine(esperaSubmetra());
							}
							if(arma == "metra" && balaMetra >= 1)
							{
								animator.SetInteger("disparo", 10);
								StartCoroutine(esperaMetra());
							}
							if(arma == "panzer" && balaPanzer >= 1)
							{
								animator.SetInteger("disparo", 10);
								StartCoroutine(esperaPanzer());
							}
						}
					}else if(!animator.GetCurrentAnimatorStateInfo(0).IsName("backJump") && !animator.GetCurrentAnimatorStateInfo(0).IsName("cae"))
					{
						if(arma2 && balaPistola >= 1)
						{
							StartCoroutine(resetArma());
							animator.SetInteger("disparo", 8);
							StartCoroutine(esperaPistolaAire());
						}else
						{
							if(arma == "escopeta" && balaEscopeta >= 1)
							{
								animator.SetInteger("disparo", 8);
								StartCoroutine(esperaEscopeta());
							}
							if(arma == "fusil" && balaFusil >= 1)
							{
								animator.SetInteger("disparo", 8);
								StartCoroutine(esperaFusil());
							}
							if(arma == "submetra" && balaSubmetra >= 1)
							{
								animator.SetInteger("disparo", 8);
								StartCoroutine(esperaSubmetra());
							}
							if(arma == "metra" && balaMetra >= 1)
							{
								animator.SetInteger("disparo", 8);
								StartCoroutine(esperaMetra());
							}
							if(arma == "panzer" && balaPanzer >= 1)
							{
								animator.SetInteger("disparo", 8);
								StartCoroutine(esperaPanzer());
							}
						}
					}else
					{
						if(arma2 && balaPistola >= 1)
						{
							StartCoroutine(resetArma());
							animator.SetInteger("disparo", 11);
							StartCoroutine(esperaPistolaAire());
						}else
						{
							if(arma == "escopeta" && balaEscopeta >= 1)
							{
								animator.SetInteger("disparo", 11);
								StartCoroutine(esperaEscopeta());
							}
							if(arma == "fusil" && balaFusil >= 1)
							{
								animator.SetInteger("disparo", 11);
								StartCoroutine(esperaFusil());
							}
							if(arma == "submetra" && balaSubmetra >= 1)
							{
								animator.SetInteger("disparo", 11);
								StartCoroutine(esperaSubmetra());
							}
							if(arma == "metra" && balaMetra >= 1)
							{
								animator.SetInteger("disparo", 11);
								StartCoroutine(esperaMetra());
							}
							if(arma == "panzer" && balaPanzer >= 1)
							{
								animator.SetInteger("disparo", 11);
								StartCoroutine(esperaPanzer());
							}
						}
					}
					pressDisparo = false;
				}
				//Disparo TECLADO
				if(Input.GetButtonDown("DISPARO 2") && rafaga)
				{
					rafaga = false;
					//ARRIBA
					if(caminarU)
					{
						if(arma2 && balaPistola >= 1)
						{
							StartCoroutine(resetArma());
							animator.SetInteger("disparo", 9);
							StartCoroutine(esperaPistolaAire());
						}else if(!arma2)
						{
							if(arma == "escopeta" && balaEscopeta >= 1)
							{
								animator.SetInteger("disparo", 9);
								StartCoroutine(esperaEscopeta());
							}
							if(arma == "fusil" && balaFusil >= 1)
							{
								animator.SetInteger("disparo", 9);
								StartCoroutine(esperaFusil());
							}
							if(arma == "submetra" && balaSubmetra >= 1)
							{
								animator.SetInteger("disparo", 9);
								StartCoroutine(esperaSubmetra());
							}
							if(arma == "metra" && balaMetra >= 1)
							{
								animator.SetInteger("disparo", 9);
								StartCoroutine(esperaMetra());
							}
							if(arma == "panzer" && balaPanzer >= 1)
							{
								animator.SetInteger("disparo", 9);
								StartCoroutine(esperaPanzer());
							}
						}
					}else if(caminarA)
					{
						if(arma2 && balaPistola >= 1)
						{
							StartCoroutine(resetArma());
							animator.SetInteger("disparo", 10);
							StartCoroutine(esperaPistolaAire());
						}else if(!arma2)
						{
							if(arma == "escopeta" && balaEscopeta >= 1)
							{
								animator.SetInteger("disparo", 10);
								StartCoroutine(esperaEscopeta());
							}
							if(arma == "fusil" && balaFusil >= 1)
							{
								animator.SetInteger("disparo", 10);
								StartCoroutine(esperaFusil());
							}
							if(arma == "submetra" && balaSubmetra >= 1)
							{
								animator.SetInteger("disparo", 10);
								StartCoroutine(esperaSubmetra());
							}
							if(arma == "metra" && balaMetra >= 1)
							{
								animator.SetInteger("disparo", 10);
								StartCoroutine(esperaMetra());
							}
							if(arma == "panzer" && balaPanzer >= 1)
							{
								animator.SetInteger("disparo", 10);
								StartCoroutine(esperaPanzer());
							}
						}
					}else if(!animator.GetCurrentAnimatorStateInfo(0).IsName("backJump") && !animator.GetCurrentAnimatorStateInfo(0).IsName("cae"))
					{
						if(arma2 && balaPistola >= 1)
						{
							StartCoroutine(resetArma());
							animator.SetInteger("disparo", 8);
							StartCoroutine(esperaPistolaAire());
						}else
						{
							if(arma == "escopeta" && balaEscopeta >= 1)
							{
								animator.SetInteger("disparo", 8);
								StartCoroutine(esperaEscopeta());
							}
							if(arma == "fusil" && balaFusil >= 1)
							{
								animator.SetInteger("disparo", 8);
								StartCoroutine(esperaFusil());
							}
							if(arma == "submetra" && balaSubmetra >= 1)
							{
								animator.SetInteger("disparo", 8);
								StartCoroutine(esperaSubmetra());
							}
							if(arma == "metra" && balaMetra >= 1)
							{
								animator.SetInteger("disparo", 8);
								StartCoroutine(esperaMetra());
							}
							if(arma == "panzer" && balaPanzer >= 1)
							{
								animator.SetInteger("disparo", 8);
								StartCoroutine(esperaPanzer());
							}
						}
					}else
					{
						if(arma2 && balaPistola >= 1)
						{
							StartCoroutine(resetArma());
							animator.SetInteger("disparo", 11);
							StartCoroutine(esperaPistolaAire());
						}else
						{
							if(arma == "escopeta" && balaEscopeta >= 1)
							{
								animator.SetInteger("disparo", 11);
								StartCoroutine(esperaEscopeta());
							}
							if(arma == "fusil" && balaFusil >= 1)
							{
								animator.SetInteger("disparo", 11);
								StartCoroutine(esperaFusil());
							}
							if(arma == "submetra" && balaSubmetra >= 1)
							{
								animator.SetInteger("disparo", 11);
								StartCoroutine(esperaSubmetra());
							}
							if(arma == "metra" && balaMetra >= 1)
							{
								animator.SetInteger("disparo", 11);
								StartCoroutine(esperaMetra());
							}
							if(arma == "panzer" && balaPanzer >= 1)
							{
								animator.SetInteger("disparo", 11);
								StartCoroutine(esperaPanzer());
							}
						}
					}
				}
			}
			//DISPARO LLAMAS
			if(llamas)
			{
				if(balaLlamas >= 1)
				{
					if(gameObject.tag == "Player")
					{
						CmdBalaLlamas();
					}else
					{
						CmdBalaLlamasMalo();
					}
					luz.SetActive(true);
					StartCoroutine(apaga());

					balaLlamas -= 1;
				}else
				{
					rafaga = true;
					llamas = false;
					GetComponent<Sonidos>().fuego = false;
					Cmd_Fuego();
					StartCoroutine(apaga());
				}
			}
			//SUELTA DISPARO CON CONTROL
			if(Input.GetAxis("DISPARO") == 0 && !pressDisparo)
			{
				shoot = false;
				animator.SetBool("llamas", false);
				if(llamas)
				{
					rafaga = true;
					Cmd_Fuego();
					StartCoroutine(apaga());
					GetComponent<Sonidos>().fuego = false;
					llamas = false;
				}
				animator.SetInteger("disparo", 0);
				//rafaga = true;
				pressDisparo = true;
			}
			//SUELTA DISPARO EN TECLADO
			if(Input.GetButtonUp("DISPARO 2"))
			{
				shoot = false;
				animator.SetBool("llamas", false);
				if(llamas)
				{
					rafaga = true;
					Cmd_Fuego();
					StartCoroutine(apaga());
					GetComponent<Sonidos>().fuego = false;
					llamas = false;
				}
				animator.SetInteger("disparo", 0);
				//rafaga = true;
			}

			//SUELTA APUNTAR EN CONTROL
			if(Input.GetAxis("APUNTAR") == 0 && !pressApuntar)// && sniper
			{
				animator.SetInteger("disparo", 0);
				animator.SetBool("apuntando", false);

				sniperListo = false;

				SniperCam.GetComponent<CamNetwork>().sniper = false;

				SniperTexture.SetActive(false);
				esconderBarra.SetActive(true);
				SniperCam.GetComponent<LensAberrations>().distortion.enabled = false;

				pressApuntar = true;
			}
			//SUELTA APUNTAR EN TECLADO
			if(Input.GetButtonUp("APUNTAR 2"))// && sniper
			{
				animator.SetInteger("disparo", 0);
				animator.SetBool("apuntando", false);

				sniperListo = false;

				SniperCam.GetComponent<CamNetwork>().sniper = false;

				SniperTexture.SetActive(false);
				esconderBarra.SetActive(true);
				SniperCam.GetComponent<LensAberrations>().distortion.enabled = false;
			}
			//SUELTA CONTROL IZQUIERDA
			if(Input.GetAxis("lado") == 0 && !pressLado)
			{
				if(!caminarD  && !caminarU && !caminarA)
				{
					velocidad = 0;
					animator.SetBool("walking", false);
					animator.SetBool("walk", false);
				}
				v3 = Vector3.zero;
				caminarI = false;
				SniperCam.GetComponent<CamNetwork>().alejar = false;

				pressLado = true;
			}
			//SUELTA TECLADO IZQUIERDA
			if(Input.GetButtonUp("left"))
			{
				if(!caminarD  && !caminarU && !caminarA)
				{
					velocidad = 0;
					animator.SetBool("walking", false);
					animator.SetBool("walk", false);
				}
				v3 = Vector3.zero;
				caminarI = false;
				SniperCam.GetComponent<CamNetwork>().alejar = false;
			}
			//SUELTA CONTROL DERECHA
			if(Input.GetAxis("lado") == 0 && !pressLado2)
			{
				if(!caminarI && !caminarU && !caminarA)
				{
					velocidad = 0;
					animator.SetBool("walking", false);
					animator.SetBool("walk", false);
				}
				v3 = Vector3.zero;
				caminarD = false;
				SniperCam.GetComponent<CamNetwork>().alejar = false;

				pressLado2 = true;
			}
			//SUELTA TECLADO DERECHA
			if(Input.GetButtonUp("right"))
			{
				if(!caminarI && !caminarU && !caminarA)
				{
					velocidad = 0;
					animator.SetBool("walking", false);
					animator.SetBool("walk", false);
				}
				v3 = Vector3.zero;
				caminarD = false;
				SniperCam.GetComponent<CamNetwork>().alejar = false;
			}

			//SUELTA CONTROL ARRIBA
			if(Input.GetAxis("profundo") == 0 && !pressProfundo)
			{
				if(!caminarA && !caminarI && !caminarD)
				{
					velocidad = 0;
					animator.SetBool("walking", false);
					animator.SetBool("walk", false);
				}
				v3 = Vector3.zero;
				caminarU = false;
				pressProfundo = true;
			}
			//SUELTA CONTROL ARRIBA
			if(Input.GetButtonUp("up"))
			{
				if(!caminarA && !caminarI && !caminarD)
				{
					velocidad = 0;
					animator.SetBool("walking", false);
					animator.SetBool("walk", false);
				}
				v3 = Vector3.zero;
				caminarU = false;
			}

			//SUELTA CONTROL ABAJO
			if(Input.GetAxis("profundo") == 0 && !pressProfundo2)
			{
				if(!caminarU && !caminarI && !caminarD)
				{
					velocidad = 0;
					animator.SetBool("walking", false);
					animator.SetBool("walk", false);
				}
				v3 = Vector3.zero;
				caminarA = false;
				pressProfundo2 = true;
			}
			//SUELTA CONTROL ABAJO
			if(Input.GetButtonUp("down"))
			{
				if(!caminarU && !caminarI && !caminarD)
				{
					velocidad = 0;
					animator.SetBool("walking", false);
					animator.SetBool("walk", false);
				}
				v3 = Vector3.zero;
				caminarA = false;
			}

			//SUELTA BOTON DE AGACHADO
			if(Input.GetButtonUp("USAR"))
			{
				animator.SetBool("cubrirse", false);
				animator.SetBool("cubierto", false);
				agachado = false;
			}
			//FALLING
			if(GetComponent<Rigidbody>().velocity.y < -4f && !animator.GetCurrentAnimatorStateInfo(0).IsName("cae") && !animator.GetCurrentAnimatorStateInfo(0).IsName("backJump"))
			{
				animator.SetBool("falling", true);
			}else
			{
				animator.SetBool("falling", false);
			}

			if(arma2)
			{
				animator.SetBool("pistola", true);
				balas = balaPistola;
				//balasTotales = 0;
			}else
			{
				animator.SetBool("pistolando", false);
				animator.SetBool("pistola", false);
			}
			if(lansallamas)
			{
				animator.SetBool("flamas", true);
			}else
			{
				animator.SetBool("flamas", false);
			}

			if(arma == "escopeta" && !arma2)
			{
				balas = balaEscopeta;
				balasTotales = EscopetaTotales;
			}
			if(arma == "fusil" && !arma2)
			{
				balas = balaFusil;
				balasTotales = FusilTotales;
			}
			if(arma == "submetra" && !arma2)
			{
				balas = balaSubmetra;
				balasTotales = SubmetraTotales;
			}
			if(arma == "metra" && !arma2)
			{
				balas = balaMetra;
				balasTotales = MetraTotales;
			}
			if(arma == "lansallamas" && !arma2)
			{
				balas = balaLlamas;
				balasTotales = 00;
			}
			if(arma == "sniper" && !arma2)
			{
				balas = balaSniper;
				balasTotales = SniperTotales;
			}
			if(arma == "panzer" && !arma2)
			{
				balas = balaPanzer;
				balasTotales = PanzerTotales;
			}
			//RECARGA
			if(load)
			{
				int totales;
				rafaga = true;
				load = false;
				if(autoload)
				{
					if(arma2)//animator = 5
					{
						balaPistola = 12;
					}else
					{
						if(arma == "escopeta")//animator = 1
						{
							totales = 8-balaEscopeta;

							if(EscopetaTotales >= 8)
							{
								balaEscopeta = 8;
								EscopetaTotales -= totales;
							}else
							{
								balaEscopeta += EscopetaTotales;
								EscopetaTotales -= totales;
							}
						}
						if(arma == "fusil")//animator = 2
						{
							totales = 5-balaFusil;

							if(FusilTotales >= 5)
							{
								balaFusil = 5;
								FusilTotales -= totales;
							}else
							{
								balaFusil += FusilTotales;
								FusilTotales -= totales;
							}
						}
						if(arma == "submetra")//animator = 3
						{
							totales = 30-balaSubmetra;

							if(SubmetraTotales >= 30)
							{
								balaSubmetra = 30;
								SubmetraTotales -= totales;
							}else
							{
								balaSubmetra += SubmetraTotales;
								SubmetraTotales -= totales;
							}
						}
						if(arma == "metra")//animator = 4
						{
							totales = 60-balaMetra;

							if(MetraTotales >= 60)
							{
								balaMetra = 60;
								MetraTotales -= totales;
							}else
							{
								balaMetra += MetraTotales;
								MetraTotales -= totales;
							}
						}
						if(arma == "panzer" && PanzerTotales >= 1)
						{
							GetComponent<CustomFinalNetwork>().bala = true;
							balaPanzer = 1;
							PanzerTotales -= 1;
						}
						if(arma == "sniper" && SniperTotales >= 1)
						{
							balaSniper = 1;
							SniperTotales -= 1;
						}
					}
					autoload = false;
				}else
				{
					if(arma2)//animator = 5
					{
						balaPistola = 12;
					}
					if(!arma2)
					{
						if(arma == "escopeta" && EscopetaTotales >= 8)//animator = 1
						{
							balaEscopeta = 8;
							EscopetaTotales -= 8;
						}else if(EscopetaTotales < 8)
						{
							balaEscopeta = EscopetaTotales;
							EscopetaTotales -= EscopetaTotales;
						}
						if(arma == "fusil" && FusilTotales >= 5)//animator = 2
						{
							balaFusil += 5;
							FusilTotales -= 5;
						}else if(FusilTotales < 5)
						{
							balaFusil = FusilTotales;
							FusilTotales -= FusilTotales;
						}
						if(arma == "submetra" && SubmetraTotales >= 30)//animator = 3
						{
							balaSubmetra = 30;
							SubmetraTotales -= 30;
						}else if(SubmetraTotales < 30)
						{
							balaSubmetra = SubmetraTotales;
							SubmetraTotales -= SubmetraTotales;
						}
						if(arma == "metra" && MetraTotales >= 60)//animator = 4
						{
							balaMetra = 60;
							MetraTotales -= 60;
						}else if(MetraTotales < 60 && MetraTotales > 0)
						{
							balaMetra = MetraTotales;
							MetraTotales -= MetraTotales;
						}
						if(arma == "sniper" && SniperTotales >= 1)
						{
							balaSniper = 1;
							SniperTotales -= 1;
						}
						if(arma == "panzer" && PanzerTotales >= 1)
						{
							balaPanzer = 1;
							PanzerTotales -= 1;
						}
					}
				}
			}
			//BALAS NO PUEDE SER MENOS DE CERO
			if(balaPistola < 0)
			{
				balaPistola = 0;
			}
			if(balaFusil < 0)
			{
				balaFusil = 0;
			}
			if(balaEscopeta < 0)
			{
				balaEscopeta = 0;
			}
			if(balaSubmetra < 0)
			{
				balaSubmetra = 0;
			}
			if(balaMetra < 0)
			{
				balaMetra = 0;
			}
			if(balaLlamas < 0)
			{
				balaLlamas = 0;
			}
			if(balaSniper < 0)
			{
				balaSniper = 0;
			}
			if(balaPanzer < 0)
			{
				balaPanzer = 0;
			}
			//BALAS MAXIMAS POR ARMA
			if(balaPistola >= 12)
			{
				balaPistola = 12;
			}
			if(balaFusil >= 5)
			{
				balaFusil = 5;
			}
			if(balaEscopeta >= 8)
			{
				balaEscopeta = 8;
			}
			if(balaSubmetra >= 30)
			{
				balaSubmetra = 30;
			}
			if(balaMetra >= 60)
			{
				balaMetra = 60;
			}
			if(balaLlamas >= 300)
			{
				balaLlamas = 300;
			}
			if(balaSniper >= 5)
			{
				balaSniper = 5;
			}
			if(balaPanzer >= 5)
			{
				balaPanzer = 5;
			}
			//BALAS MAXIMAS TOTALES POR ARMA
			if(EscopetaTotales >= 48)
			{
				EscopetaTotales = 48;
			}
			if(FusilTotales >= 40)
			{
				FusilTotales = 40;
			}
			if(SubmetraTotales >= 120)
			{
				SubmetraTotales = 120;
			}
			if(MetraTotales >= 180)
			{
				MetraTotales = 180;
			}
			if(SniperTotales >= 5)
			{
				SniperTotales = 5;
			}
			if(PanzerTotales >= 5)
			{
				PanzerTotales = 5;
			}
			//MINIMAS TOTALES POR ARMA
			if(EscopetaTotales <= 0)
			{
				EscopetaTotales = 0;
			}
			if(FusilTotales <= 0)
			{
				FusilTotales = 0;
			}
			if(SubmetraTotales <= 0)
			{
				SubmetraTotales = 0;
			}
			if(MetraTotales <= 0)
			{
				MetraTotales = 0;
			}
			if(SniperTotales <= 0)
			{
				SniperTotales = 0;
			}
			if(PanzerTotales <= 0)
			{
				PanzerTotales = 0;
			}
			//SI SE MUERE
			if(salud <= 0)
			{
				saludSumar = false;
				int muerteg = Random.Range(11,14);
				if(explocion)
				{
					animator.SetBool("muerto", true);
					animator.SetInteger("muerte", muerteg);
					var bones = (GameObject)Instantiate(Huesos, casquilloSpawn.position, transform.rotation); 
					explocion = false;
				}else if(quemado)
				{
					v3 = Vector3.zero;
					animator.SetBool("muerto", true);
					animator.SetInteger("muerte", 20);
				}else if(disparoCabeza)
				{
					v3 = Vector3.zero;
					animator.SetBool("headShot", true);
					animator.SetBool("muerto", true);
					animator.SetInteger("muerte", 4);
				}else
				{
					animator.SetBool("muerto", true);
					muerte = Random.Range(1,3);
					animator.SetInteger("muerte", muerte);
				}
				SniperCam.GetComponent<CamNetwork>().alejar = false;
				SniperCam.GetComponent<CamNetwork>().campamento = true;
				mascara = "muerto";
				Base.layer = LayerMask.NameToLayer("mira");
				CmdChangeBase("mira");
				CmdChangeMascara(mascara);

				StartCoroutine(muertee());
				vivo = false;
			}else
			{
				explocion = false;
			}
			//SI ESTA EN SNIPER
			if(sniperListo)
			{
				animator.SetBool("sniper", true);
				cuchillo = false;
				caminarA = false;
				caminarD = false;
				caminarI = false;
				caminarU = false;
			}
			if(arma == "sniper")
			{
				animator.SetBool("sniper", true);
			}else
			{
				animator.SetBool("sniper", false);
			}
		}else
		{
			SniperCam.GetComponent<LensAberrations>().vignette.intensity += 0.3f;

			if(SniperCam.GetComponent<LensAberrations>().vignette.intensity >= 1.7f)
			{
				SniperCam.GetComponent<LensAberrations>().vignette.intensity = 1.7f;
			}

			mira.SetActive(false);

			shoot = false;
			caminarI = false;
			caminarD = false;
			caminarU = false;
			caminarA = false;
			sniperListo = false;
			SniperCam.GetComponent<CamNetwork>().sniper = false;
			SniperCam.GetComponent<LensAberrations>().distortion.enabled = false;

			GetComponent<Sonidos>().fuego = false;

			quemado = false;

			SniperTexture.SetActive(false);

			animator.SetInteger("cascado", 0);
			animator.SetBool("walk", false);
			animator.SetBool("grounded", true);
			//gameObject.tag = "Untagged";
		}

		gameObject.layer = LayerMask.NameToLayer(mascara);

		if(muneco != null)
		{
			Cmd_Personaje(ordenLugar, muneco);
		}

		if(orden)
		{
			Cmd_Ordenando(ordenLugar, orden, muneco);

			orden = false;
		}

		if(Input.GetButtonDown("PAUSA"))
		{
			Pausa();
		}
	}

	public GameObject MenuPause;
	public void Pausa()
	{
		ready = false;
		SniperCam.GetComponent<Grayscale>().enabled = true;
		MenuPause.SetActive(true);
		eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(pausa1);
	}

	[Command]
	public void CmdChangeMascara(string newMascara)
	{
		RpcChangeMascara(newMascara);
		gameObject.layer = LayerMask.NameToLayer(newMascara);
	}
	[ClientRpc]
	public void RpcChangeMascara (string newMascara)
	{
		mascara = newMascara;
		gameObject.layer = LayerMask.NameToLayer(newMascara);
	}

	[Command]
	public void CmdChangeBase(string newBase)
	{
		RpcChangeBase(newBase);
	}
	[ClientRpc]
	public void RpcChangeBase (string newBase)
	{
		Base.layer = LayerMask.NameToLayer(newBase);
	}

	[Command]
	public void CmdSendSalud (float newSalud)
	{
		RpcSetSalud(newSalud);
	}
	[ClientRpc]
	public void RpcSetSalud (float newSalud)
	{
		if(!isLocalPlayer)
		{
			salud = newSalud;
		}
	}


	[Command]
	void Cmd_Personaje(Vector3 newLugar, GameObject newMuneco)
	{
		Rpc_SetPersonaje(newLugar, newMuneco);
	}
	[ClientRpc]
	public void Rpc_SetPersonaje (Vector3 newLugar, GameObject newMuneco)
	{
		if(!isLocalPlayer)
		{
			ordenLugar = newLugar;
			muneco = newMuneco;
		}
	}

	[Command]
	void Cmd_Ordenando(Vector3 newLugar, bool newOrden, GameObject newMuneco)
	{
		Rpc_SetOrden(newLugar, newOrden, newMuneco);

		orden = false;
	}
	[ClientRpc]
	public void Rpc_SetOrden (Vector3 newLugar, bool newOrden, GameObject newMuneco)
	{
		if(!isLocalPlayer)
		{
			muneco = newMuneco;
			ordenLugar = newLugar;
			orden = newOrden;

			StartCoroutine(esperaOrden());
		}
	}

	IEnumerator esperaOrden()
	{
		yield return new WaitForSeconds(0.03f);
		orden = false;
	}

	[Command]
	void CmdBalaLlamas()
	{
		if(_currentDirection == "right")
		{
			var bulletB = (GameObject)Instantiate(bulletPrefLlamas, bulletSpawnFuego.position, bulletSpawnFuego.rotation); 
			bulletB.GetComponent<Rigidbody>().velocity = bulletB.transform.right * 15;
			NetworkServer.Spawn(bulletB);
			Destroy(bulletB, 0.7f);
		}else
		{
			var bulletB = (GameObject)Instantiate(bulletPrefLlamas, bulletSpawnFuego.position, bulletSpawnFuego.rotation); 
			bulletB.GetComponent<Rigidbody>().velocity = bulletB.transform.right * -15;
			NetworkServer.Spawn(bulletB);
			Destroy(bulletB, 0.7f);
		}

	}
	[Command]
	void CmdBalaLlamasMalo()
	{
		if(_currentDirection == "right")
		{
			var bulletB = (GameObject)Instantiate(bulletPrefLlamasMalo, bulletSpawnFuego.position, bulletSpawnFuego.rotation); 
			bulletB.GetComponent<Rigidbody>().velocity = bulletB.transform.right * 15;
			NetworkServer.Spawn(bulletB);
			Destroy(bulletB, 0.7f);
		}else
		{
			var bulletB = (GameObject)Instantiate(bulletPrefLlamasMalo, bulletSpawnFuego.position, bulletSpawnFuego.rotation); 
			bulletB.GetComponent<Rigidbody>().velocity = bulletB.transform.right * -15;
			NetworkServer.Spawn(bulletB);
			Destroy(bulletB, 0.7f);
		}
	}
	public GameObject estela;
	void disparoSniper()
	{
		if(balaSniper >= 1)
		{
			SniperCam.GetComponent<CamNetwork>().disparo = true;
			SniperCam.GetComponent<CamNetwork>().maximo = 0.5f;

			SniperCam.GetComponent<CamNetwork>().shake = true;
			SniperCam.GetComponent<CamNetwork>().disparo = true;
			SniperCam.GetComponent<CamNetwork>().vib = 0.3f;

			animator.SetInteger("disparo",2);

			var bullet = (GameObject)Instantiate(estela, SniperCam.transform.position, Quaternion.Euler(4,0,0));
			bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 200;
			Destroy(bullet, 2f);


			if(gameObject.tag == "Player")
			{
				CmdBalaSniperBueno();
			}else
			{
				CmdBalaSniperMalo();
			}

			balaSniper -= 1;
		}else
		{
			animator.SetInteger("recarga", 2);
		}
		StartCoroutine(sniperQuita());
	}
	IEnumerator sniperQuita()
	{
		yield return new WaitForSeconds(0.8f);
		sniperListo = false;
		animator.SetBool("apuntando", false);
		SniperCam.GetComponent<CamNetwork>().sniper = false;
		sniperListo = false;
		SniperTexture.SetActive(false);
		esconderBarra.SetActive(true);
		SniperCam.GetComponent<LensAberrations>().distortion.enabled = false;
	}
	[Command]
	void CmdBalaSniperBueno()
	{
		var bullet = (GameObject)Instantiate(bulletPrefSniper, bulletSniperSpawn.position, Quaternion.Euler(4,0,0));
		NetworkServer.Spawn(bullet);
		bullet.GetComponent<balaSniper>().poder = saludMax*bullet.GetComponent<balaSniper>().poder/104;

		Destroy(bullet, 0.5f);
	}
	[Command]
	void CmdBalaSniperMalo()
	{
		var bullet = (GameObject)Instantiate(bulletPrefSniperMalo, bulletSniperSpawn.position, Quaternion.Euler(4,0,0));
		NetworkServer.Spawn(bullet);
		bullet.GetComponent<balaSniper>().poder = saludMax2*bullet.GetComponent<balaSniper>().poder/104;

		Destroy(bullet, 0.5f);
	}

	void Disparo()
	{
		//shoot = false;
		if(arma2)
		{
			if(balaPistola >= 1)
			{
				StartCoroutine(resetArma());
				animator.SetInteger("disparo", 5);
				StartCoroutine(esperaPistola());
			}else if(balaPistola <= 0)
			{
				rafaga = true;
				animator.SetInteger("recarga", -1);
			}
		}
		if(lansallamas)
		{
			if(balaLlamas >= 1)
			{
				//Llamas.sonar = true;
				GetComponent<Sonidos>().fuego = true;
				llamas = true;
				animator.SetBool("llamas", true);
				rafaga = true;
			}else if(balaLlamas <= 0)
			{
				GetComponent<Sonidos>().fuego = false;
				rafaga = true;
				//animator.SetInteger("recarga", 12);
				animator.SetBool("flameando", false);
				animator.SetBool("llamas", false);
				llamas = false;
				lansallamas = false;
			}
		}
		if(!arma2 && !lansallamas)
		{
			if(arma == "escopeta" && balaEscopeta >= 1)
			{
				animator.SetInteger("disparo", 1);
				StartCoroutine(esperaEscopeta());
			}else if(arma == "escopeta" && balaEscopeta == 0 && EscopetaTotales >= 1)
			{
				animator.SetInteger("recarga", 1);
			}else if(arma == "escopeta" && EscopetaTotales == 0)
			{
				rafaga = true;
			}

			if(arma == "fusil" && balaFusil >= 1)
			{
				animator.SetInteger("disparo", 2);
				StartCoroutine(esperaFusil());
			}else if(arma == "fusil" && balaFusil == 0 && FusilTotales >= 1)
			{
				animator.SetInteger("recarga", 2);
			}else if(arma == "fusil" && balaFusil == 0 && FusilTotales == 0)
			{
				rafaga = true;
			}

			if(arma == "submetra" && balaSubmetra >= 1)
			{
				animator.SetInteger("disparo", 3);
				StartCoroutine(esperaSubmetra());
			}else if(arma == "submetra" && balaSubmetra == 0 && SubmetraTotales >= 1)
			{
				animator.SetInteger("recarga", 3);
			}else if(arma == "submetra" && SubmetraTotales == 0)
			{
				rafaga = true;
			}

			if(arma == "metra" && balaMetra >= 1)
			{
				animator.SetInteger("disparo", 4);
				StartCoroutine(esperaMetra());
			}else if(arma == "metra" && balaMetra == 0 && MetraTotales >= 1)
			{
				animator.SetInteger("recarga", 3);
			}else if(arma == "metra" && MetraTotales == 0)
			{
				rafaga = true;
			}

			if(arma == "panzer" && balaPanzer >= 1)
			{
				animator.SetInteger("disparo", 7);
				StartCoroutine(esperaPanzer());
			}else if(arma == "panzer" && balaPanzer == 0 && PanzerTotales >= 1)
			{
				animator.SetInteger("recarga", 7);
			}else if(arma == "panzer" && PanzerTotales == 0)
			{
				rafaga = true;
			}
		}
	}
	//SE QUEDA CUBIERTO
	IEnumerator cover ()
	{
		yield return new WaitForSeconds(0.1f);
		animator.SetBool("cubierto", true);
	}
	//TIEMPOS DE ESPERA SIGUIENTE DISPARO
	IEnumerator muertee ()
	{
		yield return new WaitForSeconds(5f);
		GetComponent<HeroNetwork>().menu.SetActive(true);
		SniperCam.GetComponent<Grayscale>().enabled = true;
		eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(muerto1);
	}
	IEnumerator resetArma ()
	{
		yield return new WaitForSeconds(0.1f);
		animator.SetInteger("disparo", 0);
	}
	IEnumerator esperaPistola ()
	{
		yield return new WaitForSeconds(0.2f);
		rafaga = true;
	}
	IEnumerator esperaPistolaAire ()
	{
		yield return new WaitForSeconds(0.5f);
		rafaga = true;
	}
	IEnumerator esperaEscopeta ()
	{
		yield return new WaitForSeconds(0.2f);
		rafaga = true;
	}
	IEnumerator esperaFusil ()
	{
		yield return new WaitForSeconds(0.3f);//0.8
		rafaga = true;
	}
	IEnumerator esperaSubmetra ()
	{
		yield return new WaitForSeconds(0.5f);
		rafaga = true;
	}
	IEnumerator esperaMetra ()
	{
		yield return new WaitForSeconds(0.4f);//0.4
		rafaga = true;
	}
	IEnumerator esperaPanzer()
	{
		yield return new WaitForSeconds(0.5f);
		rafaga = true;
	}
	IEnumerator esperaG ()//GRANADA
	{
		yield return new WaitForSeconds(1f);
		Gready = true;
	}
	IEnumerator esperaSniper ()//SNIPER
	{
		yield return new WaitForSeconds(0.4f);
		rafaga = true;
		//CmdBalaSniperFalse();
		bulletPrefSniper.SetActive(false);
	}
	//TIEMPO POSICION CUBRIRSE
	IEnumerator coverTiempo ()
	{
		yield return new WaitForSeconds(0.7f);
		cubierto = false;
	}
	//CAMBIAR DIRECCION
	[Command]
	public void CmdChangeDirection(string direction)
	{
		if (_currentDirection != direction)
		{
			if (direction == "right")
			{
				//transform.rotation = Quaternion.Euler(0,0,0);
				//GetComponent<SkeletonAnimator>().zSpacing = -0.0002f;
				transform.localScale = new Vector3(1,1,1);
				//Girar2.transform.Rotate (0, 180, 0);
				Girar2.GetComponent<GirarNetwork>().voltear = true;
				_currentDirection = "right";
			} 
			else if (direction == "left") 
			{
				//transform.rotation = Quaternion.Euler(0,180,0);
				//GetComponent<SkeletonAnimator>().zSpacing = 0.0002f;
				transform.localScale = new Vector3(-1,1,1);
				//Girar2.transform.Rotate (0, -180, 0);
				Girar2.GetComponent<GirarNetwork>().voltear = true;
				_currentDirection = "left";
			}
		}
	}

	void cambiavivo (bool newvivo)
	{
		vivo = newvivo;
		//gameObject.layer = LayerMask.NameToLayer("muerto");
	}
	/*void cambiavivo2 (string mascara)
	{
		gameObject.layer = LayerMask.NameToLayer(mascara);
	}*/

	void FacingCallback(string direction)
	{
		if (_currentDirection != direction)
		{
			if (direction == "right")
			{
				//transform.rotation = Quaternion.Euler(0,0,0);
				//GetComponent<SkeletonAnimator>().zSpacing = -0.0002f;
				transform.localScale = new Vector3(1,1,1);
				//Girar2.transform.Rotate (0, 180, 0);
				Girar2.GetComponent<GirarNetwork>().voltear = true;
				_currentDirection = "right";
			} 
			else if (direction == "left") 
			{
				//transform.rotation = Quaternion.Euler(0,180,0);
				//GetComponent<SkeletonAnimator>().zSpacing = 0.0002f;
				transform.localScale = new Vector3(-1,1,1);
				//Girar2.transform.Rotate (0, -180, 0);
				Girar2.GetComponent<GirarNetwork>().voltear = true;
				_currentDirection = "left";
			}
		}
	}

	void OnChangeHealth(float salud)
	{
		Health.fillAmount = salud/saludMax;

		if(salud <= saludMax*70/100)
		{
			Health.color = new Color32(255,0,0,255);
		}else
		{
			Health.color = new Color32(0,255,0,255);
		}
		CmdSangre();
	}
	[Command]
	public void CmdSangre()
	{
		Health.fillAmount = salud/saludMax;

		if(salud <= saludMax*70/100)
		{
			Health.color = new Color32(255,0,0,255);
		}else
		{
			Health.color = new Color32(0,255,0,255);
		}
	}

	//COLLISIONS
	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "Water")
		{
			water = true;
			maxspeed = 8;
		}
		if(col.gameObject.tag == "Piso")
		{
			water = false;
			maxspeed = 13;
		}
		if(col.gameObject.tag == "bala")
		{
			saludSumar = false;
			rafaga = true;
			animator.SetBool("walk", false);
			caminarA = false;
			caminarD = false;
			caminarI = false;
			caminarU = false;
			velocidad = 0;
			v3 = Vector3.zero;
			if(grounded)
			{
				animator.SetInteger("cascado", 1);
			}

			salud -= col.gameObject.GetComponent<bala>().poder; //15

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = col.gameObject.GetComponent<bala>().poder.ToString("F0");
			//letras.GetComponent<TextMesh>().text = "15";
			StartCoroutine(sumar());
			efectodisparo = true;
		}
		/*if(col.gameObject.tag == "balaFusil" && vivo)
		{
			saludSumar = false;
			animator.SetBool("walk",false);
			caminarA = false;
			caminarD = false;
			caminarI = false;
			caminarU = false;
			velocidad = 0;
			v3 = Vector3.zero;
			if(grounded)
			{
				animator.SetInteger("cascado", 1);
			}
			salud -= 40;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = "40";
			StartCoroutine(sumar());
			efectodisparo = true;
			print("TIRO EN LA CABEZA CON FUSIL CUERPO");
		}
		if(col.gameObject.tag == "balaEscopeta" && vivo)
		{
			saludSumar = false;
			animator.SetBool("walk",false);
			caminarA = false;
			caminarD = false;
			caminarI = false;
			caminarU = false;
			velocidad = 0;
			v3 = Vector3.zero;
			if(grounded)
			{
				animator.SetInteger("cascado", 1);
			}
			salud -= 15;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = "15";
			StartCoroutine(sumar());
			efectodisparo = true;
		}
		if(col.gameObject.tag == "balaSubmetra" && vivo)
		{
			saludSumar = false;
			animator.SetBool("walk",false);
			caminarA = false;
			caminarD = false;
			caminarI = false;
			caminarU = false;
			velocidad = 0;
			v3 = Vector3.zero;
			if(grounded)
			{
				animator.SetInteger("cascado", 1);
			}
			salud -= 20;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = "20";
			StartCoroutine(sumar());
			efectodisparo = true;
		}
		if(col.gameObject.tag == "balaMetra" && vivo)
		{
			saludSumar = false;
			animator.SetBool("walk",false);
			caminarA = false;
			caminarD = false;
			caminarI = false;
			caminarU = false;
			velocidad = 0;
			v3 = Vector3.zero;
			if(grounded)
			{
				animator.SetInteger("cascado", 1);
			}
			salud -= 25;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = "25";
			StartCoroutine(sumar());
			efectodisparo = true;
		}
		if(col.gameObject.tag == "balaMG" && vivo)
		{
			saludSumar = false;
			rafaga = true;
			animator.SetBool("walk", false);
			caminarA = false;
			caminarD = false;
			caminarI = false;
			caminarU = false;
			velocidad = 0;
			v3 = Vector3.zero;
			//salud -= 25;
			if(grounded)
			{
				animator.SetInteger("cascado", 1);
			}

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			//letras.GetComponent<TextMesh>().text = "25";
			letras.GetComponent<TextMesh>().text = col.gameObject.GetComponent<bala>().poder.ToString();//"25";
			StartCoroutine(sumar());
			efectodisparo = true;
		}
		if(col.gameObject.tag == "balaSniper" && vivo)
		{
			saludSumar = false;
			rafaga = true;
			animator.SetBool("walk", false);
			caminarA = false;
			caminarD = false;
			caminarI = false;
			caminarU = false;
			velocidad = 0;
			v3 = Vector3.zero;
			salud -= 50;
			if(grounded)
			{
				animator.SetInteger("cascado", 1);
			}
			Destroy(col.gameObject);

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = "50";

			StartCoroutine(sumar());
			efectodisparo = true;
		}*/
		if(col.gameObject.tag == "explo")
		{
			saludSumar = false;
			if(PlayerPrefs.GetInt("violencia") == 1)
			{
				explocion = true;
			}

			caminarA = false;
			caminarD = false;
			caminarI = false;
			caminarU = false;
			velocidad = 0;
			v3 = Vector3.zero;

			animator.SetBool("granada", true);
			animator.SetInteger("cascado", 10);

			salud -= col.gameObject.GetComponent<Explo>().poder;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = col.gameObject.GetComponent<Explo>().poder.ToString("F0");

			StartCoroutine(sumar());
			efectodisparo = true;
		}
		if(col.gameObject.tag == "cuchillo" && vivo)
		{
			saludSumar = false;
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			animator.SetBool("walk",false);
			caminarA = false;
			caminarD = false;
			caminarI = false;
			caminarU = false;
			velocidad = 0;
			v3 = Vector3.zero;
			animator.SetInteger("cascado", 4);

			if(PlayerPrefs.GetInt("violencia") == 1)
			{
				//var sangre2 = (GameObject)Instantiate(sangreCuchillo[Random.Range(0,sangreCuchillo.Length)], cascadoSpawn.position, cascadoSpawn.rotation); 
			}
			salud -= 50;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = "50";
			StartCoroutine(sumar());
			efectodisparo = true;
		}
		if(col.gameObject.tag == "granade")
		{
			granadas += 3;
		}
	}
	[Command]
	public void CmdHeadShot()
	{
		salud = 0;
	}
	bool quemado;
	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.name == "uno")
		{
			CamNetwork.Area2 = !CamNetwork.Area2;
		}
		if(col.gameObject.name == "dos")
		{
			CamNetwork.Area3 = !CamNetwork.Area3;
		}
		if(col.gameObject.tag == "mira" && vivo)
		{
			mira.SetActive(true);
			mira.GetComponent<Animator>().SetBool("entry",true);
		}
		if(col.gameObject.tag == "balaLlamas" && vivo)
		{
			if(grounded)
			{
				animator.SetInteger("cascado", 1);
			}

			caminarA = false;
			caminarD = false;
			caminarI = false;
			caminarU = false;
			velocidad = 0;
			v3 = Vector3.zero;

			quemado = true;

			salud -= 2;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = "2";
			StartCoroutine(sumar());
		}
		if(col.gameObject.tag == medic)
		{
			if(salud < saludMax)
			{
				//saludSumar = true;
				disparoCabeza = false;
				CmdSendSalud(salud);
				CmdSaludSumar();
			}
		}
	}

	/*void OnTriggerStay (Collider col)
	{
		if(col.gameObject.tag == medic)
		{
			//saludSumar = true;
			salud += 0.1f;
			disparoCabeza = false;
			CmdSendSalud(salud);
		}
	}*/
	void OnTriggerExit (Collider col)
	{
		if(col.gameObject.tag == "mira" && vivo)
		{
			mira.SetActive(false);
		}
		if(col.gameObject.tag == "cobertura" && vivo)
		{
			cuchillo = false;
			cubrirse = false;
		}
		if(col.gameObject.tag == "balaLlamas" && vivo)
		{
			quemado = false;
		}
		if(col.gameObject.tag == medic)
		{
			print("YA NO SUMA LA SANGRE");
			saludSumar = false;
		}
	}

	IEnumerator sumar()
	{
		yield return new WaitForSeconds(5);
		saludSumar = true;
	}

	//EVENTOS SPINE
	public void Shot ()//PISTOLA
	{
		if(!isLocalPlayer)
		{
			return;
		}

		if(!grounded)// && animator.GetInteger("disparo") == 8)
		{
			luz.SetActive(true);
			StartCoroutine(apaga());
			if(arma2)
			{
				if(balaPistola >= 1)
				{
					if(gameObject.tag == "Player")
					{
						CmdPistolaDisparoPisoBueno();
					}else
					{
						CmdPistolaDisparoPisoMalo();
					}

					SniperCam.GetComponent<CamNetwork>().shake = true;
					SniperCam.GetComponent<CamNetwork>().vib = 0.3f;
					luz.SetActive(true);
					StartCoroutine(apaga());
					balaPistola -= 1;
				}
			}else
			{
				if(arma == "escopeta")
				{
					if(balaEscopeta >= 1)
					{
						SniperCam.GetComponent<CamNetwork>().sube = true;

						SniperCam.GetComponent<CamNetwork>().shake = true;
						SniperCam.GetComponent<CamNetwork>().vib = 0.7f;

						luz.SetActive(true);
						StartCoroutine(apaga());
						balaEscopeta -= 1;

						if(gameObject.tag == "Player")
						{
							CmdEscopetaDisparoBueno();
						}else
						{
							CmdEscopetaDisparoMalo();
						}
					}
				}
				if(arma == "fusil")
				{
					if(balaFusil >= 1 )
					{
						SniperCam.GetComponent<CamNetwork>().disparo = true;
						SniperCam.GetComponent<CamNetwork>().maximo = 1f;

						SniperCam.GetComponent<CamNetwork>().shake = true;
						SniperCam.GetComponent<CamNetwork>().vib = 0.55f;

						luz.SetActive(true);
						StartCoroutine(apaga());
						balaFusil -= 1;
						if(gameObject.tag == "Player")
						{
							CmdFusilDisparoBueno();
						}else
						{
							CmdFusilDisparoMalo();
						}
					}
				}
				if(arma == "submetra")
				{
					if(balaSubmetra >= 1)
					{
						SniperCam.GetComponent<CamNetwork>().disparo = true;
						SniperCam.GetComponent<CamNetwork>().maximo = 0.4f;
						SniperCam.GetComponent<CamNetwork>().shake = true;
						SniperCam.GetComponent<CamNetwork>().vib = 0.4f;
						luz.SetActive(true);
						StartCoroutine(apaga());
						balaSubmetra -= 1;
						if(gameObject.tag == "Player")
						{
							CmdSubmetraDisparoBueno();
						}else
						{
							CmdSubmetraDisparoMalo();
						}
					}
				}
				if(arma == "metra")
				{
					if(balaMetra >= 1)
					{
						SniperCam.GetComponent<CamNetwork>().disparo = true;
						SniperCam.GetComponent<CamNetwork>().maximo = 0.6f;

						//SniperCam.GetComponent<CamNetwork>().sube = true;

						SniperCam.GetComponent<CamNetwork>().shake = true;
						SniperCam.GetComponent<CamNetwork>().vib = 0.7f;
						luz.SetActive(true);
						StartCoroutine(apaga());
						balaMetra -= 1;
						if(gameObject.tag == "Player")
						{
							CmdMetraDisparoBueno();
						}else
						{
							CmdMetraDisparoMalo();
						}
					}
				}
				if(arma == "panzer")
				{
					if(balaPanzer >= 1)
					{
						SniperCam.GetComponent<CamNetwork>().disparo = true;
						SniperCam.GetComponent<CamNetwork>().maximo = 0.4f;

						SniperCam.GetComponent<CamNetwork>().shake = true;
						SniperCam.GetComponent<CamNetwork>().vib = 0.7f;

						luz.SetActive(true);
						StartCoroutine(apaga());

						balaPanzer -= 1;

						if(gameObject.tag == "Player")
						{
							CmdPanzerDisparoBueno();
						}else
						{
							CmdPanzerDisparoMalo();
						}
					}
				}
				if(arma == "lansallamas")
				{
					if(balaLlamas >= 1)
					{
						GetComponent<Sonidos>().fuego = true;
						llamas = true;
						rafaga = true;
					}
				}
			}
		}else
		{
			if(!isLocalPlayer)
			{
				return;
			}

			if(gameObject.tag == "Player")
			{
				CmdPistolaDisparoPisoBueno();
			}else
			{
				CmdPistolaDisparoPisoMalo();
			}

			SniperCam.GetComponent<CamNetwork>().shake = true;
			SniperCam.GetComponent<CamNetwork>().vib = 0.3f;
			luz.SetActive(true);
			StartCoroutine(apaga());
			balaPistola -= 1;
		}
	}
	float suma;
	[Command]
	public void CmdSaludSumar()
	{
		suma = saludMax*6/104;
		salud += saludMax*6/104;
		//salud += 6;

		var letras = (GameObject)Instantiate(textos2, transform.position, Quaternion.Euler(0,0,0));
		letras.GetComponent<TextMesh>().text = suma.ToString("F0");
		NetworkServer.Spawn(letras);

		var part = (GameObject)Instantiate(particulasCurar, transform.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(part);
	}

	[Command]
	public void CmdPistolaDisparoPisoBueno()
	{
		var bullet = (GameObject)Instantiate(bulletPref, bulletSpawn.position, bulletSpawn.rotation);
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.right * 100;
		NetworkServer.Spawn(bullet);
		bullet.GetComponent<bala>().poder = saludMax*bullet.GetComponent<bala>().poder/104;
		Destroy(bullet, 1.0f);

		//CASQUILLOS}
		var casquillo = (GameObject)Instantiate(casquilloPref, casquilloSpawn.position, casquilloSpawn.rotation); 
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.right * Random.Range(-10,-50);
		casquillo.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(-0.0f,-0.2f));
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.up * Random.Range(-1,-6);
		NetworkServer.Spawn(casquillo);
	}
	[Command]
	public void CmdPistolaDisparoPisoMalo()
	{
		var bullet = (GameObject)Instantiate(bulletPrefMalo, bulletSpawn.position, bulletSpawn.rotation);
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.right * 100;
		NetworkServer.Spawn(bullet);
		bullet.GetComponent<bala>().poder = saludMax2*bullet.GetComponent<bala>().poder/104;
		Destroy(bullet, 1.0f);

		//CASQUILLOS}
		var casquillo = (GameObject)Instantiate(casquilloPref, casquilloSpawn.position, casquilloSpawn.rotation); 
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.right * Random.Range(-10,-50);
		casquillo.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(-0.0f,-0.2f));
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.up * Random.Range(-1,-6);
		NetworkServer.Spawn(casquillo);
	}
	void ShotA ()//FUSIL
	{
		if(!isLocalPlayer)
		{
			return;
		}

		if(!sniperListo)
		{
			SniperCam.GetComponent<CamNetwork>().disparo = true;
			SniperCam.GetComponent<CamNetwork>().maximo = 1f;

			/*SniperCam.GetComponent<CamNetwork>().sube = true;*/

			SniperCam.GetComponent<CamNetwork>().shake = true;
			SniperCam.GetComponent<CamNetwork>().vib = 0.55f;

			luz.SetActive(true);
			StartCoroutine(apaga());
			balaFusil -= 1;
			if(gameObject.tag == "Player")
			{
				CmdFusilDisparoBueno();
			}else
			{
				CmdFusilDisparoMalo();
			}
		}
	}
	[Command]
	public void CmdFusilDisparoBueno()
	{
		var bullet = (GameObject)Instantiate(bulletPrefFusil, bulletSpawn.position, bulletSpawn.rotation); 
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.right * 100;
		NetworkServer.Spawn(bullet);
		bullet.GetComponent<bala>().poder = saludMax*bullet.GetComponent<bala>().poder/104;
		Destroy(bullet, 1.0f);
		//CASQUILLOS
		var casquillo = (GameObject)Instantiate(casquilloPref, casquilloSpawn.position, casquilloSpawn.rotation); 
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.right * Random.Range(-10,-50);
		casquillo.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(-0.0f,-0.2f));
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.up * Random.Range(-1,-6);
		NetworkServer.Spawn(casquillo);
	}
	[Command]
	public void CmdFusilDisparoMalo()
	{
		var bullet = (GameObject)Instantiate(bulletPrefFusilMalo, bulletSpawn.position, bulletSpawn.rotation); 
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.right * 100;
		NetworkServer.Spawn(bullet);
		bullet.GetComponent<bala>().poder = saludMax2*bullet.GetComponent<bala>().poder/104;
		Destroy(bullet, 1.0f);
		//CASQUILLOS}
		var casquillo = (GameObject)Instantiate(casquilloPref, casquilloSpawn.position, casquilloSpawn.rotation); 
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.right * Random.Range(-10,-50);
		casquillo.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(-0.0f,-0.2f));
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.up * Random.Range(-1,-6);
		NetworkServer.Spawn(casquillo);
	}
	void ShotB ()//ESCOPETA
	{
		if(!isLocalPlayer)
		{
			return;
		}

		/*SniperCam.GetComponent<CamNetwork>().disparo = true;
		SniperCam.GetComponent<CamNetwork>().maximo = 0.7f;

		SniperCam.GetComponent<CamNetwork>().shake = true;
		SniperCam.GetComponent<CamNetwork>().vib = 0.7f;*/

		SniperCam.GetComponent<CamNetwork>().sube = true;

		SniperCam.GetComponent<CamNetwork>().shake = true;
		SniperCam.GetComponent<CamNetwork>().vib = 0.7f;

		luz.SetActive(true);
		StartCoroutine(apaga());
		balaEscopeta -= 1;

		if(gameObject.tag == "Player")
		{
			CmdEscopetaDisparoBueno();
		}else
		{
			CmdEscopetaDisparoMalo();
		}
	}
	[Command]
	public void CmdEscopetaDisparoBueno()
	{
		if(_currentDirection == "right")
		{
			var bulletB = (GameObject)Instantiate(bulletPrefEscopeta, bulletSpawn.position, Quaternion.Euler(0,Random.Range(-15,16),bulletSpawn.rotation.eulerAngles.z+Random.Range(5,16)));//Quaternion.Euler(0,0,bulletSpawn.rotation.z+10)
			bulletB.GetComponent<Rigidbody>().velocity = bulletB.transform.right * 100;
			NetworkServer.Spawn(bulletB);
			bulletB.GetComponent<bala>().poder = saludMax*bulletB.GetComponent<bala>().poder/104;
			Destroy(bulletB, 0.5f);

			var bulletB2 = (GameObject)Instantiate(bulletPrefEscopeta, bulletSpawn.position, Quaternion.Euler(0,Random.Range(-15,16),bulletSpawn.rotation.eulerAngles.z+Random.Range(5,16)));//Quaternion.Euler(0,0,bulletSpawn.rotation.z+5)
			bulletB2.GetComponent<Rigidbody>().velocity = bulletB2.transform.right * 100;
			NetworkServer.Spawn(bulletB2);
			bulletB2.GetComponent<bala>().poder = saludMax*bulletB2.GetComponent<bala>().poder/104;
			Destroy(bulletB2, 0.5f);

			var bulletB3 = (GameObject)Instantiate(bulletPrefEscopeta, bulletSpawn.position, bulletSpawn.rotation);//Quaternion.Euler(0,0,0)
			bulletB3.GetComponent<Rigidbody>().velocity = bulletB3.transform.right * 100;
			NetworkServer.Spawn(bulletB3);
			bulletB3.GetComponent<bala>().poder = saludMax*bulletB3.GetComponent<bala>().poder/104;
			Destroy(bulletB3, 0.5f);

			var bulletB4 = (GameObject)Instantiate(bulletPrefEscopeta, bulletSpawn.position, Quaternion.Euler(0,Random.Range(-15,16),bulletSpawn.rotation.eulerAngles.z-Random.Range(-5,-16)));//Quaternion.Euler(0,0,-5)
			bulletB4.GetComponent<Rigidbody>().velocity = bulletB4.transform.right * 100;
			NetworkServer.Spawn(bulletB4);
			bulletB4.GetComponent<bala>().poder = saludMax*bulletB4.GetComponent<bala>().poder/104;
			Destroy(bulletB4, 0.5f);

			var bulletB5 = (GameObject)Instantiate(bulletPrefEscopeta, bulletSpawn.position, Quaternion.Euler(0,Random.Range(-15,16),bulletSpawn.rotation.eulerAngles.z-Random.Range(-5,-16)));
			bulletB5.GetComponent<Rigidbody>().velocity = bulletB5.transform.right * 100;
			NetworkServer.Spawn(bulletB5);
			bulletB5.GetComponent<bala>().poder = saludMax*bulletB5.GetComponent<bala>().poder/104;
			Destroy(bulletB5, 0.5f);
		}else
		{
			var bulletB = (GameObject)Instantiate(bulletPrefEscopeta, bulletSpawn.position, Quaternion.Euler(0,180+Random.Range(-15,16),bulletSpawn.rotation.eulerAngles.z+Random.Range(5,16))); 
			bulletB.GetComponent<Rigidbody>().velocity = bulletB.transform.right * 100;
			NetworkServer.Spawn(bulletB);
			bulletB.GetComponent<bala>().poder = saludMax*bulletB.GetComponent<bala>().poder/104;
			Destroy(bulletB, 0.5f);

			var bulletB2 = (GameObject)Instantiate(bulletPrefEscopeta, bulletSpawn.position, Quaternion.Euler(0,180+Random.Range(-15,16),bulletSpawn.rotation.eulerAngles.z+Random.Range(5,16))); 
			bulletB2.GetComponent<Rigidbody>().velocity = bulletB2.transform.right * 100;
			NetworkServer.Spawn(bulletB2);
			bulletB2.GetComponent<bala>().poder = saludMax*bulletB2.GetComponent<bala>().poder/104;
			Destroy(bulletB2, 0.5f);

			var bulletB3 = (GameObject)Instantiate(bulletPrefEscopeta, bulletSpawn.position, bulletSpawn.rotation); 
			bulletB3.GetComponent<Rigidbody>().velocity = bulletB3.transform.right * 100;
			NetworkServer.Spawn(bulletB3);
			bulletB3.GetComponent<bala>().poder = saludMax*bulletB3.GetComponent<bala>().poder/104;
			Destroy(bulletB3, 0.5f);

			var bulletB4 = (GameObject)Instantiate(bulletPrefEscopeta, bulletSpawn.position, Quaternion.Euler(0,180+Random.Range(-15,16),bulletSpawn.rotation.eulerAngles.z+Random.Range(-5,-16))); 
			bulletB4.GetComponent<Rigidbody>().velocity = bulletB4.transform.right * 100;
			NetworkServer.Spawn(bulletB4);
			bulletB4.GetComponent<bala>().poder = saludMax*bulletB4.GetComponent<bala>().poder/104;
			Destroy(bulletB4, 0.5f);

			var bulletB5 = (GameObject)Instantiate(bulletPrefEscopeta, bulletSpawn.position, Quaternion.Euler(0,180+Random.Range(-15,16),bulletSpawn.rotation.eulerAngles.z+Random.Range(-5,-16))); 
			bulletB5.GetComponent<Rigidbody>().velocity = bulletB5.transform.right * 100;
			NetworkServer.Spawn(bulletB5);
			bulletB5.GetComponent<bala>().poder = saludMax*bulletB5.GetComponent<bala>().poder/104;
			Destroy(bulletB5, 0.5f);
		}
		//CASQUILLOS}
		var casquillo = (GameObject)Instantiate(casquilloPrefB, casquilloSpawn.position, casquilloSpawn.rotation); 
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.right * Random.Range(-10,-50);
		casquillo.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(-0.0f,-0.2f));
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.up * Random.Range(-1,-6);
		NetworkServer.Spawn(casquillo);
	}
	[Command]
	public void CmdEscopetaDisparoMalo()
	{
		if(_currentDirection == "right")
		{
			var bulletB = (GameObject)Instantiate(bulletPrefEscopetaMalo, bulletSpawn.position, Quaternion.Euler(0,Random.Range(-15,16),bulletSpawn.rotation.eulerAngles.z+Random.Range(5,16)));//Quaternion.Euler(0,0,bulletSpawn.rotation.z+10)
			bulletB.GetComponent<Rigidbody>().velocity = bulletB.transform.right * 100;
			NetworkServer.Spawn(bulletB);
			bulletB.GetComponent<bala>().poder = saludMax2*bulletB.GetComponent<bala>().poder/104;
			Destroy(bulletB, 0.5f);

			var bulletB2 = (GameObject)Instantiate(bulletPrefEscopetaMalo, bulletSpawn.position, Quaternion.Euler(0,Random.Range(-15,16),bulletSpawn.rotation.eulerAngles.z+Random.Range(5,16)));//Quaternion.Euler(0,0,bulletSpawn.rotation.z+5)
			bulletB2.GetComponent<Rigidbody>().velocity = bulletB2.transform.right * 100;
			NetworkServer.Spawn(bulletB2);
			bulletB2.GetComponent<bala>().poder = saludMax2*bulletB2.GetComponent<bala>().poder/104;
			Destroy(bulletB2, 0.5f);

			var bulletB3 = (GameObject)Instantiate(bulletPrefEscopetaMalo, bulletSpawn.position, bulletSpawn.rotation);//Quaternion.Euler(0,0,0)
			bulletB3.GetComponent<Rigidbody>().velocity = bulletB3.transform.right * 100;
			NetworkServer.Spawn(bulletB3);
			bulletB3.GetComponent<bala>().poder = saludMax2*bulletB3.GetComponent<bala>().poder/104;
			Destroy(bulletB3, 0.5f);

			var bulletB4 = (GameObject)Instantiate(bulletPrefEscopetaMalo, bulletSpawn.position, Quaternion.Euler(0,Random.Range(-15,16),bulletSpawn.rotation.eulerAngles.z-Random.Range(-5,-16)));//Quaternion.Euler(0,0,-5)
			bulletB4.GetComponent<Rigidbody>().velocity = bulletB4.transform.right * 100;
			NetworkServer.Spawn(bulletB4);
			bulletB4.GetComponent<bala>().poder = saludMax2*bulletB4.GetComponent<bala>().poder/104;
			Destroy(bulletB4, 0.5f);

			var bulletB5 = (GameObject)Instantiate(bulletPrefEscopetaMalo, bulletSpawn.position, Quaternion.Euler(0,Random.Range(-15,16),bulletSpawn.rotation.eulerAngles.z-Random.Range(-5,-16)));
			bulletB5.GetComponent<Rigidbody>().velocity = bulletB5.transform.right * 100;
			NetworkServer.Spawn(bulletB5);
			bulletB5.GetComponent<bala>().poder = saludMax2*bulletB5.GetComponent<bala>().poder/104;
			Destroy(bulletB5, 0.5f);
		}else
		{
			var bulletB = (GameObject)Instantiate(bulletPrefEscopetaMalo, bulletSpawn.position, Quaternion.Euler(0,180+Random.Range(-15,16),bulletSpawn.rotation.eulerAngles.z+Random.Range(5,16))); 
			bulletB.GetComponent<Rigidbody>().velocity = bulletB.transform.right * 100;
			NetworkServer.Spawn(bulletB);
			bulletB.GetComponent<bala>().poder = saludMax2*bulletB.GetComponent<bala>().poder/104;
			Destroy(bulletB, 0.5f);

			var bulletB2 = (GameObject)Instantiate(bulletPrefEscopetaMalo, bulletSpawn.position, Quaternion.Euler(0,180+Random.Range(-15,16),bulletSpawn.rotation.eulerAngles.z+Random.Range(5,16))); 
			bulletB2.GetComponent<Rigidbody>().velocity = bulletB2.transform.right * 100;
			NetworkServer.Spawn(bulletB2);
			bulletB2.GetComponent<bala>().poder = saludMax2*bulletB2.GetComponent<bala>().poder/104;
			Destroy(bulletB2, 0.5f);

			var bulletB3 = (GameObject)Instantiate(bulletPrefEscopetaMalo, bulletSpawn.position, bulletSpawn.rotation); 
			bulletB3.GetComponent<Rigidbody>().velocity = bulletB3.transform.right * 100;
			NetworkServer.Spawn(bulletB3);
			bulletB3.GetComponent<bala>().poder = saludMax2*bulletB3.GetComponent<bala>().poder/104;
			Destroy(bulletB3, 0.5f);

			var bulletB4 = (GameObject)Instantiate(bulletPrefEscopetaMalo, bulletSpawn.position, Quaternion.Euler(0,180+Random.Range(-15,16),bulletSpawn.rotation.eulerAngles.z+Random.Range(-5,-16))); 
			bulletB4.GetComponent<Rigidbody>().velocity = bulletB4.transform.right * 100;
			NetworkServer.Spawn(bulletB4);
			bulletB4.GetComponent<bala>().poder = saludMax2*bulletB4.GetComponent<bala>().poder/104;
			Destroy(bulletB4, 0.5f);

			var bulletB5 = (GameObject)Instantiate(bulletPrefEscopetaMalo, bulletSpawn.position, Quaternion.Euler(0,180+Random.Range(-15,16),bulletSpawn.rotation.eulerAngles.z+Random.Range(-5,-16))); 
			bulletB5.GetComponent<Rigidbody>().velocity = bulletB5.transform.right * 100;
			NetworkServer.Spawn(bulletB5);
			bulletB5.GetComponent<bala>().poder = saludMax2*bulletB5.GetComponent<bala>().poder/104;
			Destroy(bulletB5, 0.5f);
		}
		//CASQUILLOS}
		var casquillo = (GameObject)Instantiate(casquilloPrefB, casquilloSpawn.position, casquilloSpawn.rotation); 
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.right * Random.Range(-10,-50);
		casquillo.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(-0.0f,-0.2f));
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.up * Random.Range(-1,-6);
		NetworkServer.Spawn(casquillo);
	}
	void ShotC ()//SUBMETRA
	{
		if(!isLocalPlayer)
		{
			return;
		}

		SniperCam.GetComponent<CamNetwork>().disparo = true;
		SniperCam.GetComponent<CamNetwork>().maximo = 0.4f;
		SniperCam.GetComponent<CamNetwork>().shake = true;
		SniperCam.GetComponent<CamNetwork>().vib = 0.4f;
		luz.SetActive(true);
		StartCoroutine(apaga());
		balaSubmetra -= 1;
		if(gameObject.tag == "Player")
		{
			CmdSubmetraDisparoBueno();
		}else
		{
			CmdSubmetraDisparoMalo();
		}
	}
	[Command]
	public void CmdSubmetraDisparoBueno()
	{
		var bullet = (GameObject)Instantiate(bulletPrefSubmetra, bulletSpawn.position, bulletSpawn.rotation); 
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.right * 100;
		NetworkServer.Spawn(bullet);
		bullet.GetComponent<bala>().poder = saludMax*bullet.GetComponent<bala>().poder/104;
		Destroy(bullet, 1.0f);
		//CASQUILLOS}
		var casquillo = (GameObject)Instantiate(casquilloPref, casquilloSpawn.position, casquilloSpawn.rotation); 
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.right * Random.Range(-10,-50);
		casquillo.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(-0.0f,-0.2f));
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.up * Random.Range(-1,-6);
		NetworkServer.Spawn(casquillo);
	}
	[Command]
	public void CmdSubmetraDisparoMalo()
	{
		var bullet = (GameObject)Instantiate(bulletPrefSubmetraMalo, bulletSpawn.position, bulletSpawn.rotation); 
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.right * 100;
		NetworkServer.Spawn(bullet);
		bullet.GetComponent<bala>().poder = saludMax2*bullet.GetComponent<bala>().poder/104;
		Destroy(bullet, 1.0f);
		//CASQUILLOS}
		var casquillo = (GameObject)Instantiate(casquilloPref, casquilloSpawn.position, casquilloSpawn.rotation); 
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.right * Random.Range(-10,-50);
		casquillo.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(-0.0f,-0.2f));
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.up * Random.Range(-1,-6);
		NetworkServer.Spawn(casquillo);
	}
	void ShotD ()//METRA
	{
		if(!isLocalPlayer)
		{
			return;
		}

		SniperCam.GetComponent<CamNetwork>().disparo = true;
		SniperCam.GetComponent<CamNetwork>().maximo = 0.6f;

		//SniperCam.GetComponent<CamNetwork>().sube = true;

		SniperCam.GetComponent<CamNetwork>().shake = true;
		SniperCam.GetComponent<CamNetwork>().vib = 0.7f;
		luz.SetActive(true);
		StartCoroutine(apaga());
		balaMetra -= 1;
		if(gameObject.tag == "Player")
		{
			CmdMetraDisparoBueno();
		}else
		{
			CmdMetraDisparoMalo();
		}
		print("metra");
	}
	[Command]
	public void CmdMetraDisparoBueno()
	{
		var bullet = (GameObject)Instantiate(bulletPrefMetra, bulletSpawn.position, bulletSpawn.rotation); 
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.right * 100;
		NetworkServer.Spawn(bullet);
		bullet.GetComponent<bala>().poder = saludMax*bullet.GetComponent<bala>().poder/104;
		Destroy(bullet, 1.0f);
		//CASQUILLOS}
		var casquillo = (GameObject)Instantiate(casquilloPref, casquilloSpawn.position, casquilloSpawn.rotation); 
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.right * Random.Range(-10,-50);
		casquillo.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(-0.0f,-0.2f));
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.up * Random.Range(-1,-6);
		NetworkServer.Spawn(casquillo);
	}
	[Command]
	public void CmdMetraDisparoMalo()
	{
		var bullet = (GameObject)Instantiate(bulletPrefMetraMalo, bulletSpawn.position, bulletSpawn.rotation); 
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.right * 100;
		NetworkServer.Spawn(bullet);
		bullet.GetComponent<bala>().poder = saludMax2*bullet.GetComponent<bala>().poder/104;
		Destroy(bullet, 1.0f);
		//CASQUILLOS}
		var casquillo = (GameObject)Instantiate(casquilloPref, casquilloSpawn.position, casquilloSpawn.rotation); 
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.right * Random.Range(-10,-50);
		casquillo.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(-0.0f,-0.2f));
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.up * Random.Range(-1,-6);
		NetworkServer.Spawn(casquillo);
	}
	public GameObject HumoPanzer;
	void ShotE ()//PANZER
	{
		if(!isLocalPlayer)
		{
			return;
		}

		SniperCam.GetComponent<CamNetwork>().disparo = true;
		SniperCam.GetComponent<CamNetwork>().maximo = 0.4f;

		SniperCam.GetComponent<CamNetwork>().shake = true;
		SniperCam.GetComponent<CamNetwork>().vib = 0.7f;

		luz.SetActive(true);
		StartCoroutine(apaga());

		balaPanzer -= 1;

		if(gameObject.tag == "Player")
		{
			CmdPanzerDisparoBueno();
		}else
		{
			CmdPanzerDisparoMalo();
		}

		GetComponent<CustomFinalNetwork>().bala = true;
	}
	float nacerZ;
	[Command]
	public void CmdPanzerDisparoBueno()
	{
		if(_currentDirection == "right")
		{
			var humo = (GameObject)Instantiate(HumoPanzer, new Vector3(casquilloSpawn.position.x-3, casquilloSpawn.position.y-0.5f, casquilloSpawn.position.z), bulletSpawn.rotation);//Quaternion.Euler(0,0,casquilloSpawn.rotation.z)
			NetworkServer.Spawn(humo);

			var granade = (GameObject)Instantiate(bulletPrefPanzer, granadaSpawn.position, bulletSpawn.rotation);
			NetworkServer.Spawn(granade);
			granade.GetComponent<balaPanzerNetwork>().poder = saludMax*granade.GetComponent<balaPanzerNetwork>().poder/104;
		}else
		{
			var humo = (GameObject)Instantiate(HumoPanzer, new Vector3(casquilloSpawn.position.x+3, casquilloSpawn.position.y-0.5f, casquilloSpawn.position.z), bulletSpawn.rotation);
			NetworkServer.Spawn(humo);

			var granade = (GameObject)Instantiate(bulletPrefPanzer, granadaSpawn.position, bulletSpawn.rotation);
			NetworkServer.Spawn(granade);
			granade.GetComponent<balaPanzerNetwork>().poder = saludMax*granade.GetComponent<balaPanzerNetwork>().poder/104;
		}
	}
	[Command]
	public void CmdPanzerDisparoMalo()
	{
		if(_currentDirection == "right")
		{
			var humo = (GameObject)Instantiate(HumoPanzer, new Vector3(casquilloSpawn.position.x-3, casquilloSpawn.position.y-0.5f, casquilloSpawn.position.z), bulletSpawn.rotation);//Quaternion.Euler(0,0,casquilloSpawn.rotation.z)
			NetworkServer.Spawn(humo);

			var granade = (GameObject)Instantiate(bulletPrefPanzerMalo, granadaSpawn.position, bulletSpawn.rotation);
			NetworkServer.Spawn(granade);
			granade.GetComponent<balaPanzerNetwork>().poder = saludMax2*granade.GetComponent<balaPanzerNetwork>().poder/104;
		}else
		{
			var humo = (GameObject)Instantiate(HumoPanzer, new Vector3(casquilloSpawn.position.x+3, casquilloSpawn.position.y-0.5f, casquilloSpawn.position.z), bulletSpawn.rotation);
			NetworkServer.Spawn(humo);

			var granade = (GameObject)Instantiate(bulletPrefPanzerMalo, granadaSpawn.position, bulletSpawn.rotation);
			NetworkServer.Spawn(granade);
			granade.GetComponent<balaPanzerNetwork>().poder = saludMax2*granade.GetComponent<balaPanzerNetwork>().poder/104;
		}
	}

	[Command]
	public void Cmd_Fuego()
	{
		var fire = (GameObject)Instantiate(fuegoFin, new Vector3(bulletSpawn.position.x, bulletSpawn.position.y+2, bulletSpawn.position.z), bulletSpawn.rotation); 
		NetworkServer.Spawn(fire);
	}
	IEnumerator apaga ()
	{
		yield return new WaitForSeconds(0.1f);
		luz.SetActive(false);
	}
	//VOLUMEN

	void granada ()
	{
		if(!isLocalPlayer)
		{
			return;
		}
		granadas -= 1;
		animator.SetBool("granada", false);

		if(gameObject.tag == "Player")
		{
			CmdGranada();
		}else
		{
			CmdGranadaMalo();
		}
	}
	[Command]
	public void CmdGranada()
	{
		var granade = (GameObject)Instantiate(granadePref, granadaSpawn.position, granadaSpawn.rotation);
		NetworkServer.Spawn(granade);
		granade.GetComponent<granadeNetwork>().poder = saludMax*granade.GetComponent<granadeNetwork>().poder/104;
	}
	[Command]
	public void CmdGranadaMalo()
	{
		var granade = (GameObject)Instantiate(granadePref, granadaSpawn.position, granadaSpawn.rotation);
		NetworkServer.Spawn(granade);
		granade.GetComponent<granadeNetwork>().poder = saludMax2*granade.GetComponent<granadeNetwork>().poder/104;
	}
	//CASCOS
	[Command]
	public void CmdCasco1Bueno()
	{
		var sombrero = (GameObject)Instantiate(casco1, cabeza.transform.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(sombrero);
		sombrero.GetComponent<Rigidbody>().velocity = sombrero.transform.up * 20;
		sombrero.GetComponent<Rigidbody>().velocity = sombrero.transform.right * Random.Range(-10,11);
		sombrero.GetComponent<Rigidbody>().AddTorque(transform.forward * 500 * 500);
		Destroy(sombrero, 2f);
	}
	[Command]
	public void CmdCasco1Malo()
	{
		var sombrero = (GameObject)Instantiate(casco1M, cabeza.transform.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(sombrero);
		sombrero.GetComponent<Rigidbody>().velocity = sombrero.transform.up * 20;
		sombrero.GetComponent<Rigidbody>().velocity = sombrero.transform.right * Random.Range(-10,11);
		sombrero.GetComponent<Rigidbody>().AddTorque(transform.forward * 500 * 500);
		Destroy(sombrero, 2f);
	}

	[Command]
	public void CmdCasco2Bueno()
	{
		var sombrero = (GameObject)Instantiate(casco2, cabeza.transform.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(sombrero);
		sombrero.GetComponent<Rigidbody>().velocity = sombrero.transform.up * 20;
		sombrero.GetComponent<Rigidbody>().velocity = sombrero.transform.right * Random.Range(-10,11);
		sombrero.GetComponent<Rigidbody>().AddTorque(transform.forward * 500 * 500);
		Destroy(sombrero, 2f);
	}
	[Command]
	public void CmdCasco2Malo()
	{
		var sombrero = (GameObject)Instantiate(casco2M, cabeza.transform.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(sombrero);
		sombrero.GetComponent<Rigidbody>().AddForce(transform.up * 50);
		//sombrero.GetComponent<Rigidbody>().velocity = sombrero.transform.up * 50;
		sombrero.GetComponent<Rigidbody>().velocity = sombrero.transform.right * Random.Range(-20,21);
		sombrero.GetComponent<Rigidbody>().AddTorque(transform.forward * 500 * 500);
		Destroy(sombrero, 2f);
	}

	[Command]
	public void CmdCasco3Bueno()
	{
		var sombrero = (GameObject)Instantiate(casco3, cabeza.transform.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(sombrero);
		sombrero.GetComponent<Rigidbody>().velocity = sombrero.transform.up * 20;
		sombrero.GetComponent<Rigidbody>().velocity = sombrero.transform.right * Random.Range(-10,11);
		sombrero.GetComponent<Rigidbody>().AddTorque(transform.forward * 500 * 500);
		Destroy(sombrero, 2f);
	}
	[Command]
	public void CmdCasco3Malo()
	{
		var sombrero = (GameObject)Instantiate(casco3M, cabeza.transform.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(sombrero);
		sombrero.GetComponent<Rigidbody>().velocity = sombrero.transform.up * 20;
		sombrero.GetComponent<Rigidbody>().velocity = sombrero.transform.right * Random.Range(-10,11);
		sombrero.GetComponent<Rigidbody>().AddTorque(transform.forward * 500 * 500);
		Destroy(sombrero, 2f);
	}

	[Command]
	public void CmdCasco4Bueno()
	{
		var sombrero = (GameObject)Instantiate(casco4, cabeza.transform.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(sombrero);
		sombrero.GetComponent<Rigidbody>().velocity = sombrero.transform.up * 20;
		sombrero.GetComponent<Rigidbody>().velocity = sombrero.transform.right * Random.Range(-10,11);
		sombrero.GetComponent<Rigidbody>().AddTorque(transform.forward * 500 * 500);
		Destroy(sombrero, 2f);
	}
	[Command]
	public void CmdCasco4Malo()
	{
		var sombrero = (GameObject)Instantiate(casco4M, cabeza.transform.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(sombrero);
		sombrero.GetComponent<Rigidbody>().velocity = sombrero.transform.up * 20;
		sombrero.GetComponent<Rigidbody>().velocity = sombrero.transform.right * Random.Range(-10,11);
		sombrero.GetComponent<Rigidbody>().AddTorque(transform.forward * 500 * 500);
		Destroy(sombrero, 2f);
	}

	[Command]
	public void CmdCasco5Bueno()
	{
		var sombrero = (GameObject)Instantiate(casco5, cabeza.transform.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(sombrero);
		sombrero.GetComponent<Rigidbody>().velocity = sombrero.transform.up * 20;
		sombrero.GetComponent<Rigidbody>().velocity = sombrero.transform.right * Random.Range(-10,11);
		sombrero.GetComponent<Rigidbody>().AddTorque(transform.forward * 500 * 500);
		Destroy(sombrero, 2f);
	}
	[Command]
	public void CmdCasco5Malo()
	{
		var sombrero = (GameObject)Instantiate(casco5M, cabeza.transform.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(sombrero);
		sombrero.GetComponent<Rigidbody>().velocity = sombrero.transform.up * 20;
		sombrero.GetComponent<Rigidbody>().velocity = sombrero.transform.right * Random.Range(-10,11);
		sombrero.GetComponent<Rigidbody>().AddTorque(transform.forward * 500 * 500);
		Destroy(sombrero, 2f);
	}

	[Command]
	public void CmdCasco6Bueno()
	{
		var sombrero = (GameObject)Instantiate(casco6, cabeza.transform.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(sombrero);
		sombrero.GetComponent<Rigidbody>().velocity = sombrero.transform.up * 20;
		sombrero.GetComponent<Rigidbody>().velocity = sombrero.transform.right * Random.Range(-10,11);
		sombrero.GetComponent<Rigidbody>().AddTorque(transform.forward * 500 * 500);
		Destroy(sombrero, 2f);
	}
	[Command]
	public void CmdCasco6Malo()
	{
		var sombrero = (GameObject)Instantiate(casco6M, cabeza.transform.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(sombrero);
		sombrero.GetComponent<Rigidbody>().velocity = sombrero.transform.up * 20;
		sombrero.GetComponent<Rigidbody>().velocity = sombrero.transform.right * Random.Range(-10,11);
		sombrero.GetComponent<Rigidbody>().AddTorque(transform.forward * 500 * 500);
		Destroy(sombrero, 2f);
	}
}