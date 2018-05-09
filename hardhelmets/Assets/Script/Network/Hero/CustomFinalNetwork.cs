using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine.Unity.Modules.AttachmentTools; 
using UnityEngine.Networking;

public class CustomFinalNetwork : NetworkBehaviour {
	
	[SpineSkin]
	public List<string> skinsToCombine;

	Spine.Skin combinedSkin;

	[SyncVar(hook = "ArmaHand")]
	public string armaMano;
	[SyncVar(hook = "ArmaBack")]
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
	[SyncVar]
	public string casco;
	[SyncVar]
	public string cara;
	[SyncVar]
	public string mascara;
	[SyncVar]
	public string uniforme;
	[SyncVar]
	public string cuello;
	[SyncVar]
	public string chaleco;
	[SyncVar]
	public string maleta;
	[SyncVar]
	public string cuerpo;

	public GameObject cabeza;

	void Start ()
	{
		if(!isLocalPlayer)
		{
			return;
		}

		casco = PlayerPrefs.GetString("casco");
		cara = PlayerPrefs.GetString("cara");
		mascara = PlayerPrefs.GetString("mascara");
		uniforme = PlayerPrefs.GetString("uniforme");
		cuello = PlayerPrefs.GetString("cuello");
		chaleco = PlayerPrefs.GetString("chaleco");
		maleta = PlayerPrefs.GetString("maleta");
		cuerpo = "cuerpo1";

		GetComponent<HeroNetwork>().arma2 = true;
		skinsToCombine[8] = "gun";
		CmdSendSkinPistola("gun");

		if(casco == "casco1")
		{
			cabeza.GetComponent<CabezaNetwork>().numSombrero = 1;
		}
		if(casco == "casco2")
		{
			cabeza.GetComponent<CabezaNetwork>().numSombrero = 2;
		}
		if(casco == "casco3")
		{
			cabeza.GetComponent<CabezaNetwork>().numSombrero = 3;
		}
		if(casco == "casco4")
		{
			cabeza.GetComponent<CabezaNetwork>().numSombrero = 4;
		}
		if(casco == "casco5")
		{
			cabeza.GetComponent<CabezaNetwork>().numSombrero = 5;
		}
		if(casco == "casco6")
		{
			cabeza.GetComponent<CabezaNetwork>().numSombrero = 6;
		}
	}
	//CMD PONE SKINS DE PERSONAJE
	[Command]
	public void CmdSendSkin (string newCasco, string newValueCara, string newValueMascara, string newUniforme, string newCuello, string newChaleco, string newMaleta, string newCuerpo)
	{
		RpcSetSkin(newCasco, newValueCara, newValueMascara, newUniforme, newCuello, newChaleco, newMaleta, newCuerpo);
	}
	[ClientRpc]
	public void RpcSetSkin (string newCasco, string newValueCara, string newValueMascara, string newUniforme, string newCuello, string newChaleco, string newMaleta, string newCuerpo)
	{
		if(!isLocalPlayer)
		{
			casco = newCasco;
			cara = newValueCara;
			mascara = newValueMascara;
			uniforme = newUniforme;
			cuello = newCuello;
			chaleco = newChaleco;
			maleta = newMaleta;
			cuerpo = newCuerpo;

			skinsToCombine[0] = casco;
			skinsToCombine[1] = cara;
			skinsToCombine[2] = mascara;
			skinsToCombine[3] = uniforme;
			skinsToCombine[4] = cuello;
			skinsToCombine[5] = chaleco;
			skinsToCombine[6] = maleta;
			skinsToCombine[7] = "";
			skinsToCombine[8] = "gun";
			skinsToCombine[9] = cuerpo;

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
	}
	//CMD CAMBIA LAS ARMAS DE MANO Y ESPALDA
	[Command]
	public void CmdSendSkinArmas (string newMano, string newEspalda)
	{
		RpcSetSkinArmas(newMano, newEspalda);
	}
	[ClientRpc]
	public void RpcSetSkinArmas (string newMano, string newEspalda)
	{
		if(!isLocalPlayer)
		{
			armaMano = newMano;
			armaEspalda = newEspalda;

			skinsToCombine[7] = newEspalda;//armaEspalda
			skinsToCombine[8] = newMano;//armaMano

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
		esperar = false;
	}
	//CMD CAMBIA LAS POR PISTOLA
	[Command]
	public void CmdSendSkinPistola (string newMano)
	{
		RpcSetSkinPistola(newMano);
	}
	[ClientRpc]
	public void RpcSetSkinPistola (string newMano)
	{
		if(!isLocalPlayer)
		{
			skinsToCombine[8] = "gun";//newMano

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
	}
	[Command]
	public void CmdSendPanzer (string newcohete)
	{
		RpcSetSkinPistola(newcohete);
	}
	[ClientRpc]
	public void RpcSetPanzer (string newcohete)
	{
		if(!isLocalPlayer)
		{
			skinsToCombine[11] = newcohete;

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
	}

	[Command]
	public void CmdSetCasco(string newCasco2)
	{
		RpcSetCasco(newCasco2);
		skinsToCombine[0] = newCasco2;

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
	[ClientRpc]
	public void RpcSetCasco (string newCasco2)
	{
		//if(!isLocalPlayer)
		//{
			skinsToCombine[0] = newCasco2;

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
		//}
	}

	bool listo;
	//public GameObject Enemy;
	public bool bala;
	string cohete;

	bool esperar;
	bool esperarPanzer;
	void FixedUpdate ()
	{
		if(!isLocalPlayer)
		{
			return;
		}

		if(!listo)
		{
			if(gameObject.tag == "Player")
			{
				casco = PlayerPrefs.GetString("casco");
				cara = PlayerPrefs.GetString("cara");
				mascara = PlayerPrefs.GetString("mascara");
				uniforme = PlayerPrefs.GetString("uniforme");
				cuello = PlayerPrefs.GetString("cuello");
				chaleco = PlayerPrefs.GetString("chaleco");
				maleta = PlayerPrefs.GetString("maleta");
				cuerpo = "cuerpo1";
			}else
			{
				casco = PlayerPrefs.GetString("casco")+"b";
				cara = PlayerPrefs.GetString("cara");
				mascara = PlayerPrefs.GetString("mascara")+"b";
				uniforme = PlayerPrefs.GetString("uniforme")+"b";
				cuello = PlayerPrefs.GetString("cuello")+"b";
				chaleco = PlayerPrefs.GetString("chaleco")+"b";
				maleta = PlayerPrefs.GetString("maleta")+"b";
				cuerpo = "cuerpo1b";
			}

			skinsToCombine[0] = casco;
			skinsToCombine[1] = cara;
			skinsToCombine[2] = mascara;
			skinsToCombine[3] = uniforme;
			skinsToCombine[4] = cuello;
			skinsToCombine[5] = chaleco;
			skinsToCombine[6] = maleta;
			skinsToCombine[7] = "";
			skinsToCombine[8] = "gun";
			skinsToCombine[9] = cuerpo;

			CmdSendSkin(casco, cara, mascara, uniforme, cuello, chaleco, maleta, cuerpo);

			StartCoroutine(momento());
		}

		//pone skin granada
		if(GetComponent<HeroNetwork>().granadas >= 1)
		{
			skinsToCombine[10] = "granada";
		}else
		{
			skinsToCombine[10] = "";
		}
		//pone skin rocket
		if(GetComponent<HeroNetwork>().arma == "panzer")
		{
			/*if(!esperarPanzer)
			{
				CmdSendSkinArmas(armaMano, armaEspalda);
				//CmdSendSkinArmas("panzer", armaEspalda);
				esperarPanzer = true;
			}*/
			if(bala)
			{
				if(GetComponent<HeroNetwork>().balaPanzer >= 1)
				{
					cohete = "rocket";
					skinsToCombine[11] = "rocket";
				}else
				{
					cohete = "";
					skinsToCombine[11] = "";
				}

				CmdSendPanzer(cohete);

				bala = false;
			}
		}else if(cohete != "")
		{
			cohete = "";
			skinsToCombine[11] = "";
			CmdSendPanzer(cohete);
		}

		GetComponent<HeroNetwork>().arma = armaMano;

		if(Input.GetButtonUp("INTERAMBIO") && armaEspalda != "" && segunda && !animator.GetBool("cuchillando") && !animator.GetCurrentAnimatorStateInfo(0).IsName("backJump") && !animator.GetCurrentAnimatorStateInfo(0).IsName("lansallamasShot") && !animator.GetCurrentAnimatorStateInfo(0).IsName("cae"))
		{
			GetComponent<HeroNetwork>().rafaga = true;
			segunda = false;
			StartCoroutine(espera());
			cambio = armaMano;
			GetComponent<HeroNetwork>().rafaga = true;
			GetComponent<HeroNetwork>().arma2 = false;
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

			CmdSendSkinArmas(armaMano, armaEspalda);
		}

		//PONE PISTOLA
		if(armaMano == "escopeta" && GetComponent<HeroNetwork>().balaEscopeta <= 0 && GetComponent<HeroNetwork>().EscopetaTotales <= 0 && armaEspalda != "")
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
			CmdSendSkinArmas(armaMano, armaEspalda);
		}else if(armaMano == "escopeta" && GetComponent<HeroNetwork>().balaEscopeta <= 0 && GetComponent<HeroNetwork>().EscopetaTotales <= 0 && armaEspalda == "")
		{
			soltar();
			armaMano = "";
			armaEspalda = "";

			GetComponent<HeroNetwork>().arma2 = true;
			skinsToCombine[8] = "gun";
			CmdSendSkinPistola("gun");
		}
		if(armaMano == "fusil" && GetComponent<HeroNetwork>().balaFusil <= 0 && GetComponent<HeroNetwork>().FusilTotales <= 0 && armaEspalda != "")
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
			CmdSendSkinArmas(armaMano, armaEspalda);
		}else if(armaMano == "fusil" && GetComponent<HeroNetwork>().balaFusil <= 0 && GetComponent<HeroNetwork>().FusilTotales <= 0 && armaEspalda == "")
		{
			soltar();
			armaMano = "";
			armaEspalda = "";

			GetComponent<HeroNetwork>().arma2 = true;
			skinsToCombine[8] = "gun";
			CmdSendSkinPistola("gun");
		}
		if(armaMano == "submetra" && GetComponent<HeroNetwork>().balaSubmetra <= 0 && GetComponent<HeroNetwork>().SubmetraTotales <= 0 && armaEspalda != "")
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
			CmdSendSkinArmas(armaMano, armaEspalda);
		}else if(armaMano == "submetra" && GetComponent<HeroNetwork>().balaSubmetra <= 0 && GetComponent<HeroNetwork>().SubmetraTotales <= 0 && armaEspalda == "")
		{
			soltar();
			armaMano = "";
			armaEspalda = "";

			GetComponent<HeroNetwork>().arma2 = true;
			skinsToCombine[8] = "gun";
			CmdSendSkinPistola("gun");
		}
		if(armaMano == "metra" && GetComponent<HeroNetwork>().balaMetra <= 0 && GetComponent<HeroNetwork>().MetraTotales <= 0 && armaEspalda != "")
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
			CmdSendSkinArmas(armaMano, armaEspalda);
		}else if(armaMano == "metra" && GetComponent<HeroNetwork>().balaMetra <= 0 && GetComponent<HeroNetwork>().MetraTotales <= 0 && armaEspalda == "")
		{
			soltar();
			armaMano = "";
			armaEspalda = "";

			GetComponent<HeroNetwork>().arma2 = true;
			skinsToCombine[8] = "gun";
			CmdSendSkinPistola("gun");
		}

		if(armaMano == "lansallamas" && GetComponent<HeroNetwork>().balaLlamas <= 0 && armaEspalda != "")
		{
			GetComponent<HeroNetwork>().lansallamas = false;
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
			CmdSendSkinArmas(armaMano, armaEspalda);
		}else if(armaMano == "lansallamas" && GetComponent<HeroNetwork>().balaLlamas <= 0 && armaEspalda == "")
		{
			soltar();
			GetComponent<HeroNetwork>().lansallamas = false;
			armaMano = "";
			armaEspalda = "";

			GetComponent<HeroNetwork>().arma2 = true;
			skinsToCombine[8] = "gun";
			CmdSendSkinPistola("gun");
		}

		if(armaMano == "sniper" && GetComponent<HeroNetwork>().balaSniper <= 0 && GetComponent<HeroNetwork>().SniperTotales <= 0 && armaEspalda != "")
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
			CmdSendSkinArmas(armaMano, armaEspalda);*/
		}else if(armaMano == "sniper" && GetComponent<HeroNetwork>().balaSniper <= 0 && GetComponent<HeroNetwork>().SniperTotales <= 0 && armaEspalda == "")
		{
			if(!esperar)
			{
				StartCoroutine(esperaSniper());
				esperar = true;
			}
			/*soltar();
			armaMano = "";
			armaEspalda = "";

			GetComponent<HeroNetwork>().arma2 = true;
			skinsToCombine[8] = "gun";
			CmdSendSkinPistola("gun");*/
		}

		if(armaMano == "panzer" && GetComponent<HeroNetwork>().balaPanzer <= 0 && GetComponent<HeroNetwork>().PanzerTotales <= 0 && armaEspalda != "")
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

			CmdSendSkinArmas(armaMano, armaEspalda);
		}else if(armaMano == "panzer" && GetComponent<HeroNetwork>().balaPanzer <= 0 && GetComponent<HeroNetwork>().PanzerTotales <= 0 && armaEspalda == "")
		{
			soltar();
			armaMano = "";
			armaEspalda = "";

			GetComponent<HeroNetwork>().arma2 = true;
			skinsToCombine[8] = "gun";
			CmdSendSkinPistola("gun");
		}

		if(armaMano == "")
		{
			/*GetComponent<HeroNetwork>().arma2 = true;
			skinsToCombine[8] = "gun";
			CmdSendSkinPistola("gun");*/
		}else
		{
			GetComponent<HeroNetwork>().arma2 = false;
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
			GetComponent<HeroNetwork>().lansallamas = true;
		}else
		{
			GetComponent<HeroNetwork>().lansallamas = false;
		}
		if(armaMano == "sniper")
		{
			GetComponent<HeroNetwork>().sniper = true;
		}else
		{
			GetComponent<HeroNetwork>().sniper = false;
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

		GetComponent<HeroNetwork>().arma2 = true;
		skinsToCombine[8] = "gun";
		CmdSendSkinPistola("gun");
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
		CmdSendSkinArmas(armaMano, armaEspalda);
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
			CmdVotaEscopeta();
		}
		if(votar == "fusil")
		{
			CmdVotaFusil();
		}
		if(votar == "submetra")
		{
			CmdVotaSubmetra();
		}
		if(votar == "metra")
		{
			CmdVotaMetra();
		}
		if(votar == "lansallamas")
		{
			CmdVotaLansaLlamas();
		}
		if(votar == "sniper")
		{
			CmdVotaSniper();
		}
		if(votar == "panzer")
		{
			CmdVotaPanzer();
		}
	}

	void soltar()
	{
		if(armaMano == "escopeta")
		{
			CmdVotaEscopeta();
		}
		if(armaMano == "fusil")
		{
			CmdVotaFusil();
		}
		if(armaMano == "submetra")
		{
			CmdVotaSubmetra();
		}
		if(armaMano == "metra")
		{
			CmdVotaMetra();
		}
		if(armaMano == "lansallamas")
		{
			CmdVotaLansaLlamas();
		}
		if(armaMano == "sniper")
		{
			CmdVotaSniper();
		}
		if(armaMano == "panzer")
		{
			esperarPanzer = false;
			CmdVotaPanzer();
		}
	}

	void tiraarma()
	{
		if(!isLocalPlayer)
		{
			return;
		}
		if(armaMano == "escopeta")
		{
			CmdVotaEscopeta();
		}else if(armaMano == "fusil")
		{
			CmdVotaFusil();
		}else if(armaMano == "submetra")
		{
			CmdVotaSubmetra();
		}else if(armaMano == "metra")
		{
			CmdVotaMetra();
		}else if(armaMano == "lansallamas")
		{
			CmdVotaLansaLlamas();
		}else if(armaMano == "sniper")
		{
			CmdVotaSniper();
		}else if(armaMano == "panzer")
		{
			CmdVotaPanzer();
		}else
		{
			CmdVotaPistola();
		}
	}

	[Command]
	public void CmdVotaPistola()
	{
		var arma = (GameObject)Instantiate(pistola2, bulletSpawn.position, Quaternion.Euler(0,0,0)); 
		arma.GetComponent<Rigidbody>().velocity = arma.transform.up * 20;
		arma.GetComponent<Rigidbody>().velocity = arma.transform.right * 3;
		arma.GetComponent<Rigidbody>().AddTorque(transform.forward * 500 * 500);
		NetworkServer.Spawn(arma);
		Destroy(arma, 2.0f);
	}

	[Command]
	public void CmdVotaEscopeta()
	{
		var arma = (GameObject)Instantiate(escopeta2, bulletSpawn.position, Quaternion.Euler(0,0,0)); 
		arma.GetComponent<Rigidbody>().velocity = arma.transform.up * 20;
		arma.GetComponent<Rigidbody>().velocity = arma.transform.right * 3;
		arma.GetComponent<Rigidbody>().AddTorque(transform.forward * 500 * 500);
		NetworkServer.Spawn(arma);
		Destroy(arma, 2.0f);
	}
	[Command]
	public void CmdVotaFusil()
	{
		var arma = (GameObject)Instantiate(fusil2, bulletSpawn.position, Quaternion.Euler(0,0,0)); 
		arma.GetComponent<Rigidbody>().velocity = arma.transform.up * 20;
		arma.GetComponent<Rigidbody>().velocity = arma.transform.right * 3;
		arma.GetComponent<Rigidbody>().AddTorque(transform.forward * 500 * 500);
		NetworkServer.Spawn(arma);
		Destroy(arma, 2.0f);
	}
	[Command]
	public void CmdVotaSubmetra()
	{
		var arma = (GameObject)Instantiate(submetra2, bulletSpawn.position, Quaternion.Euler(0,0,0)); 
		arma.GetComponent<Rigidbody>().velocity = arma.transform.up * 20;
		arma.GetComponent<Rigidbody>().velocity = arma.transform.right * 3;
		arma.GetComponent<Rigidbody>().AddTorque(transform.forward * 500 * 500);
		NetworkServer.Spawn(arma);
		Destroy(arma, 2.0f);
	}
	[Command]
	public void CmdVotaMetra()
	{
		var arma = (GameObject)Instantiate(metra2, bulletSpawn.position, Quaternion.Euler(0,0,0)); 
		arma.GetComponent<Rigidbody>().velocity = arma.transform.up * 20;
		arma.GetComponent<Rigidbody>().velocity = arma.transform.right * 3;
		arma.GetComponent<Rigidbody>().AddTorque(transform.forward * 500 * 500);
		NetworkServer.Spawn(arma);
		Destroy(arma, 2.0f);
	}
	[Command]
	public void CmdVotaSniper()
	{
		var arma = (GameObject)Instantiate(sniper2, bulletSpawn.position, Quaternion.Euler(0,0,-90)); 
		arma.GetComponent<Rigidbody>().velocity = arma.transform.up * 20;
		arma.GetComponent<Rigidbody>().velocity = arma.transform.right * 3;
		arma.GetComponent<Rigidbody>().AddTorque(transform.forward * 500 * 500);
		NetworkServer.Spawn(arma);
		Destroy(arma, 2.0f);
	}
	[Command]
	public void CmdVotaLansaLlamas()
	{
		GetComponent<HeroNetwork>().balaLlamas = 0;
		var arma = (GameObject)Instantiate(lansallamas2, bulletSpawn.position, Quaternion.Euler(0,0,-90)); 
		arma.GetComponent<Rigidbody>().velocity = arma.transform.up * 20;
		arma.GetComponent<Rigidbody>().velocity = arma.transform.right * 3;
		arma.GetComponent<Rigidbody>().AddTorque(transform.forward * 500 * 500);
		NetworkServer.Spawn(arma);
		Destroy(arma, 2.0f);
	}
	[Command]
	public void CmdVotaPanzer()
	{
		var arma = (GameObject)Instantiate(panzer2, bulletSpawn.position, Quaternion.Euler(0,0,-90)); 
		arma.GetComponent<Rigidbody>().velocity = arma.transform.up * 20;
		arma.GetComponent<Rigidbody>().velocity = arma.transform.right * 3;
		arma.GetComponent<Rigidbody>().AddTorque(transform.forward * 500 * 500);
		NetworkServer.Spawn(arma);
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
			GetComponent<HeroNetwork>().rafaga = true;
			GetComponent<HeroNetwork>().arma2 = false;
			votar = armaMano;

			if(armaMano == "")
			{
				if(col.gameObject.tag == "escopeta")
				{
					GetComponent<HeroNetwork>().balaEscopeta += 8;
					GetComponent<HeroNetwork>().EscopetaTotales += Random.Range(10,150);
					arma.GetComponent<UnlokArma>().escopeta = true;
				}
				if(col.gameObject.tag == "fusil")
				{
					GetComponent<HeroNetwork>().balaFusil += 5;
					GetComponent<HeroNetwork>().FusilTotales += Random.Range(10,150);
					arma.GetComponent<UnlokArma>().fusil = true;
				}
				if(col.gameObject.tag == "submetra")
				{
					GetComponent<HeroNetwork>().balaSubmetra += 30;
					GetComponent<HeroNetwork>().SubmetraTotales += Random.Range(10,150);
					arma.GetComponent<UnlokArma>().submetra = true;
				}
				if(col.gameObject.tag == "metra")
				{
					GetComponent<HeroNetwork>().balaMetra += 32;
					GetComponent<HeroNetwork>().MetraTotales += Random.Range(10,150);
					arma.GetComponent<UnlokArma>().metra = true;
				}
				if(col.gameObject.tag == "lansallamas")
				{
					GetComponent<HeroNetwork>().balaLlamas += 300;
					arma.GetComponent<UnlokArma>().llamas = true;
				}
				if(col.gameObject.tag == "sniper")
				{
					GetComponent<HeroNetwork>().SniperTotales += 5;
					GetComponent<HeroNetwork>().balaSniper = 1;
					arma.GetComponent<UnlokArma>().sniper = true;
				}
				if(col.gameObject.tag == "panzer")
				{
					GetComponent<HeroNetwork>().PanzerTotales += 5;
					GetComponent<HeroNetwork>().balaPanzer = 1;
					bala = true;
					arma.GetComponent<UnlokArma>().panzer = true;
				}
				armaMano = col.gameObject.tag;
			}else if(armaEspalda == "" && col.gameObject.tag != armaMano)
			{
				if(col.gameObject.tag == "escopeta")
				{
					GetComponent<HeroNetwork>().balaEscopeta += 8;
					GetComponent<HeroNetwork>().EscopetaTotales += Random.Range(1,49);
					arma.GetComponent<UnlokArma>().escopeta = true;
				}
				if(col.gameObject.tag == "fusil")
				{
					GetComponent<HeroNetwork>().balaFusil += 5;
					GetComponent<HeroNetwork>().FusilTotales += Random.Range(1,41);
					arma.GetComponent<UnlokArma>().fusil = true;
				}
				if(col.gameObject.tag == "submetra")
				{
					GetComponent<HeroNetwork>().balaSubmetra += 30;
					GetComponent<HeroNetwork>().SubmetraTotales += Random.Range(1,121);
					arma.GetComponent<UnlokArma>().submetra = true;
				}
				if(col.gameObject.tag == "metra")
				{
					GetComponent<HeroNetwork>().balaMetra += 32;
					GetComponent<HeroNetwork>().MetraTotales += Random.Range(1,181);
					arma.GetComponent<UnlokArma>().metra = true;
				}
				if(col.gameObject.tag == "lansallamas")
				{
					GetComponent<HeroNetwork>().balaLlamas += 300;
					arma.GetComponent<UnlokArma>().llamas = true;
				}
				if(col.gameObject.tag == "sniper")
				{
					GetComponent<HeroNetwork>().SniperTotales += 5;
					GetComponent<HeroNetwork>().balaSniper = 1;
					arma.GetComponent<UnlokArma>().sniper = true;
				}
				if(col.gameObject.tag == "panzer")
				{
					GetComponent<HeroNetwork>().PanzerTotales += 5;
					GetComponent<HeroNetwork>().balaPanzer = 1;
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
						GetComponent<HeroNetwork>().balaEscopeta += 8;
						GetComponent<HeroNetwork>().EscopetaTotales += Random.Range(1,49);
					}
					if(col.gameObject.tag == "fusil")
					{
						GetComponent<HeroNetwork>().balaFusil += 5;
						GetComponent<HeroNetwork>().FusilTotales += Random.Range(1,41);
					}
					if(col.gameObject.tag == "submetra")
					{
						GetComponent<HeroNetwork>().balaSubmetra += 30;
						GetComponent<HeroNetwork>().SubmetraTotales += Random.Range(1,121);
					}
					if(col.gameObject.tag == "metra")
					{
						GetComponent<HeroNetwork>().balaMetra += 32;
						GetComponent<HeroNetwork>().MetraTotales += Random.Range(1,181);
					}
					if(col.gameObject.tag == "lansallamas")
					{
						GetComponent<HeroNetwork>().balaLlamas += 300;
					}
					if(col.gameObject.tag == "sniper")
					{
						GetComponent<HeroNetwork>().SniperTotales += 5;
						GetComponent<HeroNetwork>().balaSniper = 1;
					}
					if(col.gameObject.tag == "panzer")
					{
						GetComponent<HeroNetwork>().PanzerTotales += 5;
						GetComponent<HeroNetwork>().balaPanzer = 1;
						bala = true;
					}
				}else
				{
					votar = armaMano;

					if(col.gameObject.tag == "escopeta")
					{
						GetComponent<HeroNetwork>().balaEscopeta += 8;
						GetComponent<HeroNetwork>().EscopetaTotales += Random.Range(1,49);
						arma.GetComponent<UnlokArma>().escopeta = true;
					}
					if(col.gameObject.tag == "fusil")
					{
						GetComponent<HeroNetwork>().balaFusil += 5;
						GetComponent<HeroNetwork>().FusilTotales += Random.Range(1,41);
						arma.GetComponent<UnlokArma>().fusil = true;
					}
					if(col.gameObject.tag == "submetra")
					{
						GetComponent<HeroNetwork>().balaSubmetra += 30;
						GetComponent<HeroNetwork>().SubmetraTotales += Random.Range(1,121);
						arma.GetComponent<UnlokArma>().submetra = true;
					}
					if(col.gameObject.tag == "metra")
					{
						GetComponent<HeroNetwork>().balaMetra += 32;
						GetComponent<HeroNetwork>().MetraTotales += Random.Range(1,181);
						arma.GetComponent<UnlokArma>().metra = true;
					}
					if(col.gameObject.tag == "lansallamas")
					{
						GetComponent<HeroNetwork>().balaLlamas += 300;
						arma.GetComponent<UnlokArma>().llamas = true;
					}
					if(col.gameObject.tag == "sniper")
					{
						GetComponent<HeroNetwork>().SniperTotales += 5;
						GetComponent<HeroNetwork>().balaSniper = 1;
						arma.GetComponent<UnlokArma>().sniper = true;
					}
					if(col.gameObject.tag == "panzer")
					{
						GetComponent<HeroNetwork>().PanzerTotales += 5;
						GetComponent<HeroNetwork>().balaPanzer = 1;
						bala = true;
						arma.GetComponent<UnlokArma>().panzer = true;
					}

					armaMano = col.gameObject.tag;
					RecogeGun();
				}
			}
			CmdSendSkinArmas(armaMano, armaEspalda);
		}

		if(col.gameObject.tag == "suplies")
		{
			GetComponent<HeroNetwork>().EscopetaTotales += 1000;
			GetComponent<HeroNetwork>().FusilTotales += 1000;
			GetComponent<HeroNetwork>().SubmetraTotales += 1000;
			GetComponent<HeroNetwork>().MetraTotales += 1000;
			GetComponent<HeroNetwork>().SniperTotales += 1000;
			GetComponent<HeroNetwork>().PanzerTotales += 1000;
			GetComponent<HeroNetwork>().balaLlamas += 1000;
			GetComponent<HeroNetwork>().balaSniper = 1;
			GetComponent<HeroNetwork>().balaPanzer = 1;
			GetComponent<HeroNetwork>().granadas += 1;
			bala = true;
			arma.GetComponent<UnlokArma>().granada = true;
		}
		if(col.gameObject.tag == "granade")
		{
			arma.GetComponent<UnlokArma>().granada = true;
		}
	}
}