using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ondass : MonoBehaviour {

	public Transform Player;

	public static bool activas;
	public static float posOndas;
	public static Vector3 nextPosition;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		nextPosition = new Vector3(Player.transform.position.x,posOndas,Player.transform.position.z-0.24f);
		if(activas)
		{
			transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * 50);
		}
	}
}
