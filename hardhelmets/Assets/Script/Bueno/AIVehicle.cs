using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIVehicle : MonoBehaviour {

	[ Header ("Driving settings")]
	[ Tooltip ("Torque added to each wheel.")] public float wheelTorque = 3000.0f; // Reference to "Wheel_Rotate".
	[ Tooltip ("Maximum Speed (Meter per Second)")] public float maxSpeed = 7.0f; // Reference to "Wheel_Rotate".
	[ Tooltip ("Rate for ease of turning."), Range (0.0f, 2.0f)] public float turnClamp = 0.8f;
	[ Tooltip ("'Solver Iteration Count' of all the rigidbodies in this tank.")] public int solverIterationCount = 7;

	// Reference to "Wheel_Rotate".
	[HideInInspector] public float leftRate;
	[HideInInspector] public float rightRate;

	Rigidbody thisRigidbody;
	bool isParkingBrake = false;
	float lagCount;
	float speedStep;
	float autoParkingBrakeVelocity = 0.5f;
	float autoParkingBrakeLag = 0.5f;

	/// MIOS
	/// 
	/// 
	//GROUND CHECHER
	public Transform groundCheck;
	float groundRadius = 5f;
	public LayerMask whatIsGround;
	public bool grounded = false;

	public GameObject seguir;

	public Transform Player;
	public Transform Base;
	public Transform target;

	public string BaseBuena;
	public string BaseMala;
	public string BuscarBase;
	public string NameEnemy;
	public string NameEnemyTank;


	public int distancia;
	public bool adelante;
	public bool atras;
	public bool der;
	public bool izq;
	public bool freno;

	public GameObject tank;
	public GameObject trail1;
	public GameObject trail2;
	public GameObject cubo;
	public bool disparo;
	public bool listo;
	public GameObject canon;
	public GameObject bulletPref;
	public GameObject fuego;
	public Transform bulletSpawn;
	public GameObject luz;
	public GameObject textos;


	public bool vivo = true;

	bool quieto;


	public float salud;

	public float saludMax;


	public bool rafaga;

	public GameObject BaseUno;

	//PANEL PARTIDA
	GameObject Panel;

	bool sumar;

	//NIVEL DE CARTA
	public int level;
	public GameObject Jugador;

	//ID_Control_CS idScript;

	void Awake ()
	{
		//tank.layer = 20;
		this.gameObject.layer = 20; // Layer11 >> for MainBody.
		thisRigidbody = GetComponent < Rigidbody > ();
		thisRigidbody.solverIterations = solverIterationCount;
	}

	void Start ()
	{
		if(gameObject.tag == "tank")
		{
			Jugador = GameObject.Find("Hero");
		}else
		{
			Jugador = GameObject.Find("Hero2");
		}

		if(gameObject.tag == "tank")
		{
			level = Jugador.GetComponent<Hero>().level;
		}else
		{
			level = PlayerPrefs.GetInt("levelCommunity");
		}

		saludMax = 9*level+250;
		salud = saludMax;

		distancia = Random.Range(30,40);
	}

	public float giro;

	void Update ()
	{
		if(Panel == null)
		{
			Panel = GameObject.Find("GAME");
		}
		if(Panel.GetComponent<GameOffline>().final)
		{
			quieto = true;

			adelante = false;
			atras = false;
			der = false;
			izq = false;
			freno = true;
		}

		//CHECA SI ESTA EN EL PISO
		grounded = Physics.CheckSphere(groundCheck.position, groundRadius, whatIsGround);

		if(Base == null)
		{
			Base = GameObject.FindWithTag(BuscarBase).transform;
		}

		if(baseNeutra != null && baseNeutra.tag == BaseBuena)
		{
			freno = false;
		}

		if(Player != null)
		{
			target = Player.transform;
		}else
		{
			target = Base.transform;
		}

		if(speedStep >= 0.7f)//VELOCIDAD MAXIMA
		{
			speedStep = 0.7f;
		}

		if(vivo)
		{
			if(grounded && !quieto)
			{
				trail1.SetActive(true);
				trail2.SetActive(true);
				if(disparo && listo)
				{
					shoot();
					disparo = false;
				}

				if(target == Player)
				{
					Vector3 lugar = transform.InverseTransformPoint(Base.transform.position);

					float angle = Vector3.Angle(transform.forward, target.transform.position-transform.position);
					if(Mathf.Abs((transform.position - target.position).x) <= 23)
					{
						if(Mathf.Abs(angle) < 130)
						{
							if(lugar.x > 0.0f)
							{
								der = true;
							}else if(lugar.x < 0.0f)
							{
								izq = true;
							}
							adelante = false;
							atras = true;
						}else
						{
							if(lugar.x > 0.0f)
							{
								der = true;
							}else if(lugar.x < 0.0f)
							{
								izq = true;
							}
							atras = false;
							adelante = true;
						}
					}else
					{
						der = false;
						izq = false;
						atras = false;
						adelante = false;
						freno = true;
					}

					disparo = true;

				}else if(Mathf.Abs((transform.position - target.position).x) >= distancia)
				{
					Vector3 lugar = transform.InverseTransformPoint(target.transform.position);

					float angle = Vector3.Angle(transform.forward, target.transform.position-transform.position);
					/*if(Mathf.Abs(angle) < 15 && !seguir.GetComponent<vistaTank>().obstacle && !seguir.GetComponent<vistaTank>().obstacle)
					{
						adelante = true;
						der = false;
						izq = false;
					}else */
					if (seguir.GetComponent<vistaTank>().obstacle)
					{
						adelante = false;
						//atras = true;
						if(lugar.x > 0.0f && !izq)
						{
							print("DERECHA");
							izq = false;
							der = true;
						}else if(lugar.x < 0.0f && !der)
						{
							print("IZQUIERDA");
							der = false;
							izq = true;
						}
						print("buscar base");
					}else
					{
						if(Mathf.Abs(angle) > 90)
						{
							adelante = false;
							//atras = true;
							if(lugar.x > 0.0f)
							{
								print("DERECHA2");
								izq = false;
								der = true;
							}else if(lugar.x < 0.0f)
							{
								print("IZQUIERDA2");
								der = false;
								izq = true;
							}
						}else
						{
							atras = false;
							adelante = true;
							der = false;
							izq = false;
							print("IR base");
						}
					}
					//adelante = true;
				}else
				{
					adelante = false;
					der = false;
					izq = false;
					print("atacar base");

					disparo = true;
				}

				float vertical = speedStep;
				float horizontal = giro;//Input.GetAxis ("Horizontal");
				float clamp = Mathf.Lerp (turnClamp, 1.0f, Mathf.Abs (vertical / 1.0f));
				horizontal = Mathf.Clamp (horizontal, -clamp, clamp);

				if(adelante)
				{
					if(GetComponent<tankSounds>().audio1.clip != GetComponent<tankSounds>().acelera)
					{
						GetComponent<tankSounds>().Accelerate();
					}
					//Vector3 lugar = transform.InverseTransformPoint(target.transform.position);

					/*if(seguir.GetComponent<vista>().arriba || seguir.GetComponent<vista>().abajo)
					{
						if(lugar.x > 0.0f)
						{
							der = true;
						}else if(lugar.x < 0.0f)
						{
							izq = true;
						}
					}else
					{
						izq = false;
						der = false;
					}*/

					speedStep += 0.5f;
					speedStep = Mathf.Clamp (speedStep, -1.0f, 1.0f);
				}else if(atras)
				{
					if(GetComponent<tankSounds>().audio1.clip != GetComponent<tankSounds>().acelera)
					{
						GetComponent<tankSounds>().Accelerate();
					}
					speedStep -= 0.5f;
					speedStep = Mathf.Clamp (speedStep, -1.0f, 1.0f);
				}else
				{
					if(GetComponent<tankSounds>().audio1.clip != GetComponent<tankSounds>().desacelera)
					{
						GetComponent<tankSounds>().Decelerate();
					}
					speedStep = 0.0f;
					giro = 0;
				}

				if(der)
				{
					giro++;
				}else if(izq)
				{
					giro--;
				}else
				{
					giro = 0;
				}
				if(freno)
				{
					speedStep = 0.0f;
					giro = 0;
					freno = false;
				}

				if (vertical < 0.0f)
				{
					horizontal = -horizontal; // like a brake-turn.
				}
				leftRate = Mathf.Clamp (-vertical - horizontal, -1.0f, 1.0f);
				rightRate = Mathf.Clamp (vertical - horizontal, -1.0f, 1.0f);
			}else
			{
				trail1.SetActive(false);
				trail2.SetActive(false);
			}
		}else
		{
			if(!sumar)
			{
				if(gameObject.tag == "tank")
				{
					Panel.GetComponent<GameOffline>().VechicleDestroyedM += 1;
				}else
				{
					Panel.GetComponent<GameOffline>().VechicleDestroyedB += 1;
				}
				sumar = true;
			}

			adelante = false;
			atras = false;
			der = false;
			izq = false;

			speedStep = 0.0f;
			giro = 0;

			freno = true;

			gameObject.layer = LayerMask.NameToLayer("muerto");
			//gameObject.tag = "Untagged";
			BaseUno.layer = LayerMask.NameToLayer("mira");
		}

		if(salud <= 0)
		{
			freno = true;
			vivo = false;
			gameObject.layer = LayerMask.NameToLayer("muerto");
			//gameObject.tag = "Untagged";
			BaseUno.layer = LayerMask.NameToLayer("mira");
		}
	}

	IEnumerator ocultar ()
	{
		yield return new WaitForSeconds(5f);
		Destroy(torreta);
	}
	IEnumerator quitar ()
	{
		yield return new WaitForSeconds(5.5f);
		Destroy(gameObject);
	}

	public GameObject torreta;
	public GameObject torretaBase;
	public GameObject explo;
	bool matar;

	void shoot ()
	{
		GetComponent<tankSounds>().canon();

		canon.GetComponent<Animator>().SetBool("disparo", true);

		listo = false;
		StartCoroutine(carga());


		luz.SetActive(true);
		StartCoroutine(apaga());

		_Disparo();
		rafaga = true;
	}

	void _Disparo()
	{
		var granade = (GameObject)Instantiate(bulletPref, bulletSpawn.position, bulletSpawn.rotation);
		granade.GetComponent<Rigidbody>().velocity = granade.transform.forward * Random.Range(35,90);//35,91

		granade.GetComponent<balaTank>().poder = saludMax*granade.GetComponent<balaTank>().poder/250;
	}

	void FixedUpdate ()
	{
		// Auto Parking Brake using 'RigidbodyConstraints'.
		if (leftRate == 0.0f && rightRate == 0.0f)
		{
			float velocityMag = thisRigidbody.velocity.magnitude;
			float angularVelocityMag = thisRigidbody.angularVelocity.magnitude;
			if (isParkingBrake == false)
			{
				if (velocityMag < autoParkingBrakeVelocity && angularVelocityMag < autoParkingBrakeVelocity)
				{
					lagCount += Time.fixedDeltaTime;
					if (lagCount > autoParkingBrakeLag)
					{
						isParkingBrake = true;
						thisRigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY;
					}
				}
			}else
			{
				if (velocityMag > autoParkingBrakeVelocity || angularVelocityMag > autoParkingBrakeVelocity)
				{
					isParkingBrake = false;
					thisRigidbody.constraints = RigidbodyConstraints.None;
					lagCount = 0.0f;
				}
			}
		}else
		{
			isParkingBrake = false;
			thisRigidbody.constraints = RigidbodyConstraints.None;
			lagCount = 0.0f;
		}

		if(rafaga)
		{
			var efecto = (GameObject)Instantiate(fuego, bulletSpawn.position, bulletSpawn.rotation);
			rafaga = false;
		}

		if(!vivo && !matar)
		{
			adelante = false;
			atras = false;
			der = false;
			izq = false;

			speedStep = 0.0f;
			giro = 0;

			freno = true;

			if(explo != null)
			{
				explo.SetActive(true);
			}

			Destroy(cubo);
			Destroy(gameObject.GetComponent<Animator>());

			torreta.transform.parent = null;

			if(!torreta.GetComponent<Rigidbody>())
			{
				torreta.AddComponent<Rigidbody>();
				torreta.GetComponent<Rigidbody>().mass = 10f;
				//torreta.GetComponent<Rigidbody>().AddTorque(transform.up * 2);
				torreta.layer = LayerMask.NameToLayer("muerto");
				torretaBase.layer = LayerMask.NameToLayer("muerto");
				//torreta.AddComponent<Rigidbody>().velocity = torreta.transform.up * 1;
			}

			StartCoroutine(ocultar());

			StartCoroutine(quitar());

			matar = true;
		}
	}

	//APAGA LA LUZ
	IEnumerator apaga ()
	{
		yield return new WaitForSeconds(0.1f);
		canon.GetComponent<Animator>().SetBool("disparo", false);
		luz.SetActive(false);
	}
	//APAGA LA LUZ
	IEnumerator carga ()
	{
		yield return new WaitForSeconds(5f);
		listo = true;
	}
	public int cosa;
	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "bala" && vivo)
		{
			salud -= col.gameObject.GetComponent<balaOffline>().poder;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = col.gameObject.GetComponent<balaOffline>().poder.ToString("F0");

			Destroy(col.gameObject);
		}
		if(col.gameObject.tag == "explo" && vivo)
		{
			salud -= col.gameObject.GetComponent<ExploOffline>().poder;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = col.gameObject.GetComponent<ExploOffline>().poder.ToString("F0");
		}
	}

	GameObject baseNeutra;
	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "newtra" && vivo || col.gameObject.tag == BaseMala && vivo)
		{
			freno = true;
		}

		if(col.gameObject.tag == NameEnemy && vivo)
		{
			Player = col.gameObject.transform;
		}
		if(col.gameObject.tag == NameEnemyTank && vivo)
		{
			Player = col.gameObject.transform;
		}

		if(col.gameObject.name == "BaseNeutra")
		{
			baseNeutra = col.gameObject;
		}
	}

	void OnTriggerStay (Collider col)
	{
		if(col.gameObject.tag == NameEnemy && vivo)
		{
			Player = col.gameObject.transform;
		}
		if(col.gameObject.tag == NameEnemyTank && vivo)
		{
			Player = col.gameObject.transform;
		}
	}

	void OnTriggerExit (Collider col)
	{
		if(col.gameObject.tag == NameEnemy)
		{
			Player = null;
		}
		if(col.gameObject.tag == NameEnemyTank)
		{
			Player = null;
		}
		if(col.gameObject.name == "BaseNeutra")
		{
			baseNeutra = null;
		}
	}
}
