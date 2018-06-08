using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class barraOflline : MonoBehaviour {

	public GameObject Player;

	public int level;

	public float carga;

	public float fill;

	// Use this for initialization
	void Start ()
	{
		level = PlayerPrefs.GetInt("PlayerLevel");
		carga = 0.0002f+(float)level/25000;

		fill = 0;
		GetComponent<Image>().fillAmount = 0;
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		if(Player.GetComponent<Hero>().ready && Player.GetComponent<Hero>().salud > 0)
		{
			fill += carga;
			GetComponent<Image>().fillAmount = fill;
		}

		if(fill >= 1)
		{
			fill = 1;
		}
		if(fill <= 0)
		{
			fill = 0;
		}
	}
}
