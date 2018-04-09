﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Steamworks;

public class SteamLogros : MonoBehaviour {

	public Image avatarImage;
	public Text descipcion;

	int avatarInt;
	uint width, height;
	Texture2D downloadedAvatar;
	Rect rect = new Rect(0,0,184,184);
	Vector2 pivot = new Vector2(0.5f, 0.5f);

	void Start ()
	{
		StartCoroutine(_FetchAvatar("WIN_FIRST_BATTLE_ONLINE"));
		//SteamUserStats.RequestCurrentStats();
	}

	IEnumerator _FetchAvatar (string ID)
	{
		avatarInt = SteamUserStats.GetAchievementIcon(ID);
		//avatarInt = SteamUserStats.GetAchievementIcon("WIN_FIRST_BATTLE_ONLINE");//SteamFriends.GetLargeFriendAvatar(SteamUser.GetSteamID());

		while(avatarInt <= 0)
		{
			print("NADA");
			yield return null;
		}

		if(avatarInt > 0)
		{
			print("MOSTRAR IMAGEN");
			SteamUtils.GetImageSize(avatarInt, out width, out height);

			if(width > 0 && height > 0)
			{
				byte[] avatarSteam = new byte[4 * (int)width * (int)height];

				SteamUtils.GetImageRGBA(avatarInt, avatarSteam, 4 * (int)width * (int)height);

				downloadedAvatar = new Texture2D((int)width, (int)height, TextureFormat.RGBA32, false);

				downloadedAvatar.LoadRawTextureData(avatarSteam);
				downloadedAvatar.Apply();

				avatarImage.sprite = Sprite.Create(downloadedAvatar, rect, pivot);
			}
		}
	}

	public void Request()
	{
		/*if(SteamUser() == null)
		{
			print("NO PUEDE CARCAR");
		}*/
	}

	public enum Achievement : int
	{
		WIN_FIRST_BATTLE_ONLINE
	};

	private Achievement_t[] m_Achievements = new Achievement_t[]
	{
			new Achievement_t(Achievement.WIN_FIRST_BATTLE_ONLINE, "FINISH_WORLD_1", "finir le monde 1"),
	};

	public void Update()//public string toString
	{
		string content = "";

		foreach (Achievement_t ach in m_Achievements)
		{
			content += "\n id : " + ach.m_eAchievementID.ToString() + " (achieved  ? " + ach.m_bAchieved + ")";
			content += "\n  name : " + ach.m_strName + " -  desc : " + ach.m_strDescription;
		}

		print(content);
		//return content;
	}

	public class Achievement_t
	{
		public Achievement m_eAchievementID;
		public string m_strName;
		public string m_strDescription;
		public bool m_bAchieved;

		/// <summary>
		/// Creates an Achievement. You must also mirror the data provided here in https://partner.steamgames.com/apps/achievements/yourappid
		/// </summary>
		/// <param name="achievement">The "API Name Progress Stat" used to uniquely identify the achievement.</param>
		/// <param name="name">The "Display Name" that will be shown to players in game and on the Steam Community.</param>
		/// <param name="desc">The "Description" that will be shown to players in game and on the Steam Community.</param>
		public Achievement_t(Achievement achievementID, string name, string desc)
		{
			m_eAchievementID = achievementID;
			m_strName = name;
			m_strDescription = desc;
			m_bAchieved = false;
		}
	}
} 
