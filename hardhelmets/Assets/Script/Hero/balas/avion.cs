using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class avion : MonoBehaviour {

	public GameObject hero;

	public float poder;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(hero == null)
		{
			hero = GameObject.Find("Hero");
		}
	}

	public void vibrar()
	{
		hero.GetComponent<Hero>().SniperCam.GetComponent<Cam>().shakeAvion = true;
		hero.GetComponent<Hero>().SniperCam.GetComponent<Cam>().vib = 0.7f;
	}

	public void destruir ()
	{
		StartCoroutine(borrar());
	}

	IEnumerator borrar()
	{
		yield return new WaitForSeconds(5);
		Destroy(gameObject);
	}
}
