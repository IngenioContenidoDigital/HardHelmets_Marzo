using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCasquillo : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnParticleCollision (GameObject other)
	{
		GetComponent<AudioSource>().Play();
	}
}
