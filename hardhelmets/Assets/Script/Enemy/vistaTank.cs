using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vistaTank : MonoBehaviour {

	public bool obstacle;

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "pared")
		{
			obstacle = true;
		}
		if(col.gameObject.tag == "Frente")
		{
			obstacle = true;
		}
		if(col.gameObject.tag == "obstaculo1")
		{
			obstacle = true;
		}
		if(col.gameObject.tag == "obstaculo2")
		{
			obstacle = true;
		}
		if(col.gameObject.name == "PuazD")
		{
			obstacle = true;
		}
	}

	void OnTriggerStay (Collider col)
	{
		if(col.gameObject.tag == "pared")
		{
			obstacle = true;
		}
		if(col.gameObject.tag == "Frente")
		{
			obstacle = true;
		}
		if(col.gameObject.tag == "obstaculo1")
		{
			obstacle = true;
		}
		if(col.gameObject.tag == "obstaculo2")
		{
			obstacle = true;
		}
		if(col.gameObject.name == "PuazD")
		{
			obstacle = true;
		}
	}

	void OnTriggerExit (Collider col)
	{
		if(col.gameObject.tag == "pared")
		{
			obstacle = false;
		}
		if(col.gameObject.tag == "Frente")
		{
			obstacle = false;
		}
		if(col.gameObject.tag == "obstaculo1")
		{
			obstacle = false;
		}
		if(col.gameObject.tag == "obstaculo2")
		{
			obstacle = false;
		}
		if(col.gameObject.name == "PuazD")
		{
			obstacle = false;
		}
	}
}
