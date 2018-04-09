using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrearCartas : MonoBehaviour {

	public GameObject nace;

	public GameObject nace2;

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
	public GameObject mina;

	public bool martillar;
	public bool crear;
	public bool martillaTorreta;
	public bool martillaTorreta2;
	public bool martillaMina;

	// Use this for initialization
	void Start () {

	}

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
			if(martillaMina)
			{
				crearMinaBueno();

				martillaMina = false;
				martillar = false;
				crear = false;
			}
		}
		//nace.transform.position = new Vector3(valorNace, -20, transform.position.z);
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
		var objeto = (GameObject)Instantiate(fusil, nace.transform.position, transform.rotation);

	}
	//ESCOPETA
	public void crearEscopeta()
	{
		EscopetaBueno();
	}
	
	public void EscopetaBueno()
	{
		var objeto = (GameObject)Instantiate(escopeta, nace.transform.position, transform.rotation);

	}
	//SUBMETRA
	public void crearSubmetra()
	{
		SubmetraBueno();
	}
	
	public void SubmetraBueno()
	{
		var objeto = (GameObject)Instantiate(submetra, nace.transform.position, transform.rotation);

	}
	//METRA
	public void crearMetra()
	{
		MetraBueno();
	}
	
	public void MetraBueno()
	{
		var objeto = (GameObject)Instantiate(metra, nace.transform.position, transform.rotation);
	}
	//SNIPER
	public void crearSniper()
	{
		SniperBueno();
	}
	
	public void SniperBueno()
	{
		var objeto = (GameObject)Instantiate(sniper, nace.transform.position, transform.rotation);
	}
	//GRANADA
	public void crearGranada()
	{
		GranadaBueno();
	}
	
	public void GranadaBueno()
	{
		var objeto = (GameObject)Instantiate(granada, nace.transform.position, transform.rotation);
	}
	//SUPPLIES
	public void crearSupplies()
	{
		SuppliesBueno();
	}
	
	public void SuppliesBueno()
	{
		var objeto = (GameObject)Instantiate(supplies, nace.transform.position, transform.rotation);
	}
	//LANSA LLAMAS
	public void crearLansaLlamas()
	{
		LansaLlamasBueno();
	}
	
	public void LansaLlamasBueno()
	{
		var objeto = (GameObject)Instantiate(lansaLlamas, nace.transform.position, transform.rotation);
	}
	//PANZER
	public void crearBazooka()
	{
		BazookaBueno();
	}
	
	public void BazookaBueno()
	{
		var objeto = (GameObject)Instantiate(bazooka, nace.transform.position, transform.rotation);
	}
	//-------------------PERSONAJES BASICOS
	//FUSILERO
	public void crearFusilero()
	{
		FusileroBueno();
	}
	
	public void FusileroBueno()
	{
		var objeto = (GameObject)Instantiate(fusilero, nace.transform.position, Quaternion.Euler(0,0,0));
	}
	//ESCOPETO
	public void crearEscopeto()
	{
		EscopetoBueno();
	}
	
	public void EscopetoBueno()
	{
		var objeto = (GameObject)Instantiate(escopeto, nace.transform.position, Quaternion.Euler(0,0,0));

	}
	//SUBMETRALLETO
	public void crearSubmetralleto()
	{
		SubmetralletoBueno();
	}
	
	public void SubmetralletoBueno()
	{
		var objeto = (GameObject)Instantiate(submetro, nace.transform.position, Quaternion.Euler(0,0,0));
	}
	//METRALLETO
	public void crearMetralleto()
	{
		MetralletoBueno();
	}
	
	public void MetralletoBueno()
	{
		var objeto = (GameObject)Instantiate(metralleto, nace.transform.position, Quaternion.Euler(0,0,0));
	}
	//MEDICO
	public void crearMedico()
	{
		MedicoBueno();
	}
	
	public void MedicoBueno()
	{
		var objeto = (GameObject)Instantiate(medico, nace.transform.position, Quaternion.Euler(0,0,0));
	}
	//-------------------PERSOJAES DEFENSA
	//MG
	public void crearMg()
	{
		MgBueno();
	}
	
	public void MgBueno()
	{
		var objeto = (GameObject)Instantiate(mg, nace.transform.position, Quaternion.Euler(0,0,0));
	}
	//MORTERO
	public void crearMortero()
	{
		MorteroBueno();
	}
	
	public void MorteroBueno()
	{
		var objeto = (GameObject)Instantiate(mortero, nace.transform.position, Quaternion.Euler(0,0,0));
	}
	//PANZER
	public void crearPanzer()
	{
		PanzerBueno();
	}
	
	public void PanzerBueno()
	{
		var objeto = (GameObject)Instantiate(panzer, nace.transform.position, Quaternion.Euler(0,0,0));
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
		var objeto = (GameObject)Instantiate(tankPesado, nace.transform.position, Quaternion.Euler(0,90,0));
	}
	//VIKINGO
	public void crearVikingo ()
	{
		VikingoBueno();
	}
	
	public void VikingoBueno()
	{
		var objeto = (GameObject)Instantiate(vikingo, nace.transform.position, Quaternion.Euler(0,0,0));
	}
	//CAMPAMENTO
	public void crearCampamento ()
	{
		CampamentoBueno();
	}
	
	public void CampamentoBueno()
	{
		var objeto = (GameObject)Instantiate(campamento, nace.transform.position, Quaternion.Euler(0,0,0));
	}
	//TORRETA
	public void crearTorreta ()
	{
		hammer = 0;
		GetComponent<Animator>().SetBool("crear", true);

		martillar = true;
		martillaTorreta = true;
	}
	
	public void TorretaBueno()
	{
		var objeto = (GameObject)Instantiate(torreta, nace2.transform.position, Quaternion.Euler(0,0,0));
	}
	//TORRETA MISIL
	public void crearTorretaMisil ()
	{
		hammer = 0;
		GetComponent<Animator>().SetBool("crear", true);

		martillar = true;
		martillaTorreta2 = true;
	}
	
	public void TorretaMisilBueno()
	{
		var objeto = (GameObject)Instantiate(torretaMisil, nace2.transform.position, Quaternion.Euler(0,0,0));
	}
	//MINA
	public void crearMina ()
	{
		hammer = 0;
		GetComponent<Animator>().SetBool("crear", true);

		martillar = true;
		martillaMina = true;
	}
	
	public void crearMinaBueno()
	{
		var objeto = (GameObject)Instantiate(mina, nace2.transform.position, Quaternion.Euler(0,0,0));
		objeto.GetComponent<mina>().poder = GetComponent<HeroNetwork>().saludMax*objeto.GetComponent<mina>().poder/104;
		print(GetComponent<HeroNetwork>().saludMax*objeto.GetComponent<mina>().poder/104);
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
}
