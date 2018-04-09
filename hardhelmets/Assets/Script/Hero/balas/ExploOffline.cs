using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploOffline : MonoBehaviour {

	int random;
	public AudioClip explo1;
	public AudioClip explo2;

	int explo;

	float efectos;

	public float poder;

	// Use this for initialization
	void Start ()
	{
		efectos = PlayerPrefs.GetFloat("efects");

		explo = Random.Range(1,3);
		GetComponent<Animator>().SetInteger("explo",explo);

		random = Random.Range(1,3);
		if(random == 1)
		{
			GetComponent<AudioSource>().volume = efectos;
			GetComponent<AudioSource>().clip = explo1;
			GetComponent<AudioSource>().Play();
		}
		if(random == 2)
		{
			GetComponent<AudioSource>().volume = efectos;
			GetComponent<AudioSource>().clip = explo2;
			GetComponent<AudioSource>().Play();
		}
		Cam.visible = true;
		StartCoroutine(tiempo());
	}

	IEnumerator tiempo()
	{
		yield return new WaitForSeconds(0.2f);
		Cam.visible = false;
	}
}
