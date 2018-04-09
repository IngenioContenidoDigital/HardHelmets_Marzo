using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buscador : MonoBehaviour {

	public GameObject objeto;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "enemy" || col.gameObject.tag == "enemyTank" || col.gameObject.tag == "enemyBase")
		{
			//objeto.GetComponent<secundario>().objetivo = col.gameObject.transform;
		}
	}

	void OnTriggerExit (Collider col)
	{
		if(col.gameObject.tag == "enemy" || col.gameObject.tag == "enemyTank" || col.gameObject.tag == "enemyBase")
		{
			//objeto.GetComponent<secundario>().objetivo = null;
		}
	}

}
