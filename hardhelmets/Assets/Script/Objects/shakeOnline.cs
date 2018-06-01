using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shakeOnline : MonoBehaviour {

	public GameObject Base;

	// Use this for initialization
	void Start ()
	{
		Base.GetComponent<Base>().camara.GetComponent<CamNetwork>().shake = true;
		Base.GetComponent<Base>().camara.GetComponent<CamNetwork>().vib = 0.7f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
