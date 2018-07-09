using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class maisal : MonoBehaviour {

	public string Loop;
	public string mueve;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update ()
	{
		if(GetComponent<SkeletonAnimation>().AnimationState.GetCurrent(0).Animation.Name != Loop) //.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0).Animation.Name != "loops")
		{
			StartCoroutine(EspAnim());
		}
	}
	IEnumerator EspAnim()
	{
		yield return new WaitForSpineAnimationComplete(GetComponent<SkeletonAnimation>().AnimationState.GetCurrent(0));
		GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, Loop, true);
	}

	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "Player" || col.gameObject.tag == "enemy" || col.gameObject.tag == "tank")
		{
			GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, mueve, false);
		}
	}
}
