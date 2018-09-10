using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creaTank : MonoBehaviour {

	public GameObject tankPesado;
	public GameObject tankPesadoB;
	public GameObject tankPesadoC;

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
		grounded = Physics.CheckSphere(groundCheck.position, groundRadius, whatIsGround);

		if(grounded && !crear)
		{
			TanquePesadoBueno();
			crear = true;
		}
	}

	public void TanquePesadoBueno()
	{
		if(PlayerPrefs.GetString("factionBuena") == "")
		{
			var objeto = (GameObject)Instantiate(tankPesado, nace.transform.position, Quaternion.Euler(0,90,0));
		}
		if(PlayerPrefs.GetString("factionBuena") == "b")
		{
			var objeto = (GameObject)Instantiate(tankPesadoB, nace.transform.position, Quaternion.Euler(0,90,0));
		}
		if(PlayerPrefs.GetString("factionBuena") == "c")
		{
			var objeto = (GameObject)Instantiate(tankPesadoC, nace.transform.position, Quaternion.Euler(0,90,0));
		}

		Destroy(gameObject);
	}
}
