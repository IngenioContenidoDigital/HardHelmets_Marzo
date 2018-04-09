using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class balaSniper : NetworkBehaviour {

	[SyncVar]
	public float poder;

	// Use this for initialization
	void Start ()
	{
		print(poder);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
