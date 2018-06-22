using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class tipodecontrol : MonoBehaviour {

	//DETECTAR MANDO CONECTADO
	public string mando;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		//DETECTAR MANDO CONECTADO

		if(Input.GetJoystickNames().Length == 0)
		{
			mando = "TECLADO";
		}
		if(Input.GetJoystickNames().Length == 33)
		{
			mando = "XBOX";
		}
		if(Input.GetJoystickNames().Length == 19)
		{
			mando = "PS4";
		}
	}
}
