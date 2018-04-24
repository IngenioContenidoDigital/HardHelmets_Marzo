using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class changeLevelOffline : MonoBehaviour {

	public int nivel;

	public UnityEngine.UI.Image imagen;
	public Sprite cero;
	public Sprite uno;
	public Sprite dos;

	public UnityEngine.UI.Image lev0;
	public Sprite ceroa;
	public Sprite cerob;
	public UnityEngine.UI.Image lev1;
	public Sprite unoa;
	public Sprite unob;
	public UnityEngine.UI.Image lev2;
	public Sprite dosa;
	public Sprite dosb;

	public UnityEngine.UI.Image level;
	public Sprite l0;
	public Sprite l1;
	public Sprite l2;

	public bool azar;

	int contar;

	public EventSystem eventsystem;
	public GameObject lvButton;

	// Use this for initialization
	void Start ()
	{

	}

	public void Update ()
	{
		if(escenarios.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("SceneEntra"))
		{
			escenarios.GetComponent<Animator>().SetBool("entra", false);
		}
		if(escenarios.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("SceneSale"))
		{
			escenarios.GetComponent<Animator>().SetBool("sale", false);
		}

		if(nivel == 0)
		{
			imagen.sprite = cero;

			lev0.sprite = cerob;
			lev1.sprite = unoa;
			lev2.sprite = dosa;

			level.sprite = l0;
		}else if(nivel == 1)
		{
			imagen.sprite = uno;

			lev0.sprite = ceroa;
			lev1.sprite = unob;
			lev2.sprite = dosa;

			level.sprite = l1;
		}else if(nivel == 2)
		{
			imagen.sprite = dos;

			lev0.sprite = ceroa;
			lev1.sprite = unoa;
			lev2.sprite = dosb;

			level.sprite = l2;
		}

		//lob.GetComponent<LobbyManager>().tablero[0] = nivel;

		if(azar)
		{
			contar += 1;
			if(contar >= 4)
			{
				GetComponent<AudioSource>().Play();
				nivel = Random.Range(0,3);
				contar = 0;
			}
		}
	}

	public GameObject escenarios;

	public void press ()
	{
		escenarios.SetActive(true);
		escenarios.GetComponent<Animator>().SetBool("entra", true);
		eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(lvButton);
	}

	public void cerrar ()
	{
		azar = false;
		escenarios.GetComponent<Animator>().SetBool("sale", true);
		//escenarios.SetActive(false);
	}

	public void scene0 ()
	{
		azar = false;
		nivel = 0;
	}
	public void scene1 ()
	{
		azar = false;
		nivel = 1;

	}
	public void scene2 ()
	{
		azar = false;
		nivel = 2;
	}

	public void Ran ()
	{
		azar = true;
	}
}
