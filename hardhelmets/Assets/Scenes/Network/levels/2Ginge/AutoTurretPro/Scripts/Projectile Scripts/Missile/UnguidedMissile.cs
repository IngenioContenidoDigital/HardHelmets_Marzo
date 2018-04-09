using UnityEngine;
using System.Collections;

public class UnguidedMissile : ProjectileBase {

	// Use this for initialization
    public float Lifetime = 5.0f;
	Vector3 rndDir = Vector3.zero;
    private MissileDeath md;
	void Start () {
		Speed = Speed * Random.Range(0.1f,2);
	    md = GetComponent<MissileDeath>();
	}


	// Update is called once per frame
	void Update ()
	{
	    Lifetime -= Time.fixedDeltaTime;
	    if (Lifetime <= 0.0f)
	    {
	        
	        md.Explode();
	    }
	    rndDir += Random.insideUnitSphere;
		rndDir.Normalize();
		transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(rndDir, transform.up), Speed);
		transform.position += (transform.forward).normalized * Speed * Time.fixedDeltaTime * 10;
	}
}
