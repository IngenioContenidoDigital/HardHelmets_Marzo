﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityStandardAssets.ImageEffects;

public class GameOffline : MonoBehaviour {


	public GameObject Player1;

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

	public GameObject marine1;
	public GameObject marine2;


	public bool final;
	public bool final2;

	//TIEMPO DE PARTIDA

	public float Falta;

	public UnityEngine.UI.Text Tiempo;

	//NOMBRES
	public string nombre1;
	public UnityEngine.UI.Text nombres1;
	public string nombre2;
	public UnityEngine.UI.Text nombres2;

	//NIVELES
	public UnityEngine.UI.Text nivel1;

	public int level1;

	public int level1Next;

	public UnityEngine.UI.Text nivel2;

	public int level2;

	public int level2Next;


	public string Armas1 = "pistola";
	public GameObject ArmasB;


	public string Armas2 = "pistola";
	public GameObject ArmasM;

	//SANGRE BASE BUENA
	public GameObject BaseB;

	public float sagreBB;
	public UnityEngine.UI.Image saludB;

	//SANGRE BASE MALA
	public GameObject BaseM;

	public float sagreBM;
	public UnityEngine.UI.Image saludM;

	//BASE ALPHA
	public GameObject Alpha;
	public GameObject AlphaB;
	public GameObject AlphaM;

	public string AlphaTomada;

	public int vecesTomada;
	public int vecesTomada2;

	//BASE BETA
	public GameObject Beta;
	public GameObject BetaB;
	public GameObject BetaM;

	public string BetaTomada;

	//ESTADISTICAS JUGADOR 1

	public int KillsB;
	public UnityEngine.UI.Text KillsBT;

	public int DeadsB;
	public UnityEngine.UI.Text DeadsBT;

	public int VechicleDestroyedB;
	public UnityEngine.UI.Text VechicleDestroyedBT;

	public int StolenCardsB;
	public UnityEngine.UI.Text StolenCardsBT;

	public int CapturedFlagsAB;

	public int CapturedFlagsBB;

	public int CapturedFlagsB;
	public UnityEngine.UI.Text CapturedFlagsBT;

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

	//ESTADISTICAS JUGADOR 2

	public int KillsM;
	public UnityEngine.UI.Text KillsMT;

	public int DeadsM;
	public UnityEngine.UI.Text DeadsMT;

	public int VechicleDestroyedM;
	public UnityEngine.UI.Text VechicleDestroyedMT;

	public int StolenCardsM;
	public UnityEngine.UI.Text StolenCardsMT;

	public int CapturedFlagsAM;

	public int CapturedFlagsBM;

	public int CapturedFlagsM;
	public UnityEngine.UI.Text CapturedFlagsMT;

	public int BaseDestroyedM;
	public UnityEngine.UI.Text BaseDestroyedMT;

	public int TotalM;

	public UnityEngine.UI.Text TotalMT;

	public int XPActualM;
	public UnityEngine.UI.Text XPMT;


	int KillsB2;

	int VechicleDestroyedB2;

	int StolenCardsB2;

	int vecesTomadab;
	int vecesTomada2b;

	int CapturedFlagsB2;

	int BaseDestroyedB2;

	int DeadsB2;

	public GameObject Bueno;
	public GameObject Malo;

	public bool explotar;

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

	// Use this for initialization
	void Start ()
	{
		XP = PlayerPrefs.GetFloat("PlayerEX");
		XPActual = XP;
		XPT.text = XP.ToString();

		XPNext = PlayerPrefs.GetInt("PlayerLevel")*10000*1.5f;

		Exp.fillAmount = XPActual/XPNext;

		gameObject.name = "GAME";

		nombre1 = PlayerPrefs.GetString("SteamName");
		nombres1.text = nombre1;
		marine1.GetComponent<combinedSkins>().skinsToCombine[0] = PlayerPrefs.GetInt("PlayerLevel").ToString();

		nombre2 = PlayerPrefs.GetString("nameCommunity");
		nombres2.text = nombre2;
		level2 = PlayerPrefs.GetInt("levelCommunity");
		marine2.GetComponent<combinedSkins>().skinsToCombine[0] = PlayerPrefs.GetInt("levelCommunity").ToString();

		cofre = PlayerPrefs.GetInt("caja1");
		banderasCofre = PlayerPrefs.GetInt("banderasCofre");
	}

	// SOLO EL SERVIDOR
	void Update ()
	{
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
				if(Player1.GetComponent<Hero>().arma == "")
				{
					Armas1 = "pistola";
				}else
				{
					Armas1 = Player1.GetComponent<Hero>().arma;
				}
			}
		}

		if(listoTodos)
		{
			level1 = PlayerPrefs.GetInt("PlayerLevel");
			level1Next = level1+1;

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
				sagreBB = BaseB.GetComponent<baseOffline>().sangre/2000;
			}

			if(BaseM == null)
			{
				BaseM = GameObject.Find("BASE MALA");
			}else
			{
				sagreBM =  BaseM.GetComponent<baseOffline>().sangre/2000;
			}

			if(Falta > 0 && Application.loadedLevelName != "Tutorial")
			{
				Falta -= Time.deltaTime;
			}

			if(Falta <= 0 && !final && !muerte)
			{
				Player1.GetComponent<Hero>().SniperCam.GetComponent<Grayscale>().enabled = true;
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
			}
			vecesTomada = Alpha.GetComponent<BaseNeutra>().veces;
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
				vecesTomada2 = Beta.GetComponent<BaseNeutra>().veces;
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

	public bool sumar1;
	public bool sumar2;
	public bool sumar3;

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
				if(Player2.GetComponent<Hero>().arma == "")
				{
					Armas2 = "pistola";
				}else
				{
					Armas2 = Player2.GetComponent<Hero>().arma;
				}
			}
			if(Arma2B != Armas2)
			{
				Arma2B = Armas2;
			}
		}

		if(listoTodos)
		{
			esperando.SetActive(false);
		}else
		{
			//esperando.SetActive(true);
		}

		if(Player1 != null && !listo)
		{
			if(Player1.GetComponent<Hero>().saludMax > 100)
			{
				listoTodos = true;
				//Player1.GetComponent<Hero>().ready = true;
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
		nivel1.text = ""+level1;
		nivel2.text = ""+level2;

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
			banderaBuena.SetActive(true);

			if(sagreBM <= 0)
			{
				Ganador.SetActive(true);
			}else if(sagreBB <= 0)
			{
				Perdedor.SetActive(true);
			}else if(CapturedFlagsB > CapturedFlagsM)
			{
				Ganador.SetActive(true);
			}else if(CapturedFlagsB < CapturedFlagsM)
			{
				Perdedor.SetActive(true);
			}else if(CapturedFlagsB == CapturedFlagsM)
			{
				Empate.SetActive(true);
			}
		}

		if(final && final2)
		{
			End.SetActive(true);

			Player1.GetComponent<Hero>().SniperCam.GetComponent<Grayscale>().enabled = true;

			if(Player1.tag == "Player")
			{
				Bueno.SetActive(true);

				int siguiente = PlayerPrefs.GetInt("PlayerLevel")+1;
				rango1.GetComponent<combinedSkins>().skinsToCombine[0] = PlayerPrefs.GetInt("PlayerLevel").ToString();
				rango2.GetComponent<combinedSkins>().skinsToCombine[0] = siguiente.ToString();

				if(AlphaTomada == "Buena")
				{
					if(Application.loadedLevelName != "Tutorial")
					{
						if(!sumar1)
						{
							int sumarBase = PlayerPrefs.GetInt("Banderas")+1;
							PlayerPrefs.SetInt("Banderas", sumarBase);
							int sumarCofre = PlayerPrefs.GetInt("banderasCofre")+1;
							PlayerPrefs.SetInt("banderasCofre", sumarCofre);

							banderasCofre = PlayerPrefs.GetInt("banderasCofre");

							sumar1 = true;
						}
					}
					StartCoroutine(EspSale1());
					//Medalla1.SetActive(true);
					//Medalla1.GetComponent<combinedSkins>().skinsToCombine[0] = "bueno";
				}
				if(BetaTomada == "Buena")
				{
					if(Application.loadedLevelName != "Tutorial")
					{
						if(!sumar2)
						{
							int sumarBase = PlayerPrefs.GetInt("Banderas")+1;
							PlayerPrefs.SetInt("Banderas", sumarBase);
							int sumarCofre = PlayerPrefs.GetInt("banderasCofre")+1;
							PlayerPrefs.SetInt("banderasCofre", sumarCofre);

							banderasCofre = PlayerPrefs.GetInt("banderasCofre");

							sumar2 = true;
						}
					}
					if(AlphaTomada == "Buena")
					{
						StartCoroutine(EspSale2());
					}else
					{
						StartCoroutine(EspSale2b());
					}

					//Medalla2.SetActive(true);
					//Medalla2.GetComponent<combinedSkins>().skinsToCombine[0] = "bueno";
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
					if(Application.loadedLevelName != "Tutorial")
					{
						if(!sumar3)
						{
							int sumarBandera = PlayerPrefs.GetInt("Bases")+1;
							PlayerPrefs.SetInt("Bases", sumarBandera);

							sumar3 = true;
						}
					}
					BaseDestroyedB = 1;
					//MedallaTorre.SetActive(true);
					//MedallaTorre.GetComponent<combinedSkins>().skinsToCombine[0] = "torreBueno";
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
			StartCoroutine(muereBase());
			explotar = false;
		}

		saludB.fillAmount = sagreBB;
		saludM.fillAmount = sagreBM;

		if(sagreBB <= 0)
		{
			if(Application.loadedLevelName == "ComunityMatch0")
			{
				posicion = new Vector3(BaseB.transform.position.x+7, BaseB.transform.position.y+2, BaseB.transform.position.z-50);
			}else
			{
				posicion = new Vector3(BaseB.transform.position.x+9, BaseB.transform.position.y+8, BaseB.transform.position.z-68);
			}
			muerte = true;
		}
		if(sagreBM <= 0)
		{
			posicion = new Vector3(BaseM.transform.position.x-7, BaseM.transform.position.y+2, BaseM.transform.position.z-50);
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

		TotalB = KillsB2+VechicleDestroyedB2+StolenCardsB2+CapturedFlagsB2+BaseDestroyedB2+vecesTomadab+vecesTomada2b;
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
			vecesTomadab += 10;
			if(vecesTomadab >= vecesTomada*70)
			{
				vecesTomadab = vecesTomada*70;
				sumatoria6 = false;
				sumatoria7 = true;
			}
		}
		if(sumatoria7)
		{
			vecesTomada2b += 10;
			if(vecesTomada2b >= vecesTomada2*70)
			{
				vecesTomada2b = vecesTomada2*70;
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

				if(Application.loadedLevelName != "Tutorial")
				{
					PlayerPrefs.SetFloat("PlayerEX",XPActual);
				}
		
				experiencia = false;
				StartCoroutine(espContinuar());
			}

			XPT.text = XPActual.ToString();
			Exp.fillAmount = XPActual/XPNext;
		}

		if(XPActual >= XPNext)
		{
			if(Application.loadedLevelName != "Tutorial")
			{
				LevelUpNext = true;
			}
		}

		if(LevelUpNext)
		{
			XPNext = level1Next*10000*level1Next/4;

			if(Application.loadedLevelName != "Tutorial")
			{
				PlayerPrefs.SetInt("PlayerLevel",level1+1);
			}
			level1Next = level1+1;

			rangoUP.SetActive(true);
			rangoUP.GetComponent<combinedSkins>().skinsToCombine[0] = PlayerPrefs.GetInt("PlayerLevel").ToString();

			rango1.GetComponent<combinedSkins>().skinsToCombine[0] = PlayerPrefs.GetInt("PlayerLevel").ToString();
			rango2.GetComponent<combinedSkins>().skinsToCombine[0] = level1Next.ToString();

			LevelUpNext = false;
		}

		if(continuar)
		{
			siguiente.SetActive(true);
			if(Input.GetButtonDown("Submit") || Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.F))
			{
				if(Application.loadedLevelName == "Tutorial")
				{
					Application.LoadLevel("Load");
					loading.nombre = "menu";
				}else
				{
					Application.LoadLevel("Load");
					loading.nombre = "Comunity";
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
	//--ORDEN DE LAS MEDALLAS
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
}
