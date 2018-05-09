using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class mina : NetworkBehaviour {

	[SyncVar]
	public float poder;

	public GameObject explocion;
	//ONDA
	public GameObject prefab;

	public string tank;

	public bool muerte;

	//PANEL PARTIDA
	GameObject Panel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Panel == null)
		{
			Panel = GameObject.Find("GAME");
		}
		if(Panel.GetComponent<Game>().final)
		{
			if(Panel.GetComponent<Game>().continuar)
			{
				print("DESTRUIR OBJETOS");
				CmdDestruir();
			}
		}

		if(muerte)
		{
			CmdExplo();
			muerte = false;
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == tank)
		{
			GetComponent<Animator>().SetBool("explotar", true);
		}

		if(col.gameObject.tag == "explo")
		{
			GetComponent<Animator>().SetBool("explotar", true);
		}
		if(col.gameObject.tag == "bala")
		{
			GetComponent<Animator>().SetBool("explotar", true);
		}
	}

	public void sonar()
	{
		GetComponent<AudioSource>().Play();
	}

	public void Explotar()
	{
		CmdExplo();
	}

	[Command]
	public void CmdExplo()
	{
		var explo = (GameObject)Instantiate(explocion, transform.position, Quaternion.identity);
		var onda = (GameObject)Instantiate(prefab, transform.position, Quaternion.identity);
		NetworkServer.Spawn(explo);
		explo.GetComponent<Explo>().poder = poder;
		NetworkServer.Spawn(onda);
		NetworkServer.Destroy(gameObject);
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
