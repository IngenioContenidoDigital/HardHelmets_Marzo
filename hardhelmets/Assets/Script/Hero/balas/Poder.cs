using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poder : MonoBehaviour {

	public float poder;

	public GameObject bomba1;
	public GameObject bomba2;
	public GameObject bomba3;
	public GameObject bomba4;
	public GameObject bomba5;
	public GameObject bomba6;
	public GameObject bomba7;
	public GameObject bomba8;
	public GameObject bomba9;
	public GameObject bomba10;
	public GameObject bomba11;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(bomba1 == null && bomba2 == null && bomba3 == null && bomba4 == null && bomba5 == null && bomba6 == null && 
			bomba7 == null && bomba8 == null && bomba9 == null && bomba10 == null && bomba11 == null)
		{
			Destroy(gameObject);
		}
	}

	public void destruirUno()
	{
		if(bomba1 != null)
		{
			bomba1.GetComponent<bombaOffline>().Explo();
		}
	}
	public void destruirDos()
	{
		if(bomba2 != null)
		{
			bomba2.GetComponent<bombaOffline>().Explo();
		}
	}
	public void destruirTres()
	{
		if(bomba3 != null)
		{
			bomba3.GetComponent<bombaOffline>().Explo();
		}
	}
	public void destruirCuatro()
	{
		if(bomba4 != null)
		{
			bomba4.GetComponent<bombaOffline>().Explo();
		}
	}
	public void destruirCinco()
	{
		if(bomba5 != null)
		{
			bomba5.GetComponent<bombaOffline>().Explo();
		}
	}
	public void destruirSeis()
	{
		if(bomba6 != null)
		{
			bomba6.GetComponent<bombaOffline>().Explo();
		}
	}
	public void destruirSiete()
	{
		if(bomba7 != null)
		{
			bomba7.GetComponent<bombaOffline>().Explo();
		}
	}
	public void destruirOcho()
	{
		if(bomba8 != null)
		{
			bomba8.GetComponent<bombaOffline>().Explo();
		}
	}
	public void destruirNueve()
	{
		if(bomba9 != null)
		{
			bomba9.GetComponent<bombaOffline>().Explo();
		}
	}
	public void destruirDiez()
	{
		if(bomba10 != null)
		{
			bomba10.GetComponent<bombaOffline>().Explo();
		}
	}
	public void destruirOnce()
	{
		if(bomba11 != null)
		{
			bomba11.GetComponent<bombaOffline>().Explo();
		}
	}
}
