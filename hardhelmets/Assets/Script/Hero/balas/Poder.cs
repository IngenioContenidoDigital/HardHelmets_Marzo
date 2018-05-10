using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poder : MonoBehaviour {

	public float poder;

	public GameObject bomba1;
	public GameObject bomba2;
	public GameObject bomba3;
	public GameObject bomba4;
	public GameObject bomba5;
	public GameObject bomba6;
	public GameObject bomba7;
	public GameObject bomba8;
	public GameObject bomba9;
	public GameObject bomba10;
	public GameObject bomba11;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(bomba1 == null && bomba2 == null && bomba3 == null && bomba4 == null && bomba5 == null && bomba6 == null && 
			bomba7 == null && bomba8 == null && bomba9 == null && bomba10 == null && bomba11 == null)
		{
			Destroy(gameObject);
		}
	}
}
