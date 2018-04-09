using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cae : MonoBehaviour {

	public GameObject polv;

	public AudioClip[] sonido;

	//AUDIO PLAY
	public AudioSource audio1;

	float efectos;

	// Use this for initialization
	void Start ()
	{
		efectos = PlayerPrefs.GetFloat("efects");
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
			audio1.clip = sonido[Random.Range(0,sonido.Length)];
			audio1.Play();
		}
	}
}
