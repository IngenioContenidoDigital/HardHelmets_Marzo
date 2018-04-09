using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AITankNetwork : NetworkBehaviour {

	public bool pelea;

	public string BuscarBase;
	public string NameEnemy;
	public string NameEnemyTank;

	public Transform Player;

	public Transform target;

	public Animator animator;

	public bool vivo = true;

	[SyncVar]
	public int salud;

	//FUNCIONES
	bool actuando2;
	bool caminar2;
	bool shoot2;


	//ESQUIVAR OBSTACULOS
	Vector3 nextPosition;
	public Transform seguir;


	//GROUND CHECHER
	public Transform groundCheck;
	float groundRadius = 1f;
	public LayerMask whatIsGround;
	public bool grounded = false;

	//SEGUNDO SCRIPT
	//OBJETOS DE DISPARO
	public GameObject bulletPref;

	public Transform bulletSpawn;
	public GameObject luz;

	int azar;

	public GameObject efecto;
	int efect;

	//FUNCIONES ALEATORIAS
	bool actuando;

	bool cubrirse;
	bool cubierto;
	public Vector3 coverPosition;
	bool shoot;
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

	//CAMARA LENTA
	int matrix;
	bool matrix2;

	Vector3 v3;
	int dir;

	bool mirando;

	//QUE TAN LEJOS DE LA BASE DISPARA
	float lejos;

	public GameObject textos;

	//VOLUMEN
	float efectos;
	public AudioClip muerte;

	//SOURCE
	public AudioSource audio1;

	public GameObject mira;

	// Use this for initialization
	void Start ()
	{
		mira.SetActive(false);

		efectos = PlayerPrefs.GetFloat("efects");

		matrix = Random.Range(1,6);
		lejos = Random.Range(50,57);
	}

	// Update is called once per frame
	public void Update ()
	{
		if(!isServer)
		{
			return;
		}

		if(grounded && GetComponent<Rigidbody>().drag == 5)
		{
			GetComponent<Rigidbody>().drag = 0;
		}

		//CHECA SI ESTA EN EL PISO
		grounded = Physics.CheckSphere(groundCheck.position, groundRadius, whatIsGround);
		animator.SetBool("grounded", grounded);

		if(animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
		{
			caminar = false;
			caminar2 = false;
			alejarce = false;
		}
		if(Player == null)
		{
			pelea = false;
		}else 
		{
			pelea = true;
		}
		if(pelea)
		{
			if(vivo)
			{
				if(grounded)
				{
					if(animator.GetCurrentAnimatorStateInfo(0).IsName("TankIdle") && !animator.GetBool("walk") && !animator.GetBool("atras"))
					{
						actuando = false;
					}
					if(Mathf.Abs((transform.position - Player.position).x) >= 25.01f && !actuando)// && Mathf.Abs((transform.position - Player.position).x) <= 35f && !actuando)
					{
						//busca y dispara
						Funciones();
						actuando = true;
					}
					if(Mathf.Abs((transform.position - Player.position).x) <= 25 && !actuando)//SI ESTA CERCA MUY CERCA
					{
						//atras
						Funciones2();
						actuando = true;
					}
					//ACCIONES UPDATE
					//ALEJARCE
					if(alejarce)
					{
						shoot = false;
						caminar = false;

						animator.SetBool("walk",false);
						animator.SetBool("atras",true);

						if(animator.GetCurrentAnimatorStateInfo(0).IsName("accelBack") || alejarce && animator.GetCurrentAnimatorStateInfo(0).IsName("driveBack"))
						{
							if(gameObject.transform.localScale.x >= 1)
							{
								GetComponent<Rigidbody>().velocity = (Vector2.left * 7);
							}else
							{
								GetComponent<Rigidbody>().velocity = (Vector2.right * 7);
							}
						}
					}

					if(caminar)
					{
						shoot = false;
						alejarce = false;

						if(animator.GetCurrentAnimatorStateInfo(0).IsName("accel") || animator.GetCurrentAnimatorStateInfo(0).IsName("drive"))
						{
							if(gameObject.transform.localScale.x >= 1)
							{
								GetComponent<Rigidbody>().velocity = (Vector2.right * 10);
							}else
							{
								GetComponent<Rigidbody>().velocity = (Vector2.left * 10);
							}

							if(Player.transform.position.z > transform.position.z)
							{
								seguir.GetComponent<vista>().arriba = true;
							}else if(Player.transform.position.z < transform.position.z)
							{
								seguir.GetComponent<vista>().abajo = true;
							}

							nextPosition = new Vector3(transform.position.x, transform.position.y, seguir.transform.position.z);
							transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * 10);
						}
					}
					//DISPARO
					if(shoot)
					{
						animator.SetBool("walking", false);
						animator.SetBool("walk", false);
						animator.SetBool("atras", false);

						alejarce = false;
						caminar = false;

						seguir.GetComponent<vista>().arriba = false;
						seguir.GetComponent<vista>().abajo = false;

						GetComponent<Rigidbody>().velocity = Vector3.zero;
					}
				}

				if(salud <= 0)
				{
					animator.SetBool("muerto", true);
					azar = Random.Range(1,3);
					animator.SetInteger("muerte", azar);

					audio1.volume = efectos;
					audio1.clip = muerte;
					audio1.Play();


					vivo = false;
				}
			}else
			{
				mira.SetActive(false);
				if(matrix == 1 && !matrix2 && Player.name == "Hero")
				{
					matrix2 = true;
					//Manager.lenta = true;
				}
				gameObject.layer = LayerMask.NameToLayer("muerto");
				//gameObject.tag = "Untagged";
			}
		}else
		{
			if(vivo)
			{
				if(grounded && target == null)
				{
					target = GameObject.FindWithTag(BuscarBase).transform;
				}

				if(salud <= 0)
				{
					animator.SetBool("muerto", true);
					int azar = Random.Range(1,3);
					animator.SetInteger("muerte", azar);
					vivo = false;

					gameObject.layer = LayerMask.NameToLayer("muerto");
					//gameObject.tag = "Untagged";

					audio1.volume = efectos;
					audio1.clip = muerte;
					audio1.Play();
				}

				if(grounded && target != null)
				{
					if(animator.GetCurrentAnimatorStateInfo(0).IsName("driveBack"))
					{
						animator.SetBool("atras",false);
						animator.SetBool("walking",false);
					}
					if(Mathf.Abs((transform.position - target.position).x) >= lejos && !caminar2)
					{
						caminar2 = true;
					}
					if(Mathf.Abs((transform.position - target.position).x) <= lejos && !actuando2)
					{
						shoot2 = true;
					}
				}

				if(caminar2 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Cae") && !animator.GetCurrentAnimatorStateInfo(0).IsName("parchuteTank"))
				{
					animator.SetBool("walk",true);

					if(animator.GetCurrentAnimatorStateInfo(0).IsName("accel") || animator.GetCurrentAnimatorStateInfo(0).IsName("drive"))
					{
						if(gameObject.transform.localScale.x >= 1)
						{
							GetComponent<Rigidbody>().velocity = (Vector2.right * 10);
						}else
						{
							GetComponent<Rigidbody>().velocity = (Vector2.left * 10);
						}

						nextPosition = new Vector3(transform.position.x, transform.position.y, seguir.transform.position.z);
						transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * 10);
					}
				}
				//DISPARO
				if(shoot2 && !animator.GetCurrentAnimatorStateInfo(0).IsName("TankShoot"))
				{
					animator.SetBool("walking", false);

					shoot2 = false;

					caminar2 = false;

					animator.SetBool("disparo", true);
				}

				if(animator.GetCurrentAnimatorStateInfo(0).IsName("TankShoot"))
				{
					actuando2 = true;
				}else
				{
					actuando2 = false;
				}
			}else
			{
				mira.SetActive(false);
				gameObject.layer = LayerMask.NameToLayer("muerto");
				//gameObject.tag = "Untagged";
			}
		}
	}

	void Funciones()
	{
		random = Random.Range(1,3);

		movimientos();
		return;
	}
	void Funciones2()
	{
		random = Random.Range(3,5);

		movimientos();
		return;
	}
	void movimientos()
	{
		if(random == 1)
		{
			dist = Random.Range(1,2.5f);
			animator.SetBool("walk",true);
			caminar = true;
			StartCoroutine(esperaCamina());
			random = 0;
		}else if(random == 2)
		{
			animator.SetBool("disparo", true);
			shoot = true;
			random = 0;
		}else if(random == 3)
		{
			dist = Random.Range(2,4);
			animator.SetBool("atras",true);
			StartCoroutine(esperaAleja());
			alejarce = true;
			random = 0;
		}else if(random == 4)
		{
			animator.SetBool("disparo", true);
			shoot = true;
			random = 0;
		}
		return;
	}

	//TIEMPOS DE ESPERA
	IEnumerator esperaCamina()
	{
		yield return new WaitForSeconds(dist);
		animator.SetBool("walk",false);
		caminar = false;
		actuando = false;
	}
	IEnumerator esperaAleja()
	{
		yield return new WaitForSeconds(dist);
		animator.SetBool("atras",false);
		alejarce = false;
		actuando = false;
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "bala" && vivo)
		{
			random = Random.Range(0,4);
			caminar = false;
			alejarce = false;
			Destroy(col.gameObject);
			salud -= 15;

			var letras = (GameObject)Instantiate(textos, new Vector3(transform.position.x, transform.position.y+9,transform.position.z), transform.rotation);
			letras.transform.parent = transform;
			letras.GetComponent<TextMesh>().text = "15";
		}
		if(col.gameObject.tag == "balaFusil" && vivo)
		{
			random = Random.Range(0,4);
			caminar = false;
			alejarce = false;
			Destroy(col.gameObject);
			salud -= 40;

			var letras = (GameObject)Instantiate(textos, new Vector3(transform.position.x, transform.position.y+9,transform.position.z), transform.rotation);
			letras.transform.parent = transform;
			letras.GetComponent<TextMesh>().text = "40";
		}
		if(col.gameObject.tag == "balaEscopeta" && vivo)
		{
			random = Random.Range(0,4);
			caminar = false;
			alejarce = false;
			Destroy(col.gameObject);
			salud -= 15;

			var letras = (GameObject)Instantiate(textos, new Vector3(transform.position.x, transform.position.y+9,transform.position.z), transform.rotation);
			letras.transform.parent = transform;
			letras.GetComponent<TextMesh>().text = "15";
		}
		if(col.gameObject.tag == "balaSubmetra" && vivo)
		{
			random = Random.Range(0,4);
			caminar = false;
			alejarce = false;
			Destroy(col.gameObject);
			salud -= 20;

			var letras = (GameObject)Instantiate(textos, new Vector3(transform.position.x, transform.position.y+9,transform.position.z), transform.rotation);
			letras.transform.parent = transform;
			letras.GetComponent<TextMesh>().text = "20";
		}
		if(col.gameObject.tag == "balaMetra" && vivo)
		{
			random = Random.Range(0,4);
			caminar = false;
			alejarce = false;
			Destroy(col.gameObject);
			salud -= 25;

			var letras = (GameObject)Instantiate(textos, new Vector3(transform.position.x, transform.position.y+9,transform.position.z), transform.rotation);
			letras.transform.parent = transform;
			letras.GetComponent<TextMesh>().text = "25";
		}
		if(col.gameObject.tag == "balaMG" && vivo)
		{
			random = Random.Range(0,4);
			caminar = false;
			alejarce = false;
			Destroy(col.gameObject);
			salud -= 25;

			var letras = (GameObject)Instantiate(textos, new Vector3(transform.position.x, transform.position.y+9,transform.position.z), transform.rotation);
			letras.transform.parent = transform;
			letras.GetComponent<TextMesh>().text = "25";
		}
		if(col.gameObject.tag == "explo" && vivo)
		{
			salud -= 50;

			var letras = (GameObject)Instantiate(textos, new Vector3(transform.position.x, transform.position.y+9,transform.position.z), transform.rotation);
			letras.transform.parent = transform;
			letras.GetComponent<TextMesh>().text = "50";
		}
		if(col.gameObject.tag == "pared" && vivo)
		{
			alejarce = false;
			animator.SetBool("walk", false);
		}
	}

	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "mira" && vivo)
		{
			mira.SetActive(true);
			mira.GetComponent<Animator>().SetBool("entry",true);
		}
		if(col.gameObject.tag == "balaLlamas" && vivo)
		{
			salud -= 10;

			var letras = (GameObject)Instantiate(textos, new Vector3(transform.position.x, transform.position.y+9,transform.position.z), Quaternion.Euler(0,0,0));
			letras.transform.parent = transform;
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
	}
	void OnTriggerExit (Collider col)
	{
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

	//EVENTOS SPINE
	void shot()
	{
		luz.SetActive(true);
		StartCoroutine(apaga());

		if(gameObject.transform.localScale.x >= 1)
		{
			CmdShotD();
		}else
		{
			CmdShotI();
		}
		actuando = false;
	}
	[Command]
	public void CmdShotD()
	{
		var granade = (GameObject)Instantiate(bulletPref, bulletSpawn.position, bulletSpawn.rotation);
		granade.GetComponent<Rigidbody>().velocity = granade.transform.up * 20;
		granade.GetComponent<Rigidbody>().AddForce(transform.right * Random.Range(80,120));
		NetworkServer.Spawn(granade);
	}
	[Command]
	public void CmdShotI()
	{
		var granade = (GameObject)Instantiate(bulletPref, bulletSpawn.position, bulletSpawn.rotation);
		granade.GetComponent<Rigidbody>().velocity = granade.transform.up * 20;
		granade.GetComponent<Rigidbody>().AddForce(transform.right * Random.Range(-80,-120));
		NetworkServer.Spawn(granade);
	}
	void fincho ()
	{
		actuando = false;
		shoot = false;
	}
	void adios()
	{
		Destroy(gameObject);
	}
}
