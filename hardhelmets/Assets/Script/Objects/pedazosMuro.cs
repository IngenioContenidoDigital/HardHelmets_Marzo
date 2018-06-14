using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pedazosMuro : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-10,10),Random.Range(0,10),Random.Range(-10,10));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/*void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "Piso" || col.gameObject.tag == "Matar")
		{
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
	}*/
}
