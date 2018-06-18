using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunitySounds : MonoBehaviour {

	public Animator animator;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("CommunityEsconder"))
		{
			animator.SetBool("sale", false);
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("CommunityMostrar"))
		{
			animator.SetBool("entra", false);
		}
	}

	public void carta()
	{
		GetComponent<AudioSource>().Play();
	}
}
