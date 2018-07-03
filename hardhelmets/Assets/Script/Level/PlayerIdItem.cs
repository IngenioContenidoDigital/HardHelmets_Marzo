using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIdItem : MonoBehaviour {

	public string tipo;

	public string nombre;

	public static string activo;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("ItemEntra"))
		{
			GetComponent<Animator>().SetBool("entra", false);
		}
		if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("ItemSale"))
		{
			GetComponent<Animator>().SetBool("sale", false);
		}
	}

	public void seleccionado()
	{
		activo = gameObject.name;

		GetComponent<AudioSource>().Play();

		GetComponent<Animator>().SetBool("entra", true);

		panel.GetComponent<PlayerIDPanel>().nombreItem = gameObject.name;

		panel.GetComponent<PlayerIDPanel>().moverArriba = false;
		panel.GetComponent<PlayerIDPanel>().moverAbajo = false;
	}
	public void desseleccionado()
	{
		GetComponent<Animator>().SetBool("sale", true);
	}

	public GameObject panel;

	public void select ()
	{
		if(panel.GetComponent<PlayerIDPanel>().zona)
		{
			if(tipo == "avatar")
			{
				panel.GetComponent<PlayerIDPanel>().avatar = nombre;
			}
			if(tipo == "borde")
			{
				panel.GetComponent<PlayerIDPanel>().borde = nombre;
			}
			if(tipo == "fondo")
			{
				panel.GetComponent<PlayerIDPanel>().fondo = nombre;
			}
		}
	}
}
