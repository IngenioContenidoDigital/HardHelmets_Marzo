using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Spine.Unity;
using UnityEngine.EventSystems;

public class manoTutorial : MonoBehaviour {

	public GameObject manoTeclado;
	public GameObject manoXbox;

	public string nombre;
	public string nombre2;
	public float costo;
	//public int cantidad;
	//public UnityEngine.UI.Text cantidadT;
	//public int cantidadTotal;

	public GameObject Barra;

	public UnityEngine.UI.Image imagen;

	public UnityEngine.UI.Image Load;

	//----IMAGENES DE CARTAS
	public Sprite a0;

	public Sprite a1;
	public Sprite a2;
	public Sprite a3;
	public Sprite a4;
	public Sprite a5;
	public Sprite a6;
	public Sprite a7;
	public Sprite a8;
	public Sprite a9;
	public Sprite a10;
	public Sprite a11;
	public Sprite a12;
	public Sprite a13;
	public Sprite a14;
	public Sprite a15;
	public Sprite a16;
	public Sprite a17;
	public Sprite a18;
	public Sprite a19;
	public Sprite a20;
	public Sprite a21;
	public Sprite a22;
	public Sprite a23;
	public Sprite a24;

	float efectos;
	public AudioClip nace;
	public AudioClip usa;

	//CONTROL
	public bool selected;
	public bool listo;

	// Use this for initialization
	void Start ()
	{
		inicial = transform.localPosition;
		//cantidadTotal = PlayerPrefs.GetInt("card"+nombre+"cantidad");
		animacion.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "inactiva", false);
	}
	/*bool entrance;
	bool animandoentrada;
	bool exit;
	bool animandosalida;*/
	string nomm;
	// Update is called once per frame
	void Update ()
	{
		nombre2 = nombre;

		efectos = PlayerPrefs.GetFloat("efects");

		if(nombre == "0")
		{
			costo = 5000;
		}else
		{
			costo = PlayerPrefs.GetInt(""+nombre+"costo");
		}

		//UNO
		if(nombre == "0")
		{
			animacion.GetComponent<skinCarta>().skinsToCombine[0] = "default";
			Load.sprite = a0;
		}else if(nombre == "1")
		{
			animacion.GetComponent<skinCarta>().skinsToCombine[0] = "1";
			Load.sprite = a1;
		}else if(nombre == "2")
		{
			animacion.GetComponent<skinCarta>().skinsToCombine[0] = "2";
			Load.sprite = a2;
		}else if(nombre == "3")
		{
			animacion.GetComponent<skinCarta>().skinsToCombine[0] = "3";
			Load.sprite = a3;
		}else if(nombre == "4")
		{
			animacion.GetComponent<skinCarta>().skinsToCombine[0] = "4";
			Load.sprite = a4;
		}else if(nombre == "5")
		{
			animacion.GetComponent<skinCarta>().skinsToCombine[0] = "5";
			Load.sprite = a5;
		}else if(nombre == "6")
		{
			animacion.GetComponent<skinCarta>().skinsToCombine[0] = "6";
			Load.sprite = a6;
		}else if(nombre == "7")
		{
			animacion.GetComponent<skinCarta>().skinsToCombine[0] = "7";
			Load.sprite = a7;
		}else if(nombre == "8")
		{
			animacion.GetComponent<skinCarta>().skinsToCombine[0] = "8";
			Load.sprite = a8;
		}else if(nombre == "9")
		{
			animacion.GetComponent<skinCarta>().skinsToCombine[0] = "9";
			Load.sprite = a9;
		}else if(nombre == "10")
		{
			animacion.GetComponent<skinCarta>().skinsToCombine[0] = "10";
			Load.sprite = a10;
		}else if(nombre == "11")
		{
			animacion.GetComponent<skinCarta>().skinsToCombine[0] = "11";
			Load.sprite = a11;
		}else if(nombre == "12")
		{
			animacion.GetComponent<skinCarta>().skinsToCombine[0] = "12";
			Load.sprite = a12;
		}else if(nombre == "13")
		{
			animacion.GetComponent<skinCarta>().skinsToCombine[0] = "13";
			Load.sprite = a13;
		}else if(nombre == "14")
		{
			animacion.GetComponent<skinCarta>().skinsToCombine[0] = "14";
			Load.sprite = a14;
		}else if(nombre == "15")
		{
			animacion.GetComponent<skinCarta>().skinsToCombine[0] = "15";
			Load.sprite = a15;
		}else if(nombre == "16")
		{
			animacion.GetComponent<skinCarta>().skinsToCombine[0] = "16";
			Load.sprite = a16;
		}else if(nombre == "17")
		{
			animacion.GetComponent<skinCarta>().skinsToCombine[0] = "17";
			Load.sprite = a17;
		}else if(nombre == "18")
		{
			animacion.GetComponent<skinCarta>().skinsToCombine[0] = "18";
			Load.sprite = a18;
		}else if(nombre == "19")
		{
			animacion.GetComponent<skinCarta>().skinsToCombine[0] = "19";
			Load.sprite = a19;
		}else if(nombre == "20")
		{
			animacion.GetComponent<skinCarta>().skinsToCombine[0] = "20";
			Load.sprite = a20;
		}else if(nombre == "21")
		{
			animacion.GetComponent<skinCarta>().skinsToCombine[0] = "21";
			Load.sprite = a21;
		}else if(nombre == "22")
		{
			animacion.GetComponent<skinCarta>().skinsToCombine[0] = "22";
			Load.sprite = a22;
		}else if(nombre == "23")
		{
			animacion.GetComponent<skinCarta>().skinsToCombine[0] = "23";
			Load.sprite = a23;
		}
		else if(nombre == "24")
		{
			animacion.GetComponent<skinCarta>().skinsToCombine[0] = "24";
			Load.sprite = a24;
		}

		Load.fillAmount = 1-Barra.GetComponent<barraOflline>().fill*100/costo;

		if(Load.fillAmount <= 0)
		{
			Load.GetComponent<Image>().enabled = false;
		}else //if(cantidad >= 1)
		{
			Load.GetComponent<Image>().enabled = true;
		}
		//cantidadT.text = cantidad.ToString();

		/*if(cantidadTotal <= 0)
		{
			cantidadTotal = 0;
			PlayerPrefs.SetInt("card"+nombre, 0);
		}*/
		//PlayerPrefs.SetInt("card"+nombre+"cantidad", cantidadTotal);

		//PlayerPrefs.SetInt("Mano"+nombre2+"cantidad", cantidad);

		/*if(cantidad <= 0)
		{
			cantidadT.text = "";
			//PlayerPrefs.SetInt("card"+nombre, 0);
			PlayerPrefs.SetInt("Mano"+nombre2, 0);
			//PlayerPrefs.SetInt("card"+nombre+"cantidad", 0);
			PlayerPrefs.SetInt("Mano"+nombre2+"cantidad", 0);

			GetComponent<Button>().enabled = false;
			GetComponent<Image>().color = new Color32(100,100,100,100);

			if(nomm != "inactiva")
			{
				animacion.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "inactiva", false);
			}

			animacion.GetComponent<skinCarta>().skinsToCombine[0] = "default";

			nombre = "0";
		}*/


		nomm = animacion.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0).Animation.Name;
		if(Barra.GetComponent<barraOflline>().fill*100 >= costo && nomm == "inactiva")// && cantidad >= 1)
		{
			GetComponent<AudioSource>().volume = efectos;
			GetComponent<AudioSource>().clip = nace;
			GetComponent<AudioSource>().Play();

			animacion.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "entrada", false);
			GetComponent<Button>().enabled = true;
			GetComponent<Image>().color = new Color32(255,255,255,255);

			Player.GetComponent<Hero>().eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(gameObject);

			listo = true;	
		}
		if(Barra.GetComponent<barraOflline>().fill*100 < costo)// && cantidad >= 1)
		{
			GetComponent<Button>().enabled = false;
			GetComponent<Image>().color = new Color32(100,100,100,100);

			if(nomm != "inactiva")
			{
				animacion.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "inactiva", false);
			}
			selected = false;
		}

		if(Player == null)
		{
			Player = GameObject.Find("Hero");
		}

		if(Input.GetAxis("Vertical") > 0 && selected && listo)
		{
			Soltar();
			listo = false;
			StartCoroutine(Reactivar());
		}
	}
	IEnumerator Reactivar()
	{
		yield return new WaitForSeconds(1f);

		if(Barra.GetComponent<barraOflline>().fill*100 >= costo)
		{
			listo = true;
		}
	}

	/*public void OnMouseDrag ()
	{
		print("ARRASTRANDO");
	}*/

	Vector3 inicial;
	bool arrastrar;
	//public GameObject cursor;
	//bool preview;

	public void OnBeginDrag ()
	{
		if(GetComponent<Button>().enabled && nombre != "0")
		{
			//inicial = transform.localPosition;
			arrastrar = true;

			animacion.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "arrastre", false);
		}
	}

	public void OnDrag ()//(PointerEventData eventData)
	{
		if(arrastrar)
		{
			//preview = true;

			transform.position = Input.mousePosition;
			if(nomm != "arrastre")
			{
				animacion.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "arrastre", false);
			}
		}
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		/*if(preview)
		{
			if(piso.GetComponent<Collider>().Raycast (ray, out hit, Mathf.Infinity))
			{
				cursor.SetActive(true);
				cursor.transform.position = new Vector3(hit.point.x,hit.point.y+1,hit.point.z);//hit.point;
			}
		}*/
	}

	public GameObject Player; 

	public Transform piso;
	public GameObject animacion;

	public float valorNace;
	public void OnDragEnd ()//(PointerEventData eventData)
	{
		if(arrastrar)
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast (ray, out hit, Mathf.Infinity))//if(piso.GetComponent<Collider>().Raycast (ray, out hit, Mathf.Infinity))
			{
				GetComponent<AudioSource>().volume = efectos;
				GetComponent<AudioSource>().clip = usa;
				GetComponent<AudioSource>().Play();

				//cantidad -= 1;
				//cantidadTotal -= 1;

				Barra.GetComponent<barraOflline>().fill -= costo/100;
				if(nombre == "1")
				{
					Player.GetComponent<CrearCartas>().crearFusil();
					if(manoTeclado != null)
					{
						Destroy(manoTeclado);
					}
					if(manoXbox != null)
					{
						Destroy(manoXbox);
					}
				}else if(nombre == "2")
				{
					Player.GetComponent<CrearCartas>().crearSubmetra();
				}else if(nombre == "3")
				{
					Player.GetComponent<CrearCartas>().crearEscopeta();
				}else if(nombre == "4")
				{
					Player.GetComponent<CrearCartas>().crearMetra();
				}else if(nombre == "5")
				{
					Player.GetComponent<CrearCartas>().crearSniper();
				}else if(nombre == "6")
				{
					Player.GetComponent<CrearCartas>().crearLansaLlamas();
				}else if(nombre == "7")
				{
					Player.GetComponent<CrearCartas>().crearBazooka();
				}else if(nombre == "8")
				{
					Player.GetComponent<CrearCartas>().crearGranada();
				}else if(nombre == "9")
				{
					Player.GetComponent<CrearCartas>().crearSupplies();
				}else if(nombre == "10")
				{
					Player.GetComponent<CrearCartas>().crearFusilero();
				}else if(nombre == "11")
				{
					Player.GetComponent<CrearCartas>().crearSubmetralleto();
				}else if(nombre == "12")
				{
					Player.GetComponent<CrearCartas>().crearEscopeto();
				}else if(nombre == "13")
				{
					Player.GetComponent<CrearCartas>().crearMedico();
				}else if(nombre == "14")
				{
					Player.GetComponent<CrearCartas>().crearMg();
				}else if(nombre == "15")
				{
					Player.GetComponent<CrearCartas>().crearMortero();
				}else if(nombre == "16")
				{
					Player.GetComponent<CrearCartas>().crearMetralleto();
				}else if(nombre == "17")
				{
					//VIEJO DEL LANZA LLAMAS
				}else if(nombre == "18")
				{
					Player.GetComponent<CrearCartas>().crearPanzer();
				}else if(nombre == "19")
				{
					Player.GetComponent<CrearCartas>().crearVikingo();
				}else if(nombre == "20")
				{
					Player.GetComponent<CrearCartas>().crearTanquePesado();
				}else if(nombre == "21")
				{
					Player.GetComponent<CrearCartas>().crearTorreta();
				}else if(nombre == "22")
				{
					Player.GetComponent<CrearCartas>().crearTorretaMisil();
				}else if(nombre == "23")
				{
					Player.GetComponent<CrearCartas>().crearCampamento();
				}else if(nombre == "24")
				{
					Player.GetComponent<CrearCartas>().crearMina();
				}
				//print("Carta: "+nombre);
			}
			animacion.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "entrada", false);
			transform.localPosition = inicial;
			Player.GetComponent<Hero>().eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(gameObject);
		}
		arrastrar = false;
		//preview = false;
		//cursor.SetActive(false);
	}
	public void Soltar ()
	{
		GetComponent<AudioSource>().volume = efectos;
		GetComponent<AudioSource>().clip = usa;
		GetComponent<AudioSource>().Play();

		//cantidad -= 1;
		//cantidadTotal -= 1;

		Barra.GetComponent<barraOflline>().fill -= costo/100;
		if(nombre == "1")
		{
			Player.GetComponent<CrearCartas>().crearFusil();
			if(manoTeclado != null)
			{
				Destroy(manoTeclado);
			}
			if(manoXbox != null)
			{
				Destroy(manoXbox);
			}
		}else if(nombre == "2")
		{
			Player.GetComponent<CrearCartas>().crearSubmetra();
		}else if(nombre == "3")
		{
			Player.GetComponent<CrearCartas>().crearEscopeta();
		}else if(nombre == "4")
		{
			Player.GetComponent<CrearCartas>().crearMetra();
		}else if(nombre == "5")
		{
			Player.GetComponent<CrearCartas>().crearSniper();
		}else if(nombre == "6")
		{
			Player.GetComponent<CrearCartas>().crearLansaLlamas();
		}else if(nombre == "7")
		{
			Player.GetComponent<CrearCartas>().crearBazooka();
		}else if(nombre == "8")
		{
			Player.GetComponent<CrearCartas>().crearGranada();
		}else if(nombre == "9")
		{
			Player.GetComponent<CrearCartas>().crearSupplies();
		}else if(nombre == "10")
		{
			Player.GetComponent<CrearCartas>().crearFusilero();
		}else if(nombre == "11")
		{
			Player.GetComponent<CrearCartas>().crearSubmetralleto();
		}else if(nombre == "12")
		{
			Player.GetComponent<CrearCartas>().crearEscopeto();
		}else if(nombre == "13")
		{
			Player.GetComponent<CrearCartas>().crearMedico();
		}else if(nombre == "14")
		{
			Player.GetComponent<CrearCartas>().crearMg();
		}else if(nombre == "15")
		{
			Player.GetComponent<CrearCartas>().crearMortero();
		}else if(nombre == "16")
		{
			Player.GetComponent<CrearCartas>().crearMetralleto();
		}else if(nombre == "17")
		{
			//VIEJO DEL LANZA LLAMAS
		}else if(nombre == "18")
		{
			Player.GetComponent<CrearCartas>().crearPanzer();
		}else if(nombre == "19")
		{
			Player.GetComponent<CrearCartas>().crearVikingo();
		}else if(nombre == "20")
		{
			Player.GetComponent<CrearCartas>().crearTanquePesado();
		}else if(nombre == "21")
		{
			Player.GetComponent<CrearCartas>().crearTorreta();
		}else if(nombre == "22")
		{
			Player.GetComponent<CrearCartas>().crearTorretaMisil();
		}else if(nombre == "23")
		{
			Player.GetComponent<CrearCartas>().crearCampamento();
		}else if(nombre == "24")
		{
			Player.GetComponent<CrearCartas>().crearMina();
		}
		//print("Carta: "+nombre);
		animacion.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "entrada", false);
		transform.localPosition = inicial;
		Player.GetComponent<Hero>().eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(gameObject);
	}

	public void Over ()//(PointerEventData eventData)
	{
		if(Barra.GetComponent<barraOflline>().fill*100 >= costo && nombre != "0")
		{
			selected = true;
			animacion.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "mouse", false);
		}
	}

	public void Sale ()//(PointerEventData eventData)
	{
		if(Barra.GetComponent<barraOflline>().fill*100 >= costo && nombre != "0")
		{
			selected = false;
			animacion.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "normal", false);
		}
	}
}
