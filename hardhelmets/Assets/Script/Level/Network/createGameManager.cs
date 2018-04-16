using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class createGameManager : NetworkBehaviour {

	public GameObject Game;

	// Use this for initialization
	void Start ()
	{
		CmdCrear();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[Command]
	public void CmdCrear()
	{
		var sombrero = (GameObject)Instantiate(Game, transform.position, Quaternion.Euler(0,0,0));
		Game.name = "GAME";
	}
}
