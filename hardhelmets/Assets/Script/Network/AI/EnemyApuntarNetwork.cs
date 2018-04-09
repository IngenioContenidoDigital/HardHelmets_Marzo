using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyApuntarNetwork : MonoBehaviour {

	public Animator animator;

	public Transform Player;
	public Vector3 nextPosition;

	public GameObject objeto;

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		if(GetComponent<AINetwork>())
		{
			if(objeto.GetComponent<AINetwork>().pelea)
			{
				Player = objeto.GetComponent<AINetwork>().Player;
			}else
			{
				Player = null;
			}
		}
		if(GetComponent<AIMedico>())
		{
			if(objeto.GetComponent<AIMedico>().pelea)
			{
				Player = objeto.GetComponent<AIMedico>().Player;
			}else
			{
				Player = null;
			}
		}


		if(Player != null)
		{
			if(objeto.transform.localScale.x == 1)//objeto.GetComponent<AINetwork>().voltear 
			{
				nextPosition = new Vector3(objeto.transform.position.x+18.19f, Player.transform.position.y+5, transform.position.z);
			}else
			{
				nextPosition = new Vector3(objeto.transform.position.x-18.19f, Player.transform.position.y+5, transform.position.z);
			}
			transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * 10);

		}else
		{
			if(objeto.transform.localScale.x == 1)//objeto.GetComponent<AINetwork>().voltear 
			{
				nextPosition = new Vector3(objeto.transform.position.x+18.19f, objeto.transform.position.y+4.1f, transform.position.z);
			}else
			{
				nextPosition = new Vector3(objeto.transform.position.x-18.19f, objeto.transform.position.y+4.1f, transform.position.z);
			}
			transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * 10);
		}
	}
}
