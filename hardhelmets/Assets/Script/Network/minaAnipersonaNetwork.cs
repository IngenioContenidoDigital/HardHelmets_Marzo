﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class minaAnipersonaNetwork : NetworkBehaviour {

	public Animator animator;

	public GameObject mina;

	public float poder;

	public GameObject explocion;
	public GameObject explocion2;
	//ONDA
	public GameObject prefab;

	public AudioSource audio1;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "Player")
		{
			animator.GetComponent<Animator>().SetBool("explo", true);
			audio1.Play();
		}

		if(col.gameObject.tag == "enemy")
		{
			animator.GetComponent<Animator>().SetBool("explo", true);
			audio1.Play();
		}

		if(col.gameObject.tag == "explo")
		{
			animator.GetComponent<Animator>().SetBool("explo", true);
			audio1.Play();
			//Cmd_Explo2();
		}
		if(col.gameObject.tag == "bala")
		{
			animator.GetComponent<Animator>().SetBool("explo", true);
			audio1.Play();
		}
	}

	public void Explo()
	{
		explocion.SetActive(true);//var explo = (GameObject)Instantiate(explocion, mina.transform.position, Quaternion.identity);
		explocion.transform.parent = null;
		var onda = (GameObject)Instantiate(prefab, mina.transform.position, Quaternion.identity);

		explocion.GetComponent<Explo>().poder = poder;

		Destroy(gameObject);
	}

	[Command]
	public void Cmd_Explo2()
	{
		var explo = (GameObject)Instantiate(explocion2, transform.position, Quaternion.identity);
		var onda = (GameObject)Instantiate(prefab, transform.position, Quaternion.identity);

		explo.GetComponent<Explo>().poder = poder;

		Destroy(gameObject);
	}
}
