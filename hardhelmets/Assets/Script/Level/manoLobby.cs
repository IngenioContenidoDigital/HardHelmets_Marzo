using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manoLobby : MonoBehaviour {

	public UnityEngine.UI.Image imagen;

	public Sprite a;
	public Sprite b;
	public Sprite c;
	public Sprite d;
	public Sprite e;
	public Sprite f;
	public Sprite g;
	public Sprite h;
	public Sprite i;
	public Sprite j;
	public Sprite k;
	public Sprite l;
	public Sprite m;
	public Sprite n;
	public Sprite o;
	public Sprite p;
	public Sprite q;
	public Sprite r;
	public Sprite s;
	public Sprite t;
	public Sprite u;
	public Sprite v;
	public Sprite w;
	public Sprite x;
	public Sprite y;
	public Sprite nada;

	public string nombre;

	public int carta;

	// Use this for initialization
	void Start ()
	{
		/*
		PlayerPrefs.SetInt(Mano1, uno);
		PlayerPrefs.SetInt(Mano2, dos);
		PlayerPrefs.SetInt(Mano3, tres);
		PlayerPrefs.SetInt(Mano4, cuatro);
		PlayerPrefs.SetInt(Mano5, cinco);
		PlayerPrefs.SetInt(Mano6, seis);
		PlayerPrefs.SetInt(Mano7, siete);
		PlayerPrefs.SetInt(Mano8, ocho);
		PlayerPrefs.SetInt(Mano9, nueve);
		PlayerPrefs.SetInt(Mano10, diez);
		*/
	}
	
	// Update is called once per frame
	void Update ()
	{
		carta = PlayerPrefs.GetInt("Mano"+nombre);

		if(carta == 0)
		{
			imagen.sprite = nada;
		}else if(carta == 1)
		{
			imagen.sprite = a;
		}else if(carta == 2)
		{
			imagen.sprite = b;
		}else if(carta == 3)
		{
			imagen.sprite = c;
		}else if(carta == 4)
		{
			imagen.sprite = d;
		}else if(carta == 5)
		{
			imagen.sprite = e;
		}else if(carta == 6)
		{
			imagen.sprite = f;
		}else if(carta == 7)
		{
			imagen.sprite = g;
		}else if(carta == 8)
		{
			imagen.sprite = h;
		}else if(carta == 9)
		{
			imagen.sprite = i;
		}else if(carta == 10)
		{
			imagen.sprite = j;
		}else if(carta == 11)
		{
			imagen.sprite = k;
		}else if(carta == 12)
		{
			imagen.sprite = l;
		}else if(carta == 13)
		{
			imagen.sprite = m;
		}else if(carta == 14)
		{
			imagen.sprite = n;
		}else if(carta == 15)
		{
			imagen.sprite = o;
		}else if(carta == 16)
		{
			imagen.sprite = p;
		}else if(carta == 17)
		{
			imagen.sprite = q;
		}else if(carta == 18)
		{
			imagen.sprite = r;
		}else if(carta == 19)
		{
			imagen.sprite = s;
		}else if(carta == 20)
		{
			imagen.sprite = t;
		}else if(carta == 21)
		{
			imagen.sprite = u;
		}else if(carta == 22)
		{
			imagen.sprite = v;
		}else if(carta == 23)
		{
			imagen.sprite = w;
		}else if(carta == 24)
		{
			imagen.sprite = x;
		}else if(carta == 25)
		{
			imagen.sprite = y;
		}
	}
}
