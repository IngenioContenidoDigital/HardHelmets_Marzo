using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class bomba : NetworkBehaviour {

	public GameObject padre;

	public float poder;

	// Use this for initialization
	void Start ()
	{
		poder = padre.GetComponent<PoderNetwork>().poder;
	}

	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnCollisionEnter (Collision col)
	{
		if(!padre.GetComponent<NetworkIdentity>().isServer)
		{
			return;
		}
		if(col.gameObject.tag == "Piso")
		{
			padre.GetComponent<PoderNetwork>().Cmd_Explo();
			padre.GetComponent<PoderNetwork>().destruir = gameObject;
		}
	}
}
