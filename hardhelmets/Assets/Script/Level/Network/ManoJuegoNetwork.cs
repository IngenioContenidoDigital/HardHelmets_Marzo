using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Spine.Unity;
using UnityEngine.Networking;

public class ManoJuegoNetwork : NetworkBehaviour {

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
	public Sprite a25;

	float efectos;
	public AudioClip nace;
	public AudioClip usa;

	//CONTROL
	public bool selected;
	public bool listo;

	public GameObject amiga1;
	public GameObject amiga2;
	public GameObject amiga3;
	public GameObject amiga4;
	public GameObject amiga5;
	public GameObject amiga6;
	public GameObject amiga7;
	public GameObject amiga8;
	public GameObject amiga9;

	// Use this for initialization
	void Start ()
	{
		inicial = transform.localPosition;

		nombre2 = nombre;
		//cantidad = PlayerPrefs.GetInt("Mano"+nombre+"cantidad");

		efectos = PlayerPrefs.GetFloat("efects");

		nombre = PlayerPrefs.GetInt("Mano"+nombre).ToString();
		costo = PlayerPrefs.GetInt(""+nombre+"costo");

		animacion.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "inactiva", false);

		//UNO
		if(nombre == "1")
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
		}else if(nombre == "25")
		{
			animacion.GetComponent<skinCarta>().skinsToCombine[0] = "25";
			Load.sprite = a25;
		}
		//cantidadTotal = PlayerPrefs.GetInt("card"+nombre+"cantidad");
	}
	/*bool entrance;
	bool animandoentrada;
	bool exit;
	bool animandosalida;*/
	string nomm;
	// Update is called once per frame
	public bool completa;
	void Update ()
	{

		Load.fillAmount = 1-Barra.GetComponent<barra>().fill*100/costo;

		if(Load.fillAmount <= 0)
		{
			Load.GetComponent<Image>().enabled = false;
		}else //if(cantidad >= 1)
		{
			Load.GetComponent<Image>().enabled = true;
		}
		/*cantidadT.text = cantidad.ToString();

		if(cantidadTotal <= 0)
		{
			cantidadTotal = 0;
			PlayerPrefs.SetInt("card"+nombre, 0);
		}
		PlayerPrefs.SetInt("card"+nombre+"cantidad", cantidadTotal);

		PlayerPrefs.SetInt("Mano"+nombre2+"cantidad", cantidad);

		if(cantidad <= 0)
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
		if(Barra.GetComponent<barra>().fill*100 >= costo && nomm == "inactiva")// && cantidad >= 1)
		{
			GetComponent<AudioSource>().volume = efectos;
			GetComponent<AudioSource>().clip = nace;
			GetComponent<AudioSource>().Play();

			animacion.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "entrada", false);
			GetComponent<Button>().enabled = true;
			GetComponent<Image>().color = new Color32(255,255,255,255);

			if(Player.GetComponent<HeroNetwork>().salud > 0 && !amiga1.GetComponent<ManoJuegoNetwork>().selected && !amiga2.GetComponent<ManoJuegoNetwork>().selected
				&& !amiga3.GetComponent<ManoJuegoNetwork>().selected && !amiga4.GetComponent<ManoJuegoNetwork>().selected && !amiga5.GetComponent<ManoJuegoNetwork>().selected
				&& !amiga6.GetComponent<ManoJuegoNetwork>().selected && !amiga7.GetComponent<ManoJuegoNetwork>().selected && !amiga8.GetComponent<ManoJuegoNetwork>().selected
				&& !amiga9.GetComponent<ManoJuegoNetwork>().selected)
			{
				Player.GetComponent<HeroNetwork>().eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(gameObject);
			}
			completa = true;
			listo = true;
		}
		if(Barra.GetComponent<barra>().fill*100 < costo)// && cantidad >= 1)
		{
			GetComponent<Button>().enabled = false;
			GetComponent<Image>().color = new Color32(100,100,100,100);

			if(nomm != "inactiva")
			{
				animacion.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "inactiva", false);
			}
			completa = false;
			selected = false;
		}else
		{
			GetComponent<Button>().enabled = true;
			GetComponent<Image>().color = new Color32(255,255,255,255);
		}
		if(costo <= 0)
		{
			completa = false;
		}

		/*if(Player.GetComponent<HeroNetwork>().salud > 0 && Input.GetAxis("Horizontal") > 0 && !selected &&  !amiga1.GetComponent<ManoJuegoNetwork>().selected && !amiga2.GetComponent<ManoJuegoNetwork>().selected
			&& !amiga3.GetComponent<ManoJuegoNetwork>().selected && !amiga4.GetComponent<ManoJuegoNetwork>().selected && !amiga5.GetComponent<ManoJuegoNetwork>().selected
			&& !amiga6.GetComponent<ManoJuegoNetwork>().selected && !amiga7.GetComponent<ManoJuegoNetwork>().selected && !amiga8.GetComponent<ManoJuegoNetwork>().selected
			&& !amiga9.GetComponent<ManoJuegoNetwork>().selected)
		{
			if(Player.GetComponent<HeroNetwork>().salud > 0 && Barra.GetComponent<barra>().fill*100 >= costo)
			{
				Player.GetComponent<HeroNetwork>().eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(gameObject);
			}if(Player.GetComponent<HeroNetwork>().salud > 0 && Barra.GetComponent<barra>().fill*100 >= amiga1.GetComponent<ManoJuegoNetwork>().costo)
			{
				Player.GetComponent<HeroNetwork>().eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(amiga1);
			}else if(Player.GetComponent<HeroNetwork>().salud > 0 && Barra.GetComponent<barra>().fill*100 >= amiga2.GetComponent<ManoJuegoNetwork>().costo)
			{
				Player.GetComponent<HeroNetwork>().eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(amiga2);
			}else if(Player.GetComponent<HeroNetwork>().salud > 0 && Barra.GetComponent<barra>().fill*100 >= amiga3.GetComponent<ManoJuegoNetwork>().costo)
			{
				Player.GetComponent<HeroNetwork>().eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(amiga3);
			}else if(Player.GetComponent<HeroNetwork>().salud > 0 && Barra.GetComponent<barra>().fill*100 >= amiga4.GetComponent<ManoJuegoNetwork>().costo)
			{
				Player.GetComponent<HeroNetwork>().eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(amiga4);
			}else if(Player.GetComponent<HeroNetwork>().salud > 0 && Barra.GetComponent<barra>().fill*100 >= amiga5.GetComponent<ManoJuegoNetwork>().costo)
			{
				Player.GetComponent<HeroNetwork>().eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(amiga5);
			}else if(Player.GetComponent<HeroNetwork>().salud > 0 && Barra.GetComponent<barra>().fill*100 >= amiga6.GetComponent<ManoJuegoNetwork>().costo)
			{
				Player.GetComponent<HeroNetwork>().eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(amiga6);
			}else if(Player.GetComponent<HeroNetwork>().salud > 0 && Barra.GetComponent<barra>().fill*100 >= amiga7.GetComponent<ManoJuegoNetwork>().costo)
			{
				Player.GetComponent<HeroNetwork>().eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(amiga7);
			}else if(Player.GetComponent<HeroNetwork>().salud > 0 && Barra.GetComponent<barra>().fill*100 >= amiga8.GetComponent<ManoJuegoNetwork>().costo)
			{
				Player.GetComponent<HeroNetwork>().eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(amiga8);
			}else if(Player.GetComponent<HeroNetwork>().salud > 0 && Barra.GetComponent<barra>().fill*100 >= amiga9.GetComponent<ManoJuegoNetwork>().costo)
			{
				Player.GetComponent<HeroNetwork>().eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(amiga9);
			}
		}*/

		if(selected)
		{
			amiga1.GetComponent<ManoJuegoNetwork>().selected = false;
			amiga2.GetComponent<ManoJuegoNetwork>().selected = false;
			amiga3.GetComponent<ManoJuegoNetwork>().selected = false;
			amiga4.GetComponent<ManoJuegoNetwork>().selected = false;
			amiga5.GetComponent<ManoJuegoNetwork>().selected = false;
			amiga6.GetComponent<ManoJuegoNetwork>().selected = false;
			amiga7.GetComponent<ManoJuegoNetwork>().selected = false;
			amiga8.GetComponent<ManoJuegoNetwork>().selected = false;
			amiga9.GetComponent<ManoJuegoNetwork>().selected = false;
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

		if(Barra.GetComponent<barra>().fill*100 >= costo)
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

				Barra.GetComponent<barra>().fill -= costo/100;
				if(nombre == "1")
				{
					Player.GetComponent<CrearCartasNetwork>().crearFusil();
				}else if(nombre == "2")
				{
					Player.GetComponent<CrearCartasNetwork>().crearSubmetra();
				}else if(nombre == "3")
				{
					Player.GetComponent<CrearCartasNetwork>().crearEscopeta();
				}else if(nombre == "4")
				{
					Player.GetComponent<CrearCartasNetwork>().crearMetra();
				}else if(nombre == "5")
				{
					Player.GetComponent<CrearCartasNetwork>().crearSniper();
				}else if(nombre == "6")
				{
					Player.GetComponent<CrearCartasNetwork>().crearLansaLlamas();
				}else if(nombre == "7")
				{
					Player.GetComponent<CrearCartasNetwork>().crearBazooka();
				}else if(nombre == "8")
				{
					Player.GetComponent<CrearCartasNetwork>().crearGranada();
				}else if(nombre == "9")
				{
					Player.GetComponent<CrearCartasNetwork>().crearSupplies();
					//Player.GetComponent<CrearCartasNetwork>().valorNace = hit.point.x;
				}else if(nombre == "10")
				{
					Player.GetComponent<CrearCartasNetwork>().crearFusilero();
				}else if(nombre == "11")
				{
					Player.GetComponent<CrearCartasNetwork>().crearSubmetralleto();
				}else if(nombre == "12")
				{
					Player.GetComponent<CrearCartasNetwork>().crearEscopeto();
				}else if(nombre == "13")
				{
					Player.GetComponent<CrearCartasNetwork>().crearMedico();
				}else if(nombre == "14")
				{
					Player.GetComponent<CrearCartasNetwork>().crearMg();
				}else if(nombre == "15")
				{
					Player.GetComponent<CrearCartasNetwork>().crearMortero();
				}else if(nombre == "16")
				{
					Player.GetComponent<CrearCartasNetwork>().crearMetralleto();
				}else if(nombre == "17")
				{
					Player.GetComponent<CrearCartasNetwork>().crearMinaPersona();
				}else if(nombre == "18")
				{
					Player.GetComponent<CrearCartasNetwork>().crearPanzer();
				}else if(nombre == "19")
				{
					Player.GetComponent<CrearCartasNetwork>().crearVikingo();
				}else if(nombre == "20")
				{
					Player.GetComponent<CrearCartasNetwork>().crearTanquePesado();
				}else if(nombre == "21")
				{
					Player.GetComponent<CrearCartasNetwork>().crearTorreta();
				}else if(nombre == "22")
				{
					Player.GetComponent<CrearCartasNetwork>().crearTorretaMisil();
				}else if(nombre == "23")
				{
					Player.GetComponent<CrearCartasNetwork>().crearCampamento();
				}else if(nombre == "24")
				{
					Player.GetComponent<CrearCartasNetwork>().crearMina();
				}else if(nombre == "25")
				{
					Player.GetComponent<CrearCartasNetwork>().bombardeo();
				}
				//print("Carta: "+nombre);
			}
			animacion.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "entrada", false);
			transform.localPosition = inicial;
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

		Barra.GetComponent<barra>().fill -= costo/100;
		if(nombre == "1")
		{
			Player.GetComponent<CrearCartasNetwork>().crearFusil();
		}else if(nombre == "2")
		{
			Player.GetComponent<CrearCartasNetwork>().crearSubmetra();
		}else if(nombre == "3")
		{
			Player.GetComponent<CrearCartasNetwork>().crearEscopeta();
		}else if(nombre == "4")
		{
			Player.GetComponent<CrearCartasNetwork>().crearMetra();
		}else if(nombre == "5")
		{
			Player.GetComponent<CrearCartasNetwork>().crearSniper();
		}else if(nombre == "6")
		{
			Player.GetComponent<CrearCartasNetwork>().crearLansaLlamas();
		}else if(nombre == "7")
		{
			Player.GetComponent<CrearCartasNetwork>().crearBazooka();
		}else if(nombre == "8")
		{
			Player.GetComponent<CrearCartasNetwork>().crearGranada();
		}else if(nombre == "9")
		{
			Player.GetComponent<CrearCartasNetwork>().crearSupplies();
		}else if(nombre == "10")
		{
			Player.GetComponent<CrearCartasNetwork>().crearFusilero();
		}else if(nombre == "11")
		{
			Player.GetComponent<CrearCartasNetwork>().crearSubmetralleto();
		}else if(nombre == "12")
		{
			Player.GetComponent<CrearCartasNetwork>().crearEscopeto();
		}else if(nombre == "13")
		{
			Player.GetComponent<CrearCartasNetwork>().crearMedico();
		}else if(nombre == "14")
		{
			Player.GetComponent<CrearCartasNetwork>().crearMg();
		}else if(nombre == "15")
		{
			Player.GetComponent<CrearCartasNetwork>().crearMortero();
		}else if(nombre == "16")
		{
			Player.GetComponent<CrearCartasNetwork>().crearMetralleto();
		}else if(nombre == "17")
		{
			Player.GetComponent<CrearCartasNetwork>().crearMinaPersona();
		}else if(nombre == "18")
		{
			Player.GetComponent<CrearCartasNetwork>().crearPanzer();
		}else if(nombre == "19")
		{
			Player.GetComponent<CrearCartasNetwork>().crearVikingo();
		}else if(nombre == "20")
		{
			Player.GetComponent<CrearCartasNetwork>().crearTanquePesado();
		}else if(nombre == "21")
		{
			Player.GetComponent<CrearCartasNetwork>().crearTorreta();
		}else if(nombre == "22")
		{
			Player.GetComponent<CrearCartasNetwork>().crearTorretaMisil();
		}else if(nombre == "23")
		{
			Player.GetComponent<CrearCartasNetwork>().crearCampamento();
		}else if(nombre == "24")
		{
			Player.GetComponent<CrearCartasNetwork>().crearMina();
		}else if(nombre == "25")
		{
			Player.GetComponent<CrearCartasNetwork>().bombardeo();
		}
		//print("Carta: "+nombre);
		animacion.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "entrada", false);
		transform.localPosition = inicial;

		StartCoroutine(momentoselect());
	}

	IEnumerator momentoselect()
	{
		yield return new WaitForSeconds(0.5f);

		if(Player.GetComponent<HeroNetwork>().salud > 0 && Barra.GetComponent<barra>().fill*100 >= costo)
		{
			Player.GetComponent<HeroNetwork>().eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(gameObject);
		}if(Player.GetComponent<HeroNetwork>().salud > 0 && Barra.GetComponent<barra>().fill*100 >= amiga1.GetComponent<ManoJuegoNetwork>().costo)
		{
			Player.GetComponent<HeroNetwork>().eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(amiga1);
		}else if(Player.GetComponent<HeroNetwork>().salud > 0 && Barra.GetComponent<barra>().fill*100 >= amiga2.GetComponent<ManoJuegoNetwork>().costo)
		{
			Player.GetComponent<HeroNetwork>().eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(amiga2);
		}else if(Player.GetComponent<HeroNetwork>().salud > 0 && Barra.GetComponent<barra>().fill*100 >= amiga3.GetComponent<ManoJuegoNetwork>().costo)
		{
			Player.GetComponent<HeroNetwork>().eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(amiga3);
		}else if(Player.GetComponent<HeroNetwork>().salud > 0 && Barra.GetComponent<barra>().fill*100 >= amiga4.GetComponent<ManoJuegoNetwork>().costo)
		{
			Player.GetComponent<HeroNetwork>().eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(amiga4);
		}else if(Player.GetComponent<HeroNetwork>().salud > 0 && Barra.GetComponent<barra>().fill*100 >= amiga5.GetComponent<ManoJuegoNetwork>().costo)
		{
			Player.GetComponent<HeroNetwork>().eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(amiga5);
		}else if(Player.GetComponent<HeroNetwork>().salud > 0 && Barra.GetComponent<barra>().fill*100 >= amiga6.GetComponent<ManoJuegoNetwork>().costo)
		{
			Player.GetComponent<HeroNetwork>().eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(amiga6);
		}else if(Player.GetComponent<HeroNetwork>().salud > 0 && Barra.GetComponent<barra>().fill*100 >= amiga7.GetComponent<ManoJuegoNetwork>().costo)
		{
			Player.GetComponent<HeroNetwork>().eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(amiga7);
		}else if(Player.GetComponent<HeroNetwork>().salud > 0 && Barra.GetComponent<barra>().fill*100 >= amiga8.GetComponent<ManoJuegoNetwork>().costo)
		{
			Player.GetComponent<HeroNetwork>().eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(amiga8);
		}else if(Player.GetComponent<HeroNetwork>().salud > 0 && Barra.GetComponent<barra>().fill*100 >= amiga9.GetComponent<ManoJuegoNetwork>().costo)
		{
			Player.GetComponent<HeroNetwork>().eventsystem.GetComponent<EventSystem>().SetSelectedGameObject(amiga9);
		}
	}

	public void Over ()//(PointerEventData eventData)
	{
		if(Barra.GetComponent<barra>().fill*100 >= costo && nombre != "0")
		{
			selected = true;
			animacion.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "mouse", false);
		}
	}

	public void Sale ()//(PointerEventData eventData)
	{
		if(Barra.GetComponent<barra>().fill*100 >= costo && nombre != "0")
		{
			selected = false;
			animacion.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "normal", false);
		}
	}
}
