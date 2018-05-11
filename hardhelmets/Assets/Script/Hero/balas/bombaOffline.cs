using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombaOffline : MonoBehaviour {

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
			Explo();
		}
	}

	public void Explo()
	{
		var explo = (GameObject)Instantiate(explocion, transform.position, Quaternion.identity);
		explo.GetComponent<ExploOffline>().poder = poder;
		Destroy(gameObject);
	}
}
