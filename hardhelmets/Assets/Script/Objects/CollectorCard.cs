using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class CollectorCard : MonoBehaviour {

	public GameObject grafica;

	public GameObject nacer;
	public GameObject Crear;

	public bool destruir;
	public bool crear;

	//------------

	public bool desb;
	public bool bloq;
	public int card;

	// Use this for initialization
	void Start ()
	{
		PlayerPrefs.SetInt("card1", 0);
		card = int.Parse(gameObject.name);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(desb)
		{
			if(PlayerPrefs.GetInt("card"+card) == 0)
			{
				Destroy(gameObject);
			}
		}
		if(bloq)
		{
			if(PlayerPrefs.GetInt("card"+card) >= 1)
			{
				Destroy(gameObject);
			}
		}

		if(destruir)
		{
			destruir = false;

			foreach(Transform child in nacer.transform)
			{
				Destroy(child.gameObject);
			}
			crear = true;
		}
		if(crear && nacer.transform.childCount <= 0)
		{
			crear = false;

			var objeto = (GameObject)Instantiate(Crear, nacer.transform.position, Quaternion.Euler(0,0,0)); 
			objeto.transform.parent = nacer.transform;
			objeto.GetComponent<RectTransform>().localScale = new Vector3(0.4f, 0.4f, 0.4f);
		}
	}


	public void entra()
	{
		
	}


	public void sale()
	{
		
	}


	public void click()
	{
		destruir = true;

		grafica.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "arrastre", false);

		StartCoroutine(EspCarta());
	}
	IEnumerator EspCarta()
	{
		yield return new WaitForSpineAnimationComplete(grafica.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0));
		grafica.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "normal", false);
	}
}
