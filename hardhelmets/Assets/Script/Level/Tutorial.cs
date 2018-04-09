using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Tutorial : MonoBehaviour {

	public GameObject dialogos;
	public GameObject window;

	//ESCRITURA


	// Use this for initialization
	void Start ()
	{
		
	}

	
	// Update is called once per frame
	void Update () 
	{

	}

	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.name == "Hero")
		{
			col.gameObject.GetComponent<Hero>().caminarA = false;
			col.gameObject.GetComponent<Hero>().caminarU = false;
			col.gameObject.GetComponent<Hero>().caminarD = false;
			col.gameObject.GetComponent<Hero>().caminarI = false;

			col.gameObject.GetComponent<Hero>().ready = false;
			dialogos.SetActive(true);
			window.SetActive(true);
			Destroy(gameObject);
		}
	}
}
