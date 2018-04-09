using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
//using UnityEngine.UI;

public class Logo : MonoBehaviour {

	public GameObject video;
	public UnityEngine.UI.Image negro;
	byte alp = 255;
	bool entrada = true;
	bool salida;

	// Use this for initialization
	void Start ()
	{
		Cursor.visible = false;

		if(PlayerPrefs.GetInt("Primera") == 0)
		{
			//POR PRIMERA VEZ DESBLOQUEA TODO
			PlayerPrefs.SetFloat("voice",1);
			PlayerPrefs.SetFloat("efects",1);
			PlayerPrefs.SetFloat("musica",1);
			PlayerPrefs.SetInt("violencia",1);
			//DESBLOQUEA TODAS LAS CARTAS
			PlayerPrefs.SetInt("card1", 0);
			PlayerPrefs.SetInt("card2", 0);
			PlayerPrefs.SetInt("card3", 0);
			PlayerPrefs.SetInt("card4", 0);
			PlayerPrefs.SetInt("card5", 0);
			PlayerPrefs.SetInt("card6", 0);
			PlayerPrefs.SetInt("card7", 0);
			PlayerPrefs.SetInt("card8", 0);
			PlayerPrefs.SetInt("card9", 0);
			PlayerPrefs.SetInt("card10", 0);
			PlayerPrefs.SetInt("card11", 0);
			PlayerPrefs.SetInt("card12", 0);
			PlayerPrefs.SetInt("card13", 0);
			PlayerPrefs.SetInt("card14", 0);
			PlayerPrefs.SetInt("card15", 0);
			PlayerPrefs.SetInt("card16", 0);
			PlayerPrefs.SetInt("card17", 0);
			PlayerPrefs.SetInt("card18", 0);
			PlayerPrefs.SetInt("card19", 0);
			PlayerPrefs.SetInt("card20", 0);
			//CANTIDAD DE CAJAS de cartas
			PlayerPrefs.SetInt("caja1",0);
			PlayerPrefs.SetInt("caja2",0);
			PlayerPrefs.SetInt("caja3",0);
			//SELECCIONA UNA BARAJA BASICA
			PlayerPrefs.SetInt("Mano1", 0);
			PlayerPrefs.SetInt("Mano2", 0);
			PlayerPrefs.SetInt("Mano3", 0);
			PlayerPrefs.SetInt("Mano4", 0);
			PlayerPrefs.SetInt("Mano5", 0);
			PlayerPrefs.SetInt("Mano6", 0);
			PlayerPrefs.SetInt("Mano7", 0);
			PlayerPrefs.SetInt("Mano8", 0);
			PlayerPrefs.SetInt("Mano9", 0);
			PlayerPrefs.SetInt("Mano10", 0);
			//CANTIDAD DE CARTAS EN LA MANO
			PlayerPrefs.SetInt("Mano1cantidad", 0);
			PlayerPrefs.SetInt("Mano2cantidad", 0);
			PlayerPrefs.SetInt("Mano3cantidad", 0);
			PlayerPrefs.SetInt("Mano4cantidad", 0);
			PlayerPrefs.SetInt("Mano5cantidad", 0);
			PlayerPrefs.SetInt("Mano6cantidad", 0);
			PlayerPrefs.SetInt("Mano7cantidad", 0);
			PlayerPrefs.SetInt("Mano8cantidad", 0);
			PlayerPrefs.SetInt("Mano9cantidad", 0);
			PlayerPrefs.SetInt("Mano10cantidad", 0);
			//MONEDAS
			PlayerPrefs.SetInt("monedas",0);
			//VALOR DE VENTA DE CARTAS
			PlayerPrefs.SetInt("card1valor", 60);
			PlayerPrefs.SetInt("card2valor", 80);
			PlayerPrefs.SetInt("card3valor", 80);
			PlayerPrefs.SetInt("card4valor", 80);
			PlayerPrefs.SetInt("card5valor", 100);
			PlayerPrefs.SetInt("card6valor", 100);
			PlayerPrefs.SetInt("card7valor", 60);
			PlayerPrefs.SetInt("card8valor", 50);
			PlayerPrefs.SetInt("card9valor", 60);
			PlayerPrefs.SetInt("card10valor", 60);
			PlayerPrefs.SetInt("card11valor", 60);
			PlayerPrefs.SetInt("card12valor", 120);
			PlayerPrefs.SetInt("card13valor", 90);
			PlayerPrefs.SetInt("card14valor", 90);
			PlayerPrefs.SetInt("card15valor", 90);
			PlayerPrefs.SetInt("card16valor", 120);
			PlayerPrefs.SetInt("card17valor", 120);
			PlayerPrefs.SetInt("card18valor", 150);
			//CANTIDAD DE CADA CARTA
			PlayerPrefs.SetInt("card1cantidad", 0);
			PlayerPrefs.SetInt("card2cantidad", 0);
			PlayerPrefs.SetInt("card3cantidad", 0);
			PlayerPrefs.SetInt("card4cantidad", 0);
			PlayerPrefs.SetInt("card5cantidad", 0);
			PlayerPrefs.SetInt("card6cantidad", 0);
			PlayerPrefs.SetInt("card7cantidad", 0);
			PlayerPrefs.SetInt("card8cantidad", 0);
			PlayerPrefs.SetInt("card9cantidad", 0);
			PlayerPrefs.SetInt("card10cantidad", 0);
			PlayerPrefs.SetInt("card11cantidad", 0);
			PlayerPrefs.SetInt("card12cantidad", 0);
			PlayerPrefs.SetInt("card13cantidad", 0);
			PlayerPrefs.SetInt("card14cantidad", 0);
			PlayerPrefs.SetInt("card15cantidad", 0);
			PlayerPrefs.SetInt("card16cantidad", 0);
			PlayerPrefs.SetInt("card17cantidad", 0);
			PlayerPrefs.SetInt("card18cantidad", 0);
			//CANTIDAD DE CARTAS APLICADAS A LA MANO
			PlayerPrefs.SetInt("card1cantidadUsadas", 0);
			PlayerPrefs.SetInt("card2cantidadUsadas", 0);
			PlayerPrefs.SetInt("card3cantidadUsadas", 0);
			PlayerPrefs.SetInt("card4cantidadUsadas", 0);
			PlayerPrefs.SetInt("card5cantidadUsadas", 0);
			PlayerPrefs.SetInt("card6cantidadUsadas", 0);
			PlayerPrefs.SetInt("card7cantidadUsadas", 0);
			PlayerPrefs.SetInt("card8cantidadUsadas", 0);
			PlayerPrefs.SetInt("card9cantidadUsadas", 0);
			PlayerPrefs.SetInt("card10cantidadUsadas", 0);
			PlayerPrefs.SetInt("card11cantidadUsadas", 0);
			PlayerPrefs.SetInt("card12cantidadUsadas", 0);
			PlayerPrefs.SetInt("card13cantidadUsadas", 0);
			PlayerPrefs.SetInt("card14cantidadUsadas", 0);
			PlayerPrefs.SetInt("card15cantidadUsadas", 0);
			PlayerPrefs.SetInt("card16cantidadUsadas", 0);
			PlayerPrefs.SetInt("card17cantidadUsadas", 0);
			PlayerPrefs.SetInt("card18cantidadUsadas", 0);
			//SELECCIONA EL SKIN BASICO
			PlayerPrefs.SetString("casco","Casco1");
			PlayerPrefs.SetString("cara","Cara1");
			//DEJA DE SER LA PRIMERA VEZ
			PlayerPrefs.SetInt("Primera",1);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(SceneManager.GetActiveScene().name == "Logo" && !video.GetComponent<VideoPlayer>().isPlaying)
		{
			salida = true;
			//StartCoroutine(espera());
			if(salida)
			{
				alp += 5;
				negro.color = new Color32(0,0,0,alp);
				if(alp >= 255)
				{
					Application.LoadLevel("Mensaje");
				}
			}
		}else
		{
			if(entrada)
			{
				alp -= 5;
				negro.color = new Color32(0,0,0,alp);
				if(alp <= 0)
				{
					entrada = false;
					StartCoroutine(espera2());
				}
			}
		
			if(salida)
			{
				alp += 5;
				negro.color = new Color32(0,0,0,alp);
				if(alp >= 255)
				{
					Application.LoadLevel("Menu");
				}
			}
		}
	}

	IEnumerator espera()
	{
		yield return new WaitForSeconds(10);
		Application.LoadLevel("Mensaje");
	}

	IEnumerator espera2()
	{
		yield return new WaitForSeconds(10);
		salida = true;
	}
}
