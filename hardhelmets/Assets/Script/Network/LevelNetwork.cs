using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LevelNetwork : NetworkBehaviour {

	public UnityEngine.UI.Image barra;

	[SyncVar]//[SyncVar(hook = "tiempo")]
	public int minutes = 3;
	[SyncVar]//[SyncVar(hook = "tiempo")]
	public float seconds = 0;

	[SyncVar]
	public float partida = 180;
	public float partidaTotal;
	public UnityEngine.UI.Text contador;

	bool crono = true;

	// Use this for initialization
	void Start ()
	{
		if(!isServer)
		{
			return;
		}

		partida = minutes*60;
		partidaTotal = partida;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!isServer)
		{
			return;
		}

		if(seconds > 0 && crono)
		{
			seconds -= Time.deltaTime;
		}

		if(seconds <= 0 && crono)
		{
			minutes -= 1;
			seconds = 60;
		}

		if(minutes <= 0 && seconds <= 0)
		{
			crono = false;
			print("FIN");
		}

		contador.text = minutes+":"+seconds;

		partida = partida-Time.deltaTime;
			
		barra.fillAmount = partida/partidaTotal;
	}

	/*void tiempo(int minutes, float seconds)
	{
		
	}*/
}
