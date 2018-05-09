using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cartas : MonoBehaviour {

	public static int uno;
	public static int dos;
	public static int tres;
	public static int cuatro;
	public static int cinco;
	public static int seis;
	public static int siete;
	public static int ocho;
	public static int nueve;
	public static int diez;

	public GameObject primera;
	bool primeraListo;
	public GameObject segunda;
	bool segundaListo;
	public GameObject tercera;
	bool terceraListo;
	public GameObject cuarta;
	bool cuartaListo;
	public GameObject quinta;
	bool quintaListo;
	public GameObject sexta;
	bool sextaListo;
	public GameObject septima;
	bool septimaListo;
	public GameObject octava;
	bool octavaListo;
	public GameObject novena;
	bool novenaListo;
	public GameObject decima;
	bool decimaListo;

	//IMAGENES
	public static int seleccionada;//numero de carta seleccionda

	// Use this for initialization
	void Start ()
	{
		uno = PlayerPrefs.GetInt("Mano1");
		dos = PlayerPrefs.GetInt("Mano2");
		tres = PlayerPrefs.GetInt("Mano3");
		cuatro = PlayerPrefs.GetInt("Mano4");
		cinco = PlayerPrefs.GetInt("Mano5");
		seis = PlayerPrefs.GetInt("Mano6");
		siete = PlayerPrefs.GetInt("Mano7");
		ocho = PlayerPrefs.GetInt("Mano8");
		nueve = PlayerPrefs.GetInt("Mano9");
		diez = PlayerPrefs.GetInt("Mano10");

		if(uno > 0)
		{
			primeraListo = true;
			GameObject card = GameObject.Find(uno.ToString());
			card.GetComponent<Cards>().usada = true;
		}
		if(dos > 0)
		{
			segundaListo = true;
			GameObject card = GameObject.Find(dos.ToString());
			card.GetComponent<Cards>().usada = true;
		}
		if(tres > 0)
		{
			terceraListo = true;
			GameObject card = GameObject.Find(tres.ToString());
			card.GetComponent<Cards>().usada = true;
		}
		if(cuatro > 0)
		{
			cuartaListo = true;
			GameObject card = GameObject.Find(cuatro.ToString());
			card.GetComponent<Cards>().usada = true;
		}
		if(cinco > 0)
		{
			quintaListo = true;
			GameObject card = GameObject.Find(cinco.ToString());
			card.GetComponent<Cards>().usada = true;
		}
		if(seis > 0)
		{
			sextaListo = true;
			GameObject card = GameObject.Find(seis.ToString());
			card.GetComponent<Cards>().usada = true;
		}
		if(siete > 0)
		{
			septimaListo = true;
			GameObject card = GameObject.Find(siete.ToString());
			card.GetComponent<Cards>().usada = true;
		}
		if(ocho > 0)
		{
			octavaListo = true;
			GameObject card = GameObject.Find(ocho.ToString());
			card.GetComponent<Cards>().usada = true;
		}
		if(nueve > 0)
		{
			novenaListo = true;
			GameObject card = GameObject.Find(nueve.ToString());
			card.GetComponent<Cards>().usada = true;
		}
		if(diez > 0)
		{
			decimaListo = true;
			GameObject card = GameObject.Find(diez.ToString());
			card.GetComponent<Cards>().usada = true;
		}
		//-
		primera.GetComponent<combinedSkins>().skinsToCombine[0] = uno.ToString();
		//--
		segunda.GetComponent<combinedSkins>().skinsToCombine[0] = dos.ToString();
		//---
		tercera.GetComponent<combinedSkins>().skinsToCombine[0] = tres.ToString();
		//----
		cuarta.GetComponent<combinedSkins>().skinsToCombine[0] = cuatro.ToString();
		//-----
		quinta.GetComponent<combinedSkins>().skinsToCombine[0] = cinco.ToString();
		//------
		sexta.GetComponent<combinedSkins>().skinsToCombine[0] = seis.ToString();
		//-------
		septima.GetComponent<combinedSkins>().skinsToCombine[0] = siete.ToString();
		//--------
		octava.GetComponent<combinedSkins>().skinsToCombine[0] = ocho.ToString();
		//---------
		novena.GetComponent<combinedSkins>().skinsToCombine[0] = nueve.ToString();
		//----------
		decima.GetComponent<combinedSkins>().skinsToCombine[0] = diez.ToString();

	}
	
	// Update is called once per frame
	void Update ()
	{
		primera.GetComponent<combinedSkins>().skinsToCombine[0] = uno.ToString();
		segunda.GetComponent<combinedSkins>().skinsToCombine[0] = dos.ToString();
		tercera.GetComponent<combinedSkins>().skinsToCombine[0] = tres.ToString();
		cuarta.GetComponent<combinedSkins>().skinsToCombine[0] = cuatro.ToString();
		quinta.GetComponent<combinedSkins>().skinsToCombine[0] = cinco.ToString();
		sexta.GetComponent<combinedSkins>().skinsToCombine[0] = seis.ToString();
		septima.GetComponent<combinedSkins>().skinsToCombine[0] = siete.ToString();
		octava.GetComponent<combinedSkins>().skinsToCombine[0] = ocho.ToString();
		novena.GetComponent<combinedSkins>().skinsToCombine[0] = nueve.ToString();
		decima.GetComponent<combinedSkins>().skinsToCombine[0] = diez.ToString();

		//IMAGEN
		if(uno == 0)
		{
			primera.GetComponent<combinedSkins>().skinsToCombine[0] = "default";
		}else if(!primeraListo)
		{
			uno = seleccionada;
			primeraListo = true;
		}
		if(dos == 0)
		{
			segunda.GetComponent<combinedSkins>().skinsToCombine[0] = "default";
		}else if(!segundaListo)
		{
			dos = seleccionada;
			segundaListo = true;
		}
		if(tres == 0)
		{
			tercera.GetComponent<combinedSkins>().skinsToCombine[0] = "default";
		}else if(!terceraListo)
		{
			tres = seleccionada;
			terceraListo = true;
		}
		if(cuatro == 0)
		{
			cuarta.GetComponent<combinedSkins>().skinsToCombine[0] = "default";
		}else if(!cuartaListo)
		{
			cuatro = seleccionada;
			cuartaListo = true;
		}
		if(cinco == 0)
		{
			quinta.GetComponent<combinedSkins>().skinsToCombine[0] = "default";
		}else if(!quintaListo)
		{
			cinco = seleccionada;
			quintaListo = true;
		}
		if(seis == 0)
		{
			sexta.GetComponent<combinedSkins>().skinsToCombine[0] = "default";
		}else if(!sextaListo)
		{
			seis = seleccionada;
			sextaListo = true;
		}
		if(siete == 0)
		{
			septima.GetComponent<combinedSkins>().skinsToCombine[0] = "default";
		}else if(!septimaListo)
		{
			siete = seleccionada;
			septimaListo = true;
		}
		if(ocho == 0)
		{
			octava.GetComponent<combinedSkins>().skinsToCombine[0] = "default";
		}else if(!octavaListo)
		{
			ocho = seleccionada;
			octavaListo = true;
		}
		if(nueve == 0)
		{
			novena.GetComponent<combinedSkins>().skinsToCombine[0] = "default";
		}else if(!novenaListo)
		{
			nueve = seleccionada;
			novenaListo = true;
		}
		if(diez == 0)
		{
			decima.GetComponent<combinedSkins>().skinsToCombine[0] = "default";
		}else if(!decimaListo)
		{
			diez = seleccionada;
			decimaListo = true;
		}
	}

	public void cambiar1 ()
	{
		if(uno != 0)
		{
			GameObject card = GameObject.Find(uno.ToString());

			primeraListo = false;
			uno = 0;

			card.GetComponent<Cards>().usada = false;
		}
		GetComponent<AudioSource>().volume = 1;
		GetComponent<AudioSource>().clip = selec[Random.Range(0,selec.Length)];
		GetComponent<AudioSource>().Play();
	}
	public void cambiar2 ()
	{
		if(dos != 0)
		{
			GameObject card = GameObject.Find(dos.ToString());

			segundaListo = false;
			dos = 0;

			card.GetComponent<Cards>().usada = false;
		}
		GetComponent<AudioSource>().volume = 1;
		GetComponent<AudioSource>().clip = selec[Random.Range(0,selec.Length)];
		GetComponent<AudioSource>().Play();
	}
	public void cambiar3 ()
	{
		if(tres != 0)
		{
			GameObject card = GameObject.Find(tres.ToString());

			terceraListo = false;
			tres = 0;

			card.GetComponent<Cards>().usada = false;
		}
		GetComponent<AudioSource>().volume = 1;
		GetComponent<AudioSource>().clip = selec[Random.Range(0,selec.Length)];
		GetComponent<AudioSource>().Play();
	}
	public void cambiar4 ()
	{
		if(cuatro != 0)
		{
			GameObject card = GameObject.Find(cuatro.ToString());

			cuartaListo = false;
			cuatro = 0;

			card.GetComponent<Cards>().usada = false;
		}
		GetComponent<AudioSource>().volume = 1;
		GetComponent<AudioSource>().clip = selec[Random.Range(0,selec.Length)];
		GetComponent<AudioSource>().Play();
	}
	public void cambiar5 ()
	{
		if(cinco != 0)
		{
			GameObject card = GameObject.Find(cinco.ToString());

			quintaListo = false;
			cinco = 0;

			card.GetComponent<Cards>().usada = false;
		}
		GetComponent<AudioSource>().volume = 1;
		GetComponent<AudioSource>().clip = selec[Random.Range(0,selec.Length)];
		GetComponent<AudioSource>().Play();
	}
	public void cambiar6 ()
	{
		if(seis != 0)
		{
			GameObject card = GameObject.Find(seis.ToString());

			sextaListo = false;
			seis = 0;

			card.GetComponent<Cards>().usada = false;
		}
		GetComponent<AudioSource>().volume = 1;
		GetComponent<AudioSource>().clip = selec[Random.Range(0,selec.Length)];
		GetComponent<AudioSource>().Play();
	}
	public void cambiar7 ()
	{
		if(siete != 0)
		{
			GameObject card = GameObject.Find(siete.ToString());

			septimaListo = false;
			siete = 0;

			card.GetComponent<Cards>().usada = false;
		}
		GetComponent<AudioSource>().volume = 1;
		GetComponent<AudioSource>().clip = selec[Random.Range(0,selec.Length)];
		GetComponent<AudioSource>().Play();
	}
	public void cambiar8 ()
	{
		if(ocho != 0)
		{
			GameObject card = GameObject.Find(ocho.ToString());

			octavaListo = false;
			ocho = 0;

			card.GetComponent<Cards>().usada = false;
		}
		GetComponent<AudioSource>().volume = 1;
		GetComponent<AudioSource>().clip = selec[Random.Range(0,selec.Length)];
		GetComponent<AudioSource>().Play();
	}
	public void cambiar9 ()
	{
		if(nueve != 0)
		{
			GameObject card = GameObject.Find(nueve.ToString());

			novenaListo = false;
			nueve = 0;

			card.GetComponent<Cards>().usada = false;
		}
		GetComponent<AudioSource>().volume = 1;
		GetComponent<AudioSource>().clip = selec[Random.Range(0,selec.Length)];
		GetComponent<AudioSource>().Play();
	}
	public void cambiar10 ()
	{
		if(diez != 0)
		{
			GameObject card = GameObject.Find(diez.ToString());

			decimaListo = false;
			diez = 0;

			card.GetComponent<Cards>().usada = false;
		}
		GetComponent<AudioSource>().volume = 1;
		GetComponent<AudioSource>().clip = selec[Random.Range(0,selec.Length)];
		GetComponent<AudioSource>().Play();
	}

	public AudioClip[] selec;
}
