using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.EventSystems;
using Spine.Unity;

public class Cajas : MonoBehaviour {

	public GameObject Cofre;

	public int abiertas;
	public GameObject otra;

	public int deb1;
	public int deb2;
	public int deb3;

	bool listo1;
	bool listo2;
	bool listo3;

	public GameObject card1;
	public GameObject card2;
	public GameObject card3;

	//CANTIDAD DE CARTAS
	int carta1;
	int carta2;
	int carta3;

	//DE QUE A QUE CARTA PUEDE DESTAPAR
	public int minima;
	public int maxima;//1,2,7,8,9,11,15

	int cajas;
	public UnityEngine.UI.Text cajasT;

	bool esconder;

	// Use this for initialization
	void Start ()
	{
		abiertas = 0;

		deb1 = Random.Range(minima,maxima);
		deb2 = Random.Range(minima,maxima);
		deb3 = Random.Range(minima,maxima);

		listo1 = true;

		cajas = PlayerPrefs.GetInt("caja1");
	}
	
	// Update is called once per frame
	void Update ()
	{
		cajasT.text = cajas.ToString();
		PlayerPrefs.SetInt("caja1", cajas);

		if(listo1)
		{
			print("TODOS DEBERIAN SER DIFERENTES");

			card1.GetComponent<skinCarta>().skinsToCombine[0] = deb1.ToString();
			card2.GetComponent<skinCarta>().skinsToCombine[0] = deb2.ToString();
			card3.GetComponent<skinCarta>().skinsToCombine[0] = deb3.ToString();

			PlayerPrefs.SetInt("card"+deb1, 1);
			PlayerPrefs.SetInt("card"+deb2, 1);
			PlayerPrefs.SetInt("card"+deb3, 1);

			//CANTIDAD DE CARTAS
			carta1 = PlayerPrefs.GetInt("card"+deb1+"cantidad");
			PlayerPrefs.SetInt("card"+deb1+"cantidad", carta1+1);
			carta2 = PlayerPrefs.GetInt("card"+deb2+"cantidad");
			PlayerPrefs.SetInt("card"+deb2+"cantidad", carta2+1);
			carta3 = PlayerPrefs.GetInt("card"+deb3+"cantidad");
			PlayerPrefs.SetInt("card"+deb3+"cantidad", carta3+1);

			listo1 = false;
		}

		if(abiertas >= 3 && cajas <= 0 && !esconder)
		{
			//otra.SetActive(true);
			StartCoroutine(cerrar());
			esconder = true;
		}
		if(abiertas >= 3 && cajas >= 1 && !esconder)
		{
			StartCoroutine(cerrarNuevo());
			esconder = true;
		}
	}
	public GameObject menu;
	IEnumerator cerrar()
	{
		yield return new WaitForSeconds(2);

		card1.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "salida", false);
		card2.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "salida", false);
		card3.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "salida", false);

		abiertas = 0;
		esconder = false;

		Cofre.GetComponent<Animator>().SetBool("cerrar", true);

		menu.GetComponent<Animator>().SetBool("sale", false);
		menu.GetComponent<Animator>().SetBool("entra", true);
	}
	IEnumerator cerrarNuevo()
	{
		yield return new WaitForSeconds(2);

		card1.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "salida", false);
		card2.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "salida", false);
		card3.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "salida", false);
		Cofre.GetComponent<Animator>().SetBool("cerrar", true);

		menu.GetComponent<Animator>().SetBool("sale", false);
		StartCoroutine(abreNuevo());
	}
	IEnumerator abreNuevo()
	{
		yield return new WaitForSeconds(2);

		abiertas = 0;
		esconder = false;

		Cofre.GetComponent<Animator>().SetBool("reiniciar", true);
	}

	public void Reopen()
	{
		if(cajas >= 1)
		{
			//otra.SetActive(false);
			cajas -= 1;
			loading.nombre = "cajas";
			Application.LoadLevel("Load");
		}
	}

	/*public void salir ()
	{
		CamMenu.segundo = true;
		Application.LoadLevel("Menu");
	}*/
}
