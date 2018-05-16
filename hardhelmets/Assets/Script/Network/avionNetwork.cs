using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class avionNetwork : MonoBehaviour {

	public GameObject hero;
	public GameObject hero2;

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
		if(hero2 == null)
		{
			hero2 = GameObject.Find("Hero2");
		}
	}

	public void vibrar()
	{
		hero.GetComponent<HeroNetwork>().SniperCam.GetComponent<CamNetwork>().shakeAvion = true;
		hero.GetComponent<HeroNetwork>().SniperCam.GetComponent<CamNetwork>().vib = 0.7f;

		hero2.GetComponent<HeroNetwork>().SniperCam.GetComponent<CamNetwork>().shakeAvion = true;
		hero2.GetComponent<HeroNetwork>().SniperCam.GetComponent<CamNetwork>().vib = 0.7f;
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
