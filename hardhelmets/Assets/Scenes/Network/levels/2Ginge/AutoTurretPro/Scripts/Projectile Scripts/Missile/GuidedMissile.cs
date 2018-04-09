using UnityEngine;
using System.Collections;

public class GuidedMissile : ProjectileBase {

	private Vector3 previousTargetPos;
	private Vector3 previousTargetPos2;
	private Vector3 projectedTarget;
	private Vector3 velocity;
	private MissileDeath md;
	private Vector3 rndDir = Vector3.zero;

	public float maxSpeed;
	public float acceleration;
	[Tooltip("In Degrees per second")]
	public float rotationSpeed;
    public float Lifetime = 1.0f;
	[RangeAttribute(0.0f,1.0f)]//generally we cant fire backwards...
    public float Inaccuracy = 1.0f;
	public bool PredictLocation = false;
    // Use this for initialization
    void Start () {
		if(Target != null)
			previousTargetPos = Target.position;
        md = GetComponent<MissileDeath>();
    }
    void OnDrawGizmosSelected()
	{
        Gizmos.DrawSphere(projectedTarget, 1.0f);
    }
	// Update is called once per frame
	void Update () {
		//lets slowly rotate around without causing jerky moving (generally +RandomDirection * 0.5 is enough because it cant change horribly).
        rndDir += Random.insideUnitSphere;
        rndDir.Normalize();
        Lifetime -= Time.fixedDeltaTime;
        if (Lifetime <= 0.0f)
        {
            md.Explode();
        }
		Vector3 tPos = transform.position + transform.forward;
		if(Target != null)
			tPos = Target.position;
		if(PredictLocation)
        	transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(((projectedTarget - transform.position).normalized + rndDir * Inaccuracy).normalized, transform.up),rotationSpeed * Time.fixedDeltaTime);
		else
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(((tPos - transform.position).normalized + rndDir * Inaccuracy).normalized, transform.up),rotationSpeed * Time.fixedDeltaTime);
		if(velocity.magnitude < maxSpeed)
		{
			velocity += transform.forward * acceleration;
		}
		else
		{
				velocity = transform.forward * maxSpeed;
			//lets simply keep the speed and rotate;
		}
        transform.position += (velocity) * Time.fixedDeltaTime;
		//given where we think the target will be, try aiming at it.
		//value is: (1/2) * (a * t ^ 2) + v0 * t + pos;
		if(PredictLocation)
		{
		    float T;
			if (Target != null)
		    {
				T = (Target.position - transform.position).magnitude / velocity.magnitude;
				Vector3 accel = (Target.transform.position - previousTargetPos) - (previousTargetPos - previousTargetPos2);
		        Vector3 pos = ((accel * Mathf.Pow(T, 2))) * 0.5f;
				pos += (Target.transform.position - previousTargetPos) * T + Target.position;
		        projectedTarget = pos;
		        previousTargetPos2 = previousTargetPos;
				previousTargetPos = Target.position;
		    }
		}



	}
}
