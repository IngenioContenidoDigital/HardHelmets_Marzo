using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerIDPanel : MonoBehaviour {

	public Animator animator;

	public GameObject PlayerId;

	public string tipo;

	public string avatar;
	public string borde;
	public string fondo;

	public bool fondoU;
	public bool fondoD;

	public bool bordeU;
	public bool bordeD;

	public GameObject menu;

	public bool zona;

	// Use this for initialization
	void Start ()
	{
		
	}
	public GameObject cuadro;
	// Update is called once per frame
	void Update ()
	{
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("PanelSale"))
		{
			animator.SetBool("entra", false);
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("PanelEsconde"))
		{
			animator.SetBool("entra", false);
			animator.SetBool("sale", false);
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("PanelIdle"))
		{
			animator.SetBool("sale", false);
		}

		if(tipo == "avatar" && avatar != "")
		{
			PlayerId.GetComponent<PlayerIdSkin>().skinsToCombine[0] = "avatar"+avatar;
			PlayerPrefs.SetString("avatar", "avatar"+avatar);
		}
		if(tipo == "borde" && borde != "")
		{
			PlayerId.GetComponent<PlayerIdSkin>().skinsToCombine[1] = "borde"+borde;
			PlayerPrefs.SetString("borde", "borde"+borde);
		}
		if(tipo == "fondo" && fondo != "")
		{
			PlayerId.GetComponent<PlayerIdSkin>().skinsToCombine[2] = "fondo"+fondo;
			PlayerPrefs.SetString("fondo", "fondo"+fondo);
		}
		//RANGO  PlayerId.GetComponent<PlayerIdSkin>().skinsToCombine[3] = "avatar"+avatar;
		if(nombreItem == first)
		{
			moverArriba = false;
		}
		if(moverArriba)
		{
			moverAbajo = false;
			GetComponent<ScrollRect>().verticalNormalizedPosition += 0.2f;
			if(cuadro.GetComponent<detectaSeleccion>().entrer)
			{
				moverArriba = false;
			}
		}
		if(moverAbajo)
		{
			moverArriba = false;
			GetComponent<ScrollRect>().verticalNormalizedPosition -= 0.2f;
			if(cuadro.GetComponent<detectaSeleccion>().entrer)
			{
				moverAbajo = false;
			}
		}
		if(nombreItem == last)
		{
			moverAbajo = false;
		}

		if(fondoU)
		{
			if(Input.GetButtonDown("left") || Input.GetAxis("Horizontal") < 0 || Input.GetAxis("HorizontalUI") < 0)
			{
				menu.GetComponent<Menu>().primero.GetComponent<Animator>().SetBool("entra", false);
				menu.GetComponent<Menu>().primero.GetComponent<Animator>().SetBool("sale", true);
				zona = false;
				fondoU = false;
			}
			if(Input.GetButtonDown("up") && !cuadro.GetComponent<detectaSeleccion>().entrer || Input.GetAxis("Vertical") > 0 && !cuadro.GetComponent<detectaSeleccion>().entrer || Input.GetAxis("VerticalUI") > 0 && !cuadro.GetComponent<detectaSeleccion>().entrer)
			{
				arriba();
			}
		}
		if(fondoD)
		{
			if(Input.GetButtonDown("left") || Input.GetAxis("Horizontal") < 0 || Input.GetAxis("HorizontalUI") < 0)
			{
				menu.GetComponent<Menu>().primero.GetComponent<Animator>().SetBool("entra", false);
				menu.GetComponent<Menu>().primero.GetComponent<Animator>().SetBool("sale", true);
				zona = false;
				fondoD = false;
			}
			if(Input.GetButtonDown("down") && !cuadro.GetComponent<detectaSeleccion>().entrer || Input.GetAxis("Vertical") < 0 && !cuadro.GetComponent<detectaSeleccion>().entrer || Input.GetAxis("VerticalUI") < 0 && !cuadro.GetComponent<detectaSeleccion>().entrer)
			{
				abajo();
			}
		}
		if(bordeU)
		{
			if(Input.GetButtonDown("left") || Input.GetAxis("Horizontal") < 0 || Input.GetAxis("HorizontalUI") < 0)
			{
				menu.GetComponent<Menu>().segundo.GetComponent<Animator>().SetBool("entra", false);
				menu.GetComponent<Menu>().segundo.GetComponent<Animator>().SetBool("sale", true);
				zona = false;
				fondoD = false;
			}
			if(Input.GetButtonDown("up") || Input.GetAxis("Vertical") > 0 || Input.GetAxis("VerticalUI") > 0)
			{
				arriba();
			}
		}
		if(bordeD)
		{
			if(Input.GetButtonDown("left") || Input.GetAxis("Horizontal") < 0 || Input.GetAxis("HorizontalUI") < 0)
			{
				menu.GetComponent<Menu>().segundo.GetComponent<Animator>().SetBool("entra", false);
				menu.GetComponent<Menu>().segundo.GetComponent<Animator>().SetBool("sale", true);
				zona = false;
				fondoD = false;
			}
			if(Input.GetButtonDown("down") || Input.GetAxis("Vertical") < 0 || Input.GetAxis("VerticalUI") < 0)
			{
				abajo();
			}
		}
		if(zona)
		{
			if(Input.GetButtonDown("Cancel"))
			{
				zona = false;
			}
		}
	}

	//FLECHAS SELECCIONADAS
	public void Ufondo()
	{
		fondoD = false;
		fondoU = true;
	}
	public void Dfondo()
	{
		fondoU = false;
		fondoD = true;
	}
	public void Uborde()
	{
		bordeD = false;
		bordeU = true;
	}
	public void Dborde()
	{
		bordeU = false;
		bordeD = true;
	}

	//PANEL DE CARTAS
	public bool moverArriba;
	public bool moverAbajo;

	public string nombreItem;
	public string first;
	public string last;

	public void arriba()
	{
		if(nombreItem != first)
		{
			moverArriba = true;
		}
	}

	public void abajo()
	{
		if(nombreItem != last)
		{
			moverAbajo = true;
		}
	}
}
