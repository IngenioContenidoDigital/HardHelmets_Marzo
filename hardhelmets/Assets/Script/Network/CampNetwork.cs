using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CampNetwork : NetworkBehaviour {

	public Animator animator;

	[SyncVar]
	public float vida = 100;

	public string NameEnemyTank;

	public GameObject mira;
	public GameObject polv;
	public GameObject textos;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!isServer)
		{
			return;
		}

		if(vida <= 0)
		{
			gameObject.layer = LayerMask.NameToLayer("muerto");
			animator.SetBool("muere", true);
		}

		if(animator.GetCurrentAnimatorStateInfo(0).IsName("golpe"))
		{
			animator.SetBool("hit", false);
		}
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "cuchillo")
		{
			animator.SetBool("hit", true);
			vida -= 5;

			var letras = (GameObject)Instantiate(textos, new Vector3(transform.position.x, transform.position.y+9,transform.position.z), Quaternion.Euler(0,0,0));
			letras.transform.parent = transform;
			letras.GetComponent<TextMesh>().text = "5";
		}
		if(col.gameObject.tag == "bala")
		{
			animator.SetBool("hit", true);
			vida -= col.gameObject.GetComponent<bala>().poder;

			var letras = (GameObject)Instantiate(textos, new Vector3(transform.position.x, transform.position.y+9,transform.position.z), Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = col.gameObject.GetComponent<balaOffline>().poder.ToString("F0");
		}
		if(col.gameObject.tag == "explo")
		{
			animator.SetBool("hit", true);
			vida -= col.gameObject.GetComponent<Explo>().poder;

			var letras = (GameObject)Instantiate(textos, new Vector3(transform.position.x, transform.position.y+9,transform.position.z), Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = col.gameObject.GetComponent<ExploOffline>().poder.ToString("F0");
		}
		if(col.gameObject.tag == NameEnemyTank)
		{
			animator.SetBool("hit", true);
			vida -= 100;

			var letras = (GameObject)Instantiate(textos, new Vector3(transform.position.x, transform.position.y+9,transform.position.z), Quaternion.Euler(0,0,0));
			letras.transform.parent = transform;
			letras.GetComponent<TextMesh>().text = "100";
		}
	}
	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "mira")
		{
			mira.SetActive(true);
			mira.GetComponent<Animator>().SetBool("entry",true);
		}

		if(col.gameObject.tag == "balaLlamas")
		{
			vida -= 5;

			var letras = (GameObject)Instantiate(textos, new Vector3(transform.position.x, transform.position.y+9,transform.position.z), Quaternion.Euler(0,0,0));
			letras.transform.parent = transform;
			letras.GetComponent<TextMesh>().text = "5";
		}
	}
	void OnTriggerExit (Collider col)
	{
		if(col.gameObject.tag == "mira")
		{
			mira.SetActive(false);
		}
	}
	//EVENTO ANIMACION
	public void polvo ()
	{
		var efect = (GameObject)Instantiate(polv, transform.position, transform.rotation);
	}
}
