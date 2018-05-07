using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minaExplo : MonoBehaviour {

	public GameObject padre;

	public float poder;

	// Use this for initialization
	void Start ()
	{
		poder = padre.GetComponent<minaAntipersona>().poder;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Explo()
	{
		padre.GetComponent<minaAntipersona>().Explo();
	}
}
