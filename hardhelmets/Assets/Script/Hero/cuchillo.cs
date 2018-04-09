using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cuchillo : MonoBehaviour {

	public GameObject Player;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "enemy")
		{
			Player.GetComponent<Hero>().cuchillo = true;
		}
	}

	void OnTriggerExit (Collider col)
	{
		Player.GetComponent<Hero>().cuchillo = false;
	}
}
