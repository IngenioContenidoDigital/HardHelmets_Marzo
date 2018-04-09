using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class balaTankNetwork : NetworkBehaviour {

	public GameObject explocion;
	//ONDA
	public GameObject prefab;

	[SyncVar]
	public float poder;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter (Collision col)
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
}
