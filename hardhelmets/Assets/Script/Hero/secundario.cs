using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secundario : MonoBehaviour {

	public Transform Player;

	//public Transform objetivo;

	public GameObject camara;

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		/*if(objetivo != null)
		{
			camara.GetComponent<CamNetwork>().objetivo = true;
		}else
		{
			camara.GetComponent<CamNetwork>().objetivo = false;
		}*/

	}

	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "enemy" || col.gameObject.tag == "enemyTank" || col.gameObject.tag == "enemyBase")
		{
			camara.GetComponent<Cam>().objetivo = true;
		}
	}
	void OnTriggerStay (Collider col)
	{
		if(col.gameObject.tag == "enemy" || col.gameObject.tag == "enemyTank" || col.gameObject.tag == "enemyBase")
		{
			camara.GetComponent<Cam>().objetivo = true;
		}
	}
	void OnTriggerExit (Collider col)
	{
		if(col.gameObject.tag == "enemy" || col.gameObject.tag == "enemyTank" || col.gameObject.tag == "enemyBase")
		{
			camara.GetComponent<Cam>().objetivo = false;
		}
	}
}
