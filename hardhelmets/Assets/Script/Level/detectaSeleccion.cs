using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectaSeleccion : MonoBehaviour {

	public bool entrer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator momento()
	{
		yield return new WaitForSeconds(0.2f);
		entrer = false;
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		entrer = true;
		StartCoroutine(momento());
	}

	void OnTriggerExit2D (Collider2D col)
	{
		entrer = false;
	}
}
