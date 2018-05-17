using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secundarioNetwork : MonoBehaviour {

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
		if(Player.tag == "Player")
		{
			if(col.gameObject.tag == "enemy" || col.gameObject.tag == "enemyTank" || col.gameObject.tag == "enemyBase" || col.gameObject.tag == "base")
			{
				camara.GetComponent<CamNetwork>().objetivo = true;
			}
		}else
		{
			if(col.gameObject.tag == "Player" || col.gameObject.tag == "tank" || col.gameObject.tag == "base" || col.gameObject.tag == "enemyBase")
			{
				camara.GetComponent<CamNetwork>().objetivo = true;
			}
		}
	}
	void OnTriggerStay (Collider col)
	{
		if(Player.tag == "Player")
		{
			if(col.gameObject.tag == "enemy" || col.gameObject.tag == "enemyTank" || col.gameObject.tag == "enemyBase" || col.gameObject.tag == "base")
			{
				camara.GetComponent<CamNetwork>().objetivo = true;
			}
		}else
		{
			if(col.gameObject.tag == "Player" || col.gameObject.tag == "tank" || col.gameObject.tag == "base" || col.gameObject.tag == "enemyBase")
			{
				camara.GetComponent<CamNetwork>().objetivo = true;
			}
		}
	}
	void OnTriggerExit (Collider col)
	{
		if(Player.tag == "Player")
		{
			if(col.gameObject.tag == "enemy" || col.gameObject.tag == "enemyTank" || col.gameObject.tag == "enemyBase" || col.gameObject.tag == "base")
			{
				camara.GetComponent<CamNetwork>().objetivo = false;
			}
		}else
		{
			if(col.gameObject.tag == "Player" || col.gameObject.tag == "tank" || col.gameObject.tag == "base" || col.gameObject.tag == "enemyBase")
			{
				camara.GetComponent<CamNetwork>().objetivo = false;
			}
		}
	}
}
