using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class apuntar : MonoBehaviour {
	/*
	//HUESOS A ROTAR
	public Transform hueso;
	public float target;
	public float AngleDeg;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		target = Mathf.Atan2(Input.mousePosition.y - transform.position.y, Input.mousePosition.x - transform.position.x);
		AngleDeg = (180 / Mathf.PI) * target;
		hueso.transform.rotation = Quaternion.Euler(0,0, AngleDeg);
	}
	*/

	public GameObject Player;
	public Animator animator;

	public bool listo;

	Vector3 mousePosition;
	Vector2 todo;
	public float ye;

	public static Vector3 Inicial;

	Vector3 point;

	public bool apunta;

	public GameObject laser;

	public bool mouse;

	void Update ()
	{
		/*point = Camera.main.ScreenToViewportPoint(Input.mousePosition);
		ye = point.y;

		if(point.y <= 0.5f)//HACIA ABAJO
		{
			ye = point.y*15-3;
		}else if(point.y >= 0.6f)//HACIA ARRIBA
		{
			ye = point.y*13;
		}else//RANGO MEDIO DE 0.51 A 0.59
		{
			ye = 4+point.y*2;
		}*/
		if(apunta)
		{
			if(listo)
			{
				if(Input.GetAxis("MIRA") > 0)
				{
					mouse = false;
					if(transform.position.y >  Player.transform.position.y+23)
					{
						transform.position = new Vector3(transform.position.x, Player.transform.position.y+23, transform.position.z);
					}else
					{
						transform.position = new Vector3(transform.position.x, transform.position.y+1, transform.position.z);
					}
				}
				if(Input.GetAxis("MIRA") < 0)
				{
					mouse = false;
					if(transform.position.y < Player.transform.position.y-4)
					{
						transform.position = new Vector3(transform.position.x, Player.transform.position.y-4, transform.position.z);
					}else
					{
						transform.position = new Vector3(transform.position.x, transform.position.y-1, transform.position.z);
					}
				}

				if(Input.GetAxis("Mouse Y") != 0)
				{
					mouse = true;
				}

				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if(Physics.Raycast (ray, out hit, Mathf.Infinity))
				{
					if(mouse)
					{
						transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);//transform.LocalPosition= new Vector3(transform.localPosition.x, hit.point.y, transform.localPosition.z);
					}
				}
				//transform.localPosition = new Vector3(transform.localPosition.x, ye, transform.localPosition.z);
			}else
			{
				if (Player.GetComponent<Hero>()._currentDirection == "right")
				{
					Inicial = new Vector3(Player.transform.position.x+15, Player.transform.position.y+4.1f, Player.transform.position.z);//+15 / +4.1
				}else
				{
					Inicial = new Vector3(Player.transform.position.x-15, Player.transform.position.y+4.1f, Player.transform.position.z);//-15 / +4.1
				}

				transform.position = Vector3.Lerp(transform.position, Inicial, Time.deltaTime * 5);
			}

			if(animator.GetCurrentAnimatorStateInfo(0).IsName("FusilShot") || animator.GetCurrentAnimatorStateInfo(0).IsName("ShotgunShot") ||
				animator.GetCurrentAnimatorStateInfo(0).IsName("SubmetraShot") || animator.GetCurrentAnimatorStateInfo(0).IsName("MetraShot") ||
				animator.GetCurrentAnimatorStateInfo(0).IsName("GunShot") || animator.GetCurrentAnimatorStateInfo(0).IsName("FusilShotWalk") ||
				animator.GetCurrentAnimatorStateInfo(0).IsName("ShotgunShotWalk") || animator.GetCurrentAnimatorStateInfo(0).IsName("SubmetraShotWalk") ||
				animator.GetCurrentAnimatorStateInfo(0).IsName("MetraShotWalk") || animator.GetCurrentAnimatorStateInfo(0).IsName("GunShotWalk") ||
				animator.GetCurrentAnimatorStateInfo(0).IsName("GunPose") || animator.GetCurrentAnimatorStateInfo(0).IsName("GunPoseWalk") ||
				animator.GetCurrentAnimatorStateInfo(0).IsName("Pose") || animator.GetCurrentAnimatorStateInfo(0).IsName("PoseWalk") ||
				animator.GetCurrentAnimatorStateInfo(0).IsName("FusilShotAgachado") ||
				animator.GetCurrentAnimatorStateInfo(0).IsName("ShotgunShotAgachado") || animator.GetCurrentAnimatorStateInfo(0).IsName("SubmetraShotAgachado") ||
				animator.GetCurrentAnimatorStateInfo(0).IsName("MetraShotAgachado") || animator.GetCurrentAnimatorStateInfo(0).IsName("GunShotAgachado") ||
				animator.GetCurrentAnimatorStateInfo(0).IsName("PoseAgachado") || animator.GetCurrentAnimatorStateInfo(0).IsName("GunPoseAgachado"))
			{
				listo = true;
				laser.GetComponent<Laser>().crear = true;
			}else
			{
				listo = false;
				laser.GetComponent<Laser>().crear = false;
			}
		}
	}
}
