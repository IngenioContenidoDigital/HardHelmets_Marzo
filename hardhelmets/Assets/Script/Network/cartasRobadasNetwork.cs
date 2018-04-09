using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class cartasRobadasNetwork : NetworkBehaviour {

	public int carta;
	bool coli;

	//PANEL PARTIDA
	GameObject Panel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!isServer)
		{
			return;
		}
		if(Panel == null)
		{
			Panel = GameObject.Find("GAME");
		}

		transform.Rotate(Vector3.up * 100 * Time.deltaTime);
	}

	void OnCollisionEnter (Collision col)
	{
		if(!isServer)
		{
			return;
		}
		///---------------CARTAS---------------
		if(col.gameObject.tag == "Player")
		{
			if(!coli)
			{
				PlayerPrefs.SetInt("card"+carta, 1);
				PlayerPrefs.SetInt("card"+carta+"cantidad", PlayerPrefs.GetInt("card"+carta+"cantidad")+1);
				coli = true;
			}

			Panel.GetComponent<Game>().StolenCardsB += 1;

			Destroy(gameObject);
		}

		if(col.gameObject.tag == "enemy")
		{
			if(!coli)
			{
				PlayerPrefs.SetInt("card"+carta, 1);
				PlayerPrefs.SetInt("card"+carta+"cantidad", PlayerPrefs.GetInt("card"+carta+"cantidad")+1);
				coli = true;
			}

			Panel.GetComponent<Game>().StolenCardsM += 1;

			Destroy(gameObject);
		}
		////SUMA EN EL PANEL
		if(col.gameObject.tag == "Hero")
		{
			Panel.GetComponent<Game>().StolenCardsB += 1;
		}

		if(col.gameObject.tag == "Hero2")
		{
			Panel.GetComponent<Game>().StolenCardsM += 1;
		}
	}
}
