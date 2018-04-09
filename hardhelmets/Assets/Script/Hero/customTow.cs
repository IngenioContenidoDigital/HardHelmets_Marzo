using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customTow : MonoBehaviour {

	public string cara = "default";
	public string casco = "default";
	public string mascara = "default";
	public string uniforme = "default";
	public string cuello = "default";
	public string cinturon = "default";
	public string chaleco = "default";
	public string maleta = "default";

	public string carab;
	public string cascob;
	public string mascarab;
	public string uniformeb;
	public string cuellob;
	//public string cinturonb;
	public string chalecob;
	public string maletab;

	/*public int carac;
	public int cascoc;
	public int mascarac;
	public int uniformec;
	public int cuelloc;
	//public string cinturonb;
	public int chalecoc;
	public int maletac;*/


	// Use this for initialization
	void Start ()
	{
		cara = PlayerPrefs.GetString("cara");
		casco = PlayerPrefs.GetString("casco");
		mascara = PlayerPrefs.GetString("mascara");
		cuello = PlayerPrefs.GetString("cuello");
		uniforme = PlayerPrefs.GetString("uniforme");
		chaleco = PlayerPrefs.GetString("chaleco");
		maleta = PlayerPrefs.GetString("maleta");

		carab = cara.Substring(cara.Length -1);
		cascob = casco.Substring(casco.Length -1);
		mascarab = mascara.Substring(mascara.Length -1);
		uniformeb = uniforme.Substring(uniforme.Length -1);
		cuellob = cuello.Substring(cuello.Length -1);
		chalecob = chaleco.Substring(chaleco.Length -1);
		maletab = maleta.Substring(maleta.Length -1);

		/*carac = int.Parse(carab);
		cascoc = int.Parse(cascob);
		mascarac = int.Parse(mascarab);
		uniformec = int.Parse(uniformeb);
		cuelloc = int.Parse(cuellob);
		chalecoc = int.Parse(chalecob);
		maletac = int.Parse(maletab);*/
	}
	
	// Update is called once per frame
	void Update ()
	{
		PlayerPrefs.SetString("cara", cara);
		PlayerPrefs.SetString("casco", casco);
		PlayerPrefs.SetString("mascara", mascara);
		PlayerPrefs.SetString("cuello", cuello);
		PlayerPrefs.SetString("uniforme", uniforme);
		PlayerPrefs.SetString("chaleco", chaleco);
		PlayerPrefs.SetString("maleta", maleta);

		if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("characterIn"))
		{
			GetComponent<Animator>().SetBool("entra", false);
		}
		if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("characterOut"))
		{
			GetComponent<Animator>().SetBool("sale", false);
		}
	}
}
