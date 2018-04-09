using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour {

	//HUD
	public GameObject Game;

	//PERSONAJES
	public GameObject Player1;
	public GameObject Player2;

	bool crear;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Player1 == null)
		{
			Player1 = GameObject.Find("Hero");
		}
		if(Player2 == null)
		{
			Player2 = GameObject.Find("Hero2");
		}

		if(Player1 != null && Player2 != null && !crear)
		{
			Crear();
			crear = true;
		}
	}

	public void Crear()
	{
		var objeto = (GameObject)Instantiate(Game, transform.position, transform.rotation);
		NetworkServer.Spawn(objeto);
	}

	/*public override void OnStartServer()
	{
		var objeto = (GameObject)Instantiate(Game, transform.position, transform.rotation);
		NetworkServer.Spawn(objeto);
	}*/
}
