using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class Lobby : NetworkLobbyManager {

	// Use this for initialization
	void Start ()
	{
		MMStar();
		MMListMatches();
	}
	
	void MMStar()
	{
		Debug.Log("@ MMStart");

		this.StartMatchMaker();
	}

	void MMListMatches()
	{
		Debug.Log("@ MMListMatches");
		this.matchMaker.ListMatches(0, 20, "", true, 0, 0, OnMatchList);
	}

	public override void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
	{
		Debug.Log("@ OnMatchList");
		base.OnMatchList(success, extendedInfo, matchList);

		if(!success)
		{
			Debug.Log("error " + extendedInfo);
		}else
		{
			if(matchList.Count > 0)
			{
				Debug.Log("BIEN" + matchList[0]);
				MMJoinMatch(matchList[0]);
			}else
			{
				MMCreateMatch();
			}
		}
	}

	void MMJoinMatch(MatchInfoSnapshot firstMatch)
	{
		Debug.Log("@ MMJoinMatch");

		this.matchMaker.JoinMatch(firstMatch.networkId, "", "", "", 0, 0, OnMatchJoined);
	}

	public override void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo)
	{
		Debug.Log("@ OnMatchJoined");

		base.OnMatchJoined(success, extendedInfo, matchInfo);

		if(!success)
		{
			Debug.Log("error al entrar a la partida" + extendedInfo);
		}else
		{
			Debug.Log("se logro entraR A LA PARTIDA" + matchInfo.networkId);
		}
	}

	void MMCreateMatch()
	{
		Debug.Log("@ MMCreateMatch");

		this.matchMaker.CreateMatch("MM", 2, true, "", "", "", 0, 0, OnMatchCreate);
	}

	public override void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
	{
		Debug.Log("@ OnMatchCreate");

		base.OnMatchCreate(success, extendedInfo, matchInfo);

		if(!success)
		{
			Debug.Log("error al crear la partida" + extendedInfo);
		}else
		{
			Debug.Log("se  CREO LA PARTIDA" + matchInfo.networkId);
		}
	}
}
