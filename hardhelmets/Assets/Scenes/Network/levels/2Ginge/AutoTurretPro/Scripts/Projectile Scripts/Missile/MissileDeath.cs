using UnityEngine;
using System.Collections;

public class MissileDeath : MonoBehaviour
{
	private float currentRange = 100.0f;
	private float explosionRange = 0.0f;
	private bool armed = false;
	private float dist;
    private bool apQuit = false;
	[Tooltip("This is the prefab that is spawned on 'Death'")]
    public GameObject ExplosivePrefab;
    //just in order to give it some values that dont warrant the need for 'GetComponent'
	//to get specific values
    public ParticleSystem explosion;
	[Tooltip("An array such that we can have a complex trail")]
    public ParticleSystem[] trail;
	//for premature explosions.
    [HideInInspector]
    public Transform wantedTarget;
    
    // Use this for initialization
    void Start ()
    {
        explosionRange = ExplosivePrefab.GetComponent<SphereCollider>().radius;
    }
    //stops the instantiation of new objects
    void OnApplicationQuit()
    {
        apQuit = true;
    }
    void OnDestroy()
    {
        if (!apQuit)
        {
            GameObject Obj = Instantiate(ExplosivePrefab, transform.position, Quaternion.identity) as GameObject;

            Destroy(Obj.gameObject, explosion.duration + 1.0f);
        }
    }

    public void Explode()
    {
		for(int i = 0; i < trail.Length; ++i)
		{
			trail[i].transform.parent = null;
			Destroy(trail[i].transform.gameObject, trail[i].duration);
		}
        Destroy(gameObject);
    }
	// Update is called once per frame
	void Update ()
	{
	    if (wantedTarget != null)
	    {
	        dist = Vector3.Distance(transform.position, wantedTarget.position);
	        if (!armed)
	            if (dist < explosionRange)
	            {
	                currentRange = dist + 1;
	                armed = true;
	            }
	        if (armed)
	        {
	            if (dist < currentRange)
	            {
	                currentRange = dist;
	            }
	            else
	            {
	                //lets detonate!!!
                    Explode();
	                
	            }
	        }
	    }
	}
}
