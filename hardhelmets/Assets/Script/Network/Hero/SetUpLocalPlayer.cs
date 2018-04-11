using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetUpLocalPlayer : NetworkBehaviour {

	//[SyncVar]
	//public string pname = "player";
	public GameObject name;
	public GameObject Cabeza;
	public GameObject Colisioncuchillo;
	public GameObject cuchillo1;
	public GameObject cuchillo2;
	public GameObject Sniper;
	public GameObject Colision;
	public GameObject Mira;
	public GameObject Apuntar;
	public GameObject Canvas;
	public GameObject Base;
	public GameObject eventos;

	public GameObject CAMARA;

	void Start ()
	{
		if(isLocalPlayer)
		{
			gameObject.name = "Hero";
			GetComponent<AudioListener>().enabled = true;
			Canvas.SetActive(true);
			//Sniper.SetActive(true);
			Mira.SetActive(true);
			CAMARA.transform.parent = null;
			//Apuntar.SetActive(true);
			eventos.SetActive(true);
		}else
		{
			CAMARA.transform.parent = null;
			CAMARA.SetActive(false);
		}
	}
	IEnumerator espera ()
	{
		yield return new WaitForSeconds(5);
		Destroy(this);
	}
	void Update ()
	{
		if(isServer)
		{
			if(isLocalPlayer)
			{
				name.GetComponent<TextMesh>().text = "Player 1";
				GetComponent<HeroNetwork>().mascara = "Player";
				GetComponent<HeroNetwork>().medic = "medicoBueno";
				Destroy(this);
			}else
			{
				gameObject.name = "Hero2";

				name.GetComponent<TextMesh>().text = "Player 2";

				GetComponent<NetworkView>().tag = "enemy";
				GetComponent<HeroNetwork>().mascara = "Enemy";
				gameObject.layer = LayerMask.NameToLayer("Enemy");
				Cabeza.gameObject.layer = LayerMask.NameToLayer("Enemy");
				Colisioncuchillo.gameObject.layer = LayerMask.NameToLayer("cuchilloMalo");
				cuchillo1.gameObject.layer = LayerMask.NameToLayer("objetoEnemy");
				cuchillo2.gameObject.layer = LayerMask.NameToLayer("objetoEnemy");
				Base.tag = "dos";
				//Colision.gameObject.tag = "enemy";//.layer  = LayerMask.NameToLayer("Enemy");
				Mira.gameObject.layer = LayerMask.NameToLayer("miraMalo");
				GetComponent<HeroNetwork>().medic = "medicoMalo";

				Destroy(this);
			}
		}else
		{
			if(isLocalPlayer)
			{
				name.GetComponent<TextMesh>().text = "Player 2";

				GetComponent<NetworkView>().tag = "enemy";
				GetComponent<HeroNetwork>().mascara = "Enemy";
				gameObject.layer = LayerMask.NameToLayer("Enemy");
				Cabeza.gameObject.layer = LayerMask.NameToLayer("Enemy");
				Colisioncuchillo.gameObject.layer = LayerMask.NameToLayer("cuchilloMalo");
				cuchillo1.gameObject.layer = LayerMask.NameToLayer("objetoEnemy");
				cuchillo2.gameObject.layer = LayerMask.NameToLayer("objetoEnemy");
				Base.tag = "dos";
				//Colision.gameObject.tag = "enemy";//.layer = LayerMask.NameToLayer("Enemy");
				Mira.gameObject.layer = LayerMask.NameToLayer("miraMalo");

				GetComponent<HeroNetwork>().CmdChangeDirection ("left");

				Destroy(this);
			}else
			{
				gameObject.name = "Hero2";

				name.GetComponent<TextMesh>().text = "Player 1";
				GetComponent<HeroNetwork>().mascara = "Player";
				Destroy(this);
			}
		}
	}

	/*public override void OnStartLocalPlayer ()
	{
		GetComponent<Material>().color = Color.green;
	}*/
}
