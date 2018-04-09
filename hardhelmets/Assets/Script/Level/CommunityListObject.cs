using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;

public class CommunityListObject : MonoBehaviour {

	public int level;

	public int[] array = new int[]{10,11,12,13,14,15,16,18,19,20,21,22};

	public int carta1;
	public int carta2;
	public int carta3;
	public int carta4;
	public int carta5;
	public int carta6;
	public int carta7;
	public int carta8;
	public int carta9;
	public int carta10;

	public bool listo1;
	public bool listo2;
	public bool listo3;
	public bool listo4;
	public bool listo5;
	public bool listo6;
	public bool listo7;
	public bool listo8;
	public bool listo9;
	public bool listo10;

	public GameObject boton;
	public Sprite a1;
	public Sprite a2;

	public GameObject perfil;

	//public static int name = 1;

	public static string activo;
	public Button joinButton;

	public bool selected;

	public GameObject content;

	public string[] arrayName = new string[]{"SeekNDstroy","Bulletz4Breakfast","BigDamnHero","LaidtoRest","IronMAN77","Xenomorphing","TylerDurden","PennywiseTheClown","BluntMachete","SniperLyfe","SilentWraith","BloodyAssault","FightClubAlum","KillSwitch","ExecuteElectrocute","BadBaneCat","IndominusRexxx","AzogtheDefiler","PervertPeewee","TubbyCandyHoarder","GotASegway","LookWhatICanDo","IPlayFarmHeroes","EatingHawaiianPizza","YOURnameHERE","TickleMeElmo","MouseRatRockBand","NinjasInPyjamas","Mistake","SomeTacos","12Nuns","AHungryPolarBear","aDistraction","XBoxShutDown","RollingBarrelz","Something","AllGoodNamesRGone","Error404","CerealKillah","ShittinBullets89","WarningLowBattery","Granny4theWIN","PookieChips","TheMustardCat","DnknDonuts","EdgarAllenPoo","PickingBoogers","BeanieWeenees","SevenofNine","CandyStripper","SmokinHotChick","MadBabyMaker","PistolPrincess","TiaraONtop","SuperGurl3000","GlitterGunner","PurpleBunnySlippers","ImTheBirthdayGirl","FlameThrower","SmittenKitten66","SniperPrincess","SexyShooter","vBluberriMuffins","CutiePatootie22","MsPiggysREVENGE","ShotHottie33","PetalPrincess","FallenAngel","Hraefn","IMTooPrettyToDie","CatWoman","SniperFemme","Zeldarian","CursedWings","IceQueen","SongbirdFatale","LadyPhantom","WarriorPriestess"};
	public string nombre;

	public Text nombrea;
	public Text nombreb;

	public GameObject rango;

	public Text levela;
	public Text levelb;

	// Use this for initialization
	void Start ()
	{
		nombre = arrayName[Random.Range(0,arrayName.Length)];
		level = Random.Range(1,51);

		nombrea.text = nombre;
		nombreb.text = nombre;

		levela.text = level.ToString();
		levelb.text = level.ToString();

		rango.GetComponent<combinedSkins>().skinsToCombine[1] = "fondo"+Random.Range(1,11).ToString();
		rango.GetComponent<combinedSkins>().skinsToCombine[2] = "borde"+Random.Range(1,6).ToString();
		rango.GetComponent<combinedSkins>().skinsToCombine[3] = "rango"+level.ToString();

		selected = false;

		listo1 = true;
	}

	// Update is called once per frame
	void Update ()
	{
		if(listo1)
		{
			carta1 = array[Random.Range(0,array.Length)];
			listo2 = true;
			listo1 = false;
		}
		if(listo2)
		{
			carta2 = array[Random.Range(0,array.Length)];
			if(carta2 != carta1)
			{
				listo3 = true;
				listo2 = false;
			}
		}
		if(listo3)
		{
			carta3 = array[Random.Range(0,array.Length)];
			if(carta3 != carta2 && carta3 != carta1)
			{
				listo4 = true;
				listo3 = false;
			}
		}
		if(listo4)
		{
			carta4 = array[Random.Range(0,array.Length)];
			if(carta4 != carta3 && carta4 != carta2 && carta4 != carta1)
			{
				listo5 = true;
				listo4 = false;
			}
		}
		if(listo5)
		{
			carta5 = array[Random.Range(0,array.Length)];
			if(carta5 != carta4 && carta5 != carta3 && carta5 != carta2 && carta5 != carta1)
			{
				listo6 = true;
				listo5 = false;
			}
		}
		if(listo6)
		{
			carta6 = array[Random.Range(0,array.Length)];
			if(carta6 != carta5 && carta6 != carta4 && carta6 != carta3 && carta6 != carta2 && carta6 != carta1)
			{
				listo7 = true;
				listo6 = false;
			}
		}
		if(listo7)
		{
			carta7 = array[Random.Range(0,array.Length)];
			if(carta7 != carta6 && carta7 != carta5 && carta7 != carta4 && carta7 != carta3 && carta7 != carta2 && carta7 != carta1)
			{
				listo8 = true;
				listo7 = false;
			}
		}
		if(listo8)
		{
			carta8 = array[Random.Range(0,array.Length)];
			if(carta8 != carta7 && carta8 != carta6 && carta8 != carta5 && carta8 != carta4 && carta8 != carta3 && carta8 != carta2 && carta8 != carta1)
			{
				listo9 = true;
				listo8 = false;
			}
		}
		if(listo9)
		{
			carta9 = array[Random.Range(0,array.Length)];
			if(carta9 != carta8 && carta9 != carta7 && carta9 != carta6 && carta9 != carta5 && carta9 != carta4 && carta9 != carta3 && carta9 != carta2 && carta9 != carta1)
			{
				listo10 = true;
				listo9 = false;
			}
		}
		if(listo10)
		{
			carta10 = array[Random.Range(0,array.Length)];
			if(carta10 != carta9 && carta10 != carta8 && carta10 != carta7 && carta10 != carta6 && carta10 != carta5 && carta10 != carta4 && carta10 != carta3 && carta10 != carta2 && carta10 != carta1)
			{
				listo10 = false;
			}
		}

		if(content.GetComponent<CommunityList>().zona && selected)
		{
			perfil.GetComponent<RectTransform>().localScale = new Vector3(0.48f, 0.48f, 0.48f);
		}else
		{
			perfil.GetComponent<RectTransform>().localScale = new Vector3(0.3f, 0.3f, 0.3f);
		}

		if(content.GetComponent<CommunityList>().zona && selected)
		{
			boton.GetComponent<UnityEngine.UI.Image>().sprite = a2;
			if(Input.GetButtonDown("Jump") || Input.GetButtonDown("Submit"))
			{
				joinButton.onClick.Invoke();
			}
		}else
		{
			boton.GetComponent<UnityEngine.UI.Image>().sprite = a1;
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if(col.gameObject.tag == "Player")
		{
			activo = gameObject.name;
			selected = true;
			GetComponent<AudioSource>().Play();
			//boton.GetComponent<UnityEngine.UI.Image>().sprite = a2;
		}
	}

	void OnTriggerExit2D (Collider2D col)
	{
		if(col.gameObject.tag == "Player")
		{
			selected = false;
			boton.GetComponent<UnityEngine.UI.Image>().sprite = a1;
		}
	}

	public void entrar()
	{
		//print("CARGAR NIVEL "+joinButton.transform.parent.name);
		selected = false;

		PlayerPrefs.SetString("nameCommunity", nombre);

		PlayerPrefs.SetInt("levelCommunity", level);

		PlayerPrefs.SetInt("ManoComunity1", carta1);
		PlayerPrefs.SetInt("ManoComunity2", carta2);
		PlayerPrefs.SetInt("ManoComunity3", carta3);
		PlayerPrefs.SetInt("ManoComunity4", carta4);
		PlayerPrefs.SetInt("ManoComunity5", carta5);
		PlayerPrefs.SetInt("ManoComunity6", carta6);
		PlayerPrefs.SetInt("ManoComunity7", carta7);
		PlayerPrefs.SetInt("ManoComunity8", carta8);
		PlayerPrefs.SetInt("ManoComunity9", carta9);
		PlayerPrefs.SetInt("ManoComunity10", carta10);

		content.GetComponent<CommunityList>().user = nombre;
		content.GetComponent<CommunityList>().level = level.ToString();

		content.GetComponent<CommunityList>().skinFondo = rango.GetComponent<combinedSkins>().skinsToCombine[1];
		content.GetComponent<CommunityList>().skinBorde = rango.GetComponent<combinedSkins>().skinsToCombine[2];
		content.GetComponent<CommunityList>().skinRango = rango.GetComponent<combinedSkins>().skinsToCombine[3];

		content.GetComponent<CommunityList>().pregunta = true;
	}
}
