using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CreateHudGame : NetworkBehaviour {

	[SyncVar]
	public GameObject Player1;
	[SyncVar]
	public GameObject Player2;

	public GameObject Game;

	public GameObject mensaje;

	public bool crear;

	// Use this for initialization
	void Start () {
		
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
			CmdCreateGame();
			mensaje.SetActive(false);
			crear = true;
		}
	}

	[Command]
	public void CmdCreateGame()
	{
		var juego = (GameObject)Instantiate(Game, transform.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(juego);
	}
}
