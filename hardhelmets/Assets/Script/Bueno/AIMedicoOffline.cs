using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMedicoOffline : MonoBehaviour {

	public bool pelea;
	public string BuscarPlayer;
	public string BuscarBase;
	public string NameEnemy;
	public string NameEnemyTank;

	public Transform Player;

	public Transform BASE;

	public int Tipo;

	public bool crearCarta;
	public GameObject carta;

	public Transform target;

	public Animator animator;

	public string _currentDirection = "right";

	public int voltear;

	public bool vivo = true;

	public float salud;

	public float saludMax;

	//FUNCIONES
	//bool actuando2;
	//bool caminar2;
	//bool shoot2;
	//bool cargar2;
	//int disparos2;

	//GROUND CHECHER
	public Transform groundCheck;
	float groundRadius = 0.3f;
	public LayerMask whatIsGround;
	public bool grounded = false;

	//SEGUNDO SCRIPT
	//OBJETOS DE DISPARO
	public GameObject bulletPrefFusil;

	public Transform bulletSpawn;
	public GameObject casquilloPref;
	public Transform casquilloSpawn;
	public GameObject luz;

	int azar;

	public Transform cascadoSpawn;

	//FUNCIONES ALEATORIAS
	bool actuando;

	bool cubrirse;
	bool cubierto;
	bool shoot;
	int disparos;
	public bool alejarce;
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

	//NIVEL DE CARTA
	public int level;
	public GameObject Jugador;

	public GameObject particulas;
	public string amigo;
	public bool curar;

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

		animator.SetBool("paracaidas", true);

		animator.SetBool("falling", true);

		shoot = false;

		_currentDirection = "right";

		lejos = Random.Range(8,25);
		muerte = Random.Range(1,3);
	}

	// Update is called once per frame
	public void Update ()
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
			alejarce = false;
			caminar = false;
			animator.SetInteger("disparo", 0);
			animator.SetBool("walk", false);
			animator.SetBool("grounded", true);
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
			alejarce = false;
		}
		/*if(Player == null)
		{
			pelea = false;
		}*/
		//VOLTEA PERSONAJE
		gameObject.transform.localScale = new Vector3(voltear,1,1);

		if(grounded && BASE == null)
		{
			BASE = GameObject.FindWithTag(BuscarBase).transform;
		}

		if(vivo && !animator.GetBool("paracaidas"))
		{
			if(seguir.GetComponent<vista>().arriba)
			{
				v3 += Vector3.forward;
			}else if(seguir.GetComponent<vista>().abajo)
			{
				v3 += Vector3.back;
			}/*else if(caminar || alejarce)
			{
				v3 = Vector3.zero;
			}*/

			if(acuchillado)
			{
				StartCoroutine(esperaCuchillo());
			}
			if(grounded && target == null)
			{
				target = GameObject.Find(BuscarPlayer).transform;
			}

			if(v3 != Vector3.zero)
			{
				GetComponent<Rigidbody>().velocity = (6 * v3.normalized);
			}

			if(curar && amigo != null)
			{
				Cura();
				StartCoroutine(esperaCura());
				curar = false;
			}

			if(pelea)
			{
				if(!alejarce)
				{
					//MIRA AL OBJKETIVO
					Vector3 v3Dir = Player.position - transform.position;
					float angle = Mathf.Atan2(0, v3Dir.x) * Mathf.Rad2Deg;

					if(angle == 180)
					{
						_currentDirection = "left";
						voltear = -1;
					}else
					{
						_currentDirection = "right";
						voltear = 1;
					}
				}

				if(Player.tag != NameEnemyTank)
				{
					if(animator.GetCurrentAnimatorStateInfo(0).IsName("walk"))
					{
						animator.SetBool("walk", false);
					}

					if(Mathf.Abs((transform.position - Player.position).x) >= 8.01f && Mathf.Abs((transform.position - Player.position).x) <= 38f && !actuando && !alejarce && !alejarce && !animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
					{
						Funciones();
					}

					if(Mathf.Abs((transform.position - Player.position).x) <= 8 && !cubrirse && !alejarce && !animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
					{
						caminar = false;
						shoot = false;

						v3 = Vector3.zero;

						dist2 = Random.Range(2,4);
						StartCoroutine(esperaAleja());
						actuando = true;
						alejarce = true;
					}																													
				}else
				{
					if(Mathf.Abs((transform.position - Player.position).x) >= 25.01f && !shoot && !actuando && !alejarce && !alejarce && !animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
					{
						FuncionesTanque();
					}
					if(Mathf.Abs((transform.position - Player.position).x) <= 25 && !cubrirse && !alejarce && !animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
					{
						caminar = false;
						shoot = false;

						v3 = Vector3.zero;

						dist2 = Random.Range(2,4);
						StartCoroutine(esperaAleja());
						actuando = true;
						alejarce = true;
					}	
				}
			}else
			{
				if(target != null)
				{
					if(!alejarce)
					{
						//MIRA AL OBJKETIVO
						Vector3 v3Dir = target.position - transform.position;
						float angle = Mathf.Atan2(0, v3Dir.x) * Mathf.Rad2Deg;

						if(angle == 180)
						{
							_currentDirection = "left";
							voltear = -1;
						}else
						{
							_currentDirection = "right";
							voltear = 1;
						}
					}
					//SI ESTA MUY LEJOS
					if(Mathf.Abs((transform.position - target.position).x) > lejos)
					{
						//actuando2 = false;
						caminar = true;
					}
					if(Mathf.Abs((transform.position - target.position).x) <= lejos)
					{
						v3 = Vector3.zero;

						caminar = false;
						animator.SetBool("walk",false);
						//actuando2 = true;
					}
				}

				if(BASE != null)
				{
					if(Mathf.Abs((transform.position - BASE.position).x) <= lejos && !actuando)
					{
						v3 = Vector3.zero;

						actuando = true;
						disparar();
					}
				}
			}

			//CAMINAR
			if(caminar && !devolver)
			{
				animator.SetInteger("descanso",0);
				animator.SetBool("walk",true);

				if(_currentDirection == "right")
				{
					v3 += Vector3.right;
				}else
				{
					v3 += Vector3.left;
				}
			}
			if(alejarce)
			{
				shoot = false;
				animator.SetBool("walk",true);
				if(_currentDirection == "right")
				{
					v3 += Vector3.left;
					voltear = -1;
				}else
				{
					v3 += Vector3.right;
					voltear = 1;
				}
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
		}
	}
	IEnumerator esperaCura()
	{
		yield return new WaitForSeconds(9f);
		if(amigo != null)
		{
			curar = true;
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
		v3 = Vector3.zero;
		animator.SetBool("walk",false);
		alejarce = false;
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
			alejarce = false;
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
			animator.SetInteger("cascado", 1);


			salud -= col.gameObject.GetComponent<balaOffline>().poder;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = col.gameObject.GetComponent<balaOffline>().poder.ToString("F0");

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

			salud -= col.gameObject.GetComponent<ExploOffline>().poder;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = col.gameObject.GetComponent<ExploOffline>().poder.ToString("F0");

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
			}else
			{
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
		if(col.gameObject.tag == amigo && vivo)
		{
			curar = true;
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

	//APAGA LA LUZ
	IEnumerator apaga ()
	{
		yield return new WaitForSeconds(0.1f);
		luz.SetActive(false);
	}

	
	public void Cura ()
	{
		var cura = (GameObject)Instantiate(particulas,transform.position, Quaternion.Euler(0,0,0));
	}
	//EVENTOS SPINE
	void ShotA ()//FUSIL
	{
		luz.SetActive(true);
		StartCoroutine(apaga());

		if(_currentDirection == "right")
		{
			ShotAD();
		}else
		{
			ShotAI();
		}

		StartCoroutine(esperaActuar());
	}
	
	public void ShotAD()
	{
		var bullet = (GameObject)Instantiate(bulletPrefFusil, bulletSpawn.position, bulletSpawn.rotation);//Quaternion.Euler(bulletSpawn.rotation.eulerAngles.z, bulletSpawn.rotation.eulerAngles.y, bulletSpawn.rotation.eulerAngles.z));
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.right * 100;

		bullet.GetComponent<balaOffline>().poder = saludMax*bullet.GetComponent<balaOffline>().poder/80;
		Destroy(bullet, 5.0f);
		//CASQUILLOS
		var casquillo = (GameObject)Instantiate(casquilloPref, casquilloSpawn.position, casquilloSpawn.rotation); 
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.right * Random.Range(-10,-50);
		casquillo.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(-0.0f,-0.2f));
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.up * Random.Range(-1,-6);

	}

	
	public void ShotAI()
	{
		var bullet = (GameObject)Instantiate(bulletPrefFusil, bulletSpawn.position, Quaternion.Euler(0,180,0));
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.right * 100;

		bullet.GetComponent<balaOffline>().poder = saludMax*bullet.GetComponent<balaOffline>().poder/80;
		Destroy(bullet, 5.0f);
		//CASQUILLOS
		var casquillo = (GameObject)Instantiate(casquilloPref, casquilloSpawn.position, casquilloSpawn.rotation); 
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.right * Random.Range(-10,-50);
		casquillo.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(-0.0f,-0.2f));
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.up * Random.Range(-1,-6);
	}

	void load2 ()
	{
		disparos = 0;
		actuando = false;
		shoot = false;
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
