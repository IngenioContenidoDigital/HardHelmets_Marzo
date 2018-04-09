using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balaSniperNetwork : MonoBehaviour {

	public GameObject Hero;
	public GameObject Player;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Hero == null)
		{
			Hero = GameObject.Find("Hero");
		}

		if(Player == null)
		{
			Player = GameObject.Find("SniperSpawn");
		}
	}
}
