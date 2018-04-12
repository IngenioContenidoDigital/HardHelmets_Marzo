using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMetra : MonoBehaviour {

	public string enemyName;
	public string tankName;
	Transform Player;

	bool crearCarta;
	public GameObject carta;

	public string _currentDirection = "right";

	public int voltear;

	//GROUND CHECHER
	public Transform groundCheck;
	float groundRadius = 0.3f;
	public LayerMask whatIsGround;
	public bool grounded = false;

	//OBJETOS DE DISPARO
	public GameObject luz;

	public Transform cascadoSpawn;

	public GameObject sangre;
	public GameObject polv;

	public Animator animator;
	int azar;

	public bool vivo = true;

	public float salud;

	public float saludMax;

	public int disp;

	//float inicial;
	public Transform bulletSpawn;
	public GameObject bala1;
	//CAMARA LENTA
	int matrix;
	bool matrix2;

	public GameObject textos;
	public GameObject textos2;
	public GameObject particulasCurar;

	public GameObject mira;

	//MUERE CON EXPLOCION
	bool explocion;
	public GameObject Huesos;
	public GameObject[] sangreCuchillo;

	public GameObject[] efectoSanre;

	//ACUCHILLADO
	bool acuchillado;

	public GameObject casquilloPref;
	public Transform casquilloSpawn;

	//ORDEN DE CAMINAR
	Vector3 v3;
	public bool caminar;
	public Vector3 lugar;

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
		matrix = Random.Range(1,6);
		//Player = GameObject.FindWithTag ("Player").transform;
		_currentDirection = "right";
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

			animator.SetInteger("disparo", 0);
			animator.SetBool("walk", false);
			animator.SetBool("grounded", true);
			caminar = false;
			animator.SetBool("caminar", false);
		}
		if(salud >= saludMax)
		{
			salud = saludMax;
		}

		grounded = Physics.CheckSphere(groundCheck.position, groundRadius, whatIsGround);
		animator.SetBool("grounded", grounded);

		if(animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
		{
			animator.SetInteger("cascado", 0);
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("KillBackJump") || animator.GetCurrentAnimatorStateInfo(0).IsName("KillSimple") ||animator.GetCurrentAnimatorStateInfo(0).IsName("KillSimple2"))
		{
			animator.SetInteger("muerte", 0);
		}
		//VOLTEA PERSONAJE
		gameObject.transform.localScale = new Vector3(voltear,1,1);

		if(vivo)
		{
			if(acuchillado)
			{
				StartCoroutine(esperaCuchillo());
			}

			if(v3 != Vector3.zero)
			{
				GetComponent<Rigidbody>().velocity = (6 * v3.normalized);
			}

			if(caminar)
			{
				if(transform.position.x < lugar.x)
				{
					if(voltear == -1)
					{
						v3 = Vector3.zero;
					}else
					{
						v3 += Vector3.right;
					}

					//_currentDirection = "right";
					voltear = 1;
				}else
				{
					if(voltear == 1)
					{
						v3 = Vector3.zero;
					}else
					{
						v3 += Vector3.left;
					}

					//_currentDirection = "left";
					voltear = -1;
				}

				if(transform.position.z < lugar.z)
				{
					v3 += Vector3.forward;
				}else
				{
					v3 += Vector3.back;
				}

				if(Mathf.Abs((transform.position - lugar).x) <= 2 && Mathf.Abs((transform.position - lugar).z) <= 2)//(transform.position.x >= lugar.x-3 && transform.position.x <= lugar.x+3 && transform.position.z >= lugar.z-2 && transform.position.z <= lugar.z+2)
				{
					print("ESTOY EN EL PUNTO");
					animator.SetBool("caminar", false);
					v3 = Vector3.zero;
					caminar = false;
				}
			}
			if(Player != null && !caminar)
			{
				//MIRA AL JUGADOR
				Vector3 v3Dir = Player.position - transform.position;
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

				if(Vector3.Distance(transform.position,Player.position) >= 1)//5.1f) && Vector3.Distance(transform.position,Player.position) <= 30f)
				{
					animator.SetInteger("disparo", 1);
				}else if(Vector3.Distance(transform.position,Player.position) <= 5)// && ajusteZ.z != Player.transform.position.z)
				{
					//Hero.cuchillo = true;
				}else
				{
					animator.SetInteger("disparo", -1);
				}
			}else
			{
				//v3 = Vector3.zero;
				animator.SetInteger("disparo", -1);
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
		}else
		{
			mira.SetActive(false);
			/*if(matrix == 1 && !matrix2 && Player.name == "Hero")
			{
				//Manager.lenta = true;
			}*/
			gameObject.layer = LayerMask.NameToLayer("muerto");
			Base.layer = LayerMask.NameToLayer("mira");
			//gameObject.tag = "Untagged";
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

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "cuchillo" && vivo)
		{
			v3 = Vector3.zero;
			caminar = false;
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
			v3 = Vector3.zero;
			caminar = false;
			animator.SetInteger("cascado", 1);
			Destroy(col.gameObject);

			salud -= col.gameObject.GetComponent<balaOffline>().poder;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = col.gameObject.GetComponent<balaOffline>().poder.ToString("F0");

			if(PlayerPrefs.GetInt("violencia") == 1)
			{
				var explo = (GameObject)Instantiate(efectoSanre[Random.Range(0,efectoSanre.Length)], new Vector3(col.gameObject.transform.position.x,col.gameObject.transform.position.y-3, col.gameObject.transform.position.z-1), cascadoSpawn.rotation);
			}
		}
		/*if(col.gameObject.tag == "balaFusil" && vivo)
		{
			v3 = Vector3.zero;
			caminar = false;
			animator.SetInteger("cascado", 1);
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
			v3 = Vector3.zero;
			caminar = false;
			animator.SetInteger("cascado", 1);
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
			v3 = Vector3.zero;
			caminar = false;
			animator.SetInteger("cascado", 1);
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
			v3 = Vector3.zero;
			caminar = false;
			animator.SetInteger("cascado", 1);
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
			v3 = Vector3.zero;
			caminar = false;
			animator.SetInteger("cascado", 1);
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
			v3 = Vector3.zero;
			caminar = false;
			animator.SetInteger("cascado", 1);
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
			v3 = Vector3.zero;
			caminar = false;
			if(PlayerPrefs.GetInt("violencia") == 1)
			{
				explocion = true;
			}

			salud -= col.gameObject.GetComponent<ExploOffline>().poder;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = col.gameObject.GetComponent<ExploOffline>().poder.ToString("F0");
		}
		if(col.gameObject.tag == tankName && vivo)
		{
			v3 = Vector3.zero;
			print(col.gameObject.GetComponent<Rigidbody>().velocity.x);
			if(col.gameObject.GetComponent<Rigidbody>().velocity.x > 2.5f)
			{
				caminar = false;
				salud -= 100;

				var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
				letras.GetComponent<TextMesh>().text = "100";

				if(PlayerPrefs.GetInt("violencia") == 1)
				{
					if(PlayerPrefs.GetInt("violencia") == 1)
					{
						var explo = (GameObject)Instantiate(efectoSanre[Random.Range(0,efectoSanre.Length)], new Vector3(col.gameObject.transform.position.x,col.gameObject.transform.position.y-3, col.gameObject.transform.position.z-1), cascadoSpawn.rotation);
					}
				}
			}
		}
		if(col.gameObject.tag == "pared" && vivo || col.gameObject.tag == "Frente" && vivo)
		{
			caminar = false;
			v3 = Vector3.zero;
			animator.SetBool("caminar", false);
		}
		if(col.gameObject.tag == "obstaculo1" || col.gameObject.tag == "obstaculo2" || col.gameObject.name == "PuazD")
		{
			caminar = false;
			v3 = Vector3.zero;
			animator.SetBool("caminar", false);
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
		if(col.gameObject.tag == enemyName)
		{
			Player = col.gameObject.transform;
		}
		if(col.gameObject.tag == tankName)
		{
			Player = col.gameObject.transform;
		}
		if(col.gameObject.tag == "balaLlamas" && vivo)
		{
			quemado = true;
			caminar = false;

			salud -= 3;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = "3";
		}
		if(col.gameObject.tag == medic)
		{
			if(salud < saludMax)
			{
				SaludSumar();
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
		if(col.gameObject.tag == enemyName)
		{
			Player = null;
		}
		if(col.gameObject.tag == tankName)
		{
			Player = null;
		}
	}

	IEnumerator esperaCuchillo()
	{
		yield return new WaitForSeconds(2);
		acuchillado = false;
	}

	//APAGA LA LUZ
	IEnumerator apaga ()
	{
		yield return new WaitForSeconds(0.1f);
		luz.SetActive(false);
	}

	float suma;
	
	public void SaludSumar()
	{
		suma = saludMax*5/80;
		salud += saludMax*5/80;
		//salud += 6;

		var letras = (GameObject)Instantiate(textos2, transform.position, Quaternion.Euler(0,0,0));
		letras.GetComponent<TextMesh>().text = suma.ToString("F0");

		var part = (GameObject)Instantiate(particulasCurar, transform.position, Quaternion.Euler(0,0,0));
	}

	//EVENTOS SPINE
	void mg1 ()
	{
		luz.SetActive(true);
		StartCoroutine(apaga());

		if(transform.localScale.x == 1)
		{
			ShotD();
		}else
		{
			ShotI();
		}

		/*inicial = Player.transform.position.x;
		if(gameObject.transform.localScale.x >= 1)
		{
			Instantiate(bala1, new Vector3(inicial-10, transform.position.y, Player.transform.position.z), transform.rotation);//inicial+10
		}else
		{
			Instantiate(bala1, new Vector3(inicial+10, transform.position.y, Player.transform.position.z), transform.rotation);//inicial+10
		}*/
	}
	
	public void ShotD()
	{

		var bullet = (GameObject)Instantiate(bala1, bulletSpawn.position, Quaternion.Euler(0,0,0));
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.right * 100;
		Destroy(bullet, 5.0f);
		//CASQUILLOS
		var casquillo = (GameObject)Instantiate(casquilloPref, casquilloSpawn.position, casquilloSpawn.rotation); 
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.right * Random.Range(-10,-50);
		casquillo.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(-0.0f,-0.2f));
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.up * Random.Range(-1,-6);
	}

	
	public void ShotI()
	{
		var bullet = (GameObject)Instantiate(bala1, bulletSpawn.position, Quaternion.Euler(0,180,0));
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.right * 100;

		bullet.GetComponent<balaOffline>().poder = saludMax*bullet.GetComponent<balaOffline>().poder/80;
		Destroy(bullet, 5.0f);
		//CASQUILLOS
		var casquillo = (GameObject)Instantiate(casquilloPref, casquilloSpawn.position, casquilloSpawn.rotation); 
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.right * Random.Range(-10,-50);
		casquillo.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(-0.0f,-0.2f));
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.up * Random.Range(-1,-6);

	}
	void mg2 ()
	{
		mg1();
		/*if(gameObject.transform.localScale.x >= 1)
		{
			Instantiate(bala2, new Vector3(inicial-8, transform.position.y, Player.transform.position.z), transform.rotation);//inicial+8
		}else
		{
			Instantiate(bala2, new Vector3(inicial+8, transform.position.y, Player.transform.position.z), transform.rotation);//inicial+8
		}*/
	}
	void mg3 ()
	{
		mg1();
		/*if(gameObject.transform.localScale.x >= 1)
		{
			Instantiate(bala1, new Vector3(inicial-6, transform.position.y, Player.transform.position.z), transform.rotation);//inicial+6
		}else
		{
			Instantiate(bala1, new Vector3(inicial+6, transform.position.y, Player.transform.position.z), transform.rotation);//inicial+6
		}*/
	}
	void mg4 ()
	{
		mg1();
		/*if(gameObject.transform.localScale.x >= 1)
		{
			Instantiate(bala2, new Vector3(inicial-4, transform.position.y, Player.transform.position.z), transform.rotation);//inicial+4
		}else
		{
			Instantiate(bala2, new Vector3(inicial+4, transform.position.y, Player.transform.position.z), transform.rotation);//inicial+4
		}*/
	}
	void mg5 ()
	{
		mg1();
		/*if(gameObject.transform.localScale.x >= 1)
		{
			Instantiate(bala1, new Vector3(inicial-2, transform.position.y, Player.transform.position.z), transform.rotation);//inicial+2
		}else
		{
			Instantiate(bala1, new Vector3(inicial+2, transform.position.y, Player.transform.position.z), transform.rotation);//inicial+2
		}*/
	}
	void mg6 ()
	{
		mg1();
		/*if(gameObject.transform.localScale.x >= 1)
		{
			Instantiate(bala2, new Vector3(inicial, transform.position.y, Player.transform.position.z), transform.rotation);//inicial
		}else
		{
			Instantiate(bala2, new Vector3(inicial, transform.position.y, Player.transform.position.z), transform.rotation);//inicial
		}*/
	}
	void blood ()
	{
		Instantiate(sangre, transform.position, transform.rotation);
	}
	void polvo ()
	{
		var efect = (GameObject)Instantiate(polv, transform.position, transform.rotation);
		Destroy(efect, 1.0f);
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
