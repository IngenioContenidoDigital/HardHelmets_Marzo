﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balaOffline : MonoBehaviour {

	public GameObject efecto;
	int efect;
	public GameObject humo;
	public GameObject[] piedras;

	Vector3 OrPos;
	Vector3 movDirection;

	public int cantidad;

	public float poder;

	// Use this for initialization
	void Start ()
	{
		OrPos = transform.position;
		GetComponent<AudioSource>().pitch = Random.Range(0.8f,1.3f);
	}

	void Update ()
	{
		movDirection = gameObject.transform.position - OrPos;
		float angle = Mathf.Atan2(movDirection.y, movDirection.x) * Mathf.Rad2Deg;

		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}


	void OnCollisionEnter (Collision col)
	{
		efect = Random.Range(1,3);
		if(col.gameObject.tag == "Player" || col.gameObject.tag == "enemy" || col.gameObject.tag == "CABEZA")
		{
			var explo = (GameObject)Instantiate(efecto, col.gameObject.transform.position, transform.rotation);
			explo.GetComponent<Animator>().SetInteger("efect",efect);
			Destroy(explo, 2.0f);
		}else
		{
			//Shot();

			var explo = (GameObject)Instantiate(efecto, transform.position, transform.rotation);
			explo.GetComponent<Animator>().SetInteger("efect",efect);
			Destroy(explo, 2.0f);
		}
		humo.transform.parent = null;
		Destroy(humo,3);
		Destroy(gameObject);
	}
		
	public void Shot()
	{
		if(cantidad == 1)
		{
			var roca1 = (GameObject)Instantiate(piedras[Random.Range(0,piedras.Length)], transform.position, transform.rotation);

		}

		if(cantidad == 2)
		{
			var roca1 = (GameObject)Instantiate(piedras[Random.Range(0,piedras.Length)], transform.position, transform.rotation);


			var roca2 = (GameObject)Instantiate(piedras[Random.Range(0,piedras.Length)], transform.position, transform.rotation);

		}

		if(cantidad == 3)
		{
			var roca1 = (GameObject)Instantiate(piedras[Random.Range(0,piedras.Length)], transform.position, transform.rotation);


			var roca2 = (GameObject)Instantiate(piedras[Random.Range(0,piedras.Length)], transform.position, transform.rotation);


			var roca3 = (GameObject)Instantiate(piedras[Random.Range(0,piedras.Length)], transform.position, transform.rotation);

		}
	}
}
