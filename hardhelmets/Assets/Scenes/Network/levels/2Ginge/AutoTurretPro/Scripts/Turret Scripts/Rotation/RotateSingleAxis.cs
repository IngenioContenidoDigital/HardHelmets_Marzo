using UnityEngine;
using System.Collections;

public class RotateSingleAxis : MonoBehaviour {

	//in case we aren't using a transform to target, for example if we are prediction
	private bool positionOverride;
	//temp variable.
	private Vector3 pos;
	//the Target to rotate towards
    public Transform Target;
	//how fast this can rotate
    public float RotationSpeed;
	//the override in order to rotate consistently around, also used to clamp the rotation,
    public Transform UpOverride;
	//max angle FROM the forward.
	public float ClampedAngle = 360;
	//turret script
    public Turret turret;
    // Use this for initialization
	public void VectorPOS(Vector3 position)
	{
		positionOverride = true;
		pos = position;
	}

    void Start()
    {
        turret = GetComponentInParent<Turret>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Target == null)
            return;
        if(turret != null)
            if (!turret.active)
                return;
		//incase of prediction
		if(!positionOverride)
			pos = Target.position;
		//current transformation
        Quaternion Q1 = transform.rotation;
		//lets normalize the direction in order to only rotate on a hyperplane.
		Vector3 uDir = ((pos) - transform.position).normalized;
        Vector3 newRight = Vector3.Cross(uDir, UpOverride.up).normalized;
		//this is the flattened planar direction;
        uDir = Vector3.Cross(UpOverride.up, newRight).normalized;
		//get the current angle;
		float angle = Vector3.Angle(uDir, UpOverride.forward);
		Quaternion Q2 = Quaternion.RotateTowards(Q1, Quaternion.LookRotation(uDir, (UpOverride.up)), RotationSpeed * Time.fixedDeltaTime);
        transform.rotation = Q2;
		//post angle
		float angle2 = Vector3.Angle(uDir, UpOverride.forward);
		//Important bit of logic here:
		/* Basically the following code is to make sure that the rotation doesnt rotate past the clamp
		 * However once we are at that angle then we need to be able to rotate Away from it, so we check if the rotation of Q1 is less than the rotation
		 *  of Q2 respective to the clamp angle)
		 */
		if(angle2 > ClampedAngle)
		{
			transform.rotation = Q1;
		}
		if(angle > ClampedAngle)
		{
			if(angle2 < angle)
			{
				transform.rotation = Q2;
			}
		}

    }
}
