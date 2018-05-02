using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girar : MonoBehaviour {

	public GameObject Player;
	public bool voltear;

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		if(Player.GetComponent<Hero>()._currentDirection == "right" && voltear)
		{
			gameObject.transform.Rotate (0, 180, gameObject.transform.eulerAngles.z);
			voltear = false;
		}else if(voltear)
		{
			gameObject.transform.Rotate (0, -180, gameObject.transform.eulerAngles.z);
			voltear = false;
		}
	}
}
