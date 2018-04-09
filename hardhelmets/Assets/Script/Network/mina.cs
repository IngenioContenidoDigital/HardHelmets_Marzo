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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
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
			CmdExplo();
			Destroy(gameObject);
		}

		if(col.gameObject.tag == "explo")
		{
			CmdExplo();
			Destroy(gameObject);
		}
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
}
