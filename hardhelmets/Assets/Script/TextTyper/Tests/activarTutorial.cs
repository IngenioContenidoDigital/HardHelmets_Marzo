using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class activarTutorial : MonoBehaviour {

	public string valor;

	public int rango;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(PlayerPrefs.GetInt(valor) < rango)
		{
			GetComponent<Button>().interactable = false;
		}else if(!GetComponent<Button>().interactable)
		{
			GetComponent<Button>().interactable = true;
		}
	}
}
