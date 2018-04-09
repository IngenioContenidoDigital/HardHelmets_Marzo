using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distancia2Offline : MonoBehaviour {

	//--------------ICONO DE ENEMIGO
	public GameObject Player1;
	public GameObject Objeto;
	public string target;

	public Vector3 minScale;
	public Vector3 maxScale;

	public UnityEngine.UI.Image icono;
	public Sprite BuenaS;
	public Sprite MalaS;
	public Sprite BuenaC;
	public Sprite MalaC;

	public float equis;

	public bool ver;

	public float division;

	void Start ()
	{

	}


	void Update ()
	{
		//--------------ICONO DE ENEMIGO
		if(Player1 == null)
		{
			Player1 = GameObject.Find("Hero");
		}
		if(Objeto == null)
		{
			Objeto = GameObject.Find(target);
		}

		if(Player1 != null && Objeto != null)
		{
			if(Objeto.tag == "BaseBuena")
			{
				icono.sprite = BuenaS;
			}else if(Objeto.tag == "BaseMala")
			{
				icono.sprite = MalaS;
			}

			//POSICION ADELANTE O ATRAS
			if(Player1.transform.position.x < Objeto.transform.position.x)
			{
				equis = -30;
			}else
			{
				equis = -770;
			}

			//TAMAÑO
			float distancia = Mathf.Abs((Player1.transform.position - Objeto.transform.position).x);
			float porcentaje = Objeto.transform.position.x / distancia;
			//iconoJugador.GetComponent<RectTransform>().localScale = Vector3.Lerp(minScale, maxScale, porcentaje);
			Vector3 taman = Vector3.Lerp(minScale, maxScale, porcentaje/division);
			GetComponent<RectTransform>().localScale = taman;


			if(!ver)
			{
				GetComponent<RectTransform>().localScale = new Vector3(0,0,0);
			}

			//ARRIBA ABAJO
			float distancia2 = Mathf.Abs((Player1.transform.position - Objeto.transform.position).z)*2.5f;

			if(Player1.transform.position.z < Objeto.transform.position.z)
			{
				GetComponent<RectTransform>().anchoredPosition = new Vector3(equis, distancia2, 0);
			}else
			{
				GetComponent<RectTransform>().anchoredPosition = new Vector3(equis, 0-distancia2, 0);
			}

			if(GetComponent<RectTransform>().anchoredPosition.y >= 120)
			{
				GetComponent<RectTransform>().anchoredPosition = new Vector3(equis,120,0);
			}
			if(GetComponent<RectTransform>().anchoredPosition.y <= -120)
			{
				GetComponent<RectTransform>().anchoredPosition = new Vector3(equis,-120,0);
			}

			//VER O NO VER
			if(Mathf.Abs((Player1.transform.position - Objeto.transform.position).x) >= 35 && Objeto.tag != "newtra")
			{
				ver = true;
			}else
			{
				ver = false;
			}
		}else
		{
			GetComponent<RectTransform>().localScale = new Vector3(0,0,0);
		}
	}

	void FixedUpdate ()
	{
		//--------------ICONO DE ENEMIGO
		if(Player1 == null)
		{
			Player1 = GameObject.Find("Hero");
		}
		if(Objeto == null)
		{
			Objeto = GameObject.Find(target);
		}

		if(Player1 != null && Objeto != null)
		{
			if(Objeto.tag == "BaseMala")
			{
				icono.sprite = BuenaC;
			}else if(Objeto.tag == "BaseBuena")
			{
				icono.sprite = MalaC;
			}

			//POSICION ADELANTE O ATRAS
			if(Player1.transform.position.x < Objeto.transform.position.x)
			{
				equis = -30;
			}else
			{
				equis = -770;
			}

			//TAMAÑO
			float distancia = Mathf.Abs((Player1.transform.position - Objeto.transform.position).x);
			float porcentaje = Objeto.transform.position.x / distancia;
			//iconoJugador.GetComponent<RectTransform>().localScale = Vector3.Lerp(minScale, maxScale, porcentaje);
			Vector3 taman = Vector3.Lerp(minScale, maxScale, porcentaje/division);
			GetComponent<RectTransform>().localScale = taman;


			if(!ver)
			{
				GetComponent<RectTransform>().localScale = new Vector3(0,0,0);
			}

			//ARRIBA ABAJO
			float distancia2 = Mathf.Abs((Player1.transform.position - Objeto.transform.position).z)*2.5f;

			if(Player1.transform.position.z < Objeto.transform.position.z)
			{
				GetComponent<RectTransform>().anchoredPosition = new Vector3(equis, distancia2, 0);
			}else
			{
				GetComponent<RectTransform>().anchoredPosition = new Vector3(equis, 0-distancia2, 0);
			}

			if(GetComponent<RectTransform>().anchoredPosition.y >= 120)
			{
				GetComponent<RectTransform>().anchoredPosition = new Vector3(equis,120,0);
			}
			if(GetComponent<RectTransform>().anchoredPosition.y <= -120)
			{
				GetComponent<RectTransform>().anchoredPosition = new Vector3(equis,-120,0);
			}

			//VER O NO VER
			if(Mathf.Abs((Player1.transform.position - Objeto.transform.position).x) >= 35 && Objeto.tag != "newtra")
			{
				ver = true;
			}else
			{
				ver = false;
			}
		}else
		{
			GetComponent<RectTransform>().localScale = new Vector3(0,0,0);
		}
	}
}
