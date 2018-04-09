using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Prototype.NetworkLobby;

public class serverList : MonoBehaviour {

	public int cantidad;

	public bool up;

	public float mover;

	public GameObject Selector;

	public bool top;
	public bool down;
	public float mover2;

	public static bool zona;

	public bool soltar;

	// Use this for initialization
	void Start ()
	{
		
	}
	public bool mouse;
	public void ratonEntra()
	{
		mouse = true;
		zona = true;
		LobbyServerEntry.zona = true;
	}
	public void ratonSale()
	{
		mouse = false;
		zona = false;
		LobbyServerEntry.zona = false;
	}
	// Update is called once per frame
	void Update ()
	{
		cantidad = transform.childCount;

		/*if(mouse)
		{
			Selector.transform.position = new Vector2(Selector.transform.position.x, Input.mousePosition.y);
			//Selector.GetComponent<RectTransform>().anchoredPosition = new Vector3(Selector.GetComponent<RectTransform>().anchoredPosition.x,Input.mousePosition.y);
		}*/
		if(zona && !soltar)
		{
			if(Input.GetButtonDown("up") && serverListObject.activo != 1 || Input.GetAxis("Vertical") > 0 && serverListObject.activo != 1 || Input.GetAxis("VerticalUI") > 0 && serverListObject.activo != 1)
			{
				if(!top)
				{
					Selector.GetComponent<RectTransform>().anchoredPosition = new Vector3(Selector.GetComponent<RectTransform>().anchoredPosition.x,Selector.GetComponent<RectTransform>().anchoredPosition.y+mover2);
				}else
				{
					content.GetComponent<RectTransform>().anchoredPosition = new Vector3(content.GetComponent<RectTransform>().anchoredPosition.x,content.GetComponent<RectTransform>().anchoredPosition.y-mover);
				}
				soltar = true;
				mouse = false;
				StartCoroutine(momento());
			}
			//ABAJO
			if(Input.GetButtonDown("down") && serverListObject.activo != cantidad || Input.GetAxis("Vertical") < 0 && serverListObject.activo != cantidad || Input.GetAxis("VerticalUI") < 0 && serverListObject.activo != cantidad)
			{
				if(!down)
				{
					Selector.GetComponent<RectTransform>().anchoredPosition = new Vector3(Selector.GetComponent<RectTransform>().anchoredPosition.x,Selector.GetComponent<RectTransform>().anchoredPosition.y-mover2);
				}else
				{
					content.GetComponent<RectTransform>().anchoredPosition = new Vector3(content.GetComponent<RectTransform>().anchoredPosition.x,content.GetComponent<RectTransform>().anchoredPosition.y+mover);
				}
				soltar = true;
				mouse = false;
				StartCoroutine(momento());
			}
		}
	}
	IEnumerator momento()
	{
		yield return new WaitForSeconds(0.2f);
		soltar = false;
	}
	public void entra()
	{
		zona = true;
		LobbyServerEntry.zona = true;
	}
	public void sale()
	{
		zona = false;
		LobbyServerEntry.zona = false;
	}

	public GameObject content;
}
