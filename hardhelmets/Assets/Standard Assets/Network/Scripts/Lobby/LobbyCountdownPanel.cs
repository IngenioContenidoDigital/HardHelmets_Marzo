using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Prototype.NetworkLobby
{
    public class LobbyCountdownPanel : MonoBehaviour
    {
		public GameObject versus;

		void Start ()
		{
			versus.SetActive(false);
		}

		void Update ()
		{
			if(UIText.text == "Match Starting in 2")
			{
				StartCoroutine(espera());
			}
		}

		IEnumerator espera ()
		{
			yield return new WaitForSeconds(1.5f);
			versus.SetActive(true);
		}

        public Text UIText;
    }
}