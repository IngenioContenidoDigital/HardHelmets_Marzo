using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balaFuego : MonoBehaviour {

	public float poder;

	// Use this for initialization
	void Start ()
	{
		
	}

	void Update ()
	{
		
	}


	void OnCollisionEnter (Collision col)
	{
		Destroy(gameObject);
	}
}
