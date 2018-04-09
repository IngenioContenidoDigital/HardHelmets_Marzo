using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSniper : MonoBehaviour {

	public GameObject Mover;
	public GameObject seguir;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update ()
	{
		if(seguir == null)
		{
			seguir = GameObject.Find("balaSniper");
		}else
		{
			Mover.transform.position = Vector3.Lerp(transform.position, seguir.transform.position, Time.deltaTime * 100);
		}
	}
}
