using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserNetwork : MonoBehaviour {

	public GameObject Player;

	public GameObject nacer;

	public GameObject apuntar;

	public bool crear;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(crear)
		{
			GetComponent<LineRenderer>().SetPosition(0, nacer.transform.position);
			GetComponent<LineRenderer>().SetPosition(1, apuntar.transform.position);
		}else
		{
			GetComponent<LineRenderer>().SetPosition(0, new Vector3(0,0,0));
			GetComponent<LineRenderer>().SetPosition(1, new Vector3(0,0,0));
		}
	}

	private void AddColliderToLine(LineRenderer line, Vector3 startPoint, Vector3 endPoint)
	{
		//create the collider for the line
		BoxCollider lineCollider = new GameObject("LineCollider").AddComponent<BoxCollider>();
		//set the collider as a child of your line
		lineCollider.transform.parent = line.transform; 
		// get width of collider from line 
		float lineWidth = line.endWidth; 
		// get the length of the line using the Distance method
		float lineLength = Vector3.Distance(startPoint, endPoint);      
		// size of collider is set where X is length of line, Y is width of line
		//z will be how far the collider reaches to the sky
		lineCollider.size = new Vector3(lineLength, lineWidth, 1f);   
		// get the midPoint
		Vector3 midPoint = (startPoint + endPoint) / 2;
		// move the created collider to the midPoint
		lineCollider.transform.position = midPoint;


		//heres the beef of the function, Mathf.Atan2 wants the slope, be careful however because it wants it in a weird form
		//it will divide for you so just plug in your (y2-y1),(x2,x1)
		float angle = Mathf.Atan2((endPoint.x - startPoint.x), (endPoint.z - startPoint.z));

		// angle now holds our answer but it's in radians, we want degrees
		// Mathf.Rad2Deg is just a constant equal to 57.2958 that we multiply by to change radians to degrees
		angle *= Mathf.Rad2Deg;

		//were interested in the inverse so multiply by -1
		angle *= -1; 
		// now apply the rotation to the collider's transform, carful where you put the angle variable
		// in 3d space you don't wan't to rotate on your y axis
		lineCollider.transform.Rotate(0, angle, 0);
	}
}
