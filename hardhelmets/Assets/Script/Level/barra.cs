using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine;

public class barra : MonoBehaviour {

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
	void Update ()
	{
		if(Player.GetComponent<HeroNetwork>().ready && Player.GetComponent<HeroNetwork>().salud > 0)
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
