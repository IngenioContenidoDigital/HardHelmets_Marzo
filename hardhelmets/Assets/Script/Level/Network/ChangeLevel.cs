using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Prototype.NetworkLobby
{
	public class ChangeLevel : NetworkBehaviour {

		public GameObject lob;

		[SyncVar]
		public int nivel;

		public GameObject boton;

		public UnityEngine.UI.Image imagen;
		public Sprite cero;
		public Sprite uno;
		public Sprite dos;

		public UnityEngine.UI.Image lev0;
		public Sprite ceroa;
		public Sprite cerob;
		public UnityEngine.UI.Image lev1;
		public Sprite unoa;
		public Sprite unob;
		public UnityEngine.UI.Image lev2;
		public Sprite dosa;
		public Sprite dosb;

		public UnityEngine.UI.Image level;
		public Sprite l0;
		public Sprite l1;
		public Sprite l2;

		public bool azar;

		int contar;

		public GameObject master;

		// Use this for initialization
		void Start ()
		{
			
		}
		
		public void Update ()
		{
			if(isLocalPlayer)
			{
				boton.GetComponent<UnityEngine.UI.Button>().enabled = true;

				master = GameObject.Find("Canvas - Mensajes");
				eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
			}
			if(ready)
			{
				but.sprite = silisto;
			}else
			{
				but.sprite = nolisto;
			}


			if(escenarios.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("SceneEntra"))
			{
				escenarios.GetComponent<Animator>().SetBool("entra", false);
			}
			if(escenarios.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("SceneSale"))
			{
				escenarios.GetComponent<Animator>().SetBool("sale", false);
			}

			if(lob == null)
			{
				lob = GameObject.Find("LobbyManager");
			}

			if(nivel == 0)
			{
				imagen.sprite = cero;

				lev0.sprite = cerob;
				lev1.sprite = unoa;
				lev2.sprite = dosa;

				level.sprite = l0;
			}else if(nivel == 1)
			{
				imagen.sprite = uno;

				lev0.sprite = ceroa;
				lev1.sprite = unob;
				lev2.sprite = dosa;

				level.sprite = l1;
			}else if(nivel == 2)
			{
				imagen.sprite = dos;

				lev0.sprite = ceroa;
				lev1.sprite = unoa;
				lev2.sprite = dosb;

				level.sprite = l2;
			}
				
			if(isLocalPlayer)
			{
				lob.GetComponent<LobbyManager>().tablero[0] = nivel;

			}else
			{
				lob.GetComponent<LobbyManager>().tablero[1] = nivel;
			}

			if(azar)
			{
				contar += 1;
				if(contar >= 4)
				{
					GetComponent<AudioSource>().Play();
					nivel = Random.Range(0,3);
					contar = 0;
				}
				CmdSendLevel(nivel);
			}
			if(Input.GetButtonDown("Cancel"))
			{
				cerrar();
			}
		}

		[Command]
		public void CmdSendLevel (int newNivel)
		{
			RpcGetLevel(newNivel);
		}

		[ClientRpc]
		public void RpcGetLevel (int newNivel)
		{
			if(!isLocalPlayer)
			{
				nivel = newNivel;
			}
		}

		public GameObject escenarios;

		bool primera;

		public EventSystem eventSystem;
		public GameObject boton1;
		public GameObject boton2;

		public void press ()
		{
			master.GetComponent<regresaLobby>().actual2 = "nivel";
			escenarios.SetActive(true);
			if(!primera && !isServer)
			{
				print("CORRER ESCENARIOS ARRIBA");
				escenarios.GetComponent<RectTransform>().anchoredPosition = new Vector3(escenarios.GetComponent<RectTransform>().anchoredPosition.x, escenarios.GetComponent<RectTransform>().anchoredPosition.y+150, 0);
				primera = true;
			}
			escenarios.GetComponent<Animator>().SetBool("entra", true);

			print("boton seleccionado");
			eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(boton1);
		}

		public void cerrar ()
		{
			azar = false;
			escenarios.GetComponent<Animator>().SetBool("sale", true);
			master.GetComponent<regresaLobby>().actual2 = "jugadores";
			eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(boton2);
			//escenarios.SetActive(false);
		}

		public void scene0 ()
		{
			azar = false;
			nivel = 0;
			if(!isServer)
			{
				CmdSendLevel(nivel);
			}
		}
		public void scene1 ()
		{
			azar = false;
			nivel = 1;
			if(!isServer)
			{
				CmdSendLevel(nivel);
			}
		}
		public void scene2 ()
		{
			azar = false;
			nivel = 2;
			if(!isServer)
			{
				CmdSendLevel(nivel);
			}
		}

		public void Ran ()
		{
			azar = true;
		}

		bool ready;

		public UnityEngine.UI.Image but;
		public Sprite silisto;
		public Sprite silisto2;
		public Sprite nolisto;
		public Sprite nolisto2;

		public GameObject listo;
		public bool set;

		public void entry ()
		{
			ready = !ready;
			set = true;
		}

		[Command]
		public void CmdSetBoton (bool newSet)
		{
			RpcSetBoton (newSet);
		}

		[ClientRpc]
		public void RpcSetBoton (bool newSet)
		{
			if(!isLocalPlayer)
			{
				listo.SetActive(newSet);
			}
		}

		public void cambia ()
		{
			if(ready)
			{
				but.sprite = silisto2;
			}else
			{
				but.sprite = nolisto2;
			}
		}
		public void cambia2 ()
		{
			if(ready)
			{
				but.sprite = silisto;
			}else
			{
				but.sprite = nolisto;
			}
		}
	}
}
