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

	public UnityEngine.UI.Image primera;
	bool primeraListo;
	public UnityEngine.UI.Image segunda;
	bool segundaListo;
	public UnityEngine.UI.Image tercera;
	bool terceraListo;
	public UnityEngine.UI.Image cuarta;
	bool cuartaListo;
	public UnityEngine.UI.Image quinta;
	bool quintaListo;
	public UnityEngine.UI.Image sexta;
	bool sextaListo;
	public UnityEngine.UI.Image septima;
	bool septimaListo;
	public UnityEngine.UI.Image octava;
	bool octavaListo;
	public UnityEngine.UI.Image novena;
	bool novenaListo;
	public UnityEngine.UI.Image decima;
	bool decimaListo;

	//IMAGENES
	public static int seleccionada;//numero de carta seleccionda
	public Sprite blanco;//imagen cuando esta en blanco
	public Sprite imagen;//imagen de la carta seleccionada
	public Sprite a1;//----IMAGENES DE CARTAS
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
	
		/*if(uno == 0)
		{
			uno = 1;
			dos = 2;
			tres = 3;
			cuatro = 4;
			cinco = 5;
			seis = 6;
			siete = 7;
			ocho = 8;
			nueve = 9;
			diez = 10;
		}*/
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
		//UNO
		if(uno == 1)
		{
			primera.sprite = a1;
		}else if(uno == 2)
		{
			primera.sprite = a2;
		}else if(uno == 3)
		{
			primera.sprite = a3;
		}else if(uno == 4)
		{
			primera.sprite = a4;
		}else if(uno == 5)
		{
			primera.sprite = a5;
		}else if(uno == 6)
		{
			primera.sprite = a6;
		}else if(uno == 7)
		{
			primera.sprite = a7;
		}else if(uno == 8)
		{
			primera.sprite = a8;
		}else if(uno == 9)
		{
			primera.sprite = a9;
		}else if(uno == 10)
		{
			primera.sprite = a10;
		}else if(uno == 11)
		{
			primera.sprite = a11;
		}else if(uno == 12)
		{
			primera.sprite = a12;
		}else if(uno == 13)
		{
			primera.sprite = a13;
		}else if(uno == 14)
		{
			primera.sprite = a14;
		}else if(uno == 15)
		{
			primera.sprite = a15;
		}else if(uno == 16)
		{
			primera.sprite = a16;
		}else if(uno == 17)
		{
			primera.sprite = a17;
		}else if(uno == 18)
		{
			primera.sprite = a18;
		}else if(uno == 19)
		{
			primera.sprite = a19;
		}else if(uno == 20)
		{
			primera.sprite = a20;
		}else if(uno == 21)
		{
			primera.sprite = a21;
		}else if(uno == 22)
		{
			primera.sprite = a22;
		}else if(uno == 23)
		{
			primera.sprite = a23;
		}else if(uno == 24)
		{
			primera.sprite = a24;
		}
		//DOS
		if(dos == 1)
		{
			segunda.sprite = a1;
		}else if(dos == 2)
		{
			segunda.sprite = a2;
		}else if(dos == 3)
		{
			segunda.sprite = a3;
		}else if(dos == 4)
		{
			segunda.sprite = a4;
		}else if(dos == 5)
		{
			segunda.sprite = a5;
		}else if(dos == 6)
		{
			segunda.sprite = a6;
		}else if(dos == 7)
		{
			segunda.sprite = a7;
		}else if(dos == 8)
		{
			segunda.sprite = a8;
		}else if(dos == 9)
		{
			segunda.sprite = a9;
		}else if(dos == 10)
		{
			segunda.sprite = a10;
		}else if(dos == 11)
		{
			segunda.sprite = a11;
		}else if(dos == 12)
		{
			segunda.sprite = a12;
		}else if(dos == 13)
		{
			segunda.sprite = a13;
		}else if(dos == 14)
		{
			segunda.sprite = a14;
		}else if(dos == 15)
		{
			segunda.sprite = a15;
		}else if(dos == 16)
		{
			segunda.sprite = a16;
		}else if(dos == 17)
		{
			segunda.sprite = a17;
		}else if(dos == 18)
		{
			segunda.sprite = a18;
		}else if(dos == 19)
		{
			segunda.sprite = a19;
		}else if(dos == 20)
		{
			segunda.sprite = a20;
		}else if(dos == 21)
		{
			segunda.sprite = a21;
		}else if(dos == 22)
		{
			segunda.sprite = a22;
		}else if(dos == 23)
		{
			segunda.sprite = a23;
		}else if(dos == 24)
		{
			segunda.sprite = a24;
		}
		//TRES
		if(tres == 1)
		{
			tercera.sprite = a1;
		}else if(tres == 2)
		{
			tercera.sprite = a2;
		}else if(tres == 3)
		{
			tercera.sprite = a3;
		}else if(tres == 4)
		{
			tercera.sprite = a4;
		}else if(tres == 5)
		{
			tercera.sprite = a5;
		}else if(tres == 6)
		{
			tercera.sprite = a6;
		}else if(tres == 7)
		{
			tercera.sprite = a7;
		}else if(tres == 8)
		{
			tercera.sprite = a8;
		}else if(tres == 9)
		{
			tercera.sprite = a9;
		}else if(tres == 10)
		{
			tercera.sprite = a10;
		}else if(tres == 11)
		{
			tercera.sprite = a11;
		}else if(tres == 12)
		{
			tercera.sprite = a12;
		}else if(tres == 13)
		{
			tercera.sprite = a13;
		}else if(tres == 14)
		{
			tercera.sprite = a14;
		}else if(tres == 15)
		{
			tercera.sprite = a15;
		}else if(tres == 16)
		{
			tercera.sprite = a16;
		}else if(tres == 17)
		{
			tercera.sprite = a17;
		}else if(tres == 18)
		{
			tercera.sprite = a18;
		}else if(tres == 19)
		{
			tercera.sprite = a19;
		}else if(tres == 20)
		{
			tercera.sprite = a20;
		}else if(tres == 21)
		{
			tercera.sprite = a21;
		}else if(tres == 22)
		{
			tercera.sprite = a22;
		}else if(tres == 23)
		{
			tercera.sprite = a23;
		}else if(tres == 24)
		{
			tercera.sprite = a24;
		}
		//CUATRO
		if(cuatro == 1)
		{
			cuarta.sprite = a1;
		}else if(cuatro == 2)
		{
			cuarta.sprite = a2;
		}else if(cuatro == 3)
		{
			cuarta.sprite = a3;
		}else if(cuatro == 4)
		{
			cuarta.sprite = a4;
		}else if(cuatro == 5)
		{
			cuarta.sprite = a5;
		}else if(cuatro == 6)
		{
			cuarta.sprite = a6;
		}else if(cuatro == 7)
		{
			cuarta.sprite = a7;
		}else if(cuatro == 8)
		{
			cuarta.sprite = a8;
		}else if(cuatro == 9)
		{
			cuarta.sprite = a9;
		}else if(cuatro == 10)
		{
			cuarta.sprite = a10;
		}else if(cuatro == 11)
		{
			cuarta.sprite = a11;
		}else if(cuatro == 12)
		{
			cuarta.sprite = a12;
		}else if(cuatro == 13)
		{
			cuarta.sprite = a13;
		}else if(cuatro == 14)
		{
			cuarta.sprite = a14;
		}else if(cuatro == 15)
		{
			cuarta.sprite = a15;
		}else if(cuatro == 16)
		{
			cuarta.sprite = a16;
		}else if(cuatro == 17)
		{
			cuarta.sprite = a17;
		}else if(cuatro == 18)
		{
			cuarta.sprite = a18;
		}else if(cuatro == 19)
		{
			cuarta.sprite = a19;
		}else if(cuatro == 20)
		{
			cuarta.sprite = a20;
		}else if(cuatro == 21)
		{
			cuarta.sprite = a21;
		}else if(cuatro == 22)
		{
			cuarta.sprite = a22;
		}else if(cuatro == 23)
		{
			cuarta.sprite = a23;
		}else if(cuatro == 24)
		{
			cuarta.sprite = a24;
		}
		//CINCO
		if(cinco == 1)
		{
			quinta.sprite = a1;
		}else if(cinco == 2)
		{
			quinta.sprite = a2;
		}else if(cinco == 3)
		{
			quinta.sprite = a3;
		}else if(cinco == 4)
		{
			quinta.sprite = a4;
		}else if(cinco == 5)
		{
			quinta.sprite = a5;
		}else if(cinco == 6)
		{
			quinta.sprite = a6;
		}else if(cinco == 7)
		{
			quinta.sprite = a7;
		}else if(cinco == 8)
		{
			quinta.sprite = a8;
		}else if(cinco == 9)
		{
			quinta.sprite = a9;
		}else if(cinco == 10)
		{
			quinta.sprite = a10;
		}else if(cinco == 11)
		{
			quinta.sprite = a11;
		}else if(cinco == 12)
		{
			quinta.sprite = a12;
		}else if(cinco == 13)
		{
			quinta.sprite = a13;
		}else if(cinco == 14)
		{
			quinta.sprite = a14;
		}else if(cinco == 15)
		{
			quinta.sprite = a15;
		}else if(cinco == 16)
		{
			quinta.sprite = a16;
		}else if(cinco == 17)
		{
			quinta.sprite = a17;
		}else if(cinco == 18)
		{
			quinta.sprite = a18;
		}else if(cinco == 19)
		{
			quinta.sprite = a19;
		}else if(cinco == 20)
		{
			quinta.sprite = a20;
		}else if(cinco == 21)
		{
			quinta.sprite = a21;
		}else if(cinco == 22)
		{
			quinta.sprite = a22;
		}else if(cinco == 23)
		{
			quinta.sprite = a23;
		}else if(cinco == 24)
		{
			quinta.sprite = a24;
		}
		//SEIS
		if(seis == 1)
		{
			sexta.sprite = a1;
		}else if(seis == 2)
		{
			sexta.sprite = a2;
		}else if(seis == 3)
		{
			sexta.sprite = a3;
		}else if(seis == 4)
		{
			sexta.sprite = a4;
		}else if(seis == 5)
		{
			sexta.sprite = a5;
		}else if(seis == 6)
		{
			sexta.sprite = a6;
		}else if(seis == 7)
		{
			sexta.sprite = a7;
		}else if(seis == 8)
		{
			sexta.sprite = a8;
		}else if(seis == 9)
		{
			sexta.sprite = a9;
		}else if(seis == 10)
		{
			sexta.sprite = a10;
		}else if(seis == 11)
		{
			sexta.sprite = a11;
		}else if(seis == 12)
		{
			sexta.sprite = a12;
		}else if(seis == 13)
		{
			sexta.sprite = a13;
		}else if(seis == 14)
		{
			sexta.sprite = a14;
		}else if(seis == 15)
		{
			sexta.sprite = a15;
		}else if(seis == 16)
		{
			sexta.sprite = a16;
		}else if(seis == 17)
		{
			sexta.sprite = a17;
		}else if(seis == 18)
		{
			sexta.sprite = a18;
		}else if(seis == 19)
		{
			sexta.sprite = a19;
		}else if(seis == 20)
		{
			sexta.sprite = a20;
		}else if(seis == 21)
		{
			sexta.sprite = a21;
		}else if(seis == 22)
		{
			sexta.sprite = a22;
		}else if(seis == 23)
		{
			sexta.sprite = a23;
		}else if(seis == 24)
		{
			sexta.sprite = a24;
		}
		//SIETE
		if(siete == 1)
		{
			septima.sprite = a1;
		}else if(siete == 2)
		{
			septima.sprite = a2;
		}else if(siete == 3)
		{
			septima.sprite = a3;
		}else if(siete == 4)
		{
			septima.sprite = a4;
		}else if(siete == 5)
		{
			septima.sprite = a5;
		}else if(siete == 6)
		{
			septima.sprite = a6;
		}else if(siete == 7)
		{
			septima.sprite = a7;
		}else if(siete == 8)
		{
			septima.sprite = a8;
		}else if(siete == 9)
		{
			septima.sprite = a9;
		}else if(siete == 10)
		{
			septima.sprite = a10;
		}else if(siete == 11)
		{
			septima.sprite = a11;
		}else if(siete == 12)
		{
			septima.sprite = a12;
		}else if(siete == 13)
		{
			septima.sprite = a13;
		}else if(siete == 14)
		{
			septima.sprite = a14;
		}else if(siete == 15)
		{
			septima.sprite = a15;
		}else if(siete == 16)
		{
			septima.sprite = a16;
		}else if(siete == 17)
		{
			septima.sprite = a17;
		}else if(siete == 18)
		{
			septima.sprite = a18;
		}else if(siete == 19)
		{
			septima.sprite = a19;
		}else if(siete == 20)
		{
			septima.sprite = a20;
		}else if(siete == 21)
		{
			septima.sprite = a21;
		}else if(siete == 22)
		{
			septima.sprite = a22;
		}else if(siete == 23)
		{
			septima.sprite = a23;
		}else if(siete == 24)
		{
			septima.sprite = a24;
		}
		//OCHO
		if(ocho == 1)
		{
			octava.sprite = a1;
		}else if(ocho == 2)
		{
			octava.sprite = a2;
		}else if(ocho == 3)
		{
			octava.sprite = a3;
		}else if(ocho == 4)
		{
			octava.sprite = a4;
		}else if(ocho == 5)
		{
			octava.sprite = a5;
		}else if(ocho == 6)
		{
			octava.sprite = a6;
		}else if(ocho == 7)
		{
			octava.sprite = a7;
		}else if(ocho == 8)
		{
			octava.sprite = a8;
		}else if(ocho == 9)
		{
			octava.sprite = a9;
		}else if(ocho == 10)
		{
			octava.sprite = a10;
		}else if(ocho == 11)
		{
			octava.sprite = a11;
		}else if(ocho == 12)
		{
			octava.sprite = a12;
		}else if(ocho == 13)
		{
			octava.sprite = a13;
		}else if(ocho == 14)
		{
			octava.sprite = a14;
		}else if(ocho == 15)
		{
			octava.sprite = a15;
		}else if(ocho == 16)
		{
			octava.sprite = a16;
		}else if(ocho == 17)
		{
			octava.sprite = a17;
		}else if(ocho == 18)
		{
			octava.sprite = a18;
		}else if(ocho == 19)
		{
			octava.sprite = a19;
		}else if(ocho == 20)
		{
			octava.sprite = a20;
		}else if(ocho == 21)
		{
			octava.sprite = a21;
		}else if(ocho == 22)
		{
			octava.sprite = a22;
		}else if(ocho == 23)
		{
			octava.sprite = a23;
		}else if(ocho == 24)
		{
			octava.sprite = a24;
		}
		//NUEVE
		if(nueve == 1)
		{
			novena.sprite = a1;
		}else if(nueve == 2)
		{
			novena.sprite = a2;
		}else if(nueve == 3)
		{
			novena.sprite = a3;
		}else if(nueve == 4)
		{
			novena.sprite = a4;
		}else if(nueve == 5)
		{
			novena.sprite = a5;
		}else if(nueve == 6)
		{
			novena.sprite = a6;
		}else if(nueve == 7)
		{
			novena.sprite = a7;
		}else if(nueve == 8)
		{
			novena.sprite = a8;
		}else if(nueve == 9)
		{
			novena.sprite = a9;
		}else if(nueve == 10)
		{
			novena.sprite = a10;
		}else if(nueve == 11)
		{
			novena.sprite = a11;
		}else if(nueve == 12)
		{
			novena.sprite = a12;
		}else if(nueve == 13)
		{
			novena.sprite = a13;
		}else if(nueve == 14)
		{
			novena.sprite = a14;
		}else if(nueve == 15)
		{
			novena.sprite = a15;
		}else if(nueve == 16)
		{
			novena.sprite = a16;
		}else if(nueve == 17)
		{
			novena.sprite = a17;
		}else if(nueve == 18)
		{
			novena.sprite = a18;
		}else if(nueve == 19)
		{
			novena.sprite = a19;
		}else if(nueve == 20)
		{
			novena.sprite = a20;
		}else if(nueve == 21)
		{
			novena.sprite = a21;
		}else if(nueve == 22)
		{
			novena.sprite = a22;
		}else if(nueve == 23)
		{
			novena.sprite = a23;
		}else if(nueve == 24)
		{
			novena.sprite = a24;
		}
		//DIEZ
		if(diez == 1)
		{
			decima.sprite = a1;
		}else if(diez == 2)
		{
			decima.sprite = a2;
		}else if(diez == 3)
		{
			decima.sprite = a3;
		}else if(diez == 4)
		{
			decima.sprite = a4;
		}else if(diez == 5)
		{
			decima.sprite = a5;
		}else if(diez == 6)
		{
			decima.sprite = a6;
		}else if(diez == 7)
		{
			decima.sprite = a7;
		}else if(diez == 8)
		{
			decima.sprite = a8;
		}else if(diez == 9)
		{
			decima.sprite = a9;
		}else if(diez == 10)
		{
			decima.sprite = a10;
		}else if(diez == 11)
		{
			decima.sprite = a11;
		}else if(diez == 12)
		{
			decima.sprite = a12;
		}else if(diez == 13)
		{
			decima.sprite = a13;
		}else if(diez == 14)
		{
			decima.sprite = a14;
		}else if(diez == 15)
		{
			decima.sprite = a15;
		}else if(diez == 16)
		{
			decima.sprite = a16;
		}else if(diez == 17)
		{
			decima.sprite = a17;
		}else if(diez == 18)
		{
			decima.sprite = a18;
		}else if(diez == 19)
		{
			decima.sprite = a19;
		}else if(diez == 20)
		{
			decima.sprite = a20;
		}else if(diez == 21)
		{
			decima.sprite = a21;
		}else if(diez == 22)
		{
			decima.sprite = a22;
		}else if(diez == 23)
		{
			decima.sprite = a23;
		}else if(diez == 24)
		{
			decima.sprite = a24;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(seleccionada == 1)
		{
			imagen = a1;
		}else if(seleccionada == 2)
		{
			imagen = a2;
		}else if(seleccionada == 3)
		{
			imagen = a3;
		}else if(seleccionada == 4)
		{
			imagen = a4;
		}else if(seleccionada == 5)
		{
			imagen = a5;
		}else if(seleccionada == 6)
		{
			imagen = a6;
		}else if(seleccionada == 7)
		{
			imagen = a7;
		}else if(seleccionada == 8)
		{
			imagen = a8;
		}else if(seleccionada == 9)
		{
			imagen = a9;
		}else if(seleccionada == 10)
		{
			imagen = a10;
		}else if(seleccionada == 11)
		{
			imagen = a11;
		}else if(seleccionada == 12)
		{
			imagen = a12;
		}else if(seleccionada == 13)
		{
			imagen = a13;
		}else if(seleccionada == 14)
		{
			imagen = a14;
		}else if(seleccionada == 15)
		{
			imagen = a15;
		}else if(seleccionada == 16)
		{
			imagen = a16;
		}else if(seleccionada == 17)
		{
			imagen = a17;
		}else if(seleccionada == 18)
		{
			imagen = a18;
		}else if(seleccionada == 19)
		{
			imagen = a19;
		}else if(seleccionada == 20)
		{
			imagen = a20;
		}else if(seleccionada == 21)
		{
			imagen = a21;
		}else if(seleccionada == 22)
		{
			imagen = a22;
		}else if(seleccionada == 23)
		{
			imagen = a23;
		}else if(seleccionada == 24)
		{
			imagen = a24;
		}

		//IMAGEN
		if(uno == 0)
		{
			primera.sprite = blanco;
		}else if(!primeraListo)
		{
			uno = seleccionada;
			primera.sprite = imagen;
			primeraListo = true;
		}
		if(dos == 0)
		{
			segunda.sprite = blanco;
		}else if(!segundaListo)
		{
			dos = seleccionada;
			segunda.sprite = imagen;
			segundaListo = true;
		}
		if(tres == 0)
		{
			tercera.sprite = blanco;
		}else if(!terceraListo)
		{
			tres = seleccionada;
			tercera.sprite = imagen;
			terceraListo = true;
		}
		if(cuatro == 0)
		{
			cuarta.sprite = blanco;
		}else if(!cuartaListo)
		{
			cuatro = seleccionada;
			cuarta.sprite = imagen;
			cuartaListo = true;
		}
		if(cinco == 0)
		{
			quinta.sprite = blanco;
		}else if(!quintaListo)
		{
			cinco = seleccionada;
			quinta.sprite = imagen;
			quintaListo = true;
		}
		if(seis == 0)
		{
			sexta.sprite = blanco;
		}else if(!sextaListo)
		{
			seis = seleccionada;
			sexta.sprite = imagen;
			sextaListo = true;
		}
		if(siete == 0)
		{
			septima.sprite = blanco;
		}else if(!septimaListo)
		{
			siete = seleccionada;
			septima.sprite = imagen;
			septimaListo = true;
		}
		if(ocho == 0)
		{
			octava.sprite = blanco;
		}else if(!octavaListo)
		{
			ocho = seleccionada;
			octava.sprite = imagen;
			octavaListo = true;
		}
		if(nueve == 0)
		{
			novena.sprite = blanco;
		}else if(!novenaListo)
		{
			nueve = seleccionada;
			novena.sprite = imagen;
			novenaListo = true;
		}
		if(diez == 0)
		{
			decima.sprite = blanco;
		}else if(!decimaListo)
		{
			diez = seleccionada;
			decima.sprite = imagen;
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
