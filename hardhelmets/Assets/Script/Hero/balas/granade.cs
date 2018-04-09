using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class granade : MonoBehaviour {

	public GameObject explocion;

	public Light luz;
	int total;
	bool mas;
	bool menos;

	bool contar;
	int tiempo;

	//ONDA
	public GameObject prefab;

	public AudioClip sonido;

	float efectos;

	public float poder;

	void Start ()
	{
		efectos = PlayerPrefs.GetFloat("efects");

		GetComponent<Rigidbody>().velocity = transform.up * 20;
		GetComponent<Rigidbody>().AddForce(transform.right * 50);
	}

	void Update ()
	{
		if(contar)
		{
			tiempo++;
			if(total <= 0)
			{
				menos = false;
				mas = true;
			}
			if(total >= 50)
			{
				mas = false;
				menos = true;
			}
			if(mas)
			{
				total += 5;
			}
			if(menos)
			{
				total -= 5;
			}
		}
		if(tiempo >= 100)
		{
			Explo();
		}
		luz.intensity = total;
	}

	void OnCollisionEnter (Collision col)
	{
		contar = true;
		if(col.gameObject.tag == "explo")
		{
			Explo();
		}
		if(col.gameObject.tag == "Piso")
		{
			GetComponent<AudioSource>().volume = efectos;
			GetComponent<AudioSource>().clip = sonido;
			GetComponent<AudioSource>().Play();
		}
		//var explo = (GameObject)Instantiate(explocion, transform.position, Quaternion.identity); 
		//Destroy(explo, 2.0f);
		//Destroy(gameObject);
	}
	public void Explo()
	{
		var explo = (GameObject)Instantiate(explocion, transform.position, Quaternion.identity);
		var onda = (GameObject)Instantiate(prefab, transform.position, Quaternion.identity);
		explo.GetComponent<ExploOffline>().poder = poder;
		Destroy(gameObject);
	}
}
