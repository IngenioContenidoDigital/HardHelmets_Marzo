using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class spinePasaALoop : MonoBehaviour {

	public string Loop;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0).Animation.Name != Loop) //.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0).Animation.Name != "loops")
		{
			StartCoroutine(EspAnim());
		}
	}
	IEnumerator EspAnim()
	{
		yield return new WaitForSpineAnimationComplete(GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0));
		GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, Loop, true);
	}
}
