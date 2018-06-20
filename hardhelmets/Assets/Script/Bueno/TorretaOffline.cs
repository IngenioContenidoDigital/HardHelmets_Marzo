using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TorretaOffline : MonoBehaviour {

	public NavMeshAgent agent;

	public Animator animator;

	public string enemyName;
	public string tankName;
	public Transform objetivo;

	Quaternion otro;

	public GameObject girar;

	public float salud;

	public float saludMax;

	public bool disparo;
	public bool disparando;

	public float zeta;

	public GameObject Explo;
	public GameObject textos;

	public GameObject bulletPrefFusil;

	public Transform bulletSpawn;
	public GameObject casquilloPref;
	public Transform casquilloSpawn;
	public GameObject luz;

	//VOLUMEN
	float efectos;

	public AudioSource audio1;

	//NIVEL DE CARTA
	public int level;
	public GameObject Jugador;

	void Start ()
	{
		efectos = PlayerPrefs.GetFloat("efects");

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

		saludMax = 8*level+200;
		salud = saludMax;

		StartCoroutine(Quitaragente());
	}

	IEnumerator Quitaragente()
	{
		yield return new WaitForSeconds(0.02f);
		GetComponent<NavMeshObstacle>().enabled = true;
		Destroy(agent);
	}

	// Update is called once per frame
	public float vel;
	bool sumar;
	//PANEL PARTIDA
	GameObject Panel;
	void Update ()
	{
		if(Panel == null)
		{
			Panel = GameObject.Find("GAME");
		}
		if(Panel.GetComponent<GameOffline>().final)
		{
			objetivo = null;
			disparo = false;
		}

		if(salud <= 0)
		{
			if(!sumar)
			{
				if(gameObject.tag == "Player")
				{
					Panel.GetComponent<GameOffline>().VechicleDestroyedM += 1;
				}else
				{
					Panel.GetComponent<GameOffline>().VechicleDestroyedB += 1;
				}
				sumar = true;
			}
		}

		if(animator.GetCurrentAnimatorStateInfo(0).IsName("auto_turret_machine_gun_level_1_fire"))
		{
			animator.SetBool("disparo", false);
		}

		if(objetivo != null && salud > 0)
		{
			disparo = true;

			//girar.transform.LookAt(objetivo);
			//girar.transform.eulerAngles = new Vector3(0, girar.transform.eulerAngles.y, 0);

			Vector3 Target = objetivo.position - girar.transform.position;
			girar.transform.rotation = Quaternion.RotateTowards(girar.transform.rotation, Quaternion.LookRotation(Target), Time.time * vel);
			girar.transform.eulerAngles = new Vector3(0, girar.transform.eulerAngles.y, 0);
		}else
		{
			disparo = false;
			animator.SetBool("disparo", false);
		}

		if(disparo && !disparando)
		{
			animator.SetBool("disparo", true);
			disparando = true;
		}
	}
	bool rafaga;
	public GameObject fuego;
	public Transform rafagaSpawn;
	void FixedUpdate ()
	{
		if(salud <= 0)
		{
			Explo.SetActive(true);
			StartCoroutine(matar());
		}
		if(rafaga)
		{
			var efecto = (GameObject)Instantiate(fuego, rafagaSpawn.position, rafagaSpawn.transform.rotation);
			rafaga = false;
		}
	}

	IEnumerator matar ()
	{
		yield return new WaitForSeconds(2);
		Destroy(gameObject);
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "bala")
		{
			salud -= col.gameObject.GetComponent<balaOffline>().poder;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = col.gameObject.GetComponent<balaOffline>().poder.ToString("F0");
		}

		if(col.gameObject.tag == "explo")
		{
			salud -= col.gameObject.GetComponent<ExploOffline>().poder;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = col.gameObject.GetComponent<ExploOffline>().poder.ToString("F0");
		}
	}

	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == enemyName)
		{
			objetivo = col.gameObject.transform;
		}
		if(col.gameObject.tag == tankName)
		{
			objetivo = col.gameObject.transform;
		}
		if(col.gameObject.tag == "mina")
		{
			col.gameObject.GetComponent<MinaOffline>().muerte = true;
		}
		if(col.gameObject.tag == "mira")
		{
			mira.SetActive(true);
			mira.GetComponent<Animator>().SetBool("entry",true);
		}
	}
	void OnTriggerStay (Collider col)
	{
		if(col.gameObject.tag == enemyName)
		{
			objetivo = col.gameObject.transform;
		}
		if(col.gameObject.tag == tankName)
		{
			objetivo = col.gameObject.transform;
		}
	}
	void OnTriggerExit (Collider col)
	{
		if(col.gameObject.tag == enemyName)
		{
			objetivo = null;
		}
		if(col.gameObject.tag == tankName)
		{
			objetivo = null;
		}
		if(col.gameObject.tag == "mira")
		{
			mira.SetActive(false);
		}
	}
	public GameObject mira;

	public void disparar ()
	{
		disparando = false;
	}

	//APAGA LA LUZ
	IEnumerator apaga ()
	{
		yield return new WaitForSeconds(0.1f);
		luz.SetActive(false);
	}

	public void Shot ()
	{
		Disparo();

		audio1.volume = efectos;
		audio1.Play();

		rafaga = true;

		luz.SetActive(true);
		StartCoroutine(apaga());
	}
		
	public void Disparo()
	{
		var bullet = (GameObject)Instantiate(bulletPrefFusil, bulletSpawn.position, Quaternion.Euler(bulletSpawn.rotation.eulerAngles.x, bulletSpawn.rotation.eulerAngles.y, bulletSpawn.rotation.eulerAngles.z));//bulletSpawn.rotation);
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 100;

		bullet.GetComponent<balaOffline>().poder = saludMax*bullet.GetComponent<balaOffline>().poder/200;
		//CASQUILLOS
		var casquillo = (GameObject)Instantiate(casquilloPref, casquilloSpawn.position, casquilloSpawn.rotation); 
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.right * Random.Range(-20,21);
		casquillo.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(-0.0f,-0.2f));
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.up * Random.Range(3,7);
	}
}
