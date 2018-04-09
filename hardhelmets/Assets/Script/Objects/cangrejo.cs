using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cangrejo : MonoBehaviour {

	public bool caminar;
	bool caminando;

	int dir;
	int dir2;

	Vector3 v3;

	public GameObject Enemy;
	bool alejar;

	void Start ()
	{
		StartCoroutine(mover());
	}
	
	// Update is called once per frame
	void Update ()
	{
		Enemy = GameObject.Find("Hero");

		//foreach(GameObject malo in Enemy)
		//{
		if(Vector3.Distance(transform.position, Enemy.transform.position) <= 10)
		{
			alejar = true;
			v3 = transform.position - Enemy.transform.position;
			v3.Normalize();
		}else
		{
			alejar = false;
		}
		//}
		if(alejar)
		{
			caminar = false;
			GetComponent<Animator>().SetBool("caminar", true);
			GetComponent<Rigidbody>().velocity = (4f * v3.normalized);
		}

		if(caminar)
		{
			GetComponent<Animator>().SetBool("caminar", true);

			if(dir == 1)
			{
				v3 += Vector3.left;
			}
			if(dir == 2)
			{
				v3 += Vector3.right;
			}
			if(dir2 == 1)
			{
				v3 += Vector3.forward;
			}
			if(dir2 == 2)
			{
				v3 += Vector3.back;
			}
		}else
		{
			v3 = Vector3.zero;
			GetComponent<Animator>().SetBool("caminar", false);
		}
		if(v3 != Vector3.zero)
		{
			GetComponent<Rigidbody>().velocity = (2f * v3.normalized); 
		}
	}

	IEnumerator mover ()
	{
		yield return new WaitForSeconds(Random.Range(2,6));
		dir = Random.Range(1,3);
		dir2 = Random.Range(1,3);
		caminar = true;
		StartCoroutine(espera());
	}
	IEnumerator espera ()
	{
		yield return new WaitForSeconds(Random.Range(4,9));
		caminar = false;
		StartCoroutine(mover());
	}
}
