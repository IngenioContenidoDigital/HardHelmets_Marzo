using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puaz : MonoBehaviour {

	public GameObject destruir;
	public GameObject parte1;
	public GameObject polvo;

	public int ajuste;

	public int sangre;

	// Use this for initialization
	void Start ()
	{
		sangre = 100;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(sangre <= 0)
		{
			Destroy(destruir);
			var part = (GameObject)Instantiate(parte1, destruir.transform.position, destruir.transform.rotation); //new Vector3(transform.position.x,transform.position.y+ajuste,transform.position.z), Quaternion.Euler(0,90,0)
			var efect = (GameObject)Instantiate(polvo, transform.position, Quaternion.Euler(0,0,0));
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "bala" || col.gameObject.tag == "balaEscopeta" || col.gameObject.tag == "balaFusil"|| col.gameObject.tag == "balaMetra"|| col.gameObject.tag == "balaSubmetra")
		{
			sangre -= 5;
		}
		if(col.gameObject.tag == "balaSniper")
		{
			sangre -= 10;
		}
		if(col.gameObject.tag == "explo")
		{
			sangre -= 100;
		}

		if(col.gameObject.tag == "tank" || col.gameObject.tag == "enemyTank")
		{
			Destroy(destruir);
			//PARTE1
			var part = (GameObject)Instantiate(parte1, destruir.transform.position, destruir.transform.rotation); //new Vector3(transform.position.x,transform.position.y+ajuste,transform.position.z), Quaternion.Euler(0,90,0)
			var efect = (GameObject)Instantiate(polvo, transform.position, Quaternion.Euler(0,0,0));
			Destroy(gameObject);
		}
	}
	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "balaLlamas")
		{
			sangre -= 1;
		}
	}
}
