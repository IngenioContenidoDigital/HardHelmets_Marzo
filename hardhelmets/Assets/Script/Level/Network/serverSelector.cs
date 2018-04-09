using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.NetworkLobby;

namespace Prototype.NetworkLobby
{

public class serverSelector : MonoBehaviour {

	public GameObject content;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if(col.gameObject.name == "Top")
		{
			content.GetComponent<serverList>().top = true;
		}
		if(col.gameObject.name == "Down")
		{
			content.GetComponent<serverList>().down = true;
		}
	}

	void OnTriggerExit2D (Collider2D col)
	{
		if(col.gameObject.name == "Top")
		{
			content.GetComponent<serverList>().top = false;
		}
		if(col.gameObject.name == "Down")
		{
			content.GetComponent<serverList>().down = false;
		}
	}
}
}
