using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinaOffline : MonoBehaviour {

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
			Explo();
			muerte = false;
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == tank)
		{
			Explo();
			Destroy(gameObject);
		}

		if(col.gameObject.tag == "explo")
		{
			Explo();
			Destroy(gameObject);
		}
		if(col.gameObject.tag == "bala")
		{
			Explo();
			Destroy(gameObject);
		}
	}
		
	public void Explo()
	{
		var explo = (GameObject)Instantiate(explocion, transform.position, Quaternion.identity);
		var onda = (GameObject)Instantiate(prefab, transform.position, Quaternion.identity);

		explo.GetComponent<ExploOffline>().poder = poder;

		Destroy(gameObject);
	}
}
