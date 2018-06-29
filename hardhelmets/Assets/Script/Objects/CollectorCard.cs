using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;

public class CollectorCard : MonoBehaviour {

	public string idioma;

	public GameObject grafica;

	public bool Objecto3D;
	public bool Grados360;

	public GameObject nacer;
	public GameObject nacer2;
	public GameObject Crear;

	public bool destruir;
	public bool crear;

	public float posX;
	public float posY;

	public float escala;

	//------------

	public bool desb;
	public bool bloq;
	public int card;

	//----DESCRIPCION
	public float poder;
	public float poderfinal;
	public Text power;
	public bool especifico;
	public bool medico;
	public string specificI;
	public string specificE;
	public string specificC;
	public Text descripcion;

	public string ingles;
	public string español;
	public string chino;

	public float saludMax;
	public float suma;

	public GameObject gris;

	// Use this for initialization
	void Start ()
	{
		idioma = PlayerPrefs.GetString("idioma");

		PlayerPrefs.SetInt("card1", 0);
		card = int.Parse(gameObject.name);

		saludMax = 100+PlayerPrefs.GetInt("PlayerLevel")*4;

		suma = saludMax*2/104;

		poderfinal = saludMax*poder/104;

		if(PlayerPrefs.GetInt("card"+card) == 0)
		{
			gris.SetActive(true);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(desb)
		{
			if(PlayerPrefs.GetInt("card"+card) == 0)
			{
				Destroy(gameObject);
			}
		}
		if(bloq)
		{
			if(PlayerPrefs.GetInt("card"+card) >= 1)
			{
				Destroy(gameObject);
			}
		}

		if(destruir)
		{
			destruir = false;

			foreach(Transform child in nacer.transform)
			{
				Destroy(child.gameObject);
			}
			foreach(Transform child in nacer2.transform)
			{
				Destroy(child.gameObject);
			}
			crear = true;
		}
		if(crear && nacer.transform.childCount <= 0 && nacer2.transform.childCount <= 0)
		{
			crear = false;

			if(Objecto3D)
			{
				var objeto = (GameObject)Instantiate(Crear, new Vector3(nacer2.transform.position.x+posX, nacer2.transform.position.y+posY, nacer2.transform.position.z), Quaternion.Euler(0,0,0)); 
				if(Grados360)
				{
					objeto.transform.rotation = Quaternion.Euler(0,Random.Range(0,360),0);
				}
				objeto.transform.parent = nacer2.transform;
				objeto.transform.localScale = new Vector3(escala, escala, escala);
			}else
			{
				var objeto = (GameObject)Instantiate(Crear, new Vector3(nacer.transform.position.x+posX, nacer.transform.position.y+posY, nacer.transform.position.z), Quaternion.Euler(0,0,0)); //(posX,posY,0)
				objeto.transform.parent = nacer.transform;
				objeto.GetComponent<RectTransform>().localScale = new Vector3(0.4f, 0.4f, 0.4f);
			}
		}
		if(Input.GetButtonDown("Cancel"))
		{
			Application.LoadLevel("Load");
			loading.nombre = "menu";
		}
	}


	public void entra()
	{
		
	}

	public void sale()
	{
		
	}

	public void click()
	{
		destruir = true;

		grafica.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "arrastre", false);

		if(especifico)
		{
			if(idioma == "ENGLISH")
			{
				power.text = specificI;
			}
			if(idioma == "SPANISH")
			{
				power.text = specificE;
			}
			if(idioma == "CHINESE")
			{
				power.text = specificC;
			}
		}else if(medico)
		{
			power.text = "+"+suma.ToString("F0");
		}else
		{
			power.text = poderfinal.ToString("F0");
		}

		if(idioma == "ENGLISH")
		{
			descripcion.GetComponent<UnityEngine.UI.Text>().text = ingles;
		}
		if(idioma == "SPANISH")
		{
			descripcion.GetComponent<UnityEngine.UI.Text>().text = español;
		}
		if(idioma == "CHINESE")
		{
			descripcion.GetComponent<UnityEngine.UI.Text>().text = chino;
		}

		StartCoroutine(EspCarta());
	}
	IEnumerator EspCarta()
	{
		yield return new WaitForSpineAnimationComplete(grafica.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0));
		grafica.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "normal", false);
	}
}
