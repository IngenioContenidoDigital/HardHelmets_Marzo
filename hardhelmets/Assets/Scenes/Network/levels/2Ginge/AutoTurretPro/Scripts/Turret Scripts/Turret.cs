using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour
{
	
	//this gets set from the provided prefab
	private float prefabSpeed;
	private Vector3 prevDir = Vector3.zero;
	//prediction variables
	[HideInInspector]
	//the final projected target
	public Vector3 projectedTarget;
	//these two are used to find the velocity and acceleration of the target
	private Vector3 previousTargetPos;
	private Vector3 previousTargetPos2;
	//storage of rotational components.
	private RotateSingleAxis[] RSAs;
	//where the 'firing' sequence is in the loop (simulates a wrapping array.. kind of)
	private int currentIndex = 0;
	//Per Spawn cooldown
	private float[] cooldowns;
	//the current local spawnCooldown
	private float spawnCooldown = 0.0f;


	//Projectile to spawn
    public GameObject ProjectilePrefab;
	//where we can spawn from
    public Transform[] Spawners;
	//how quickly we can spawn
    public float spawnInterval = 0.0f;
	//how many Projectiles we can spawn BEFORE triggering the Spawn Cooldown.
    public int spawnMax = 0;
	//per 'salvo' cooldown (basically instantiates x projectiles where x is spawnMax).
    public float SpawnCooldown = 1.0f;
	//this is the transform that is the 'eye' for firing
    public Transform ForwardT;
	//Required angle in order to start instantiating projectiles
    public float FiringAngle = 10.0f;
    public bool active = true;
	//if the firing of projectiles is deferred to a different script (eg spooling)
    public bool deferFiring = false;
    public Transform Target;
	public bool PredictLocation = false;
    [RangeAttribute(1,10)]
	//really this is only going to improve it very slightly
    public int PredictionAccuracy = 1;


	//use this function in order to update the corresponding targeting structures.
	void UpdateTarget(Transform t)
	{
		//Updates current target
		Target = t;
		//per Rotation Axis script, lets update the target.
		for(int i = 0; i < RSAs.Length; ++i)
		{
			//set the target
			RSAs[i].Target = t;
			//also override the position because
			RSAs[i].VectorPOS(projectedTarget);
		}
	}
	// Use this for initialization
	void Start ()
	{
		//find the constituent rotational axis for updating
		RSAs = GetComponentsInChildren<RotateSingleAxis>();
		for(int i = 0; i < RSAs.Length; ++i)
		{
			RSAs[i].Target = Target;
		}
		//initializing
        if(Target != null)
		previousTargetPos = Target.position;
		//how fast a projectile moves in a single frame (used for prediction).
		prefabSpeed = ProjectilePrefab.GetComponent<ProjectileBase>().Speed * Time.fixedDeltaTime;
		//clamping the values so no errors occur.
		spawnMax = Mathf.Clamp(spawnMax, 0, Spawners.Length);
		//creating new variables to keep track of any potential overflow.
        cooldowns = new float[Spawners.Length];
		//initialization
	    for (int i = 0; i < cooldowns.Length; ++i)
	    {
	        cooldowns[i] = 0.0f;
	    }
	}
    void UpdateTimers()
    {
        spawnCooldown -= Time.fixedDeltaTime;
        for (int i = 0; i < cooldowns.Length; ++i)
        {
            cooldowns[i] -= Time.fixedDeltaTime;
        }
    }
	//firing function
    public void FireProjectile()
    {
        if (!active)
            return;
        if (currentIndex >= Spawners.Length-1)
            currentIndex = 0;
		GameObject obj = Instantiate(ProjectilePrefab, Spawners[currentIndex].position, Spawners[currentIndex].rotation) as GameObject;
		MissileDeath mid = obj.GetComponent<MissileDeath>();
		if(mid != null)
		{
			mid.wantedTarget = Target;
		}
        GuidedMissile gm = obj.GetComponent<GuidedMissile>();
        if (gm != null)
            gm.Target = Target;
        cooldowns[currentIndex] = spawnInterval;
        //SET THE OBJ's Target to our Target.
        currentIndex += 1;
    }
	// prediction is using speed and acceleration. (Recursive)
    Vector3 PredictPOS2(int count, Vector3 pPos)
    {
        //Base Case
        if (count == 0)
        {
			//updating fields
            projectedTarget = pPos;
            previousTargetPos2 = previousTargetPos;
            previousTargetPos = Target.position;
            if (active)
                for (int i = 0; i < RSAs.Length; ++i)
                RSAs[i].VectorPOS(projectedTarget);
            return pPos;
        }

        Vector3 cTarget = pPos;
		//time to target
        float T = ((cTarget - ForwardT.position).magnitude / (prefabSpeed));
		//velocity of target, note that if the target hasn't moved then this can be zero.
        Vector3 velocity = (Target.position - previousTargetPos);
		//this keeps a buffer on the velocity incase of 'zero' velocity 
		//(can occur if there are two frames of this script between two updates of physics or other transform altering occurances).
        if (velocity.magnitude == 0)
        {
			//apply an already saved velocity
            velocity = prevDir;
            //such that we can zero out the velocity after stopping. (slowly).
            prevDir = prevDir * 0.9f;
            //Debug.LogError("Zero'dOut");
        }
        else
        {
			//we have velocity, lets save it for later.
            prevDir = velocity;
        }
		//slightly alterred version of: X = Xo + V*T + 1/2(A * T ^ 2)
        cTarget = Target.position + velocity * T - velocity * prefabSpeed + (((Target.position - previousTargetPos) - (previousTargetPos - previousTargetPos2)) * 0.5f * Mathf.Pow(T, 2));
        //recurse
        return PredictPOS2((--count),cTarget);
    }
	// Update is called once per fixed frame period
	void FixedUpdate ()
	{
	    if (Target == null)
	        return;
		if (ProjectilePrefab == null)
	        return;
		Vector3 tPos = Target.position;
		//Incase we are predicting the position.
		if(PredictLocation)
			tPos = PredictPOS2(PredictionAccuracy, Target.position);
		else
		{
			projectedTarget = Target.position;
		}
		//we dont need to handle timers or firing if its deferred.
	    if (deferFiring)
	        return;
		
        UpdateTimers();

		//if the firing vector is over the angle threshold dont fire, but still update timers.
		if (Vector3.Angle(ForwardT.forward, (tPos - ForwardT.position).normalized) > FiringAngle)
	        return;
	    
        //lets spawn projectiles
	    if (spawnCooldown <= 0.0f)
	    {
	        for (int spawnNum = 0; spawnNum < spawnMax; ++spawnNum)
	        {
	            int index = spawnNum + currentIndex;
	            while (index >= Spawners.Length)
                    index = index - Spawners.Length;
	            if (cooldowns[index] > 0.0f)
	                break;
	            Invoke("FireProjectile", (spawnNum + 1) * spawnInterval);
	            cooldowns[index] = (spawnNum + 1) * spawnInterval;
	        }
	        spawnCooldown = SpawnCooldown;
	    }
	}
}
