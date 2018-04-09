using UnityEngine;
using System.Collections;

public class SimpleBullet : ProjectileBase {
    public float Lifetime = 1.0f;
	[RangeAttribute(0.0f,1.0f)]
	public float Inaccuracy = 0.0f;
	void Start()
	{
		transform.forward = (transform.forward + Random.insideUnitSphere * Random.Range(0.0f,Inaccuracy)).normalized;
	}
	// Update is called once per frame
	void Update () {
		
		transform.position += transform.forward * Time.fixedDeltaTime * Speed;
        Lifetime -= Time.fixedDeltaTime;
        if (Lifetime <= 0.0f)
            Destroy(gameObject);
	}
}
