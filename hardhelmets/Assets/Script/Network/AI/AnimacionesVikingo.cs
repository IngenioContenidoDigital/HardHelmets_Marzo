using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionesVikingo : MonoBehaviour {

	public Animator animator;

	public GameObject polv;
	public GameObject gota;

	public Transform PasoD;
	public Transform PasoI;
	public GameObject pasopolvo;
	public GameObject pasopolvo2;

	public GameObject strikearma;
	public Transform strikeSpawn;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(GetComponent<AIVikingo>())
		{
			if(!animator.GetCurrentAnimatorStateInfo(0).IsName("dispara") && !animator.GetCurrentAnimatorStateInfo(0).IsName("casca") && GetComponent<AIVikingo>().disparando)
			{
				GetComponent<AIVikingo>().disparando = false;
				animator.SetBool("girar", false);
			}
		}
		if(GetComponent<AIVikingoNetwork>())
		{
			if(!animator.GetCurrentAnimatorStateInfo(0).IsName("dispara") && !animator.GetCurrentAnimatorStateInfo(0).IsName("casca") && GetComponent<AIVikingoNetwork>().disparando)
			{
				GetComponent<AIVikingoNetwork>().disparando = false;
				animator.SetBool("girar", false);
			}
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
		{
			animator.SetBool("paracaidas", false);
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("giro") || animator.GetCurrentAnimatorStateInfo(0).IsName("giro2"))
		{
			animator.SetBool("girar", false);
		}

		if(animator.GetCurrentAnimatorStateInfo(0).IsName("dispara"))
		{
			animator.SetBool("disparando", true);
			animator.SetBool("cascado", false);
			animator.SetBool("disparar", false);
		}else
		{
			animator.SetBool("disparando", false);
		}
		if(animator.GetCurrentAnimatorStateInfo(1).IsName("golpeado") || animator.GetCurrentAnimatorStateInfo(0).IsName("vuela"))
		{
			animator.SetBool("cascado", false);
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("golpe"))
		{
			animator.SetBool("golpe", false);
			animator.SetBool("caminar", false);
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("cae"))
		{
			animator.SetBool("muerto2", true);
			animator.SetBool("muerto", false);
		}
		//PARA EL LLAMERO
		if(GetComponent<AIVikingo>() && GetComponent<AIVikingo>().Tipo == 2 || GetComponent<AIVikingoNetwork>() && GetComponent<AIVikingoNetwork>().Tipo == 2)
		{
			if(animator.GetCurrentAnimatorStateInfo(1).IsName("golpeado2"))
			{
				animator.SetBool("cascado", false);
			}
			if(animator.GetCurrentAnimatorStateInfo(0).IsName("giro") || animator.GetCurrentAnimatorStateInfo(0).IsName("giro2"))
			{
				animator.SetBool("girar", false);
			}
			if(animator.GetCurrentAnimatorStateInfo(0).IsName("shoot"))
			{
				if(GetComponent<AIVikingo>())
				{
					GetComponent<AIVikingo>().agent.isStopped = true;
				}else
				{
					GetComponent<AIVikingoNetwork>().agent.isStopped = true;
				}
				animator.SetBool("girar", false);
				animator.SetBool("walk", false);
				animator.SetBool("shot", false);
				animator.SetBool("disparando", true);
			}else
			{
				if(GetComponent<AIVikingo>())
				{
					GetComponent<AIVikingo>().particulas.Stop();
				}else
				{
					GetComponent<AIVikingoNetwork>().particulas.Stop();
				}
				animator.SetBool("disparando", false);
			}
			if(animator.GetCurrentAnimatorStateInfo(0).IsName("muerto"))
			{
				animator.SetBool("murio", true);
			}
			if(animator.GetCurrentAnimatorStateInfo(0).IsName("casca"))
			{
				animator.SetBool("golpe", false);
			}

			if(animator.GetCurrentAnimatorStateInfo(0).IsName("idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("walk"))
			{
				if(GetComponent<AIVikingo>())
				{
					GetComponent<AIVikingo>().particulas.Stop();
					GetComponent<AIVikingo>().disparafuego = false;
				}else
				{
					GetComponent<AIVikingoNetwork>().particulas.Stop();
					GetComponent<AIVikingoNetwork>().disparafuego = false;
				}
			}
		}
	}

	//EVENTO SPINE
	void polvo ()
	{
		if(GetComponent<AIVikingo>())
		{
			if(!GetComponent<AIVikingo>().water)
			{
				var efect = (GameObject)Instantiate(polv, transform.position, transform.rotation);
			}
		}else
		{
			if(!GetComponent<AIVikingoNetwork>().water)
			{
				var efect = (GameObject)Instantiate(polv, transform.position, transform.rotation);
			}
		}
	}

	void paso()
	{
		if(GetComponent<AIVikingo>())
		{
			if(!GetComponent<AIVikingo>().water)
			{
				var efect = (GameObject)Instantiate(pasopolvo, PasoD.transform.position, transform.rotation);
			}else
			{
				var efect = (GameObject)Instantiate(gota, transform.position, transform.rotation);
			}
		}else
		{
			if(!GetComponent<AIVikingoNetwork>().water)
			{
				var efect = (GameObject)Instantiate(pasopolvo, PasoD.transform.position, transform.rotation);
			}else
			{
				var efect = (GameObject)Instantiate(gota, transform.position, transform.rotation);
			}
		}
	}
	void paso2()
	{
		if(GetComponent<AIVikingo>())
		{
			if(!GetComponent<AIVikingo>().water)
			{
				var efect = (GameObject)Instantiate(pasopolvo2, PasoD.transform.position, transform.rotation);
			}else
			{
				var efect = (GameObject)Instantiate(gota, transform.position, transform.rotation);
			}
		}else
		{
			if(!GetComponent<AIVikingoNetwork>().water)
			{
				var efect = (GameObject)Instantiate(pasopolvo2, PasoD.transform.position, transform.rotation);
			}else
			{
				var efect = (GameObject)Instantiate(gota, transform.position, transform.rotation);
			}
		}
	}
	void rafaga ()
	{
		if(transform.localScale.x == 1.13f)
		{
			var efect = (GameObject)Instantiate(strikearma, strikeSpawn.transform.position, Quaternion.Euler(0,0,-90));
		}else
		{
			var efect = (GameObject)Instantiate(strikearma, strikeSpawn.transform.position, Quaternion.Euler(0,0,90));
		}
	}
}
