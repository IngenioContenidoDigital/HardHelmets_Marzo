﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Base : NetworkBehaviour {

	[SyncVar]
	public float sangre;

	[SyncVar]
	public float saludMax = 2000;

	public GameObject textos;

	public GameObject luz;

	public GameObject fuego1;
	public GameObject fuego2;
	public GameObject fuego3;
	public GameObject fuego4;

	public GameObject destruida;

	public AudioSource audio1;

	public GameObject camara;

	void Start ()
	{
		saludMax = 2000;
		sangre = saludMax;
	}

	public GameObject Panel;

	void Update ()
	{
		if(camara == null)
		{
			camara = GameObject.FindGameObjectWithTag("MainCamera");
		}

		if(sangre <= saludMax*70/100)
		{
			fuego1.SetActive(true);
		}
		if(sangre <= saludMax*50/100)
		{
			fuego2.SetActive(true);
		}
		if(sangre <= saludMax*30/100)
		{
			fuego3.SetActive(true);
		}
		if(sangre <= saludMax*10/100)
		{
			fuego4.SetActive(true);
		}

		if(sangre <= 0 && !matada)
		{
			matada = true;
			StartCoroutine(momentito());
		}

		if(sangre > 0)
		{
			matada = false;
		}

		if(!isServer)
		{
			if(Panel == null)
			{
				Panel = GameObject.Find("GAME");
			}else
			{
				if(gameObject.name == "BASE")
				{
					sangre = Panel.GetComponent<Game>().sagreBB*saludMax;
				}else
				{
					sangre = Panel.GetComponent<Game>().sagreBM*saludMax;
				}
			}

			print("SOY LA BASE EN EL CLIENTE");
		}

	}

	public bool matada;

	IEnumerator momentito()
	{
		yield return new WaitForSeconds(1f);
		GetComponent<Animator>().SetBool("muere", true);
		audio1.Play();
		destruida.SetActive(true);
	}

	void OnCollisionEnter (Collision col)
	{
		if(isServer)
		{
			if(col.gameObject.tag == "bala")
			{
				sangre -= col.gameObject.GetComponent<bala>().poder;

				var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
				letras.GetComponent<TextMesh>().text = col.gameObject.GetComponent<bala>().poder.ToString("F0");
			}

			if(col.gameObject.tag == "explo")
			{
				sangre -= col.gameObject.GetComponent<Explo>().poder;

				var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
				letras.GetComponent<TextMesh>().text = col.gameObject.GetComponent<Explo>().poder.ToString("F0");
			}
		}
	}

	public void vivracion()
	{
		camara.GetComponent<CamNetwork>().shake = true;
	}
}
