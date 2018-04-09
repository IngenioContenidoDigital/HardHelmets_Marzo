using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creditos : MonoBehaviour {

	public GameObject salir;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetButtonDown("Cancel"))
		{
			Regresar();
		}
	}

	public void Regresar()
	{
		salir.SetActive(true);
	}

	public void end()
	{
		Application.LoadLevel("Load");
		loading.nombre = "menu";
	}
}
