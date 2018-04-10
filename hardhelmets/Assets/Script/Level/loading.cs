using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Spine.Unity;

public class loading : MonoBehaviour {

	//public UnityEngine.UI.Image loadingBar;

	private AsyncOperation Async = null;

	public static string nombre;

	//OPCIONES DE ANIMACION
	public string[] anim = {"walk","idle","Agachado"};
	public string inicio;

	public string[] soldado = {"primero","segundo"};
	public string skin;

	public GameObject personaje;

	public bool fin;

	//TEXTOS
	public string[] Tips = {"Use mortars and explotions to shoot down heavy enemies like tank and turrets","Use action button over defender card to orden a new position","Use moves button to make especial moves","Use ricght click to see laser gun","If shoot crouch yo have less probabilities to been shooted","Increase your rank to have more healt and abilities"};
	public UnityEngine.UI.Text Tip;

	bool cambio;

	// Use this for initialization
	void Start ()
	{
		StartCoroutine(espera());
		inicio = anim[Random.Range(0,anim.Length)];

		personaje.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, inicio, true);

		skin = soldado[Random.Range(0,soldado.Length)];
		personaje.GetComponent<skinCarta>().skinsToCombine[0] = skin;

		Tip.text = Tips[Random.Range(0,Tips.Length)];
		StartCoroutine(Change());
	}

	void Update ()
	{
		if(Input.GetButtonDown("DISPARO 2") || Input.GetAxis("DISPARO") != 0)
		{
			if(inicio == "walk")
			{
				personaje.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "ShotgunShotWalk", true);
			}else if(inicio == "idle")
			{
				personaje.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "Descanso", true);
			}else if(inicio == "Agachado")
			{
				personaje.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "ShotgunRecargaAgachado", true);
			}
			StartCoroutine(momento());
		}
		if(fin)
		{
			personaje.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, inicio, true);
			fin = false;
		}
		if(cambio)
		{
			Tip.text = Tips[Random.Range(0,Tips.Length)];
			StartCoroutine(Change());
			cambio = false;
		}
		/*puntos.fillAmount += 0.02f;
		if(puntos.fillAmount >= 1)
		{
			puntos.fillAmount = 0;
		}*/
	}

	IEnumerator momento()
	{
		yield return new WaitForSpineAnimationComplete(personaje.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0));
		fin = true;
	}
	IEnumerator Change()
	{
		yield return new WaitForSeconds(5);
		cambio = true;
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
