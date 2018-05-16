﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class granadaHumo : MonoBehaviour {

	public GameObject avion;

	public ParticleSystem humo;

	bool destruir;

	// Use this for initialization
	void Start ()
	{
		GetComponent<Rigidbody>().velocity = transform.up * 20;
		GetComponent<Rigidbody>().AddForce(transform.right * 50);

		StartCoroutine(llamar());
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(humo.isStopped && !destruir)
		{
			destruir = true;
			StartCoroutine(desaparecer());
		}
	}

	IEnumerator desaparecer()
	{
		yield return new WaitForSeconds(5);
		Destroy(gameObject);
	}

	IEnumerator llamar()
	{
		yield return new WaitForSeconds(5);
		var ataque = (GameObject)Instantiate(avion, transform.position, Quaternion.Euler(0,0,0));
	}
}
