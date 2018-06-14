using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puaz : MonoBehaviour {

	public GameObject parte1;
	public GameObject textos;
	public GameObject polvo;
	public GameObject explocion;

	public float sangre;

	// Use this for initialization
	void Start ()
	{
		sangre = 300;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(sangre <= 0)
		{
			Destroy(gameObject);
			parte1.SetActive(true);
			explocion.SetActive(true);
		}
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "bala")
		{
			sangre -= col.gameObject.GetComponent<balaOffline>().poder;

			var partic = (GameObject)Instantiate(polvo, col.gameObject.transform.position, Quaternion.Euler(0,0,0));

			var letras = (GameObject)Instantiate(textos, col.gameObject.transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = col.gameObject.GetComponent<balaOffline>().poder.ToString("F0");
		}
		if(col.gameObject.tag == "explo")
		{
			sangre -= col.gameObject.GetComponent<ExploOffline>().poder;

			var partic = (GameObject)Instantiate(polvo, col.gameObject.transform.position, Quaternion.Euler(0,0,0));

			var letras = (GameObject)Instantiate(textos, col.gameObject.transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = col.gameObject.GetComponent<ExploOffline>().poder.ToString("F0");
		}

		if(col.gameObject.tag == "Matar")
		{
			sangre -= 5000;
		}

		if(col.gameObject.tag == "tank" || col.gameObject.tag == "enemyTank")
		{
			sangre -= 5000;
		}
	}
	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "balaLlamas")
		{
			//sangre -= 1;
		}
	}
}
