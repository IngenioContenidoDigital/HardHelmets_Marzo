using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine.Unity.Modules.AttachmentTools;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerIdSkinLobby : NetworkBehaviour {

	public GameObject Player;

	[SyncVar]
	public string avatar;
	[SyncVar]
	public string borde;
	[SyncVar]
	public string fondo;

	[SyncVar]
	public string name;

	public UnityEngine.UI.Text nombre;

	[SyncVar]
	public int nivel;
	[SyncVar]
	public int flags;
	[SyncVar]
	public int bas;

	public UnityEngine.UI.Text level;
	public UnityEngine.UI.Text banderas;
	public UnityEngine.UI.Text bases;

	public GameObject boton;

	public void Update ()
	{
		if(isServer)
		{
			boton.GetComponent<LobbyButtonSearch>().Player = "server";
		}
		if(isLocalPlayer)
		{
			print("SOY EL JUGADOR LOCAL");

			name = PlayerPrefs.GetString("SteamName");
			nivel = PlayerPrefs.GetInt("PlayerLevel");
			flags = PlayerPrefs.GetInt("banderas");
			bas = PlayerPrefs.GetInt("bases");

			nombre.text = name;

			level.text = nivel.ToString();
			banderas.text = flags.ToString();
			bases.text = bas.ToString();

			avatar = PlayerPrefs.GetString("avatar");
			borde = PlayerPrefs.GetString("borde");
			fondo = PlayerPrefs.GetString("fondo");

			Player.GetComponent<combinedSkins>().skinsToCombine[0] = avatar;
			Player.GetComponent<combinedSkins>().skinsToCombine[1] = borde;
			Player.GetComponent<combinedSkins>().skinsToCombine[2] = fondo;

			CmdSendName(avatar, borde, fondo, name, nivel, flags, bas);
		}
	}

	[Command]
	public void CmdSendName(string newAvatar, string newBorde, string newFondo, string newName, int newNivel, int newFlags, int newBas)
	{
		RpcGetName (newAvatar, newBorde, newFondo, newName, newNivel, newFlags, newBas);
	}
	[ClientRpc]
	public void RpcGetName (string newAvatar, string newBorde, string newFondo, string newName, int newNivel, int newFlags, int newBas)
	{
		nombre.text = newName;

		level.text = newNivel.ToString();
		banderas.text = newFlags.ToString();
		bases.text = newBas.ToString();

		Player.GetComponent<combinedSkins>().skinsToCombine[0] = newAvatar;
		Player.GetComponent<combinedSkins>().skinsToCombine[1] = newBorde;
		Player.GetComponent<combinedSkins>().skinsToCombine[2] = newFondo;

		if(!isLocalPlayer)
		{
			
		}
	}
}
