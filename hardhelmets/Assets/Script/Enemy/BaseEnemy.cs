using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

public class BaseEnemy : MonoBehaviour {

	public GameObject Heroe;

	bool vivo;
	public float sangre;
	float saludMax;

	public GameObject siguiente;

	public UnityEngine.UI.Image salud;

	public GameObject luz;

	public GameObject fuego1;
	public GameObject fuego2;
	public GameObject fuego3;
	public GameObject fuego4;

	public GameObject destruida;

	public AudioSource audio2;
	float efectos;

	void Start ()
	{
		efectos = PlayerPrefs.GetFloat("efects");
		vivo = true;
		saludMax = sangre;
	}

	void Update ()
	{
		if(vivo)
		{
			salud.fillAmount = sangre/1000;

			if(sangre <= saludMax*70/100)
			{
				fuego1.SetActive(true);
			}
			if(sangre <= saludMax*50/100)
			{
				fuego2.SetActive(true);
			}
			if(sangre <= saludMax*30/100)
			{
				fuego3.SetActive(true);
			}
			if(sangre <= saludMax*10/100)
			{
				fuego4.SetActive(true);
			}

			if(sangre <= 0)
			{
				audio2.volume = efectos;
				audio2.Play();

				if(gameObject.name != "EnemyBase 3")
				{
					siguiente.SetActive(true);
				}else
				{
					siguiente.GetComponent<ManagerEnemy>().fin = true;
				}

				GetComponent<Animator>().SetBool("muere", true);
				Destroy(fuego1);
				Destroy(fuego2);
				Destroy(fuego3);
				Destroy(fuego4);

				destruida.SetActive(true);
				vivo = false;
			}										
		}
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "bala")
		{
			luz.SetActive(true);
			StartCoroutine(apaga());
			sangre -= 10;
		}
		if(col.gameObject.tag == "balaFusil")
		{
			luz.SetActive(true);
			StartCoroutine(apaga());
			sangre -= 25;
		}
		if(col.gameObject.tag == "balaEscopeta")
		{
			luz.SetActive(true);
			StartCoroutine(apaga());
			sangre -= 10;
		}
		if(col.gameObject.tag == "balaSubmetra")
		{
			luz.SetActive(true);
			StartCoroutine(apaga());
			sangre -= 20;
		}
		if(col.gameObject.tag == "balaMetra")
		{
			luz.SetActive(true);
			StartCoroutine(apaga());
			sangre -= 25;
		}
		if(col.gameObject.tag == "balaMG")
		{
			luz.SetActive(true);
			StartCoroutine(apaga());
			sangre -= 50;
		}
		if(col.gameObject.tag == "explo")
		{
			luz.SetActive(true);
			StartCoroutine(apaga());
			sangre -= 100;
		}
	}

	public void muerte ()
	{
		Destroy(gameObject);
	}

	public void vivracion ()
	{
		if(Vector3.Distance(transform.position, Heroe.transform.position) <= 80)
		{
			ProCamera2DShake.Instance.Shake();
		}
	}

	IEnumerator apaga ()
	{
		yield return new WaitForSeconds(0.1f);
		luz.SetActive(false);
	}
}
