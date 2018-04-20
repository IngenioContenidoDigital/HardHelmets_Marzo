using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommunityVSPlayerId : MonoBehaviour {

	public Text nombre;
	public Text level;
	public Text torres;
	public Text bases;

	// Use this for initialization
	void Start ()
	{
		nombre.text = PlayerPrefs.GetString("nameCommunity");
		level.text = PlayerPrefs.GetInt("levelCommunity").ToString();
		torres.text = PlayerPrefs.GetInt("nameTorres").ToString();
		bases.text = PlayerPrefs.GetInt("nameBases").ToString();

		//skinsToCombine[0] = PlayerPrefs.GetString("avatar");
		GetComponent<combinedSkins>().skinsToCombine[1] = PlayerPrefs.GetString("FondoCommunity");
		GetComponent<combinedSkins>().skinsToCombine[2] = PlayerPrefs.GetString("BordeCommunity");
		GetComponent<combinedSkins>().skinsToCombine[3] = "rango"+PlayerPrefs.GetInt("levelCommunity").ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
