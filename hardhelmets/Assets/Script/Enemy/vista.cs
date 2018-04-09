using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vista : MonoBehaviour {

	public bool arriba;
	public bool abajo;

	float tiempo;

	int random;

	public bool activado;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(arriba)
		{
			
		}
		if(abajo)
		{
			
		}

		if(random == 1)
		{
			arriba = true;
			random = 0;
		}if(random == 2)
		{
			abajo = true;
			random = 0;
		}
	}

	void OnTriggerEnter (Collider col)
	{
		/*if(col.gameObject.tag == "enemyTank" || col.gameObject.tag == "tank")//col.gameObject.tag == "enemy" || col.gameObject.tag == "Player" || 
		{
			random = Random.Range(1,3);
			//arriba = false;
			//abajo = true;
		}*/
		if(col.gameObject.tag == "pared")
		{
			arriba = false;
			abajo = true;
		}
		if(col.gameObject.tag == "Frente")
		{
			abajo = false;
			arriba = true;
		}
		if(col.gameObject.tag == "obstaculo1")
		{
			arriba = true;
			abajo = false;
		}
		if(col.gameObject.tag == "obstaculo2")
		{
			abajo = true;
			arriba = false;
		}
		if(col.gameObject.name == "PuazD")
		{
			arriba = true;
			abajo = false;
		}
	}

	void OnTriggerExit (Collider col)
	{
		if(col.gameObject.tag == "pared")
		{
			tiempo = Random.Range(0.2f, 1.5f);
			StartCoroutine(espera());
			//abajo = false;
			//arriba = false;
		}
		if(col.gameObject.tag == "Frente")
		{
			tiempo = Random.Range(0.5f, 2.0f);
			StartCoroutine(espera());
			//abajo = false;
			//arriba = false;
		}
		if(col.gameObject.tag == "obstaculo1")
		{
			tiempo = Random.Range(0.5f, 2.0f);
			StartCoroutine(espera());
			//abajo = false;
			//arriba = false;
		}
		if(col.gameObject.tag == "obstaculo2")
		{
			tiempo = Random.Range(0.5f, 2.0f);
			StartCoroutine(espera());
			//abajo = false;
			//arriba = false;
		}
		if(col.gameObject.name == "PuazD")
		{
			tiempo = Random.Range(0.5f, 2.0f);
			StartCoroutine(espera());
			//abajo = false;
			//arriba = false;
		}
	}

	IEnumerator espera ()
	{
		yield return new WaitForSeconds (tiempo);
		abajo = false;
		arriba = false;
	}
}
