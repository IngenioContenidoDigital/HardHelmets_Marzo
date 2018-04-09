using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class navegacionCartas : MonoBehaviour {

	public Button este;

	public Navigation navegar;

	public GameObject amiga1I;
	public GameObject amiga2I;
	public GameObject amiga3I;
	public GameObject amiga4I;
	public GameObject amiga5I;
	public GameObject amiga6I;
	public GameObject amiga7I;
	public GameObject amiga8I;
	public GameObject amiga9I;

	public GameObject amiga1D;
	public GameObject amiga2D;
	public GameObject amiga3D;
	public GameObject amiga4D;
	public GameObject amiga5D;
	public GameObject amiga6D;
	public GameObject amiga7D;
	public GameObject amiga8D;
	public GameObject amiga9D;

	// Use this for initialization
	void Start ()
	{
		este = gameObject.GetComponent<Button>();
		navegar = este.navigation;
	}

	// Update is called once per frame
	void Update ()
	{
		navegar.mode = Navigation.Mode.Explicit;

		if(amiga1I.GetComponent<manoJuego>().completa)
		{
			navegar.selectOnLeft = amiga1I.GetComponent<Button>();
		}else if(amiga2I.GetComponent<manoJuego>().completa)
		{
			navegar.selectOnLeft = amiga2I.GetComponent<Button>();
		}else if(amiga3I.GetComponent<manoJuego>().completa)
		{
			navegar.selectOnLeft = amiga3I.GetComponent<Button>();
		}else if(amiga4I.GetComponent<manoJuego>().completa)
		{
			navegar.selectOnLeft = amiga4I.GetComponent<Button>();
		}else if(amiga5I.GetComponent<manoJuego>().completa)
		{
			navegar.selectOnLeft = amiga5I.GetComponent<Button>();
		}else if(amiga6I.GetComponent<manoJuego>().completa)
		{
			navegar.selectOnLeft = amiga6I.GetComponent<Button>();
		}else if(amiga7I.GetComponent<manoJuego>().completa)
		{
			navegar.selectOnLeft = amiga7I.GetComponent<Button>();
		}else if(amiga8I.GetComponent<manoJuego>().completa)
		{
			navegar.selectOnLeft = amiga8I.GetComponent<Button>();
		}else if(amiga9I.GetComponent<manoJuego>().completa)
		{
			navegar.selectOnLeft = amiga9I.GetComponent<Button>();
		}

		if(amiga1D.GetComponent<manoJuego>().completa)
		{ 
			navegar.selectOnRight = amiga1D.GetComponent<Button>();
		}else if(amiga2D.GetComponent<manoJuego>().completa)
		{
			navegar.selectOnRight = amiga2D.GetComponent<Button>();
		}else if(amiga3D.GetComponent<manoJuego>().completa)
		{
			navegar.selectOnRight = amiga3D.GetComponent<Button>();
		}else if(amiga4D.GetComponent<manoJuego>().completa)
		{
			navegar.selectOnRight = amiga4D.GetComponent<Button>();
		}else if(amiga5D.GetComponent<manoJuego>().completa)
		{
			navegar.selectOnRight = amiga5D.GetComponent<Button>();
		}else if(amiga6D.GetComponent<manoJuego>().enabled)
		{
			navegar.selectOnRight = amiga6D.GetComponent<Button>();
		}else if(amiga7D.GetComponent<manoJuego>().completa)
		{
			navegar.selectOnRight = amiga7D.GetComponent<Button>();
		}else if(amiga8D.GetComponent<manoJuego>().completa)
		{
			navegar.selectOnRight = amiga8D.GetComponent<Button>();
		}else if(amiga9D.GetComponent<manoJuego>().completa)
		{
			navegar.selectOnRight = amiga9D.GetComponent<Button>();
		}

		este.navigation = navegar;
	}
}
