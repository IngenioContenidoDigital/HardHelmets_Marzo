using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class bomba : NetworkBehaviour {

	public GameObject padre;

	public float poder;

	public GameObject explocion;

	// Use this for initialization
	void Start ()
	{
		poder = padre.GetComponent<Poder>().poder;
	}

	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "Piso")
		{
			Cmd_Explo();
		}
	}
	[Command]
	public void Cmd_Explo()
	{
		var explo = (GameObject)Instantiate(explocion, transform.position, Quaternion.identity);
		NetworkServer.Spawn(explo);
		explo.GetComponent<Explo>().poder = poder;
		Destroy(gameObject);
	}
}
