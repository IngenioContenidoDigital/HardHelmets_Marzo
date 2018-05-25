using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIVikingo : MonoBehaviour {

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

	public int Tipo;

	public bool crearCarta;
	public GameObject carta;

	public Transform target;

	public Animator animator;

	public string _currentDirection = "right";

	public float voltear;

	//GROUND CHECHER
	public Transform groundCheck;
	float groundRadius = 0.3f;
	public LayerMask whatIsGround;
	public bool grounded = false;

	public bool disparando;

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
	public GameObject casquilloPref;
	public Transform casquilloSpawn;
	//LUZ DE ARMAS
	public GameObject luz;

	bool explocion;
	bool acuchillado;
	public GameObject Huesos;
	public GameObject[] sangreCuchillo;

	public Transform cascadoSpawn;

	public GameObject Base;

	public bool water;

	//LLAMERO
	public ParticleSystem particulas;

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

		saludMax = 8*level+200;
		salud = saludMax;

		mira.SetActive(false);

		animator.SetBool("paracaidas", true);

		if(Tipo == 1)
		{
			animator.SetBool("falling", true);
		}

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

			animator.SetBool("disparar", false);
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
		if(Tipo == 1)
		{
			gameObject.transform.localScale = new Vector3(voltear,1.13f,1.13f);
		}

		if(vivo && !animator.GetBool("paracaidas"))
		{
			if(gameObject.tag == "Player")
			{
				gameObject.layer = LayerMask.NameToLayer("Player");
			}else
			{
				gameObject.layer = LayerMask.NameToLayer("Enemy");
			}

			if(grounded && target == null)
			{
				target = GameObject.FindWithTag(BuscarBase).transform;
			}else if(grounded && target.tag == BaseBuena)
			{
				target = null;
			}

			if(Tipo == 2 && gameObject.transform.localScale.x != voltear && !animator.GetCurrentAnimatorStateInfo(0).IsName("giro") && !animator.GetBool("girar"))
			{
				regreso();
			}

			if(target != null && !disparando)
			{
				//MIRA AL OBJKETIVO
				Vector3 v3Dir = target.position - transform.position;
				float angle = Mathf.Atan2(0, v3Dir.x) * Mathf.Rad2Deg;

				if(angle == 180 && _currentDirection != "left")
				{
					_currentDirection = "left";
					voltear = -1;
					if(Tipo == 2)
					{
						animator.SetBool("girar", true);
					}
				}
				if(angle == 0 && _currentDirection != "right")
				{
					_currentDirection = "right";
					voltear = 1;
					if(Tipo == 2)
					{
						animator.SetBool("girar", true);
					}
				}

				if(Mathf.Abs((transform.position - target.position).x) >= distancia && !animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))//SI ESTA LEJOS EN X
				{
					agent.isStopped = false;
					agent.SetDestination(target.position);
				}
				if(Mathf.Abs((transform.position - target.position).x) < distancia)//SI ESTA CERCA EN X
				{
					if(target.tag == BaseMala || target.tag == "newtra") // BASES NEUTRAS
					{
						agent.isStopped = true;
					}else
					{
						if(Mathf.Abs((transform.position - target.position).z) <= distanciaZ)//SI ESTA CERCA EN Z
						{
							if(target.tag == BuscarBase) // BASE ENEMIGA
							{
								if(!disparando)
								{
									disparando = true;
									if(Tipo == 2)
									{
										animator.SetBool("shot", true);
									}else
									{
										animator.SetBool("disparar", true);
									}
								}
								agent.isStopped = true;
							}else // ENEMIGO
							{
								if(!disparando)
								{
									disparando = true;

									if(Tipo == 2)
									{
										animator.SetBool("shot", true);
									}else
									{
										animator.SetBool("disparar", true);
									}
								}
								agent.isStopped = true;
							}

						}else//SI NO ESTA CERCA EN Z
						{
							if(!disparando)//SI NO ESTA DISPARANDO
							{
								agent.isStopped = false;
								agent.SetDestination(new Vector3(transform.position.x+Random.Range(-8,8), transform.position.y, target.position.z));
							}
						}
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
					}else if(target.GetComponent<AI>() != null)
					{
						if(target.GetComponent<AI>().salud <= 0)
						{
							target = null;
							agent.isStopped = true;
						}
					}else if(target.GetComponent<AIMortero>() != null)
					{
						if(target.GetComponent<AIMortero>().salud <= 0)
						{
							target = null;
							agent.isStopped = true;
						}
					}else if(target.GetComponent<AIMetra>() != null)
					{
						if(target.GetComponent<AIMetra>().salud <= 0)
						{
							target = null;
							agent.isStopped = true;
						}
					}else if(target.GetComponent<AIVikingo>() != null)
					{
						if(target.GetComponent<AIVikingo>().salud <= 0)
						{
							target = null;
							agent.isStopped = true;
						}
					}else if(target.GetComponent<AIVehicle>() != null)
					{
						if(target.GetComponent<AIVehicle>().salud <= 0)
						{
							target = null;
							agent.isStopped = true;
						}
					}
				}
			}else if(disparando)
			{
				agent.isStopped = true;
				animator.SetBool("walk", false);
			}
			if(!agent.isStopped)
			{
				if(!animator.GetBool("girar"))
				{
					animator.SetBool("walk", true);
				}
			}else
			{
				animator.SetBool("walk", false);
			}

			if(salud <= 0)
			{
				if(explocion)
				{
					var bones = (GameObject)Instantiate(Huesos, cascadoSpawn.position, transform.rotation);
					explocion = false;
				}
				vivo = false;
				animator.SetBool("muerto", true);

				gameObject.layer = LayerMask.NameToLayer("muerto");
				Base.layer = LayerMask.NameToLayer("mira");
			}else
			{
				explocion = false;
			}
		}else
		{
			agent.isStopped = true;
			mira.SetActive(false);

			if(salud <= 0)
			{
				animator.SetBool("muerto", true);
			}

			gameObject.layer = LayerMask.NameToLayer("muerto");
			Base.layer = LayerMask.NameToLayer("mira");
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

			acuchillado = true;

			//animator.SetBool("walk",false);
			animator.SetBool("cascado", true);

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

			//animator.SetBool("walk",false);

			animator.SetBool("cascado", true);

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

			//animator.SetBool("walk",false);

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
		if(col.gameObject.tag == NameEnemy || col.gameObject.tag == NameEnemyTank)
		{
			animator.SetBool("golpe", true);
			disparando = true;
		}
	}

	bool quemado;
	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "newtra" && vivo || col.gameObject.tag == BaseMala && vivo)
		{
			target = col.gameObject.transform;
			agent.isStopped = true;
			//animator.SetBool("walk",false);
		}

		if(col.gameObject.tag == "mira" && vivo)
		{
			mira.SetActive(true);
			mira.GetComponent<Animator>().SetBool("entry",true);
		}

		if(col.gameObject.tag == "balaLlamas" && vivo)
		{
			//animator.SetBool("walk",false);

			animator.SetBool("cascado", true);
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

		if(col.gameObject.tag == NameEnemy)
		{
			target = null;
		}
		if(col.gameObject.tag == NameEnemyTank)
		{
			target = null;
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
	//PARA EL LLAMERO
	public void regreso()
	{
		//VOLTEA PERSONAJE
		gameObject.transform.localScale = new Vector3(voltear,1,1);
	}
	//PATADA
	public GameObject golpe;
	public void pata ()
	{
		golpe.SetActive(true);
	}
	public void patapaga()
	{
		golpe.SetActive(false);
	}
	public void fuegoentra ()
	{
		particulas.Play();
	}
	public void fuegosale ()
	{
		particulas.Stop();
	}

	//PARA EL VIKINGO
	void termino ()
	{
		disparando = false;
	}
	void rafaga ()
	{
		luz.SetActive(true);
		StartCoroutine(apaga());

		if(_currentDirection != "left")
		{
			DisparoD();
		}else
		{
			DisparoI();
		}
	}
	//
	public void DisparoD()
	{
		var bullet = (GameObject)Instantiate(bulletPref, bulletSpawn.position, bulletSpawn.rotation); 
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.right * 100;

		bullet.GetComponent<balaOffline>().poder = saludMax*bullet.GetComponent<balaOffline>().poder/200;
		bullet.GetComponent<balaOffline>().cantidad = 1;
		Destroy(bullet, 1.0f);
		//CASQUILLOS}
		var casquillo = (GameObject)Instantiate(casquilloPref, casquilloSpawn.position, Quaternion.Euler(0,0,90)); 
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.right * Random.Range(-10,-50);
		casquillo.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(-0.0f,-0.2f));
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.up * Random.Range(-1,-6);
	}
	//
	public void DisparoI()
	{
		var bullet = (GameObject)Instantiate(bulletPref, bulletSpawn.position, Quaternion.Euler(0,180,0)); 
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.right * 100;

		bullet.GetComponent<balaOffline>().poder = saludMax*bullet.GetComponent<balaOffline>().poder/200;
		bullet.GetComponent<balaOffline>().cantidad = 1;
		Destroy(bullet, 1.0f);
		//CASQUILLOS}
		var casquillo = (GameObject)Instantiate(casquilloPref, casquilloSpawn.position, Quaternion.Euler(0,0,-90)); 
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.right * Random.Range(-10,-50);
		casquillo.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(-0.0f,-0.2f));
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.up * Random.Range(-1,-6);
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
}
