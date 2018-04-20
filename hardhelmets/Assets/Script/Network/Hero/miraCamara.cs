using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miraCamara : MonoBehaviour {

	public GameObject Player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Player.GetComponent<HeroNetwork>()._currentDirection == "right")
		{
			transform.localScale = new Vector3(0.1f,0.1f,0.1f);
		}else
		{
			transform.localScale = new Vector3(-0.1f,0.1f,0.1f);
		}
	}
}
