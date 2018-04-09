using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pedazos : MonoBehaviour {

	public GameObject polv;

	public AudioClip[] madera;

	//AUDIO PLAY
	public AudioSource audio1;

	//VOLUMEN
	float efectos;

	// Use this for initialization
	void Start ()
	{
		efectos = PlayerPrefs.GetFloat("efects");

		GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-10,10),Random.Range(0,10),Random.Range(-10,10));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "Piso")
		{
			var efect = (GameObject)Instantiate(polv, transform.position, transform.rotation);

			audio1.volume = efectos;
			audio1.clip = madera[Random.Range(0,madera.Length)];
			audio1.Play();

			StartCoroutine(esperar());
		}
	}

	IEnumerator esperar ()
	{
		yield return new WaitForSeconds(1.5f);
		GetComponent<BoxCollider>().enabled = false;
		StartCoroutine(esperar2());
	}

	IEnumerator esperar2 ()
	{
		yield return new WaitForSeconds(2);
		Destroy(gameObject);
	}
}
