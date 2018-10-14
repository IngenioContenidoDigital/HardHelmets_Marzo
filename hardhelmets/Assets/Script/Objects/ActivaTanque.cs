using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivaTanque : MonoBehaviour {

	public GameObject tank;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void completanimation ()
	{
		tank.transform.parent = null;
		tank.SetActive(true);
		Destroy(gameObject);
	}
}
