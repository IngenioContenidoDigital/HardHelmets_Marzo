using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IP : MonoBehaviour {

	string ip;
	public UnityEngine.UI.Text tecto;
	// Use this for initialization
	void Start ()
	{
		ip = Network.player.ipAddress;
		tecto.text = "IP: "+ip;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
