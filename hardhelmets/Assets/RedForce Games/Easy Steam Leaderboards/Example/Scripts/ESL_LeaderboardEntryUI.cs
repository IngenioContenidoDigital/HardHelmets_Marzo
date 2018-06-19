using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using EasySteamLeaderboard;

public class ESL_LeaderboardEntryUI : MonoBehaviour
{
	//ase
	public Text RankText;
	public Text PlayerNameText;
	public Text ScoreText;

	public GameObject Usuario;
	public GameObject borde;

	public void Update()
	{
		if(Usuario == null)
		{
			Usuario = GameObject.Find("UserRank");
		}else
		{
			if(Usuario.GetComponent<Text>().text == RankText.text && gameObject.name != "YourEntry")
			{
				borde.SetActive(true);
			}
		}
	}

	public void Initialize(ESL_LeaderboardEntry entry)
	{
		if (entry == null)
		{
			Reset();
			return;
		}

		PlayerNameText.text = entry.PlayerName;
		RankText.text = entry.GlobalRank.ToString();
		ScoreText.text = entry.Score;
	}

	public void Initialize(string pname, int rank, string score)
	{
		PlayerNameText.text = pname;
		RankText.text = rank.ToString();
		ScoreText.text = score;
	}

	public void Reset()
	{
		RankText.text = "-";
		PlayerNameText.text = "-";
		ScoreText.text = "-";
	}
}
