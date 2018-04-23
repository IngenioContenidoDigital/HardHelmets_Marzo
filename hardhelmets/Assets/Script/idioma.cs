using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class idioma : MonoBehaviour {

	public string lengua;
	public EventSystem eventsystem;

	[Header("ESPAÑOL")]
	public GameObject Español;
	[Header("CHINO")]
	public GameObject Chino;


	void Update ()
	{
		lengua = PlayerPrefs.GetString("idioma");

		if(eventsystem.GetComponent<EventSystem>().currentSelectedGameObject == gameObject)
		{
			if(lengua == "SPANISH")
			{
				Español.SetActive(true);

				Chino.SetActive(false);
			}
			if(lengua == "CHINESE")
			{
				Chino.SetActive(true);

				Español.SetActive(false);
			}
		}else
		{
			Español.SetActive(false);
			Chino.SetActive(false);
		}
	}
}
