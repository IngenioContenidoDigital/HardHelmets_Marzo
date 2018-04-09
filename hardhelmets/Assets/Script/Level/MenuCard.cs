using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCard : MonoBehaviour {

	//VOLUMEN
	float efectos;

	void Start ()
	{
		efectos = PlayerPrefs.GetFloat("efects");
	}


	void Update()
	{
		if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("cardStay") || GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("cardExit"))
		{
			GetComponent<Animator>().SetBool("sale",false);
		}
	}

	public void Entra()
	{
		if(GetComponent<Image>().sprite.name != "menucartasSp_3")
		{
			GetComponent<AudioSource>().volume = efectos;
			GetComponent<AudioSource>().Play();

			GetComponent<Animator>().SetBool("sale",false);
			GetComponent<Animator>().SetBool("entra",true);
		}
	}

	public void Sale()
	{
		if(GetComponent<Image>().sprite.name != "menucartasSp_3")
		{
			GetComponent<AudioSource>().volume = efectos;
			GetComponent<AudioSource>().Play();

			GetComponent<Animator>().SetBool("sale",true);
			GetComponent<Animator>().SetBool("entra",false);
		}
	}
}
