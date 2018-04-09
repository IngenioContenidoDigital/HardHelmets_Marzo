using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class faceCamera : MonoBehaviour {

	public Quaternion rotation;

	// Use this for initialization
	void Start ()
	{
		transform.parent = null;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//var target = Quaternion.LookRotation(Camera.main.transform.position - transform.position);
		//transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 10);

		//transform.LookAt(Camera.main.transform);

		transform.rotation = rotation;
	}
}
