using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Turret))]
public class SpoolFiring : MonoBehaviour {
	//turret script in order to access the FireProjectile function
	private Turret mTurret;
	//the rotation script of the spooling barrel itself (this script is also used as a blade rotator for a helicopter and a Gimbaling Intake Engine.
    public Rotate rotate;

	//its not like we ACTUALLY need 2000 barrels, though I would be surprised.
	[RangeAttribute(1,2000)]
    public int numberOfBarrels = 1;
	// Use this for initialization
	void Start ()
	{
	    mTurret = GetComponent<Turret>();
	}
	
	// Update is called once per frame, this is how we defer the firing
	void Update () {
        if (numberOfBarrels < 1)
            numberOfBarrels = 1;
		//only if the turret is active will this run.
		rotate.running = mTurret.active;
		//however if there is no target, then stop running.
		if(mTurret.Target == null)
			rotate.running = false;
		//just a bit of logic to make the barrel seem rotationally based.
		if(rotate.totalRotation > 360.0f/numberOfBarrels)
        {
			rotate.totalRotation -= 360.0f / numberOfBarrels;
			//if its angled toward the target. (specifically the PROJECTED target)
			if(Vector3.Angle(mTurret.ForwardT.forward, (mTurret.projectedTarget - mTurret.ForwardT.position).normalized) <= mTurret.FiringAngle)
			{
                mTurret.FireProjectile();
			}
        }
	
	}
}
