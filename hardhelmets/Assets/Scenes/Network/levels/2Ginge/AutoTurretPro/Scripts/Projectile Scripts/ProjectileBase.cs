using UnityEngine;
using System.Collections;

public class ProjectileBase : MonoBehaviour {
	//essentially this is for the tracking.
	//base speed
	public float Speed;
	//base target
	public Transform Target;
	//damage
	public float Damage = 1.0f;
}
