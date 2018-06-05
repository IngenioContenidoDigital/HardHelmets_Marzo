using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Com.LuisPedroFonseca.ProCamera2D;
using UnityStandardAssets.ImageEffects;

public class Cam : MonoBehaviour {

	public GameObject Player;

	//public GameObject Ocultar;

	public Vector3 nextPosition;

	//SNIPER
	public bool sniper;
	//BOMBARDEO
	public bool bombardeo;
	public GameObject imagenBomba;
	public GameObject TexturaBombardeo;
	public bool bombardeocarga;
	public bool bombardeocarga2;
	public bool tirarbomba;
	public bool tirarbomba2;
	public bool tirarbomba3;

	public bool cancelar;

	public GameObject misiles;
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

	public bool disparo;
	public bool disparo2;

	public bool alejar;
	//public float velocidad;

	//public float equis;
	//public float yee;

	public GameObject vibrador;
	public float vib;
	public bool shake;
	public bool shakeAvion;

	//OBJETIVO
	public bool objetivo;
	//CAMPAMENTO
	public bool campamento;
	public Vector3 campPos;

	//PANEL PARTIDA
	GameObject Panel;
	bool ver;

	public bool mouse;

	//LIMITES DE CAMARA
	public int limiteObjetivo;
	public int limiteAleja;
	// Use this for initialization
	void Start ()
	{
		//Player = GameObject.Find("Hero");
		musica = PlayerPrefs.GetFloat("musica");
	}
	// Update is called once per frame
	void Update ()
	{
		if(sniper || bombardeo)
		{
			Cursor.visible = false;
		}else
		{
			Cursor.visible = true;
		}
		if(cancelar)
		{
			bombardeo = false;
			bombardeocarga2 = false;
			tirarbomba = false;
			tirarbomba2 = false;
			tirarbomba3 = false;
			TexturaBombardeo.SetActive(false);
			GetComponent<Grayscale>().enabled = false;

			Player.GetComponent<Hero>().ready = true;
			Player.GetComponent<Hero>().esconderBarra.SetActive(true);
			GetComponent<AudioSource>().Stop();
			alejar = false;

			cancelar = false;
		}
		if(Panel == null)
		{
			Panel = GameObject.Find("GAME");
		}else if(Panel.GetComponent<GameOffline>().muerte)
		{
			ver = true;
			if(transform.position.x-0.5f <= Panel.GetComponent<GameOffline>().posicion.x)
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

		if(sube)
		{
			loop = true;
			//ProCamera2DShake.Instance.Shake();
			shake = true;
			intensidad += 0.02f;
			GetComponent<BloomOptimized>().intensity = intensidad;
			if(intensidad >= 0.2f)//2
			{
				baja = true;
				sube = false;
			}
		}
		if(baja)
		{
			intensidad -= 0.02f;
			GetComponent<BloomOptimized>().intensity = intensidad;
			if(intensidad <= 0.01f)
			{
				GetComponent<BloomOptimized>().intensity = 0.01f;
				intensidad = 0.01f;
				loop = false;
				//StartCoroutine(explo());
				baja = false;
			}
		}
		if(disparo)
		{
			intensidad += 0.02f;
			GetComponent<BloomOptimized>().intensity = intensidad;
			if(intensidad >= 0.2f)//2
			{
				disparo2 = true;
				disparo = false;
			}
		}
		if(disparo2)
		{
			intensidad = 0.1f;
			GetComponent<BloomOptimized>().intensity = 0.01f;
			disparo2 = false;
		}

		if(sniper && !ver)
		{
			//Ocultar.SetActive(false);

			GetComponent<DirtyLensFlare>().enabled = true;
			//GetComponent<ProCamera2D>().enabled = false;
			//GetComponent<ProCamera2DForwardFocus>().enabled = false;
			//GetComponent<ProCamera2DSpeedBasedZoom>().enabled = false;

			GetComponent<Camera>().fieldOfView = 28;

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


			if(Player.GetComponent<Hero>()._currentDirection == "right")//LIMITES HACIA ADELANTE
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
			if(Player.GetComponent<Hero>()._currentDirection == "left")//LIMITES HACIA ATRAS
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
			if(transform.position.y <= Player.transform.position.y+5)//+1
			{
				nextPosition = new Vector3(transform.position.x, transform.position.y+0.2f, transform.position.z);
			}else if(transform.position.y >= Player.transform.position.y+18)//+20
			{
				nextPosition = new Vector3(transform.position.x, transform.position.y-0.2f, transform.position.z);
			}

			transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * 20);
		}else if(bombardeo && !ver)
		{
			alejar = true;
			TexturaBombardeo.SetActive(true);

			GetComponent<DirtyLensFlare>().enabled = true;

			GetComponent<Camera>().fieldOfView = 28;

			if(Input.GetAxis("MIRA") != 0 || Input.GetAxis("MIRA H") != 0)
			{
				mouse = false;
			}

			if(Input.GetAxis("Mouse Y") != 0 || Input.GetAxis("Mouse X") != 0)
			{
				mouse = true;
			}

			if(Input.GetAxis("DISPARO") > 0)
			{
				bombardeocarga2 = false;
				bombardeocarga = true;
			}
			if(Input.GetButtonDown("DISPARO 2"))
			{
				bombardeocarga = false;
				bombardeocarga2 = true;
			}

			if(bombardeocarga && Input.GetAxis("DISPARO") == 0)
			{
				bombardeocarga = false;
			}
			if(Input.GetButtonUp("DISPARO 2"))
			{
				bombardeocarga2 = false;
			}

			if(bombardeocarga && !tirarbomba2 || bombardeocarga2 && !tirarbomba2)
			{
				if(!GetComponent<AudioSource>().isPlaying)
				{
					GetComponent<AudioSource>().Play();
				}
				h = 0;
				v = 0;
				imagenBomba.GetComponent<UnityEngine.UI.Image>().fillAmount += 0.006f;
			}else
			{
				GetComponent<AudioSource>().Stop();
				imagenBomba.GetComponent<UnityEngine.UI.Image>().fillAmount = 0;
			}

			if(imagenBomba.GetComponent<UnityEngine.UI.Image>().fillAmount >= 1)
			{
				tirarbomba = true;
			}

			if(tirarbomba && !tirarbomba2)
			{
				imagenBomba.GetComponent<UnityEngine.UI.Image>().fillAmount = 0;

				var bullet = (GameObject)Instantiate(misiles, new Vector3(transform.position.x, Player.transform.position.y+10, Player.transform.position.z), Quaternion.Euler(0,0,0)); 
				bullet.GetComponent<Poder>().poder = Player.GetComponent<Hero>().saludMax*bullet.GetComponent<Poder>().poder/104;

				tirarbomba2 = true;
			}

			if(tirarbomba2)
			{
				if(Input.GetAxis("DISPARO") == 0)
				{
					tirarbomba3 = true;
				}
				if(Input.GetAxis("DISPARO") > 0 && tirarbomba3)
				{
					bombardeo = false;
					bombardeocarga2 = false;
					tirarbomba = false;
					tirarbomba2 = false;
					tirarbomba3 = false;
					TexturaBombardeo.SetActive(false);
					GetComponent<Grayscale>().enabled = false;

					Player.GetComponent<Hero>().pausado = false;
					Player.GetComponent<Hero>().ready = true;
					Player.GetComponent<Hero>().esconderBarra.SetActive(true);
					GetComponent<AudioSource>().Stop();
					alejar = false;
				}
				if(Input.GetButtonDown("DISPARO 2"))
				{
					bombardeo = false;
					bombardeocarga2 = false;
					tirarbomba = false;
					tirarbomba2 = false;
					tirarbomba3 = false;
					TexturaBombardeo.SetActive(false);
					GetComponent<Grayscale>().enabled = false;

					Player.GetComponent<Hero>().pausado = false;
					Player.GetComponent<Hero>().ready = true;
					Player.GetComponent<Hero>().esconderBarra.SetActive(true);
					GetComponent<AudioSource>().Stop();
					alejar = false;
				}
			}

			if(!bombardeocarga && !bombardeocarga2)
			{
				if(mouse)
				{
					h = horizontal * Input.GetAxis("Mouse X");
					v = vertical * Input.GetAxis("Mouse Y");
				}else
				{
					h = horizontal * Input.GetAxis("MIRA H");
					v = vertical * Input.GetAxis("MIRA");
				}
			}


			if(Player.GetComponent<Hero>()._currentDirection == "right")//LIMITES HACIA ADELANTE
			{
				if(transform.position.x >= Player.transform.position.x+180)
				{
					nextPosition = new Vector3(transform.position.x-0.5f, transform.position.y+v, transform.position.z);//velocidad);
				}else if(transform.position.x <= Player.transform.position.x+9)
				{
					nextPosition = new Vector3(transform.position.x+2f, transform.position.y+v, transform.position.z);//velocidad);
				}else
				{
					nextPosition = new Vector3(transform.position.x+h, transform.position.y+v, transform.position.z);//velocidad);
				}
			}
			if(Player.GetComponent<Hero>()._currentDirection == "left")//LIMITES HACIA ATRAS
			{
				if(transform.position.x <= Player.transform.position.x-180)
				{
					nextPosition = new Vector3(transform.position.x+0.5f, transform.position.y+v, transform.position.z);//velocidad);
				}else if(transform.position.x >= Player.transform.position.x-9)
				{
					nextPosition = new Vector3(transform.position.x-2f, transform.position.y+v, transform.position.z);//velocidad);
				}else
				{
					nextPosition = new Vector3(transform.position.x+h, transform.position.y+v, transform.position.z);//velocidad);
				}
			}
			//LIMITES HACIA ARRIBA Y ABAJO
			if(transform.position.y <= Player.transform.position.y+5)//1
			{
				nextPosition = new Vector3(transform.position.x, transform.position.y+0.2f, transform.position.z);//velocidad);
			}else if(transform.position.y >= Player.transform.position.y+18)//20
			{
				nextPosition = new Vector3(transform.position.x, transform.position.y-0.2f, transform.position.z);//velocidad);
			}

			transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * 20);
		}else if(campamento && !ver)
		{
			nextPosition = new Vector3(campPos.x, campPos.y, campPos.z);//campPos.y+5
			transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * 50);
		}else if(!ver)
		{
			campPos = new Vector3(transform.position.x, transform.position.y, transform.position.z-5);
			GetComponent<DirtyLensFlare>().enabled = false;

			if(objetivo)
			{
				altura = 10.8f;

				GetComponent<Camera>().fieldOfView += 0.15f;
				if(GetComponent<Camera>().fieldOfView  >= 25)
				{
					GetComponent<Camera>().fieldOfView = 25;
				}

			}else
			{
				altura = 8.5f;
				GetComponent<Camera>().fieldOfView -= 0.15f;
				if(GetComponent<Camera>().fieldOfView  <= 15.5f)
				{
					GetComponent<Camera>().fieldOfView = 15.5f;
				}
			}

			if(Player.transform.position.z > -30)
			{
				ajuste = -90.5f;
			}else
			{
				ajuste = -98.5f;
			}
			//velocidad = -40;
			//SIGUE AL JUGADOR
			if(Player.GetComponent<Hero>()._currentDirection == "right")//-28.5 -20.5
			{
				nextPosition = new Vector3(Player.transform.position.x+4, altura, ajuste);//Player.transform.position.x+equis, Player.transform.position.y+8, velocidad
			}else
			{
				nextPosition = new Vector3(Player.transform.position.x-4, altura, ajuste);//Player.transform.position.x-equis, Player.transform.position.y+8, velocidad
			}

			transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * 2);

		}else if(ver)
		{
			nextPosition = Panel.GetComponent<GameOffline>().posicion;
			transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * 2);
		}
		if(!bombardeo)
		{
			imagenBomba.GetComponent<UnityEngine.UI.Image>().fillAmount = 0;
			imagenBomba.SetActive(true);
		}
		/*if(transform.position.z+50 >= Player.transform.position.z)
		{
			equis -= 0.3f;
			if(equis <= 12)//12
			{
				print("CERCA");
				equis = 12;//12
			}
		}*/

		//POSICION DE LA CAMARA EN Z
		if(alejar)
		{
			/*equis += 0.3f;
			if(equis >= 20)
			{
				equis = 20;
			}

			velocidad -= 0.3f;*/
			/*if(transform.position.z <= limiteAleja)//LIMITE EN Z
			{
				velocidad = -200;
			}else if(velocidad <= Player.transform.position.z-70)
			{
				velocidad = Player.transform.position.z-70;
			}*/
			/*GetComponent<UnityStandardAssets.CinematicEffects.DepthOfField>().focus.focusPlane += 0.3f;
			if(GetComponent<UnityStandardAssets.CinematicEffects.DepthOfField>().focus.focusPlane >= 49.3f)
			{
				GetComponent<UnityStandardAssets.CinematicEffects.DepthOfField>().focus.focusPlane = 49.3f;
			}*/
		}else
		{
			/*equis -= 0.2f;
			if(equis <= 6)//10
			{
				equis = 6;//10
			}

			velocidad += 0.2f;*/
			/*if(velocidad >= Player.transform.position.z-40)
			{
				velocidad = Player.transform.position.z-40;
			}*/

			/*GetComponent<UnityStandardAssets.CinematicEffects.DepthOfField>().focus.focusPlane -= 0.3f;
			if(GetComponent<UnityStandardAssets.CinematicEffects.DepthOfField>().focus.focusPlane <= 34.5f)
			{
				GetComponent<UnityStandardAssets.CinematicEffects.DepthOfField>().focus.focusPlane = 34.5f;
			}*/
		}

		//VIBRACION
		if(shake)
		{
			vibrador.transform.position = new Vector3(vibrador.transform.position.x+Random.Range(-vib,vib), vibrador.transform.position.y+Random.Range(-vib,vib), vibrador.transform.position.z);
			StartCoroutine(tiempo());
		}
		if(shakeAvion)
		{
			vibrador.transform.position = new Vector3(vibrador.transform.position.x+Random.Range(-vib,vib), vibrador.transform.position.y+Random.Range(-vib,vib), vibrador.transform.position.z);
			StartCoroutine(tiempoAvion());
		}
	}
	public float ajuste;
	public float altura;
	IEnumerator finalizar()
	{
		yield return new WaitForSeconds(2f);
		Panel.GetComponent<GameOffline>().explotar = true;
	}
	IEnumerator tiempo()
	{
		yield return new WaitForSeconds(0.1f);
		shake = false;
	}
	IEnumerator tiempoAvion()
	{
		yield return new WaitForSeconds(0.5f);
		shakeAvion = false;
	}
}

