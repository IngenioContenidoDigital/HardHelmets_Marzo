using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploOffline : MonoBehaviour {

	public AudioClip[] explo;

	//int explo;

	float efectos;

	public float poder;

	// Use this for initialization
	void Start ()
	{
		efectos = PlayerPrefs.GetFloat("efects");

		//explo = Random.Range(1,3);
		//GetComponent<Animator>().SetInteger("explo",explo);

		GetComponent<AudioSource>().clip = explo[Random.Range(0,explo.Length)];
		GetComponent<AudioSource>().Play();

		Cam.visible = true;
		StartCoroutine(tiempo());
	}

	IEnumerator tiempo()
	{
		yield return new WaitForSeconds(0.2f);
		Cam.visible = false;
	}
}
