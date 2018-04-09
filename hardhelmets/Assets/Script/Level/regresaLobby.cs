using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Prototype.NetworkLobby;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;

namespace Prototype.NetworkLobby {

	public class regresaLobby : MonoBehaviour {

		public string actual;
		public string actual2;

		public GameObject cartas;
		public GameObject mano;

		public GameObject mensaje;

		//PANELES
		public GameObject crear;
		public GameObject lista;
		public GameObject jugadores;
		public GameObject servidores;
		public GameObject master;

		public bool jugado;

		public void Update()
		{
			if(master.GetComponent<LobbyManager>().actual == "partida")
			{
				actual = "partida";
			}

			if(cartas == null)
			{
				print("BUSCANDO CARTAS");
				cartas = GameObject.Find("CARTAS");
			}

			if(mano == null)
			{
				mano = GameObject.Find("Mano");
			}


			if(actual != "")
			{
				crear.SetActive(false);
				lista.SetActive(false);
			}else
			{
				jugadores.SetActive(false);
				servidores.SetActive(false);

				crear.SetActive(true);
				lista.SetActive(true);
			}
			if(actual == "partida")
			{
				crear.SetActive(false);
				lista.SetActive(false);

				servidores.SetActive(false);
				jugadores.SetActive(true);
			}
			if(crear == null)
			{
				crear = GameObject.Find("CreateButton");
			}

			if(Input.GetButtonDown("Cancel"))
			{
				if(actual2 != "nivel" && actual2 != "cartas" && actual == "jugadores" ||actual2 != "nivel" && actual2 != "cartas" && actual == "partida")
				{
					cancelButton.onClick.Invoke();
					StartCoroutine(seleccionarboton());
				}else
				{
					Regresar();
				}
			}

			if(Application.loadedLevelName == "LevelNetwork0" && !jugado || Application.loadedLevelName == "LevelNetwork1" && !jugado || Application.loadedLevelName == "LevelNetwork2" && !jugado )
			{
				actual = "jugando";

				crear.SetActive(false);
				lista.SetActive(false);
				jugadores.SetActive(false);
				servidores.SetActive(false);
				jugadores.SetActive(false);
				jugadores.SetActive(false);

				jugado = true;
			}
			if(Application.loadedLevelName == "Lobby" && jugado)
			{
				actual = "";
				actual2 = "";

				jugado = false;
			}
		}
		IEnumerator seleccionarboton()
		{
			yield return new WaitForSeconds(0.5f);
			eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(crearButton);
		}
		public Button cancelButton;

		public void baraja()
		{
			actual2 = "cartas";

			crear.SetActive(false);
			lista.SetActive(false);
			//jugadores.SetActive(false);
			//servidores.SetActive(false);

			cartas.SetActive(true);
			cartas.GetComponent<Animator>().SetBool("entra", true);
			eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(flecha1);
		}
		public void baraja2()
		{
			actual2 = "cartas2";

			crear.SetActive(false);
			lista.SetActive(false);
			//jugadores.SetActive(false);
			//servidores.SetActive(false);

			cartas.SetActive(true);
			cartas.GetComponent<Animator>().SetBool("entra", true);
			eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(flecha1);
		}
		public void Regresar ()
		{
			if(actual2 != "nivel")
			{
				if(actual2 == "cartas")
				{
					if(mano.GetComponent<Mano>().guardar)
					{
						actual2 = "";
						cartas.GetComponent<Animator>().SetBool("sale", true);
						eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(barajaServer);
					}else
					{
						mensaje.SetActive(true);
						StartCoroutine(esconder());
					}
				}else if(actual2 == "cartas2")
				{
					if(mano.GetComponent<Mano>().guardar)
					{
						actual2 = "";
						cartas.GetComponent<Animator>().SetBool("sale", true);
						eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(barajaList);
					}else
					{
						mensaje.SetActive(true);
						StartCoroutine(esconder());
					}
				}else
				{
					if(actual == "jugadores" || actual == "partida")
					{
						actual = "";
						//Network.CloseConnection(Network.connections[0], true);
						Network.Disconnect();
						perfilServer.SetActive(false);
						perfilLista.SetActive(false);

						eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(crearButton);

					}else if(actual == "servidores")
					{
						actual = "";

						perfilServer.SetActive(false);
						perfilLista.SetActive(false);
						eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(crearButton);
					}else
					{
						Application.LoadLevel("Load");
						loading.nombre = "menu";

						Destroy(master);
					}
				}
			}
		}
		public GameObject escenarios;
		IEnumerator esconder()
		{
			yield return new WaitForSeconds(1);
			mensaje.SetActive(false);
		}
		//FUNCIONES BOTONES ESCONDER OTROS PANELES
		public void create()
		{
			actual = "jugadores";
			jugadores.SetActive(true);

			perfilServer.SetActive(true);

			eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(barajaServer);
		}
		public void list()
		{
			actual = "servidores";

			serverListObject.name = 1;
			servidores.SetActive(true);

			perfilLista.SetActive(true);

			eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(barajaList);
		}
		public GameObject perfilServer;
		public GameObject perfilLista;

		public EventSystem eventSystem;

		public GameObject crearButton;
		public GameObject barajaServer;
		public GameObject flecha1;
		public GameObject barajaList;

	}
}
