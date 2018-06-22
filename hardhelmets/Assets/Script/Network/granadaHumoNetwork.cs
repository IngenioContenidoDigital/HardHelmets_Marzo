using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class granadaHumoNetwork : NetworkBehaviour {

	public GameObject avion;

	public ParticleSystem humo;

	public GameObject Player;

	bool destruir;

	public string lado;

	// Use this for initialization
	void Start ()
	{
		if(!isServer)
		{
			return;
		}

		if(lado == "R")
		{
			GetComponent<Rigidbody>().velocity = transform.up * 20;
			GetComponent<Rigidbody>().AddForce(transform.right * 50);
		}else
		{
			GetComponent<Rigidbody>().velocity = transform.up * 20;
			GetComponent<Rigidbody>().AddForce(transform.right * -50);
		}

		StartCoroutine(llamar());
	}

	// Update is called once per frame
	void Update () 
	{
		if(humo.isStopped && !destruir)
		{
			destruir = true;
			StartCoroutine(desaparecer());
		}
	}

	IEnumerator desaparecer()
	{
		yield return new WaitForSeconds(5);
		Destroy(gameObject);
	}

	IEnumerator llamar()
	{
		yield return new WaitForSeconds(5);
		Cmd_crearAvion();
	}

	[Command]
	public void Cmd_crearAvion()
	{
		var ataque = (GameObject)Instantiate(avion, transform.position, Quaternion.Euler(0,0,0));
		if(gameObject.tag == "Player")
		{
			ataque.GetComponent<avionNetwork>().poder = Player.GetComponent<HeroNetwork>().saludMax*ataque.GetComponent<avionNetwork>().poder/104;
		}else
		{
			ataque.GetComponent<avionNetwork>().poder = Player.GetComponent<HeroNetwork>().saludMax2*ataque.GetComponent<avionNetwork>().poder/104;
		}
		NetworkServer.Spawn(ataque);
	}
}
