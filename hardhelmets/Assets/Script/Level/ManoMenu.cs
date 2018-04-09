using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManoMenu : MonoBehaviour {

	public string nombre;

	public UnityEngine.UI.Image imagen;

	//----IMAGENES DE CARTAS
	public Sprite a1;
	public Sprite a2;
	public Sprite a3;
	public Sprite a4;
	public Sprite a5;
	public Sprite a6;
	public Sprite a7;
	public Sprite a8;
	public Sprite a9;
	public Sprite a10;
	public Sprite a11;
	public Sprite a12;
	public Sprite a13;
	public Sprite a14;
	public Sprite a15;
	public Sprite a16;
	public Sprite a17;
	public Sprite a18;
	public Sprite a19;
	public Sprite a20;
	public Sprite a21;
	public Sprite a22;
	public Sprite a23;
	public Sprite a24;

	// Use this for initialization
	void Start ()
	{
		nombre = PlayerPrefs.GetInt("Mano"+nombre).ToString();

		//UNO
		if(nombre == "1")
		{
			imagen.sprite = a1;
		}else if(nombre == "2")
		{
			imagen.sprite = a2;
		}else if(nombre == "3")
		{
			imagen.sprite = a3;
		}else if(nombre == "4")
		{
			imagen.sprite = a4;
		}else if(nombre == "5")
		{
			imagen.sprite = a5;
		}else if(nombre == "6")
		{
			imagen.sprite = a6;
		}else if(nombre == "7")
		{
			imagen.sprite = a7;
		}else if(nombre == "8")
		{
			imagen.sprite = a8;
		}else if(nombre == "9")
		{
			imagen.sprite = a9;
		}else if(nombre == "10")
		{
			imagen.sprite = a10;
		}else if(nombre == "11")
		{
			imagen.sprite = a11;
		}else if(nombre == "12")
		{
			imagen.sprite = a12;
		}else if(nombre == "13")
		{
			imagen.sprite = a13;
		}else if(nombre == "14")
		{
			imagen.sprite = a14;
		}else if(nombre == "15")
		{
			imagen.sprite = a15;
		}else if(nombre == "16")
		{
			imagen.sprite = a16;
		}else if(nombre == "17")
		{
			imagen.sprite = a17;
		}else if(nombre == "18")
		{
			imagen.sprite = a18;
		}else if(nombre == "19")
		{
			imagen.sprite = a19;
		}else if(nombre == "20")
		{
			imagen.sprite = a20;
		}else if(nombre == "21")
		{
			imagen.sprite = a21;
		}else if(nombre == "22")
		{
			imagen.sprite = a22;
		}else if(nombre == "23")
		{
			imagen.sprite = a23;
		}else if(nombre == "24")
		{
			imagen.sprite = a24;
		}
	}
}
