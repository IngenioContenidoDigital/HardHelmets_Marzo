using UnityEngine;
using System.Collections;

public class DamageableObject : MonoBehaviour {

	private Vector3 pos, pos2;
	//A TERRIBLELY implemented script to move an object from position to position
	//not as bad as shuffle-sort algorithm.
	public float speed;
	//how much damage this object can take before being destroyed
    public float Health = 100;
	void Start()
	{
		//current pos
		pos = transform.position;
		//other pos simply mirrored on Z from origin
		pos2 = new Vector3(pos.x, pos.y, pos.z * -1);
	}
	void FixedUpdate()
	{
		//lets update huzzah, 
	    float t = Mathf.PingPong(Time.fixedTime * speed, 1.0f);
	    t = 3 * Mathf.Pow(t, 2) - 2 * Mathf.Pow(t, 3);
        transform.position = Vector3.Lerp(pos,pos2, t);
	}
	void ApplyDamage(Transform other)
	{
		ProjectileBase projBase = other.transform.GetComponent<ProjectileBase>();
		Health -= projBase.Damage;
	}
	//the magic occurs
    void OnTriggerEnter(Collider other)
    {
		//if its a missile
        if (other.transform.tag == "Explosion")
        {
			//lets damage this object then destroy the damager (also check for destruction)
			ApplyDamage(other.transform);
            if(Health <= 0.0f)
                Destroy(gameObject);
        }
		//or if its a bullet
        if (other.transform.tag == "Bullet")
        {
			//lets damage this object then destroy the damager (also check for destruction)
			ApplyDamage(other.transform);
            if (Health <= 0.0f)
                Destroy(gameObject);
            Destroy(other.gameObject);
        }

    }
}
