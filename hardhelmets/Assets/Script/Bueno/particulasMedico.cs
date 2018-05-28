﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particulasMedico : MonoBehaviour {

	public AudioClip[] sonar;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnParticleCollision (GameObject other)
	{
		Rigidbody body = other.GetComponent<Rigidbody>();

		if(body)
		{
			GetComponent<AudioSource>().clip = sonar[Random.Range(0,sonar.Length)];
			GetComponent<AudioSource>().Play();
			//OFFLINE
			if(body.GetComponent<Hero>())
			{
				body.GetComponent<Hero>().SaludSumar();
			}
			if(body.GetComponent<AI>())
			{
				body.GetComponent<AI>().SaludSumar();
			}
			if(body.GetComponent<AIMetra>())
			{
				body.GetComponent<AIMetra>().SaludSumar();
			}
			if(body.GetComponent<AIMortero>())
			{
				body.GetComponent<AIMortero>().SaludSumar();
			}
			if(body.GetComponent<AIVikingo>())
			{
				body.GetComponent<AIVikingo>().SaludSumar();
			}
			//ONLINE
			if(body.GetComponent<HeroNetwork>())
			{
				body.GetComponent<HeroNetwork>().SaludSumar();
			}
			if(body.GetComponent<AINetwork>())
			{
				body.GetComponent<AINetwork>().SaludSumar();
			}
			if(body.GetComponent<AIMetraNetwork>())
			{
				body.GetComponent<AIMetraNetwork>().SaludSumar();
			}
			if(body.GetComponent<AIMorteroNetwork>())
			{
				body.GetComponent<AIMorteroNetwork>().SaludSumar();
			}
			if(body.GetComponent<AIMetraMaloNetwork>())
			{
				body.GetComponent<AIMetraMaloNetwork>().SaludSumar();
			}
			if(body.GetComponent<AIMorteroMaloNetwork>())
			{
				body.GetComponent<AIMorteroMaloNetwork>().SaludSumar();
			}
			if(body.GetComponent<AIVikingoNetwork>())
			{
				body.GetComponent<AIVikingoNetwork>().CmdSaludSumar();
			}
		}
	}
}
