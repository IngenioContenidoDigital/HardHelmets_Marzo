using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPanzerNew : MonoBehaviour {

	public NavMeshAgent agent;

	public bool vivo = true;

	public float salud;

	public float saludMax;

	public int distancia;
	public int minima;
	public int maxima;

	public int distanciaZ;

	public string BaseBuena;
	public string BaseMala;
	public string BuscarBase;
	public string NameEnemy;
	public string NameEnemyTank;

	public bool crearCarta;
	public GameObject carta;

	public Transform target;

	public Animator animator;

	public string _currentDirection = "right";

	public int voltear;

	//GROUND CHECHER
	public Transform groundCheck;
	float groundRadius = 0.3f;
	public LayerMask whatIsGround;
	public bool grounded = false;

	public bool disparando;
	public int disparos;

	public GameObject textos;
	public GameObject textos2;

	public GameObject mira;
	//NIVEL DE CARTA
	public int level;
	public GameObject Jugador;

	//PANEL PARTIDA
	GameObject Panel;

	//OBJETOS DE DISPARO
	public GameObject bulletPref;

	public Transform bulletSpawn;
	//LUZ DE ARMAS
	public GameObject luz;

	bool explocion;
	bool acuchillado;
	public GameObject Huesos;
	public GameObject[] sangreCuchillo;

	public Transform cascadoSpawn;

	public GameObject Base;

	public bool water;

	// Use this for initialization
	void Start ()
	{
		distancia = Random.Range(minima,maxima);//20-30
		distanciaZ = Random.Range(0,8);

		Jugador = GameObject.Find("Hero");

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

			animator.SetBool("disparo", false);
			animator.SetBool("walk", false);
			animator.SetBool("grounded", true);
		}
		if(salud >= saludMax)
		{
			salud = saludMax;
		}
		//CHECA SI ESTA EN EL PISO
		grounded = Physics.CheckSphere(groundCheck.position, groundRadius, whatIsGround);
		animator.SetBool("grounded", grounded);

		//VOLTEA PERSONAJE
		gameObject.transform.localScale = new Vector3(voltear,1,1);

		//ANIMACION CAYENDO
		if(GetComponent<Rigidbody>().velocity.y < -4f)
		{
			animator.SetBool("falling", true);
		}else
		{
			animator.SetBool("falling", false);
		}
		if(vivo)
		{
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
				}else
				{
					_currentDirection = "right";
					voltear = 1;
				}

				if(Mathf.Abs((transform.position - target.position).x) >= distancia && !animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
				{
					agent.isStopped = false;
					agent.SetDestination(target.position);
				}
				if(Mathf.Abs((transform.position - target.position).x) < distancia)
				{
					if(Mathf.Abs((transform.position - target.position).z) <= distanciaZ)
					{
						if(target.tag == BuscarBase) // BASE ENEMIGA
						{
							if(!disparando)
							{
								disparando = true;

								disparos += 1;
								animator.SetBool("disparo", true);

								StartCoroutine(esperaDisparo());
							}
							agent.isStopped = true;
						}else if(target.tag == BaseMala || target.tag == "newtra") // BASES NEUTRAS
						{
							agent.isStopped = true;
						}else // ENEMIGO
						{
							if(!disparando)
							{
								disparando = true;

								disparos += 1;
								animator.SetBool("disparo", true);

								StartCoroutine(esperaDisparo());
							}
							agent.isStopped = true;
						}

					}else if(!animator.GetBool("recargando"))
					{
						agent.isStopped = false;
						agent.SetDestination(new Vector3(transform.position.x+Random.Range(-8,8), transform.position.y, target.position.z));
					}
				}
				if(target.tag == NameEnemy || target.tag == NameEnemyTank)
				{
					if(target.GetComponent<Hero>() != null)
					{
						if(target.GetComponent<Hero>().salud <= 0)
						{
							target = null;
							agent.isStopped = true;
						}
					}
					if(target.GetComponent<AI>() != null)
					{
						if(target.GetComponent<AI>().salud <= 0)
						{
							target = null;
							agent.isStopped = true;
						}
					}
				}
			}
			if(!agent.isStopped)
			{
				animator.SetBool("walk", true);
			}else
			{
				animator.SetBool("walk", false);
			}

			if(salud <= 0)
			{
				int muerteg = Random.Range(11,14);
				if(explocion)
				{
					animator.SetBool("muerto", true);
					animator.SetInteger("muerte", muerteg);
					var bones = (GameObject)Instantiate(Huesos, cascadoSpawn.position, transform.rotation);
					explocion = false;
				}else if(acuchillado)
				{
					animator.SetBool("acuchillado", true);
					animator.SetInteger("muerte", Random.Range(1,3));
					acuchillado = false;
				}else if(quemado)
				{
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

				vivo = false;

				agent.isStopped = true;

				StartCoroutine(muertee());
			}else
			{
				explocion = false;
			}

		}else
		{
			mira.SetActive(false);
			gameObject.layer = LayerMask.NameToLayer("muerto");
		}
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

		if(col.gameObject.tag == "cuchillo" && vivo)
		{
			agent.isStopped = false;

			GetComponent<Rigidbody>().velocity = Vector3.zero;
			animator.SetBool("walk",false);

			animator.SetInteger("cascado", 4);

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
			agent.isStopped = false;

			animator.SetBool("walk",false);

			animator.SetInteger("cascado", 1);

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
				var sangre2 = (GameObject)Instantiate(sangreCuchillo[Random.Range(0,sangreCuchillo.Length)], cascadoSpawn.position, cascadoSpawn.rotation);
			}
		}
		if(col.gameObject.tag == "explo" && vivo)
		{
			agent.isStopped = false;

			animator.SetBool("walk",false);

			salud -= col.gameObject.GetComponent<ExploOffline>().poder;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = col.gameObject.GetComponent<ExploOffline>().poder.ToString("F0");

			if(PlayerPrefs.GetInt("violencia") == 1)
			{
				explocion = true;
			}
		}

		if(col.gameObject.tag == NameEnemyTank && vivo)
		{
			if(col.gameObject.GetComponent<Rigidbody>().velocity.x > 2.5f)
			{
				salud -= 100;

				var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
				letras.GetComponent<TextMesh>().text = "100";

				if(PlayerPrefs.GetInt("violencia") == 1)
				{
					var sangre2 = (GameObject)Instantiate(sangreCuchillo[Random.Range(0,sangreCuchillo.Length)], cascadoSpawn.position, cascadoSpawn.rotation); 
				}
			}
		}
	}

	bool quemado;
	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "newtra" && vivo || col.gameObject.tag == BaseMala && vivo)
		{
			target = col.gameObject.transform;
			agent.isStopped = true;
			animator.SetBool("walk",false);
		}

		if(col.gameObject.tag == "mira" && vivo)
		{
			mira.SetActive(true);
			mira.GetComponent<Animator>().SetBool("entry",true);
		}

		if(col.gameObject.tag == "balaLlamas" && vivo)
		{
			animator.SetBool("walk",false);

			animator.SetInteger("cascado", 1);
			Destroy(col.gameObject);

			quemado = true;
			salud -= 1;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = "10";
		}
		if(col.gameObject.tag == NameEnemy && vivo)
		{
			target = col.gameObject.transform;
			agent.isStopped = false;
		}
		if(col.gameObject.tag == NameEnemyTank && vivo)
		{
			target = col.gameObject.transform;
			agent.isStopped = false;
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
	}

	IEnumerator esperaCarta()
	{
		yield return new WaitForSeconds(1);
		if(vivo)
		{
			crearCarta = false;
		}
	}

	IEnumerator muertee ()
	{
		yield return new WaitForSeconds(8f);
		Destroy(gameObject);
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

	IEnumerator esperaDisparo()
	{
		yield return new WaitForSeconds(3f);
		disparando = false;
	}

	//EVENTOS SPINE
	//DISPAROS
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
	//CARGA
	void load2 ()
	{
		disparando = false;
		disparos = 0;
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
