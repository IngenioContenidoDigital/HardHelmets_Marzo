using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AINetwork : NetworkBehaviour {

	public bool pelea;

	public string BaseBuena;
	public string BaseMala;
	public string BuscarBase;
	public string NameEnemy;
	public string NameEnemyTank;

	public Transform Player;

	public int Tipo;

	public bool crearCarta;
	public GameObject carta;

	public Transform target;

	public Animator animator;

	public string _currentDirection = "right";

	[SyncVar(hook = "FacingCallback")]
	public int voltear;

	public bool vivo = true;

	[SyncVar]
	public float salud;
	[SyncVar]
	public float saludMax;

	//FUNCIONES
	bool actuando2;
	bool caminar2;
	bool shoot2;
	bool cargar2;
	int disparos2;

	//GROUND CHECHER
	public Transform groundCheck;
	float groundRadius = 0.3f;
	public LayerMask whatIsGround;
	public bool grounded = false;

	//SEGUNDO SCRIPT
	//OBJETOS DE DISPARO
	public GameObject bulletPref;
	public GameObject bulletPrefFusil;
	public GameObject bulletPrefEscopeta;
	public GameObject bulletPrefSubmetra;
	public GameObject bulletPrefMetra;

	public Transform bulletSpawn;
	public GameObject casquilloPref;
	public GameObject casquilloPrefB;
	public Transform casquilloSpawn;
	public GameObject granadePref;
	public GameObject luz;

	int azar;

	public Transform cascadoSpawn;

	//FUNCIONES ALEATORIAS
	bool actuando;

	bool cubrirse;
	bool cubierto;
	//public Vector3 coverPosition;
	bool shoot;
	int disparos;
	public bool alejarce;
	public bool alejarce2;
	public bool caminar;
	float dist;
	int dist2;
	int random;
	int tiempo;
	int tiempocover;

	bool rafaga = true;
	bool recarga;
	bool cargando = false;

	Vector3 v3;
	int dir;

	bool mirando;

	//QUE TAN LEJOS DE LA BASE DISPARA
	float lejos;

	public GameObject textos;
	public GameObject textos2;
	public GameObject particulasCurar;

	public GameObject mira;

	//ESQUIVAR OBSTACULOS
	bool devolver;

	//MUERE CON EXPLOCION
	bool explocion;
	public GameObject Huesos;
	public GameObject[] sangreCuchillo;

	public GameObject[] efectoSanre;

	//ACUCHILLADO
	bool acuchillado;
	int muerte;

	public bool water;
	int maxspeed;

	//VISION PARA ESQUIVAR OBSTACULOS
	public GameObject seguir;

	public GameObject Base;

	//PANEL PARTIDA
	GameObject Panel;

	//CURACION
	public string medic;

	//NIVEL DE CARTA
	public int level;
	public GameObject Jugador;


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

		saludMax = 3.4f*level+80;
		salud = saludMax;
			
		mira.SetActive(false);

		animator.SetBool("paracaidas", true);

		animator.SetBool("falling", true);

		shoot2 = false;
		shoot = false;

		_currentDirection = "right";

		lejos = Random.Range(25,31);
		muerte = Random.Range(1,3);
	}
	// Update is called once per frame
	public void Update ()
	{
		print("Nivel: " +level);
		print("Sangre: "+saludMax);
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

			cubrirse = false;
			cubierto = false;
			shoot = false;
			shoot2 = false;
			alejarce = false;
			alejarce2 = false;
			caminar = false;
			caminar2 = false;
			animator.SetInteger("disparo", 0);
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

		if(animator.GetCurrentAnimatorStateInfo(0).IsName("KillSimple3"))
		{
			vivo = false;
			animator.SetBool("muerto", true);
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
		{
			caminar = false;
			caminar2 = false;
			alejarce = false;
		}
		if(Player == null)
		{
			pelea = false;
		}
		//VOLTEA PERSONAJE
		gameObject.transform.localScale = new Vector3(voltear,1,1);

		if(pelea)
		{
			if(vivo)
			{
				if(seguir.GetComponent<vista>().arriba)
				{
					animator.SetBool("walk", true);
					v3 += Vector3.forward;
				}else if(seguir.GetComponent<vista>().abajo)
				{
					animator.SetBool("walk", true);
					v3 += Vector3.back;
				}else if(caminar || alejarce || alejarce2)
				{
					animator.SetBool("walk", false);
					v3 = Vector3.zero;
				}

				if(acuchillado)
				{
					StartCoroutine(esperaCuchillo());
				}
				if(grounded && !animator.GetBool("paracaidas"))
				{
					//MIRA AL OBJKETIVO
					Vector3 v3Dir = Player.position - transform.position;
					float angle = Mathf.Atan2(0, v3Dir.x) * Mathf.Rad2Deg;
					if(angle == 180 && !alejarce && !alejarce2)
					{
						_currentDirection = "left";
						voltear = -1;
						//gameObject.transform.localScale = new Vector3(-1,1,1);
					}else if(!alejarce && !alejarce2)
					{
						_currentDirection = "right";
						voltear = 1;
						//gameObject.transform.localScale = new Vector3(1,1,1);
					}

					animator.SetBool("falling", false);
					animator.SetBool("falling2", false);

					if(Player.tag != NameEnemyTank)
					{
						if(animator.GetCurrentAnimatorStateInfo(0).IsName("walk"))
						{
							animator.SetBool("walk", false);
						}
						if(Mathf.Abs((transform.position - Player.position).x) >= 5.01f && Mathf.Abs((transform.position - Player.position).x) <= 38f && !actuando && !alejarce && !alejarce2 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))//(Vector3.Distance(transform.position.x, Player.position.x) >= 8.01f && Vector3.Distance(transform.position.x, Player.position.x) <= 25f && !actuando && !alejarce && !alejarce2 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
						{
							Funciones();
						}
						if(Mathf.Abs((transform.position - Player.position).x) <= 5 && !cubrirse && !alejarce2 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))//(Vector3.Distance(transform.position.x, Player.position.x) <= 5 && !cubrirse && !alejarce2 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
						{
							caminar = false;
							shoot = false;
							alejarce2 = true;
							dist2 = Random.Range(1,3);
							StartCoroutine(esperaAleja2());
							actuando = true;
						}																													
					}else
					{
						if(Mathf.Abs((transform.position - Player.position).x) >= 25.01f && !shoot && !actuando && !alejarce && !alejarce2 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))// && Mathf.Abs((transform.position - Player.position).x) <= 35f && !shoot && !actuando && !alejarce && !alejarce2 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))//(Vector3.Distance(transform.position.x, Player.position.x) >= 8.01f && Vector3.Distance(transform.position.x, Player.position.x) <= 25f && !actuando && !alejarce && !alejarce2 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
						{
							FuncionesTanque();
						}
						if(Mathf.Abs((transform.position - Player.position).x) <= 25 && !cubrirse && !alejarce2 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))//(Vector3.Distance(transform.position.x, Player.position.x) <= 5 && !cubrirse && !alejarce2 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
						{
							caminar = false;
							shoot = false;
							alejarce2 = true;
							dist2 = Random.Range(1,3);
							StartCoroutine(esperaAleja2());
							actuando = true;
						}	
					}

					//ACCIONES UPDATE
					//ALEJARCE
					if(alejarce)
					{
						shoot = false;
						animator.SetBool("walk",true);
						if(_currentDirection == "left")
						{
							v3 += Vector3.left;
							//GetComponent<Rigidbody>().velocity = (Vector2.right * maxspeed);
							voltear = 1;
							//gameObject.transform.localScale = new Vector3(1,1,1);
						}else
						{
							v3 += Vector3.right;
							//GetComponent<Rigidbody>().velocity = (Vector2.left * maxspeed);
							voltear = -1;
							//gameObject.transform.localScale = new Vector3(-1,1,1);
						}
					}
					//ALEJARCE
					if(alejarce2)
					{
						shoot = false;
						animator.SetBool("walk",true);
						if(_currentDirection == "left")
						{
							v3 += Vector3.right;
							//GetComponent<Rigidbody>().velocity = (Vector2.right * maxspeed);
							voltear = 1;
							//gameObject.transform.localScale = new Vector3(1,1,1);
						}else
						{
							v3 += Vector3.left;
							//GetComponent<Rigidbody>().velocity = (Vector2.left * maxspeed);
							voltear = -1;
							//gameObject.transform.localScale = new Vector3(-1,1,1);
						}
					}else
					{
						dir = Random.Range(1,3);
					}

					//CAMINAR
					if(caminar)
					{
						animator.SetInteger("descanso",0);
						animator.SetBool("walk",true);
						if(_currentDirection == "right")
						{
							v3 += Vector3.right;
							//GetComponent<Rigidbody>().velocity = (Vector2.right * maxspeed);
						}else
						{
							v3 += Vector3.left;
							//GetComponent<Rigidbody>().velocity = (Vector2.left * maxspeed);
						}
					}
					if(animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
					{
						v3 = Vector3.zero;
						actuando = false;
					}
					//DISPARO
					if(shoot)
					{
						v3 = Vector3.zero;

						if(disparos >= 3)//2
						{
							animator.SetInteger("recarga", Tipo);
						}else
						{
							disparos += 1;
							animator.SetInteger("disparo", Tipo);
						}

						shoot = false;
					}
				}else if(!grounded)
				{
					alejarce = false;
				}
				//ANIMACION CAYENDO
				if(GetComponent<Rigidbody>().velocity.y < -4f)
				{
					animator.SetBool("falling", true);
				}else
				{
					animator.SetBool("falling", false);
				}

				if(salud <= 0)
				{
					int muerteg = Random.Range(11,14);
					if(explocion)
					{
						animator.SetBool("muerto", true);
						animator.SetInteger("muerte", muerteg);
						var bones = (GameObject)Instantiate(Huesos, casquilloSpawn.position, transform.rotation);
						explocion = false;
					}else if(acuchillado)
					{
						v3 = Vector3.zero;
						animator.SetBool("acuchillado", true);
						animator.SetInteger("muerte", muerte);
						acuchillado = false;
					}else if(quemado)
					{
						v3 = Vector3.zero;
						animator.SetBool("muerto", true);
						animator.SetInteger("muerte", 20);
					}else
					{
						v3 = Vector3.zero;
						animator.SetBool("muerto", true);
						int azar = Random.Range(1,3);
						animator.SetInteger("muerte", azar);
					}
					gameObject.layer = LayerMask.NameToLayer("muerto");
					Base.layer = LayerMask.NameToLayer("mira");

					StartCoroutine(muertee());

					vivo = false;
				}else
				{
					explocion = false;
				}
				if(v3 != Vector3.zero)
				{
					GetComponent<Rigidbody>().velocity = (6 * v3.normalized);
				}
			}else if(!vivo)
			{
				mira.SetActive(false);
				gameObject.layer = LayerMask.NameToLayer("muerto");
				//gameObject.tag = "Untagged";
			}
		}else
		{
			if(vivo && !animator.GetBool("paracaidas"))
			{
				if(seguir.GetComponent<vista>().arriba)
				{
					v3 += Vector3.forward;
				}else if(seguir.GetComponent<vista>().abajo)
				{
					v3 += Vector3.back;
				}else //if(caminar || alejarce || alejarce2)
				{
					v3 = Vector3.zero;
				}

				if(acuchillado)
				{
					StartCoroutine(esperaCuchillo());
				}
				if(grounded && target == null)
				{
					target = GameObject.FindWithTag(BuscarBase).transform;
				}else if(grounded && target.tag == BaseBuena)
				{
					target = null;
				}

				if(target != null)
				{
					//MIRA AL OBJKETIVO
					Vector3 v3Dir = target.position - transform.position;
					float angle = Mathf.Atan2(0, v3Dir.x) * Mathf.Rad2Deg;

					if(angle == 180)
					{
						_currentDirection = "left";
						voltear = -1;
						//gameObject.transform.localScale = new Vector3(-1,1,1);
					}else
					{
						_currentDirection = "right";
						voltear = 1;
						//gameObject.transform.localScale = new Vector3(1,1,1);
					}
				}

				if(salud <= 0)
				{
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
					}else
					{
						v3 = Vector3.zero;
						animator.SetBool("muerto", true);
						int azar = Random.Range(1,3);
						animator.SetInteger("muerte", azar);
					}

					gameObject.layer = LayerMask.NameToLayer("muerto");
					Base.layer = LayerMask.NameToLayer("mira");

					StartCoroutine(muertee());

					vivo = false;
				}else
				{
					explocion = false;
				}

				if(grounded && target != null)
				{
					if(target.tag == BuscarBase)
					{
						if(Tipo != 1)
						{
							//SI ESTA MUY LEJOS
							if(Mathf.Abs((transform.position - target.position).x) >= lejos && !caminar2 && !devolver)
							{
								actuando2 = false;
								caminar2 = true;
							}
							if(Mathf.Abs((transform.position - target.position).x) <= lejos-0.1f && !actuando2)
							{
								if(disparos2 <= 0)
								{
									shoot2 = true;
								}else
								{
									cargar2 = true;
								}
							}
						}else
						{
							if(Mathf.Abs((transform.position - target.position).x) >= lejos-8 && !caminar2 && !devolver)
							{
								print("CAMINAR A BASE");
								caminar2 = true;
							}
							if(Mathf.Abs((transform.position - target.position).x) <= lejos-8.1f && !actuando2)
							{
								print("ATACAR BASE");
								shoot2 = true;
							}
						}
					}else
					{
						//SI ESTA MUY LEJOS
						if(Mathf.Abs((transform.position - target.position).x) >= 20 && !caminar2 && !devolver)
						{
							actuando2 = false;
							caminar2 = true;
						}
						if(Mathf.Abs((transform.position - target.position).x) <= 20-Random.Range(1,7))
						{
							v3 = Vector3.zero;
							print("TOMANDO LA BASE");

							caminar2 = false;
							animator.SetBool("walk",false);
							actuando2 = true;
						}
					}
				}

				if(caminar2 && !devolver)
				{
					animator.SetInteger("descanso",0);
					animator.SetBool("walk",true);

					if(_currentDirection == "right")
					{
						v3 += Vector3.right;
						//GetComponent<Rigidbody>().velocity = (Vector2.right * maxspeed);
					}else
					{
						v3 += Vector3.left;
						//GetComponent<Rigidbody>().velocity = (Vector2.left * maxspeed);
					}
				}
				//DISPARO
				if(shoot2)
				{
					v3 = Vector3.zero;

					actuando2 = true; 

					caminar2 = false;
					animator.SetBool("walk",false);

					print("DEBERIA DISPARAR");
					animator.SetInteger("disparo", Tipo);
					disparos2 += 1;
					shoot2 = false;
				}
				//CARGAR
				if(cargar2)
				{
					v3 = Vector3.zero;

					actuando2 = true; 

					caminar2 = false;
					animator.SetBool("walk",false);

					print("DEBERIA CARGAR" + "x: "+ disparos2);
					animator.SetInteger("recarga", Tipo);

					cargar2 = false;
				}

				if(devolver)
				{
					caminar2 = false;
					shoot2 = false;
					animator.SetBool("walk",true);
					if(_currentDirection == "left")
					{
						v3 += Vector3.right;
						//GetComponent<Rigidbody>().velocity = (Vector2.right * maxspeed);
						voltear = 1;
						//gameObject.transform.localScale = new Vector3(1,1,1);
					}else if(acuchillado)
					{
						animator.SetBool("acuchillado", true);
						animator.SetInteger("muerte", muerte);
						acuchillado = false;
					}else
					{
						v3 += Vector3.left;
						//GetComponent<Rigidbody>().velocity = (Vector2.left * maxspeed);
						voltear = -1;
						//gameObject.transform.localScale = new Vector3(-1,1,1);
					}
				}
				if(v3 != Vector3.zero)
				{
					GetComponent<Rigidbody>().velocity = (6 * v3.normalized);
				}
			}else if(!vivo)
			{
				mira.SetActive(false);
				gameObject.layer = LayerMask.NameToLayer("muerto");
				//gameObject.tag = "Untagged";
			}
		}
	}

	IEnumerator muertee ()
	{
		yield return new WaitForSeconds(8f);
		Destroy(gameObject);
	}
	void FacingCallback(int voltear)
	{
		if (voltear == 1)
		{
			gameObject.transform.localScale = new Vector3(1,1,1);
		}else
		{
			gameObject.transform.localScale = new Vector3(-1,1,1);
		}
	}

	void FuncionesTanque()
	{
		actuando = true;
		random = Random.Range(3,6);
		movimientos();
		return;
	}
	void Funciones()
	{
		actuando = true;
		if(cubrirse)
		{
			random = Random.Range(6,10);
		}else
		{
			random = Random.Range(1,6);
		}
		movimientos();
		return;
	}
	void movimientos()
	{
		//RANDOM FUNCTIONS
		if(random == 1)
		{
			random = 0;
			camina();
		}else if(random == 2)
		{
			random = 0;
			disparar();
		}else if(random == 3)
		{
			random = 0;
			disparar();
		}else if(random == 4)
		{
			random = 0;
			if(Player.transform.position.z-3 > transform.position.z || Player.transform.position.z+3 < transform.position.z)
			{
				camina();
			}else
			{
				disparar();
			}
		}else if(random == 5)
		{
			random = 0;
			if(Player.transform.position.z-3 > transform.position.z || Player.transform.position.z+3 < transform.position.z)
			{
				camina();
			}else
			{
				disparar();
			}
		}else if(random == 6)
		{
			random = 0;
			disparar();
			//alejar();
		}else if(random == 7)
		{
			random = 0;
			if(cubrirse)
			{
				cover();
			}
		}else if(random == 8)
		{
			random = 0;
			if(cubrirse)
			{
				cover();
			}
		}else if(random == 9)
		{
			random = 0;
			disparar();
		}else if(random == 10)
		{
			random = 0;
			disparar();
		}
	}
	//MOVIMIENTOS DE PERSONAJE
	//CUBRIRSE
	void cover()
	{
		cubierto = true;
		animator.SetBool("cubrirse", true);
		StartCoroutine(esperacover());
	}
	//CAMINAR
	void camina()
	{
		dist = Random.Range(0.5f,1.5f);
		StartCoroutine(esperaCamina());
		caminar = true;
	}
	//ALEJARCE
	void alejar()
	{
		dist = Random.Range(2,4);
		StartCoroutine(esperaAleja());
		alejarce = true;
	}
	//DISPARO
	void disparar()
	{
		shoot = true;
	}
	//TIEMPOS DE ESPERA
	IEnumerator esperacover()
	{
		yield return new WaitForSeconds(4);
		animator.SetBool("cubierto", false);
		animator.SetBool("cubrirse", false);
		cubierto = false;
		actuando = false;
	}
	IEnumerator esperaAleja()
	{
		yield return new WaitForSeconds(dist);
		animator.SetBool("walk",false);
		alejarce = false;
		actuando = false;
	}
	IEnumerator esperaAleja2()
	{
		yield return new WaitForSeconds(dist2);
		animator.SetBool("walk",false);
		alejarce2 = false;
		actuando = false;
	}
	IEnumerator esperadevolver()
	{
		yield return new WaitForSeconds(dist2);
		animator.SetBool("walk",false);
		devolver = false;
	}
	IEnumerator esperaCamina()
	{
		yield return new WaitForSeconds(dist);
		animator.SetBool("walk",false);
		caminar = false;
		actuando = false;
	}
	IEnumerator esperaCuchillo()
	{
		yield return new WaitForSeconds(2);
		acuchillado = false;
	}
	IEnumerator esperaActuar()
	{
		yield return new WaitForSeconds(Random.Range(2,5));
		actuando = false;
		actuando2 = false;
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "Water")
		{
			water = true;
			maxspeed = 6;
		}
		if(col.gameObject.tag == "Piso")
		{
			water = false;
			maxspeed = 8;
		}

		if(col.gameObject.tag == "cuchillo" && vivo)
		{
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			animator.SetBool("walk",false);
			caminar = false;
			caminar2 = false;
			alejarce = false;
			alejarce2 = false;
			animator.SetInteger("cascado", 4);
			acuchillado = true;
			//efect = Random.Range(3,5);
			if(PlayerPrefs.GetInt("violencia") == 1)
			{
				var sangre2 = (GameObject)Instantiate(sangreCuchillo[Random.Range(0,sangreCuchillo.Length)], cascadoSpawn.position, cascadoSpawn.rotation); 
			}
			salud -= 15;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = "15";

			crearCarta = true;
			StartCoroutine(esperaCarta());
		}
		if(col.gameObject.tag == "bala" && vivo)
		{
			//caminar = true;
			random = Random.Range(0,4);
			animator.SetBool("walk",false);
			caminar = false;
			alejarce = false;
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			caminar2 = false;
			alejarce2 = false;
			animator.SetInteger("cascado", 1);

			salud -= col.gameObject.GetComponent<bala>().poder;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = col.gameObject.GetComponent<bala>().poder.ToString("F0");

			if(PlayerPrefs.GetInt("violencia") == 1)
			{
				var explo = (GameObject)Instantiate(efectoSanre[Random.Range(0,efectoSanre.Length)], new Vector3(col.gameObject.transform.position.x,col.gameObject.transform.position.y-3, col.gameObject.transform.position.z-1), cascadoSpawn.rotation);
			}
		}
		/*if(col.gameObject.tag == "balaFusil" && vivo)
		{
			//caminar = true;
			random = Random.Range(0,4);
			animator.SetBool("walk",false);
			caminar = false;
			alejarce = false;
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			caminar2 = false;
			alejarce2 = false;
			animator.SetInteger("cascado", 1);

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
			//caminar = true;
			random = Random.Range(0,4);
			animator.SetBool("walk",false);
			caminar = false;
			alejarce = false;
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			caminar2 = false;
			alejarce2 = false;
			animator.SetInteger("cascado", 1);

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
			//caminar = true;
			random = Random.Range(0,4);
			animator.SetBool("walk",false);
			caminar = false;
			alejarce = false;
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			caminar2 = false;
			alejarce2 = false;
			animator.SetInteger("cascado", 1);

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
			//caminar = true;
			random = Random.Range(0,4);
			animator.SetBool("walk",false);
			caminar = false;
			alejarce = false;
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			caminar2 = false;
			alejarce2 = false;
			animator.SetInteger("cascado", 1);

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
			//caminar = true;
			random = Random.Range(0,4);
			animator.SetBool("walk",false);
			caminar = false;
			alejarce = false;
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			caminar2 = false;
			alejarce2 = false;
			animator.SetInteger("cascado", 1);

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
			//caminar = true;
			random = Random.Range(0,4);
			animator.SetBool("walk",false);
			caminar = false;
			alejarce = false;
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			caminar2 = false;
			alejarce2 = false;
			animator.SetInteger("cascado", 1);
			Destroy(col.gameObject);
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
			random = Random.Range(0,4);
			animator.SetBool("walk",false);
			caminar = false;
			alejarce = false;
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			caminar2 = false;
			alejarce2 = false;

			salud -= col.gameObject.GetComponent<Explo>().poder;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = col.gameObject.GetComponent<Explo>().poder.ToString("F0");

			if(PlayerPrefs.GetInt("violencia") == 1)
			{
				explocion = true;
			}
		}
		if(col.gameObject.tag == "pared" && vivo)
		{
			alejarce = false;
			animator.SetBool("walk", false);
		}
		if(col.gameObject.tag == NameEnemyTank && vivo)
		{
			print(col.gameObject.GetComponent<Rigidbody>().velocity.x);
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			caminar2 = false;
			alejarce2 = false;
			if(col.gameObject.GetComponent<Rigidbody>().velocity.x > 2.5f)
			{
				salud -= 100;

				var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
				letras.GetComponent<TextMesh>().text = "100";

				if(PlayerPrefs.GetInt("violencia") == 1)
				{
					var explo = (GameObject)Instantiate(efectoSanre[Random.Range(0,efectoSanre.Length)], new Vector3(col.gameObject.transform.position.x,col.gameObject.transform.position.y-3, col.gameObject.transform.position.z-1), cascadoSpawn.rotation);
				}
			}
		}
		//COLLISIONA CON ALGUN OBSTACULO
		if(col.gameObject.tag == "obstaculo1" || col.gameObject.tag == "obstaculo2" || col.gameObject.name == "PuazD")
		{
			//CAMINA HACIA ARRIBA
			if(pelea)
			{
				caminar = false;
				dist2 = Random.Range(1,3);
				StartCoroutine(esperaAleja2());
				alejarce2 = true;
			}else
			{
				caminar2 = false;
				dist2 = Random.Range(1,3);
				StartCoroutine(esperadevolver());
				devolver = true;
			}
		}
	}

	void OnCollisionStay (Collision col)
	{
		if(col.gameObject.tag == "pared" && vivo)
		{
			animator.SetBool("walk", false);
		}
	}
	bool quemado;
	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "newtra" && vivo || col.gameObject.tag == BaseMala && vivo)
		{
			target = col.gameObject.transform;
			v3 = Vector3.zero;
			caminar2 = false;
			animator.SetBool("walk",false);
			actuando2 = true;
		}

		if(col.gameObject.tag == "mira" && vivo)
		{
			mira.SetActive(true);
			mira.GetComponent<Animator>().SetBool("entry",true);
		}

		/*if(col.gameObject.tag == "cobertura" && vivo)
		{
			animator.SetBool("walk",false);
			caminar = false;
			coverPosition = new Vector3(col.gameObject.transform.position.x, transform.position.y, col.gameObject.transform.position.z);
			cubrirse = true;
		}*/
		if(col.gameObject.tag == "balaLlamas" && vivo)
		{
			random = Random.Range(0,4);
			animator.SetBool("walk",false);
			caminar = false;
			alejarce = false;
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			caminar2 = false;
			alejarce2 = false;
			animator.SetInteger("cascado", 1);
			Destroy(col.gameObject);

			quemado = true;
			salud -= 1;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = "10";
		}
		if(col.gameObject.tag == NameEnemy && vivo)
		{
			pelea = true;
			Player = col.gameObject.transform;
		}
		if(col.gameObject.tag == NameEnemyTank && vivo)
		{
			pelea = true;
			Player = col.gameObject.transform;
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
		if(Player == null)
		{
			if(col.gameObject.tag == NameEnemy && vivo)
			{
				pelea = true;
				Player = col.gameObject.transform;
			}
			if(col.gameObject.tag == NameEnemyTank && vivo)
			{
				pelea = true;
				Player = col.gameObject.transform;
			}
		}
	}
	void OnTriggerExit (Collider col)
	{
		if(col.gameObject.tag == "balaLlamas" && vivo)
		{
			quemado = false;
		}

		if(col.gameObject.tag == "mira" && vivo)
		{
			mira.SetActive(false);
		}
		if(col.gameObject.tag == "cobertura" && vivo)
		{
			cubrirse = false;
			cubierto = false;
		}
		if(col.gameObject.tag == NameEnemy)
		{
			pelea = false;
		}
		if(col.gameObject.tag == NameEnemyTank)
		{
			pelea = false;
		}
	}

	float suma;
	[Command]
	public void CmdSaludSumar()
	{
		suma = saludMax*5/80;
		salud += saludMax*5/80;
		//salud += 6;

		var letras = (GameObject)Instantiate(textos2, transform.position, Quaternion.Euler(0,0,0));
		letras.GetComponent<TextMesh>().text = suma.ToString("F0");
		NetworkServer.Spawn(letras);

		var part = (GameObject)Instantiate(particulasCurar, transform.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(part);
	}

	//APAGA LA LUZ
	IEnumerator apaga ()
	{
		yield return new WaitForSeconds(0.1f);
		luz.SetActive(false);
	}

	//EVENTOS SPINE
	void Shot ()
	{
		shoot = false;
		rafaga = false;
		luz.SetActive(true);
		StartCoroutine(apaga());

		if(_currentDirection == "right")
		{
			/*var bullet = (GameObject)Instantiate(bulletPref, bulletSpawn.position, bulletSpawn.rotation); 
			bullet.GetComponent<Rigidbody>().velocity = bullet.transform.right * 100;
			Destroy(bullet, 5.0f);*/
		}else
		{
			/*var bullet = (GameObject)Instantiate(bulletPref, bulletSpawn.position, Quaternion.Euler(0,180,0)); 
			bullet.GetComponent<Rigidbody>().velocity = bullet.transform.right * 100;
			Destroy(bullet, 5.0f);*/
		}

		//CASQUILLO
		var casquillo = (GameObject)Instantiate(casquilloPref, casquilloSpawn.position, casquilloSpawn.rotation); 
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.right * Random.Range(-10,-50);//-50;
		casquillo.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(-0.0f,-0.2f));
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.up * Random.Range(-1,-6);//-5;

		StartCoroutine(esperaActuar());
	}
	void ShotA ()//FUSIL
	{
		luz.SetActive(true);
		StartCoroutine(apaga());

		if(_currentDirection == "right")
		{
			CmdShotAD();
		}else
		{
			CmdShotAI();
		}

		StartCoroutine(esperaActuar());
	}
	[Command]
	public void CmdShotAD()
	{
		var bullet = (GameObject)Instantiate(bulletPrefFusil, bulletSpawn.position, bulletSpawn.rotation);
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.right * 100;
		NetworkServer.Spawn(bullet);
		bullet.GetComponent<bala>().poder = saludMax*bullet.GetComponent<bala>().poder/80;
		Destroy(bullet, 5.0f);
		//CASQUILLOS
		var casquillo = (GameObject)Instantiate(casquilloPref, casquilloSpawn.position, casquilloSpawn.rotation); 
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.right * Random.Range(-10,-50);
		casquillo.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(-0.0f,-0.2f));
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.up * Random.Range(-1,-6);
		NetworkServer.Spawn(casquillo);
	}

	[Command]
	public void CmdShotAI()
	{
		var bullet = (GameObject)Instantiate(bulletPrefFusil, bulletSpawn.position, Quaternion.Euler(0,180,0));
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.right * 100;
		NetworkServer.Spawn(bullet);
		bullet.GetComponent<bala>().poder = saludMax*bullet.GetComponent<bala>().poder/80;
		Destroy(bullet, 5.0f);
		//CASQUILLOS
		var casquillo = (GameObject)Instantiate(casquilloPref, casquilloSpawn.position, casquilloSpawn.rotation); 
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.right * Random.Range(-10,-50);
		casquillo.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(-0.0f,-0.2f));
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.up * Random.Range(-1,-6);
		NetworkServer.Spawn(casquillo);
	}

	void ShotB ()//ESCOPETA
	{
		luz.SetActive(true);
		StartCoroutine(apaga());
		if(_currentDirection == "right")
		{
			CmdShotBD();
		}else
		{
			CmdShotBI();
		}

		StartCoroutine(esperaActuar());
	}
	[Command]
	void CmdShotBD()
	{
		var bulletB = (GameObject)Instantiate(bulletPrefEscopeta, bulletSpawn.position, Quaternion.Euler(0,Random.Range(-15,16),bulletSpawn.rotation.eulerAngles.z+Random.Range(5,16)));//Quaternion.Euler(0,0,bulletSpawn.rotation.z+10)
		bulletB.GetComponent<Rigidbody>().velocity = bulletB.transform.right * 100;
		NetworkServer.Spawn(bulletB);
		bulletB.GetComponent<bala>().poder = saludMax*bulletB.GetComponent<bala>().poder/80;
		Destroy(bulletB, 0.5f);

		var bulletB2 = (GameObject)Instantiate(bulletPrefEscopeta, bulletSpawn.position, Quaternion.Euler(0,Random.Range(-15,16),bulletSpawn.rotation.eulerAngles.z+Random.Range(5,16)));//Quaternion.Euler(0,0,bulletSpawn.rotation.z+5)
		bulletB2.GetComponent<Rigidbody>().velocity = bulletB2.transform.right * 100;
		NetworkServer.Spawn(bulletB2);
		bulletB2.GetComponent<bala>().poder = saludMax*bulletB2.GetComponent<bala>().poder/80;
		Destroy(bulletB2, 0.5f);

		var bulletB3 = (GameObject)Instantiate(bulletPrefEscopeta, bulletSpawn.position, bulletSpawn.rotation);//Quaternion.Euler(0,0,0)
		bulletB3.GetComponent<Rigidbody>().velocity = bulletB3.transform.right * 100;
		NetworkServer.Spawn(bulletB3);
		bulletB3.GetComponent<bala>().poder = saludMax*bulletB3.GetComponent<bala>().poder/80;
		Destroy(bulletB3, 0.5f);

		var bulletB4 = (GameObject)Instantiate(bulletPrefEscopeta, bulletSpawn.position, Quaternion.Euler(0,Random.Range(-15,16),bulletSpawn.rotation.eulerAngles.z-Random.Range(-5,-16)));//Quaternion.Euler(0,0,-5)
		bulletB4.GetComponent<Rigidbody>().velocity = bulletB4.transform.right * 100;
		NetworkServer.Spawn(bulletB4);
		bulletB4.GetComponent<bala>().poder = saludMax*bulletB4.GetComponent<bala>().poder/80;
		Destroy(bulletB4, 0.5f);

		var bulletB5 = (GameObject)Instantiate(bulletPrefEscopeta, bulletSpawn.position, Quaternion.Euler(0,Random.Range(-15,16),bulletSpawn.rotation.eulerAngles.z-Random.Range(-5,-16)));
		bulletB5.GetComponent<Rigidbody>().velocity = bulletB5.transform.right * 100;
		NetworkServer.Spawn(bulletB5);
		bulletB5.GetComponent<bala>().poder = saludMax*bulletB5.GetComponent<bala>().poder/80;
		Destroy(bulletB5, 0.5f);
		//CASQUILLOS
		var casquillo = (GameObject)Instantiate(casquilloPrefB, casquilloSpawn.position, casquilloSpawn.rotation); 
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.right * Random.Range(-10,-50);
		casquillo.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(-0.0f,-0.2f));
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.up * Random.Range(-1,-6);
		NetworkServer.Spawn(casquillo);
	}
	[Command]
	public void CmdShotBI()
	{
		var bulletB = (GameObject)Instantiate(bulletPrefEscopeta, bulletSpawn.position, Quaternion.Euler(0,180+Random.Range(-15,16),bulletSpawn.rotation.eulerAngles.z+Random.Range(5,16))); 
		bulletB.GetComponent<Rigidbody>().velocity = bulletB.transform.right * 100;
		NetworkServer.Spawn(bulletB);
		bulletB.GetComponent<bala>().poder = saludMax*bulletB.GetComponent<bala>().poder/80;
		Destroy(bulletB, 0.5f);

		var bulletB2 = (GameObject)Instantiate(bulletPrefEscopeta, bulletSpawn.position, Quaternion.Euler(0,180+Random.Range(-15,16),bulletSpawn.rotation.eulerAngles.z+Random.Range(5,16))); 
		bulletB2.GetComponent<Rigidbody>().velocity = bulletB2.transform.right * 100;
		NetworkServer.Spawn(bulletB2);
		bulletB2.GetComponent<bala>().poder = saludMax*bulletB2.GetComponent<bala>().poder/80;
		Destroy(bulletB2, 0.5f);

		var bulletB3 = (GameObject)Instantiate(bulletPrefEscopeta, bulletSpawn.position, bulletSpawn.rotation); 
		bulletB3.GetComponent<Rigidbody>().velocity = bulletB3.transform.right * 100;
		NetworkServer.Spawn(bulletB3);
		bulletB3.GetComponent<bala>().poder = saludMax*bulletB3.GetComponent<bala>().poder/80;
		Destroy(bulletB3, 0.5f);

		var bulletB4 = (GameObject)Instantiate(bulletPrefEscopeta, bulletSpawn.position, Quaternion.Euler(0,180+Random.Range(-15,16),bulletSpawn.rotation.eulerAngles.z+Random.Range(-5,-16))); 
		bulletB4.GetComponent<Rigidbody>().velocity = bulletB4.transform.right * 100;
		NetworkServer.Spawn(bulletB4);
		bulletB4.GetComponent<bala>().poder = saludMax*bulletB4.GetComponent<bala>().poder/80;
		Destroy(bulletB4, 0.5f);

		var bulletB5 = (GameObject)Instantiate(bulletPrefEscopeta, bulletSpawn.position, Quaternion.Euler(0,180+Random.Range(-15,16),bulletSpawn.rotation.eulerAngles.z+Random.Range(-5,-16))); 
		bulletB5.GetComponent<Rigidbody>().velocity = bulletB5.transform.right * 100;
		NetworkServer.Spawn(bulletB5);
		bulletB5.GetComponent<bala>().poder = saludMax*bulletB5.GetComponent<bala>().poder/80;
		Destroy(bulletB5, 0.5f);
		//CASQUILLOS
		var casquillo = (GameObject)Instantiate(casquilloPrefB, casquilloSpawn.position, casquilloSpawn.rotation); 
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.right * Random.Range(-10,-50);
		casquillo.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(-0.0f,-0.2f));
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.up * Random.Range(-1,-6);
		NetworkServer.Spawn(casquillo);
	}

	void ShotC ()//SUBMETRA
	{
		luz.SetActive(true);
		StartCoroutine(apaga());

		if(_currentDirection == "right")
		{
			CmdShotCD();
		}else
		{
			CmdShotCI();
		}

		StartCoroutine(esperaActuar());
	}
	[Command]
	public void CmdShotCD()
	{
		var bullet = (GameObject)Instantiate(bulletPrefSubmetra, bulletSpawn.position, bulletSpawn.rotation); 
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.right * 100;
		NetworkServer.Spawn(bullet);
		bullet.GetComponent<bala>().poder = saludMax*bullet.GetComponent<bala>().poder/80;
		Destroy(bullet, 5.0f);
		//CASQUILLOS
		var casquillo = (GameObject)Instantiate(casquilloPref, casquilloSpawn.position, casquilloSpawn.rotation); 
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.right * Random.Range(-10,-50);
		casquillo.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(-0.0f,-0.2f));
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.up * Random.Range(-1,-6);
		NetworkServer.Spawn(casquillo);
	}
	[Command]
	public void CmdShotCI()
	{
		var bullet = (GameObject)Instantiate(bulletPrefSubmetra, bulletSpawn.position, Quaternion.Euler(0,180,0)); 
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.right * 100;
		NetworkServer.Spawn(bullet);
		bullet.GetComponent<bala>().poder = saludMax*bullet.GetComponent<bala>().poder/80;
		Destroy(bullet, 5.0f);
		//CASQUILLOS
		var casquillo = (GameObject)Instantiate(casquilloPref, casquilloSpawn.position, casquilloSpawn.rotation); 
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.right * Random.Range(-10,-50);
		casquillo.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(-0.0f,-0.2f));
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.up * Random.Range(-1,-6);
		NetworkServer.Spawn(casquillo);
	}
	void ShotD ()//METRA
	{
		luz.SetActive(true);
		StartCoroutine(apaga());

		if(_currentDirection == "right")
		{
			CmdShotDD();
		}else
		{
			CmdShotDI();
		}

		StartCoroutine(esperaActuar());
	}
	[Command]
	public void CmdShotDD()
	{

		var bullet = (GameObject)Instantiate(bulletPrefMetra, bulletSpawn.position, bulletSpawn.rotation); 
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.right * 100;
		NetworkServer.Spawn(bullet);
		bullet.GetComponent<bala>().poder = saludMax*bullet.GetComponent<bala>().poder/80;
		Destroy(bullet, 5.0f);
		//CASQUILLOS
		var casquillo = (GameObject)Instantiate(casquilloPref, casquilloSpawn.position, casquilloSpawn.rotation); 
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.right * Random.Range(-10,-50);
		casquillo.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(-0.0f,-0.2f));
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.up * Random.Range(-1,-6);
		NetworkServer.Spawn(casquillo);
	}
	[Command]
	public void CmdShotDI()
	{
		var bullet = (GameObject)Instantiate(bulletPrefMetra, bulletSpawn.position, Quaternion.Euler(0,180,0)); 
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.right * 100;
		NetworkServer.Spawn(bullet);
		bullet.GetComponent<bala>().poder = saludMax*bullet.GetComponent<bala>().poder/80;
		Destroy(bullet, 5.0f);
		//CASQUILLOS
		var casquillo = (GameObject)Instantiate(casquilloPref, casquilloSpawn.position, casquilloSpawn.rotation); 
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.right * Random.Range(-10,-50);
		casquillo.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(-0.0f,-0.2f));
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.up * Random.Range(-1,-6);
		NetworkServer.Spawn(casquillo);
	}
	void load2 ()
	{
		disparos = 0;
		disparos2 = 0;
		actuando = false;
		actuando2 = false;
		shoot = false;
		shoot2 = false;
		cargar2 = false;
	}
	/*void granada ()
	{
		if(_currentDirection == "right")
		{
			CmdGranadaD();
		}else
		{
			CmdGranadaDI();
		}
	}
	[Command]
	public void CmdGranadaD()
	{
		var granade = (GameObject)Instantiate(granadePref, bulletSpawn.position, bulletSpawn.rotation);
		granade.GetComponent<Rigidbody>().velocity = granade.transform.up * 20;
		granade.GetComponent<Rigidbody>().AddForce(transform.right * 30);
		NetworkServer.Spawn(granade);
	}
	[Command]
	public void CmdGranadaDI()
	{
		var granade = (GameObject)Instantiate(granadePref, bulletSpawn.position, bulletSpawn.rotation);
		granade.GetComponent<Rigidbody>().velocity = granade.transform.up * 20;
		granade.GetComponent<Rigidbody>().AddForce(transform.right * -30);
		NetworkServer.Spawn(granade);
	}*/

	//CREA CARTA
	IEnumerator esperaCarta()
	{
		yield return new WaitForSeconds(1);
		if(vivo)
		{
			crearCarta = false;
		}
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
	public GameObject Arma;
	void tiraarma()
	{
		CmdVotaArma();
	}

	[Command]
	public void CmdVotaArma()
	{
		var arma = (GameObject)Instantiate(Arma, bulletSpawn.position, Quaternion.Euler(0,0,0)); 

		arma.GetComponent<Rigidbody>().velocity = arma.transform.up * 20;
		arma.GetComponent<Rigidbody>().velocity = arma.transform.right * 3;
		arma.GetComponent<Rigidbody>().AddTorque(transform.forward * 500 * 500);

		NetworkServer.Spawn(arma);
		Destroy(arma, 2.0f);
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
