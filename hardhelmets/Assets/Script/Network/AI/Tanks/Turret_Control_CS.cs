using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Turret_Control_CS : MonoBehaviour
{
	//Vector3 rot;
	public GameObject tanque;
	public Transform objetivo;

	Quaternion otro;

	public void Update ()
	{
		objetivo = tanque.GetComponent<AITank2>().target;

		if(objetivo != null)
		{
			/*rot = objetivo.transform.position;
			rot.y = transform.position.y;
			transform.LookAt(rot);*/

			otro = Quaternion.LookRotation(objetivo.transform.position - transform.position);
			otro.x = 0;
			otro.z = 0;
			transform.rotation = Quaternion.Slerp(transform.rotation, otro, 1 * Time.deltaTime);
		}else
		{
			otro = Quaternion.LookRotation(transform.position - transform.position);
			otro.x = 0;
			otro.y = 0;
			otro.z = 0;
			transform.localRotation = Quaternion.Slerp(transform.localRotation, otro, 1 * Time.deltaTime);
		}
	}
}
