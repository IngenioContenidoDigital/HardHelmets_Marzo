using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class crearTankNetwork : NetworkBehaviour {

	public GameObject tankPesado;

	public GameObject nace;

	//GROUND CHECHER
	public Transform groundCheck;
	float groundRadius = 5f;
	public LayerMask whatIsGround;
	public bool grounded = false;

	public bool crear;

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		if(!isServer)
		{
			return;
		}
		grounded = Physics.CheckSphere(groundCheck.position, groundRadius, whatIsGround);

		if(grounded && !crear)
		{
			CmdTanquePesadoBueno();
			crear = true;
		}
	}

	public void CmdTanquePesadoBueno()
	{
		var objeto = (GameObject)Instantiate(tankPesado, nace.transform.position, Quaternion.Euler(0,90,0));
		NetworkServer.Spawn(objeto);
		Destroy(gameObject);
	}
}
