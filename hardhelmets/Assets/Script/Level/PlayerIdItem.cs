using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIdItem : MonoBehaviour {

	public string tipo;

	public string nombre;

	public static string activo;
	public Button joinButton;

	public bool selected;

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
		if(panel.GetComponent<PlayerIDPanel>().zona && selected && Input.GetButtonDown("Jump") || Input.GetButtonDown("Submit"))
		{
			joinButton.onClick.Invoke();
		}
	}

	public GameObject panel;

	void OnTriggerEnter2D (Collider2D col)
	{
		if(col.gameObject.tag == "Player")
		{
			activo = gameObject.name;
			selected = true;

			GetComponent<AudioSource>().Play();

			GetComponent<Animator>().SetBool("entra", true);

			panel.GetComponent<PlayerIDPanel>().nombreItem = gameObject.name;

			panel.GetComponent<PlayerIDPanel>().moverArriba = false;
			panel.GetComponent<PlayerIDPanel>().moverAbajo = false;
		}
	}

	void OnTriggerExit2D (Collider2D col)
	{
		if(col.gameObject.tag == "Player")
		{
			selected = false;
		
			GetComponent<Animator>().SetBool("sale", true);
		}
	}

	public void select ()
	{
		if(panel.GetComponent<PlayerIDPanel>().zona && selected)
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
