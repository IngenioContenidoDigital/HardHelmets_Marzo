using UnityEngine;
using System.Collections;


public class Rotate : MonoBehaviour {

	//used to spool up the rotation
	private float currentTime = 0.0f;
	//applied per frame
	private Vector3 currentRotationSpeed;
	// Use this for initialization
	public Vector3 LocalRotationSpeed;
	[HideInInspector]
    public float totalRotation = 0.0f;
	//if its running
	public bool running;
	//how quickly it spools up to full speed.
	public float spoolUpTime = 1.0f;
	//already at 100%
	public bool prewarm = false;
	void Start () {
		currentTime = 0.0f;
		if(prewarm)
			currentTime = spoolUpTime;
	}
	
	// Update is called once per frame
	void Update () {
		if(running)
		{
			currentTime += Time.fixedDeltaTime;
			if(currentTime > spoolUpTime)
				currentTime = spoolUpTime;
		}
		else
		{
			currentTime -= Time.fixedDeltaTime;
			if(currentTime < 0)
				currentTime = 0;
		}
		if(spoolUpTime != 0)
		{
			currentRotationSpeed = Vector3.Lerp(Vector3.zero, LocalRotationSpeed,(currentTime/spoolUpTime));
		}
		else
		{
			currentRotationSpeed = LocalRotationSpeed;
		}
		//an pigeon way to go
		totalRotation += (currentRotationSpeed.x + currentRotationSpeed.y + currentRotationSpeed.z) * Time.fixedDeltaTime;
        //now to rotate
		transform.Rotate(currentRotationSpeed * Time.fixedDeltaTime);
	
	}
}
