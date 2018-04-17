using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RematchNetwork : MonoBehaviour {

	public GameObject Player;

	public EventSystem eventsystem;

	public GameObject selectedObj;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (eventsystem.GetComponent<EventSystem>().currentSelectedGameObject == null)
		{
			eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(selectedObj);
			//EventSystem.current.SetSelectedGameObject(selectedObj);
		}

		selectedObj = eventsystem.GetComponent<EventSystem>().currentSelectedGameObject;

		//CAMBIE ENTRE CONTROL Y TECLADO
		if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
		{
			eventsystem.GetComponent<StandaloneInputModule>().horizontalAxis = "Horizontal";
			eventsystem.GetComponent<StandaloneInputModule>().verticalAxis = "Vertical";
		}

		if(Input.GetButtonDown("HorizontalUI") || Input.GetButtonDown("VerticalUI"))
		{
			eventsystem.GetComponent<StandaloneInputModule>().horizontalAxis = "HorizontalUI";
			eventsystem.GetComponent<StandaloneInputModule>().verticalAxis = "VerticalUI";
		}
	}
}
