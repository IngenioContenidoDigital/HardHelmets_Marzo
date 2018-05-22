﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animacionesVikingoOffline : MonoBehaviour {

	public Animator animator;

	public GameObject polv;
	public GameObject gota;

	public Transform PasoD;
	public Transform PasoI;
	public GameObject pasopolvo;
	public GameObject pasopolvo2;

	public GameObject strikearma;
	public Transform strikeSpawn;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update ()
	{
		if(!animator.GetCurrentAnimatorStateInfo(0).IsName("dispara") && GetComponent<AIVikingo>().disparando)
		{
			GetComponent<AIVikingo>().disparando = false;
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
		{
			//GetComponent<AI>().actuando = false;
		}

		if(animator.GetCurrentAnimatorStateInfo(0).IsName("caminar"))
		{
			animator.SetBool("atras", false);
		}

		if(animator.GetCurrentAnimatorStateInfo(0).IsName("dispara"))
		{
			animator.SetBool("walk", false);
			animator.SetBool("disparando", true);
			animator.SetBool("cascado", false);
			animator.SetBool("disparar", false);
		}else
		{
			animator.SetBool("disparando", false);
		}

		if(animator.GetCurrentAnimatorStateInfo(0).IsName("caminarreversa"))
		{
			animator.SetBool("caminar", false);
			animator.SetBool("disparar", false);
			animator.SetBool("atras", false);
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("golpeado") || animator.GetCurrentAnimatorStateInfo(0).IsName("vuela"))
		{
			animator.SetBool("cascado", false);
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("golpe"))
		{
			animator.SetBool("golpe", false);
			animator.SetBool("caminar", false);
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("cae"))
		{
			animator.SetBool("muerto2", true);
			animator.SetBool("muerto", false);
		}
	}

	//EVENTO SPINE
	void polvo ()
	{
		if(!GetComponent<AIVikingo>().water)
		{
			var efect = (GameObject)Instantiate(polv, transform.position, transform.rotation);
		}
	}

	void paso()
	{
		if(!GetComponent<AIVikingo>().water)
		{
			var efect = (GameObject)Instantiate(pasopolvo, PasoD.transform.position, transform.rotation);
		}else
		{
			var efect = (GameObject)Instantiate(gota, transform.position, transform.rotation);
		}
	}
	void paso2()
	{
		if(!GetComponent<AIVikingo>().water)
		{
			var efect = (GameObject)Instantiate(pasopolvo2, PasoI.transform.position, transform.rotation);
		}else
		{
			var efect = (GameObject)Instantiate(gota, transform.position, transform.rotation);
		}
	}
	void rafaga ()
	{
		if(transform.localScale.x == 1.13f)
		{
			var efect = (GameObject)Instantiate(strikearma, strikeSpawn.transform.position, Quaternion.Euler(0,0,-90));
		}else
		{
			var efect = (GameObject)Instantiate(strikearma, strikeSpawn.transform.position, Quaternion.Euler(0,0,90));
		}
	}
}
