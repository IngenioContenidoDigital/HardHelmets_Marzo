using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.EventSystems;

using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
using Prototype.NetworkLobby;

public class cofreLobby : MonoBehaviour {

	public GameObject Cartas;

	public int abiertas;

	public int deb1;
	public int deb2;
	public int deb3;

	bool listo1;
	bool listo2;
	bool listo3;

	public GameObject card1;
	public GameObject card2;
	public GameObject card3;

	public GameObject card1b;
	public GameObject card2b;
	public GameObject card3b;

	public GameObject baul;

	//CANTIDAD DE CARTAS
	public int carta1;
	public int carta2;
	public int carta3;

	//DE QUE A QUE CARTA PUEDE DESTAPAR
	public int minima;
	public int maxima;//1,2,7,8,9,11,15

	public int cajas;

	bool esconder;

	public GameObject master;

	public bool open;

	// Use this for initialization
	void Start ()
	{
		abiertas = 0;

		deb1 = Random.Range(minima,maxima);
		deb2 = Random.Range(minima,maxima);
		deb3 = Random.Range(minima,maxima);

		listo1 = true;

		cajas = PlayerPrefs.GetInt("caja1");
	}

	// Update is called once per frame
	void Update ()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if(Input.GetMouseButtonDown(0))
		{
			if(Physics.Raycast(ray, out hit))
			{
				if(hit.collider == gameObject.GetComponent<Collider>())
				{
					GetComponent<BoxCollider>().enabled = false;
					cajas -= 1;

					PlayerPrefs.SetInt("card"+deb1, carta1);
					PlayerPrefs.SetInt("card"+deb2, carta2);
					PlayerPrefs.SetInt("card"+deb3, carta3);

					GetComponent<Animator>().SetBool("abre", true);
					if(gameObject.name == "Baul")
					{
						master.GetComponent<CommunityList>().pantalla = "esconder";
					}else
					{
						master.GetComponent<regresaLobby>().actual = "esconder";
					}

					open = false;
				}
			}
		}

		PlayerPrefs.SetInt("caja", cajas);

		if(open && Input.GetButtonDown("Jump") || open && Input.GetButtonDown("Submit"))
		{
			open = false;
			GetComponent<BoxCollider>().enabled = false;
			cajas -= 1;
			print("RESTAR COFRE");

			PlayerPrefs.SetInt("card"+deb1, carta1);
			PlayerPrefs.SetInt("card"+deb2, carta2);
			PlayerPrefs.SetInt("card"+deb3, carta3);

			GetComponent<Animator>().SetBool("abre", true);
			if(gameObject.name == "Baul")
			{
				master.GetComponent<CommunityList>().pantalla = "esconder";
			}else
			{
				master.GetComponent<regresaLobby>().actual = "esconder";
			}
		}

		if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("entrac"))
		{
			GetComponent<Animator>().SetBool("cerrar", false);

			GetComponent<BoxCollider>().enabled = true;
			abiertas = 0;

			card1b.GetComponent<CajaCarta>().seleccionada = false;
			card2b.GetComponent<CajaCarta>().seleccionada = false;
			card3b.GetComponent<CajaCarta>().seleccionada = false;
			card1b.GetComponent<EventTrigger>().enabled = true;
			card2b.GetComponent<EventTrigger>().enabled = true;
			card3b.GetComponent<EventTrigger>().enabled = true;

			deb1 = Random.Range(minima,maxima);
			deb2 = Random.Range(minima,maxima);
			deb3 = Random.Range(minima,maxima);

			listo1 = true;

			cajas = PlayerPrefs.GetInt("caja1");

			GetComponent<Animator>().SetBool("reiniciar", false);
		}
		if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("abrec"))
		{
			GetComponent<Animator>().SetBool("abre", false);
		}
		/*if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("cierrac"))
		{
			GetComponent<Animator>().SetBool("cerrar", false);
		}*/
		////CARTAS
		PlayerPrefs.SetInt("caja1", cajas);

		if(listo1)
		{
			print("TODOS DEBERIAN SER DIFERENTES");

			card1.GetComponent<skinCarta>().skinsToCombine[0] = deb1.ToString();
			card2.GetComponent<skinCarta>().skinsToCombine[0] = deb2.ToString();
			card3.GetComponent<skinCarta>().skinsToCombine[0] = deb3.ToString();

			/*PlayerPrefs.SetInt("card"+deb1, 1);
			PlayerPrefs.SetInt("card"+deb2, 1);
			PlayerPrefs.SetInt("card"+deb3, 1);*/

			//CANTIDAD DE CARTAS
			carta1 = PlayerPrefs.GetInt("card"+deb1);
			carta1 = carta1+1;

			carta2 = PlayerPrefs.GetInt("card"+deb2);
			carta2 = carta2+1;

			carta3 = PlayerPrefs.GetInt("card"+deb3);
			carta3 = carta3+1;

			listo1 = false;
		}

		if(abiertas >= 3 && cajas <= 0 && !esconder)
		{
			open = false;
			//otra.SetActive(true);
			StartCoroutine(cerrar());
			esconder = true;
		}
		if(abiertas >= 3 && cajas >= 1 && !esconder)
		{
			open = false;
			StartCoroutine(cerrarNuevo());
			esconder = true;
		}
	}

	public EventSystem eventsystem;
	public GameObject m1;
	IEnumerator cerrar()
	{
		yield return new WaitForSeconds(2);

		card1.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "salida", false);
		card2.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "salida", false);
		card3.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "salida", false);

		abiertas = 0;
		esconder = false;

		GetComponent<Animator>().SetBool("cerrar", true);

		eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(m1);

		if(gameObject.name == "Baul")
		{
			master.GetComponent<CommunityList>().pantalla = "";
			master.GetComponent<CommunityList>().menu.GetComponent<Animator>().SetBool("sale", false);
			master.GetComponent<CommunityList>().menu.GetComponent<Animator>().SetBool("entra", true);
		}else
		{
			master.GetComponent<regresaLobby>().actual = master.GetComponent<regresaLobby>().actual2;
			master.GetComponent<regresaLobby>().menu.GetComponent<Animator>().SetBool("sale", false);
			master.GetComponent<regresaLobby>().menu.GetComponent<Animator>().SetBool("entra", true);
		}
	}
	IEnumerator cerrarNuevo()
	{
		yield return new WaitForSeconds(2);

		card1.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "salida", false);
		card2.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "salida", false);
		card3.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "salida", false);
		GetComponent<Animator>().SetBool("cerrar", true);

		if(cajas >= 1)
		{
			if(gameObject.name == "Baul")
			{
				master.GetComponent<CommunityList>().pantalla = "cofre";
			}else
			{
				master.GetComponent<regresaLobby>().actual = "cofre";
			}
			StartCoroutine(abreNuevo());
		}else
		{
			if(gameObject.name == "Baul")
			{
				master.GetComponent<CommunityList>().pantalla = "";
				master.GetComponent<CommunityList>().menu.GetComponent<Animator>().SetBool("sale", false);
				master.GetComponent<CommunityList>().menu.GetComponent<Animator>().SetBool("entra", true);
			}else
			{
				print("NO MAS COFRES");
				master.GetComponent<regresaLobby>().Regresar();
				/*master.GetComponent<regresaLobby>().actual = master.GetComponent<regresaLobby>().actual2;
				master.GetComponent<regresaLobby>().menu.GetComponent<Animator>().SetBool("sale", false);
				master.GetComponent<regresaLobby>().menu.GetComponent<Animator>().SetBool("entra", true);*/
			}
		}
	}
	IEnumerator abreNuevo()
	{
		yield return new WaitForSeconds(2);

		abiertas = 0;
		esconder = false;

		GetComponent<Animator>().SetBool("reiniciar", true);
	}

	public void aparecen()
	{
		Cartas.SetActive(true);
		card1.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "entradacaja", false);
		card2.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "entradacaja", false);
		card3.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "entradacaja", false);
	}

	public void desaparecen()
	{
		Cartas.SetActive(false);
	}
	public void reset()
	{
		if(gameObject.name == "Baul")
		{
			master.GetComponent<CommunityList>().pantalla = "cofre";
		}else
		{
			master.GetComponent<regresaLobby>().actual = "cofre";
		}
	}
	public void ahora()
	{
		open = true;
	}

	//SONIDOS
	public AudioSource audio1;
	public AudioClip nacer;
	public void nace()
	{
		audio1.clip = nacer;
		audio1.Play();
	}
	public AudioClip[] cart;
	public void abre()
	{
		audio1.clip = cart[Random.Range(0,cart.Length)];
		audio1.Play();
	}

	public AudioClip sale1;
	public void exit()
	{
		audio1.clip = sale1;
		audio1.Play();
	}

	public AudioClip sale2;
	public void exit2()
	{
		audio1.clip = sale2;
		audio1.Play();
	}
}
