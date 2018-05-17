using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AIVikingoNetwork : NetworkBehaviour {

	public Animator animator;

	//GROUND CHECHER
	public Transform groundCheck;
	float groundRadius = 0.3f;
	public LayerMask whatIsGround;
	public bool grounded = false;

	public Transform Player;
	public Transform Base;
	public Transform target;

	public string BaseBuena;
	public string BaseMala;
	public string BuscarBase;
	public string NameEnemy;
	public string NameEnemyTank;

	[SyncVar]
	public string _currentDirection = "right";

	[SyncVar(hook = "FacingCallback")]
	public float voltear;

	[SyncVar(hook = "noVivo")]
	public bool vivo = true;

	[SyncVar]
	public float salud;
	[SyncVar]
	public float saludMax;


	//ACCIONES DE PERSONAJE
	public int distancia;
	Vector3 v3;
	public bool actuando;
	public bool caminar;
	bool disparar;
	bool atras;
	bool cascar;

	int momento;

	public GameObject textos;
	public GameObject textos2;
	public GameObject mira;


	public GameObject bulletPref;
	public GameObject bulletPrefMalo;
	public Transform bulletSpawn;

	public GameObject casquilloPref;
	public Transform casquilloSpawn;

	public bool water;

	public GameObject luz;

	public bool crearCarta;
	bool acuchillado;

	public Transform cascadoSpawn;
	public GameObject[] efectoSanre;
	public GameObject[] sangreCuchillo;

	public GameObject carta;

	//VISION PARA ESQUIVAR OBSTACULOS
	public GameObject seguir;

	public GameObject BaseUno;

	//PANEL PARTIDA
	GameObject Panel;

	//CURACION
	public string medic;

	//NIVEL DE CARTA
	public int level;
	public GameObject Jugador;

	// Use this for initialization
	void Start ()
	{
		if(!isServer)
		{
			return;
		}

		if(gameObject.tag == "Player")
		{
			Jugador = GameObject.Find("Hero");
		}else
		{
			Jugador = GameObject.Find("Hero2");
		}

		level = Jugador.GetComponent<HeroNetwork>().level;

		saludMax = 8*level+200;
		salud = saludMax;

		distancia = Random.Range(20,35);
	}
	// Update is called once per frame
	void Update ()
	{
		if(!isServer)
		{
			return;
		}

		if(Panel == null)
		{
			Panel = GameObject.Find("GAME");
		}
		if(Panel.GetComponent<Game>().final)
		{
			vivo = false;
			actuando = false;

			caminar = false;
			disparar = false;
			atras = false;
			cascar = false;
			animator.SetBool("disparar", false);
			animator.SetBool("atras", false);
			animator.SetBool("walk", false);
			animator.SetBool("grounded", true);
			if(Panel.GetComponent<Game>().continuar)
			{
				print("DESTRUIR OBJETOS");
				CmdDestruir();
			}
		}
		if(salud >= saludMax)
		{
			salud = saludMax;
		}

		//CHECA SI ESTA EN EL PISO
		grounded = Physics.CheckSphere(groundCheck.position, groundRadius, whatIsGround);
		animator.SetBool("grounded", grounded);

		//VOLTEA PERSONAJE
		gameObject.transform.localScale = new Vector3(voltear,1.13f,1.13f);


		if(Base == null)
		{
			Base = GameObject.FindWithTag(BuscarBase).transform;
		}

		if(baseNeutra != null && baseNeutra.tag == BaseBuena)
		{
			actuando = false;
		}

		if(Player != null)
		{
			target = Player.transform;
		}else
		{
			target = Base.transform;
		}

		if(vivo)
		{
			if(grounded)
			{
				if(Mathf.Abs((transform.position - target.position).x) <= distancia && !actuando)
				{
					//momento = Random.Range(1,4);
					//StartCoroutine(esperaDisparo());
					disparar = true;
					actuando = true;
				}else if(!actuando && !atras)
				{
					caminar = true;
					actuando = true;
				}

				//ACCIONES DEL PERSONAJE
				//MIRA AL OBJKETIVO
				Vector3 v3Dir = target.position - transform.position;
				float angle = Mathf.Atan2(0, v3Dir.x) * Mathf.Rad2Deg;

				if(angle == 180 && _currentDirection == "right")
				{
					atras = true;
					//voltear = -1.13f;
				}else if(angle == 0 && _currentDirection == "left")
				{
					atras = true;
					//voltear = 1.13f;
				}
				print(angle);

				if(caminar)
				{
					disparar = false;
					atras = false;

					animator.SetBool("caminar",true);

					if(angle == 0 && _currentDirection == "right")
					{
						if(Mathf.Abs((transform.position - target.position).x) >= distancia)
						{
							v3 += Vector3.right;
						}
					}else if(angle == 180 && _currentDirection == "left")
					{
						if(Mathf.Abs((transform.position - target.position).x) >= distancia)
						{
							v3 += Vector3.left;
						}
					}else if(!seguir.GetComponent<vista>().arriba && !seguir.GetComponent<vista>().abajo)//AGREGADO
					{
						v3 = Vector3.zero;
					}else
					{
						animator.SetBool("caminar",false);
						atras = true;
					}

					if(seguir.GetComponent<vista>().arriba)
					{
						animator.SetBool("walk", true);
						v3 += Vector3.forward;
					}else if(seguir.GetComponent<vista>().abajo)
					{
						animator.SetBool("walk", true);
						v3 += Vector3.back;
					}

					if(target.transform.position.z > transform.position.z)
					{
						v3 += Vector3.forward;
					}else if (target.transform.position.z < transform.position.z)
					{
						v3 += Vector3.back;
					}else if(!seguir.GetComponent<vista>().arriba && !seguir.GetComponent<vista>().abajo)//AGREGADO
					{
						v3 = Vector3.zero;
					}

					if(Mathf.Abs((transform.position - target.position).x) <= distancia && Mathf.Abs((transform.position - target.position).z) <= 5)
					{
						actuando = false;
						animator.SetBool("caminar",false);
						caminar = false;
						v3 = Vector3.zero;
					}
				}

				if(disparar)
				{
					if(target != Base)
					{
						v3 = Vector3.zero;

						animator.SetBool("caminar", false);

						if(Mathf.Abs((transform.position - target.position).z) <= 8)//5
						{
							if(Mathf.Abs((transform.position - target.position).x) <= 7)//14
							{
								print("BUSQUE PARA CASCAR");
								cascar = true;
								caminar = false;
								disparar = false;
							}else
							{
								print("DISPARANDO AL ENEMIGO");
								distancia = Random.Range(20,35);
								caminar = false;
								atras = false;
								animator.SetBool("disparar",true);

								v3 = Vector3.zero;

								disparar = false;
							}

						}else
						{
							caminar = true;
							disparar = false;
						}
					}else
					{
						distancia = Random.Range(20,35);
						caminar = false;
						atras = false;
						animator.SetBool("disparar",true);

						v3 = Vector3.zero;

						disparar = false;
					}
				}
				if(atras)
				{
					animator.SetBool("caminar", false);

					caminar = false;
					disparar = false;
					animator.SetBool("atras",true);

					if(_currentDirection == "right")
					{
						print("IZQUIERDA");
						v3 += Vector3.left;
					}else
					{
						print("DERECHA");
						v3 += Vector3.right;
					}
				}
				if(cascar)
				{
					v3 = Vector3.zero;

					caminar = false;
					disparar = false;

					animator.SetBool("caminar",true);

					if(angle == 0 && _currentDirection == "right")
					{
						v3 += Vector3.right;
					}else if(angle == 180 && _currentDirection == "left")
					{
						v3 += Vector3.left;
					}else
					{
						animator.SetBool("caminar",false);
						atras = true;
					}

					if(target.transform.position.z > transform.position.z)
					{
						v3 += Vector3.forward;
					}else if (target.transform.position.z < transform.position.z)
					{
						v3 += Vector3.back;
					}
				}
			}

			if(v3 != Vector3.zero)
			{
				GetComponent<Rigidbody>().velocity = (5 * v3.normalized);
			}

		}else
		{
			animator.SetBool("muerto", true);

			mira.SetActive(false);

			gameObject.layer = LayerMask.NameToLayer("muerto");
			BaseUno.layer = LayerMask.NameToLayer("mira");
			//gameObject.tag = "Untagged";

			StartCoroutine(muertee());
		}

		if(salud <= 0)
		{
			vivo = false;
			v3 = Vector3.zero;
			animator.SetBool("muerto", true);

			gameObject.layer = LayerMask.NameToLayer("muerto");
			BaseUno.layer = LayerMask.NameToLayer("mira");
		}
	}

	IEnumerator muertee ()
	{
		yield return new WaitForSeconds(8f);
		Destroy(gameObject);
	}

	//APAGA LA LUZ
	IEnumerator apaga ()
	{
		yield return new WaitForSeconds(0.1f);
		luz.SetActive(false);
	}
	IEnumerator esperaDisparo()
	{
		yield return new WaitForSeconds(momento);
		disparar = true;
	}
	void vuelta ()
	{
		if(_currentDirection == "right")
		{
			_currentDirection = "left";
			voltear = -1.13f;
		}else
		{
			_currentDirection = "right";
			voltear = 1.13f;
		}
		atras = false;
	}

	void noVivo(bool vivo)
	{
		vivo = false;
	}

	void FacingCallback(float voltear)
	{
		if (voltear == 1.13f)
		{
			_currentDirection = "right";
			gameObject.transform.localScale = new Vector3(1.13f,1.13f,1.13f);
		}else
		{
			_currentDirection = "left";
			gameObject.transform.localScale = new Vector3(-1.13f,1.13f,1.13f);
		}
	}
	IEnumerator esperaGolpe ()
	{
		float moment = Random.Range(0,0.2f);
		yield return new WaitForSeconds(moment);
		animator.SetBool("golpe", true);
	}
	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "Water")
		{
			water = true;
		}
		if(col.gameObject.tag == "Piso")
		{
			water = false;
		}

		if(col.gameObject.tag == NameEnemy || col.gameObject.tag == NameEnemyTank)
		{
			v3 = Vector3.zero;
			StartCoroutine(esperaGolpe());
			actuando = false;
			atras = false;
			caminar = false;
			disparar = false;
			cascar = false;
		}

		if(col.gameObject.tag == "cuchillo" && vivo)
		{
			actuando = false;

			v3 = Vector3.zero;

			animator.SetBool("atras",false);
			animator.SetBool("caminar",false);
			animator.SetBool("disparar",false);
			atras = false;
			caminar = false;
			disparar = false;
			cascar = false;

			animator.SetBool("cascado", true);

			acuchillado = true;

			if(PlayerPrefs.GetInt("violencia") == 1)
			{
				var sangre2 = (GameObject)Instantiate(sangreCuchillo[Random.Range(0,sangreCuchillo.Length)], cascadoSpawn.position, cascadoSpawn.rotation); 
			}

			salud -= 50;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = "50";

			crearCarta = true;
			StartCoroutine(esperaCarta());
		}
		if(col.gameObject.tag == "bala" && vivo)
		{
			actuando = false;

			v3 = Vector3.zero;

			animator.SetBool("atras",false);
			animator.SetBool("caminar",false);
			animator.SetBool("disparar",false);
			atras = false;
			caminar = false;
			disparar = false;
			cascar = false;

			animator.SetBool("cascado", true);

			if(col.gameObject.GetComponent<bala>())
			{
				salud -= col.gameObject.GetComponent<bala>().poder;

				var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
				letras.GetComponent<TextMesh>().text = col.gameObject.GetComponent<bala>().poder.ToString("F0");
			}else
			{
				salud -= col.gameObject.GetComponent<balaSniper>().poder;

				var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
				letras.GetComponent<TextMesh>().text = col.gameObject.GetComponent<balaSniper>().poder.ToString("F0");
			}

			Destroy(col.gameObject);

			if(PlayerPrefs.GetInt("violencia") == 1)
			{
				var explo = (GameObject)Instantiate(efectoSanre[Random.Range(0,efectoSanre.Length)], new Vector3(col.gameObject.transform.position.x,col.gameObject.transform.position.y-3, col.gameObject.transform.position.z-1), cascadoSpawn.rotation);
			}
		}
		/*if(col.gameObject.tag == "balaFusil" && vivo)
		{
			actuando = false;

			v3 = Vector3.zero;

			animator.SetBool("atras",false);
			animator.SetBool("caminar",false);
			animator.SetBool("disparar",false);
			atras = false;
			caminar = false;
			disparar = false;
			cascar = false;

			animator.SetBool("cascado", true);
			Destroy(col.gameObject);
			salud -= 40;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = "40";

			if(PlayerPrefs.GetInt("violencia") == 1)
			{
				var explo = (GameObject)Instantiate(efectoSanre[Random.Range(0,efectoSanre.Length)], new Vector3(col.gameObject.transform.position.x,col.gameObject.transform.position.y-3, col.gameObject.transform.position.z-1), cascadoSpawn.rotation);
			}
		}
		if(col.gameObject.tag == "balaEscopeta" && vivo)
		{
			actuando = false;

			v3 = Vector3.zero;

			animator.SetBool("atras",false);
			animator.SetBool("caminar",false);
			animator.SetBool("disparar",false);
			atras = false;
			caminar = false;
			disparar = false;
			cascar = false;

			animator.SetBool("cascado", true);
			Destroy(col.gameObject);
			salud -= 15;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = "15";

			if(PlayerPrefs.GetInt("violencia") == 1)
			{
				var explo = (GameObject)Instantiate(efectoSanre[Random.Range(0,efectoSanre.Length)], new Vector3(col.gameObject.transform.position.x,col.gameObject.transform.position.y-3, col.gameObject.transform.position.z-1), cascadoSpawn.rotation);
			}
		}
		if(col.gameObject.tag == "balaSubmetra" && vivo)
		{
			actuando = false;

			v3 = Vector3.zero;

			animator.SetBool("atras",false);
			animator.SetBool("caminar",false);
			animator.SetBool("disparar",false);
			atras = false;
			caminar = false;
			disparar = false;
			cascar = false;

			animator.SetBool("cascado", true);
			Destroy(col.gameObject);
			salud -= 20;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = "20";

			if(PlayerPrefs.GetInt("violencia") == 1)
			{
				var explo = (GameObject)Instantiate(efectoSanre[Random.Range(0,efectoSanre.Length)], new Vector3(col.gameObject.transform.position.x,col.gameObject.transform.position.y-3, col.gameObject.transform.position.z-1), cascadoSpawn.rotation);
			}
		}
		if(col.gameObject.tag == "balaMetra" && vivo)
		{
			actuando = false;

			v3 = Vector3.zero;

			animator.SetBool("atras",false);
			animator.SetBool("caminar",false);
			animator.SetBool("disparar",false);
			atras = false;
			caminar = false;
			disparar = false;
			cascar = false;

			animator.SetBool("cascado", true);
			Destroy(col.gameObject);
			salud -= 25;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = "25";

			if(PlayerPrefs.GetInt("violencia") == 1)
			{
				var explo = (GameObject)Instantiate(efectoSanre[Random.Range(0,efectoSanre.Length)], new Vector3(col.gameObject.transform.position.x,col.gameObject.transform.position.y-3, col.gameObject.transform.position.z-1), cascadoSpawn.rotation);
			}
		}
		if(col.gameObject.tag == "balaMG" && vivo)
		{
			actuando = false;

			v3 = Vector3.zero;

			animator.SetBool("atras",false);
			animator.SetBool("caminar",false);
			animator.SetBool("disparar",false);
			atras = false;
			caminar = false;
			disparar = false;
			cascar = false;

			animator.SetBool("cascado", true);
			Destroy(col.gameObject);
			salud -= 25;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = "25";

			if(PlayerPrefs.GetInt("violencia") == 1)
			{
				var explo = (GameObject)Instantiate(efectoSanre[Random.Range(0,efectoSanre.Length)], new Vector3(col.gameObject.transform.position.x,col.gameObject.transform.position.y-3, col.gameObject.transform.position.z-1), cascadoSpawn.rotation);
			}
		}*/
		/*if(col.gameObject.tag == "balaSniper" && vivo)
		{
			actuando = false;

			v3 = Vector3.zero;

			animator.SetBool("atras",false);
			animator.SetBool("caminar",false);
			animator.SetBool("disparar",false);
			atras = false;
			caminar = false;
			disparar = false;
			cascar = false;

			animator.SetBool("cascado", true);
			col.gameObject.SetActive(false);
			salud -= 100;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = "100";

			if(PlayerPrefs.GetInt("violencia") == 1)
			{
				var explo = (GameObject)Instantiate(efectoSanre[Random.Range(0,efectoSanre.Length)], new Vector3(col.gameObject.transform.position.x,col.gameObject.transform.position.y-3, col.gameObject.transform.position.z-1), cascadoSpawn.rotation);
			}
		}*/
		if(col.gameObject.tag == "explo" && vivo)
		{
			actuando = false;

			animator.SetBool("atras",false);
			animator.SetBool("caminar",false);
			animator.SetBool("disparar",false);
			atras = false;
			caminar = false;
			disparar = false;
			cascar = false;

			v3 = Vector3.zero;

			animator.SetBool("cascado", true);

			salud -= col.gameObject.GetComponent<Explo>().poder;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = col.gameObject.GetComponent<Explo>().poder.ToString("F0");

			if(PlayerPrefs.GetInt("violencia") == 1)
			{
				var explo = (GameObject)Instantiate(efectoSanre[Random.Range(0,efectoSanre.Length)], new Vector3(col.gameObject.transform.position.x,col.gameObject.transform.position.y-3, col.gameObject.transform.position.z-1), cascadoSpawn.rotation);
			}
		}
		if(col.gameObject.tag == "obstaculo1" || col.gameObject.tag == "obstaculo2" || col.gameObject.name == "PuazD")
		{
			//CAMINA HACIA ARRIBA
			animator.SetBool("caminar",false);
			atras = true;
			caminar = false;
			atras = true;
		}
	}
	void OnCollisionExit (Collision col)
	{
		if(col.gameObject.tag == NameEnemy)
		{
			actuando = false;
			atras = false;
			caminar = false;
			disparar = false;
			cascar = false;
		}
	}

	//CREA CARTA
	IEnumerator esperaCarta()
	{
		yield return new WaitForSeconds(1);
		if(vivo)
		{
			crearCarta = false;
		}
	}
	GameObject baseNeutra;
	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "newtra" && vivo || col.gameObject.tag == BaseMala && vivo)
		{
			target = col.gameObject.transform;
			v3 = Vector3.zero;
			caminar = false;
			animator.SetBool("caminar", false);
			actuando = true;
		}

		if(col.gameObject.tag == "mira" && vivo)
		{
			mira.SetActive(true);
			mira.GetComponent<Animator>().SetBool("entry",true);
		}

		if(col.gameObject.tag == NameEnemy && vivo)
		{
			Player = col.gameObject.transform;
		}
		if(col.gameObject.tag == NameEnemyTank && vivo)
		{
			Player = col.gameObject.transform;
		}

		if(col.gameObject.name == "BaseNeutra")
		{
			baseNeutra = col.gameObject;
		}
		if(col.gameObject.tag == "balaLlamas" && vivo)
		{
			actuando = false;

			v3 = Vector3.zero;

			animator.SetBool("atras",false);
			animator.SetBool("caminar",false);
			animator.SetBool("disparar",false);
			atras = false;
			caminar = false;
			disparar = false;
			cascar = false;

			animator.SetBool("cascado", true);
			Destroy(col.gameObject);
			salud -= 3;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = "3";

			if(PlayerPrefs.GetInt("violencia") == 1)
			{
				var explo = (GameObject)Instantiate(efectoSanre[Random.Range(0,efectoSanre.Length)], new Vector3(col.gameObject.transform.position.x,col.gameObject.transform.position.y-3, col.gameObject.transform.position.z-1), cascadoSpawn.rotation);
			}
		}
		if(col.gameObject.tag == medic)
		{
			if(salud < saludMax)
			{
				CmdSaludSumar();
			}
		}
	}
	void OnTriggerStay (Collider col)
	{
		if(col.gameObject.tag == NameEnemy && vivo)
		{
			Player = col.gameObject.transform;
		}
		if(col.gameObject.tag == NameEnemyTank && vivo)
		{
			Player = col.gameObject.transform;
		}
	}
	void OnTriggerExit (Collider col)
	{
		if(col.gameObject.tag == "mira" && vivo)
		{
			mira.SetActive(false);
		}
		if(col.gameObject.tag == NameEnemy)
		{
			Player = null;
		}
		if(col.gameObject.tag == NameEnemyTank)
		{
			Player = null;
		}
		if(col.gameObject.name == "BaseNeutra")
		{
			baseNeutra = null;
		}
	}

	float suma;
	[Command]
	public void CmdSaludSumar()
	{
		suma = saludMax*2/80;
		salud += saludMax*2/80;
		//salud += 6;

		var letras = (GameObject)Instantiate(textos2, transform.position, Quaternion.Euler(0,0,0));
		letras.GetComponent<TextMesh>().text = "+"+suma.ToString("F0");
		NetworkServer.Spawn(letras);
	}

	//EVENTO SPINE
	void giro ()
	{
		v3 = Vector3.zero;
		if(transform.localScale.x == 1.13f)
		{
			voltear = -1.13f;
		}else
		{
			voltear = 1.13f;
		}
		atras = false;
		actuando = false;
	}
	void termino ()
	{
		actuando = false;
	}
	void rafaga ()
	{
		luz.SetActive(true);
		StartCoroutine(apaga());

		if(transform.localScale.x == 1.13f)
		{
			CmdDisparoD();
		}else
		{
			CmdDisparoI();
		}
	}
	//[Command]
	public void CmdDisparoD()
	{
		var bullet = (GameObject)Instantiate(bulletPref, bulletSpawn.position, bulletSpawn.rotation); 
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.right * 100;
		NetworkServer.Spawn(bullet);
		bullet.GetComponent<bala>().poder = saludMax*bullet.GetComponent<bala>().poder/200;
		bullet.GetComponent<bala>().cantidad = 1;
		Destroy(bullet, 1.0f);
		//CASQUILLOS}
		var casquillo = (GameObject)Instantiate(casquilloPref, casquilloSpawn.position, Quaternion.Euler(0,0,90)); 
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.right * Random.Range(-10,-50);
		casquillo.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(-0.0f,-0.2f));
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.up * Random.Range(-1,-6);
		NetworkServer.Spawn(casquillo);
	}
	//[Command]
	public void CmdDisparoI()
	{
		var bullet = (GameObject)Instantiate(bulletPref, bulletSpawn.position, Quaternion.Euler(0,180,0)); 
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.right * 100;
		NetworkServer.Spawn(bullet);
		bullet.GetComponent<bala>().poder = saludMax*bullet.GetComponent<bala>().poder/200;
		bullet.GetComponent<bala>().cantidad = 1;
		Destroy(bullet, 1.0f);
		//CASQUILLOS}
		var casquillo = (GameObject)Instantiate(casquilloPref, casquilloSpawn.position, Quaternion.Euler(0,0,-90)); 
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.right * Random.Range(-10,-50);
		casquillo.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(-0.0f,-0.2f));
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.up * Random.Range(-1,-6);
		NetworkServer.Spawn(casquillo);
	}

	void muerto ()
	{
		if(!isServer)
		{
			return;
		}

		if(gameObject.tag == "Player")
		{
			Panel.GetComponent<Game>().KillsM += 1;
		}else
		{
			Panel.GetComponent<Game>().KillsB += 1;
		}

		Destroy(gameObject);
	}

	void cartacrear()
	{
		if(crearCarta)
		{
			CmdCarta();
		}
	}
	[Command]
	public void CmdCarta()
	{
		var card = (GameObject)Instantiate(carta, bulletSpawn.position, bulletSpawn.rotation);
		card.GetComponent<Rigidbody>().velocity = card.transform.up * 30;
		//carta.GetComponent<Rigidbody>().AddForce(transform.right * -30);
		NetworkServer.Spawn(card);
	}

	[Command]
	public void CmdDestruir()
	{
		RpcDestruirCliente();
		Destroy(gameObject);
	}

	[ClientRpc]
	public void RpcDestruirCliente()
	{
		Destroy(gameObject);
	}
}
