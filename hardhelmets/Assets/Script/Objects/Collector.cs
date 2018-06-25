using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour {

	public GameObject Todo;
	public GameObject desbloqueadas;
	public GameObject bloquedas;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
