﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using UnityEngine.Networking;
using UnityStandardAssets.ImageEffects;
using Prototype.NetworkLobby;

public class Game : NetworkBehaviour {
	
	[SyncVar]
	public GameObject Player1;
	[SyncVar]
	public GameObject Player2;

	public GameObject arriba;
	public GameObject End;

	public GameObject banderaBuena;
	public GameObject banderaMala;

	public GameObject Medalla1;
	public GameObject MedallaTorre;
	public GameObject Medalla2;

	public GameObject rango1;
	public GameObject rango2;
	public GameObject rangoUP;

	[SyncVar]
	public bool final;

	public bool final2;

	//TIEMPO DE PARTIDA
	[SyncVar]
	public float Falta;

	public UnityEngine.UI.Text Tiempo;

	//NOMBRES
	public string nombre1;
	public UnityEngine.UI.Text nombres1;

	public string nombre2;
	public UnityEngine.UI.Text nombres2;

	//NIVELES
	public UnityEngine.UI.Text nivel1;
	[SyncVar]
	public int level1;

	public int level1Next;

	public UnityEngine.UI.Text nivel2;
	[SyncVar]
	public int level2;

	public int level2Next;

	[SyncVar]
	public string Armas1 = "pistola";
	public GameObject ArmasB;

	[SyncVar]
	public string Armas2 = "pistola";
	public GameObject ArmasM;

	//SANGRE BASE BUENA
	public GameObject BaseB;
	[SyncVar]
	public float sagreBB;
	public UnityEngine.UI.Image saludB;

	//SANGRE BASE MALA
	public GameObject BaseM;
	[SyncVar]
	public float sagreBM;
	public UnityEngine.UI.Image saludM;

	//BASE ALPHA
	public GameObject Alpha;
	public GameObject AlphaB;
	public GameObject AlphaM;
	[SyncVar]
	public string AlphaTomada;

	public int vecesTomadaAlphaB;
	public int vecesTomadaAlphaM;

	//BASE BETA
	public GameObject Beta;
	public GameObject BetaB;
	public GameObject BetaM;
	[SyncVar]
	public string BetaTomada;

	public int vecesTomadaBetaB;
	public int vecesTomadaBetaM;

	//ESTADISTICAS JUGADOR 1
	[SyncVar]
	public int KillsB;
	public UnityEngine.UI.Text KillsBT;
	[SyncVar]
	public int DeadsB;
	public UnityEngine.UI.Text DeadsBT;
	[SyncVar]
	public int VechicleDestroyedB;
	public UnityEngine.UI.Text VechicleDestroyedBT;
	[SyncVar]
	public int StolenCardsB;
	public UnityEngine.UI.Text StolenCardsBT;
	[SyncVar]
	public int CapturedFlagsAB;
	[SyncVar]
	public int CapturedFlagsBB;
	[SyncVar]
	public int CapturedFlagsB;
	public UnityEngine.UI.Text CapturedFlagsBT;
	[SyncVar]
	public int BaseDestroyedB;
	public UnityEngine.UI.Text BaseDestroyedBT;

	public int TotalB;
	public int TotalBFinal;
	public UnityEngine.UI.Text TotalBT;

	public float XP;
	public float XPActual;
	public float XPNext;
	public UnityEngine.UI.Text XPT;

	public UnityEngine.UI.Image Exp;

	public int KillsB2;

	public int VechicleDestroyedB2;

	public int StolenCardsB2;

	public int CapturedFlagsB2;

	public int vecesTomadaAlphaB2;
	public int vecesTomadaBetaB2;

	public int BaseDestroyedB2;

	public int DeadsB2;

	//ESTADISTICAS JUGADOR 2
	[SyncVar]
	public int KillsM;
	public UnityEngine.UI.Text KillsMT;
	[SyncVar]
	public int DeadsM;
	public UnityEngine.UI.Text DeadsMT;
	[SyncVar]
	public int VechicleDestroyedM;
	public UnityEngine.UI.Text VechicleDestroyedMT;
	[SyncVar]
	public int StolenCardsM;
	public UnityEngine.UI.Text StolenCardsMT;
	[SyncVar]
	public int CapturedFlagsAM;
	[SyncVar]
	public int CapturedFlagsBM;
	[SyncVar]
	public int CapturedFlagsM;
	public UnityEngine.UI.Text CapturedFlagsMT;
	[SyncVar]
	public int BaseDestroyedM;
	public UnityEngine.UI.Text BaseDestroyedMT;

	[SyncVar]
	public int TotalM;
	public int TotalMFinal;
	public UnityEngine.UI.Text TotalMT;

	public float XPM;
	public float XPActualM;
	public float XPNextM;
	public UnityEngine.UI.Text XPMT;

	public int KillsM2;

	public int VechicleDestroyedM2;

	public int StolenCardsM2;

	public int vecesTomadaAlphaM2;
	public int vecesTomadaBetaM2;

	public int CapturedFlagsM2;

	public int BaseDestroyedM2;

	public int DeadsM2;

	public GameObject Bueno;
	public GameObject Malo;

	public bool explotar;

	[SyncVar]
	bool listoTodos;

	public GameObject musica;

	public GameObject fondo;

	public GameObject finpartida;

	public GameObject Ganador;
	public GameObject Perdedor;
	public GameObject Empate;

	bool titulos;
	bool titulos2;
	bool titulos3;

	bool ponerVictoria;
	bool ponerVictoria2;
	public int victoriaS;
	public int victoriaC;

	public GameObject RegresaLobby;

	// Use this for initialization
	void Start ()
	{
		gameObject.name = "GAME";
		Time.timeScale = 1;

		if(isServer)
		{
			XP = PlayerPrefs.GetFloat("PlayerEX");
			XPActual = XP;
			XPT.text = XP.ToString();

			XPNext = PlayerPrefs.GetInt("PlayerLevel")*10000*1.5f;

			Exp.fillAmount = XPActual/XPNext;

			nombre1 = PlayerPrefs.GetString("SteamName");
			nombres1.text = nombre1;

			nombre2 = Player2.GetComponent<HeroNetwork>().nombre;
			nombres2.text = nombre2;

			cofre = PlayerPrefs.GetInt("caja1");
			banderasCofre = PlayerPrefs.GetInt("banderasCofre");

		}else
		{
			XPM = PlayerPrefs.GetFloat("PlayerEX");
			XPActualM = XPM;
			XPMT.text = XPM.ToString();

			XPNextM = PlayerPrefs.GetInt("PlayerLevel")*10000*1.5f;

			Exp.fillAmount = XPActual/XPNext;

			nombre1 = Player1.GetComponent<HeroNetwork>().nombre;
			nombres1.text = nombre1;

			nombre2 = PlayerPrefs.GetString("SteamName");
			nombres2.text = nombre2;

			cofre = PlayerPrefs.GetInt("caja1");
			banderasCofre = PlayerPrefs.GetInt("banderasCofre");
		}
	}

	/*[Command]
	public void CmdSendNivel (int newNivel)
	{
		level2 = newNivel;
		RpcGetNivel (newNivel);
	}

	[ClientRpc]
	public void RpcGetNivel (int newNivel)
	{
		if(!isLocalPlayer)
		{
			level2 = newNivel;
		}
	}*/
	// SOLO EL SERVIDOR
	void Update ()
	{
		if(RegresaLobby == null)
		{
			RegresaLobby = GameObject.Find("Canvas - Mensajes");
		}

		if(Application.loadedLevelName == "Looby")
		{
			Destroy(gameObject);
		}

		if(!isServer)
		{
			nombre1 = Player1.GetComponent<HeroNetwork>().nombre;
			nombres1.text = nombre1;

			nombre2 = PlayerPrefs.GetString("SteamName");
			nombres2.text = nombre2;

			return;
		}else
		{
			nombre1 = PlayerPrefs.GetString("SteamName");
			nombres1.text = nombre1;

			nombre2 = Player2.GetComponent<HeroNetwork>().nombre;
			nombres2.text = nombre2;
		}

		if(Player1 == null)
		{
			Player1 = GameObject.Find("Hero");
		}else
		{
			if(Armas1 == "")
			{
				Armas1 = "pistola";
			}else
			{
				if(Player1.GetComponent<HeroNetwork>().arma == "")
				{
					Armas1 = "pistola";
				}else
				{
					Armas1 = Player1.GetComponent<HeroNetwork>().arma;
				}
			}
		}

		if(listoTodos)
		{
			level1 = Player1.GetComponent<HeroNetwork>().level;
			//level1Next = level1+1;

			level2 = Player2.GetComponent<HeroNetwork>().level;
			//level2Next = level2+1;

			if(Alpha == null)
			{
				Alpha = GameObject.Find("ALPHA");
			}
			if(Beta == null)
			{
				Beta = GameObject.Find("BETA");
			}

			if(BaseB == null)
			{
				BaseB = GameObject.Find("BASE");
			}else
			{
				sagreBB = BaseB.GetComponent<Base>().sangre/2000;
			}

			if(BaseM == null)
			{
				BaseM = GameObject.Find("BASE MALA");
			}else
			{
				sagreBM =  BaseM.GetComponent<Base>().sangre/2000;
			}

			if(Falta > 0)
			{
				Falta -= Time.deltaTime;
			}
			if(Falta <= 0 && !final && !muerte)
			{
				Player1.GetComponent<HeroNetwork>().SniperCam.GetComponent<Grayscale>().enabled = true;
				Player2.GetComponent<HeroNetwork>().SniperCam.GetComponent<Grayscale>().enabled = true;
				final = true;
			}

			if(Alpha != null)
			{
				if(Alpha.tag == "BaseBuena")
				{
					AlphaTomada = "Buena";
					CapturedFlagsAB = 1;
					CapturedFlagsAM = 0;
				}else if(Alpha.tag == "BaseMala")
				{
					AlphaTomada = "Mala";
					CapturedFlagsAB = 0;
					CapturedFlagsAM = 1;
				}else
				{
					AlphaTomada = "";
					CapturedFlagsAB = 0;
					CapturedFlagsAM = 0;
				}
				vecesTomadaAlphaB = Alpha.GetComponent<BaseNeutraNetwork>().veces;
				vecesTomadaAlphaM = Alpha.GetComponent<BaseNeutraNetwork>().vecesmala;
			}

			if(Beta != null)
			{
				if(Beta.tag == "BaseBuena")
				{
					BetaTomada = "Buena";
					CapturedFlagsBB = 1;
					CapturedFlagsBM = 0;
				}else if(Beta.tag == "BaseMala")
				{
					BetaTomada = "Mala";
					CapturedFlagsBB = 0;
					CapturedFlagsBM = 1;
				}else
				{
					BetaTomada = "";
					CapturedFlagsBB = 0;
					CapturedFlagsBM = 0;
				}
				vecesTomadaBetaB = Beta.GetComponent<BaseNeutraNetwork>().veces;
				vecesTomadaBetaM = Beta.GetComponent<BaseNeutraNetwork>().vecesmala;
			}
		}
	}

	public GameObject destruccion;
	public Vector3 posicion;
	public bool muerte;

	bool anim1;
	bool anim2;

	bool listo;

	public GameObject esperando;

	[Command]
	public void CmdSendArma (string NewArma2)
	{
		Armas2 = NewArma2;
		//RpcGetArma(Armas2);
	}
	/*[ClientRpc]
	public void RpcGetArma (string Armas2)
	{
		
	}*/
	public string Arma2B;

	public GameObject iconos;

	public bool sumatoria;
	public bool sumatoria2;
	public bool sumatoria3;
	public bool sumatoria4;
	public bool sumatoria5;
	public bool sumatoria6;
	public bool sumatoria7;
	public bool sumatoria8;

	public AudioSource audio1;
	public AudioSource audio2;

	bool finalizado;

	public int cofre;
	public int banderasCofre;

	public GameObject Baul;

	public bool sumar1Server;
	public bool sumar2Server;
	public bool sumar3Server;
	public bool sumar1Cliente;
	public bool sumar2Cliente;
	public bool sumar3Cliente;

	public int rematchS;
	public int rematchC;

	[SyncVar]
	public float sleccionFinal;

	public bool bajartiempo;

	public Text TiempoSleccion;

	bool cargar;

	//SERVIDOR Y CLIENTE
	void FixedUpdate ()
	{
		if(Player2 == null)
		{
			Player2 = GameObject.Find("Hero2");
		}else
		{
			if(Armas2 == "")
			{
				Armas2 = "pistola";
			}else
			{
				if(Player2.GetComponent<HeroNetwork>().arma == "")
				{
					Armas2 = "pistola";
				}else
				{
					Armas2 = Player2.GetComponent<HeroNetwork>().arma;
				}
			}
			if(Arma2B != Armas2)
			{
				Arma2B = Armas2;
				CmdSendArma(Armas2);
			}
		}

		if(listoTodos)
		{
			esperando.SetActive(false);
		}else
		{
			esperando.SetActive(true);
		}

		if(Player1 != null && Player2 != null && !listo)
		{
			if(Player1.GetComponent<HeroNetwork>().saludMax > 100 && Player2.GetComponent<HeroNetwork>().saludMax2 > 100)
			{
				listoTodos = true;
				Player1.GetComponent<HeroNetwork>().ready = true;
				Player2.GetComponent<HeroNetwork>().ready = true;
				listo = true;
			}
		}
			
		if(ArmasB.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0).Animation.Name != Armas1)
		{
			ArmasB.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, Armas1, false);
		}

		if(ArmasM.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0).Animation.Name != Armas2)
		{
			ArmasM.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, Armas2, false);
		}

		if(!final)
		{
			Tiempo.text = ""+Falta.ToString("F0");
		}
		nivel1.text = "L V "+level1;
		nivel2.text = "L V "+level2;

		if(final && !final2)
		{
			if(sagreBB <= 0 || sagreBM <= 0)
			{
				titulos2 = true;
			}else
			{
				finpartida.SetActive(true);

				if(!titulos)
				{
					titulos = true;
					StartCoroutine(esperatitulos());
				}
			}
		}
		if(titulos2 && !final2)
		{
			Destroy(musica);
			arriba.SetActive(false);
			iconos.SetActive(false);

			finpartida.SetActive(false);
			if(!titulos3)
			{
				titulos3 = true;
				StartCoroutine(esperafinal());
			}
			fondo.SetActive(true);

			if(isServer)
			{
				banderaBuena.SetActive(true);

				if(sagreBM <= 0)
				{
					Ganador.SetActive(true);
					if(!ponerVictoria)
					{
						victoriaS += 1;
						CmdSendVictoriaCliente(victoriaS);
						ponerVictoria = true;
					}

				}else if(sagreBB <= 0)
				{
					Perdedor.SetActive(true);
					if(!ponerVictoria2)
					{
						victoriaC += 1;
						CmdSendVictoria2Cliente(victoriaC);
						ponerVictoria2 = true;
					}
				}else if(CapturedFlagsB > CapturedFlagsM)
				{
					Ganador.SetActive(true);
					if(!ponerVictoria)
					{
						victoriaS += 1;
						CmdSendVictoriaCliente(victoriaS);
						ponerVictoria = true;
					}
				}else if(CapturedFlagsB < CapturedFlagsM)
				{
					Perdedor.SetActive(true);
					if(!ponerVictoria2)
					{
						victoriaC += 1;
						CmdSendVictoria2Cliente(victoriaC);
						ponerVictoria2 = true;
					}
				}else if(CapturedFlagsB == CapturedFlagsM)
				{
					Empate.SetActive(true);
				}
			}else
			{
				banderaMala.SetActive(true);

				if(sagreBB <= 0)
				{
					Ganador.SetActive(true);
				}else if(sagreBM <= 0)
				{
					Perdedor.SetActive(true);
				}else if(CapturedFlagsM > CapturedFlagsB)
				{
					Ganador.SetActive(true);
				}else if(CapturedFlagsM < CapturedFlagsB)
				{
					Perdedor.SetActive(true);
				}else if(CapturedFlagsM == CapturedFlagsB)
				{
					Empate.SetActive(true);
				}
			}
		}

		if(final && final2)
		{
			titulos = false;
			ponerVictoria = false;
			ponerVictoria2 = false;

			Time.timeScale = 1;
			End.SetActive(true);

			Player1.GetComponent<HeroNetwork>().SniperCam.GetComponent<Grayscale>().enabled = true;
			Player2.GetComponent<HeroNetwork>().SniperCam.GetComponent<Grayscale>().enabled = true;

			if(isServer)
			{
				banderaBuena.SetActive(true);
				Bueno.SetActive(true);

				int siguiente = PlayerPrefs.GetInt("PlayerLevel")+1;
				rango1.GetComponent<combinedSkins>().skinsToCombine[0] = PlayerPrefs.GetInt("PlayerLevel").ToString();
				rango2.GetComponent<combinedSkins>().skinsToCombine[0] = siguiente.ToString();

				if(AlphaTomada == "Buena")
				{
					if(!sumar1Server)
					{
						int sumarBase = PlayerPrefs.GetInt("Banderas")+1;
						PlayerPrefs.SetInt("Banderas", sumarBase);
						int sumarCofre = PlayerPrefs.GetInt("banderasCofre")+1;
						PlayerPrefs.SetInt("banderasCofre", sumarCofre);

						banderasCofre = PlayerPrefs.GetInt("banderasCofre");

						sumar1Server = true;
					}
					StartCoroutine(EspSale1());
				}
				if(BetaTomada == "Buena")
				{
					if(!sumar2Server)
					{
						int sumarBase = PlayerPrefs.GetInt("Banderas")+1;
						PlayerPrefs.SetInt("Banderas", sumarBase);
						int sumarCofre = PlayerPrefs.GetInt("banderasCofre")+1;
						PlayerPrefs.SetInt("banderasCofre", sumarCofre);

						banderasCofre = PlayerPrefs.GetInt("banderasCofre");

						sumar2Server = true;
					}
					if(AlphaTomada == "Buena")
					{
						StartCoroutine(EspSale2());
					}else
					{
						StartCoroutine(EspSale2b());
					}
				}

				if(sagreBM <= 0)
				{
					if(AlphaTomada == "Buena" && BetaTomada == "Buena")
					{
						StartCoroutine(EspSale3c());
					}else if(AlphaTomada == "Buena")
					{
						StartCoroutine(EspSale3b());
					}else
					{
						StartCoroutine(EspSale3());
					}
					if(!sumar3Server)
					{
						int sumarBandera = PlayerPrefs.GetInt("Bases")+1;
						PlayerPrefs.SetInt("Bases", sumarBandera);

						sumar3Server = true;
					}
					BaseDestroyedB = 1;
				}
			}else
			{
				banderaMala.SetActive(true);
				Malo.SetActive(true);

				int siguiente = PlayerPrefs.GetInt("PlayerLevel")+1;
				rango1.GetComponent<combinedSkins>().skinsToCombine[0] = PlayerPrefs.GetInt("PlayerLevel").ToString();
				rango2.GetComponent<combinedSkins>().skinsToCombine[0] = siguiente.ToString();

				if(AlphaTomada == "Mala")
				{
					if(!sumar1Cliente)
					{
						int sumarBase = PlayerPrefs.GetInt("Banderas")+1;
						PlayerPrefs.SetInt("Banderas", sumarBase);
						int sumarCofre = PlayerPrefs.GetInt("banderasCofre")+1;
						PlayerPrefs.SetInt("banderasCofre", sumarCofre);

						banderasCofre = PlayerPrefs.GetInt("banderasCofre");

						sumar1Cliente = true;
					}
					StartCoroutine(EspSale1M());
				}
				if(BetaTomada == "Mala")
				{
					if(!sumar2Cliente)
					{
						int sumarBase = PlayerPrefs.GetInt("Banderas")+1;
						PlayerPrefs.SetInt("Banderas", sumarBase);
						int sumarCofre = PlayerPrefs.GetInt("banderasCofre")+1;
						PlayerPrefs.SetInt("banderasCofre", sumarCofre);

						banderasCofre = PlayerPrefs.GetInt("banderasCofre");

						sumar2Cliente = true;
					}
					if(AlphaTomada == "Mala")
					{
						StartCoroutine(EspSale2M());
					}else
					{
						StartCoroutine(EspSale2bM());
					}
				}

				if(sagreBB <= 0)
				{
					if(AlphaTomada == "Mala" && BetaTomada == "Mala")
					{
						StartCoroutine(EspSale3cM());
					}else if(AlphaTomada == "Mala" || BetaTomada == "Mala")
					{
						StartCoroutine(EspSale3bM());
					}else
					{
						StartCoroutine(EspSale3M());
					}
					if(!sumar3Cliente)
					{
						int sumarBandera = PlayerPrefs.GetInt("Bases")+1;
						PlayerPrefs.SetInt("Bases", sumarBandera);

						sumar3Cliente = true;
					}
					BaseDestroyedB = 1;
				}
			}

			if(!finalizado)
			{
				StartCoroutine(esperaSumar());
				finalizado = true;
			}
		}

		if(explotar)
		{
			Time.timeScale = 0.3f;
			StartCoroutine(muereBase());
			explotar = false;
		}

		saludB.fillAmount = sagreBB;
		saludM.fillAmount = sagreBM;

		if(sagreBB <= 0)
		{
			posicion = new Vector3(BaseB.transform.position.x+7, BaseB.transform.position.y+2, BaseB.transform.position.z-50);
			muerte = true;
		}
		if(sagreBM <= 0)
		{
			posicion = new Vector3(BaseM.transform.position.x-7, BaseB.transform.position.y+2, BaseB.transform.position.z-50);
			muerte = true;
		}

		if(AlphaTomada == "Buena")
		{
			if(AlphaM.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0).Animation.Name == "loop")
			{
				AlphaM.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "salida", false);
				StartCoroutine(AlphaMSale());
			}

			if(AlphaB.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0).Animation.Name == "no")
			{
				AlphaB.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "entrada", false);
				StartCoroutine(AlphaBEntra());
			}

		}else if(AlphaTomada== "Mala")
		{
			if(AlphaB.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0).Animation.Name == "loop")
			{
				AlphaB.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "salida", false);
				StartCoroutine(AlphaBSale());
			}

			if(AlphaM.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0).Animation.Name == "no")
			{
				AlphaM.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "entrada", false);
				StartCoroutine(AlphaMEntra());
			}
		}else
		{
			AlphaB.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "no", false);
			AlphaM.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "no", false);
		}

		if(BetaTomada== "Buena")
		{
			if(BetaM.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0).Animation.Name == "loop")
			{
				BetaM.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "salida", false);
				StartCoroutine(BetaMSale());
			}
			if(BetaB.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0).Animation.Name == "no")
			{
				BetaB.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "entrada", false);
				StartCoroutine(BetaBEntra());
			}
		}else if(BetaTomada == "Mala")
		{
			if(BetaB.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0).Animation.Name == "loop")
			{
				BetaB.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "salida", false);
				StartCoroutine(BetaBSale());
			}
			if(BetaM.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0).Animation.Name == "no")
			{
				BetaM.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "entrada", false);
				StartCoroutine(BetaMEntra());
			}
		}else
		{
			BetaB.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "no", false);
			BetaM.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "no", false);
		}
			
		CapturedFlagsB = CapturedFlagsAB + CapturedFlagsBB;
		CapturedFlagsM = CapturedFlagsAM + CapturedFlagsBM;

		///////ESTADISTICAS FINALES

		if(isServer)
		{
			TotalB = KillsB2+VechicleDestroyedB2+StolenCardsB2+CapturedFlagsB2+BaseDestroyedB2+vecesTomadaAlphaB2+vecesTomadaBetaB2;
			TotalBFinal = TotalB-DeadsB2;
			TotalBT.text = TotalBFinal.ToString();

			if(sumatoria)
			{
				KillsB2 += 100;
				if(KillsB2 >= KillsB*300)
				{
					KillsB2 = KillsB*300;
					sumatoria = false;
					sumatoria2 = true;
				}
				KillsBT.text = KillsB2.ToString();
			}
			if(sumatoria2)
			{
				VechicleDestroyedB2 += 100;
				if(VechicleDestroyedB2 >= VechicleDestroyedB*500)
				{
					VechicleDestroyedB2 = VechicleDestroyedB*500;
					sumatoria2 = false;
					sumatoria3 = true;
				}
				VechicleDestroyedBT.text = VechicleDestroyedB2.ToString();
			}
			if(sumatoria3)
			{
				if(banderasCofre >= 3)
				{
					PlayerPrefs.SetInt("caja1", cofre+1);
					PlayerPrefs.SetInt("banderasCofre", PlayerPrefs.GetInt("banderasCofre")-3);

					if(PlayerPrefs.GetInt("banderasCofre") <= 0)
					{
						PlayerPrefs.SetInt("banderasCofre", 0);
					}

					Baul.SetActive(true);
					Baul.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "cofre", false);

					banderasCofre = 0;
				}
				StolenCardsB2 += 100;
				if(StolenCardsB2 >= StolenCardsB*380)
				{
					StolenCardsB2 = StolenCardsB*380;
					sumatoria3 = false;
					sumatoria4 = true;
				}
				StolenCardsBT.text = StolenCardsB2.ToString();
			}
			if(sumatoria4)
			{
				CapturedFlagsB2 += 100;
				if(CapturedFlagsB2 >= CapturedFlagsB*900)
				{
					CapturedFlagsB2 = CapturedFlagsB*900;
					sumatoria4 = false;
					sumatoria5 = true;
				}
				CapturedFlagsBT.text = CapturedFlagsB2.ToString();
			}
			if(sumatoria5)
			{
				BaseDestroyedB2 += 100;
				if(BaseDestroyedB2 >= BaseDestroyedB*2000)
				{
					BaseDestroyedB2 = BaseDestroyedB*2000;
					sumatoria5 = false;
					sumatoria6 = true;
				}
				BaseDestroyedBT.text = BaseDestroyedB2.ToString();
			}
			if(sumatoria6)
			{
				vecesTomadaAlphaB2 += 10;
				if(vecesTomadaAlphaB2 >= vecesTomadaAlphaB*70)
				{
					vecesTomadaAlphaB2 = vecesTomadaAlphaB*70;
					sumatoria6 = false;
					sumatoria7 = true;
				}
			}
			if(sumatoria7)
			{
				vecesTomadaBetaB2 += 10;
				if(vecesTomadaBetaB2 >= vecesTomadaBetaB*70)
				{
					vecesTomadaBetaB2 = vecesTomadaBetaB*70;
					sumatoria7 = false;
					sumatoria8 = true;
				}
			}
			if(sumatoria8)
			{
				DeadsB2 -= 10;
				if(DeadsB2 <= 0-DeadsB*80)
				{
					DeadsB2 = 0-DeadsB*80;
					experiencia = true;
					audio1.Stop();
					audio2.Play();
					sumatoria8 = false;
				}
				DeadsBT.text = "-"+DeadsB2.ToString();
			}
			if(experiencia)
			{
				XPActual += 30;
				if(XPActual >= XP+TotalBFinal)
				{
					XPActual = XP+TotalBFinal;
					audio2.Stop();
					PlayerPrefs.SetFloat("PlayerEX",XPActual);

					experiencia = false;
					StartCoroutine(espContinuar());
				}

				XPT.text = XPActual.ToString();
				Exp.fillAmount = XPActual/XPNext;
			}

			if(XPActual >= XPNext)
			{
				LevelUpNext = true;
			}

			if(LevelUpNext)
			{
				XPNext = level1Next*10000*level1Next/4;

				PlayerPrefs.SetInt("PlayerLevel",level1+1);
				level1Next = level1+1;

				rangoUP.SetActive(true);
				rangoUP.GetComponent<combinedSkins>().skinsToCombine[0] = PlayerPrefs.GetInt("PlayerLevel").ToString();

				rango1.GetComponent<combinedSkins>().skinsToCombine[0] = PlayerPrefs.GetInt("PlayerLevel").ToString();
				rango2.GetComponent<combinedSkins>().skinsToCombine[0] = level1Next.ToString();

				LevelUpNext = false;
			}

			if(continuar)
			{
				Player1.GetComponent<HeroNetwork>().continuar = true;
				rematchS = Player1.GetComponent<HeroNetwork>().rematch;
				rematchC = Player2.GetComponent<HeroNetwork>().rematch;

				if(sleccionFinal > 0)
				{
					sleccionFinal -= Time.deltaTime;
					if(rematchS > -1 && rematchC > -1 && !bajartiempo)
					{
						print("BAJAR TIEMPO A 5");
						if(sleccionFinal > 5)
						{
							sleccionFinal = 5;
						}
						bajartiempo = true;
					}
				}else
				{
					sleccionFinal = 0;
					if(rematchS+rematchC == 2)
					{
						if(!cargar)
						{
							CmdEndGame(Application.loadedLevelName);
							cargar = true;
						}
					}else
					{
						if(!cargar)
						{
							RegresaLobby.GetComponent<regresaLobby>().retirada = true;
							NetworkManager.singleton.StopHost();
							NetworkManager.singleton.StopClient();

							Application.LoadLevel("Load");
							loading.nombre = "Lobby";
							cargar = true;
						}
					}
				}
				//TiempoSleccion.text = "Waiting... "+sleccionFinal.ToString("F0");
			}
		}else//PLAYER 2 "CLIENTE"
		{
			TotalM = KillsM2+VechicleDestroyedM2+StolenCardsM2+CapturedFlagsM2+BaseDestroyedM2+vecesTomadaAlphaM2+vecesTomadaBetaM2;
			TotalMFinal = TotalM-DeadsM2;
			TotalMT.text = TotalMFinal.ToString();

			if(sumatoria)
			{
				KillsM2 += 100;
				if(KillsM2 >= KillsM*300)
				{
					KillsM2 = KillsM*300;
					sumatoria = false;
					sumatoria2 = true;
				}
				KillsMT.text = KillsM2.ToString();
			}
			if(sumatoria2)
			{
				VechicleDestroyedM2 += 100;
				if(VechicleDestroyedM2 >= VechicleDestroyedM*500)
				{
					VechicleDestroyedM2 = VechicleDestroyedM*500;
					sumatoria2 = false;
					sumatoria3 = true;
				}
				VechicleDestroyedMT.text = VechicleDestroyedM2.ToString();
			}
			if(sumatoria3)
			{
				if(banderasCofre >= 3)
				{
					PlayerPrefs.SetInt("caja1", cofre+1);
					PlayerPrefs.SetInt("banderasCofre", PlayerPrefs.GetInt("banderasCofre")-3);

					if(PlayerPrefs.GetInt("banderasCofre") <= 0)
					{
						PlayerPrefs.SetInt("banderasCofre", 0);
					}

					Baul.SetActive(true);
					Baul.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "cofre", false);

					banderasCofre = 0;
				}

				StolenCardsM2 += 100;
				if(StolenCardsM2 >= StolenCardsM*380)
				{
					StolenCardsM2 = StolenCardsM*380;
					sumatoria3 = false;
					sumatoria4 = true;
				}
				StolenCardsMT.text = StolenCardsM2.ToString();
			}
			if(sumatoria4)
			{
				CapturedFlagsM2 += 100;
				if(CapturedFlagsM2 >= CapturedFlagsM*900)
				{
					CapturedFlagsM2 = CapturedFlagsM*900;
					sumatoria4 = false;
					sumatoria5 = true;
				}
				CapturedFlagsMT.text = CapturedFlagsM2.ToString();
			}
			if(sumatoria5)
			{
				BaseDestroyedM2 += 100;
				if(BaseDestroyedM2 >= BaseDestroyedM*2000)
				{
					BaseDestroyedM2 = BaseDestroyedM*2000;
					sumatoria5 = false;
					sumatoria6 = true;
				}
				BaseDestroyedMT.text = BaseDestroyedB2.ToString();
			}
			if(sumatoria6)
			{
				vecesTomadaAlphaM2 += 10;
				if(vecesTomadaAlphaM2 >= vecesTomadaAlphaM*70)
				{
					vecesTomadaAlphaM2 = vecesTomadaAlphaM*70;
					sumatoria6 = false;
					sumatoria7 = true;
				}
			}
			if(sumatoria7)
			{
				vecesTomadaBetaM2 += 10;
				if(vecesTomadaBetaM2 >= vecesTomadaBetaM*70)
				{
					vecesTomadaBetaM2 = vecesTomadaBetaM*70;
					sumatoria7 = false;
					sumatoria8 = true;
				}
			}
			if(sumatoria8)
			{
				DeadsM2 -= 10;
				if(DeadsM2 <= 0-DeadsM*80)
				{
					DeadsM2 = 0-DeadsM*80;
					experiencia = true;
					audio1.Stop();
					audio2.Play();
					sumatoria8 = false;
				}
				DeadsMT.text = "-"+DeadsM2.ToString();
			}
			if(experiencia)
			{
				XPActualM += 30;
				if(XPActualM >= XPM+TotalMFinal)
				{
					XPActualM = XPM+TotalMFinal;
					audio2.Stop();
					PlayerPrefs.SetFloat("PlayerEX",XPActualM);

					experiencia = false;
					StartCoroutine(espContinuar());
				}

				XPMT.text = XPActualM.ToString();
				Exp.fillAmount = XPActualM/XPNextM;
			}

			if(XPActualM >= XPNextM)
			{
				LevelUpNext = true;
			}

			if(LevelUpNext)
			{
				XPNext = level1Next*10000*level1Next/4;

				PlayerPrefs.SetInt("PlayerLevel",level1+1);
				level1Next = level1+1;

				rangoUP.SetActive(true);
				rangoUP.GetComponent<combinedSkins>().skinsToCombine[0] = PlayerPrefs.GetInt("PlayerLevel").ToString();

				rango1.GetComponent<combinedSkins>().skinsToCombine[0] = PlayerPrefs.GetInt("PlayerLevel").ToString();
				rango2.GetComponent<combinedSkins>().skinsToCombine[0] = level1Next.ToString();

				LevelUpNext = false;
			}

			if(continuar)
			{
				Player2.GetComponent<HeroNetwork>().continuar = true;

				rematchS = Player1.GetComponent<HeroNetwork>().rematch;
				rematchC = Player2.GetComponent<HeroNetwork>().rematch;

				if(sleccionFinal <= 0)
				{
					if(rematchS+rematchC == 2)
					{
						if(!cargar)
						{
							//CmdEndGame(Application.loadedLevelName);
							cargar = true;
						}
					}else
					{
						if(!cargar)
						{
							RegresaLobby.GetComponent<regresaLobby>().retirada = true;
							cargar = true;
						}
					}
				}
			}
		}
	}
	//SE ACABO EL TIEMPO
	IEnumerator esperatitulos()
	{
		yield return new WaitForSeconds(5f);
		titulos2 = true;
	}

	IEnumerator esperafinal()
	{
		yield return new WaitForSeconds(7f);
		final2 = true;
		Ganador.SetActive(false);
		Perdedor.SetActive(false);
		Empate.SetActive(false);
	}

	[Command]
	public void CmdEndGame(string level)
	{
		NetworkManager.singleton.ServerChangeScene(level);
	}
	//--ORDEN DE LAS MEDALLAS BUENAS
	IEnumerator EspSale1()
	{
		yield return new WaitForSeconds(2f);
		Medalla1.SetActive(true);
		Medalla1.GetComponent<combinedSkins>().skinsToCombine[0] = "bueno";
	}
	IEnumerator EspSale2()
	{
		yield return new WaitForSeconds(4f);
		Medalla2.SetActive(true);
		Medalla2.GetComponent<combinedSkins>().skinsToCombine[0] = "bueno";
	}
	IEnumerator EspSale2b()
	{
		yield return new WaitForSeconds(2f);
		Medalla2.SetActive(true);
		Medalla2.GetComponent<combinedSkins>().skinsToCombine[0] = "bueno";
	}

	IEnumerator EspSale3()
	{
		yield return new WaitForSeconds(2f);
		MedallaTorre.SetActive(true);
		MedallaTorre.GetComponent<combinedSkins>().skinsToCombine[0] = "torreBueno";
	}
	IEnumerator EspSale3b()
	{
		yield return new WaitForSeconds(4f);
		MedallaTorre.SetActive(true);
		MedallaTorre.GetComponent<combinedSkins>().skinsToCombine[0] = "torreBueno";
	}
	IEnumerator EspSale3c()
	{
		yield return new WaitForSeconds(6f);
		MedallaTorre.SetActive(true);
		MedallaTorre.GetComponent<combinedSkins>().skinsToCombine[0] = "torreBueno";
	}
	//--ORDEN DE LAS MEDALLAS MALAS
	IEnumerator EspSale1M()
	{
		yield return new WaitForSeconds(2f);
		Medalla1.SetActive(true);
		Medalla1.GetComponent<combinedSkins>().skinsToCombine[0] = "malo";
	}
	IEnumerator EspSale2M()
	{
		yield return new WaitForSeconds(4f);
		Medalla2.SetActive(true);
		Medalla2.GetComponent<combinedSkins>().skinsToCombine[0] = "malo";
	}
	IEnumerator EspSale2bM()
	{
		yield return new WaitForSeconds(2f);
		Medalla2.SetActive(true);
		Medalla2.GetComponent<combinedSkins>().skinsToCombine[0] = "malo";
	}

	IEnumerator EspSale3M()
	{
		yield return new WaitForSeconds(2f);
		MedallaTorre.SetActive(true);
		MedallaTorre.GetComponent<combinedSkins>().skinsToCombine[0] = "torremalo";
	}
	IEnumerator EspSale3bM()
	{
		yield return new WaitForSeconds(4f);
		MedallaTorre.SetActive(true);
		MedallaTorre.GetComponent<combinedSkins>().skinsToCombine[0] = "torremalo";
	}
	IEnumerator EspSale3cM()
	{
		yield return new WaitForSeconds(6f);
		MedallaTorre.SetActive(true);
		MedallaTorre.GetComponent<combinedSkins>().skinsToCombine[0] = "torremalo";
	}
	//---

	public bool experiencia;
	public bool LevelUpNext;
	public bool continuar;
	public GameObject siguiente;

	IEnumerator espContinuar()
	{
		yield return new WaitForSeconds(4f);
		continuar = true;
	}

	IEnumerator esperaSumar()
	{
		yield return new WaitForSeconds(1f);
		finalizado = true;
		sumatoria = true;
		audio1.Play();
	}

	IEnumerator AlphaBEntra()
	{
		yield return new WaitForSpineAnimationComplete(AlphaB.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0));
		AlphaB.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "loop", true);
	}
	IEnumerator AlphaBSale()
	{
		yield return new WaitForSpineAnimationComplete(AlphaB.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0));
		AlphaB.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "no", false);
	}

	IEnumerator BetaBEntra()
	{
		yield return new WaitForSpineAnimationComplete(BetaB.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0));
		BetaB.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "loop", true);
	}
	IEnumerator BetaBSale()
	{
		yield return new WaitForSpineAnimationComplete(BetaB.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0));
		BetaB.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "no", false);
	}

	IEnumerator AlphaMEntra()
	{
		yield return new WaitForSpineAnimationComplete(AlphaM.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0));
		AlphaM.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "loop", true);
	}
	IEnumerator AlphaMSale()
	{
		yield return new WaitForSpineAnimationComplete(AlphaM.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0));
		AlphaM.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "no", false);
	}

	IEnumerator BetaMEntra()
	{
		yield return new WaitForSpineAnimationComplete(BetaM.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0));
		BetaM.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "loop", true);
	}
	IEnumerator BetaMSale()
	{
		yield return new WaitForSpineAnimationComplete(BetaM.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0));
		BetaM.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "no", false);
	}

	IEnumerator muereBase ()
	{
		yield return new WaitForSeconds(1);
		Time.timeScale = 1;
		destruccion.SetActive(true);
		StartCoroutine(ultimo());
	}

	IEnumerator ultimo ()
	{
		yield return new WaitForSpineAnimationComplete(destruccion.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0));
		final = true;
	}

	public void menu()
	{
		Application.LoadLevel("Lobby");
	}

	[Command]
	public void CmdSendVictoriaCliente(int newVictoriaS)
	{
		RpcSetVictoria(newVictoriaS);
	}
	[ClientRpc]
	public void RpcSetVictoria(int newVictoriaS)
	{
		victoriaS = newVictoriaS;
	}

	[Command]
	public void CmdSendVictoria2Cliente(int newVictoriaC)
	{
		RpcSetVictoria2(newVictoriaC);
	}
	[ClientRpc]
	public void RpcSetVictoria2(int newVictoriaC)
	{
		victoriaC = newVictoriaC;
	}

	[Command]
	public void CmdSendVictoriaServer(int newVictoriaC)
	{
		victoriaC = newVictoriaC;
	}
}
