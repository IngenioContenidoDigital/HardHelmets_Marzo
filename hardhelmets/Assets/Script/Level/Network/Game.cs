using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using UnityEngine.Networking;
using UnityStandardAssets.ImageEffects;
using Prototype.NetworkLobby;
using UnityEngine.EventSystems;
using EasySteamLeaderboard;

public class Game : NetworkBehaviour {
	
	[SyncVar]
	public GameObject Player1;
	[SyncVar]
	public GameObject Player2;

	public GameObject arriba;
	public GameObject End;

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

	public GameObject skinlevel1;

	public int level1Next;

	public UnityEngine.UI.Text nivel2;
	[SyncVar]
	public int level2;

	public GameObject skinlevel2;

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
	bool esconderAlphaB;
	bool esconderAlphaM;

	[SyncVar]
	public string AlphaTomada;

	public int vecesTomadaAlphaB;
	public int vecesTomadaAlphaM;

	//BASE BETA
	public GameObject Beta;
	public GameObject BetaB;
	public GameObject BetaM;
	bool esconderBetaB;
	bool esconderBetaM;

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
	public int TotalBFinalFINAL;
	public UnityEngine.UI.Text TotalBT;

	public float XP;
	public float XPActual;

	public float XPTotal;

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
	public int TotalMFinalFINAL;
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

	public GameObject finpartida;

	//JUGADOR 1
	public GameObject Ganador1;
	public GameObject Perdedor1;
	//JUGADOR 2
	public GameObject Ganador2;
	public GameObject Perdedor2;
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

		if(isServer)
		{
			XPTotal = PlayerPrefs.GetFloat("PlayerEXTotal");

			XP = PlayerPrefs.GetFloat("PlayerEX");
			XPActual = XP;
			XPT.text = XP.ToString();

			XPNext = PlayerPrefs.GetInt("PlayerLevel")*10000*1.5f;

			Exp.fillAmount = XPActual/XPNext;

			nombre1 = PlayerPrefs.GetString("SteamName");
			nombres1.text = nombre1;

			cofre = PlayerPrefs.GetInt("caja1");
			banderasCofre = PlayerPrefs.GetInt("banderasCofre");

		}else
		{
			XPTotal = PlayerPrefs.GetFloat("PlayerEXTotal");

			XPM = PlayerPrefs.GetFloat("PlayerEX");
			XPActualM = XPM;
			XPMT.text = XPM.ToString();

			XPNextM = PlayerPrefs.GetInt("PlayerLevel")*10000*1.5f;

			Exp.fillAmount = XPActual/XPNext;

			nombre2 = PlayerPrefs.GetString("SteamName");
			nombres2.text = nombre2;

			cofre = PlayerPrefs.GetInt("caja1");
			banderasCofre = PlayerPrefs.GetInt("banderasCofre");
		}
	}

	// SOLO EL SERVIDOR
	void Update ()
	{
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

		if(RegresaLobby == null)
		{
			RegresaLobby = GameObject.Find("Canvas - Mensajes");
		}

		if(musica == null)
		{
			musica = GameObject.Find("MUSICA BATALLA");
		}

		if(!isServer)
		{
			if(Player1 != null)
			{
				nombre1 = Player1.GetComponent<HeroNetwork>().nombre;
				nombres1.text = nombre1;
			}

			return;
		}else
		{
			if(Player2 != null)
			{
				nombre2 = Player2.GetComponent<HeroNetwork>().nombre;
				nombres2.text = nombre2;
			}
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
	public GameObject destruccion2;
	public GameObject destruccion3;
	public GameObject destruccion4;
	public Vector3 posicion;
	public bool muerte;

	bool anim1;
	bool anim2;

	bool listo;

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

	bool cargar;
	bool cargar2;

	string idioma;

	//SERVIDOR Y CLIENTE
	public void FixedUpdate ()
	{
		idioma = PlayerPrefs.GetString("idioma");

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
		if(sagreBB > 0 && sagreBM > 0)
		{
			muerte = false;
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
		if(skinlevel1.GetComponent<combinedSkins>().skinsToCombine[0] != level1.ToString())
		{
			skinlevel1.GetComponent<combinedSkins>().skinsToCombine[0] = level1.ToString();

			skinlevel2.GetComponent<combinedSkins>().skinsToCombine[0] = level2.ToString();
		}
		if(skinlevel2.GetComponent<combinedSkins>().skinsToCombine[0] != level2.ToString())
		{
			skinlevel2.GetComponent<combinedSkins>().skinsToCombine[0] = level2.ToString();
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
		nivel1.text = level1.ToString();
		nivel2.text = level2.ToString();

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
			musica.GetComponent<AudioSource>().Stop();
			arriba.SetActive(false);
			iconos.SetActive(false);

			finpartida.SetActive(false);
			if(!titulos3)
			{
				titulos3 = true;
				StartCoroutine(esperafinal());
			}

			if(isServer)
			{
				if(sagreBM <= 0)
				{
					Ganador1.SetActive(true);
					if(Ganador1.GetComponent<SkeletonAnimation>().AnimationState.GetCurrent(0).Animation.Name != "entrada")
					{
						Ganador1.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "entrada", true);
					}
					if(idioma == "ENGLISH")
					{
						Ganador1.GetComponent<combinedSkins>().skinsToCombine[0] = "victory";
					}
					if(idioma == "SPANISH")
					{
						Ganador1.GetComponent<combinedSkins>().skinsToCombine[0] = "victoria";
					}
					if(idioma == "CHINESE")
					{
						Ganador1.GetComponent<combinedSkins>().skinsToCombine[0] = "victoriaCH";
					}
					if(!ponerVictoria)
					{
						victoriaS += 1;
						CmdSendVictoriaCliente(victoriaS);
						ponerVictoria = true;
					}

				}else if(sagreBB <= 0)
				{
					Perdedor1.SetActive(true);
					if(Perdedor1.GetComponent<SkeletonAnimation>().AnimationState.GetCurrent(0).Animation.Name != "entrada")
					{
						Perdedor1.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "entrada", true);
					}
					if(idioma == "ENGLISH")
					{
						Perdedor1.GetComponent<combinedSkins>().skinsToCombine[0] = "defeated";
					}
					if(idioma == "SPANISH")
					{
						Perdedor1.GetComponent<combinedSkins>().skinsToCombine[0] = "derrota";
					}
					if(idioma == "CHINESE")
					{
						Perdedor1.GetComponent<combinedSkins>().skinsToCombine[0] = "derrotaCH";
					}
					if(!ponerVictoria2)
					{
						victoriaC += 1;
						CmdSendVictoria2Cliente(victoriaC);
						ponerVictoria2 = true;
					}
				}else if(CapturedFlagsB > CapturedFlagsM)
				{
					Ganador1.SetActive(true);
					if(Ganador1.GetComponent<SkeletonAnimation>().AnimationState.GetCurrent(0).Animation.Name != "entrada")
					{
						Ganador1.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "entrada", true);
					}
					if(idioma == "ENGLISH")
					{
						Ganador1.GetComponent<combinedSkins>().skinsToCombine[0] = "victory";
					}
					if(idioma == "SPANISH")
					{
						Ganador1.GetComponent<combinedSkins>().skinsToCombine[0] = "victoria";
					}
					if(idioma == "CHINESE")
					{
						Ganador1.GetComponent<combinedSkins>().skinsToCombine[0] = "victoriaCH";
					}
					if(!ponerVictoria)
					{
						victoriaS += 1;
						CmdSendVictoriaCliente(victoriaS);
						ponerVictoria = true;
					}
				}else if(CapturedFlagsB < CapturedFlagsM)
				{
					Perdedor1.SetActive(true);
					if(Perdedor1.GetComponent<SkeletonAnimation>().AnimationState.GetCurrent(0).Animation.Name != "entrada")
					{
						Perdedor1.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "entrada", true);
					}
					if(idioma == "ENGLISH")
					{
						Perdedor1.GetComponent<combinedSkins>().skinsToCombine[0] = "defeated";
					}
					if(idioma == "SPANISH")
					{
						Perdedor1.GetComponent<combinedSkins>().skinsToCombine[0] = "derrota";
					}
					if(idioma == "CHINESE")
					{
						Perdedor1.GetComponent<combinedSkins>().skinsToCombine[0] = "derrotaCH";
					}
					if(!ponerVictoria2)
					{
						victoriaC += 1;
						CmdSendVictoria2Cliente(victoriaC);
						ponerVictoria2 = true;
					}
				}else if(CapturedFlagsB == CapturedFlagsM)
				{
					Empate.SetActive(true);
					if(Empate.GetComponent<SkeletonAnimation>().AnimationState.GetCurrent(0).Animation.Name != "entrada")
					{
						Empate.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "entrada", true);
					}
					if(idioma == "ENGLISH")
					{
						Empate.GetComponent<combinedSkins>().skinsToCombine[0] = "withdrawal";
					}
					if(idioma == "SPANISH")
					{
						Empate.GetComponent<combinedSkins>().skinsToCombine[0] = "retirada";
					}
					if(idioma == "CHINESE")
					{
						Empate.GetComponent<combinedSkins>().skinsToCombine[0] = "retiradaCH";
					}
				}
			}else
			{
				if(sagreBB <= 0)
				{
					Ganador2.SetActive(true);
					if(Ganador2.GetComponent<SkeletonAnimation>().AnimationState.GetCurrent(0).Animation.Name != "entrada")
					{
						Ganador2.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "entrada", true);
					}
					if(idioma == "ENGLISH")
					{
						Ganador2.GetComponent<combinedSkins>().skinsToCombine[0] = "victory";
					}
					if(idioma == "SPANISH")
					{
						Ganador2.GetComponent<combinedSkins>().skinsToCombine[0] = "victoria";
					}
					if(idioma == "CHINESE")
					{
						Ganador2.GetComponent<combinedSkins>().skinsToCombine[0] = "victoriaCH";
					}
				}else if(sagreBM <= 0)
				{
					Perdedor2.SetActive(true);
					if(Perdedor2.GetComponent<SkeletonAnimation>().AnimationState.GetCurrent(0).Animation.Name != "entrada")
					{
						Perdedor2.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "entrada", true);
					}
					if(idioma == "ENGLISH")
					{
						Perdedor2.GetComponent<combinedSkins>().skinsToCombine[0] = "defeated";
					}
					if(idioma == "SPANISH")
					{
						Perdedor2.GetComponent<combinedSkins>().skinsToCombine[0] = "derrota";
					}
					if(idioma == "CHINESE")
					{
						Perdedor2.GetComponent<combinedSkins>().skinsToCombine[0] = "derrotaCH";
					}
				}else if(CapturedFlagsM > CapturedFlagsB)
				{
					Ganador2.SetActive(true);
					if(Ganador2.GetComponent<SkeletonAnimation>().AnimationState.GetCurrent(0).Animation.Name != "entrada")
					{
						Ganador2.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "entrada", true);
					}
					if(idioma == "ENGLISH")
					{
						Ganador2.GetComponent<combinedSkins>().skinsToCombine[0] = "victory";
					}
					if(idioma == "SPANISH")
					{
						Ganador2.GetComponent<combinedSkins>().skinsToCombine[0] = "victoria";
					}
					if(idioma == "CHINESE")
					{
						Ganador2.GetComponent<combinedSkins>().skinsToCombine[0] = "victoriaCH";
					}
				}else if(CapturedFlagsM < CapturedFlagsB)
				{
					Perdedor2.SetActive(true);
					if(Perdedor2.GetComponent<SkeletonAnimation>().AnimationState.GetCurrent(0).Animation.Name != "entrada")
					{
						Perdedor2.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "entrada", true);
					}
					if(idioma == "ENGLISH")
					{
						Perdedor2.GetComponent<combinedSkins>().skinsToCombine[0] = "defeated";
					}
					if(idioma == "SPANISH")
					{
						Perdedor2.GetComponent<combinedSkins>().skinsToCombine[0] = "derrota";
					}
					if(idioma == "CHINESE")
					{
						Perdedor2.GetComponent<combinedSkins>().skinsToCombine[0] = "derrotaCH";
					}
				}else if(CapturedFlagsM == CapturedFlagsB)
				{
					Empate.SetActive(true);
					if(Empate.GetComponent<SkeletonAnimation>().AnimationState.GetCurrent(0).Animation.Name != "entrada")
					{
						Empate.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "entrada", true);
					}
					if(idioma == "ENGLISH")
					{
						Empate.GetComponent<combinedSkins>().skinsToCombine[0] = "withdrawal";
					}
					if(idioma == "SPANISH")
					{
						Empate.GetComponent<combinedSkins>().skinsToCombine[0] = "retirada";
					}
					if(idioma == "CHINESE")
					{
						Empate.GetComponent<combinedSkins>().skinsToCombine[0] = "retiradaCH";
					}
				}
			}
		}

		if(final && final2)
		{
			if(Ganador1.activeSelf && Ganador1.GetComponent<SkeletonAnimation>().AnimationState.GetCurrent(0).Animation.Name != "loop")
			{
				Ganador1.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "loop", true);
			}
			if(Perdedor1.activeSelf && Perdedor1.GetComponent<SkeletonAnimation>().AnimationState.GetCurrent(0).Animation.Name != "loop")
			{
				Perdedor1.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "loop", true);
			}

			if(Ganador2.activeSelf && Ganador2.GetComponent<SkeletonAnimation>().AnimationState.GetCurrent(0).Animation.Name != "loop")
			{
				Ganador2.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "loop", true);
			}
			if(Perdedor2.activeSelf && Perdedor2.GetComponent<SkeletonAnimation>().AnimationState.GetCurrent(0).Animation.Name != "loop")
			{
				Perdedor2.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "loop", true);
			}
			if(Empate.activeSelf && Empate.GetComponent<SkeletonAnimation>().AnimationState.GetCurrent(0).Animation.Name != "loop")
			{
				Empate.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "loop", true);
			}

			titulos = false;
			ponerVictoria = false;
			ponerVictoria2 = false;

			End.SetActive(true);

			if(isServer)
			{
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
		}else
		{
			End.SetActive(false);

			if(Medalla1.activeSelf)
			{
				Medalla1.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "entrada", false);
				Medalla1.SetActive(false);
			}
			if(MedallaTorre.activeSelf)
			{
				MedallaTorre.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "entrada", false);
				MedallaTorre.SetActive(false);
			}
			if(Medalla2.activeSelf)
			{
				Medalla2.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "entrada", false);
				Medalla2.SetActive(false);
			}

			sleccionFinal = 30;
		}

		if(explotar)
		{
			explotar = false;
			StartCoroutine(muereBase());
		}

		saludB.fillAmount = sagreBB;
		saludM.fillAmount = sagreBM;

		if(sagreBB <= 0)
		{
			if(Application.loadedLevelName == "LevelNetwork0")
			{
				posicion = new Vector3(BaseB.transform.position.x+7, BaseB.transform.position.y+10, BaseB.transform.position.z-100);
			}else
			{
				posicion = new Vector3(BaseB.transform.position.x+9, BaseB.transform.position.y+10, BaseB.transform.position.z-100);
			}
			muerte = true;
		}
		if(sagreBM <= 0)
		{
			posicion = new Vector3(BaseM.transform.position.x-7, BaseB.transform.position.y+10, BaseB.transform.position.z-100);
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
			esconderAlphaB = false;
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
			esconderAlphaM = false;
		}else
		{
			if(!esconderAlphaB)
			{
				AlphaB.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "salida", false);
				StartCoroutine(AlphaBSale());
				esconderAlphaB = true;
			}
			if(!esconderAlphaM)
			{
				AlphaM.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "salida", false);
				StartCoroutine(AlphaMSale());
				esconderAlphaM = true;
			}
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
			esconderBetaB = false;
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
			esconderBetaM = false;
		}else
		{
			if(!esconderBetaB)
			{
				BetaB.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "salida", false);
				StartCoroutine(BetaBSale());
				esconderBetaB = true;
			}
			if(!esconderBetaM)
			{
				BetaM.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "salida", false);
				StartCoroutine(BetaMSale());
				esconderBetaM = true;
			}
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
				if(!sumarmatados)
				{
					int matados = PlayerPrefs.GetInt("Kills")+KillsB;
					PlayerPrefs.SetInt("Kills", matados);

					sumarmatados = true;
				}

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
					audio1.Stop();
					audio2.Play();
					TotalBFinalFINAL = TotalBFinal;
					experiencia = true;
					sumatoria8 = false;
				}
				DeadsBT.text = "-"+DeadsB2.ToString();
			}
			if(experiencia)
			{
				XPActual += 30;
				XPTotal += 30;
				PlayerPrefs.SetFloat("PlayerEXTotal", XPTotal);
				TotalBFinalFINAL -= 30;
				if(TotalBFinalFINAL <= 0)//if(XPActual >= XP+TotalBFinal)
				{
					//XPActual = XP+TotalBFinal;
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
				PlayerPrefs.SetFloat("PlayerEX",0);//RESET EXPERIENCIA EN CADA NIVEL
				XPActual = 0;
			}

			if(LevelUpNext)
			{
				XPNext = level1Next*10000*level1Next/4;

				PlayerPrefs.SetInt("PlayerLevel",level1+1);
				level1Next = level1+1;

				rangoUP.SetActive(true);
				rangoUP.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "levelUp", false);
				rangoUP.GetComponent<AudioSource>().Play();
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
						if(sleccionFinal > 5)
						{
							sleccionFinal = 5;
						}
						bajartiempo = true;
					}

					if(victoriaS == 2 && !bajartiempo || victoriaC == 2 && !bajartiempo )
					{
						if(sleccionFinal > 10)
						{
							sleccionFinal = 10;
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
							Player1.GetComponent<HeroNetwork>().menu.GetComponent<campamentos>().nace = 0;
							Player1.GetComponent<HeroNetwork>().menu.GetComponent<campamentos>().nacer();

							Player1.GetComponent<HeroNetwork>().rematch = -1;
							Player1.GetComponent<HeroNetwork>().continuar = false;
							Player1.GetComponent<HeroNetwork>().ventanaRematch.SetActive(false);

							Player2.GetComponent<HeroNetwork>().rematch = -1;

							cargar = true;

							ResetValues();
							CmdResetCliente();
						}
					}else
					{
						if(!cargar)
						{
							CmdLobbyCliente();

							RegresaLobby.GetComponent<regresaLobby>().retirada = true;
							NetworkManager.singleton.StopHost();
							NetworkManager.singleton.StopClient();

							Application.LoadLevel("Load");
							loading.nombre = "Lobby";
							cargar = true;
						}
					}
				}

				if(!subirpuntaje)
				{
					if(XPTotal > 0)
					{
						UploadScoreToLeaderboard();
					}

					subirpuntaje = true;
				}
			}
		}else//PLAYER 2 "CLIENTE"
		{
			TotalM = KillsM2+VechicleDestroyedM2+StolenCardsM2+CapturedFlagsM2+BaseDestroyedM2+vecesTomadaAlphaM2+vecesTomadaBetaM2;
			TotalMFinal = TotalM-DeadsM2;
			TotalMT.text = TotalMFinal.ToString();

			if(sumatoria)
			{
				if(!sumarmatados)
				{
					int matados = PlayerPrefs.GetInt("Kills")+KillsM;
					PlayerPrefs.SetInt("Kills", matados);

					sumarmatados = true;
				}

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
					audio1.Stop();
					audio2.Play();
					TotalMFinalFINAL = TotalMFinal;
					sumatoria8 = false;
					experiencia = true;
				}
				DeadsMT.text = "-"+DeadsM2.ToString();
			}
			if(experiencia)
			{
				XPActualM += 30;
				XPTotal += 30;
				PlayerPrefs.SetFloat("PlayerEXTotal", XPTotal);
				TotalMFinalFINAL -= 30;
				if(TotalMFinalFINAL <= 0)//if(XPActualM >= XPM+TotalMFinal)
				{
					//XPActualM = XPM+TotalMFinal;
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
				PlayerPrefs.SetFloat("PlayerEX",0);//RESET EXPERIENCIA EN CADA NIVEL
				XPActualM = 0;
			}

			if(LevelUpNext)
			{
				XPNext = level1Next*10000*level1Next/4;

				PlayerPrefs.SetInt("PlayerLevel",level1+1);
				level1Next = level1+1;

				rangoUP.SetActive(true);
				rangoUP.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "levelUp", false);
				rangoUP.GetComponent<AudioSource>().Play();
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
						//TODO SE RESETEA DESDE EL SERVIDOR
					}else
					{
						//todo se hace desde el servidor
					}
				}
				if(!subirpuntaje)
				{
					if(XPTotal > 0)
					{
						UploadScoreToLeaderboard();
					}
					subirpuntaje = true;
				}
			}
		}
	}
	//SUBIR A BASES DE DATOS DE STEAM
	public bool sumarmatados;
	public bool subirpuntaje;

	public void UploadScoreToLeaderboard()
	{
		int score = Mathf.RoundToInt(XPTotal);
		string lbid = "BestSoldiers";

		EasySteamLeaderboards.Instance.UploadScoreToLeaderboard(lbid, score, (result) =>
			{
				//check if leaderboard successfully fetched
				if (result.resultCode == ESL_ResultCode.Success)
				{
					Debug.Log("Succesfully Uploaded!");
					if(PlayerPrefs.GetInt("Kills") > 0)
					{
						UploadScoreToLeaderboardKills();
					}
				}
				else
				{
					Debug.Log("Failed Uploading: " + result.resultCode.ToString());
					StopAllCoroutines();
				}
			});
	}
	public void UploadScoreToLeaderboardKills()
	{
		int score = PlayerPrefs.GetInt("Kills");
		string lbid = "BestKills";

		EasySteamLeaderboards.Instance.UploadScoreToLeaderboard(lbid, score, (result) =>
			{
				//check if leaderboard successfully fetched
				if (result.resultCode == ESL_ResultCode.Success)
				{
					Debug.Log("Succesfully Uploaded!");
					if(PlayerPrefs.GetInt("Banderas") > 0)
					{
						UploadScoreToLeaderboardFlags();
					}

				}
				else
				{
					Debug.Log("Failed Uploading: " + result.resultCode.ToString());
					StopAllCoroutines();
				}
			});
	}
	public void UploadScoreToLeaderboardFlags()
	{
		int score = PlayerPrefs.GetInt("Banderas");
		string lbid = "BestFlags";

		EasySteamLeaderboards.Instance.UploadScoreToLeaderboard(lbid, score, (result) =>
			{
				//check if leaderboard successfully fetched
				if (result.resultCode == ESL_ResultCode.Success)
				{
					Debug.Log("Succesfully Uploaded!");
					if(PlayerPrefs.GetInt("Bases") > 0)
					{
						UploadScoreToLeaderboardBases();
					}

				}
				else
				{
					Debug.Log("Failed Uploading: " + result.resultCode.ToString());
					StopAllCoroutines();
				}
			});
	}
	public void UploadScoreToLeaderboardBases()
	{
		int score = PlayerPrefs.GetInt("Bases");
		string lbid = "BestBases";

		EasySteamLeaderboards.Instance.UploadScoreToLeaderboard(lbid, score, (result) =>
			{
				//check if leaderboard successfully fetched
				if (result.resultCode == ESL_ResultCode.Success)
				{
					Debug.Log("Succesfully Uploaded!");
					if(PlayerPrefs.GetInt("Victorias") > 0)
					{
						UploadScoreToLeaderboardWins();
					}
				}
				else
				{
					Debug.Log("Failed Uploading: " + result.resultCode.ToString());
					StopAllCoroutines();
				}
			});
	}
	public void UploadScoreToLeaderboardWins()
	{
		int score = PlayerPrefs.GetInt("Victorias");
		string lbid = "BestWins";

		EasySteamLeaderboards.Instance.UploadScoreToLeaderboard(lbid, score, (result) =>
			{
				//check if leaderboard successfully fetched
				if (result.resultCode == ESL_ResultCode.Success)
				{
					Debug.Log("Succesfully Uploaded!");
				}
				else
				{
					Debug.Log("Failed Uploading: " + result.resultCode.ToString());
					StopAllCoroutines();
				}
			});
	}
	//SUBIR A BASES DE DATOS DE STEAM
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
		//Ganador.SetActive(false);
		//Perdedor.SetActive(false);
		//Empate.SetActive(false);
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
		if(destruccion != null)
		{
			destruccion.SetActive(true);
		}else if(destruccion2 != null)
		{
			destruccion2.SetActive(true);
		}else if(destruccion3 != null)
		{
			destruccion3.SetActive(true);
		}else if(destruccion4 != null)
		{
			destruccion4.SetActive(true);
		}
		/*if(destruccion.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0).Animation.Name != "explosion")
		{
			destruccion.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "explosion", false);
		}*/
		StartCoroutine(ultimo());
	}

	IEnumerator ultimo ()
	{
		if(destruccion != null)
		{
			yield return new WaitForSpineAnimationComplete(destruccion.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0));
			Destroy(destruccion);
		}else if(destruccion2 != null)
		{
			yield return new WaitForSpineAnimationComplete(destruccion2.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0));
			Destroy(destruccion2);
		}else if(destruccion3 != null)
		{
			yield return new WaitForSpineAnimationComplete(destruccion3.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0));
			Destroy(destruccion3);
		}else if(destruccion4 != null)
		{
			yield return new WaitForSpineAnimationComplete(destruccion4.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0));
			Destroy(destruccion4);
		}
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

	public void ResetValues()
	{
		Player1.GetComponent<HeroNetwork>().barra.GetComponent<barra>().fill = 0;
		
		Falta = 420;
		continuar = false;
		bajartiempo = false;

		//sleccionFinal = 30;
		rematchS = -1;
		rematchC = -1;
		cargar = false;
		cargar2 = false;

		rangoUP.SetActive(false);

		//SANGRE BASE BUENA
		BaseB.GetComponent<Base>().sangre = BaseB.GetComponent<Base>().saludMax;
		BaseB.GetComponent<Animator>().SetBool("muere", false);
		BaseB.GetComponent<Base>().matada = false;
		BaseB.GetComponent<Base>().fuego1.SetActive(false);
		BaseB.GetComponent<Base>().fuego2.SetActive(false);
		BaseB.GetComponent<Base>().fuego3.SetActive(false);
		BaseB.GetComponent<Base>().fuego4.SetActive(false);
		BaseB.GetComponent<Base>().destruida.SetActive(false);


		//SANGRE BASE MALA
		BaseM.GetComponent<Base>().sangre = BaseM.GetComponent<Base>().saludMax;
		BaseM.GetComponent<Animator>().SetBool("muere", false);
		BaseM.GetComponent<Base>().matada = false;
		BaseM.GetComponent<Base>().fuego1.SetActive(false);
		BaseM.GetComponent<Base>().fuego2.SetActive(false);
		BaseM.GetComponent<Base>().fuego3.SetActive(false);
		BaseM.GetComponent<Base>().fuego4.SetActive(false);
		BaseM.GetComponent<Base>().destruida.SetActive(false);

		//ESTADISTICAS JUGADOR 1
		KillsB = 0;
		DeadsB = 0;
		VechicleDestroyedB = 0;
		StolenCardsB = 0;
		CapturedFlagsAB = 0;
		CapturedFlagsBB = 0;
		CapturedFlagsB = 0;
		BaseDestroyedB = 0;
		TotalB = 0;
		TotalBFinal = 0;
		KillsB2 = 0;
		VechicleDestroyedB2 = 0;
		StolenCardsB2 = 0;
		CapturedFlagsB2 = 0;
		vecesTomadaAlphaB2 = 0;
		vecesTomadaBetaB2 = 0;
		BaseDestroyedB2 = 0;
		DeadsB2 = 0;

		//ESTADISTICAS JUGADOR 2
		KillsM = 0;
		DeadsM = 0;
		VechicleDestroyedM = 0;
		StolenCardsM = 0;
		CapturedFlagsAM = 0;
		CapturedFlagsBM = 0;
		CapturedFlagsM = 0;
		BaseDestroyedM = 0;
		TotalM = 0;
		TotalMFinal = 0;
		KillsM2 = 0;
		VechicleDestroyedM2 = 0;
		StolenCardsM2 = 0;
		vecesTomadaAlphaM2 = 0;
		vecesTomadaBetaM2 = 0;
		CapturedFlagsM2 = 0;
		BaseDestroyedM2 = 0;
		DeadsM2 = 0;

		explotar = false;
		Player1.GetComponent<HeroNetwork>().SniperCam.GetComponent<CamNetwork>().ver = false;
		Player1.GetComponent<HeroNetwork>().SniperCam.GetComponent<CamNetwork>().objetivo = false;

		iconos.SetActive(true);

		sumatoria = false;
		sumatoria2 = false;
		sumatoria3 = false;
		sumatoria4 = false;
		sumatoria5 = false;
		sumatoria6 = false;
		sumatoria7 = false;
		sumatoria8 = false;

		sumarmatados = false;
		subirpuntaje = false;

		Baul.SetActive(false);

		sumar1Server = false;
		sumar2Server = false;
		sumar3Server = false;
		sumar1Cliente = false;
		sumar2Cliente = false;
		sumar3Cliente = false;

		Ganador1.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "nada", false);
		Perdedor1.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "nada", false);
		Ganador2.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "nada", false);
		Perdedor2.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "nada", false);

		Ganador1.GetComponent<combinedSkins>().skinsToCombine[0] = "default";
		Perdedor1.GetComponent<combinedSkins>().skinsToCombine[0] = "default";
		Ganador2.GetComponent<combinedSkins>().skinsToCombine[0] = "default";
		Perdedor2.GetComponent<combinedSkins>().skinsToCombine[0] = "default";

		Ganador1.SetActive(false);
		Perdedor1.SetActive(false);

		Ganador2.SetActive(false);
		Perdedor2.SetActive(false);

		Empate.SetActive(false);

		ponerVictoria = false;
		ponerVictoria2 = false;

		titulos = false;
		titulos2 = false;
		titulos3 = false;

		final = false;
		final2 = false;

		finalizado = false;

		//BASE ALPHA
		Alpha.GetComponent<BaseNeutraNetwork>().puntosTotales = 0;
		Alpha.GetComponent<BaseNeutraNetwork>().veces = 0;
		Alpha.GetComponent<BaseNeutraNetwork>().vecesmala = 0;
		Alpha.GetComponent<BaseNeutraNetwork>().tomada = "newtra";

		Alpha.GetComponent<BaseNeutraNetwork>().ajuste = 0;

		AlphaTomada = "";

		AlphaB.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "no", false);
		AlphaM.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "no", false);

		vecesTomadaAlphaB = 0;
		vecesTomadaAlphaM = 0;

		//BASE BETA
		if(Beta != null)
		{
			Beta.GetComponent<BaseNeutraNetwork>().puntosTotales = 0;
			Beta.GetComponent<BaseNeutraNetwork>().veces = 0;
			Beta.GetComponent<BaseNeutraNetwork>().vecesmala = 0;
			Beta.GetComponent<BaseNeutraNetwork>().tomada = "newtra";
			Beta.tag = "newtra";

			Beta.GetComponent<BaseNeutraNetwork>().ajuste = 0;
		}

		BetaTomada = "";

		BetaB.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "no", false);
		BetaM.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "no", false);

		vecesTomadaBetaB = 0;
		vecesTomadaBetaM = 0;

		End.SetActive(false);
		arriba.SetActive(true);

		explotar = false;
		Player1.GetComponent<HeroNetwork>().SniperCam.GetComponent<CamNetwork>().ver = false;
		muerte = false;
		explotar = false;
		Player1.GetComponent<HeroNetwork>().SniperCam.GetComponent<CamNetwork>().ver = false;
		Player1.GetComponent<HeroNetwork>().vivo = true;

		musica.GetComponent<AudioSource>().Play();
	}
	[Command]
	public void CmdResetCliente()
	{
		RpcResetValuesCliente();
	}
	[ClientRpc]
	public void RpcResetValuesCliente()
	{
		Player2.GetComponent<HeroNetwork>().barra.GetComponent<barra>().fill = 0;

		Player2.GetComponent<HeroNetwork>().menu.GetComponent<campamentos>().nace = 0;
		Player2.GetComponent<HeroNetwork>().menu.GetComponent<campamentos>().nacer();

		Player2.GetComponent<HeroNetwork>().rematch = -1;
		Player2.GetComponent<HeroNetwork>().continuar = false;
		Player2.GetComponent<HeroNetwork>().ventanaRematch.SetActive(false);

		Player1.GetComponent<HeroNetwork>().rematch = -1;


		Falta = 420;
		continuar = false;
		bajartiempo = false;

		sumarmatados = false;
		subirpuntaje = false;

		//sleccionFinal = 30;
		rematchS = -1;
		rematchC = -1;
		cargar = false;
		cargar2 = false;

		rangoUP.SetActive(false);

		//SANGRE BASE BUENA
		BaseB.GetComponent<Base>().sangre = BaseB.GetComponent<Base>().saludMax;
		BaseB.GetComponent<Animator>().SetBool("muere", false);
		BaseB.GetComponent<Base>().matada = false;
		BaseB.GetComponent<Base>().fuego1.SetActive(false);
		BaseB.GetComponent<Base>().fuego2.SetActive(false);
		BaseB.GetComponent<Base>().fuego3.SetActive(false);
		BaseB.GetComponent<Base>().fuego4.SetActive(false);
		BaseB.GetComponent<Base>().destruida.SetActive(false);


		//SANGRE BASE MALA
		BaseM.GetComponent<Base>().sangre = BaseM.GetComponent<Base>().saludMax;
		BaseM.GetComponent<Animator>().SetBool("muere", false);
		BaseM.GetComponent<Base>().matada = false;
		BaseM.GetComponent<Base>().fuego1.SetActive(false);
		BaseM.GetComponent<Base>().fuego2.SetActive(false);
		BaseM.GetComponent<Base>().fuego3.SetActive(false);
		BaseM.GetComponent<Base>().fuego4.SetActive(false);
		BaseM.GetComponent<Base>().destruida.SetActive(false);

		//ESTADISTICAS JUGADOR 1
		KillsB = 0;
		DeadsB = 0;
		VechicleDestroyedB = 0;
		StolenCardsB = 0;
		CapturedFlagsAB = 0;
		CapturedFlagsBB = 0;
		CapturedFlagsB = 0;
		BaseDestroyedB = 0;
		TotalB = 0;
		TotalBFinal = 0;
		KillsB2 = 0;
		VechicleDestroyedB2 = 0;
		StolenCardsB2 = 0;
		CapturedFlagsB2 = 0;
		vecesTomadaAlphaB2 = 0;
		vecesTomadaBetaB2 = 0;
		BaseDestroyedB2 = 0;
		DeadsB2 = 0;

		//ESTADISTICAS JUGADOR 2
		KillsM = 0;
		DeadsM = 0;
		VechicleDestroyedM = 0;
		StolenCardsM = 0;
		CapturedFlagsAM = 0;
		CapturedFlagsBM = 0;
		CapturedFlagsM = 0;
		BaseDestroyedM = 0;
		TotalM = 0;
		TotalMFinal = 0;
		KillsM2 = 0;
		VechicleDestroyedM2 = 0;
		StolenCardsM2 = 0;
		vecesTomadaAlphaM2 = 0;
		vecesTomadaBetaM2 = 0;
		CapturedFlagsM2 = 0;
		BaseDestroyedM2 = 0;
		DeadsM2 = 0;

		explotar = false;
		Player2.GetComponent<HeroNetwork>().SniperCam.GetComponent<CamNetwork>().ver = false;
		Player2.GetComponent<HeroNetwork>().SniperCam.GetComponent<CamNetwork>().objetivo = false;
		muerte = false;

		iconos.SetActive(true);

		sumatoria = false;
		sumatoria2 = false;
		sumatoria3 = false;
		sumatoria4 = false;
		sumatoria5 = false;
		sumatoria6 = false;
		sumatoria7 = false;
		sumatoria8 = false;

		sumarmatados = false;
		subirpuntaje = false;

		Baul.SetActive(false);

		sumar1Server = false;
		sumar2Server = false;
		sumar3Server = false;
		sumar1Cliente = false;
		sumar2Cliente = false;
		sumar3Cliente = false;

		Ganador1.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "nada", false);
		Perdedor1.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "nada", false);
		Ganador2.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "nada", false);
		Perdedor2.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "nada", false);

		Ganador1.GetComponent<combinedSkins>().skinsToCombine[0] = "default";
		Perdedor1.GetComponent<combinedSkins>().skinsToCombine[0] = "default";
		Ganador2.GetComponent<combinedSkins>().skinsToCombine[0] = "default";
		Perdedor2.GetComponent<combinedSkins>().skinsToCombine[0] = "default";

		Ganador1.SetActive(false);
		Perdedor1.SetActive(false);

		Ganador2.SetActive(false);
		Perdedor2.SetActive(false);

		Empate.SetActive(false);

		ponerVictoria = false;
		ponerVictoria2 = false;

		titulos = false;
		titulos2 = false;
		titulos3 = false;

		final = false;
		final2 = false;

		finalizado = false;

		//BASE ALPHA
		Alpha.GetComponent<BaseNeutraNetwork>().puntosTotales = 0;
		Alpha.GetComponent<BaseNeutraNetwork>().veces = 0;
		Alpha.GetComponent<BaseNeutraNetwork>().vecesmala = 0;
		Alpha.GetComponent<BaseNeutraNetwork>().tomada = "newtra";

		Alpha.GetComponent<BaseNeutraNetwork>().ajuste = 0;

		AlphaTomada = "";

		AlphaB.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "no", false);
		AlphaM.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "no", false);

		vecesTomadaAlphaB = 0;
		vecesTomadaAlphaM = 0;

		//BASE BETA
		if(Beta != null)
		{
			Beta.GetComponent<BaseNeutraNetwork>().puntosTotales = 0;
			Beta.GetComponent<BaseNeutraNetwork>().veces = 0;
			Beta.GetComponent<BaseNeutraNetwork>().vecesmala = 0;
			Beta.GetComponent<BaseNeutraNetwork>().tomada = "newtra";
			Beta.tag = "newtra";

			Beta.GetComponent<BaseNeutraNetwork>().ajuste = 0;
		}

		BetaTomada = "";

		BetaB.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "no", false);
		BetaM.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "no", false);

		vecesTomadaBetaB = 0;
		vecesTomadaBetaM = 0;

		End.SetActive(false);
		arriba.SetActive(true);

		explotar = false;
		Player2.GetComponent<HeroNetwork>().SniperCam.GetComponent<CamNetwork>().ver = false;
		muerte = false;
		explotar = false;
		Player2.GetComponent<HeroNetwork>().SniperCam.GetComponent<CamNetwork>().ver = false;
		Player2.GetComponent<HeroNetwork>().vivo = true;

		musica.GetComponent<AudioSource>().Play();
	}

	[Command]
	public void CmdLobbyCliente()
	{
		RpcLobbyCliente();
	}
	[ClientRpc]
	public void RpcLobbyCliente()
	{
		RegresaLobby.GetComponent<regresaLobby>().retirada = true;
		cargar = true;
	}
}