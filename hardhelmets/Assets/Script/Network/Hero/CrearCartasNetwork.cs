using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.ImageEffects;

public class CrearCartasNetwork : NetworkBehaviour {

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
	//ESPECIALES
	public GameObject misiles;
	//------MALOS

	//PERSONAJES NORMALES 
	public GameObject fusileroMalo;
	public GameObject escopetoMalo;
	public GameObject submetroMalo;
	//PERSONAJES FUERTES 
	public GameObject metralletoMalo;
	public GameObject vikingoMalo;
	//PERSONAJES DEFENSA
	public GameObject mgMalo;
	public GameObject morteroMalo;
	public GameObject panzerMalo;
	public GameObject medicoMalo;
	//VEHICULOS
	public GameObject tankPesadoMalo;
	//OTROS
	public GameObject campamentoMalo;
	public GameObject torretaMalo;
	public GameObject torretaMisilMalo;
	public GameObject minaAntiTanqueMalo;
	public GameObject minaAntiPersonaMalo;
	//ESPECIALES
	public GameObject misilesMalo;

	public bool martillar;
	public bool crear;
	public bool martillaTorreta;
	public bool martillaTorreta2;

	// Use this for initialization
	void Start () {
		
	}

	void Update ()
	{
		if(!isLocalPlayer)
		{
			return;
		}

		if(martillar && crear)
		{
			if(martillaTorreta)
			{
				if(gameObject.tag == "Player")
				{
					CmdTorretaBueno();
				}else
				{
					CmdTorretaMalo();
				}

				martillaTorreta = false;
				martillar = false;
				crear = false;
			}
			if(martillaTorreta2)
			{
				if(gameObject.tag == "Player")
				{
					CmdTorretaMisilBueno();
				}else
				{
					CmdTorretaMisilMalo();
				}

				martillaTorreta2 = false;
				martillar = false;
				crear = false;
			}
		}
		if(tirar)
		{
			StartCoroutine(reactivar());
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
		if(!tirar)
		{
			CmdFusilBueno();

			tirar = true;
		}
	}
	[Command]
	public void CmdFusilBueno()
	{
		var objeto = (GameObject)Instantiate(fusil, nace.transform.position, transform.rotation);
		NetworkServer.Spawn(objeto);
	}
	//ESCOPETA
	public void crearEscopeta()
	{
		if(!tirar)
		{
			CmdEscopetaBueno();

			tirar = true;
		}
	}
	[Command]
	public void CmdEscopetaBueno()
	{
		var objeto = (GameObject)Instantiate(escopeta, nace.transform.position, transform.rotation);
		NetworkServer.Spawn(objeto);
	}
	//SUBMETRA
	public void crearSubmetra()
	{
		if(!tirar)
		{
			CmdSubmetraBueno();

			tirar = true;
		}
	}
	[Command]
	public void CmdSubmetraBueno()
	{
		var objeto = (GameObject)Instantiate(submetra, nace.transform.position, transform.rotation);
		NetworkServer.Spawn(objeto);
	}
	//METRA
	public void crearMetra()
	{
		if(!tirar)
		{
			CmdMetraBueno();

			tirar = true;
		}
	}
	[Command]
	public void CmdMetraBueno()
	{
		var objeto = (GameObject)Instantiate(metra, nace.transform.position, transform.rotation);
		NetworkServer.Spawn(objeto);
	}
	//SNIPER
	public void crearSniper()
	{
		if(!tirar)
		{
			CmdSniperBueno();

			tirar = true;
		}
	}
	[Command]
	public void CmdSniperBueno()
	{
		var objeto = (GameObject)Instantiate(sniper, nace.transform.position, transform.rotation);
		NetworkServer.Spawn(objeto);
	}
	//GRANADA
	public void crearGranada()
	{
		if(!tirar)
		{
			CmdGranadaBueno();

			tirar = true;
		}
	}
	[Command]
	public void CmdGranadaBueno()
	{
		var objeto = (GameObject)Instantiate(granada, nace.transform.position, transform.rotation);
		NetworkServer.Spawn(objeto);
	}
	//SUPPLIES
	public void crearSupplies()
	{
		if(!tirar)
		{
			CmdSuppliesBueno();

			tirar = true;
		}
	}
	[Command]
	public void CmdSuppliesBueno()
	{
		var objeto = (GameObject)Instantiate(supplies, nace.transform.position, transform.rotation);
		NetworkServer.Spawn(objeto);
	}
	//LANSA LLAMAS
	public void crearLansaLlamas()
	{
		if(!tirar)
		{
			CmdLansaLlamasBueno();

			tirar = true;
		}
	}
	[Command]
	public void CmdLansaLlamasBueno()
	{
		var objeto = (GameObject)Instantiate(lansaLlamas, nace.transform.position, transform.rotation);
		NetworkServer.Spawn(objeto);
	}
	//PANZER
	public void crearBazooka()
	{
		if(!tirar)
		{
			CmdBazookaBueno();

			tirar = true;
		}
	}
	[Command]
	public void CmdBazookaBueno()
	{
		var objeto = (GameObject)Instantiate(bazooka, nace.transform.position, transform.rotation);
		NetworkServer.Spawn(objeto);
	}
	//-------------------PERSONAJES BASICOS
	//FUSILERO
	public void crearFusilero()
	{
		if(!tirar)
		{
			if(gameObject.tag == "Player")
			{
				CmdFusileroBueno();
			}else
			{
				CmdFusileroMalo();
			}

			tirar = true;
		}
	}
	[Command]
	public void CmdFusileroBueno()
	{
		var objeto = (GameObject)Instantiate(fusilero, nace.transform.position, Quaternion.Euler(0,0,0));
		//objeto.transform.position = new Vector3(valorNace,-20, gameObject.transform.position.z);
		NetworkServer.Spawn(objeto);
	}
	[Command]
	public void CmdFusileroMalo()
	{
		var objeto = (GameObject)Instantiate(fusileroMalo, nace.transform.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(objeto);
		//objeto.transform.position = new Vector3(valorNace,-20, gameObject.transform.position.z);
	}
	//ESCOPETO
	public void crearEscopeto()
	{
		if(!tirar)
		{
			if(gameObject.tag == "Player")
			{
				CmdEscopetoBueno();
			}else
			{
				CmdEscopetoMalo();
			}

			tirar = true;
		}
	}
	[Command]
	public void CmdEscopetoBueno()
	{
		var objeto = (GameObject)Instantiate(escopeto, nace.transform.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(objeto);
	}
	[Command]
	public void CmdEscopetoMalo()
	{
		var objeto = (GameObject)Instantiate(escopetoMalo, nace.transform.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(objeto);
	}
	//SUBMETRALLETO
	public void crearSubmetralleto()
	{
		if(!tirar)
		{
			if(gameObject.tag == "Player")
			{
				CmdSubmetralletoBueno();
			}else
			{
				CmdSubmetralletoMalo();
			}

			tirar = true;
		}
	}
	[Command]
	public void CmdSubmetralletoBueno()
	{
		var objeto = (GameObject)Instantiate(submetro, nace.transform.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(objeto);
	}
	[Command]
	public void CmdSubmetralletoMalo()
	{
		var objeto = (GameObject)Instantiate(submetroMalo, nace.transform.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(objeto);
	}
	//METRALLETO
	public void crearMetralleto()
	{
		if(!tirar)
		{
			if(gameObject.tag == "Player")
			{
				CmdMetralletoBueno();
			}else
			{
				CmdMetralletoMalo();
			}

			tirar = true;
		}
	}
	[Command]
	public void CmdMetralletoBueno()
	{
		var objeto = (GameObject)Instantiate(metralleto, nace.transform.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(objeto);
	}
	[Command]
	public void CmdMetralletoMalo()
	{
		var objeto = (GameObject)Instantiate(metralletoMalo, nace.transform.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(objeto);
	}
	//MEDICO
	public void crearMedico()
	{
		if(!tirar)
		{
			if(gameObject.tag == "Player")
			{
				CmdMedicoBueno();
			}else
			{
				CmdMedicoMalo();
			}

			tirar = true;
		}
	}
	[Command]
	public void CmdMedicoBueno()
	{
		var objeto = (GameObject)Instantiate(medico, nace.transform.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(objeto);
	}
	[Command]
	public void CmdMedicoMalo()
	{
		var objeto = (GameObject)Instantiate(medicoMalo, nace.transform.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(objeto);
	}
	//-------------------PERSOJAES DEFENSA
	//MG
	public void crearMg()
	{
		if(!tirar)
		{
			if(gameObject.tag == "Player")
			{
				CmdMgBueno();
			}else
			{
				CmdMgMalo();
			}

			tirar = true;
		}
	}
	[Command]
	public void CmdMgBueno()
	{
		var objeto = (GameObject)Instantiate(mg, nace.transform.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(objeto);
	}
	[Command]
	public void CmdMgMalo()
	{
		var objeto = (GameObject)Instantiate(mgMalo, nace.transform.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(objeto);
	}
	//MORTERO
	public void crearMortero()
	{
		if(!tirar)
		{
			if(gameObject.tag == "Player")
			{
				CmdMorteroBueno();
			}else
			{
				CmdMorteroMalo();
			}

			tirar = true;
		}
	}
	[Command]
	public void CmdMorteroBueno()
	{
		var objeto = (GameObject)Instantiate(mortero, nace.transform.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(objeto);
	}
	[Command]
	public void CmdMorteroMalo()
	{
		var objeto = (GameObject)Instantiate(morteroMalo, nace.transform.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(objeto);
	}
	//PANZER
	public void crearPanzer()
	{
		if(!tirar)
		{
			if(gameObject.tag == "Player")
			{
				CmdPanzerBueno();
			}else
			{
				CmdPanzerMalo();
			}

			tirar = true;
		}
	}
	[Command]
	public void CmdPanzerBueno()
	{
		var objeto = (GameObject)Instantiate(panzer, nace.transform.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(objeto);
	}
	[Command]
	public void CmdPanzerMalo()
	{
		var objeto = (GameObject)Instantiate(panzerMalo, nace.transform.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(objeto);
	}
	//----------VEHICULOS
	//TANQUE LIGERO
	//TANQUE PESADO
	public void crearTanquePesado()
	{
		if(!tirar)
		{
			if(gameObject.tag == "Player")
			{
				CmdTanquePesadoBueno();
			}else
			{
				CmdTanquePesadoMalo();
			}

			tirar = true;
		}
	}
	[Command]
	public void CmdTanquePesadoBueno()
	{
		var objeto = (GameObject)Instantiate(tankPesado, nace.transform.position, Quaternion.Euler(0,90,0));
		NetworkServer.Spawn(objeto);
	}
	[Command]
	public void CmdTanquePesadoMalo()
	{
		var objeto = (GameObject)Instantiate(tankPesadoMalo, nace.transform.position, Quaternion.Euler(0,-90,0));
		NetworkServer.Spawn(objeto);
	}
	//VIKINGO
	public void crearVikingo ()
	{
		if(!tirar)
		{
			if(gameObject.tag == "Player")
			{
				CmdVikingoBueno();
			}else
			{
				CmdVikingoMalo();
			}

			tirar = true;
		}
	}
	[Command]
	public void CmdVikingoBueno()
	{
		var objeto = (GameObject)Instantiate(vikingo, nace.transform.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(objeto);
	}
	[Command]
	public void CmdVikingoMalo()
	{
		var objeto = (GameObject)Instantiate(vikingoMalo, nace.transform.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(objeto);
	}
	//CAMPAMENTO
	public void crearCampamento ()
	{
		if(!tirar)
		{
			if(gameObject.tag == "Player")
			{
				CmdCampamentoBueno();
			}else
			{
				CmdCampamentoMalo();
			}

			tirar = true;
		}
	}
	[Command]
	public void CmdCampamentoBueno()
	{
		var objeto = (GameObject)Instantiate(campamento, nace.transform.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(objeto);
	}
	[Command]
	public void CmdCampamentoMalo()
	{
		var objeto = (GameObject)Instantiate(campamentoMalo, nace.transform.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(objeto);
	}
	//TORRETA
	public void crearTorreta ()
	{
		hammer = 0;
		GetComponent<Animator>().SetInteger("crear", 1);

		martillar = true;
		martillaTorreta2 = false;
		martillaTorreta = true;
		/*if(gameObject.tag == "Player")
		{
			martillaTorreta = true;
			//CmdTorretaBueno();
		}else
		{
			martillaTorretaMala = true;
			//CmdTorretaMalo();
		}*/
	}
	[Command]
	public void CmdTorretaBueno()
	{
		var objeto = (GameObject)Instantiate(torreta, nace2.transform.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(objeto);
	}
	[Command]
	public void CmdTorretaMalo()
	{
		var objeto = (GameObject)Instantiate(torretaMalo, nace2.transform.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(objeto);
	}
	//TORRETA MISIL
	public void crearTorretaMisil ()
	{
		hammer = 0;
		GetComponent<Animator>().SetInteger("crear", 1);

		martillar = true;
		martillaTorreta = false;
		martillaTorreta2 = true;
	}
	[Command]
	public void CmdTorretaMisilBueno()
	{
		var objeto = (GameObject)Instantiate(torretaMisil, nace2.transform.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(objeto);
	}
	[Command]
	public void CmdTorretaMisilMalo()
	{
		var objeto = (GameObject)Instantiate(torretaMisilMalo, nace2.transform.position, Quaternion.Euler(0,0,0));
		NetworkServer.Spawn(objeto);
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
			if(gameObject.tag == "Player")
			{
				CmdcrearMinaBueno();
			}else
			{
				CmdcrearMinaMalo();
			}
			minas = 0;
		}
		if(minas == 2)
		{
			if(gameObject.tag == "Player")
			{
				CmdcrearMinaPersonaBueno();
			}else
			{
				CmdcrearMinaPersonaMalo();
			}
			minas = 0;
		}
	}
	[Command]
	public void CmdcrearMinaBueno()
	{
		var objeto = (GameObject)Instantiate(minaAntiTanque, nace3.transform.position, Quaternion.Euler(0,0,0));
		objeto.GetComponent<mina>().poder = GetComponent<HeroNetwork>().saludMax*objeto.GetComponent<mina>().poder/104;
		NetworkServer.Spawn(objeto);
	}
	[Command]
	public void CmdcrearMinaMalo()
	{
		var objeto = (GameObject)Instantiate(minaAntiTanqueMalo, nace3.transform.position, Quaternion.Euler(0,0,0));
		objeto.GetComponent<mina>().poder = GetComponent<HeroNetwork>().saludMax2*objeto.GetComponent<mina>().poder/104;
		NetworkServer.Spawn(objeto);
	}
	//ANTIPERSONA
	[Command]
	public void CmdcrearMinaPersonaBueno()
	{
		var objeto = (GameObject)Instantiate(minaAntiPersona, nace3.transform.position, Quaternion.Euler(0,Random.Range(0,360),0));
		objeto.GetComponent<minaAnipersonaNetwork>().poder = GetComponent<HeroNetwork>().saludMax*objeto.GetComponent<minaAnipersonaNetwork>().poder/104;
		NetworkServer.Spawn(objeto);
	}
	[Command]
	public void CmdcrearMinaPersonaMalo()
	{
		var objeto = (GameObject)Instantiate(minaAntiPersonaMalo, nace3.transform.position, Quaternion.Euler(0,Random.Range(0,360),0));
		objeto.GetComponent<minaAnipersonaNetwork>().poder = GetComponent<HeroNetwork>().saludMax*objeto.GetComponent<minaAnipersonaNetwork>().poder/104;
		NetworkServer.Spawn(objeto);
	}


	public int hammer;
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
		GetComponent<HeroNetwork>().ready = false;

		GetComponent<HeroNetwork>().caminarA = false;
		GetComponent<HeroNetwork>().caminarD = false;
		GetComponent<HeroNetwork>().caminarI = false;
		GetComponent<HeroNetwork>().caminarU = false;

		GetComponent<HeroNetwork>().v3 = Vector3.zero;

		GetComponent<HeroNetwork>().rafaga = true;
		GetComponent<HeroNetwork>().cuchillo = false;

		GetComponent<HeroNetwork>().SniperCam.GetComponent<CamNetwork>().bombardeo = true;

		GetComponent<HeroNetwork>().esconderBarra.SetActive(false);
		GetComponent<HeroNetwork>().SniperCam.GetComponent<Grayscale>().enabled = true;
		//GetComponent<Hero>().SniperCam.GetComponent<LensAberrations>().distortion.enabled = true;
		//animator.SetInteger("disparo",20);
	}

	[Command]
	public void Cmd_misiles()
	{
		var bullet = (GameObject)Instantiate(misiles, new Vector3(GetComponent<HeroNetwork>().SniperCam.transform.position.x, transform.position.y+10, transform.position.z), Quaternion.Euler(0,0,0)); 
		NetworkServer.Spawn(bullet);
		bullet.GetComponent<PoderNetwork>().poder = GetComponent<HeroNetwork>().saludMax*bullet.GetComponent<PoderNetwork>().poder/104;
	}
	[Command]
	public void Cmd_misilesMalo()
	{
		var bullet = (GameObject)Instantiate(misilesMalo, new Vector3(GetComponent<HeroNetwork>().transform.position.x, transform.position.y+10, transform.position.z), Quaternion.Euler(0,0,0)); 
		NetworkServer.Spawn(bullet);
		bullet.GetComponent<PoderNetwork>().poder = GetComponent<HeroNetwork>().saludMax*bullet.GetComponent<PoderNetwork>().poder/104;
	}
}
