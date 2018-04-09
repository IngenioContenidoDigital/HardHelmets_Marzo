using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cartasRobadas : MonoBehaviour {

	public int carta;
	bool coli;

	//PANEL PARTIDA
	public GameObject Panel;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update ()
	{
		if(Panel == null)
		{
			Panel = GameObject.Find("GAME");
		}

		transform.Rotate(Vector3.up * 100 * Time.deltaTime);
	}

	void OnCollisionEnter (Collision col)
	{
		///---------------CARTAS---------------
		if(col.gameObject.tag == "Player")
		{
			if(!coli)
			{
				PlayerPrefs.SetInt("card"+carta, PlayerPrefs.GetInt("card"+carta)+1);
				coli = true;
			}

			Panel.GetComponent<GameOffline>().StolenCardsB += 1;

			Destroy(gameObject);
		}
	}
}
