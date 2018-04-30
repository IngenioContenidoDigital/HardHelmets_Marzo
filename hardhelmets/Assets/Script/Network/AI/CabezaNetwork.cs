using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CabezaNetwork : NetworkBehaviour {

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
		if(!Player.GetComponent<NetworkIdentity>().isLocalPlayer)
		{
			return;
		}

		if(disparoCabeza)
		{
			Player.GetComponent<HeroNetwork>().saludSumar = false;
			if(tirosCabeza >= 1)
			{
				Player.GetComponent<HeroNetwork>().tirocabeza = true;

				Player.GetComponent<HeroNetwork>().salud -= poder*3;

				var letras = (GameObject)Instantiate(textos, Player.transform.position, Quaternion.Euler(0,0,0));
				letras.GetComponent<TextMesh>().text = "HEAD SHOT";
				if(sniper)
				{
					if(numSombrero == 1)
					{
						if(Player.tag == "Player")
						{
							Player.GetComponent<HeroNetwork>().CmdCasco1Bueno();
						}else
						{
							Player.GetComponent<HeroNetwork>().CmdCasco1Malo();
						}
					}
					if(numSombrero == 2)
					{
						if(Player.tag == "Player")
						{
							Player.GetComponent<HeroNetwork>().CmdCasco2Bueno();
						}else
						{
							Player.GetComponent<HeroNetwork>().CmdCasco2Malo();
						}
					}
					if(numSombrero == 3)
					{
						if(Player.tag == "Player")
						{
							Player.GetComponent<HeroNetwork>().CmdCasco3Bueno();
						}else
						{
							Player.GetComponent<HeroNetwork>().CmdCasco3Malo();
						}
					}
					if(numSombrero == 4)
					{
						if(Player.tag == "Player")
						{
							Player.GetComponent<HeroNetwork>().CmdCasco4Bueno();
						}else
						{
							Player.GetComponent<HeroNetwork>().CmdCasco4Malo();
						}
					}
					if(numSombrero == 5)
					{
						if(Player.tag == "Player")
						{
							Player.GetComponent<HeroNetwork>().CmdCasco5Bueno();
						}else
						{
							Player.GetComponent<HeroNetwork>().CmdCasco5Malo();
						}
					}
					if(numSombrero == 6)
					{
						if(Player.tag == "Player")
						{
							Player.GetComponent<HeroNetwork>().CmdCasco6Bueno();
						}else
						{
							Player.GetComponent<HeroNetwork>().CmdCasco6Malo();
						}
					}
					sniper = false;
				}
			}else
			{
				Player.GetComponent<HeroNetwork>().tirocabeza = true;

				Player.GetComponent<HeroNetwork>().salud -= poder;

				Player.GetComponent<CustomFinalNetwork>().CmdSetCasco("");

				if(numSombrero == 1)
				{
					if(Player.tag == "Player")
					{
						Player.GetComponent<HeroNetwork>().CmdCasco1Bueno();
					}else
					{
						Player.GetComponent<HeroNetwork>().CmdCasco1Malo();
					}
				}
				if(numSombrero == 2)
				{
					if(Player.tag == "Player")
					{
						Player.GetComponent<HeroNetwork>().CmdCasco2Bueno();
					}else
					{
						Player.GetComponent<HeroNetwork>().CmdCasco2Malo();
					}
				}
				if(numSombrero == 3)
				{
					if(Player.tag == "Player")
					{
						Player.GetComponent<HeroNetwork>().CmdCasco3Bueno();
					}else
					{
						Player.GetComponent<HeroNetwork>().CmdCasco3Malo();
					}
				}
				if(numSombrero == 4)
				{
					if(Player.tag == "Player")
					{
						Player.GetComponent<HeroNetwork>().CmdCasco4Bueno();
					}else
					{
						Player.GetComponent<HeroNetwork>().CmdCasco4Malo();
					}
				}
				if(numSombrero == 5)
				{
					if(Player.tag == "Player")
					{
						Player.GetComponent<HeroNetwork>().CmdCasco5Bueno();
					}else
					{
						Player.GetComponent<HeroNetwork>().CmdCasco5Malo();
					}
				}
				if(numSombrero == 6)
				{
					if(Player.tag == "Player")
					{
						Player.GetComponent<HeroNetwork>().CmdCasco6Bueno();
					}else
					{
						Player.GetComponent<HeroNetwork>().CmdCasco6Malo();
					}
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
		if(!Player.GetComponent<NetworkIdentity>().isLocalPlayer)
		{
			return;
		}
		if(col.gameObject.tag == "bala")
		{
			if(col.gameObject.GetComponent<balaSniper>())
			{
				poder = col.gameObject.GetComponent<balaSniper>().poder;
			}else
			{
				poder = col.gameObject.GetComponent<bala>().poder;
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
