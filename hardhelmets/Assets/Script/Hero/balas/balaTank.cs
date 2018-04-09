using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balaTank : MonoBehaviour {

	public GameObject explocion;
	//ONDA
	public GameObject prefab;

	public float poder;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter (Collision col)
	{
		Explo();
	}
		
	public void Explo()
	{
		var explo = (GameObject)Instantiate(explocion, transform.position, Quaternion.identity);
		var onda = (GameObject)Instantiate(prefab, transform.position, Quaternion.identity);

		explo.GetComponent<ExploOffline>().poder = poder;
		Destroy(gameObject);
	}
}
