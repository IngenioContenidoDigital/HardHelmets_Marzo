using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PreAlpha : MonoBehaviour {

	DateTime currentDay;
	public int mes;
	public int dia;
	public int hora;
	public int minutos;
	public int segundos;

	public string restante;


	public UnityEngine.UI.Text Fecha;

	static string Url="https://www.google.com";
	public bool coneccion;

	// Use this for initialization
	void Start () 
	{
		
	}

	// Update is called once per frame
	void Update ()
	{
		currentDay = DateTime.Now;

		mes = 12-Convert.ToInt32(currentDay.Month);
		dia = Convert.ToInt32(currentDay.Day);
		hora = Convert.ToInt32(currentDay.Hour);
		minutos = Convert.ToInt32(currentDay.Minute);
		segundos = Convert.ToInt32(currentDay.Second);

		Fecha.text = mes+" : "+dia+" : "+hora+" : "+minutos+" : "+segundos;

		if(Application.internetReachability == NetworkReachability.NotReachable)
		{
			print("error");
			coneccion = false;
		}else
		{
			print("conectado");
			coneccion = true;
		}

		if(currentDay.Year == 2017 && currentDay.Month <= 12 && currentDay.Day <= 31 && coneccion)
		{
			print("ESTA DISPONIBLE JUGAR");
		}

		//getSecondsLeft(24,60,0);
	}

	/*public int getSecondsLeft(int hours,int minutes ,int seconds) 
	{
		//Create Desired time
		DateTime target = new DateTime(24,60,0,hours,minutes,seconds);

		//Get the current time
		DateTime now = System.DateTime.Now;

		//Convert both to seconds
		int targetSec = target.Hour*60*60 + target.Minute*60 + target.Second;
		int nowSec = now.Hour * 60 * 60 + now.Minute * 60 + now.Second;

		//Get the difference in seconds
		int diff = targetSec - nowSec;

		print(diff);
		return diff;
	}*/
}
