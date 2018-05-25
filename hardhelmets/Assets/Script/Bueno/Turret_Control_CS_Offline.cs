using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Control_CS_Offline : MonoBehaviour {

	//Vector3 rot;
	public GameObject tanque;
	public Transform objetivo;

	Quaternion otro;

	public void Update ()
	{
		objetivo = tanque.GetComponent<AIVehicle>().target;

		if(objetivo != null)
		{
			/*rot = objetivo.transform.position;
			rot.y = transform.position.y;
			transform.LookAt(rot);*/

			otro = Quaternion.LookRotation(objetivo.transform.position - transform.position);
			otro.x = 0;
			otro.z = 0;
			transform.rotation = Quaternion.Slerp(transform.rotation, otro, 1.5f * Time.deltaTime);
		}else
		{
			otro = Quaternion.LookRotation(transform.position - transform.position);
			otro.x = 0;
			otro.y = 0;
			otro.z = 0;
			transform.localRotation = Quaternion.Slerp(transform.localRotation, otro, 1.5f * Time.deltaTime);
		}
	}
}
