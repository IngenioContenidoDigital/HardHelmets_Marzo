using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BotonSelected : MonoBehaviour {

	public EventSystem eventsystem;
	// Use this for initialization
	void Start ()
	{
		eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
