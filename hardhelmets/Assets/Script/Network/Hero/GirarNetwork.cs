using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirarNetwork : MonoBehaviour {

	public GameObject Player;
	public bool voltear;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Player.GetComponent<HeroNetwork>()._currentDirection == "right" && voltear)
		{
			gameObject.transform.Rotate (0, 180, 0);
			voltear = false;
		}else if(voltear)
		{
			gameObject.transform.Rotate (0, -180, 0);
			voltear = false;
		}
	}
}
