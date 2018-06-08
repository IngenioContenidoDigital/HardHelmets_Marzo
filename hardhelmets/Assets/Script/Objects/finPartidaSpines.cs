using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class finPartidaSpines : MonoBehaviour {

	// The [SpineEvent] attribute makes the inspector for this MonoBehaviour
	// draw the field as a dropdown list of existing event names in your SkeletonData.
	[SpineEvent] public string alemania = "ger";
	[SpineEvent] public string marcha = "march"; 

	void Start ()
	{
		var skeletonAnimation = GetComponent<SkeletonAnimation>();
		if (skeletonAnimation == null) return;   // told you to add this to SkeletonAnimation's GameObject.

		// This is how you subscribe via a declared method. The method needs the correct signature.
		skeletonAnimation.state.Event += HandleEvent;
	}

	void HandleEvent (Spine.TrackEntry entry, Spine.Event e)
	{
		if (e.Data.Name == alemania)
		{         
			audio1.clip = ger;
			audio1.Play();
		}
		if (e.Data.Name == marcha)
		{         
			audio2.clip = march;
			audio2.Play();
		}
	}

	[Header("SONIDOS")]
	public AudioSource audio1;
	public AudioSource audio2;

	public AudioClip ger;
	public AudioClip march;
}
