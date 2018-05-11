using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PoderNetwork : NetworkBehaviour {

	public float poder;

	public GameObject bomba1;
	public GameObject bomba2;
	public GameObject bomba3;
	public GameObject bomba4;
	public GameObject bomba5;
	public GameObject bomba6;
	public GameObject bomba7;
	public GameObject bomba8;
	public GameObject bomba9;
	public GameObject bomba10;
	public GameObject bomba11;

	public GameObject explocion;

	public GameObject destruir;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update ()
	{
		if(bomba1 == null && bomba2 == null && bomba3 == null && bomba4 == null && bomba5 == null && bomba6 == null && 
			bomba7 == null && bomba8 == null && bomba9 == null && bomba10 == null && bomba11 == null)
		{
			Destroy(gameObject);
		}
	}

	[Command]
	public void Cmd_Explo()
	{
		var explo = (GameObject)Instantiate(explocion, transform.position, Quaternion.identity);
		NetworkServer.Spawn(explo);
		explo.GetComponent<Explo>().poder = poder;
		Destroy(destruir);
	}
}
