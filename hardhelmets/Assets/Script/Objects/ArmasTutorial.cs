using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using RedBlueGames.Tools.TextTyper;

public class ArmasTutorial : MonoBehaviour {

	public int vida = 50;

	bool vivo = true;

	public GameObject polv;

	public GameObject destruir;
	public GameObject textos;

	public AudioClip cae;
	public AudioClip coje;

	//AUDIO PLAY
	public AudioSource audio1;

	bool caer;

	//VOLUMEN
	float efectos;

	public GameObject Dialogos;

	public string buscar;

	public GameObject Hero;


	// Use this for initialization
	void Start ()
	{
		efectos = PlayerPrefs.GetFloat("efects");
	}

	// Update is called once per frame
	void Update ()
	{
		Dialogos = GameObject.Find(buscar);

		Hero = GameObject.Find("Hero");

		if(vivo)
		{
			if(vida <= 0)
			{
				gameObject.layer = LayerMask.NameToLayer("muerto");
				GetComponent<Animator>().SetBool("rompe",true);

				var partes = (GameObject)Instantiate(destruir, transform.position, transform.rotation); 

				StartCoroutine(espera());

				audio1.volume = efectos;
				audio1.clip = coje;
				audio1.Play();
				vivo = false;
			}
		}
	}

	//COLLISIONS
	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "Piso" || col.gameObject.tag == "Water" || col.gameObject.tag == "escopeta"
			|| col.gameObject.tag == "fusil" || col.gameObject.tag == "granade" || col.gameObject.tag == "escopeta"
			|| col.gameObject.tag == "lansallamas" || col.gameObject.tag == "metra" || col.gameObject.tag == "sniper"
			|| col.gameObject.tag == "submetra" || col.gameObject.tag == "suplies")
		{
			if(!caer)
			{
				GetComponent<Animator>().SetBool("cae",true);

				audio1.volume = efectos;
				audio1.clip = cae;
				audio1.Play();
				caer = true;
			}
		}
		if(col.gameObject.tag == "Player" || col.gameObject.tag == "enemy")
		{
			gameObject.layer = LayerMask.NameToLayer("muerto");
			GetComponent<Animator>().SetBool("rompe",true);

			var partes = (GameObject)Instantiate(destruir, transform.position, transform.rotation); 

			if(gameObject.tag == "fusil")
			{
				if(!Hero.GetComponent<CustomFinal>().arma.GetComponent<UnlokArma>().fusil)
				{
					col.gameObject.GetComponent<Hero>().caminarA = false;
					col.gameObject.GetComponent<Hero>().caminarU = false;
					col.gameObject.GetComponent<Hero>().caminarD = false;
					col.gameObject.GetComponent<Hero>().caminarI = false;

					col.gameObject.GetComponent<Hero>().eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(col.gameObject.GetComponent<Hero>().carta1);

					col.gameObject.GetComponent<Hero>().ready = false;

					Dialogos.SetActive(true);
					if(Dialogos.GetComponent<DialogosTutorial3>())
					{
						Dialogos.GetComponent<DialogosTutorial3>().enabled = true;
					}
					if(Dialogos.GetComponent<DialogosTutorial7>())
					{
						Dialogos.GetComponent<DialogosTutorial7>().enabled = true;
					}
				}
			}else
			{
				if(!Hero.GetComponent<CustomFinal>().arma.GetComponent<UnlokArma>().granada)
				{
					col.gameObject.GetComponent<Hero>().caminarA = false;
					col.gameObject.GetComponent<Hero>().caminarU = false;
					col.gameObject.GetComponent<Hero>().caminarD = false;
					col.gameObject.GetComponent<Hero>().caminarI = false;

					col.gameObject.GetComponent<Hero>().eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(col.gameObject.GetComponent<Hero>().carta1);

					col.gameObject.GetComponent<Hero>().ready = false;

					Dialogos.SetActive(true);
					if(Dialogos.GetComponent<DialogosTutorial3>())
					{
						Dialogos.GetComponent<DialogosTutorial3>().enabled = true;
					}
					if(Dialogos.GetComponent<DialogosTutorial7>())
					{
						Dialogos.GetComponent<DialogosTutorial7>().enabled = true;
					}
				}
			}

			StartCoroutine(espera());

			audio1.volume = efectos;
			audio1.clip = coje;
			audio1.Play();
		}
		//MUERTES
		if(col.gameObject.tag == "bala")
		{
			vida -= 15;

			Destroy(col.gameObject);

			var letras = (GameObject)Instantiate(textos, new Vector3(transform.position.x, transform.position.y+4,transform.position.z), transform.rotation);
			letras.transform.parent = transform;
			letras.GetComponent<TextMesh>().text = "15";
		}

		if(col.gameObject.tag == "balaFusil")
		{
			vida -= 40;

			Destroy(col.gameObject);

			var letras = (GameObject)Instantiate(textos, new Vector3(transform.position.x, transform.position.y+4,transform.position.z), transform.rotation);
			letras.transform.parent = transform;
			letras.GetComponent<TextMesh>().text = "40";
		}
		if(col.gameObject.tag == "balaEscopeta")
		{
			vida -= 15;

			Destroy(col.gameObject);

			var letras = (GameObject)Instantiate(textos, new Vector3(transform.position.x, transform.position.y+4,transform.position.z), transform.rotation);
			letras.transform.parent = transform;
			letras.GetComponent<TextMesh>().text = "15";
		}
		if(col.gameObject.tag == "balaSubmetra")
		{
			vida -= 20;

			Destroy(col.gameObject);

			var letras = (GameObject)Instantiate(textos, new Vector3(transform.position.x, transform.position.y+4,transform.position.z), transform.rotation);
			letras.transform.parent = transform;
			letras.GetComponent<TextMesh>().text = "20";
		}
		if(col.gameObject.tag == "balaMetra")
		{
			vida -= 25;

			Destroy(col.gameObject);

			var letras = (GameObject)Instantiate(textos, new Vector3(transform.position.x, transform.position.y+4,transform.position.z), transform.rotation);
			letras.transform.parent = transform;
			letras.GetComponent<TextMesh>().text = "25";
		}
		if(col.gameObject.tag == "balaMG")
		{
			vida -= 25;

			Destroy(col.gameObject);

			var letras = (GameObject)Instantiate(textos, new Vector3(transform.position.x, transform.position.y+4,transform.position.z), transform.rotation);
			letras.transform.parent = transform;
			letras.GetComponent<TextMesh>().text = "25";
		}
		if(col.gameObject.tag == "balaSniper")
		{
			vida -= 50;

			Destroy(col.gameObject);

			var letras = (GameObject)Instantiate(textos, new Vector3(transform.position.x, transform.position.y+4,transform.position.z), transform.rotation);
			letras.transform.parent = transform;
			letras.GetComponent<TextMesh>().text = "50";
		}
		if(col.gameObject.tag == "explo")
		{
			vida -= 50;

			var letras = (GameObject)Instantiate(textos, new Vector3(transform.position.x, transform.position.y+4,transform.position.z), transform.rotation);
			letras.transform.parent = transform;
			letras.GetComponent<TextMesh>().text = "50";
		}
	}

	IEnumerator espera ()
	{
		yield return new WaitForSeconds(2);
		Destroy(gameObject);
	}

	void polvo ()
	{
		var efect = (GameObject)Instantiate(polv, transform.position, transform.rotation);
	}
}
