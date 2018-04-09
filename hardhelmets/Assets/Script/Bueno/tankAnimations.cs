using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

public class tankAnimations : MonoBehaviour {

	public GameObject Heroe;

	public GameObject polv;

	public Animator animator;

	public GameObject explo1;
	public GameObject explo2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Heroe == null)
		{
			Heroe = GameObject.Find("Hero");
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("TankShoot"))
		{
			animator.SetBool("disparo", false);
			animator.SetBool("frenando", false);
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("accel") || animator.GetCurrentAnimatorStateInfo(0).IsName("accelBack"))
		{
			animator.SetBool("walk", false);
			animator.SetBool("walking", true);
			animator.SetBool("frenando", false);
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("drive"))
		{
			animator.SetBool("walking", true);
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("muerto"))
		{
			animator.SetBool("muerto", false);
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("frenada"))
		{
			animator.SetBool("frenando", true);
		}
	}

	void polvo ()
	{
		var efect = (GameObject)Instantiate(polv, transform.position, transform.rotation);
	}

	public void explotion1 ()
	{
		explo1.SetActive(true);
	}

	public void explotion2 ()
	{
		explo2.SetActive(true);
	}

	public void vivracion ()
	{
		if(Vector3.Distance(transform.position, Heroe.transform.position) <= 50)
		{
			ProCamera2DShake.Instance.Shake();
		}
	}
}
