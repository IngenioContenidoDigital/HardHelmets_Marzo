using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyApuntar : MonoBehaviour {

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
		if(objeto.GetComponent<AI>().pelea)
		{
			Player = objeto.GetComponent<AI>().Player;
		}else
		{
			Player = null;
		}

		if(Player != null)
		{
			nextPosition = new Vector3(transform.position.x, Player.transform.position.y+5, transform.position.z);
			transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * 10);
		}else
		{
			nextPosition = new Vector3(transform.position.x, objeto.transform.position.y+4.1f, transform.position.z);
			transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * 10);
		}
	}
}
