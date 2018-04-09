using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectButtonLobby : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	public Button joinButton;
	public bool selected;
	public void Update()
	{
		if(selected && Input.GetButtonDown("Jump") || selected && Input.GetButtonDown("Submit"))
		{
			joinButton.onClick.Invoke();
		}
	}
	public void entry()
	{
		selected = true;
	}
	public void exit()
	{
		selected = false;
	}
}
