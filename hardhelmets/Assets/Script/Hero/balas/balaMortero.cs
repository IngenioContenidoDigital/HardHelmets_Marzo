using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balaMortero : MonoBehaviour {

	public GameObject explo;

	//ONDA
	public GameObject prefab;

	public float poder;

	void Start ()
	{

	}

	//EVENTOS SPINE
	void bomba ()
	{
		Explo();
	}
		
	public void Explo()
	{
		var explocion = (GameObject)Instantiate(explo, transform.position, Quaternion.identity);
		var onda = (GameObject)Instantiate(prefab, transform.position, Quaternion.identity);

		explo.GetComponent<Explo>().poder = poder;
	}
}
