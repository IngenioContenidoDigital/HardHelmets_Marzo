using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Covertura : MonoBehaviour {

	public GameObject actionButon;
	public GameObject covertura;

	public bool listo;

	void Start ()
	{
		
	}

	void Update () 
	{
		if(listo)
		{
			if(Input.GetButtonDown("USAR"))
			{
				actionButon.SetActive(false);
			}
			if(Input.GetButtonDown("DISPARO") || Input.GetButtonDown("left") || Input.GetButtonDown("right") || Input.GetButtonDown("up") || Input.GetButtonDown("down") || Input.GetButtonDown("GRANADA"))
			{
				actionButon.SetActive(true);
			}
		}
	}

	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "Player")
		{
			listo = true;
			actionButon.SetActive(true);
			actionButon.transform.parent = null;
		}
	}
	void OnTriggerExit (Collider col)
	{
		if(col.gameObject.tag == "Player")
		{
			listo = false;
			actionButon.SetActive(false);
			actionButon.transform.parent = covertura.transform;
		}
	}
}
