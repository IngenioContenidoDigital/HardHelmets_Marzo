using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsMG : MonoBehaviour {

	public Animator animator;

	string _currentDirection = "right";

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

	public GameObject strike;
	public GameObject strikearma;
	public GameObject strikeescopeta;
	public GameObject strikegun;
	public Transform strikeSpawn;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("paracaidasS"))
		{
			animator.SetBool("paracaidas", true);
		}else
		{
			animator.SetBool("paracaidas", false);
		}
		//QUIETO AL AZAR
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("MGNormal"))
		{
			animator.SetBool("walking", false);
			animator.SetBool("paracaidas", false);
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("walk"))
		{
			animator.SetBool("walking", true);
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("paracaidasS"))
		{
			animator.SetBool("paracaidas", true);
		}
		//MUERTE
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("KillSimple") || animator.GetCurrentAnimatorStateInfo(0).IsName("KillSimple2") || animator.GetCurrentAnimatorStateInfo(0).IsName("KillBackJump")
			|| animator.GetCurrentAnimatorStateInfo(0).IsName("KillEX") || animator.GetCurrentAnimatorStateInfo(0).IsName("KillEX2") || animator.GetCurrentAnimatorStateInfo(0).IsName("KillQuemado"))
		{
			animator.SetBool("muerto", true);
			animator.SetInteger("muerte", 0);
			gameObject.layer = LayerMask.NameToLayer("muerto");
			//gameObject.tag = "Untagged";
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
			animator.SetBool("muerto", true);
			animator.SetBool("acuchillado", false);
			animator.SetInteger("muerte", 0);
		}
		//CASCADO
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("Hit") || animator.GetCurrentAnimatorStateInfo(0).IsName("HitKnife")|| animator.GetCurrentAnimatorStateInfo(0).IsName("MGHit"))
		{
			animator.SetBool("walk", false);
			animator.SetInteger("cascado", 0);
		}
		if(inwater)
		{
			Ondass.activas = true;
			Ondas.SetActive(true);
		}else
		{
			Ondass.activas = false;
			Ondas.SetActive(false);
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
		if(!inwater)
		{
			var efect = (GameObject)Instantiate(polv, transform.position, transform.rotation);
			Destroy(efect, 1.0f);
		}
	}
	void paso()
	{
		if(!inwater)
		{
			var efect = (GameObject)Instantiate(pasopolvo, PasoD.transform.position, PasoD.transform.rotation);
		}else
		{
			var efect = (GameObject)Instantiate(splash, transform.position, transform.rotation);
		}
	}
	void paso2()
	{
		if(!inwater)
		{
			var efect = (GameObject)Instantiate(pasopolvo2, PasoI.transform.position, PasoI.transform.rotation);
		}else
		{
			var efect = (GameObject)Instantiate(splash, transform.position, transform.rotation);
		}
	}
	void rafaga ()
	{
		if(transform.localScale.x == 1)
		{
			var efect = (GameObject)Instantiate(strike, strikeSpawn.transform.position, transform.rotation);
		}else
		{
			var efect = (GameObject)Instantiate(strike, strikeSpawn.transform.position, Quaternion.Euler(0,0,strikeSpawn.transform.rotation.z-180));
		}
	}
	void rafagaarma ()
	{
		if(transform.localScale.x == 1)
		{
			var efect = (GameObject)Instantiate(strikearma, strikeSpawn.transform.position, strikeSpawn.rotation);
		}else
		{
			var efect = (GameObject)Instantiate(strikearma, strikeSpawn.transform.position, Quaternion.Euler(0,0,strikeSpawn.transform.rotation.z-180));
		}
	}
	void rafagaarmametra ()
	{
		if(transform.localScale.x == 1)
		{
			var efect = (GameObject)Instantiate(strikearma, strikeSpawn.transform.position, Quaternion.Euler(0,0,strikeSpawn.transform.rotation.z));
		}else
		{
			var efect = (GameObject)Instantiate(strikearma, strikeSpawn.transform.position, Quaternion.Euler(0,0,strikeSpawn.transform.rotation.z-180));
		}
		//var efect = (GameObject)Instantiate(strikearma, strikeSpawn.transform.position, strikeSpawn.rotation);
	}
	void rafagagun ()
	{
		if(transform.localScale.x == 1)
		{
			var efect = (GameObject)Instantiate(strikegun, strikeSpawn.transform.position,strikeSpawn.rotation);
		}else
		{
			var efect = (GameObject)Instantiate(strikegun, strikeSpawn.transform.position, Quaternion.Euler(0,0,strikeSpawn.transform.rotation.z-180));
		}
	}
	void rafagaescopeta ()
	{
		if(transform.localScale.x == 1)
		{
			var efect = (GameObject)Instantiate(strikeescopeta, strikeSpawn.transform.position, strikeSpawn.rotation);
		}else
		{
			var efect = (GameObject)Instantiate(strikeescopeta, strikeSpawn.transform.position, Quaternion.Euler(0,0,strikeSpawn.transform.rotation.z-180));
		}
	}
}
