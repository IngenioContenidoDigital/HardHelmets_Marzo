﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Com.LuisPedroFonseca.ProCamera2D;
using UnityStandardAssets.ImageEffects;
using UnityEngine.Networking;

public class CamNetwork : NetworkBehaviour {
	
	public GameObject Player;

	//public GameObject Ocultar;

	public Vector3 nextPosition;

	//SNIPER
	public bool sniper;
	//public GameObject balaSniper;
	Ray ray;
	RaycastHit hit;
	float horizontal = 2;
	float vertical = 2;
	public float h;
	public float v;


	//SONIDOS DE HAMBIENTE
	public static bool Area2;
	public static bool Area3;

	float musica;

	public GameObject[] Explo;
	public static bool visible;
	public bool sube;
	bool baja;
	bool loop;
	float intensidad;
	public float maximo;

	public bool disparo;
	public bool disparo2;

	public bool alejar;
	public float velocidad;

	public float equis;
	public float yee;

	public GameObject vibrador;
	public float vib;
	public bool shake;

	//OBJETIVO
	public bool objetivo;
	//CAMPAMENTO
	public bool campamento;
	public Vector3 campPos;

	//IMAGENES DE FONDO
	public GameObject Imagen;

	public Sprite Cero;
	public Sprite Uno;
	public Sprite Dos;
	public Sprite Tres;

	public bool PonerImagen;

	//PANEL PARTIDA
	GameObject Panel;
	bool ver;

	public bool mouse;
	// Use this for initialization
	void Start ()
	{
		if(!PonerImagen)
		{
			if(Application.loadedLevelName == "LevelNetwork0")
			{
				Imagen.GetComponent<SpriteRenderer>().sprite = Cero;
			}else if(Application.loadedLevelName == "LevelNetwork1")
			{
				Imagen.GetComponent<SpriteRenderer>().sprite = Uno;
			}else if(Application.loadedLevelName == "LevelNetwork2")
			{
				Imagen.GetComponent<SpriteRenderer>().sprite = Dos;
			}else if(Application.loadedLevelName == "LevelNetwork3")
			{
				Imagen.GetComponent<SpriteRenderer>().sprite = Tres;
			}
			PonerImagen = true;
		}
		//Player = GameObject.Find("Hero");
		musica = PlayerPrefs.GetFloat("musica");
		intensidad = 0.1f;
		GetComponent<Bloom>().bloomThreshold = 0.62f;
	}
	// Update is called once per frame
	void Update ()
	{
		if(!Player.GetComponent<NetworkIdentity>().isLocalPlayer)
		{
			return;
		}

		if(Panel == null)
		{
			Panel = GameObject.Find("GAME");
		}
		if(Panel != null && Panel.GetComponent<Game>().muerte)
		{
			ver = true;
			if(transform.position.x-0.5f <= Panel.GetComponent<Game>().posicion.x)
			{
				StartCoroutine(finalizar());
			}
		}

		if(Player == null)
		{
			Player = GameObject.Find("Hero");
		}
		//Player.GetComponent<Hero>().SniperCam = gameObject;
		Explo = GameObject.FindGameObjectsWithTag("explo");

		foreach(GameObject explo in Explo)
		{
			if(explo.GetComponent<Renderer>().isVisible && visible && !sube && !loop)
			{
				sube = true;
			}
		}

		GetComponent<Bloom>().bloomIntensity = intensidad;

		if(sube)
		{
			loop = true;
			//ProCamera2DShake.Instance.Shake();
			shake = true;
			intensidad += 0.7f;
			GetComponent<Bloom>().bloomThreshold = -0.05f;
			if(intensidad >= 2)
			{
				baja = true;
				sube = false;
			}
		}
		if(baja)
		{
			intensidad -= 0.5f;
			if(intensidad <= 0.1f)
			{
				GetComponent<Bloom>().bloomThreshold = 0.62f;
				intensidad = 0.1f;
				loop = false;
				//StartCoroutine(explo());
				baja = false;
			}
		}
		if(disparo)
		{
			intensidad += 0.15f;
			GetComponent<Bloom>().bloomThreshold = -0.05f;
			if(intensidad >= maximo)
			{
				disparo2 = true;
				disparo = false;
			}
		}
		if(disparo2)
		{
			intensidad = 0.1f;
			GetComponent<Bloom>().bloomThreshold = 0.62f;
			disparo2 = false;
		}

		if(sniper && !ver)
		{
			//Ocultar.SetActive(false);

			GetComponent<DirtyLensFlare>().enabled = true;
			//GetComponent<ProCamera2D>().enabled = false;
			//GetComponent<ProCamera2DForwardFocus>().enabled = false;
			//GetComponent<ProCamera2DSpeedBasedZoom>().enabled = false;

			GetComponent<Camera>().fieldOfView = 20.5f;

			if(Input.GetAxis("MIRA") != 0 || Input.GetAxis("MIRA H") != 0)
			{
				mouse = false;
			}

			if(Input.GetAxis("Mouse Y") != 0 || Input.GetAxis("Mouse X") != 0)
			{
				mouse = true;
			}

			if(mouse)
			{
				h = horizontal * Input.GetAxis("Mouse X");
				v = vertical * Input.GetAxis("Mouse Y");
			}else
			{
				h = horizontal * Input.GetAxis("MIRA H");
				v = vertical * Input.GetAxis("MIRA");
			}

			if(Player.GetComponent<HeroNetwork>()._currentDirection == "right")//LIMITES HACIA ADELANTE
			{
				if(transform.position.x >= Player.transform.position.x+180)
				{
					nextPosition = new Vector3(transform.position.x-0.5f, transform.position.y+v, transform.position.z);//zeta);
				}else if(transform.position.x <= Player.transform.position.x+9)
				{
					nextPosition = new Vector3(transform.position.x+2f, transform.position.y+v, transform.position.z);//zeta);
				}else
				{
					nextPosition = new Vector3(transform.position.x+h, transform.position.y+v, transform.position.z);//zeta);
				}
			}
			if(Player.GetComponent<HeroNetwork>()._currentDirection == "left")//LIMITES HACIA ATRAS
			{
				if(transform.position.x <= Player.transform.position.x-180)
				{
					nextPosition = new Vector3(transform.position.x+0.5f, transform.position.y+v, transform.position.z);//zeta);
				}else if(transform.position.x >= Player.transform.position.x-9)
				{
					nextPosition = new Vector3(transform.position.x-2f, transform.position.y+v, transform.position.z);//zeta);
				}else
				{
					nextPosition = new Vector3(transform.position.x+h, transform.position.y+v, transform.position.z);//zeta);
				}
			}
			//LIMITES HACIA ARRIBA Y ABAJO
			if(transform.position.y <= Player.transform.position.y+1)
			{
				nextPosition = new Vector3(transform.position.x, transform.position.y+0.2f, transform.position.z);
			}else if(transform.position.y >= Player.transform.position.y+20)
			{
				nextPosition = new Vector3(transform.position.x, transform.position.y-0.2f, transform.position.z);
			}

			transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * 20);
		}else if(campamento && !ver)
		{
			nextPosition = new Vector3(campPos.x, campPos.y, campPos.z);//campPos.y+5, velocidad
			transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * 50);
		}else if(!ver)
		{
			campPos = new Vector3(transform.position.x, transform.position.y, transform.position.z-5);
			GetComponent<DirtyLensFlare>().enabled = false;

			if(objetivo)
			{
				velocidad = -220;
				if(Player.GetComponent<HeroNetwork>()._currentDirection == "right")
				{
					nextPosition = new Vector3(Player.transform.position.x+20, Player.transform.position.y+10, velocidad);
				}else
				{
					nextPosition = new Vector3(Player.transform.position.x-20, Player.transform.position.y+10, velocidad);
				}

				transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * 2);
			}else
			{
				//SIGUE AL JUGADOR
				if(Player.GetComponent<HeroNetwork>()._currentDirection == "right")
				{
					nextPosition = new Vector3(Player.transform.position.x+equis, Player.transform.position.y+5, velocidad);//-170-velocidad);
				}else
				{
					nextPosition = new Vector3(Player.transform.position.x-equis, Player.transform.position.y+5, velocidad);
				}

				transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * 2);
			}
		}else if(ver)
		{
			nextPosition = Panel.GetComponent<Game>().posicion;
			transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * 2);
		}

		if(transform.position.z+50 >= Player.transform.position.z)
		{
			equis -= 0.3f;
			if(equis <= 12)
			{
				equis = 12;
			}
		}

		//POSICION DE LA CAMARA EN Z
		if(alejar)
		{
			equis += 0.3f;
			if(equis >= 20)
			{
				equis = 20;
			}

			velocidad -= 0.3f;
			if(transform.position.z <= -200)//LIMITE EN Z
			{
				velocidad = -200;
			}else if(velocidad <= Player.transform.position.z-70)
			{
				velocidad = Player.transform.position.z-70;
			}
		}else
		{
			equis -= 0.2f;
			if(equis <= 10)
			{
				equis = 10;
			}

			velocidad += 0.2f;
			if(velocidad >= Player.transform.position.z-55)
			{
				velocidad = Player.transform.position.z-55;
			}
		}

		//VIBRACION
		if(shake)
		{
			vibrador.transform.position = new Vector3(vibrador.transform.position.x+Random.Range(-vib,vib), vibrador.transform.position.y+Random.Range(-vib,vib), vibrador.transform.position.z);
			StartCoroutine(tiempo());
		}
	}
	IEnumerator finalizar()
	{
		yield return new WaitForSeconds(2f);
		Panel.GetComponent<Game>().explotar = true;
	}
	IEnumerator tiempo()
	{
		yield return new WaitForSeconds(0.1f);
		shake = false;
	}
}

