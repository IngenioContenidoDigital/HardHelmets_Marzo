using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dinero : MonoBehaviour {

	//CREDITOS DISPONIBLE
	public static int Coins;
	public static int Diamonds;
	public UnityEngine.UI.Text coins;
	public UnityEngine.UI.Text diamonds;

	// Use this for initialization
	void Start ()
	{
		//PlayerPrefs.SetInt("Coins",10);
		//PlayerPrefs.SetInt("Diamonds",1000);
		if(PlayerPrefs.GetString("casco") == "")
		{
			PlayerPrefs.SetString("casco", "casco1");
			PlayerPrefs.SetString("cuerpo", "cuerpo1");
			PlayerPrefs.SetString("uniforme", "uniforme1");
			PlayerPrefs.SetString("cuello", "cuello1");
			PlayerPrefs.SetString("cinturon", "cinturon1");
			PlayerPrefs.SetString("chaleco", "chaleco1");
			PlayerPrefs.SetString("maleta", "maleta1");
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		Coins = PlayerPrefs.GetInt("Coins");
		Diamonds = PlayerPrefs.GetInt("Diamonds");
		//CREDITOS DISPONIBLE
		coins.text = Coins.ToString();
		diamonds.text =  Diamonds.ToString();
	}
}
