using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minaAntipersona : MonoBehaviour {

	public Animator animator;

	public GameObject mina;

	public float poder;

	public GameObject explocion;
	public GameObject explocion2;
	//ONDA
	public GameObject prefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "Player")
		{
			animator.GetComponent<Animator>().SetBool("explo", true);
			GetComponent<AudioSource>().Play();
		}

		if(col.gameObject.tag == "enemy")
		{
			animator.GetComponent<Animator>().SetBool("explo", true);
			GetComponent<AudioSource>().Play();
		}

		if(col.gameObject.tag == "explo")
		{
			Explo2();
		}
	}

	public void Explo()
	{
		explocion.SetActive(true);//var explo = (GameObject)Instantiate(explocion, mina.transform.position, Quaternion.identity);
		explocion.transform.parent = null;
		var onda = (GameObject)Instantiate(prefab, mina.transform.position, Quaternion.identity);

		/*if(explo.GetComponent<Explo>())
		{
			explo.GetComponent<Explo>().poder = poder;
		}else
		{
			explo.GetComponent<ExploOffline>().poder = poder;
		}*/

		Destroy(gameObject);
	}

	public void Explo2()
	{
		var explo = (GameObject)Instantiate(explocion2, transform.position, Quaternion.identity);
		var onda = (GameObject)Instantiate(prefab, transform.position, Quaternion.identity);

		if(explo.GetComponent<Explo>())
		{
			explo.GetComponent<Explo>().poder = poder;
		}else
		{
			explo.GetComponent<ExploOffline>().poder = poder;
		}

		Destroy(gameObject);
	}
}
