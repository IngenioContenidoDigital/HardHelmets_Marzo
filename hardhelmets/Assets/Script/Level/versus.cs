using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Spine.Unity;
using UnityEngine.Networking;
using Prototype.NetworkLobby;
using UnityEngine.UI;

public class versus : MonoBehaviour {

	public string nombre;

	public GameObject Lobby;

	public GameObject Fondo;
	public GameObject Estrella;
	public GameObject P1;
	public GameObject P2;

	public GameObject Player1;
	public GameObject Player2;

	public GameObject Player1Skin;
	public GameObject Player2Skin;

	public Text name1;
	public Text level1;
	public Text torres1;
	public Text bases1;

	public Text name2;
	public Text level2;
	public Text torres2;
	public Text bases2;

	void Start ()
	{
		//gameObject.transform.parent = Lobby.transform;
		StartCoroutine(momentoFondo());
		StartCoroutine(momentoEstrella());
		StartCoroutine(momentoP1());
		StartCoroutine(momentoP2());
	}

	public void Update ()
	{
		Player1 = GameObject.Find("PlayerInfo1");
		Player2 = GameObject.Find("PlayerInfo2");

		Player1Skin.GetComponent<combinedSkins>().skinsToCombine[0] = Player1.GetComponent<PlayerIdSkinLobby>().avatar;
		Player1Skin.GetComponent<combinedSkins>().skinsToCombine[1] = Player1.GetComponent<PlayerIdSkinLobby>().borde;
		Player1Skin.GetComponent<combinedSkins>().skinsToCombine[2] = Player1.GetComponent<PlayerIdSkinLobby>().fondo;

		Player2Skin.GetComponent<combinedSkins>().skinsToCombine[0] = Player2.GetComponent<PlayerIdSkinLobby>().avatar;
		Player2Skin.GetComponent<combinedSkins>().skinsToCombine[1] = Player2.GetComponent<PlayerIdSkinLobby>().borde;
		Player2Skin.GetComponent<combinedSkins>().skinsToCombine[2] = Player2.GetComponent<PlayerIdSkinLobby>().fondo;

		name1.text = Player1.GetComponent<PlayerIdSkinLobby>().name;
		level1.text = Player1.GetComponent<PlayerIdSkinLobby>().nivel.ToString();
		torres1.text = Player1.GetComponent<PlayerIdSkinLobby>().flags.ToString();
		bases1.text = Player1.GetComponent<PlayerIdSkinLobby>().bas.ToString();

		name2.text = Player2.GetComponent<PlayerIdSkinLobby>().name;
		level2.text = Player2.GetComponent<PlayerIdSkinLobby>().nivel.ToString();
		torres2.text = Player2.GetComponent<PlayerIdSkinLobby>().flags.ToString();
		bases2.text = Player2.GetComponent<PlayerIdSkinLobby>().bas.ToString();
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
