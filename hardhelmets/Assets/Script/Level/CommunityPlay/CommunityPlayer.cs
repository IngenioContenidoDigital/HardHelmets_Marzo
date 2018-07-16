using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunityPlayer : MonoBehaviour {

	public int[] carta;

	public int tirar;
	public int valor;

	public int level;

	public float barra;

	public float carga;

	public bool otra;

	public GameObject a10;
	public GameObject a11;
	public GameObject a12;
	public GameObject a13;
	public GameObject a14;
	public GameObject a15;
	public GameObject a16;
	public GameObject a18;
	public GameObject a19;
	public GameObject a20;
	public GameObject a21;
	public GameObject a22;
	public GameObject a23;
	public GameObject a24;

	public GameObject crear;

	public GameObject Alpha;
	public GameObject Beta;

	public GameObject[] nacerBase;
	public GameObject[] nacerAlpha;
	public GameObject[] nacerBeta;

	// Use this for initialization
	void Start ()
	{
		level = PlayerPrefs.GetInt("levelCommunity");
		carga = 0.0002f+(float)level/25000;
		barra = 0;

		carta[0] = PlayerPrefs.GetInt("ManoComunity1");
		carta[1] = PlayerPrefs.GetInt("ManoComunity2");
		carta[2] = PlayerPrefs.GetInt("ManoComunity3");
		carta[3] = PlayerPrefs.GetInt("ManoComunity4");
		carta[4] = PlayerPrefs.GetInt("ManoComunity5");
		carta[5] = PlayerPrefs.GetInt("ManoComunity6");
		carta[6] = PlayerPrefs.GetInt("ManoComunity7");
		carta[7] = PlayerPrefs.GetInt("ManoComunity8");
		carta[8] = PlayerPrefs.GetInt("ManoComunity9");
		carta[9] = PlayerPrefs.GetInt("ManoComunity10");

		tirar = carta[Random.Range(0,carta.Length)];
		valor = PlayerPrefs.GetInt(tirar.ToString()+"costo");
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		barra += carga;

		if(barra >= 1)
		{
			barra = 1;
		}
		if(barra <= 0)
		{
			barra = 0;
		}

		//LISTO PARA ITRAR CARTA
		if(barra >= (float)valor/100)
		{
			barra -= (float)valor/100;

			if(tirar == 10)
			{
				crear = a10;
				objeto();
			}
			if(tirar == 11)
			{
				crear = a11;
				objeto();
			}
			if(tirar == 12)
			{
				crear = a12;
				objeto();
			}
			if(tirar == 13)
			{
				crear = a13;
				objeto();
			}
			if(tirar == 14)
			{
				crear = a14;
				objeto();
			}
			if(tirar == 15)
			{
				crear = a15;
				objeto();
			}
			if(tirar == 16)
			{
				crear = a16;
				objeto();
			}
			if(tirar == 18)
			{
				crear = a18;
				objeto();
			}
			if(tirar == 19)
			{
				crear = a19;
				objeto();
			}
			if(tirar == 20)
			{
				crear = a20;
				objeto();
			}
			if(tirar == 21)
			{
				if(PlayerPrefs.GetString("factionMala") == "b")
				{
					crear = a21;
				}else
				{
					crear = a22;
				}
				objeto();
			}
			if(tirar == 22)
			{
				if(PlayerPrefs.GetString("factionMala") == "b")
				{
					crear = a23;
				}else
				{
					crear = a24;
				}
				objeto();
			}

			otra = true;
		}

		if(otra)
		{
			tirar = carta[Random.Range(0,carta.Length)];
			valor = PlayerPrefs.GetInt(tirar.ToString()+"costo");

			otra = false;
		}
	}

	public void objeto ()
	{
		if(Beta != null && Beta.tag == "BaseMala")
		{
			var objeto = (GameObject)Instantiate(crear, nacerBeta[Random.Range(0,nacerBeta.Length)].transform.position, transform.rotation);
		}else if(Alpha.tag == "BaseMala")
		{
			var objeto = (GameObject)Instantiate(crear, nacerAlpha[Random.Range(0,nacerAlpha.Length)].transform.position, transform.rotation);
		}else
		{
			var objeto = (GameObject)Instantiate(crear, nacerBase[Random.Range(0,nacerBase.Length)].transform.position, transform.rotation);
		}
	}
}
