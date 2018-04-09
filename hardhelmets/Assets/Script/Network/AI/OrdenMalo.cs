using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OrdenMalo : NetworkBehaviour {

	public Animator animator;

	public bool preview;
	public GameObject circulo;
	public GameObject objeto;
	public GameObject cursor;

	bool pos;
	Vector3 lugar;

	public GameObject Hero;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update ()
	{
		if(isServer)
		{
			return;
		}

		if(objeto == null)
		{
			objeto = GameObject.Find("Piso");
		}

		if(Hero == null)
		{
			Hero = GameObject.Find("Hero");
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
		}

		if(Input.GetMouseButtonDown(1))
		{
			if(pos)
			{
				Hero.GetComponent<HeroNetwork>().muneco = gameObject;
				Hero.GetComponent<HeroNetwork>().ordenLugar = lugar;
				Hero.GetComponent<HeroNetwork>().orden = true;
				
				preview = false;

			}else if(Physics.Raycast(ray, out hit))
			{
				if(hit.collider == gameObject.GetComponent<Collider>())
				{
					preview = true;
					Hero.GetComponent<HeroNetwork>().muneco = null;
					Hero.GetComponent<HeroNetwork>().orden = false;
				}
			}
		}
	}
}