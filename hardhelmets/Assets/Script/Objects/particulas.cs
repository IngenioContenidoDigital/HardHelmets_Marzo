using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particulas : MonoBehaviour {

	float escala;
	// Use this for initialization
	void Start ()
	{
		StartCoroutine(esperar2());

		escala = Random.Range(0.07f,0.1f);
		transform.localScale = new Vector3(escala,escala,escala);

		GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1,1),Random.Range(0,5),Random.Range(-5,5));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "Piso")
		{
			/*var efect = (GameObject)Instantiate(polv, transform.position, transform.rotation);

			audio1.volume = efectos;
			audio1.clip = madera[Random.Range(0,madera.Length)];
			audio1.Play();*/

			StartCoroutine(esperar());
		}
	}

	IEnumerator esperar ()
	{
		yield return new WaitForSeconds(5);
		Destroy(gameObject);
	}

	IEnumerator esperar2 ()
	{
		yield return new WaitForSeconds(15);
		Destroy(gameObject);
	}
}
