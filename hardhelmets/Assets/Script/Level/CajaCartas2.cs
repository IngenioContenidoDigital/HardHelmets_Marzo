using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.EventSystems;

public class CajaCartas2 : MonoBehaviour {

	public int abiertas;

	public bool seleccionada;

	public int cajas;

	public int deb1;
	public int deb2;
	public int deb3;

	public GameObject card1;
	public GameObject card2;
	public GameObject card3;

	public int minima;
	public int maxima;//1,2,7,8,9,11,15

	public bool esconder;

	public GameObject cofre;

	void Start ()
	{
		abiertas = 0;

		deb1 = Random.Range(minima,maxima);
		deb2 = Random.Range(minima,maxima);
		deb3 = Random.Range(minima,maxima);

		cajas = PlayerPrefs.GetInt("caja1");

		card1.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "entradacaja", false);
		card2.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "entradacaja", false);
		card3.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "entradacaja", false);

		card1.GetComponent<skinCarta>().skinsToCombine[0] = deb1.ToString();
		card2.GetComponent<skinCarta>().skinsToCombine[0] = deb2.ToString();
		card3.GetComponent<skinCarta>().skinsToCombine[0] = deb3.ToString();
	}

	public void reiniciar ()
	{
		abiertas = 0;

		deb1 = Random.Range(minima,maxima);
		deb2 = Random.Range(minima,maxima);
		deb3 = Random.Range(minima,maxima);

		cajas = PlayerPrefs.GetInt("caja1");

		card1.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "entradacaja", false);
		card2.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "entradacaja", false);
		card3.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "entradacaja", false);

		card1.GetComponent<skinCarta>().skinsToCombine[0] = deb1.ToString();
		card2.GetComponent<skinCarta>().skinsToCombine[0] = deb2.ToString();
		card3.GetComponent<skinCarta>().skinsToCombine[0] = deb3.ToString();
	}

	void Update ()
	{
		if(Input.GetButtonDown("Jump") || Input.GetButtonDown("Submit") || Input.GetAxis("DISPARO") > 0 || Input.GetButtonDown("DISPARO 2"))
		{
			if(!seleccionada)
			{
				card1.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "destapar", false);
				card2.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "destapar", false);
				card3.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "destapar", false);

				abiertas += 3;

				cajas -= 1;

				PlayerPrefs.SetInt("card"+deb1, PlayerPrefs.GetInt("card"+deb1)+1);
				PlayerPrefs.SetInt("card"+deb2, PlayerPrefs.GetInt("card"+deb2)+1);
				PlayerPrefs.SetInt("card"+deb3, PlayerPrefs.GetInt("card"+deb3)+1);

			}
			seleccionada = true;
		}
		PlayerPrefs.SetInt("caja", cajas);

		if(abiertas >= 3 && !esconder)
		{
			StartCoroutine(cerrar());
			esconder = true;
		}
	}

	public EventSystem eventsystem;
	public GameObject enter;
	IEnumerator cerrar()
	{
		yield return new WaitForSeconds(2);

		card1.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "salida", false);
		card2.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "salida", false);
		card3.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "salida", false);

		abiertas = 0;
		esconder = false;

		seleccionada = false;

		eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(enter);
		gameObject.SetActive(false);

		cofre.SetActive(false);
	}
}
