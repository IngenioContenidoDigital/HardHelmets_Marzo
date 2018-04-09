using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Steamworks;

public class DisplayName : MonoBehaviour {

	[Header ("UI Component")]
	public Text displayName;
	public Text Nivel;
	public Image avatarImage;

	[Header ("Stat String Data")]
	public string[] statStrings;
	int startC;

	int level;

	void Start () 
	{
		level = PlayerPrefs.GetInt("PlayerLevel");

		if(!SteamManager.Initialized)
		{
			return;
		}

		//Display User Name
		displayName.text = SteamFriends.GetPersonaName();

		//PLAYER LEVEL
		//starCount.text = SteamUser.GetPlayerSteamLevel().ToString();

		Nivel.text = level.ToString();

		//PONE IMAGEN DE AVATAR
		StartCoroutine(_FetchAvatar());

		/*//CUANTAS ESTRELLAS TIENE
		startC = 0;
		foreach(string stat in statStrings)
		{
			int outDate = 0;
			SteamUserStats.GetStat(stat, out outDate);
			startC += outDate;
		}

		starCount.text = startC.ToString();

		//starCount.text = ();*/
	}

	int avatarInt;
	uint width, height;
	Texture2D downloadedAvatar;
	Rect rect = new Rect(0,0,184,184);
	Vector2 pivot = new Vector2(0.5f, 0.5f);

	IEnumerator _FetchAvatar ()
	{
		avatarInt = SteamFriends.GetLargeFriendAvatar(SteamUser.GetSteamID());

			while(avatarInt == -1)
			{
				yield return null;
			}

		if(avatarInt > 0)
		{
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
}
