using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class spawnSniperNetwork : NetworkBehaviour {

	public GameObject Mover;
	public GameObject seguir;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!isLocalPlayer)
		{
			return;
		}

		if(seguir == null)
		{
			seguir = GameObject.Find("balaSniper");
		}else
		{
			Mover.transform.position = Vector3.Lerp(transform.position, seguir.transform.position, Time.deltaTime * 100);
		}
	}
}
