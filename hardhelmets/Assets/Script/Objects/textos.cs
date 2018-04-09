using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textos : MonoBehaviour {

	int random;

	public GameObject padre;

	// Use this for initialization
	void Start ()
	{
		GetComponent<TextMesh>().text = padre.GetComponent<TextMesh>().text;

		random = Random.Range(1,4);
		GetComponent<Animator>().SetInteger("inicio", random);
		//transform.parent = null;
	}
	
	public void destruir()
	{
		Destroy(padre);
	}
}
