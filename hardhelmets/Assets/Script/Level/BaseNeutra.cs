using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseNeutra : MonoBehaviour {

	public string tomada = "neutra";

	public bool sumarBueno;
	public bool sumarMalo;

	public int puntosBueno;
	public int puntosMalo;

	public float puntosTotales;

	public GameObject Bandera;

	public Texture good;
	public Texture bad;

	public GameObject mover;

	public float altura;
	public float ajuste;

	//VOLUMEN
	public bool sonar;

	public bool sonando;
	float efectos;
	public AudioSource audio1;
	public AudioSource audio2;

	public GameObject Dialogos;

	public GameObject Hero;

	void Start ()
	{
		efectos = PlayerPrefs.GetFloat("efects");
		audio1.volume = efectos;
	}

	void OnChangeColor (float puntosTotales)
	{
		if(puntosTotales > 0)
		{
			Bandera.GetComponent<Renderer>().material.mainTexture = good;
		}
		if(puntosTotales < 0)
		{
			Bandera.GetComponent<Renderer>().material.mainTexture = bad;
		}
	}
	// Update is called once per frame
	void Update ()
	{
		if(tomada == "buena")
		{
			gameObject.tag = "BaseBuena";
		}else if(tomada == "mala")
		{
			gameObject.tag = "BaseMala";
		}else
		{
			gameObject.tag = "newtra";
		}

		if(sonar)
		{
			if(!sonando)
			{
				audio1.Play();
				sonando = true;
			}
		}else
		{
			audio1.Stop();
		}

		if(puntosBueno > 0)
		{
			sumarBueno = true;
		}else
		{
			sumarBueno = false;
		}

		altura = -0.58f+ajuste;
		mover.transform.localPosition = new Vector3(mover.transform.localPosition.x, altura, mover.transform.localPosition.z);

		if(puntosMalo > 0)
		{
			sumarMalo = true;
		}else
		{
			sumarMalo = false;
		}

		if(sumarBueno)
		{
			if(puntosTotales != 100)
			{
				sonar = true;
			}else if(!sumarMalo)
			{
				sonar = false;
				sonando = false;
			}
			if(Application.loadedLevelName == "Tutorial")
			{
				puntosTotales += 0.2f*puntosBueno;
			}else
			{
				puntosTotales += 0.1f*puntosBueno;
			}
		}else if(!sumarMalo)
		{
			sonar = false;
			sonando = false;
		}

		if(sumarMalo)
		{
			if(puntosTotales != -100)
			{
				sonar = true;
			}else if(!sumarBueno)
			{
				sonar = false;
				sonando = false;
			}

			puntosTotales -= 0.1f*puntosMalo;  
		}

		if(puntosTotales > 0)
		{
			ajuste = puntosTotales/142;
			Bandera.GetComponent<Renderer>().material.mainTexture = good;
		}
		if(puntosTotales < 0)
		{
			//ajuste = puntosTotales-(puntosTotales*2)/142;
			ajuste = Mathf.Abs(puntosTotales/142);
			Bandera.GetComponent<Renderer>().material.mainTexture = bad;
		}
		if(puntosTotales >= 100)
		{
			puntosTotales = 100;

			sumarBueno = false;
			sumarMalo = false;

			tomada = "buena";
			gameObject.tag = "BaseBuena";
			//GetComponent<NetworkView>().tag = "BaseBuena";
			if(!mas)
			{
				if(veces == 0 && Application.loadedLevelName == "Tutorial")
				{
					Hero.GetComponent<Hero>().caminarA = false;
					Hero.GetComponent<Hero>().caminarU = false;
					Hero.GetComponent<Hero>().caminarD = false;
					Hero.GetComponent<Hero>().caminarI = false;

					Hero.GetComponent<Hero>().ready = false;

					Dialogos.SetActive(true);
				}
				veces += 1;
				audio2.Play();
				mas = true;
			}
		}else
		{
			mas = false;
		}

		if(tomada == "buena" && puntosTotales < 0)
		{
			tomada = "newtra";
		}else if(tomada == "mala" && puntosTotales > 0)
		{
			tomada = "newtra";
		}

		if(puntosTotales <= -100)
		{
			puntosTotales = -100;

			sumarBueno = false;
			sumarMalo = false;

			tomada = "mala";
			gameObject.tag = "BaseMala";
		}
	}
	public bool mas;
	public int veces;
	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "uno")
		{
			puntosBueno += 1;
		}
		if(col.gameObject.tag == "dos")
		{
			puntosMalo += 1;
		}
	}

	void OnTriggerExit (Collider col)
	{
		if(col.gameObject.tag == "uno")
		{
			puntosBueno -= 1;
		}
		if(col.gameObject.tag == "dos")
		{
			puntosMalo -= 1;
		}
	}
}
