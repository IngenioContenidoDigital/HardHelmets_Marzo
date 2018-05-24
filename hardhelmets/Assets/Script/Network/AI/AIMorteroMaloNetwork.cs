using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AI;

public class AIMorteroMaloNetwork : NetworkBehaviour {

	public NavMeshAgent agent;

	public string enemyName;
	public string tankName;
	public Transform Player;

	bool crearCarta;
	public GameObject carta;

	public string _currentDirection = "right";

	[SyncVar(hook = "FacingCallback")]
	public int voltear;

	//GROUND CHECHER
	public Transform groundCheck;
	float groundRadius = 0.3f;
	public LayerMask whatIsGround;
	public bool grounded = false;

	//OBJETOS DE DISPARO
	public Transform bulletSpawn;
	public GameObject luz;

	public Transform cascadoSpawn;

	public GameObject carne;
	public GameObject sangre;
	public GameObject polv;

	public Animator animator;
	int azar;

	public bool vivo = true;

	[SyncVar]
	public float salud;
	[SyncVar]
	public float saludMax;

	public int disp;
	bool shoot;
	bool contar;

	public GameObject bala;
	//CAMARA LENTA
	int matrix;
	bool matrix2;

	public GameObject textos;
	public GameObject textos2;

	public GameObject mira;

	//MUERE CON EXPLOCION
	bool explocion;
	public GameObject Huesos;
	public GameObject[] sangreCuchillo;

	public GameObject[] efectoSanre;

	//ACUCHILLADO
	bool acuchillado;

	//ORDEN DE CAMINAR
	public bool caminar;
	public Vector3 lugar;

	GameObject Jugador;

	public GameObject Base;

	//PANEL PARTIDA
	GameObject Panel;

	//CURACION
	public string medic;

	//NIVEL DE CARTA
	public int level;
	public GameObject Jugador2;

	// Use this for initialization
	void Start ()
	{
		if(!isServer)
		{
			return;
		}

		if(gameObject.tag == "Player")
		{
			Jugador2 = GameObject.Find("Hero");
		}else
		{
			Jugador2 = GameObject.Find("Hero2");
		}

		level = Jugador2.GetComponent<HeroNetwork>().level;

		saludMax = 3.4f*level+80;
		salud = saludMax;

		mira.SetActive(false);

		animator.SetBool("paracaidas", true);

		matrix = Random.Range(1,6);
		//Player = GameObject.FindWithTag ("Player").transform;
		_currentDirection = "right";
	}
	bool ponermascara;
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

			animator.SetInteger("disparo", 0);
			animator.SetBool("walk", false);
			animator.SetBool("grounded", true);
			caminar = false;
			animator.SetBool("caminar", false);
			if(Panel.GetComponent<Game>().continuar)
			{
				print("DESTRUIR OBJETOS");
				CmdDestruir();
			}
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

		if(salud >= saludMax)
		{
			salud = saludMax;
		}

		if(vivo && !animator.GetBool("paracaidas"))
		{
			if(!ponermascara)
			{
				if(gameObject.tag == "Player")
				{
					gameObject.layer = LayerMask.NameToLayer("Player");
					CmdChangeMascara("Player");
				}else
				{
					gameObject.layer = LayerMask.NameToLayer("Enemy");
					CmdChangeMascara("Enemy");
				}
				ponermascara = true;
			}

			if(acuchillado)
			{
				StartCoroutine(esperaCuchillo());
			}

			if(caminar)
			{
				animator.SetBool("caminar", true);

				agent.isStopped = false;
				agent.SetDestination(lugar);

				if(transform.position.x >= lugar.x-3 && transform.position.x <= lugar.x+3)
				{
					animator.SetBool("caminar", false);
					caminar = false;
				}
			}else
			{
				animator.SetBool("caminar", false);
				agent.isStopped = true;
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
				//SI ESTA LEJOS
				if(Vector3.Distance(transform.position,Player.position) >= 2 && disp < 1)//8f && Vector3.Distance(transform.position,Player.position) <= 30f && disp < 1)
				{
					shoot = true;
				}
			}

			//SHOT
			if(shoot)
			{
				animator.SetInteger("disparo", 1);
				shoot = false;
			}
			if(disp >= 4)
			{
				animator.SetInteger("disparo", 0);
				StartCoroutine(tiempo());
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
					caminar = false;
					animator.SetBool("muerto", true);
					animator.SetInteger("muerte", 20);
				}else
				{
					animator.SetBool("muerto", true);
					int azar = Random.Range(1,3);
					animator.SetInteger("muerte", azar);
				}

				gameObject.layer = LayerMask.NameToLayer("muerto");
				Base.layer = LayerMask.NameToLayer("mira");
				CmdChangeMascara("muerto");

				StartCoroutine(muertee());

				vivo = false;
			}else
			{
				explocion = false;
			}
		}else
		{
			agent.isStopped = true;
			mira.SetActive(false);
			Base.layer = LayerMask.NameToLayer("mira");
			if(ponermascara)
			{
				gameObject.layer = LayerMask.NameToLayer("muerto");
				CmdChangeMascara("muerto");

				ponermascara = false;
			}
			//gameObject.tag = "Untagged";
		}

		if(Jugador == null)
		{
			Jugador = GameObject.Find("Hero2");
		}else if(Jugador.GetComponent<HeroNetwork>().orden && Jugador.GetComponent<HeroNetwork>().muneco == this.gameObject)
		{
			lugar = Jugador.GetComponent<HeroNetwork>().ordenLugar;
			caminar = true;
		}
	}

	[Command]
	public void CmdChangeMascara(string newMascara)
	{
		RpcChangeMascara(newMascara);
	}
	[ClientRpc]
	public void RpcChangeMascara (string newMascara)
	{
		gameObject.layer = LayerMask.NameToLayer(newMascara);
	}

	IEnumerator muertee ()
	{
		yield return new WaitForSeconds(8f);
		Destroy(gameObject);
	}

	IEnumerator esperaCuchillo()
	{
		yield return new WaitForSeconds(2);
		acuchillado = false;
	}

	IEnumerator tiempo ()
	{
		yield return new WaitForSeconds(Random.Range(3,10));
		disp = 0;
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
			caminar = false;
			animator.SetInteger("cascado", 1);
			Destroy(col.gameObject);

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

			if(PlayerPrefs.GetInt("violencia") == 1)
			{
				var explo = (GameObject)Instantiate(efectoSanre[Random.Range(0,efectoSanre.Length)], new Vector3(col.gameObject.transform.position.x,col.gameObject.transform.position.y-3, col.gameObject.transform.position.z-1), cascadoSpawn.rotation);
			}
		}

		if(col.gameObject.tag == "explo" && vivo)
		{
			caminar = false;
			if(PlayerPrefs.GetInt("violencia") == 1)
			{
				explocion = true;
			}

			salud -= col.gameObject.GetComponent<Explo>().poder;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = col.gameObject.GetComponent<Explo>().poder.ToString("F0");
		}
		if(col.gameObject.tag == tankName && vivo)
		{
			caminar = false;
			print(col.gameObject.GetComponent<Rigidbody>().velocity.x);
			if(col.gameObject.GetComponent<Rigidbody>().velocity.x > 2.5f)
			{
				salud -= 100;

				var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
				letras.GetComponent<TextMesh>().text = "100";

				if(PlayerPrefs.GetInt("violencia") == 1)
				{
					var explo = (GameObject)Instantiate(efectoSanre[Random.Range(0,efectoSanre.Length)], col.gameObject.transform.position, cascadoSpawn.rotation);
				}
			}
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
			caminar = false;
			animator.SetInteger("cascado", 1);
			Destroy(col.gameObject);

			quemado = true;

			salud -= 3;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = "3";
		}
		if(col.gameObject.tag == medic)
		{
			if(salud < saludMax)
			{
				CmdSaludSumar();
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

	//public Transform piso;

	//EVENTOS SPINE
	void mortero()
	{
		disp += 1;
		CmdShot();
		//Instantiate(bala, new Vector3(Player.transform.position.x+Random.Range(10,-10), transform.position.y+0.2f, Player.transform.position.z+Random.Range(10,-10)), transform.rotation);
	}
	[Command]
	public void CmdShot()
	{
		var bullet = (GameObject)Instantiate(bala, new Vector3(Player.transform.position.x+Random.Range(10,-10), transform.position.y+0.2f, Player.transform.position.z+Random.Range(10,-10)), transform.rotation);
		NetworkServer.Spawn(bullet);
		bullet.GetComponent<balaMorteroNetwork>().poder = saludMax*bullet.GetComponent<balaMorteroNetwork>().poder/80;
	}
	//EVENTOS SPINE
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
