using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerEnemy : MonoBehaviour {

	public int puntos;
	int costo;

	int cantidadTankH;
	int cantidadTank;
	int azar;

	public Transform nace;
	public Transform spawn1;
	public Transform spawn2;
	public Transform spawn3;
	public bool base1 = true;
	public bool base2;
	public bool base3;
	public Vector3 nacer;

	//int[] objetos = {1,1,1,1,1,2,2,2,2,2,3,3,3,3,4,4,4,4,5,5,5,6,6,6,7,7,7,8,8,9,9};
	//OBJETOS A CREAR
	public GameObject Fusilero;//1
	public GameObject Escopeto;//2
	public GameObject Submetralleto;//3
	public GameObject Metralleto;//4

	public GameObject Bazuco;//5
	public GameObject Mortero;//6
	public GameObject MG;//7

	public GameObject Lighttank;//8
	public GameObject Heavytank;//9

	public GameObject[] objetos;

	public bool fin;

	void Start ()
	{
		puntos = 0;
		cantidadTankH = 1;
		cantidadTank = 1;
	}

	void Update ()
	{
		if(nace != null)
		{
			if(nace.name == "EnemyBase 1")
			{
				base1 = true;
				base2 = false;
				base3 = false;
			}else if(nace.name == "EnemyBase 2")
			{
				base1 = false;
				base2 = true;
				base3 = false;
			}else if(nace.name == "EnemyBase 3")
			{
				base1 = false;
				base2 = false;
				base3 = true;
			}
		}

		if(base1)
		{
			nacer = new Vector3(spawn1.position.x+Random.Range(-18,17), -24, spawn1.position.z+Random.Range(-16,17));
		}
		if(base2)
		{
			nacer = new Vector3(spawn2.position.x+Random.Range(-18,17),-24,spawn2.position.z+Random.Range(-11,7));
		}
		if(base3)
		{
			nacer = new Vector3(spawn3.position.x+Random.Range(-18,17),-24,spawn3.position.z+Random.Range(-11,9));
		}

		if(!fin)
		{
			if(nace == null)
			{
				nace = GameObject.FindWithTag ("enemyBase").transform;
			}

			puntos += 1;

			if(puntos >= 300)
			{
				var objeto = (GameObject)Instantiate(objetos[Random.Range(0,objetos.Length)], nacer, nace.rotation);
				//azar = Random.Range(1,10);
				puntos = 0;
			}

			/*if(azar == 1)
			{
				var objeto = (GameObject)Instantiate(Fusilero, nacer, nace.rotation);
				azar = 0;
			}else if(azar == 2)
			{
				var objeto = (GameObject)Instantiate(Escopeto, nacer, nace.rotation);
				azar = 0;
			}else if(azar == 3)
			{
				var objeto = (GameObject)Instantiate(Submetralleto, nacer, nace.rotation);
				azar = 0;
			}else if(azar == 4)
			{
				var objeto = (GameObject)Instantiate(Metralleto, nacer, nace.rotation);
				azar = 0;
			}else if(azar == 5)
			{
				var objeto = (GameObject)Instantiate(Bazuco, nacer, nace.rotation);
				azar = 0;
			}else if(azar == 6)
			{
				var objeto = (GameObject)Instantiate(Mortero, nacer, nace.rotation);
				azar = 0;
			}else if(azar == 7)
			{
				var objeto = (GameObject)Instantiate(MG, nacer, nace.rotation);
				azar = 0;
			}else if(azar == 8)
			{
				if(cantidadTank >= 1)
				{
					var objeto = (GameObject)Instantiate(Lighttank, nacer, nace.rotation);
					cantidadTank -= 1;
				}
				azar = 0;
			}else if(azar == 9)
			{
				if(cantidadTankH >= 1)
				{
					var objeto = (GameObject)Instantiate(Heavytank, nacer, nace.rotation);
					cantidadTankH -= 1;
				}
				azar = 0;
			}*/
		}
	}
}
