using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Mano : MonoBehaviour {

	public Animator animator;

	public int uno;
	public int dos;
	public int tres;
	public int cuatro;
	public int cinco;
	public int seis;
	public int siete;
	public int ocho;
	public int nueve;
	public int diez;

	//public GameObject listo;

	public UnityEngine.UI.Image primera;
	public UnityEngine.UI.Image segunda;
	public UnityEngine.UI.Image tercera;
	public UnityEngine.UI.Image cuarta;
	public UnityEngine.UI.Image quinta;
	public UnityEngine.UI.Image sexta;
	public UnityEngine.UI.Image septima;
	public UnityEngine.UI.Image octava;
	public UnityEngine.UI.Image novena;
	public UnityEngine.UI.Image decima;


	public GameObject vender;

	public bool guardar;

	//MOVIMIENTOS TECLADO
	public bool zona;

	public bool I;
	public bool D;

	public void izquierda()
	{
		zona = true;
		D = false;
		I = true;
	}
	public void derecha()
	{
		zona = true;
		I = false;
		D = true;
	}
	public void ninguno()
	{
		zona = false;

		I = false;
		D = false;
	}

	void Start ()
	{
		
	}

	public GameObject cuadro;
	public bool coll;

	void Update ()
	{
		if(I && !cuadro.GetComponent<detectaSeleccion>().entrer)
		{
			if(Input.GetButtonDown("left"))
			{
				atras();
			}
			if(Input.GetAxis("Horizontal") < 0 )
			{
				atras();
			}
			if(Input.GetAxis("HorizontalUI") < 0)
			{
				atras();
			}
		}
		if(D && !cuadro.GetComponent<detectaSeleccion>().entrer)
		{
			if(Input.GetButtonDown("right"))
			{
				adelante();
			}
			if(Input.GetAxis("Horizontal") > 0)
			{
				adelante();
			}
			if(Input.GetAxis("HorizontalUI") > 0)
			{
				adelante();
			}
		}

		if(animator.GetCurrentAnimatorStateInfo(0).IsName("BarajaEntra") || animator.GetCurrentAnimatorStateInfo(0).IsName("Baraja2Entra") || animator.GetCurrentAnimatorStateInfo(0).IsName("BarajaEntra3"))
		{
			animator.SetBool("entra", false);
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("BarajaSale") || animator.GetCurrentAnimatorStateInfo(0).IsName("Baraja2Sale") || animator.GetCurrentAnimatorStateInfo(0).IsName("BarajaSale3"))
		{
			animator.SetBool("sale", false);
		}

		if(Cartas.uno != 0 && Cartas.dos != 0 && Cartas.tres != 0 )//&& Cartas.cuatro != 0 && Cartas.cinco != 0 && Cartas.seis != 0 && Cartas.siete != 0 && Cartas.ocho != 0 && Cartas.nueve != 0 && Cartas.diez != 0)
		{
			//listo.SetActive(true);
		}else
		{
			//listo.SetActive(false);
		}

		if(moverAdelante)
		{
			moverAtras = false;
			content.GetComponent<ScrollRect>().horizontalNormalizedPosition += 0.01f;
			if(cuadro.GetComponent<detectaSeleccion>().entrer)
			{
				moverAdelante = false;
			}
		}
		if(moverAtras)
		{
			moverAdelante = false;
			content.GetComponent<ScrollRect>().horizontalNormalizedPosition -= 0.01f;//0.01
			if(cuadro.GetComponent<detectaSeleccion>().entrer)
			{
				moverAtras = false;
			}
		}
		//GUARDA LA BARAJA ACTUAL
		if(Cartas.uno != 0)
		{
			uno = Cartas.uno;
			dos = Cartas.dos;
			tres = Cartas.tres;
			cuatro = Cartas.cuatro;
			cinco = Cartas.cinco;
			seis = Cartas.seis;
			siete = Cartas.siete;
			ocho = Cartas.ocho;
			nueve = Cartas.nueve;
			diez = Cartas.diez;

			PlayerPrefs.SetInt("Mano1", uno);
			PlayerPrefs.SetInt("Mano2", dos);
			PlayerPrefs.SetInt("Mano3", tres);
			PlayerPrefs.SetInt("Mano4", cuatro);
			PlayerPrefs.SetInt("Mano5", cinco);
			PlayerPrefs.SetInt("Mano6", seis);
			PlayerPrefs.SetInt("Mano7", siete);
			PlayerPrefs.SetInt("Mano8", ocho);
			PlayerPrefs.SetInt("Mano9", nueve);
			PlayerPrefs.SetInt("Mano10", diez);

			guardar = true;
		}else
		{
			guardar = false;
		}
	}

	public AudioClip selec;
	public void finalizar ()
	{
		uno = Cartas.uno;
		dos = Cartas.dos;
		tres = Cartas.tres;
		cuatro = Cartas.cuatro;
		cinco = Cartas.cinco;
		seis = Cartas.seis;
		siete = Cartas.siete;
		ocho = Cartas.ocho;
		nueve = Cartas.nueve;
		diez = Cartas.diez;

		PlayerPrefs.SetInt("Mano1", uno);
		PlayerPrefs.SetInt("Mano2", dos);
		PlayerPrefs.SetInt("Mano3", tres);
		PlayerPrefs.SetInt("Mano4", cuatro);
		PlayerPrefs.SetInt("Mano5", cinco);
		PlayerPrefs.SetInt("Mano6", seis);
		PlayerPrefs.SetInt("Mano7", siete);
		PlayerPrefs.SetInt("Mano8", ocho);
		PlayerPrefs.SetInt("Mano9", nueve);
		PlayerPrefs.SetInt("Mano10", diez);

		GetComponent<AudioSource>().volume = 1;
		GetComponent<AudioSource>().clip = selec;
		GetComponent<AudioSource>().Play();
	}

	//PANEL DE CARTAS
	public GameObject content;

	public bool moverAdelante;
	public bool moverAtras;

	public string nombreCarta;
	public string first;
	public string last;

	public float valor;

	public void adelante()
	{
		if(nombreCarta != last)
		{
			moverAdelante = true;
		}
	}

	public void atras()
	{
		if(nombreCarta != first)
		{
			moverAtras = true;
		}
	}
}
