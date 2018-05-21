using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanzerAnimations : MonoBehaviour {

	public Animator animator;

	//EFECTOS
	public GameObject sangre;
	public GameObject polv;
	//----AGUA
	public GameObject splash;
	bool inwater;
	public GameObject Ondas;

	public Transform PasoD;
	public Transform PasoI;
	public GameObject pasopolvo;
	public GameObject pasopolvo2;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update ()
	{
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("panzerpose"))
		{
			animator.SetBool("normal", false);
		}

		if(animator.GetCurrentAnimatorStateInfo(0).IsName("jump2"))
		{
			animator.SetBool("falling", false);
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("panzerwalk"))
		{
			animator.SetBool("walk", false);
			animator.SetBool("walking", true);
		}
		//MUERTE
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("KillSimple") || animator.GetCurrentAnimatorStateInfo(0).IsName("KillSimple2") || animator.GetCurrentAnimatorStateInfo(0).IsName("KillBackJump")
			|| animator.GetCurrentAnimatorStateInfo(0).IsName("KillEX") || animator.GetCurrentAnimatorStateInfo(0).IsName("KillEX2") || animator.GetCurrentAnimatorStateInfo(0).IsName("KillQuemado"))
		{
			animator.SetBool("muerto", true);
			animator.SetInteger("muerte", 0);
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("KillSimple3"))
		{
			animator.SetBool("muerto", true);
			animator.SetBool("headShot", false);
			gameObject.layer = LayerMask.NameToLayer("muerto");
			//gameObject.tag = "Untagged";
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("KillKnife") || animator.GetCurrentAnimatorStateInfo(0).IsName("KillKnife2"))
		{
			animator.SetBool("acuchillado", false);
			animator.SetInteger("muerte", 0);
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("KillJump"))
		{
			animator.SetInteger("muerte", 0);
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
		{
			animator.SetInteger("cascado", 0);
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("panzerShot"))
		{
			animator.SetBool("disparo", false);
		}
	}

	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "Water")
		{
			if(GetComponent<Rigidbody>().velocity.y <= -4f)
			{
				var efect = (GameObject)Instantiate(splash, new Vector3(PasoD.transform.position.x, col.gameObject.transform.position.y+0.1f, PasoD.transform.position.z), Quaternion.Euler(90,0,0));
			}
			Ondass.posOndas = col.gameObject.transform.position.y;
			inwater = true;
		}
	}
	void OnTriggerExit (Collider col)
	{
		if(col.gameObject.tag == "Water")
		{
			if(GetComponent<Rigidbody>().velocity.y >= 4f)
			{
				var efect = (GameObject)Instantiate(splash, new Vector3(PasoD.transform.position.x, col.gameObject.transform.position.y+0.1f, PasoD.transform.position.z), Quaternion.Euler(90,0,0));
			}
			inwater = false;
		}
	}

	//EVENTOS SPINE
	void blood ()
	{
		if(PlayerPrefs.GetInt("violencia") == 1)
		{
			var efect = (GameObject)Instantiate(sangre, transform.position, transform.rotation);
		}
	}
	void muerto ()
	{
		Destroy(gameObject);
	}
	void polvo ()
	{
		if(!GetComponent<AI>().water )
		{
			var efect = (GameObject)Instantiate(polv, transform.position, transform.rotation);
			Destroy(efect, 1.0f);
		}
	}
	void paso()
	{
		if(!GetComponent<AI>().water)
		{
			var efect = (GameObject)Instantiate(pasopolvo, PasoD.transform.position, PasoD.transform.rotation);
		}else
		{
			var efect = (GameObject)Instantiate(splash, transform.position, transform.rotation);
		}
	}
	void paso2()
	{
		if(!GetComponent<AI>().water)
		{
			var efect = (GameObject)Instantiate(pasopolvo2, PasoI.transform.position, PasoI.transform.rotation);
		}else
		{
			var efect = (GameObject)Instantiate(splash, transform.position, transform.rotation);
		}
	}
}
