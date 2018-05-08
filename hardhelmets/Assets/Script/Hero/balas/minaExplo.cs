using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minaExplo : MonoBehaviour {

	public GameObject padre;

	public float poder;

	// Use this for initialization
	void Start ()
	{
		if(padre.GetComponent<minaAnipersonaNetwork>())
		{
			poder = padre.GetComponent<minaAnipersonaNetwork>().poder;
		}else
		{
			poder = padre.GetComponent<minaAntipersona>().poder;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Explo()
	{
		if(padre.GetComponent<minaAnipersonaNetwork>())
		{
			padre.GetComponent<minaAnipersonaNetwork>().Explo();
		}else
		{
			padre.GetComponent<minaAntipersona>().Explo();
		}
	}
}
