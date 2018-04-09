using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.Types;
using System.Collections;
using Prototype.NetworkLobby;
using UnityEngine.EventSystems;

namespace Prototype.NetworkLobby
{
    public class LobbyServerEntry : MonoBehaviour 
    {
        public Text serverInfoText;
        public Text slotInfo;
        public Button joinButton;

		public static bool zona;

		public void Populate(MatchInfoSnapshot match, LobbyManager lobbyManager, Color c)
		{
            serverInfoText.text = match.name;

            slotInfo.text = match.currentSize.ToString() + "/" + match.maxSize.ToString(); ;

			NetworkID networkID = match.networkId;

			joinButton.onClick.RemoveAllListeners();
	        joinButton.onClick.AddListener(() => { JoinMatch(networkID, lobbyManager); });

            GetComponent<Image>().color = c;
        }

        void JoinMatch(NetworkID networkID, LobbyManager lobbyManager)
        {
			lobbyManager.matchMaker.JoinMatch(networkID, "", "", "", 0, 0, lobbyManager.OnMatchJoined);
			lobbyManager.backDelegate = lobbyManager.StopClientClbk;
            lobbyManager._isMatchmaking = true;
            lobbyManager.DisplayIsConnecting();
        }

		public Text ping;

		public bool click;

		public GameObject master;
		public GameObject eventSystem;
		public GameObject barajaList;

		void Update()
		{
			if(master == null)
			{
				master = GameObject.Find("LobbyManager");
			}
			if(eventSystem == null)
			{
				eventSystem = GameObject.Find("EventSystem");
			}
			if(barajaList == null)
			{
				barajaList = GameObject.Find("CartasBoton2");
			}

			/*int i = 0;
			slotInfo.text = Network.GetAveragePing(Network.connections[i] + " ms").ToString();
			//i++;*/
			if(NetworkClient.allClients.Count != 0)
			{
				int pig = NetworkClient.allClients[0].GetRTT();
				ping.text = pig.ToString()+" ms";
			}

			if(zona && selected && Input.GetButtonDown("Jump") || zona && selected && Input.GetButtonDown("Submit"))
			{
				joinButton.onClick.Invoke();
				master.GetComponent<LobbyManager>().actual = "partida";
				eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(barajaList);
			}
		}

		public bool selected; 
    }
}