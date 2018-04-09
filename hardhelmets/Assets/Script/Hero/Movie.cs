using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movie : MonoBehaviour {

	public Animator animator;

	public bool caminar;
	public bool saltar;
	public bool caer;
	public bool callendo;
	public bool piso;
	public int descanso;
	public int disparo;
	public int recarga;
	public bool recargando;
	public bool pistola;
	public bool cuchillo;
	public bool cuchillo2;
	public bool cambio;
	public int muerte;
	public int cascado;
	public bool muerto;
	public bool cubrirse;
	public bool cubierto;
	public bool saltoAtras;
	public bool saltoAdelante;


	void Update ()
	{
		if(caminar)
		{
			animator.SetBool("walk",true);
		}else
		{
			animator.SetBool("walk",false);
		}
		if(saltar)
		{
			animator.SetBool("jump",true);
		}else
		{
			animator.SetBool("jump",false);
		}
		if(caer)
		{
			animator.SetBool("falling",true);
		}else
		{
			animator.SetBool("falling",false);
		}
		if(piso)
		{
			animator.SetBool("grounded",true);
		}else
		{
			animator.SetBool("grounded",false);
		}
	}
}
