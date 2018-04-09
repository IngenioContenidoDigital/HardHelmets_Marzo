using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class balaPanzerNetwork : NetworkBehaviour {

	public GameObject explocion;
	bool girar;
	bool fuerza;

	[SyncVar]
	public float poder;

	// Use this for initialization
	void Start ()
	{
		//GetComponent<Rigidbody>().velocity = transform.right * 1;
		StartCoroutine(vuelta());
	}
	
	// Update is called once per frame
	void Update ()
	{
		GetComponent<Rigidbody>().velocity = transform.right * 40;
		if(girar)
		{
			transform.Rotate(new Vector3(0,0,300) * Time.deltaTime);
			StartCoroutine(parar());
		}
		if(fuerza)
		{
			GetComponent<Rigidbody>().velocity = transform.right * 60;
			StartCoroutine(dest());
		}
	}

	IEnumerator parar()
	{
		yield return new WaitForSeconds(1.12f);
		girar = false;
		fuerza = true;
	}
	IEnumerator vuelta()
	{
		yield return new WaitForSeconds(0.3f);
		girar = true;
	}
	IEnumerator dest()
	{
		yield return new WaitForSeconds(2f);
		CmdExplo();
	}

	void OnCollisionEnter (Collision col)
	{
		CmdExplo();
	}

	[Command]
	public void CmdExplo()
	{
		var explo = (GameObject)Instantiate(explocion, transform.position, Quaternion.identity);
		NetworkServer.Spawn(explo);
		explo.GetComponent<Explo>().poder = poder;
		Destroy(gameObject);
	}
}
