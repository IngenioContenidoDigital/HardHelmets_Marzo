using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseOffline : MonoBehaviour {

	public float sangre;
	public float saludMax = 2000;

	public GameObject textos;

	public GameObject luz;

	public GameObject fuego1;
	public GameObject fuego2;
	public GameObject fuego3;
	public GameObject fuego4;

	public GameObject destruida;

	public AudioSource audio1;

	public GameObject camera;

	void Start ()
	{
		sangre = saludMax;
	}

	void Update ()
	{
		if(sangre <= saludMax*70/100)
		{
			fuego1.SetActive(true);
		}
		if(sangre <= saludMax*50/100)
		{
			fuego2.SetActive(true);
		}
		if(sangre <= saludMax*30/100)
		{
			fuego3.SetActive(true);
		}
		if(sangre <= saludMax*10/100)
		{
			fuego4.SetActive(true);
		}

		if(sangre <= 0 && !matada)
		{
			matada = true;
			StartCoroutine(momentito());
		}
	}

	bool matada;

	IEnumerator momentito()
	{
		yield return new WaitForSeconds(1f);
		GetComponent<Animator>().SetBool("muere", true);
		audio1.Play();
		destruida.SetActive(true);
	}

	public void vivracion()
	{
		camera.GetComponent<Cam>().shake = true;
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "bala")
		{
			sangre -= 2;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = "2";
		}
		if(col.gameObject.tag == "balaFusil")
		{
			sangre -= 3;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = "3";
		}
		if(col.gameObject.tag == "balaEscopeta")
		{
			sangre -= 2;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = "2";
		}
		if(col.gameObject.tag == "balaSubmetra")
		{
			sangre -= 2;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = "2";
		}
		if(col.gameObject.tag == "balaMetra")
		{
			sangre -= 3;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = "3";
		}
		if(col.gameObject.tag == "balaMG")
		{
			sangre -= 3;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = "3";
		}
		if(col.gameObject.tag == "balaSniper")
		{
			sangre -= 1;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = "1";
		}
		if(col.gameObject.tag == "explo")
		{
			sangre -= 50;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = "50";
		}
	}
}
