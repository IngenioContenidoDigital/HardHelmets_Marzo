using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTutorial : MonoBehaviour {

	public Animator animator;

	public bool vivo;

	public float salud;

	public GameObject mira;

	public bool crearCarta;

	public GameObject textos;

	public static int colision;
	public static int matado;
	public GameObject Dialogos;
	public GameObject Dialogos2;
	public GameObject siguiente;

	public GameObject Hero;

	public GameObject pedazos;

	public GameObject limite;

	public GameObject humo;

	public GameObject Base;

	// Use this for initialization
	void Start ()
	{
		mira.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("hit"))
		{
			animator.SetBool("cascado", false);
		}
		Hero = GameObject.Find("Hero");
		if(vivo)
		{
			if(salud <= 0)
			{
				matado += 1;

				vivo = false;
			}
		}else
		{
			LayerMask.NameToLayer("muerto");
			if(matado >= maximo && gameObject.name != "Espantapajaros")
			{
				Hero.GetComponent<Hero>().caminarA = false;
				Hero.GetComponent<Hero>().caminarU = false;
				Hero.GetComponent<Hero>().caminarD = false;
				Hero.GetComponent<Hero>().caminarI = false;

				Hero.GetComponent<Hero>().ready = false;

				if(gameObject.name == "Tutorial8")
				{
					Dialogos2.SetActive(true);
				}

				Hero.GetComponent<Hero>().SniperCam.GetComponent<Cam>().objetivo = false;

				Destroy(limite);

				siguiente.SetActive(true);
			}
			if(Base != null)
			{
				Base.layer = LayerMask.NameToLayer("mira");
			}
			pedazos.SetActive(true);
			mira.SetActive(false);
			StartCoroutine(matar());
		}
	}
	IEnumerator matar()
	{
		yield return new WaitForSeconds(0.3f);
		Destroy(gameObject);
	}
	public int maximo;

	void OnCollisionEnter (Collision col)
	{
		if(gameObject.name != "Tutorial8")
		{
			if(col.gameObject.tag == "cuchillo" && vivo)
			{
				GetComponent<Rigidbody>().velocity = Vector3.zero;

				animator.SetBool("cascado", true);

				salud -= 15;

				var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
				letras.GetComponent<TextMesh>().text = "15";

				var partic = (GameObject)Instantiate(humo, col.gameObject.transform.position, Quaternion.Euler(0,0,0));
			}
			if(col.gameObject.tag == "bala" && vivo)
			{
				GetComponent<Rigidbody>().velocity = Vector3.zero;

				animator.SetBool("cascado", true);

				salud -= col.gameObject.GetComponent<balaOffline>().poder;

				var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
				letras.GetComponent<TextMesh>().text = col.gameObject.GetComponent<balaOffline>().poder.ToString("F0");

				var partic = (GameObject)Instantiate(humo, col.gameObject.transform.position, Quaternion.Euler(0,0,0));
			}
		}else
		{
			if(col.gameObject.tag == "bala" && vivo)
			{
				GetComponent<Rigidbody>().velocity = Vector3.zero;

				animator.SetBool("cascado", true);

				var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
				letras.GetComponent<TextMesh>().text = "USE HAND GRANADE";

				var partic = (GameObject)Instantiate(humo, col.gameObject.transform.position, Quaternion.Euler(0,0,0));
			}
		}

		if(col.gameObject.tag == "explo" && vivo)
		{
			GetComponent<Rigidbody>().velocity = Vector3.zero;

			animator.SetBool("cascado", true);

			/*animator.SetBool("granada", true);
			animator.SetInteger("cascado", 10);*/

			salud -= col.gameObject.GetComponent<ExploOffline>().poder*3;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = col.gameObject.GetComponent<ExploOffline>().poder.ToString("F0");

			var partic = (GameObject)Instantiate(humo, col.gameObject.transform.position, Quaternion.Euler(0,0,0));
		}
	}


	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "mira" && vivo)
		{
			if(colision == 0)
			{
				StartCoroutine(espera());
			}
			colision += 1;
			mira.SetActive(true);
			mira.GetComponent<Animator>().SetBool("entry",true);
		}
	}
	void OnTriggerExit (Collider col)
	{
		if(col.gameObject.tag == "mira" && vivo)
		{
			mira.SetActive(false);
		}
	}

	IEnumerator espera()
	{
		yield return new WaitForSeconds(0.4f);

		Hero.GetComponent<Hero>().caminarA = false;
		Hero.GetComponent<Hero>().caminarU = false;
		Hero.GetComponent<Hero>().caminarD = false;
		Hero.GetComponent<Hero>().caminarI = false;

		Hero.GetComponent<Hero>().ready = false;

		Dialogos.SetActive(true);
	}
}
