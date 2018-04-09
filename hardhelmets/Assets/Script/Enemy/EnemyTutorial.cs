using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTutorial : MonoBehaviour {

	public Animator animator;

	public bool vivo;

	public float salud;

	public GameObject mira;

	public bool crearCarta;

	public GameObject[] sangreCuchillo;
	public GameObject[] efectoSanre;

	public GameObject textos;

	public bool explocion;
	public GameObject Huesos;

	public static int colision;
	public static int matado;
	public GameObject Dialogos;
	public GameObject Dialogos2;
	public GameObject siguiente;

	public GameObject Hero;

	public GameObject pedazos;

	public GameObject limite;

	public ParticleSystem humo;

	// Use this for initialization
	void Start ()
	{
		mira.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("targetHit"))
		{
			animator.SetBool("cascado", false);
		}
		Hero = GameObject.Find("Hero");
		if(vivo)
		{
			if(salud <= 0)
			{
				if(explocion)
				{
					animator.SetBool("muerte", true);
					var bones = (GameObject)Instantiate(Huesos, transform.position, transform.rotation);
					explocion = false;
				}else
				{
					animator.SetBool("muerte", true);
				}
				matado += 1;

				vivo = false;
			}else
			{
				explocion = false;
			}
		}else
		{
			if(matado >= maximo)
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

				Destroy(limite);

				siguiente.SetActive(true);
			}
			pedazos.SetActive(true);
			mira.SetActive(false);
			Destroy(mira);
			Destroy(gameObject);
		}
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

				if(PlayerPrefs.GetInt("violencia") == 1)
				{
					var sangre2 = (GameObject)Instantiate(sangreCuchillo[Random.Range(0,sangreCuchillo.Length)], transform.position, transform.rotation); 
				}
				salud -= 15;

				var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
				letras.GetComponent<TextMesh>().text = "15";
			}
			if(col.gameObject.tag == "bala" && vivo)
			{
				GetComponent<Rigidbody>().velocity = Vector3.zero;

				animator.SetBool("cascado", true);

				salud -= col.gameObject.GetComponent<balaOffline>().poder;

				var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
				letras.GetComponent<TextMesh>().text = col.gameObject.GetComponent<balaOffline>().poder.ToString("F0");

				if(PlayerPrefs.GetInt("violencia") == 1)
				{
					var explo = (GameObject)Instantiate(efectoSanre[Random.Range(0,efectoSanre.Length)], new Vector3(col.gameObject.transform.position.x,col.gameObject.transform.position.y-3, col.gameObject.transform.position.z-1), transform.rotation);
				}
				humo.Play();
			}
		}else
		{
			if(col.gameObject.tag == "bala" && vivo)
			{
				GetComponent<Rigidbody>().velocity = Vector3.zero;

				animator.SetBool("cascado", true);

				var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
				letras.GetComponent<TextMesh>().text = "USE HAND GRANADE";

				humo.Play();
			}
		}

		if(col.gameObject.tag == "explo" && vivo)
		{
			GetComponent<Rigidbody>().velocity = Vector3.zero;

			animator.SetBool("cascado", true);

			/*animator.SetBool("granada", true);
			animator.SetInteger("cascado", 10);*/

			salud -= col.gameObject.GetComponent<ExploOffline>().poder;

			var letras = (GameObject)Instantiate(textos, transform.position, Quaternion.Euler(0,0,0));
			letras.GetComponent<TextMesh>().text = col.gameObject.GetComponent<ExploOffline>().poder.ToString("F0");

			if(PlayerPrefs.GetInt("violencia") == 1)
			{
				explocion = true;
			}
			humo.Play();
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
