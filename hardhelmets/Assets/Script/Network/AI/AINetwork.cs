using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AI;

public class AINetwork : NetworkBehaviour {

	public NavMeshAgent agent;

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

	public string _currentDirection = "right";

	[SyncVar(hook = "FacingCallback")]
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
	public GameObject bulletPrefFusil;
	public GameObject bulletPrefEscopeta;
	public GameObject bulletPrefSubmetra;
	public GameObject bulletPrefMetra;
	public GameObject cohete;

	public Transform bulletSpawn;
	public GameObject casquilloPref;
	public GameObject casquilloPrefB;
	public Transform casquilloSpawn;
	public GameObject granadePref;
	//LUZ DE ARMAS
	public GameObject luz;

	bool explocion;
	bool acuchillado;
	public GameObject Huesos;
	public GameObject[] sangreCuchillo;

	public Transform cascadoSpawn;

	public GameObject Base;

	public bool water;

	//MEDICO
	public ParticleSystem particulas;
	public string amigo;

	// Use this for initialization
	void Start ()
	{
		if(!isServer)
		{
			return;
		}

		distancia = Random.Range(minima,maxima);//20-30
		distanciaZ = Random.Range(3,8);

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

		_currentDirection = "right";

		if(Tipo == 5)
		{
			groundRadius = 0.5f;
		}
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

		//VOLTEA PERSONAJE
		gameObject.transform.localScale = new Vector3(voltear,1,1);

		//ANIMACION CAYENDO
		if(GetComponent<Rigidbody>().velocity.y < -4f)
		{
			if(Tipo != 5)
			{
				animator.SetBool("falling", true);
			}
		}else
		{
			animator.SetBool("falling", false);
		}
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
								if(Tipo == 5 && !disparando)
								{
									animator.SetBool("disparo", true);
									StartCoroutine(esperaDisparoPanzer());
								}else
								{
									if(disparos >= 3)
									{
										animator.SetInteger("recarga", Tipo);
									}else if(!disparando)
									{
										disparando = true;

										disparos += 1;
										animator.SetInteger("disparo", Tipo);

										StartCoroutine(esperaDisparo());
									}
								}
								agent.isStopped = true;
							}else if(target.tag == amigo)//MEDICO -- TARGET ES AMIGO
							{
								agent.isStopped = true;
								if(target.GetComponent<HeroNetwork>())//SI EL JUGADOR TIENE LA SANGRE AL MAXIMO DEJA DE CURAR
								{
									if(particulas.isPlaying && target.GetComponent<HeroNetwork>().salud >= target.GetComponent<HeroNetwork>().saludMax)
									{
										print("TIENE LA SANGRE LLENA");
										particulas.Stop();
									}
									if(!particulas.isPlaying && target.GetComponent<HeroNetwork>().salud < target.GetComponent<HeroNetwork>().saludMax)
									{
										particulas.Play();
									}
								}
								if(target.GetComponent<AINetwork>())//SI EL JUGADOR TIENE LA SANGRE AL MAXIMO DEJA DE CURAR
								{
									if(particulas.isPlaying && target.GetComponent<AINetwork>().salud >= target.GetComponent<AINetwork>().saludMax)
									{
										print("TIENE LA SANGRE LLENA");
										particulas.Stop();
									}
									if(!particulas.isPlaying && target.GetComponent<AINetwork>().salud < target.GetComponent<AINetwork>().saludMax)
									{
										particulas.Play();
									}
								}
							}else // ENEMIGO
							{
								if(Tipo == 5 && !disparando)
								{
									animator.SetBool("disparo", true);
									StartCoroutine(esperaDisparoPanzer());
								}else
								{
									print("DISPARAR AL ENEMIGO");
									if(disparos >= 3)
									{
										animator.SetInteger("recarga", Tipo);
									}else if(!disparando)
									{
										disparando = true;

										disparos += 1;
										animator.SetInteger("disparo", Tipo);

										StartCoroutine(esperaDisparo());
									}
								}
								agent.isStopped = true;
							}

						}else//SI NO ESTA CERCA EN Z
						{
							if(Tipo == 5 && !animator.GetCurrentAnimatorStateInfo(0).IsName("panzerShot"))//SI ES EL PANZER
							{
								agent.isStopped = false;
								agent.SetDestination(new Vector3(transform.position.x+Random.Range(-8,8), transform.position.y, target.position.z));
							}else if(!animator.GetBool("recargando"))//SI NO ES PANZER
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
			}
			if(!agent.isStopped)
			{
				animator.SetBool("walk", true);
			}else
			{
				if(Tipo == 5)
				{
					animator.SetBool("normal", true);
				}
				animator.SetBool("walk", false);
			}

			if(salud <= 0)
			{
				print("MUERTE");
				int muerteg = Random.Range(11,14);
				if(explocion)
				{
					animator.SetBool("muerto", true);
					animator.SetInteger("muerte", muerteg);
					var bones = (GameObject)Instantiate(Huesos, casquilloSpawn.position, transform.rotation);
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
				CmdChangeMascara("muerto");
				CmdChangeMascaraBase("mira");

				vivo = false;

				agent.isStopped = true;

				StartCoroutine(muertee());
			}else
			{
				explocion = false;
			}
		}else
		{
			agent.isStopped = true;
			mira.SetActive(false);
		
			if(ponermascara)
			{
				gameObject.layer = LayerMask.NameToLayer("muerto");
				CmdChangeMascara("muerto");

				Base.layer = LayerMask.NameToLayer("mira");
				CmdChangeMascaraBase("mira");

				ponermascara = false;
			}
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
			animator.SetBool("walking", false);
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
				var sangre2 = (GameObject)Instantiate(sangreCuchillo[Random.Range(0,sangreCuchillo.Length)], cascadoSpawn.position, cascadoSpawn.rotation);
			}
		}
		if(col.gameObject.tag == "explo" && vivo)
		{
			agent.isStopped = false;

			animator.SetBool("walk",false);

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
		//MEDICO
		if(col.gameObject.tag == amigo && vivo)
		{
			target = col.gameObject.transform;
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
		//MEDICO
		if(col.gameObject.tag == amigo)
		{
			target = null;
			particulas.Stop();
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

	IEnumerator esperaDisparo()
	{
		yield return new WaitForSeconds(1.5f);
		disparando = false;
	}
	IEnumerator esperaDisparoPanzer()
	{
		yield return new WaitForSeconds(3f);
		disparando = false;
	}

	//EVENTOS SPINE
	//DISPAROS
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

		var bulletB3 = (GameObject)Instantiate(bulletPrefEscopeta, bulletSpawn.position, Quaternion.Euler(0,0,0));//Quaternion.Euler(0,0,0)
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

		var bulletB3 = (GameObject)Instantiate(bulletPrefEscopeta, bulletSpawn.position, Quaternion.Euler(0,180,0)); 
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
	//PANZER
	public GameObject HumoPanzer;
	public GameObject strikeescopeta;
	void rocket ()
	{
		luz.SetActive(true);
		StartCoroutine(apaga());

		if(gameObject.transform.localScale.x >= 1)
		{
			CmdShotDP();
			var efect = (GameObject)Instantiate(strikeescopeta, bulletSpawn.transform.position, bulletSpawn.rotation);
		}else
		{
			CmdShotIP();
			var efect = (GameObject)Instantiate(strikeescopeta, bulletSpawn.transform.position, Quaternion.Euler(0,0,bulletSpawn.rotation.z-180));
		}
	}
	[Command]
	public void CmdShotDP()
	{
		var humo = (GameObject)Instantiate(HumoPanzer, new Vector3(bulletSpawn.position.x-4, bulletSpawn.position.y-0.5f, bulletSpawn.position.z), Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(humo);

		var granade = (GameObject)Instantiate(cohete, bulletSpawn.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(granade);
		granade.GetComponent<balaPanzerNetwork>().poder = saludMax*granade.GetComponent<balaPanzerNetwork>().poder/80;
	}

	[Command]
	public void CmdShotIP()
	{
		var humo = (GameObject)Instantiate(HumoPanzer, new Vector3(bulletSpawn.position.x+4, bulletSpawn.position.y-0.5f, bulletSpawn.position.z), Quaternion.Euler(0,180,0));
		NetworkServer.Spawn(humo);

		var granade = (GameObject)Instantiate(cohete, bulletSpawn.position, Quaternion.Euler(0,180,0));
		NetworkServer.Spawn(granade);
		granade.GetComponent<balaPanzerNetwork>().poder = saludMax*granade.GetComponent<balaPanzerNetwork>().poder/80;
	}
	//CARGA
	void load2 ()
	{
		disparando = false;
		disparos = 0;
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
