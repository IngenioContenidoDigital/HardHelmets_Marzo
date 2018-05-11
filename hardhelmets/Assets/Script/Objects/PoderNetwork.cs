using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PoderNetwork : NetworkBehaviour {

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

	public GameObject explocion;

	public GameObject destruir;
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

	[Command]
	public void Cmd_Explo()
	{
		var explo = (GameObject)Instantiate(explocion, destruir.transform.position, Quaternion.identity);
		NetworkServer.Spawn(explo);
		explo.GetComponent<Explo>().poder = poder;
		RpcDestruir();

		Destroy(destruir);
	}
	[ClientRpc]
	public void RpcDestruir ()
	{
		Destroy(destruir);
	}

	public void destruirUno()
	{
		if(bomba1 != null)
		{
			destruir = bomba1;
			Cmd_Explo();
		}
	}
	public void destruirDos()
	{
		if(bomba2 != null)
		{
			destruir = bomba2;
			Cmd_Explo();
		}
	}
	public void destruirTres()
	{
		if(bomba3 != null)
		{
			destruir = bomba3;
			Cmd_Explo();
		}
	}
	public void destruirCuatro()
	{
		if(bomba4 != null)
		{
			destruir = bomba4;
			Cmd_Explo();
		}
	}
	public void destruirCinco()
	{
		if(bomba5 != null)
		{
			destruir = bomba5;
			Cmd_Explo();
		}
	}
	public void destruirSeis()
	{
		if(bomba6 != null)
		{
			destruir = bomba6;
			Cmd_Explo();
		}
	}
	public void destruirSiete()
	{
		if(bomba7 != null)
		{
			destruir = bomba7;
			Cmd_Explo();
		}
	}
	public void destruirOcho()
	{
		if(bomba8 != null)
		{
			destruir = bomba8;
			Cmd_Explo();
		}
	}
	public void destruirNueve()
	{
		if(bomba9 != null)
		{
			destruir = bomba9;
			Cmd_Explo();
		}
	}
	public void destruirDiez()
	{
		if(bomba10 != null)
		{
			destruir = bomba10;
			Cmd_Explo();
		}
	}
	public void destruirOnce()
	{
		if(bomba11 != null)
		{
			destruir = bomba11;
			Cmd_Explo();
		}
	}
}
