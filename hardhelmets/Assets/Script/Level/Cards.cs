﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Cards : MonoBehaviour {

	public int carta;
	public int level;
	public int cantidadTotal;

	public bool usada;

	public UnityEngine.UI.Text cantidaT;
	public UnityEngine.UI.Image imagen;
	public UnityEngine.UI.Text levelT;

	//SUBMINT BUTTON EN CARTA
	public Button joinButton;
	public bool selected;

	void Start ()
	{
		carta = int.Parse(gameObject.name);

		cantidadTotal = PlayerPrefs.GetInt("card"+carta);

		level = PlayerPrefs.GetInt("card"+carta+"level");
	}
	public AudioSource audio1;
	public void click ()
	{
		if(!usada && mano.GetComponent<Mano>().zona && selected)
		{
			audio1.volume = 1;
			audio1.clip = selec[Random.Range(0,selec.Length)];
			audio1.Play();

			if(Cartas.uno == 0 || Cartas.dos == 0 || Cartas.tres == 0 || Cartas.cuatro == 0 || Cartas.cinco == 0 ||
				Cartas.seis == 0 || Cartas.siete == 0 || Cartas.ocho == 0 || Cartas.nueve == 0 || Cartas.diez == 0)
			{
				//Cartas.libre = carta;
				//cantidad -=1;
				Cartas.seleccionada = carta;

				if(Cartas.uno == 0)
				{
					Cartas.uno = carta;
				}else if(Cartas.dos == 0)
				{
					Cartas.dos = carta;
				}else if(Cartas.tres == 0)
				{
					Cartas.tres = carta;
				}else if(Cartas.cuatro == 0)
				{
					Cartas.cuatro = carta;
				}else if(Cartas.cinco == 0)
				{
					Cartas.cinco = carta;
				}else if(Cartas.seis == 0)
				{
					Cartas.seis = carta;
				}else if(Cartas.siete == 0)
				{
					Cartas.siete = carta;
				}else if(Cartas.ocho == 0)
				{
					Cartas.ocho = carta;
				}else if(Cartas.nueve == 0)
				{
					Cartas.nueve = carta;
				}else if(Cartas.diez == 0)
				{
					Cartas.diez = carta;
				}

				usada = true;

				//GetComponent<Button>().enabled = false;
				//GetComponent<Image>().color = new Color32(100,100,100,100);

			}
		}
	}
	public Sprite atras;
	public Sprite normal;
	public Sprite volteada;
	public GameObject boton;

	void Update ()
	{
		if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("CardSelect"))
		{
			GetComponent<Animator>().SetBool("entra", false);
		}
		if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("CardDeselect"))
		{
			GetComponent<Animator>().SetBool("sale", false);
		}

		//ACTUALIZA EL VALOR DE CARTAS TOTALES
		if(cantidadTotal != PlayerPrefs.GetInt("card"+carta))
		{
			cantidadTotal = PlayerPrefs.GetInt("card"+carta);
		}
		cantidaT.text = "X "+cantidadTotal.ToString();

		//SI LA CARTA NO EXISTE
		if(PlayerPrefs.GetInt("card"+carta) == 0)
		{
			cantidaT.text = "";
			//boton.SetActive(false);
			GetComponent<Button>().enabled = false;
			imagen.sprite = volteada;
			//GetComponent<Image>().color = new Color32(100,100,100,100);
		}

		if(usada)
		{
			//boton.SetActive(false);
			GetComponent<Button>().enabled = false;
			//GetComponent<Image>().color = new Color32(150,150,150,150);
			imagen.sprite = atras;

		}else if(cantidadTotal > 0)
		{
			//boton.SetActive(true);
			GetComponent<Button>().enabled = true;
			GetComponent<Image>().color = new Color32(255,255,255,255);
			imagen.sprite = normal;
		}
		//SELECCIONAR CARTA CON SUBMIT BURRON
		if(!usada && mano.GetComponent<Mano>().zona && selected && Input.GetButtonDown("Jump") || Input.GetButtonDown("Submit"))
		{
			joinButton.onClick.Invoke();
		}
	}

	public AudioClip[] selec;

	public GameObject sell;
	public GameObject ventana;

	public void Sell ()
	{
		sell.GetComponent<vender>().nombre = carta;
		sell.GetComponent<vender>().cantidad = cantidadTotal;
		ventana.SetActive(true);
	}


	////NUEVO
	public GameObject mano;

	void OnTriggerEnter2D (Collider2D col)
	{
		if(col.gameObject.tag == "Player")
		{
			S_Select ();
			if(cantidadTotal > 0)
			{
				selected = true;
			}

			GetComponent<Animator>().SetBool("entra", true);

			mano.GetComponent<Mano>().nombreCarta = gameObject.name;

			mano.GetComponent<Mano>().moverAdelante = false;
			mano.GetComponent<Mano>().moverAtras = false;
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
	public AudioSource audio2;
	public void S_Select ()
	{
		audio2.Play();
	}
}
