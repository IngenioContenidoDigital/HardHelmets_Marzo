using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cuchilloNetwork : MonoBehaviour {

	public GameObject Player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "Player" || col.gameObject.tag == "enemy")
		{
			Player.GetComponent<HeroNetwork>().cuchillo = true;
		}
	}

	void OnTriggerExit (Collider col)
	{
		Player.GetComponent<HeroNetwork>().cuchillo = false;
	}
}
