using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.NetworkLobby;

public class serverListObject : MonoBehaviour {

	public GameObject boton;
	public Sprite a1;
	public Sprite a2;

	public static int name = 1;

	public static int activo;

	public bool selected;


	// Use this for initialization
	void Start ()
	{
		gameObject.name = name.ToString();
		name += 1;

		selected = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		GetComponent<LobbyServerEntry>().selected = selected;
		if(!LobbyServerEntry.zona)
		{
			boton.GetComponent<UnityEngine.UI.Image>().sprite = a1;
		}else if(selected)
		{
			boton.GetComponent<UnityEngine.UI.Image>().sprite = a2;
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if(col.gameObject.tag == "Player")
		{
			activo = int.Parse(gameObject.name);
			selected = true;
			GetComponent<AudioSource>().Play();
			boton.GetComponent<UnityEngine.UI.Image>().sprite = a2;
		}
	}

	void OnTriggerExit2D (Collider2D col)
	{
		if(col.gameObject.tag == "Player")
		{
			selected = false;
			boton.GetComponent<UnityEngine.UI.Image>().sprite = a1;
		}
	}
}
