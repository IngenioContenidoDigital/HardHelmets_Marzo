using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balaAvion : MonoBehaviour {

	public GameObject padre;

	public float poder;

	// Use this for initialization
	void Start ()
	{
		if(padre.GetComponent<avion>())
		{
			poder = padre.GetComponent<avion>().poder;
		}
		if(padre.GetComponent<avionNetwork>())
		{
			poder = padre.GetComponent<avionNetwork>().poder;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnParticleCollision (GameObject other)
	{
		Rigidbody body = other.GetComponent<Rigidbody>();

		if(body)
		{
			//OFFLINE
			if(body.GetComponent<Hero>())
			{
				body.GetComponent<Hero>().salud -= poder;
			}
			if(body.GetComponent<AI>())
			{
				body.GetComponent<AI>().salud -= poder;
			}
			if(body.GetComponent<AIPanzer>())
			{
				body.GetComponent<AIPanzer>().salud -= poder;
			}
			if(body.GetComponent<AIMetra>())
			{
				body.GetComponent<AIMetra>().salud -= poder;
			}
			if(body.GetComponent<AIMortero>())
			{
				body.GetComponent<AIMortero>().salud -= poder;
			}
			//ONLINE
			if(body.GetComponent<HeroNetwork>())
			{
				body.GetComponent<HeroNetwork>().salud -= poder;
			}
			if(body.GetComponent<AINetwork>())
			{
				body.GetComponent<AINetwork>().salud -= poder;
			}
			if(body.GetComponent<AIPanzerNetwork>())
			{
				body.GetComponent<AIPanzerNetwork>().salud -= poder;
			}
			if(body.GetComponent<AIMetraNetwork>())
			{
				body.GetComponent<AIMetraNetwork>().salud -= poder;
			}
			if(body.GetComponent<AIMorteroNetwork>())
			{
				body.GetComponent<AIMorteroNetwork>().salud -= poder;
			}
			if(body.GetComponent<AIMetraMaloNetwork>())
			{
				body.GetComponent<AIMetraMaloNetwork>().salud -= poder;
			}
			if(body.GetComponent<AIMorteroMaloNetwork>())
			{
				body.GetComponent<AIMorteroMaloNetwork>().salud -= poder;
			}
		}
	}
}
