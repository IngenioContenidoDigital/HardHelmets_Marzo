using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vender : MonoBehaviour {

	public int nombre;
	int monedas;


	public UnityEngine.UI.Text monedasT;

	public GameObject ventana;

	public UnityEngine.UI.Image carta;

	public int cantidad;
	public int cantidadUsadas;
	public int eliminar;

	public UnityEngine.UI.Text una;
	public UnityEngine.UI.Text todas;

	int valor;
	int valorTotal;

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

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		//cantidadUsadas = PlayerPrefs.GetInt("card"+nombre+"cantidadUsadas");
		//cantidad = PlayerPrefs.GetInt("card"+nombre+"cantidad");

		monedas = PlayerPrefs.GetInt("monedas");
		monedasT.text = monedas.ToString();

		if(cantidad == 1)
		{
			eliminar = 1;
		}else
		{
			eliminar = cantidad-1;
		}

		valor = PlayerPrefs.GetInt("card"+nombre+"valor");
		valorTotal = valor*eliminar;

		una.text = "Desea cambiar 1 carta por: "+valor;
		todas.text = "Desea cambiar "+eliminar+" por: "+valorTotal;

		if(nombre == 1)
		{
			carta.sprite = a1;
		}else if(nombre == 2)
		{
			carta.sprite = a2;
		}else if(nombre == 3)
		{
			carta.sprite = a3;
		}else if(nombre == 4)
		{
			carta.sprite = a4;
		}else if(nombre == 5)
		{
			carta.sprite = a5;
		}else if(nombre == 6)
		{
			carta.sprite = a6;
		}else if(nombre == 7)
		{
			carta.sprite = a7;
		}else if(nombre == 8)
		{
			carta.sprite = a8;
		}else if(nombre == 9)
		{
			carta.sprite = a9;
		}else if(nombre == 10)
		{
			carta.sprite = a10;
		}else if(nombre == 11)
		{
			carta.sprite = a11;
		}else if(nombre == 12)
		{
			carta.sprite = a12;
		}else if(nombre == 13)
		{
			carta.sprite = a13;
		}else if(nombre == 14)
		{
			carta.sprite = a14;
		}else if(nombre == 15)
		{
			carta.sprite = a15;
		}else if(nombre == 16)
		{
			carta.sprite = a16;
		}else if(nombre == 17)
		{
			carta.sprite = a17;
		}else if(nombre == 18)
		{
			carta.sprite = a18;
		}

		if(cantidad == 0)
		{
			ventana.SetActive(false);
		}
	}

	public void venderuna()
	{
		cantidad -= 1;
		PlayerPrefs.SetInt("card"+nombre+"cantidad", PlayerPrefs.GetInt("card"+nombre+"cantidad")-1);

		monedas += valor;
		PlayerPrefs.SetInt("monedas", monedas);
	}
		
	public void vendertodas()
	{
		if(eliminar == 1 && cantidad == 1 && cantidadUsadas == 0)
		{
			PlayerPrefs.SetInt("card"+nombre, 0);
			PlayerPrefs.SetInt("card"+nombre+"cantidad", 0);
			PlayerPrefs.SetInt("card"+nombre+"cantidadUsadas", 0);
		}else if(cantidadUsadas >= 1)
		{
			cantidad -= eliminar;
			PlayerPrefs.SetInt("card"+nombre+"cantidad", PlayerPrefs.GetInt("card"+nombre+"cantidad")-eliminar);

			//PlayerPrefs.SetInt("card"+nombre+"cantidadUsadas", cantidadUsadas);
		}else
		{
			cantidad -= eliminar;
			PlayerPrefs.SetInt("card"+nombre+"cantidad", PlayerPrefs.GetInt("card"+nombre+"cantidad")-eliminar);
		}

		monedas += valorTotal;
		PlayerPrefs.SetInt("monedas", monedas);
	}

	public void cerrar()
	{
		ventana.SetActive(false);
	}
}
