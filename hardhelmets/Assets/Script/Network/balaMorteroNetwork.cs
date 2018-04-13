using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class balaMorteroNetwork : NetworkBehaviour {

	public GameObject explo;

	//ONDA
	public GameObject prefab;

	[SyncVar]
	public float poder;

	void Start ()
	{
		
	}

	//EVENTOS SPINE
	void bomba ()
	{
		CmdExplo();
	}

	[Command]
	public void CmdExplo()
	{
		var explocion = (GameObject)Instantiate(explo, transform.position, Quaternion.identity);
		var onda = (GameObject)Instantiate(prefab, transform.position, Quaternion.identity);
		NetworkServer.Spawn(explocion);
		explo.GetComponent<Explo>().poder = poder;
		NetworkServer.Spawn(onda);
		Destroy(gameObject);
	}
}
