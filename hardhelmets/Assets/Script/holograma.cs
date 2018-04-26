using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holograma : MonoBehaviour {

	public float ye;//-27.92f
	public float zeta;//-181.7f

	public GameObject hologram;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.name == "Hero")
		{
			Instantiate(hologram, new Vector3(col.transform.position.x, ye, zeta), Quaternion.Euler(0,0,0));
		}
	}
}
