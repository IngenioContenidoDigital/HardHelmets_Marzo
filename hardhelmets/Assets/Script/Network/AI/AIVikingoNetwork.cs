using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AI;

public class AIVikingoNetwork : NetworkBehaviour {

	public NavMeshAgent agent;

	[SyncVar(hook = "noVivo")]
	public bool vivo = true;

	[SyncVar]
	public float salud;
	[SyncVar]
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

	[SyncVar]
	public string _currentDirection = "right";

	[SyncVar(hook = "FacingCallback")]
	public float voltear;

	//GROUND CHECHER
	public Transform groundCheck;
	float groundRadius = 0.6f;
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
	public GameObject sangre;

	public Transform cascadoSpawn;

	public GameObject Base;

	public bool water;

	//LLAMERO
	public ParticleSystem particulas;

	// Use this for initialization
	void Start ()
	{
		if(!isServer)
		{
			return;
		}

		distancia = Random.Range(minima,maxima);//20-30
		distanciaZ = Random.Range(0,3);

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

		mira.SetActive(false);

		animator.SetBool("paracaidas", true);

		_currentDirection = "right";
	}
	bool ponermascara;
	public GameObject spheraColision;
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

			animator.SetBool("disparar", false);
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

		if(vivo && !animator.GetBool("paracaidas"))
		{
			if(!ponermascara)
			{
				if(gameObject.tag == "Player")
				{
					gameObject.layer = LayerMask.NameToLayer("Player");
					CmdChangeMascara("Player");
					Base.layer = LayerMask.NameToLayer("Vista");
					CmdChangeMascaraBase("Vista");
				}else
				{
					gameObject.layer = LayerMask.NameToLayer("Enemy");
					CmdChangeMascara("Enemy");
					Base.layer = LayerMask.NameToLayer("Vista");
					CmdChangeMascaraBase("Vista");
				}
				ponermascara = true;
			}

			if(grounded && target == null)
			{
				target = GameObject.Find(BuscarBase).transform;
			}else if(grounded && target.tag == BaseBuena)
			{
				target = null;
			}

			if(gameObject.transform.localScale.x != voltear && !animator.GetCurrentAnimatorStateInfo(0).IsName("giro") && !animator.GetBool("girar"))
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
					if(Tipo == 1)
					{
						voltear = -1.13f;
					}else
					{
						voltear = -1;
					}
					animator.SetBool("girar", true);
				}
				if(angle == 0 && _currentDirection != "right")
				{
					_currentDirection = "right";
					if(Tipo == 1)
					{
						voltear = 1.13f;
					}else
					{
						voltear = 1;
					}
					animator.SetBool("girar", true);
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
					if(target.GetComponent<HeroNetwork>() != null)
					{
						if(target.GetComponent<HeroNetwork>().salud <= 0)
						{
							target = null;
							agent.isStopped = true;
							spheraColision.SetActive(false);
							StartCoroutine(momentoSphere());
						}
					}else if(target.GetComponent<AINetwork>() != null)
					{
						if(target.GetComponent<AINetwork>().salud <= 0)
						{
							target = null;
							agent.isStopped = true;
							spheraColision.SetActive(false);
							StartCoroutine(momentoSphere());
						}
					}else if(target.GetComponent<AIMorteroNetwork>() != null)
					{
						if(target.GetComponent<AIMorteroNetwork>().salud <= 0)
						{
							target = null;
							agent.isStopped = true;
							spheraColision.SetActive(false);
							StartCoroutine(momentoSphere());
						}
					}else if(target.GetComponent<AIMetraNetwork>() != null)
					{
						if(target.GetComponent<AIMetraNetwork>().salud <= 0)
						{
							target = null;
							agent.isStopped = true;
							spheraColision.SetActive(false);
							StartCoroutine(momentoSphere());
						}
					}else if(target.GetComponent<AIVikingoNetwork>() != null)
					{
						if(target.GetComponent<AIVikingoNetwork>().salud <= 0)
						{
							target = null;
							agent.isStopped = true;
							spheraColision.SetActive(false);
							StartCoroutine(momentoSphere());
						}
					}else if(target.GetComponent<AITank2>() != null)
					{
						if(target.GetComponent<AITank2>().salud <= 0)
						{
							target = null;
							agent.isStopped = true;
							spheraColision.SetActive(false);
							StartCoroutine(momentoSphere());
						}
					}else if(target.GetComponent<AIMetraMaloNetwork>() != null)
					{
						if(target.GetComponent<AIMetraMaloNetwork>().salud <= 0)
						{
							target = null;
							agent.isStopped = true;
							spheraColision.SetActive(false);
							StartCoroutine(momentoSphere());
						}
					}else if(target.GetComponent<AIMorteroMaloNetwork>() != null)
					{
						if(target.GetComponent<AIMorteroMaloNetwork>().salud <= 0)
						{
							target = null;
							agent.isStopped = true;
							spheraColision.SetActive(false);
							StartCoroutine(momentoSphere());
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
				CmdChangeMascaraBase("mira");
			}else
			{
				explocion = false;
			}

			//DISPARO DE FUEGO
			if(disparafuego)
			{
				if(fuegodisparo == 0)
				{
					if(_currentDirection == "right")
					{
						var bullet = (GameObject)Instantiate(bulletPref, bulletSpawn.position, bulletSpawn.rotation); 
						bullet.GetComponent<Rigidbody>().velocity = bullet.transform.right * 10;

						bullet.GetComponent<balaFuego>().poder = saludMax*bullet.GetComponent<balaFuego>().poder/200;
						Destroy(bullet, 1.5f);
					}else
					{
						var bullet = (GameObject)Instantiate(bulletPref, bulletSpawn.position, bulletSpawn.rotation); 
						bullet.GetComponent<Rigidbody>().velocity = bullet.transform.right * -10;

						bullet.GetComponent<balaFuego>().poder = saludMax*bullet.GetComponent<balaFuego>().poder/200;
						Destroy(bullet, 1.5f);
					}
				}
			}
			fuegodisparo += 1;
			if(fuegodisparo >= 5)
			{
				fuegodisparo = 0;
			}
		}else
		{
			agent.isStopped = true;
			mira.SetActive(false);

			if(salud <= 0)
			{
				animator.SetBool("muerto", true);
			}

			if(ponermascara)
			{
				gameObject.layer = LayerMask.NameToLayer("muerto");
				CmdChangeMascara("muerto");

				ponermascara = false;
			}
			Base.layer = LayerMask.NameToLayer("mira");
			CmdChangeMascaraBase("mira");
		}
	}

	IEnumerator momentoSphere()
	{
		yield return new WaitForSeconds(0.3f);
		spheraColision.SetActive(true);
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
	[Command]
	public void CmdChangeMascaraBase(string newMascara)
	{
		RpcChangeMascaraBase(newMascara);
	}
	[ClientRpc]
	public void RpcChangeMascaraBase (string newMascara)
	{
		Base.layer = LayerMask.NameToLayer(newMascara);
	}

	void noVivo(bool vivo)
	{
		vivo = false;
	}

	void FacingCallback(float voltear)
	{
		if(Tipo == 1)
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
		}else
		{
			if (voltear == 1)
			{
				_currentDirection = "right";
				gameObject.transform.localScale = new Vector3(1,1,1);
			}else
			{
				_currentDirection = "left";
				gameObject.transform.localScale = new Vector3(-1,1,1);
			}
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

			animator.SetBool("walk",false);
			animator.SetBool("cascado", true);

			//efect = Random.Range(3,5);
			if(PlayerPrefs.GetInt("violencia") == 1)
			{
				var sangre2 = (GameObject)Instantiate(sangre, col.transform.position, cascadoSpawn.rotation); 
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

			if(PlayerPrefs.GetInt("violencia") == 1)
			{
				var sangre2 = (GameObject)Instantiate(sangre, col.transform.position, cascadoSpawn.rotation); 
			}
		}
		if(col.gameObject.tag == "explo" && vivo)
		{
			agent.isStopped = false;

			animator.SetBool("walk",false);

			if(PlayerPrefs.GetInt("violencia") == 1)
			{
				var sangre2 = (GameObject)Instantiate(sangre, col.transform.position, cascadoSpawn.rotation); 
			}

			salud -= col.gameObject.GetComponent<Explo>().poder;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = col.gameObject.GetComponent<Explo>().poder.ToString("F0");

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
					var sangre2 = (GameObject)Instantiate(sangre, col.transform.position, cascadoSpawn.rotation); 
				}
			}
		}
		if(col.gameObject.tag == NameEnemy || col.gameObject.tag == NameEnemyTank)
		{
			animator.SetBool("golpe", true);
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

			animator.SetBool("cascado", true);
			Destroy(col.gameObject);

			if(PlayerPrefs.GetInt("violencia") == 1)
			{
				var sangre2 = (GameObject)Instantiate(sangre, col.transform.position, cascadoSpawn.rotation); 
			}

			quemado = true;
			salud -= col.gameObject.GetComponent<balaFuego>().poder;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = col.gameObject.GetComponent<balaFuego>().poder.ToString("F0");
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
	[Command]
	public void CmdSaludSumar()
	{
		suma = saludMax*2/80;
		salud += saludMax*2/80;
	}
	public void SaludSumar()
	{
		CmdSaludSumar();

		suma = saludMax*2/80;
		var letras = (GameObject)Instantiate(textos2, transform.position, Quaternion.Euler(0,0,0));
		letras.GetComponent<TextMesh>().text = "+"+suma.ToString("F0");
	}
	//APAGA LA LUZ
	IEnumerator apaga ()
	{
		yield return new WaitForSeconds(0.1f);
		luz.SetActive(false);
	}
	public bool disparafuego;
	int fuegodisparo;
	//EVENTOS SPINE
	//PARA EL LLAMERO
	public void regreso()
	{
		//VOLTEA PERSONAJE
		if(Tipo == 1)
		{
			gameObject.transform.localScale = new Vector3(voltear,1.13f,1.13f);
		}else
		{
			gameObject.transform.localScale = new Vector3(voltear,1,1);
		}
		Rpc_regreso(voltear);
	}
	[ClientRpc]
	public void Rpc_regreso(float newvoltear)
	{
		if(Tipo == 1)
		{
			gameObject.transform.localScale = new Vector3(newvoltear,1.13f,1.13f);
		}else
		{
			gameObject.transform.localScale = new Vector3(newvoltear,1,1);
		}
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
		disparafuego = true;
		particulas.Play();
	}
	public void fuegosale ()
	{
		disparafuego = false;
		particulas.Stop();
	}
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
