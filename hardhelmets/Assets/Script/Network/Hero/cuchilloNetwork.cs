using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cuchilloNetwork : MonoBehaviour {

	public GameObject Player;

	public bool cuchillado;
	public GameObject enemigo;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(cuchillado && enemigo == null)
		{
			Player.GetComponent<Hero>().cuchillo = false;

			cuchillado = false;
		}
	}

	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "Player" || col.gameObject.tag == "enemy")
		{
			cuchillado = true;
			enemigo = col.gameObject;

			Player.GetComponent<HeroNetwork>().cuchillo = true;
			Player.GetComponent<HeroNetwork>().rafaga = true;
		}
	}

	void OnTriggerExit (Collider col)
	{
		cuchillado = false;
		enemigo = null;

		Player.GetComponent<HeroNetwork>().cuchillo = false;
	}
}
