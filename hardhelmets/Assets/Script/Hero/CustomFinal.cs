using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine.Unity.Modules.AttachmentTools; 

public class CustomFinal : MonoBehaviour {

	public List<string> skinsToCombine;

	Spine.Skin combinedSkin;


	public string armaMano;

	public string armaEspalda;

	string cambio;

	string votar;
	public GameObject arma;
	//ARMAS
	public GameObject escopeta;
	public GameObject fusil;
	public GameObject submetra;
	public GameObject metra;
	public GameObject lansallamas;
	public GameObject sniper;
	//INSERVIBLES
	public GameObject pistola2;
	public GameObject escopeta2;
	public GameObject fusil2;
	public GameObject submetra2;
	public GameObject metra2;
	public GameObject lansallamas2;
	public GameObject sniper2;
	public GameObject panzer2;
	public Transform bulletSpawn;

	public Animator animator;

	public bool segunda = true;

	//SKINS
	public string casco;
	public string cara;
	public string mascara;
	public string uniforme;
	public string cuello;
	public string chaleco;
	public string maleta;
	public string cuerpo;

	public GameObject cabeza;

	public string skin;

	void Start ()
	{
		skin = PlayerPrefs.GetString("factionBuena");

		casco = PlayerPrefs.GetString("casco");
		cara = PlayerPrefs.GetString("cara");
		mascara = PlayerPrefs.GetString("mascara");
		uniforme = PlayerPrefs.GetString("uniforme");
		cuello = PlayerPrefs.GetString("cuello");
		chaleco = PlayerPrefs.GetString("chaleco");
		maleta = PlayerPrefs.GetString("maleta");
		cuerpo = "cuerpo1";

		GetComponent<Hero>().arma2 = true;
		skinsToCombine[8] = "gun";

		if(casco == "casco1")
		{
			cabeza.GetComponent<Cabeza>().numSombrero = 1;
		}
		if(casco == "casco2")
		{
			cabeza.GetComponent<Cabeza>().numSombrero = 2;
		}
		if(casco == "casco3")
		{
			cabeza.GetComponent<Cabeza>().numSombrero = 3;
		}
		if(casco == "casco4")
		{
			cabeza.GetComponent<Cabeza>().numSombrero = 4;
		}
		if(casco == "casco5")
		{
			cabeza.GetComponent<Cabeza>().numSombrero = 5;
		}
		if(casco == "casco6")
		{
			cabeza.GetComponent<Cabeza>().numSombrero = 6;
		}
	}

	public bool listo;
	//public GameObject Enemy;
	public bool bala;
	string cohete;

	bool esperar;
	bool esperarPanzer;

	bool poner;
	void FixedUpdate ()
	{
		if(!listo)
		{
			//if(gameObject.tag == "Player")
			//{
				casco = PlayerPrefs.GetString("casco");
				cara = PlayerPrefs.GetString("cara");
				mascara = PlayerPrefs.GetString("mascara");
				uniforme = PlayerPrefs.GetString("uniforme");
				cuello = PlayerPrefs.GetString("cuello");
				chaleco = PlayerPrefs.GetString("chaleco");
				maleta = PlayerPrefs.GetString("maleta");
				cuerpo = "cuerpo1";
			//}else
			/*{
				casco = PlayerPrefs.GetString("casco")+"b";
				cara = PlayerPrefs.GetString("cara");
				mascara = PlayerPrefs.GetString("mascara")+"b";
				uniforme = PlayerPrefs.GetString("uniforme")+"b";
				cuello = PlayerPrefs.GetString("cuello")+"b";
				chaleco = PlayerPrefs.GetString("chaleco")+"b";
				maleta = PlayerPrefs.GetString("maleta")+"b";
				cuerpo = "cuerpo1b";
			}*/

			skinsToCombine[0] = casco+skin;
			skinsToCombine[1] = cara;
			skinsToCombine[2] = mascara+skin;
			skinsToCombine[3] = uniforme+skin;
			skinsToCombine[4] = cuello+skin;
			skinsToCombine[5] = chaleco+skin;
			skinsToCombine[6] = maleta+skin;
			skinsToCombine[7] = "";
			skinsToCombine[8] = "gun";
			skinsToCombine[9] = cuerpo+skin;

			StartCoroutine(momento());
		}
		if(GetComponent<Hero>().salud <= GetComponent<Hero>().saludMax*40/100)
		{
			if(!poner)
			{
				skinsToCombine[12] = "cascado";
				poner = true;
			}
		}else if(poner)
		{
			skinsToCombine[12] = "";
			poner = false;
		}

		//pone skin granada
		if(GetComponent<Hero>().granadas >= 1)
		{
			skinsToCombine[10] = "granada";
		}else
		{
			skinsToCombine[10] = "";
		}
		//pone skin rocket
		if(GetComponent<Hero>().arma == "panzer")
		{
			/*if(!esperarPanzer)
			{
				esperarPanzer = true;
			}*/
			if(bala)
			{
				if(GetComponent<Hero>().balaPanzer >= 1)
				{
					cohete = "rocket";
					skinsToCombine[11] = "rocket";
				}else
				{
					cohete = "";
					skinsToCombine[11] = "";
				}

				bala = false;
			}
		}else if(cohete != "")
		{
			cohete = "";
			skinsToCombine[11] = "";
		}

		GetComponent<Hero>().arma = armaMano;

		if(Input.GetButtonUp("INTERAMBIO") && armaEspalda != "" && segunda && !animator.GetBool("cuchillando") && !animator.GetCurrentAnimatorStateInfo(0).IsName("backJump") && !animator.GetCurrentAnimatorStateInfo(0).IsName("lansallamasShot") && !animator.GetCurrentAnimatorStateInfo(0).IsName("cae"))
		{
			GetComponent<Hero>().rafaga = true;
			segunda = false;
			StartCoroutine(espera());
			cambio = armaMano;
			GetComponent<Hero>().rafaga = true;
			GetComponent<Hero>().arma2 = false;
			animator.SetBool("cambio", true);

			if(armaEspalda == "escopeta2")
			{
				armaEspalda = cambio+"2";
				armaMano = "escopeta";
			}else if(armaEspalda == "fusil2")
			{
				armaEspalda = cambio+"2";
				armaMano = "fusil";
			}else if(armaEspalda == "submetra2")
			{
				armaEspalda = cambio+"2";
				armaMano = "submetra";
			}else if(armaEspalda == "metra2")
			{
				armaEspalda = cambio+"2";
				armaMano = "metra";
			}else if(armaEspalda == "lansallamas2")
			{
				armaEspalda = cambio+"2";
				armaMano = "lansallamas";
			}else if(armaEspalda == "sniper2")
			{
				armaEspalda = cambio+"2";
				armaMano = "sniper";
			}else if(armaEspalda == "panzer2")
			{
				armaEspalda = cambio+"2";
				armaMano = "panzer";
				esperarPanzer = false;
				bala = true;
			}

			ArmaHand(armaMano);
			ArmaBack(armaEspalda);
		}

		//PONE PISTOLA
		if(armaMano == "escopeta" && GetComponent<Hero>().balaEscopeta <= 0 && GetComponent<Hero>().EscopetaTotales <= 0 && armaEspalda != "")
		{
			soltar();
			if(armaEspalda == "fusil2")
			{
				armaMano = "fusil";
				armaEspalda = "";
			}else if(armaEspalda == "submetra2")
			{
				armaMano = "submetra";
				armaEspalda = "";
			}else if(armaEspalda == "metra2")
			{
				armaMano = "metra";
				armaEspalda = "";
			}else if(armaEspalda == "lansallamas2")
			{
				armaMano = "lansallamas";
				armaEspalda = "";
			}else if(armaEspalda == "sniper2")
			{
				armaMano = "sniper";
				armaEspalda = "";
			}else if(armaEspalda == "panzer2")
			{
				armaMano = "panzer";
				armaEspalda = "";
				bala = true;
			}
		}else if(armaMano == "escopeta" && GetComponent<Hero>().balaEscopeta <= 0 && GetComponent<Hero>().EscopetaTotales <= 0 && armaEspalda == "")
		{
			soltar();
			armaMano = "";
			armaEspalda = "";

			GetComponent<Hero>().arma2 = true;
			skinsToCombine[8] = "gun";
		}
		if(armaMano == "fusil" && GetComponent<Hero>().balaFusil <= 0 && GetComponent<Hero>().FusilTotales <= 0 && armaEspalda != "")
		{
			soltar();
			if(armaEspalda == "escopeta2")
			{
				armaMano = "escopeta";
				armaEspalda = "";
			}else if(armaEspalda == "submetra2")
			{
				armaMano = "submetra";
				armaEspalda = "";
			}else if(armaEspalda == "metra2")
			{
				armaMano = "metra";
				armaEspalda = "";
			}else if(armaEspalda == "lansallamas2")
			{
				armaMano = "lansallamas";
				armaEspalda = "";
			}else if(armaEspalda == "sniper2")
			{
				armaMano = "sniper";
				armaEspalda = "";
			}else if(armaEspalda == "panzer2")
			{
				armaMano = "panzer";
				armaEspalda = "";
				bala = true;
			}
		}else if(armaMano == "fusil" && GetComponent<Hero>().balaFusil <= 0 && GetComponent<Hero>().FusilTotales <= 0 && armaEspalda == "")
		{
			soltar();
			armaMano = "";
			armaEspalda = "";

			GetComponent<Hero>().arma2 = true;
			skinsToCombine[8] = "gun";
		}
		if(armaMano == "submetra" && GetComponent<Hero>().balaSubmetra <= 0 && GetComponent<Hero>().SubmetraTotales <= 0 && armaEspalda != "")
		{
			soltar();
			if(armaEspalda == "fusil2")
			{
				armaMano = "fusil";
				armaEspalda = "";
			}else if(armaEspalda == "escopeta2")
			{
				armaMano = "escopeta";
				armaEspalda = "";
			}else if(armaEspalda == "metra2")
			{
				armaMano = "metra";
				armaEspalda = "";
			}else if(armaEspalda == "lansallamas2")
			{
				armaMano = "lansallamas";
				armaEspalda = "";
			}else if(armaEspalda == "sniper2")
			{
				armaMano = "sniper";
				armaEspalda = "";
			}else if(armaEspalda == "panzer2")
			{
				armaMano = "panzer";
				armaEspalda = "";
				bala = true;
			}
		}else if(armaMano == "submetra" && GetComponent<Hero>().balaSubmetra <= 0 && GetComponent<Hero>().SubmetraTotales <= 0 && armaEspalda == "")
		{
			soltar();
			armaMano = "";
			armaEspalda = "";

			GetComponent<Hero>().arma2 = true;
			skinsToCombine[8] = "gun";
		}
		if(armaMano == "metra" && GetComponent<Hero>().balaMetra <= 0 && GetComponent<Hero>().MetraTotales <= 0 && armaEspalda != "")
		{
			soltar();
			if(armaEspalda == "fusil2")
			{
				armaMano = "fusil";
				armaEspalda = "";
			}else if(armaEspalda == "submetra2")
			{
				armaMano = "submetra";
				armaEspalda = "";
			}else if(armaEspalda == "escopeta2")
			{
				armaMano = "escopeta";
				armaEspalda = "";
			}else if(armaEspalda == "lansallamas2")
			{
				armaMano = "lansallamas";
				armaEspalda = "";
			}else if(armaEspalda == "sniper2")
			{
				armaMano = "sniper";
				armaEspalda = "";
			}else if(armaEspalda == "panzer2")
			{
				armaMano = "panzer";
				armaEspalda = "";
				bala = true;
			}
		}else if(armaMano == "metra" && GetComponent<Hero>().balaMetra <= 0 && GetComponent<Hero>().MetraTotales <= 0 && armaEspalda == "")
		{
			soltar();
			armaMano = "";
			armaEspalda = "";

			GetComponent<Hero>().arma2 = true;
			skinsToCombine[8] = "gun";
		}

		if(armaMano == "lansallamas" && GetComponent<Hero>().balaLlamas <= 0 && armaEspalda != "")
		{
			GetComponent<Hero>().lansallamas = false;
			soltar();
			if(armaEspalda == "fusil2")
			{
				armaMano = "fusil";
				armaEspalda = "";
			}else if(armaEspalda == "submetra2")
			{
				armaMano = "submetra";
				armaEspalda = "";
			}else if(armaEspalda == "escopeta2")
			{
				armaMano = "escopeta";
				armaEspalda = "";
			}else if(armaEspalda == "metra2")
			{
				armaMano = "metra";
				armaEspalda = "";
			}else if(armaEspalda == "sniper2")
			{
				armaMano = "sniper";
				armaEspalda = "";
			}else if(armaEspalda == "panzer2")
			{
				armaMano = "panzer";
				armaEspalda = "";
				bala = true;
			}
		}else if(armaMano == "lansallamas" && GetComponent<Hero>().balaLlamas <= 0 && armaEspalda == "")
		{
			soltar();
			GetComponent<Hero>().lansallamas = false;
			armaMano = "";
			armaEspalda = "";

			GetComponent<Hero>().arma2 = true;
			skinsToCombine[8] = "gun";
		}

		if(armaMano == "sniper" && GetComponent<Hero>().balaSniper <= 0 && GetComponent<Hero>().SniperTotales <= 0 && armaEspalda != "")
		{
			if(!esperar)
			{
				StartCoroutine(esperaSniper2());
				esperar = true;
			}
			/*soltar();
			if(armaEspalda == "fusil2")
			{
				armaMano = "fusil";
				armaEspalda = "";
			}else if(armaEspalda == "submetra2")
			{
				armaMano = "submetra";
				armaEspalda = "";
			}else if(armaEspalda == "escopeta2")
			{
				armaMano = "escopeta";
				armaEspalda = "";
			}else if(armaEspalda == "metra2")
			{
				armaMano = "metra";
				armaEspalda = "";
			}else if(armaEspalda == "lansallamas2")
			{
				armaMano = "lansallamas";
				armaEspalda = "";
			}else if(armaEspalda == "panzer2")
			{
				armaMano = "panzer";
				armaEspalda = "";
				bala = true;
			}
			*/
		}else if(armaMano == "sniper" && GetComponent<Hero>().balaSniper <= 0 && GetComponent<Hero>().SniperTotales <= 0 && armaEspalda == "")
		{
			if(!esperar)
			{
				StartCoroutine(esperaSniper());
				esperar = true;
			}
			/*soltar();
			armaMano = "";
			armaEspalda = "";

			GetComponent<Hero>().arma2 = true;
			skinsToCombine[8] = "gun";
			*/
		}

		if(armaMano == "panzer" && GetComponent<Hero>().balaPanzer <= 0 && GetComponent<Hero>().PanzerTotales <= 0 && armaEspalda != "")
		{
			soltar();
			if(armaEspalda == "fusil2")
			{
				armaMano = "fusil";
				armaEspalda = "";
			}else if(armaEspalda == "submetra2")
			{
				armaMano = "submetra";
				armaEspalda = "";
			}else if(armaEspalda == "escopeta2")
			{
				armaMano = "escopeta";
				armaEspalda = "";
			}else if(armaEspalda == "metra2")
			{
				armaMano = "metra";
				armaEspalda = "";
			}else if(armaEspalda == "lansallamas2")
			{
				armaMano = "lansallamas";
				armaEspalda = "";
			}else if(armaEspalda == "sniper2")
			{
				armaMano = "sniper";
				armaEspalda = "";
			}
				
		}else if(armaMano == "panzer" && GetComponent<Hero>().balaPanzer <= 0 && GetComponent<Hero>().PanzerTotales <= 0 && armaEspalda == "")
		{
			soltar();
			armaMano = "";
			armaEspalda = "";

			GetComponent<Hero>().arma2 = true;
			skinsToCombine[8] = "gun";
		}

		if(armaMano == "")
		{
			/*GetComponent<Hero>().arma2 = true;
			skinsToCombine[8] = "gun";*/
		}else
		{
			GetComponent<Hero>().arma2 = false;
			skinsToCombine[8] = armaMano;
		}

		if(armaEspalda == "")
		{
			skinsToCombine[7] = "";
		}else
		{
			skinsToCombine[7] = armaEspalda;
		}

		if(armaMano == "lansallamas")
		{
			GetComponent<Hero>().lansallamas = true;
		}else
		{
			GetComponent<Hero>().lansallamas = false;
		}
		if(armaMano == "sniper")
		{
			GetComponent<Hero>().sniper = true;
		}else
		{
			GetComponent<Hero>().sniper = false;
		}


		var skeletonComponent = GetComponent<ISkeletonComponent>();
		if (skeletonComponent == null) return;
		var skeleton = skeletonComponent.Skeleton;
		if (skeleton == null) return;

		combinedSkin = combinedSkin ?? new Spine.Skin("combined");
		combinedSkin.Clear();
		foreach (var skinName in skinsToCombine) {
			var skin = skeleton.Data.FindSkin(skinName);
			if (skin != null) combinedSkin.Append(skin);
		}

		skeleton.SetSkin(combinedSkin);
		skeleton.SetToSetupPose();
		var animationStateComponent = skeletonComponent as IAnimationStateComponent;
		if (animationStateComponent != null) animationStateComponent.AnimationState.Apply(skeleton);
	}
	IEnumerator esperaSniper()
	{
		yield return new WaitForSeconds(0.7f);
		soltar();
		armaMano = "";
		armaEspalda = "";

		GetComponent<Hero>().arma2 = true;
		skinsToCombine[8] = "gun";
		esperar = false;
	}
	IEnumerator esperaSniper2()
	{
		yield return new WaitForSeconds(1f);
		soltar();
		if(armaEspalda == "fusil2")
		{
			armaMano = "fusil";
			armaEspalda = "";
		}else if(armaEspalda == "submetra2")
		{
			armaMano = "submetra";
			armaEspalda = "";
		}else if(armaEspalda == "escopeta2")
		{
			armaMano = "escopeta";
			armaEspalda = "";
		}else if(armaEspalda == "metra2")
		{
			armaMano = "metra";
			armaEspalda = "";
		}else if(armaEspalda == "lansallamas2")
		{
			armaMano = "lansallamas";
			armaEspalda = "";
		}else if(armaEspalda == "panzer2")
		{
			armaMano = "panzer";
			armaEspalda = "";
			bala = true;
		}
		esperar = false;
	}
	IEnumerator espera()
	{
		yield return new WaitForSeconds(0.3f);
		segunda = true;
	}
	IEnumerator momento()
	{
		yield return new WaitForSeconds(4f);
		listo = true;
	}
	void ArmaHand(string armaMano)
	{
		armaMano = armaMano;
		skinsToCombine[8] = armaMano;

		var skeletonComponent = GetComponent<ISkeletonComponent>();
		if (skeletonComponent == null) return;
		var skeleton = skeletonComponent.Skeleton;
		if (skeleton == null) return;

		combinedSkin = combinedSkin ?? new Spine.Skin("combined");
		combinedSkin.Clear();
		foreach (var skinName in skinsToCombine) {
			var skin = skeleton.Data.FindSkin(skinName);
			if (skin != null) combinedSkin.Append(skin);
		}

		skeleton.SetSkin(combinedSkin);
		skeleton.SetToSetupPose();
		var animationStateComponent = skeletonComponent as IAnimationStateComponent;
		if (animationStateComponent != null) animationStateComponent.AnimationState.Apply(skeleton);
	}

	void ArmaBack(string armaEspalda)
	{
		armaEspalda = armaEspalda;
		skinsToCombine[7] = armaEspalda;

		var skeletonComponent = GetComponent<ISkeletonComponent>();
		if (skeletonComponent == null) return;
		var skeleton = skeletonComponent.Skeleton;
		if (skeleton == null) return;

		combinedSkin = combinedSkin ?? new Spine.Skin("combined");
		combinedSkin.Clear();
		foreach (var skinName in skinsToCombine) {
			var skin = skeleton.Data.FindSkin(skinName);
			if (skin != null) combinedSkin.Append(skin);
		}

		skeleton.SetSkin(combinedSkin);
		skeleton.SetToSetupPose();
		var animationStateComponent = skeletonComponent as IAnimationStateComponent;
		if (animationStateComponent != null) animationStateComponent.AnimationState.Apply(skeleton);
	}

	void RecogeGun ()
	{
		//skinsToCombine[8] = armaMano;
		if(votar == "escopeta")
		{
			VotaEscopeta();
		}
		if(votar == "fusil")
		{
			VotaFusil();
		}
		if(votar == "submetra")
		{
			VotaSubmetra();
		}
		if(votar == "metra")
		{
			VotaMetra();
		}
		if(votar == "lansallamas")
		{
			VotaLansaLlamas();
		}
		if(votar == "sniper")
		{
			VotaSniper();
		}
		if(votar == "panzer")
		{
			VotaPanzer();
		}
	}

	void soltar()
	{
		if(armaMano == "escopeta")
		{
			VotaEscopeta();
		}
		if(armaMano == "fusil")
		{
			VotaFusil();
		}
		if(armaMano == "submetra")
		{
			VotaSubmetra();
		}
		if(armaMano == "metra")
		{
			VotaMetra();
		}
		if(armaMano == "lansallamas")
		{
			VotaLansaLlamas();
		}
		if(armaMano == "sniper")
		{
			VotaSniper();
		}
		if(armaMano == "panzer")
		{
			esperarPanzer = false;
			VotaPanzer();
		}
	}

	void tiraarma()
	{
		if(armaMano == "escopeta")
		{
			VotaEscopeta();
		}else if(armaMano == "fusil")
		{
			VotaFusil();
		}else if(armaMano == "submetra")
		{
			VotaSubmetra();
		}else if(armaMano == "metra")
		{
			VotaMetra();
		}else if(armaMano == "lansallamas")
		{
			VotaLansaLlamas();
		}else if(armaMano == "sniper")
		{
			VotaSniper();
		}else if(armaMano == "panzer")
		{
			VotaPanzer();
		}else
		{
			VotaPistola();
		}
	}
		
	public void VotaPistola()
	{
		var arma = (GameObject)Instantiate(pistola2, bulletSpawn.position, Quaternion.Euler(0,0,0)); 
		arma.GetComponent<Rigidbody>().velocity = arma.transform.up * 20;
		arma.GetComponent<Rigidbody>().velocity = arma.transform.right * 3;
		arma.GetComponent<Rigidbody>().AddTorque(transform.forward * 500 * 500);
		Destroy(arma, 2.0f);
	}
		
	public void VotaEscopeta()
	{
		var arma = (GameObject)Instantiate(escopeta2, bulletSpawn.position, Quaternion.Euler(0,0,0)); 
		arma.GetComponent<Rigidbody>().velocity = arma.transform.up * 20;
		arma.GetComponent<Rigidbody>().velocity = arma.transform.right * 3;
		arma.GetComponent<Rigidbody>().AddTorque(transform.forward * 500 * 500);
		Destroy(arma, 2.0f);
	}
	public void VotaFusil()
	{
		var arma = (GameObject)Instantiate(fusil2, bulletSpawn.position, Quaternion.Euler(0,0,0)); 
		arma.GetComponent<Rigidbody>().velocity = arma.transform.up * 20;
		arma.GetComponent<Rigidbody>().velocity = arma.transform.right * 3;
		arma.GetComponent<Rigidbody>().AddTorque(transform.forward * 500 * 500);
		Destroy(arma, 2.0f);
	}
	public void VotaSubmetra()
	{
		var arma = (GameObject)Instantiate(submetra2, bulletSpawn.position, Quaternion.Euler(0,0,0)); 
		arma.GetComponent<Rigidbody>().velocity = arma.transform.up * 20;
		arma.GetComponent<Rigidbody>().velocity = arma.transform.right * 3;
		arma.GetComponent<Rigidbody>().AddTorque(transform.forward * 500 * 500);
		Destroy(arma, 2.0f);
	}
	public void VotaMetra()
	{
		var arma = (GameObject)Instantiate(metra2, bulletSpawn.position, Quaternion.Euler(0,0,0)); 
		arma.GetComponent<Rigidbody>().velocity = arma.transform.up * 20;
		arma.GetComponent<Rigidbody>().velocity = arma.transform.right * 3;
		arma.GetComponent<Rigidbody>().AddTorque(transform.forward * 500 * 500);
		Destroy(arma, 2.0f);
	}
	public void VotaSniper()
	{
		var arma = (GameObject)Instantiate(sniper2, bulletSpawn.position, Quaternion.Euler(0,0,-90)); 
		arma.GetComponent<Rigidbody>().velocity = arma.transform.up * 20;
		arma.GetComponent<Rigidbody>().velocity = arma.transform.right * 3;
		arma.GetComponent<Rigidbody>().AddTorque(transform.forward * 500 * 500);
		Destroy(arma, 2.0f);
	}
	public void VotaLansaLlamas()
	{
		GetComponent<Hero>().balaLlamas = 0;
		var arma = (GameObject)Instantiate(lansallamas2, bulletSpawn.position, Quaternion.Euler(0,0,-90)); 
		arma.GetComponent<Rigidbody>().velocity = arma.transform.up * 20;
		arma.GetComponent<Rigidbody>().velocity = arma.transform.right * 3;
		arma.GetComponent<Rigidbody>().AddTorque(transform.forward * 500 * 500);
		Destroy(arma, 2.0f);
	}
	public void VotaPanzer()
	{
		var arma = (GameObject)Instantiate(panzer2, bulletSpawn.position, Quaternion.Euler(0,0,-90)); 
		arma.GetComponent<Rigidbody>().velocity = arma.transform.up * 20;
		arma.GetComponent<Rigidbody>().velocity = arma.transform.right * 3;
		arma.GetComponent<Rigidbody>().AddTorque(transform.forward * 500 * 500);
		Destroy(arma, 2.0f);
	}


	void CambiaGun ()
	{
		print("INTERCAMBIO");
		skinsToCombine[7] = armaEspalda;
		skinsToCombine[8] = armaMano;
	}

	void OnCollisionEnter (Collision col)//OnTriggerEnter (Collider col)//
	{
		if(col.gameObject.tag == "escopeta" || col.gameObject.tag == "fusil" || col.gameObject.tag == "submetra" || col.gameObject.tag == "metra" || col.gameObject.tag == "lansallamas" || col.gameObject.tag == "sniper" || col.gameObject.tag == "panzer")
		{
			GetComponent<Hero>().rafaga = true;
			GetComponent<Hero>().arma2 = false;
			votar = armaMano;

			if(armaMano == "")
			{
				if(col.gameObject.tag == "escopeta")
				{
					GetComponent<Hero>().balaEscopeta += 8;
					GetComponent<Hero>().EscopetaTotales += Random.Range(10,150);
					arma.GetComponent<UnlokArma>().escopeta = true;
				}
				if(col.gameObject.tag == "fusil")
				{
					GetComponent<Hero>().balaFusil += 5;
					GetComponent<Hero>().FusilTotales += Random.Range(10,150);
					arma.GetComponent<UnlokArma>().fusil = true;
				}
				if(col.gameObject.tag == "submetra")
				{
					GetComponent<Hero>().balaSubmetra += 30;
					GetComponent<Hero>().SubmetraTotales += Random.Range(10,150);
					arma.GetComponent<UnlokArma>().submetra = true;
				}
				if(col.gameObject.tag == "metra")
				{
					GetComponent<Hero>().balaMetra += 32;
					GetComponent<Hero>().MetraTotales += Random.Range(10,150);
					arma.GetComponent<UnlokArma>().metra = true;
				}
				if(col.gameObject.tag == "lansallamas")
				{
					GetComponent<Hero>().balaLlamas += 300;
					arma.GetComponent<UnlokArma>().llamas = true;
				}
				if(col.gameObject.tag == "sniper")
				{
					GetComponent<Hero>().SniperTotales += 5;
					GetComponent<Hero>().balaSniper = 1;
					arma.GetComponent<UnlokArma>().sniper = true;
				}
				if(col.gameObject.tag == "panzer")
				{
					GetComponent<Hero>().PanzerTotales += 5;
					GetComponent<Hero>().balaPanzer = 1;
					bala = true;
					arma.GetComponent<UnlokArma>().panzer = true;
				}
				armaMano = col.gameObject.tag;
			}else if(armaEspalda == "" && col.gameObject.tag != armaMano)
			{
				if(col.gameObject.tag == "escopeta")
				{
					GetComponent<Hero>().balaEscopeta += 8;
					GetComponent<Hero>().EscopetaTotales += Random.Range(1,49);
					arma.GetComponent<UnlokArma>().escopeta = true;
				}
				if(col.gameObject.tag == "fusil")
				{
					GetComponent<Hero>().balaFusil += 5;
					GetComponent<Hero>().FusilTotales += Random.Range(1,41);
					arma.GetComponent<UnlokArma>().fusil = true;
				}
				if(col.gameObject.tag == "submetra")
				{
					GetComponent<Hero>().balaSubmetra += 30;
					GetComponent<Hero>().SubmetraTotales += Random.Range(1,121);
					arma.GetComponent<UnlokArma>().submetra = true;
				}
				if(col.gameObject.tag == "metra")
				{
					GetComponent<Hero>().balaMetra += 32;
					GetComponent<Hero>().MetraTotales += Random.Range(1,181);
					arma.GetComponent<UnlokArma>().metra = true;
				}
				if(col.gameObject.tag == "lansallamas")
				{
					GetComponent<Hero>().balaLlamas += 300;
					arma.GetComponent<UnlokArma>().llamas = true;
				}
				if(col.gameObject.tag == "sniper")
				{
					GetComponent<Hero>().SniperTotales += 5;
					GetComponent<Hero>().balaSniper = 1;
					arma.GetComponent<UnlokArma>().sniper = true;
				}
				if(col.gameObject.tag == "panzer")
				{
					GetComponent<Hero>().PanzerTotales += 5;
					GetComponent<Hero>().balaPanzer = 1;
					bala = true;
					arma.GetComponent<UnlokArma>().panzer = true;
				}
				armaEspalda = col.gameObject.tag+"2";
			}

			if(armaMano != "" && armaEspalda != "")
			{
				if(col.gameObject.tag == armaMano || col.gameObject.tag+"2" == armaEspalda)
				{
					if(col.gameObject.tag == "escopeta")
					{
						GetComponent<Hero>().balaEscopeta += 8;
						GetComponent<Hero>().EscopetaTotales += Random.Range(1,49);
					}
					if(col.gameObject.tag == "fusil")
					{
						GetComponent<Hero>().balaFusil += 5;
						GetComponent<Hero>().FusilTotales += Random.Range(1,41);
					}
					if(col.gameObject.tag == "submetra")
					{
						GetComponent<Hero>().balaSubmetra += 30;
						GetComponent<Hero>().SubmetraTotales += Random.Range(1,121);
					}
					if(col.gameObject.tag == "metra")
					{
						GetComponent<Hero>().balaMetra += 32;
						GetComponent<Hero>().MetraTotales += Random.Range(1,181);
					}
					if(col.gameObject.tag == "lansallamas")
					{
						GetComponent<Hero>().balaLlamas += 300;
					}
					if(col.gameObject.tag == "sniper")
					{
						GetComponent<Hero>().SniperTotales += 5;
						GetComponent<Hero>().balaSniper = 1;
					}
					if(col.gameObject.tag == "panzer")
					{
						GetComponent<Hero>().PanzerTotales += 5;
						GetComponent<Hero>().balaPanzer = 1;
						bala = true;
					}
				}else
				{
					votar = armaMano;

					if(col.gameObject.tag == "escopeta")
					{
						GetComponent<Hero>().balaEscopeta += 8;
						GetComponent<Hero>().EscopetaTotales += Random.Range(1,49);
						arma.GetComponent<UnlokArma>().escopeta = true;
					}
					if(col.gameObject.tag == "fusil")
					{
						GetComponent<Hero>().balaFusil += 5;
						GetComponent<Hero>().FusilTotales += Random.Range(1,41);
						arma.GetComponent<UnlokArma>().fusil = true;
					}
					if(col.gameObject.tag == "submetra")
					{
						GetComponent<Hero>().balaSubmetra += 30;
						GetComponent<Hero>().SubmetraTotales += Random.Range(1,121);
						arma.GetComponent<UnlokArma>().submetra = true;
					}
					if(col.gameObject.tag == "metra")
					{
						GetComponent<Hero>().balaMetra += 32;
						GetComponent<Hero>().MetraTotales += Random.Range(1,181);
						arma.GetComponent<UnlokArma>().metra = true;
					}
					if(col.gameObject.tag == "lansallamas")
					{
						GetComponent<Hero>().balaLlamas += 300;
						arma.GetComponent<UnlokArma>().llamas = true;
					}
					if(col.gameObject.tag == "sniper")
					{
						GetComponent<Hero>().SniperTotales += 5;
						GetComponent<Hero>().balaSniper = 1;
						arma.GetComponent<UnlokArma>().sniper = true;
					}
					if(col.gameObject.tag == "panzer")
					{
						GetComponent<Hero>().PanzerTotales += 5;
						GetComponent<Hero>().balaPanzer = 1;
						bala = true;
						arma.GetComponent<UnlokArma>().panzer = true;
					}

					armaMano = col.gameObject.tag;
					RecogeGun();
				}
			}
		}

		if(col.gameObject.tag == "suplies")
		{
			GetComponent<Hero>().EscopetaTotales += 1000;
			GetComponent<Hero>().FusilTotales += 1000;
			GetComponent<Hero>().SubmetraTotales += 1000;
			GetComponent<Hero>().MetraTotales += 1000;
			GetComponent<Hero>().SniperTotales += 1000;
			GetComponent<Hero>().PanzerTotales += 1000;
			GetComponent<Hero>().balaLlamas += 1000;
			GetComponent<Hero>().balaSniper = 1;
			GetComponent<Hero>().balaPanzer = 1;
			GetComponent<Hero>().granadas += 1;
			bala = true;
			arma.GetComponent<UnlokArma>().granada = true;
		}
		if(col.gameObject.tag == "granade")
		{
			arma.GetComponent<UnlokArma>().granada = true;
		}
	}
}