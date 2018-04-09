using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunitySelector : MonoBehaviour {

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
			content.GetComponent<CommunityList>().top = true;
		}
		if(col.gameObject.name == "Down")
		{
			content.GetComponent<CommunityList>().down = true;
		}
	}

	void OnTriggerExit2D (Collider2D col)
	{
		if(col.gameObject.name == "Top")
		{
			content.GetComponent<CommunityList>().top = false;
		}
		if(col.gameObject.name == "Down")
		{
			content.GetComponent<CommunityList>().down = false;
		}
	}
}
