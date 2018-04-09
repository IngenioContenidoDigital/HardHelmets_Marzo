using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loading2 : MonoBehaviour {

	//public UnityEngine.UI.Image loadingBar;

	private AsyncOperation Async = null;

	public static string nombre;

	public string name1;
	public Text name1T;
	public string name2;
	public Text name2T;

	// Use this for initialization
	void Start ()
	{
		StartCoroutine(espera());
	}

	void Update ()
	{
		name1 = PlayerPrefs.GetString("SteamName");
		name1T.text = name1;
		name2 = PlayerPrefs.GetString("nameCommunity");
		name2T.text = name2;
	}

	public void ClickAsync (string level)
	{
		StartCoroutine(LoadLevelWithBar(level));
	}

	IEnumerator LoadLevelWithBar(string level)
	{
		Async = SceneManager.LoadSceneAsync(level);
		//Async.allowSceneActivation = false;

		while (!Async.isDone)
		{
			if(Async.progress < 0.9f)
			{
				//loadingBar.fillAmount = Async.progress/0.9f;

			}else
			{
				//loadingBar.fillAmount = Async.progress/0.9f;
				Async.allowSceneActivation = true;
			}

			yield return null;
		}
		yield return Async;
	}

	public void Iniciar()
	{
		ClickAsync(nombre);
	}

	IEnumerator espera()
	{
		yield return new WaitForSeconds(1.5f);
		Iniciar();
	}
}
