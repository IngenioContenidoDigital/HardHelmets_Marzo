using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class CrearCartas : MonoBehaviour {

	public GameObject nace;

	public GameObject nace2;

	public GameObject nace3;

	public bool tirar;

	//[SyncVar(hook = "OnChange")]
	//public float valorNace;

	//------BUENOS

	//ARMAS
	public GameObject fusil;
	public GameObject escopeta;
	public GameObject submetra;
	public GameObject metra;
	public GameObject sniper;
	public GameObject granada;
	public GameObject supplies;
	public GameObject lansaLlamas;
	public GameObject bazooka;
	//PERSONAJES NORMALES 
	public GameObject fusilero;
	public GameObject escopeto;
	public GameObject submetro;
	//PERSONAJES FUERTES 
	public GameObject metralleto;
	public GameObject vikingo;
	//PERSONAJES DEFENSA
	public GameObject mg;
	public GameObject mortero;
	public GameObject panzer;
	public GameObject medico;
	//VEHICULOS
	public GameObject tankPesado;
	//OTROS
	public GameObject campamento;
	public GameObject torreta;
	public GameObject torretaMisil;
	public GameObject minaAntiTanque;
	public GameObject minaAntiPersona;

	public bool martillar;
	public bool crear;
	public bool martillaTorreta;
	public bool martillaTorreta2;

	// Use this for initialization
	void Start ()
	{
		
	}
	public bool probando;//BORRAR TODO LO QUE DIGA ESTA VARIABLE
	void Update ()
	{
		if(martillar && crear)
		{
			if(martillaTorreta)
			{
				TorretaBueno();

				martillaTorreta = false;
				martillar = false;
				crear = false;
			}
			if(martillaTorreta2)
			{
				TorretaMisilBueno();

				martillaTorreta2 = false;
				martillar = false;
				crear = false;
			}
		}

		if(tirar)
		{
			StartCoroutine(reactivar());
		}
		if(probando)
		{
			bombardeo();
			probando = false;
		}
		//nace.transform.position = new Vector3(valorNace, -20, transform.position.z);
	}

	IEnumerator reactivar()
	{
		yield return new WaitForSeconds(0.2f);
		tirar = false;
	}

	/*void OnChange(float valorNace)
	{
		nace.transform.position = new Vector3(valorNace, -20, transform.position.z);
	}*/
	//-------------------ARMAS
	//FUSIL
	public void crearFusil()
	{
		FusilBueno();
	}
	
	public void FusilBueno()
	{
		if(!tirar)
		{
			var objeto = (GameObject)Instantiate(fusil, nace.transform.position, transform.rotation);

			tirar = true;
		}
	}
	//ESCOPETA
	public void crearEscopeta()
	{
		EscopetaBueno();
	}
	
	public void EscopetaBueno()
	{
		if(!tirar)
		{
			var objeto = (GameObject)Instantiate(escopeta, nace.transform.position, transform.rotation);

			tirar = true;
		}

	}
	//SUBMETRA
	public void crearSubmetra()
	{
		SubmetraBueno();
	}
	
	public void SubmetraBueno()
	{
		if(!tirar)
		{
			var objeto = (GameObject)Instantiate(submetra, nace.transform.position, transform.rotation);

			tirar = true;
		}

	}
	//METRA
	public void crearMetra()
	{
		MetraBueno();
	}
	
	public void MetraBueno()
	{
		if(!tirar)
		{
			var objeto = (GameObject)Instantiate(metra, nace.transform.position, transform.rotation);

			tirar = true;
		}
	}
	//SNIPER
	public void crearSniper()
	{
		SniperBueno();
	}
	
	public void SniperBueno()
	{
		if(!tirar)
		{
			var objeto = (GameObject)Instantiate(sniper, nace.transform.position, transform.rotation);

			tirar = true;
		}
	}
	//GRANADA
	public void crearGranada()
	{
		GranadaBueno();
	}
	
	public void GranadaBueno()
	{
		if(!tirar)
		{
			var objeto = (GameObject)Instantiate(granada, nace.transform.position, transform.rotation);

			tirar = true;
		}
	}
	//SUPPLIES
	public void crearSupplies()
	{
		SuppliesBueno();
	}
	
	public void SuppliesBueno()
	{
		if(!tirar)
		{
			var objeto = (GameObject)Instantiate(supplies, nace.transform.position, transform.rotation);

			tirar = true;
		}
	}
	//LANSA LLAMAS
	public void crearLansaLlamas()
	{
		LansaLlamasBueno();
	}
	
	public void LansaLlamasBueno()
	{
		if(!tirar)
		{
			var objeto = (GameObject)Instantiate(lansaLlamas, nace.transform.position, transform.rotation);

			tirar = true;
		}
	}
	//PANZER
	public void crearBazooka()
	{
		BazookaBueno();
	}
	
	public void BazookaBueno()
	{
		if(!tirar)
		{
			var objeto = (GameObject)Instantiate(bazooka, nace.transform.position, transform.rotation);

			tirar = true;
		}
	}
	//-------------------PERSONAJES BASICOS
	//FUSILERO
	public void crearFusilero()
	{
		FusileroBueno();
	}
	
	public void FusileroBueno()
	{
		if(!tirar)
		{
			var objeto = (GameObject)Instantiate(fusilero, nace.transform.position, Quaternion.Euler(0,0,0));

			tirar = true;
		}
	}
	//ESCOPETO
	public void crearEscopeto()
	{
		EscopetoBueno();
	}
	
	public void EscopetoBueno()
	{
		if(!tirar)
		{
			var objeto = (GameObject)Instantiate(escopeto, nace.transform.position, Quaternion.Euler(0,0,0));

			tirar = true;
		}
	}
	//SUBMETRALLETO
	public void crearSubmetralleto()
	{
		SubmetralletoBueno();
	}
	
	public void SubmetralletoBueno()
	{
		if(!tirar)
		{
			var objeto = (GameObject)Instantiate(submetro, nace.transform.position, Quaternion.Euler(0,0,0));

			tirar = true;
		}
	}
	//METRALLETO
	public void crearMetralleto()
	{
		MetralletoBueno();
	}
	
	public void MetralletoBueno()
	{
		if(!tirar)
		{
			var objeto = (GameObject)Instantiate(metralleto, nace.transform.position, Quaternion.Euler(0,0,0));

			tirar = true;
		}
	}
	//MEDICO
	public void crearMedico()
	{
		MedicoBueno();
	}
	
	public void MedicoBueno()
	{
		if(!tirar)
		{
			var objeto = (GameObject)Instantiate(medico, nace.transform.position, Quaternion.Euler(0,0,0));

			tirar = true;
		}
	}
	//-------------------PERSOJAES DEFENSA
	//MG
	public void crearMg()
	{
		MgBueno();
	}
	
	public void MgBueno()
	{
		if(!tirar)
		{
			var objeto = (GameObject)Instantiate(mg, nace.transform.position, Quaternion.Euler(0,0,0));

			tirar = true;
		}
	}
	//MORTERO
	public void crearMortero()
	{
		MorteroBueno();
	}
	
	public void MorteroBueno()
	{
		if(!tirar)
		{
			var objeto = (GameObject)Instantiate(mortero, nace.transform.position, Quaternion.Euler(0,0,0));

			tirar = true;
		}
	}
	//PANZER
	public void crearPanzer()
	{
		PanzerBueno();
	}
	
	public void PanzerBueno()
	{
		if(!tirar)
		{
			var objeto = (GameObject)Instantiate(panzer, nace.transform.position, Quaternion.Euler(0,0,0));

			tirar = true;
		}
	}
	//----------VEHICULOS
	//TANQUE LIGERO
	//TANQUE PESADO
	public void crearTanquePesado()
	{
		TanquePesadoBueno();
	}
	
	public void TanquePesadoBueno()
	{
		if(!tirar)
		{
			var objeto = (GameObject)Instantiate(tankPesado, nace.transform.position, Quaternion.Euler(0,90,0));

			tirar = true;
		}
	}
	//VIKINGO
	public void crearVikingo ()
	{
		VikingoBueno();
	}
	
	public void VikingoBueno()
	{
		if(!tirar)
		{
			var objeto = (GameObject)Instantiate(vikingo, nace.transform.position, Quaternion.Euler(0,0,0));

			tirar = true;
		}
	}
	//CAMPAMENTO
	public void crearCampamento ()
	{
		CampamentoBueno();
	}
	
	public void CampamentoBueno()
	{
		if(!tirar)
		{
			var objeto = (GameObject)Instantiate(campamento, nace.transform.position, Quaternion.Euler(0,0,0));

			tirar = true;
		}
	}
	//TORRETA
	public void crearTorreta ()
	{
		hammer = 0;
		GetComponent<Animator>().SetInteger("crear", 1);

		martillar = true;
		martillaTorreta = true;
	}
	
	public void TorretaBueno()
	{
		if(!tirar)
		{
			var objeto = (GameObject)Instantiate(torreta, nace2.transform.position, Quaternion.Euler(0,0,0));

			tirar = true;
		}
	}
	//TORRETA MISIL
	public void crearTorretaMisil ()
	{
		hammer = 0;
		GetComponent<Animator>().SetInteger("crear", 1);

		martillar = true;
		martillaTorreta2 = true;
	}
	
	public void TorretaMisilBueno()
	{
		if(!tirar)
		{
			var objeto = (GameObject)Instantiate(torretaMisil, nace2.transform.position, Quaternion.Euler(0,0,0));

			tirar = true;
		}
	}
	//MINA
	public int minas;
	public void crearMina ()
	{
		minas = 1;
		GetComponent<Animator>().SetInteger("crear", 2);
	}
	public void crearMinaPersona ()
	{
		minas = 2;
		GetComponent<Animator>().SetInteger("crear", 2);
	}
	public void mina()
	{
		if(minas == 1)
		{
			var objeto = (GameObject)Instantiate(minaAntiTanque, nace3.transform.position, Quaternion.Euler(0,0,0));
			objeto.GetComponent<MinaOffline>().poder = GetComponent<Hero>().saludMax*objeto.GetComponent<MinaOffline>().poder/104;

			minas = 0;
		}
		if(minas == 2)
		{
			var objeto = (GameObject)Instantiate(minaAntiPersona, nace3.transform.position, Quaternion.Euler(0,Random.Range(0,360),0));
			objeto.GetComponent<minaAntipersona>().poder = GetComponent<Hero>().saludMax*objeto.GetComponent<minaAntipersona>().poder/104;

			minas = 0;
		}
	}

	int hammer;
	//OBJETOS CON MARTILLOS
	public void HAMMER ()
	{
		hammer += 1;
		if(hammer == 4)
		{
			crear = true;
			hammer = 0;
		}
	}
	//BOMBARDEO
	public void bombardeo()
	{
		GetComponent<Hero>().ready = false;

		GetComponent<Hero>().caminarA = false;
		GetComponent<Hero>().caminarD = false;
		GetComponent<Hero>().caminarI = false;
		GetComponent<Hero>().caminarU = false;

		GetComponent<Hero>().v3 = Vector3.zero;

		GetComponent<Hero>().rafaga = true;
		GetComponent<Hero>().cuchillo = false;

		GetComponent<Hero>().SniperCam.GetComponent<Cam>().bombardeo = true;

		GetComponent<Hero>().esconderBarra.SetActive(false);
		GetComponent<Hero>().SniperCam.GetComponent<Grayscale>().enabled = true;
		//GetComponent<Hero>().SniperCam.GetComponent<LensAberrations>().distortion.enabled = true;
		//animator.SetInteger("disparo",20);
	}
	//ATAQUE AEREO
	public void aereo()
	{
		print("LLAMAR AL AVION");
		GetComponent<Hero>().granadahumo = true;
		GetComponent<Hero>().avion = true;
	}
}
