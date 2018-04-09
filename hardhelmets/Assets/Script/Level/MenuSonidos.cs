using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSonidos : MonoBehaviour {

	public AudioClip cartas;
	public AudioClip cartassalen;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void carta()
	{
		GetComponent<AudioSource>().volume = 1;
		GetComponent<AudioSource>().clip = cartas;
		GetComponent<AudioSource>().Play();
	}

	public void cartasale()
	{
		GetComponent<AudioSource>().volume = 1;
		GetComponent<AudioSource>().clip = cartassalen;
		GetComponent<AudioSource>().Play();
	}


	public AudioClip Supplies1;
	public AudioClip Supplies2;

	public void SuppliesEntra()
	{
		GetComponent<AudioSource>().volume = 1;
		GetComponent<AudioSource>().clip = Supplies1;
		GetComponent<AudioSource>().Play();
	}

	public void SuppliesSale()
	{
		GetComponent<AudioSource>().volume = 1;
		GetComponent<AudioSource>().clip = Supplies2;
		GetComponent<AudioSource>().Play();
	}
}
