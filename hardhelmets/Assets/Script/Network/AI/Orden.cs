using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Orden : NetworkBehaviour {

	public int tipo;

	public Animator animator;

	public bool preview;
	public GameObject circulo;
	public GameObject objeto;
	public GameObject cursor;

	bool pos;
	public Vector3 lugar;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!isServer)
		{
			return;
		}

		if(objeto == null)
		{
			objeto = GameObject.Find("Piso");
		}

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if(preview)
		{
			circulo.SetActive(true);
			if(objeto.GetComponent<Collider>().Raycast (ray, out hit, Mathf.Infinity))
			{
				cursor.SetActive(true);
				cursor.transform.position = new Vector3(hit.point.x,hit.point.y+1,hit.point.z);//hit.point;
				lugar = hit.point;
			}
			pos = true;
		}else
		{
			circulo.SetActive(false);
			cursor.SetActive(false);
			pos = false;
			//caminar = true;
		}

		if(Input.GetMouseButtonDown(1))
		{
			if(pos)
			{
				if(tipo == 1)
				{
					CaminarMetra();
				}else
				{
					CaminarMortero();
				}
				preview = false;

			}else if(Physics.Raycast(ray, out hit))
			{
				if(hit.collider == gameObject.GetComponent<Collider>())
				{
					preview = true;
				}
			}
		}
	}
		
	void CaminarMetra()
	{
		print("DEBERIA CAMINAR METRALLETO");
		animator.SetBool("caminar", true);

		GetComponent<AIMetraNetwork>().caminar = true;
		GetComponent<AIMetraNetwork>().lugar = lugar;
	}
		
	void CaminarMortero()
	{
		print("DEBERIA CAMINAR MORTERERO");
		animator.SetBool("caminar", true);

		GetComponent<AIMorteroNetwork>().caminar = true;
		GetComponent<AIMorteroNetwork>().lugar = lugar;
	}
}
