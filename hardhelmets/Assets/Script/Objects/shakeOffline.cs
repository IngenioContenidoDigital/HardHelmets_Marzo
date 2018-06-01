using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shakeOffline : MonoBehaviour {

	public GameObject SniperCam;

	// Use this for initialization
	void Start ()
	{
		SniperCam.GetComponent<Cam>().shake = true;
		SniperCam.GetComponent<Cam>().vib = 0.7f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
