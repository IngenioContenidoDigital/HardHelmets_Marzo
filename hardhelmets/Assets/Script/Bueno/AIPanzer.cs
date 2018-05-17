using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPanzer : MonoBehaviour {

	public bool pelea;

	bool crearCarta;
	public GameObject carta;

	public string BaseBuena;
	public string BaseMala;
	public string BuscarBase;
	public string NameEnemy;
	public string NameEnemyTank;

	public Transform Player;

	public Transform target;

	public Animator animator;
	string _currentDirection = "right";

	public int voltear;

	public bool vivo = true;


	public float salud;

	public float saludMax;

	//FUNCIONES
	bool actuando2;
	bool caminar2;
	bool shoot2;
	int disparos2;


	//ESQUIVAR OBSTACULOS
	public GameObject seguir;


	//GROUND CHECHER
	public Transform groundCheck;
	float groundRadius = 0.3f;
	public LayerMask whatIsGround;
	public bool grounded = false;

	//SEGUNDO SCRIPT
	//OBJETOS DE DISPARO
	public GameObject bulletPref;

	public Transform bulletSpawn;
	public GameObject luz;

	int azar;

	public Transform cascadoSpawn;
	public GameObject carne;

	//FUNCIONES ALEATORIAS
	bool actuando;

	bool cubrirse;
	bool cubierto;
	public Vector3 coverPosition;
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

	//CAMARA LENTA
	int matrix;
	bool matrix2;

	Vector3 v3;
	int dir;

	bool mirando;

	//QUE TAN LEJOS DE LA BASE DISPARA
	float lejos;

	public GameObject textos;
	public GameObject textos2;

	public GameObject mira;

	//MUERE CON EXPLOCION
	bool explocion;
	public GameObject Huesos;
	public GameObject[] sangreCuchillo;

	public GameObject[] efectoSanre;

	public bool water;
	int maxspeed;

	//ACUCHILLADO
	bool acuchillado;

	public GameObject Base;

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
		if(gameObject.tag == "Player")
		{
			Jugador = GameObject.Find("Hero");
		}else
		{
			Jugador = GameObject.Find("Hero2");
		}

		if(gameObject.tag == "Player")
		{
			level = Jugador.GetComponent<Hero>().level;
		}else
		{
			level = PlayerPrefs.GetInt("levelCommunity");
		}

		saludMax = 3.4f*level+80;
		salud = saludMax;

		mira.SetActive(false);

		animator.SetBool("falling", true);

		matrix = Random.Range(1,6);
		_currentDirection = "right";

		lejos = Random.Range(25,31);
	}
	// Update is called once per frame
	void Update ()
	{
		if(Panel == null)
		{
			Panel = GameObject.Find("GAME");
		}
		if(Panel.GetComponent<GameOffline>().final)
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
		}

		//CHECA SI ESTA EN EL PISO
		grounded = Physics.CheckSphere(groundCheck.position, groundRadius, whatIsGround);
		animator.SetBool("grounded", grounded);

		//VOLTEA PERSONAJE
		gameObject.transform.localScale = new Vector3(voltear,1,1);

		if(salud >= saludMax)
		{
			salud = saludMax;
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

		if(pelea)
		{
			if(vivo)
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
				if(grounded)
				{
					animator.SetBool("falling", false);

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

					if(Player.tag != NameEnemyTank)
					{
						if(Mathf.Abs((transform.position - Player.position).x) >= 15.01f && !actuando && !alejarce && !alejarce2 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
						{
							disparar();
							//Funciones();
							actuando = true;
						}
						if(Mathf.Abs((transform.position - Player.position).x) <= 15 && !cubrirse && !alejarce2 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
						{
							animator.SetBool("walk", true);
							caminar = false;
							shoot = false;
							alejarce2 = true;
							dist2 = Random.Range(2,4);
							StartCoroutine(esperaAleja2());
							actuando = true;
						}																															
					}else
					{
						if(Mathf.Abs((transform.position - Player.position).x) >= 25.01f && !shoot && !actuando && !alejarce && !alejarce2 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
						{
							FuncionesTanque();
						}
						if(Mathf.Abs((transform.position - Player.position).x) <= 25 && !cubrirse && !alejarce2 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
						{
							animator.SetBool("walk", true);
							caminar = false;
							shoot = false;
							alejarce2 = true;
							dist2 = Random.Range(3,6);
							StartCoroutine(esperaAleja2());
							actuando = true;
						}	
					}

					//ACCIONES UPDATE
					//ALEJARCE
					if(alejarce)
					{
						print("ALEJARCE");
						animator.SetBool("disparo", false);

						shoot = false;
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
					}
					//ALEJARCE
					if(alejarce2)
					{
						print("ALEJARCE 2");
						animator.SetBool("disparo", false);

						shoot = false;
						if(animator.GetCurrentAnimatorStateInfo(0).IsName("panzerwalk"))
						{
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
						}
					}else
					{
						dir = Random.Range(1,3);
					}
					//CAMINAR
					if(caminar)
					{
						print("CAMINAR");
						animator.SetBool("disparo", false);
						if(animator.GetCurrentAnimatorStateInfo(0).IsName("panzerwalk"))
						{
							if(_currentDirection == "right")
							{
								v3 += Vector3.right;
								//GetComponent<Rigidbody>().velocity = (Vector2.right * maxspeed);
							}else
							{
								v3 += Vector3.left;
								//GetComponent<Rigidbody>().velocity = (Vector2.left * maxspeed);
							}
							if(Player.transform.position.z > transform.position.z)
							{
								seguir.GetComponent<vista>().arriba = true;
							}else if(Player.transform.position.z < transform.position.z)
							{
								seguir.GetComponent<vista>().abajo = true;
							}
						}
					}
					if(animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
					{
						v3 = Vector3.zero;
						actuando = false;
					}
					//DISPARO
					if(shoot && !animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
					{
						if(Mathf.Abs((transform.position - Player.position).z) <= 3)
						{
							animator.SetBool("disparo", true);

							v3 = Vector3.zero;
							print("DISPARO");
							seguir.GetComponent<vista>().arriba = false;
							seguir.GetComponent<vista>().abajo = false;

							shoot = false;
							actuando = false;
						}else
						{
							camina();
						}
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
					int muerte = Random.Range(1,3);
					int muerteg = Random.Range(11,14);
					if(explocion)
					{
						animator.SetBool("muerto", true);
						animator.SetInteger("muerte", muerteg);
						var bones = (GameObject)Instantiate(Huesos, transform.position, transform.rotation);
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
			}else
			{
				mira.SetActive(false);
				if(matrix == 1 && !matrix2 && Player.name == "Hero")
				{
					matrix2 = true;
				}
				gameObject.layer = LayerMask.NameToLayer("muerto");
				//gameObject.tag = "Untagged";
			}
		}else
		{
			if(vivo)
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
				if(grounded)
				{
					animator.SetBool("falling", false);
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
					int muerte = Random.Range(1,3);
					if(explocion)
					{
						animator.SetBool("muerto", true);
						animator.SetInteger("muerte", muerteg);
						var bones = (GameObject)Instantiate(Huesos, transform.position, transform.rotation); 
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

				if(grounded && target != null)
				{
					if(target.tag == BuscarBase)
					{
						//SI ESTA MUY LEJOS
						if(Mathf.Abs((transform.position - target.position).x) >= lejos && !caminar2)
						{
							animator.SetBool("walk", true);
							animator.SetBool("walking", false);
							caminar2 = true;
						}
						if(Mathf.Abs((transform.position - target.position).x) <= lejos-0.1f && !actuando2)
						{
							shoot2 = true;
						}
					}else
					{
						//SI ESTA MUY LEJOS
						if(Mathf.Abs((transform.position - target.position).x) > 20 && !caminar2)
						{
							actuando2 = false;
							caminar2 = true;
						}
						if(Mathf.Abs((transform.position - target.position).x) <= 20-Random.Range(1,7))
						{
							v3 = Vector3.zero;

							caminar2 = false;
							animator.SetBool("normal", true);
							animator.SetBool("walk", false);
							animator.SetBool("walking", false);
							print("TOMANDO LA BASE");

							actuando2 = true;
						}
					}
				}
				if(caminar2)
				{
					print("CAMINANDO A BASE");
					animator.SetBool("walk", true);
					animator.SetBool("disparo", false);
					if(animator.GetCurrentAnimatorStateInfo(0).IsName("panzerwalk"))
					{
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
				}
				//DISPARO
				if(shoot2 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
				{
					v3 = Vector3.zero;

					caminar2 = false;
					animator.SetBool("normal", true);
					animator.SetBool("walk", false);
					animator.SetBool("walking", false);

					if(disparos2 >= 1)
					{
						disparos2 = 0;
					}else
					{
						disparos2 += 1;
						animator.SetBool("disparo", true);
					}

					shoot2 = false;
				}
				if(animator.GetCurrentAnimatorStateInfo(0).IsName("panzerShot"))
				{
					actuando2 = true;
				}else
				{
					actuando2 = false;
				}
				if(v3 != Vector3.zero)
				{
					GetComponent<Rigidbody>().velocity = (6 * v3.normalized);
				}
			}else
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

	IEnumerator esperaCuchillo()
	{
		yield return new WaitForSeconds(2);
		acuchillado = false;
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
		random = Random.Range(1,6);
		/*if(cubrirse)
		{
			random = Random.Range(6,10);
		}else
		{
			random = Random.Range(1,6);
		}*/
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
			disparar();
		}else if(random == 5)
		{
			random = 0;
			disparar();
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
		animator.SetBool("walk", true);
		animator.SetBool("walking", true);
		dist = Random.Range(0.5f,1.5f);
		StartCoroutine(esperaCamina());
		caminar = true;
	}
	//ALEJARCE
	void alejar()
	{
		animator.SetBool("walk", true);
		animator.SetBool("walking", true);
		dist = Random.Range(2,4);
		StartCoroutine(esperaAleja());
		alejarce = true;
	}
	//DISPARO
	void disparar()
	{
		//animator.SetBool("disparo", true);
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
		animator.SetBool("normal", true);
		animator.SetBool("walk", false);
		animator.SetBool("walking", false);
		alejarce = false;
		actuando = false;
	}
	IEnumerator esperaAleja2()
	{
		yield return new WaitForSeconds(dist2);
		animator.SetBool("normal", true);
		animator.SetBool("walk", false);
		animator.SetBool("walking", false);
		alejarce2 = false;
		actuando = false;
	}
	IEnumerator esperaCamina()
	{
		yield return new WaitForSeconds(dist);
		animator.SetBool("normal", true);
		animator.SetBool("walk", false);
		animator.SetBool("walking", false);
		caminar = false;
		actuando = false;
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
			animator.SetBool("walk", false);
			animator.SetBool("walking", false);
			caminar = false;
			alejarce = false;
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			caminar2 = false;
			alejarce2 = false;
			animator.SetInteger("cascado", 4);
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
			//caminar = true;
			random = Random.Range(0,4);
			animator.SetBool("walk", false);
			animator.SetBool("walking", false);
			caminar = false;
			alejarce = false;
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			caminar2 = false;
			alejarce2 = false;
			animator.SetInteger("cascado", 1);
			Destroy(col.gameObject);


			if(col.gameObject.GetComponent<balaOffline>())
			{
				salud -= col.gameObject.GetComponent<balaOffline>().poder;

				var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
				letras.GetComponent<TextMesh>().text = col.gameObject.GetComponent<balaOffline>().poder.ToString("F0");
			}else
			{
				salud -= col.gameObject.GetComponent<balaSniperOffline>().poder;

				var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
				letras.GetComponent<TextMesh>().text = col.gameObject.GetComponent<balaSniperOffline>().poder.ToString("F0");
			}

			if(PlayerPrefs.GetInt("violencia") == 1)
			{
				var explo = (GameObject)Instantiate(efectoSanre[Random.Range(0,efectoSanre.Length)], new Vector3(col.gameObject.transform.position.x,col.gameObject.transform.position.y-3, col.gameObject.transform.position.z-1), cascadoSpawn.rotation);
			}
		}

		if(col.gameObject.tag == "explo" && vivo)
		{
			random = Random.Range(0,4);
			animator.SetBool("walk",false);
			caminar = false;
			alejarce = false;
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			caminar2 = false;
			alejarce2 = false;

			if(PlayerPrefs.GetInt("violencia") == 1)
			{
				explocion = true;
			}

			salud -= col.gameObject.GetComponent<ExploOffline>().poder;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = col.gameObject.GetComponent<ExploOffline>().poder.ToString("F0");
		}
		if(col.gameObject.tag == "pared" && vivo)
		{
			alejarce = false;
			animator.SetBool("normal", true);
			animator.SetBool("walk", false);
			animator.SetBool("walking", false);
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
	}

	void OnCollisionStay (Collision col)
	{
		if(col.gameObject.tag == "pared" && vivo)
		{
			animator.SetBool("normal", true);
			animator.SetBool("walk", false);
			animator.SetBool("walking", false);
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
			animator.SetBool("walking",false);
			actuando2 = true;
		}

		if(col.gameObject.tag == "mira" && vivo)
		{
			mira.SetActive(true);
			mira.GetComponent<Animator>().SetBool("entry",true);
		}
		if(col.gameObject.tag == "cobertura" && vivo)
		{
			animator.SetBool("walk", false);
			animator.SetBool("walking", false);
			caminar = false;
			coverPosition = new Vector3(col.gameObject.transform.position.x, transform.position.y, col.gameObject.transform.position.z);
			cubrirse = true;
		}
		if(col.gameObject.tag == "balaLlamas" && vivo)
		{
			random = Random.Range(0,4);
			animator.SetBool("walk", false);
			animator.SetBool("walking", false);
			caminar = false;
			alejarce = false;
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			caminar2 = false;
			alejarce2 = false;
			animator.SetInteger("cascado", 1);
			Destroy(col.gameObject);

			quemado = true;
			salud -= 10;

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
				SaludSumar();
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
	
	public void SaludSumar()
	{
		suma = saludMax*2/80;
		salud += saludMax*2/80;
		//salud += 6;

		var letras = (GameObject)Instantiate(textos2, transform.position, Quaternion.Euler(0,0,0));
		letras.GetComponent<TextMesh>().text = "+"+suma.ToString("F0");
	}

	//APAGA LA LUZ
	IEnumerator apaga ()
	{
		yield return new WaitForSeconds(0.1f);
		luz.SetActive(false);
	}

	//EVENTOS SPINE
	public void cancel ()
	{
		shoot = false;
		actuando = false;
	}
	public GameObject HumoPanzer;
	public GameObject strikeescopeta;
	void rocket ()
	{
		luz.SetActive(true);
		StartCoroutine(apaga());

		if(gameObject.transform.localScale.x >= 1)
		{
			ShotD();
			var efect = (GameObject)Instantiate(strikeescopeta, bulletSpawn.transform.position, bulletSpawn.rotation);
		}else
		{
			ShotI();
			var efect = (GameObject)Instantiate(strikeescopeta, bulletSpawn.transform.position, Quaternion.Euler(0,0,bulletSpawn.rotation.z-180));
		}

		shoot = false;
		actuando = false;
	}

	
	public void ShotD()
	{
		var humo = (GameObject)Instantiate(HumoPanzer, new Vector3(bulletSpawn.position.x-4, bulletSpawn.position.y-0.5f, bulletSpawn.position.z), Quaternion.Euler(0,0,0));


		var granade = (GameObject)Instantiate(bulletPref, bulletSpawn.position, Quaternion.Euler(0,0,0));

		granade.GetComponent<balaPanzer>().poder = saludMax*granade.GetComponent<balaPanzer>().poder/80;
	}

	
	public void ShotI()
	{
		var humo = (GameObject)Instantiate(HumoPanzer, new Vector3(bulletSpawn.position.x+4, bulletSpawn.position.y-0.5f, bulletSpawn.position.z), Quaternion.Euler(0,180,0));


		var granade = (GameObject)Instantiate(bulletPref, bulletSpawn.position, Quaternion.Euler(0,180,0));

		granade.GetComponent<balaPanzer>().poder = saludMax*granade.GetComponent<balaPanzer>().poder/80;
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
	void muerto ()
	{
		if(gameObject.tag == "Player")
		{
			Panel.GetComponent<GameOffline>().KillsM += 1;
		}else
		{
			Panel.GetComponent<GameOffline>().KillsB += 1;
		}

		Destroy(gameObject);
	}
	void cartacrear()
	{
		if(crearCarta)
		{
			Carta();
		}
	}
	
	public void Carta()
	{
		var card = (GameObject)Instantiate(carta, bulletSpawn.position, bulletSpawn.rotation);
		card.GetComponent<Rigidbody>().velocity = card.transform.up * 30;
		//carta.GetComponent<Rigidbody>().AddForce(transform.right * -30);
	}
	public GameObject Arma;
	void tiraarma()
	{
		VotaArma();
	}

	
	public void VotaArma()
	{
		var arma = (GameObject)Instantiate(Arma, bulletSpawn.position, Quaternion.Euler(0,0,0)); 

		arma.GetComponent<Rigidbody>().velocity = arma.transform.up * 20;
		arma.GetComponent<Rigidbody>().velocity = arma.transform.right * 3;
		arma.GetComponent<Rigidbody>().AddTorque(transform.forward * 500 * 500);

		Destroy(arma, 2.0f);
	}
}
