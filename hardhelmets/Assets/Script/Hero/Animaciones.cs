using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animaciones : MonoBehaviour {

	public Animator animator;

	//TIEMPO QUIETO
	public float quieto = 0.0f;
	public int azar;

	//EFECTOS
	public GameObject sangre;
	public GameObject polv;
	public GameObject gota;
	public GameObject splash;
	bool inwater;
	public GameObject Ondas;
	public GameObject soul;

	public Transform PasoD;
	public Transform PasoI;
	public GameObject pasopolvo;
	public GameObject pasopolvo2;

	public GameObject strikearma;
	public GameObject strikeescopeta;
	public GameObject strikegun;

	public Transform strikeSpawnPistola;
	public Transform strikeSpawnFusil;
	public Transform strikeSpawnEscopeta;
	public Transform strikeSpawnSubmetra;
	public Transform strikeSpawnMetra;

	public Vector3 inicial;

	int vidas;

	//PARA QUE NAZCA ALMA UNA SOLA VEZ
	public bool hacer;

	//PANEL PARTIDA
	GameObject Panel;

	public GameObject barra;

	// Use this for initialization
	void Start ()
	{
		vidas = 1;
		inicial = transform.position;
	}

	// Update is called once per frame
	void Update ()
	{
		if(Panel == null)
		{
			Panel = GameObject.Find("GAME");
		}else
		{
			if(Panel.GetComponent<GameOffline>().final || Panel.GetComponent<GameOffline>().muerte)
			{
				GetComponent<Hero>().vivo = false;
				GetComponent<Hero>().menu.SetActive(false);
				barra.SetActive(false);
			}
		}

		//QUIETO AL AZAR
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("GunIdle"))
		{
			//quieto += 0.1f;
			animator.SetBool("walking", false);
			animator.SetBool("cubierto", false);
			animator.SetBool("paracaidas", false);
			animator.SetBool("apuntando", false);
			animator.SetBool("movimiento", false);
		}else
		{
			quieto = 0;
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("Descanso") || animator.GetCurrentAnimatorStateInfo(0).IsName("Descanso2"))
		{
			animator.SetInteger("descanso", 0);
			azar = 0;
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("GunPose") || animator.GetCurrentAnimatorStateInfo(0).IsName("GunPoseWalk") || animator.GetCurrentAnimatorStateInfo(0).IsName("Pose")
			|| animator.GetCurrentAnimatorStateInfo(0).IsName("PoseWalk") || animator.GetCurrentAnimatorStateInfo(0).IsName("PoseAgachado")  || animator.GetCurrentAnimatorStateInfo(0).IsName("GunPoseAgachado"))
		{
			animator.SetBool("pose", false);
		}

		//MOVIMIENTOS BASICOS
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("walk") || animator.GetCurrentAnimatorStateInfo(0).IsName("GunWalk"))
		{
			animator.SetBool("walking", true);
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("jump1"))
		{
			animator.SetBool("jump", false);
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("roll"))
		{
			animator.SetBool("rolling", true);
			animator.SetBool("roll", false);
		}else
		{
			animator.SetBool("rolling", false);
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("hammer") || animator.GetCurrentAnimatorStateInfo(0).IsName("mina"))
		{
			animator.SetInteger("crear", 0);
		}
		//CALLENDO
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("jump2"))
		{
			animator.SetBool("falling2", true);
		}
		//CAE AL PISO
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("jump3"))
		{
			animator.SetBool("falling2", false);
			//GetComponent<Hero>().rafaga = true;
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("paracaidasS"))
		{
			animator.SetBool("paracaidas", true);
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("backJump") || animator.GetCurrentAnimatorStateInfo(0).IsName("cae"))
		{
			GetComponent<Hero>().agachado = false;
		}
		//DISPARO
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("FusilShot") || animator.GetCurrentAnimatorStateInfo(0).IsName("ShotgunShot") || animator.GetCurrentAnimatorStateInfo(0).IsName("SubmetraShot") ||
			animator.GetCurrentAnimatorStateInfo(0).IsName("Granada") || animator.GetCurrentAnimatorStateInfo(0).IsName("GunShot") || animator.GetCurrentAnimatorStateInfo(0).IsName("panzerShot2") || animator.GetCurrentAnimatorStateInfo(0).IsName("MetraShot"))
		{
			animator.SetInteger("disparo", 0);
			//animator.SetBool("cubierto", false);
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("sniper"))
		{
			animator.SetBool("apuntando", true);
			animator.SetBool("cubierto", false);
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("MetraShot") && GetComponent<Hero>().balaMetra <= 0 ||
			animator.GetCurrentAnimatorStateInfo(0).IsName("MetraShotAgachado") && GetComponent<Hero>().balaMetra <= 0 ||
			animator.GetCurrentAnimatorStateInfo(0).IsName("MetraShotWalk") && GetComponent<Hero>().balaMetra <= 0 )
		{
			animator.SetInteger("disparo", 0);
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("FusilShotWalk") || animator.GetCurrentAnimatorStateInfo(0).IsName("ShotgunShotWalk") || animator.GetCurrentAnimatorStateInfo(0).IsName("SubmetraShotWalk") || animator.GetCurrentAnimatorStateInfo(0).IsName("GranadaWalk")
			|| animator.GetCurrentAnimatorStateInfo(0).IsName("jumpShot") || animator.GetCurrentAnimatorStateInfo(0).IsName("jumpShotUp") || animator.GetCurrentAnimatorStateInfo(0).IsName("jumpShotDown") || animator.GetCurrentAnimatorStateInfo(0).IsName("backJumpShot") ||
			animator.GetCurrentAnimatorStateInfo(0).IsName("panzerShot2Walk") || animator.GetCurrentAnimatorStateInfo(0).IsName("MetraShotWalk"))
		{
			animator.SetInteger("disparo", 0);
		}
		//MEZCLA---------
		if(animator.GetCurrentAnimatorStateInfo(1).IsName("GunShot") || animator.GetCurrentAnimatorStateInfo(1).IsName("ShotgunShotMescla") || animator.GetCurrentAnimatorStateInfo(1).IsName("FusilgunShotMescla")
			|| animator.GetCurrentAnimatorStateInfo(1).IsName("panzerShot2Mescla") || animator.GetCurrentAnimatorStateInfo(1).IsName("SubmetraShotMescla") || animator.GetCurrentAnimatorStateInfo(1).IsName("MetraShotMescla") 
			|| animator.GetCurrentAnimatorStateInfo(1).IsName("GranadaMescla"))
		{
			animator.SetInteger("disparo", 0);
		}
		if(animator.GetCurrentAnimatorStateInfo(1).IsName("MetraShotMescla") && GetComponent<Hero>().balaMetra <= 0)
		{
			animator.SetInteger("disparo", 0);
		}
		//RECARGA
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("FusilRecarga") || animator.GetCurrentAnimatorStateInfo(0).IsName("GunRecarga") ||
			animator.GetCurrentAnimatorStateInfo(0).IsName("ShotgunRecarga") || animator.GetCurrentAnimatorStateInfo(0).IsName("RecargaMetraySub") ||
			animator.GetCurrentAnimatorStateInfo(0).IsName("lansallamasrecarga") || animator.GetCurrentAnimatorStateInfo(0).IsName("panzerRecarga"))
		{
			animator.SetBool("recargando", true);
			//animator.SetBool("cubierto", false);
			//GetComponent<Hero>().rafaga = true;
			animator.SetInteger("recarga", 0);
			animator.SetInteger("disparo", 0);

			animator.SetBool("sniper", false);
		}else if(!animator.GetCurrentAnimatorStateInfo(0).IsName("GunIRecarga"))
		{
			animator.SetBool("recargando", false);
		}
		//MEZCLA------------
		if(animator.GetCurrentAnimatorStateInfo(1).IsName("GunRecarga") || animator.GetCurrentAnimatorStateInfo(1).IsName("ShotgunRecargaMescla")
			|| animator.GetCurrentAnimatorStateInfo(1).IsName("FusilRecargaMescla") || animator.GetCurrentAnimatorStateInfo(1).IsName("RecargaMetraySubMescla")
			|| animator.GetCurrentAnimatorStateInfo(1).IsName("panzerRecargaMescla"))
		{
			animator.SetBool("recargando", true);
			animator.SetInteger("recarga", 0);
			animator.SetInteger("disparo", 0);
		}else
		{
			animator.SetBool("recargando", false);
		}

		//DISPAROS AGACHADO
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("FusilShotAgachado") || animator.GetCurrentAnimatorStateInfo(0).IsName("ShotgunShotAgachado") || animator.GetCurrentAnimatorStateInfo(0).IsName("SubmetraShotAgachado") ||
			animator.GetCurrentAnimatorStateInfo(0).IsName("GunShotAgachado") || animator.GetCurrentAnimatorStateInfo(0).IsName("GranadaAgachado") || animator.GetCurrentAnimatorStateInfo(0).IsName("panzerShot2Agachado") || animator.GetCurrentAnimatorStateInfo(0).IsName("MetraShotAgachado"))
		{
			animator.SetInteger("disparo", 0);

		}
		//RECARGAS AGACHADO
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("GunRecargaAgachado") || animator.GetCurrentAnimatorStateInfo(0).IsName("FusilRecargaAgachado") || animator.GetCurrentAnimatorStateInfo(0).IsName("ShotgunRecargaAgachado") ||
			animator.GetCurrentAnimatorStateInfo(0).IsName("RecargaMetraySubAgachado") || animator.GetCurrentAnimatorStateInfo(0).IsName("panzerRecargaAgachado"))
		{
			animator.SetBool("recargando", true);
			animator.SetInteger("recarga", 0);
		}
		//PISTOLA
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("GunIdle"))
		{
			animator.SetBool("pistolando", true);
			animator.SetBool("cubierto", false);
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("GunIRecarga"))
		{
			animator.SetBool("recargando", true);
			//GetComponent<Hero>().cargando = true;
		}
		//LANSA LLAMAS
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("lansallamasPose") || animator.GetCurrentAnimatorStateInfo(0).IsName("lansallamasPoseWalk") || animator.GetCurrentAnimatorStateInfo(0).IsName("lansallamasShot") || animator.GetCurrentAnimatorStateInfo(0).IsName("lansallamasPoseAgachado") || animator.GetCurrentAnimatorStateInfo(0).IsName("lansallamasShotagachado"))
		{
			if(GetComponent<Hero>().balaLlamas > 0)
			{
				animator.SetBool("flameando", true);
			}else
			{
				animator.SetBool("flameando", false);
			}
			//GetComponent<Hero>().rafaga = true;
		}else
		{
			animator.SetBool("flameando", false);
		}
		//CUCHILLO
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("Knife1") || animator.GetCurrentAnimatorStateInfo(0).IsName("KnifeWalk"))
		{
			animator.SetBool("cuchillo", false);
			animator.SetBool("cuchillando", true);
		}else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Knife2"))
		{
			animator.SetBool("cuchillo2", false);
			animator.SetBool("cuchillando", true);
			GetComponent<Hero>().cuchillo = false;
		}else
		{
			animator.SetBool("cuchillando", false);
		}
		//CAMBIO ARMA
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("Cambio") || animator.GetCurrentAnimatorStateInfo(0).IsName("Cambio2"))
		{
			animator.SetBool("cambio", false);
		}
		//MUERTE
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("KillSimple") || animator.GetCurrentAnimatorStateInfo(0).IsName("KillSimple2") || animator.GetCurrentAnimatorStateInfo(0).IsName("KillSimple3") || animator.GetCurrentAnimatorStateInfo(0).IsName("KillBackJump")
			|| animator.GetCurrentAnimatorStateInfo(0).IsName("KillEX") || animator.GetCurrentAnimatorStateInfo(0).IsName("KillEX2") || animator.GetCurrentAnimatorStateInfo(0).IsName("KillQuemado"))
		{
			animator.SetInteger("muerte", 0);
			animator.SetBool("headShot", false);
		}
		//CASCADO
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("Hit") || animator.GetCurrentAnimatorStateInfo(0).IsName("HitKnife") || animator.GetCurrentAnimatorStateInfo(0).IsName("KillJump"))
		{
			GetComponent<Hero>().caminarI = false;
			GetComponent<Hero>().caminarD = false;
			GetComponent<Hero>().caminarU = false;
			GetComponent<Hero>().caminarA = false;
			animator.SetBool("walk", false);
			animator.SetInteger("cascado", 0);
		}
		//AGACHADO
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("Agachado"))
		{
			animator.SetBool("cubrirse", false);
			//animator.SetBool("cubierto", true);
		}
		//SALTO ATRAS
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("backJump"))
		{
			animator.SetBool("jumpback", false);
			animator.SetBool("cubrirse", false);
			animator.SetBool("cubierto", false);
			animator.SetBool("movimiento", true);
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("frena2"))
		{
			animator.SetBool("jumpfront", false);
		}

		if(animator.GetCurrentAnimatorStateInfo(0).IsName("cae"))
		{
			animator.SetBool("granada", false);

			animator.SetBool("jumpback", false);
			animator.SetBool("falling2", false);
			animator.SetBool("cubrirse", false);
			animator.SetBool("cubierto", false);
			animator.SetInteger("disparo", 0);
			if(animator.GetBool("grounded") && !animator.GetBool("flamas"))
			{
				animator.SetBool("movimiento", false);
			}
			//GetComponent<Hero>().rafaga = true;
		}
		//TIEMPO QUIETO
		if(quieto >= 12)
		{
			azar = Random.Range(1,3);
			animator.SetInteger("descanso", azar);
			quieto = 0;
		}
		if(inwater)
		{
			Ondass.activas = true;
			Ondas.SetActive(true);
		}else
		{
			Ondass.activas = false;
			Ondas.SetActive(false);
		}
		if(!GetComponent<Hero>().vivo)
		{
			if(hacer)
			{
				var efect2 = (GameObject)Instantiate(soul, transform.position, transform.rotation);
				hacer = false;
			}
		}else
		{
			hacer = true;
		}
	}
	public void levantarse()
	{
		animator.SetBool("movimiento", false);
		//GetComponent<Hero>().fast = false;
	}

	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "Water")
		{
			if(GetComponent<Rigidbody>().velocity.y <= -4f)
			{
				var efect = (GameObject)Instantiate(splash, new Vector3(PasoD.transform.position.x, col.gameObject.transform.position.y+0.1f, PasoD.transform.position.z), Quaternion.Euler(90,0,0));
			}
			Ondass.posOndas = col.gameObject.transform.position.y;
			inwater = true;
		}
	}
	void OnTriggerExit (Collider col)
	{
		if(col.gameObject.tag == "Water")
		{
			if(GetComponent<Rigidbody>().velocity.y >= 4f)
			{
				var efect = (GameObject)Instantiate(splash, new Vector3(PasoD.transform.position.x, col.gameObject.transform.position.y+0.1f, PasoD.transform.position.z), Quaternion.Euler(90,0,0));
			}
			inwater = false;
		}
	}
	//EVENTOS SPINE
	void load ()
	{
		GetComponent<Hero>().load = true;
	}
	void blood ()
	{
		if(PlayerPrefs.GetInt("violencia") == 1)
		{
			var efect = (GameObject)Instantiate(sangre, transform.position, transform.rotation);
		}
	}
	void muerto ()
	{
		if(gameObject.tag == "Player")
		{
			Panel.GetComponent<GameOffline>().KillsM += 1;
			Panel.GetComponent<GameOffline>().DeadsB -= 1;
		}else
		{
			Panel.GetComponent<GameOffline>().KillsB += 1;
			Panel.GetComponent<GameOffline>().DeadsM -= 1;
		}

		GetComponent<Hero>().menu.SetActive(true);
	}
	void polvo ()
	{
		if(!GetComponent<Hero>().water)
		{
			var efect = (GameObject)Instantiate(polv, transform.position, transform.rotation);
		}
	}

	void paso()
	{
		if(!GetComponent<Hero>().water)
		{
			var efect = (GameObject)Instantiate(pasopolvo, PasoD.transform.position, PasoD.transform.rotation);
		}else
		{
			var efect = (GameObject)Instantiate(gota, transform.position, transform.rotation);
		}
	}
	void paso2()
	{
		if(!GetComponent<Hero>().water)
		{
			var efect = (GameObject)Instantiate(pasopolvo2, PasoI.transform.position, PasoI.transform.rotation);
		}else
		{
			var efect = (GameObject)Instantiate(gota, transform.position, transform.rotation);
		}
	}
	void rafagafusil ()
	{
		if(GetComponent<Hero>()._currentDirection == "right")
		{
			var efect = (GameObject)Instantiate(strikearma, strikeSpawnFusil.transform.position, strikeSpawnFusil.rotation);//Quaternion.Euler(0,0,strikeSpawn.rotation.z));
		}else
		{
			var efect = (GameObject)Instantiate(strikearma, strikeSpawnFusil.transform.position, Quaternion.Euler(0,180,strikeSpawnFusil.rotation.z));//Quaternion.Euler(0,0,strikeSpawn.rotation.z));
		}
	}
	void rafagasubmetra ()
	{
		if(GetComponent<Hero>()._currentDirection == "right")
		{
			var efect = (GameObject)Instantiate(strikearma, strikeSpawnSubmetra.transform.position, strikeSpawnSubmetra.rotation);//Quaternion.Euler(0,0,strikeSpawn.rotation.z));
		}else
		{
			var efect = (GameObject)Instantiate(strikearma, strikeSpawnSubmetra.transform.position, Quaternion.Euler(0,180,strikeSpawnSubmetra.rotation.z));//Quaternion.Euler(0,0,strikeSpawn.rotation.z));
		}
	}
	void rafagaarmametra ()
	{
		if(GetComponent<Hero>()._currentDirection == "right")
		{
			var efect = (GameObject)Instantiate(strikearma, strikeSpawnMetra.transform.position, strikeSpawnMetra.rotation);//Quaternion.Euler(0,0,strikeSpawn.rotation.z));
		}else
		{
			var efect = (GameObject)Instantiate(strikearma, strikeSpawnMetra.transform.position, Quaternion.Euler(0,180,strikeSpawnMetra.rotation.z));//Quaternion.Euler(0,0,strikeSpawn.rotation.z));
		}
	}
	void rafagagun ()
	{

		if(GetComponent<Hero>()._currentDirection == "right")
		{
			var efect = (GameObject)Instantiate(strikegun, strikeSpawnPistola.transform.position, strikeSpawnPistola.rotation);//Quaternion.Euler(strikeSpawn.rotation.x,strikeSpawn.rotation.y,strikeSpawn.rotation.z-90));//strikeSpawn.rotation);//Quaternion.Euler(0,0,strikeSpawn.rotation.z));
		}else
		{
			var efect = (GameObject)Instantiate(strikegun, strikeSpawnPistola.transform.position, Quaternion.Euler(0,0,strikeSpawnPistola.rotation.z-180));//Quaternion.Euler(0,0,strikeSpawn.rotation.z));
		}
	}
	void rafagaescopeta ()
	{
		if(GetComponent<Hero>()._currentDirection == "right")
		{
			var efect = (GameObject)Instantiate(strikeescopeta, strikeSpawnEscopeta.transform.position, strikeSpawnEscopeta.rotation);//Quaternion.Euler(0,0,strikeSpawn.rotation.z));
		}else
		{
			var efect = (GameObject)Instantiate(strikeescopeta, strikeSpawnEscopeta.transform.position, Quaternion.Euler(0,0,strikeSpawnEscopeta.rotation.z-180));//Quaternion.Euler(0,0,strikeSpawn.rotation.z));
		}
	}
	void cuchillo ()
	{
		GetComponent<Hero>().rafaga = true;
	}
	void cuchillob ()
	{
		GetComponent<Hero>().rafaga = true;
	}
}
