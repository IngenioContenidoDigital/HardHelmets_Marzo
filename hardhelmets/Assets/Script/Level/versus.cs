using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Spine.Unity;
using Prototype.NetworkLobby;

public class versus : MonoBehaviour {

	public string nombre;

	public GameObject Lobby;

	public GameObject Fondo;
	public GameObject Estrella;
	public GameObject P1;
	public GameObject P2;

	void Start ()
	{
		//gameObject.transform.parent = Lobby.transform;
		StartCoroutine(momentoFondo());
		StartCoroutine(momentoEstrella());
		StartCoroutine(momentoP1());
		StartCoroutine(momentoP2());
	}

	IEnumerator momentoFondo()
	{
		yield return new WaitForSpineAnimationComplete(Fondo.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0));
		Fondo.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "loop", true);
	}
	IEnumerator momentoEstrella()
	{
		yield return new WaitForSpineAnimationComplete(Estrella.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0));
		Estrella.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "loop", true);
	}
	IEnumerator momentoP1()
	{
		yield return new WaitForSpineAnimationComplete(P1.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0));
		P1.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "loop", true);
	}
	IEnumerator momentoP2()
	{
		yield return new WaitForSpineAnimationComplete(P2.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0));
		P2.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "loop", true);
	}
}
