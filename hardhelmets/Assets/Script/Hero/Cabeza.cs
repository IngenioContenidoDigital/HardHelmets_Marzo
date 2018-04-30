using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabeza : MonoBehaviour {

	public GameObject Player;
	public Animator animator;

	public GameObject textos;

	public bool disparoCabeza;
	public int tirosCabeza;
	bool sniper;
	public int numSombrero;

	int tiros;

	//poder de cascada
	float poder;

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		if(disparoCabeza)
		{
			Player.GetComponent<Hero>().saludSumar = false;
			if(tirosCabeza >= 1)
			{
				Player.GetComponent<Hero>().tirocabeza = true;

				Player.GetComponent<Hero>().salud -= poder*3;

				var letras = (GameObject)Instantiate(textos, Player.transform.position, Quaternion.Euler(0,0,0));
				letras.GetComponent<TextMesh>().text = "HEAD SHOT";
				if(sniper)
				{
					if(numSombrero == 1)
					{
						Player.GetComponent<Hero>().Casco1Bueno();
					}
					if(numSombrero == 2)
					{
						Player.GetComponent<Hero>().Casco2Bueno();
					}
					if(numSombrero == 3)
					{
						Player.GetComponent<Hero>().Casco3Bueno();
					}
					if(numSombrero == 4)
					{
						Player.GetComponent<Hero>().Casco4Bueno();
					}
					if(numSombrero == 5)
					{
						Player.GetComponent<Hero>().Casco5Bueno();
					}
					if(numSombrero == 6)
					{
						Player.GetComponent<Hero>().Casco6Bueno();
					}
					sniper = false;
				}
			}else
			{
				Player.GetComponent<Hero>().tirocabeza = true;

				Player.GetComponent<Hero>().salud -= poder;

				Player.GetComponent<CustomFinal>().casco = "";

				if(numSombrero == 1)
				{
					Player.GetComponent<Hero>().Casco1Bueno();
				}
				if(numSombrero == 2)
				{
					Player.GetComponent<Hero>().Casco2Bueno();
				}
				if(numSombrero == 3)
				{
					Player.GetComponent<Hero>().Casco3Bueno();
				}
				if(numSombrero == 4)
				{
					Player.GetComponent<Hero>().Casco4Bueno();
				}
				if(numSombrero == 5)
				{
					Player.GetComponent<Hero>().Casco5Bueno();
				}
				if(numSombrero == 6)
				{
					Player.GetComponent<Hero>().Casco6Bueno();
				}

				var letras = (GameObject)Instantiate(textos, Player.transform.position, Quaternion.Euler(0,0,0));
				letras.GetComponent<TextMesh>().text = "HELMET";

				tirosCabeza += 1;
			}
			disparoCabeza = false;
		}
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "bala")
		{
			if(col.gameObject.GetComponent<balaSniper>())
			{
				poder = col.gameObject.GetComponent<balaSniperOffline>().poder;
			}else
			{
				poder = col.gameObject.GetComponent<balaOffline>().poder;
			}

			disparoCabeza = true;
			Destroy(col.gameObject);
		}
		/*if(col.gameObject.tag == "balaFusil")
		{
			disparoCabeza = true;
			Destroy(col.gameObject);
		}
		if(col.gameObject.tag == "balaEscopeta")
		{
			disparoCabeza = true;
			Destroy(col.gameObject);
		}
		if(col.gameObject.tag == "balaSubmetra")
		{
			disparoCabeza = true;
			Destroy(col.gameObject);
		}
		if(col.gameObject.tag == "balaMetra")
		{
			disparoCabeza = true;
			Destroy(col.gameObject);
		}
		if(col.gameObject.tag == "balaMG")
		{
			disparoCabeza = true;
			Destroy(col.gameObject);
		}
		if(col.gameObject.tag == "balaSniper")
		{
			tirosCabeza += 1;
			disparoCabeza = true;
			sniper = true;
			Destroy(col.gameObject);
		}*/
	}
}
