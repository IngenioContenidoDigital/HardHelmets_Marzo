using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Collector : MonoBehaviour {

	public GameObject Todo;
	public GameObject desbloqueadas;
	public GameObject bloquedas;

	public EventSystem eventsystem;

	public GameObject selectedObj;

	public GameObject inicio;

	// Use this for initialization
	void Start ()
	{
		inicio.GetComponent<Button>().onClick.Invoke();
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
	}

	public void All()
	{
		desbloqueadas.SetActive(false);
		bloquedas.SetActive(false);
		Todo.SetActive(true);
	}
	public void Unlocked()
	{
		bloquedas.SetActive(false);
		Todo.SetActive(false);
		desbloqueadas.SetActive(true);
	}
	public void Locked()
	{
		desbloqueadas.SetActive(false);
		Todo.SetActive(false);
		bloquedas.SetActive(true);
	}
}
