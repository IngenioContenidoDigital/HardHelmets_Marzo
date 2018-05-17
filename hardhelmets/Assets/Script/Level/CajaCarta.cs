using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class CajaCarta : MonoBehaviour {

	public GameObject Cofre;

	public GameObject carta;
	public bool seleccionada;

	void Update ()
	{
		if(Input.GetButtonDown("Jump") || Input.GetButtonDown("Submit"))
		{
			if(!seleccionada)
			{
				carta.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "destapar", false);
				if(Cofre.GetComponent<Cofre>())
				{
					Cofre.GetComponent<Cofre>().abiertas += 1;
				}
				if(Cofre.GetComponent<cofreLobby>())
				{
					Cofre.GetComponent<cofreLobby>().abiertas += 1;
				}
			}
			seleccionada = true;
		}
	}

	public void Carta ()
	{
		if(!seleccionada)
		{
			carta.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "destapar", false);
			if(Cofre.GetComponent<Cofre>())
			{
				Cofre.GetComponent<Cofre>().abiertas += 1;
			}
			if(Cofre.GetComponent<cofreLobby>())
			{
				Cofre.GetComponent<cofreLobby>().abiertas += 1;
			}
		}
		seleccionada = true;
	}

	public void Over ()
	{
		if(!seleccionada)
		{
			carta.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "mousecaja", false);
		}
	}
	public void Exit ()
	{
		if(!seleccionada)
		{
			carta.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "normalcaja", false);
		}
	}
}
