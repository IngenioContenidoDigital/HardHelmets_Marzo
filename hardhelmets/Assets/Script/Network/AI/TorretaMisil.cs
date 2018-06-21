using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AI;

public class TorretaMisil : NetworkBehaviour {

	public NavMeshAgent agent;

	public string enemyName;
	public string tankName;
	public Transform objetivo;

	Quaternion otro;

	public GameObject girar;

	[SyncVar]
	public float salud;
	[SyncVar]
	public float saludMax;

	public bool disparo;
	public bool disparando;

	public float zeta;

	public GameObject Explo;
	public GameObject textos;

	public GameObject bulletPref;

	public Transform[] bulletSpawn1;
	public Transform[] bulletSpawn2;
	public Transform bulletSpawnA;
	public Transform bulletSpawnB;
	public Transform bulletSpawnC;
	public Transform bulletSpawnD;
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

		StartCoroutine(Quitaragente());

		if(!isServer)
		{
			return;
		}

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
	}
	IEnumerator Quitaragente()
	{
		yield return new WaitForSeconds(0.02f);
		GetComponent<NavMeshObstacle>().enabled = true;
		Destroy(agent);
	}

	bool listo;
	// Update is called once per frame
	bool sumar;
	//PANEL PARTIDA
	GameObject Panel;

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
			objetivo = null;
			disparo = false;
			if(Panel.GetComponent<Game>().continuar)
			{
				print("DESTRUIR OBJETOS");
				CmdDestruir();
			}
		}

		if(salud <= 0)
		{
			if(!sumar)
			{
				if(gameObject.tag == "Player")
				{
					Panel.GetComponent<Game>().VechicleDestroyedM += 1;
				}else
				{
					Panel.GetComponent<Game>().VechicleDestroyedB += 1;
				}
				sumar = true;
			}
		}

		if(objetivo != null && salud > 0)
		{
			disparo = true;

			girar.transform.LookAt(objetivo);
			girar.transform.eulerAngles = new Vector3(0, girar.transform.eulerAngles.y, 0);
		}else
		{
			disparo = false;
		}

		if(disparo && !disparando)
		{
			disparando = true;

			bulletSpawnA = bulletSpawn1[Random.Range(0,bulletSpawn1.Length)];
			bulletSpawnB = bulletSpawn2[Random.Range(0,bulletSpawn2.Length)];

			audio1.volume = efectos;
			audio1.Play();

			luz.SetActive(true);
			StartCoroutine(apaga());

			CmdDisparo();
		}
		if(disparando && !listo)
		{
			listo = true;
			StartCoroutine(espera());
		}
	}
	bool rafaga;
	bool rafaga2;
	public GameObject fuego;
	public GameObject fuego2;
	void FixedUpdate ()
	{
		if(salud <= 0)
		{
			Explo.SetActive(true);
			StartCoroutine(matar());
		}
		if(rafaga)
		{
			var efecto = (GameObject)Instantiate(fuego, bulletSpawnC.position, bulletSpawnC.transform.rotation);
			var efecto2 = (GameObject)Instantiate(fuego2, bulletSpawnC.position, bulletSpawnC.transform.rotation);
			rafaga = false;
		}
		if(rafaga2)
		{
			var efecto = (GameObject)Instantiate(fuego, bulletSpawnD.position, bulletSpawnD.transform.rotation);
			var efecto2 = (GameObject)Instantiate(fuego2, bulletSpawnD.position, bulletSpawnD.transform.rotation);
			rafaga2 = false;
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
			salud -= col.gameObject.GetComponent<bala>().poder;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = col.gameObject.GetComponent<bala>().poder.ToString("F0");
		}
		if(col.gameObject.tag == "explo")
		{
			salud -= col.gameObject.GetComponent<Explo>().poder;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = col.gameObject.GetComponent<Explo>().poder.ToString("F0");
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
			col.gameObject.GetComponent<mina>().muerte = true;
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
	//APAGA LA LUZ
	IEnumerator apaga ()
	{
		yield return new WaitForSeconds(0.1f);
		luz.SetActive(false);
	}

	IEnumerator espera ()
	{
		yield return new WaitForSeconds(5);
		disparando = false;
		listo = false;
	}

	[Command]
	public void CmdDisparo()
	{
		StartCoroutine(disparo2());
		rafaga = true;

		var bullet = (GameObject)Instantiate(bulletPref, bulletSpawnA.position, Quaternion.Euler(bulletSpawnA.rotation.eulerAngles.x, bulletSpawnA.rotation.eulerAngles.y, bulletSpawnA.rotation.eulerAngles.z));//bulletSpawn.rotation);
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 100;
		NetworkServer.Spawn(bullet);
		bullet.GetComponent<balaPanzerNetwork>().poder = saludMax*bullet.GetComponent<balaPanzerNetwork>().poder/200;

		luz.SetActive(true);
	}
	IEnumerator disparo2 ()
	{
		yield return new WaitForSeconds(0.5f);
		CmdDisparo2();
	}
	[Command]
	public void CmdDisparo2()
	{
		rafaga2 = true;

		var bullet2 = (GameObject)Instantiate(bulletPref, bulletSpawnB.position, Quaternion.Euler(bulletSpawnB.rotation.eulerAngles.x, bulletSpawnB.rotation.eulerAngles.y, bulletSpawnB.rotation.eulerAngles.z));//bulletSpawn.rotation);
		bullet2.GetComponent<Rigidbody>().velocity = bullet2.transform.forward * 100;
		NetworkServer.Spawn(bullet2);
		bullet2.GetComponent<balaPanzerNetwork>().poder = saludMax*bullet2.GetComponent<balaPanzerNetwork>().poder/200;
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
