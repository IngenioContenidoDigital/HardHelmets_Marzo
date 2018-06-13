using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseOffline : MonoBehaviour {

	public float sangre;
	public float saludMax = 2000;

	public GameObject textos;

	public GameObject fuego1;
	public GameObject fuego2;
	public GameObject fuego3;
	public GameObject fuego4;
	public GameObject fuego5;

	public GameObject destruida;

	//public AudioSource audio1;

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
		if(sangre <= saludMax*20/100)
		{
			fuego4.SetActive(true);
		}
		if(sangre <= saludMax*10/100)
		{
			if(fuego5 != null)
			{
				fuego5.SetActive(true);
			}
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
		//GetComponent<Animator>().SetBool("muere", true);
		//audio1.Play();
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
			sangre -= col.gameObject.GetComponent<balaOffline>().poder;

			var letras = (GameObject)Instantiate(textos, col.gameObject.transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = col.gameObject.GetComponent<balaOffline>().poder.ToString("F0");
		}
		if(col.gameObject.tag == "explo")
		{
			sangre -= col.gameObject.GetComponent<ExploOffline>().poder;

			var letras = (GameObject)Instantiate(textos, col.gameObject.transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = col.gameObject.GetComponent<ExploOffline>().poder.ToString("F0");
		}
	}
	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "balaLlamas")
		{
			sangre -= col.gameObject.GetComponent<balaFuego>().poder;

			var letras = (GameObject)Instantiate(textos, col.gameObject.transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = col.gameObject.GetComponent<balaFuego>().poder.ToString("F0");
		}
	}
}
