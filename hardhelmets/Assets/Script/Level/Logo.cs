using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
//using UnityEngine.UI;

public class Logo : MonoBehaviour {

	public GameObject video;
	public UnityEngine.UI.Image negro;
	byte alp = 255;
	bool entrada = true;
	bool salida;

	// Use this for initialization
	void Start ()
	{
		//Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(SceneManager.GetActiveScene().name == "Logo" && !video.GetComponent<VideoPlayer>().isPlaying)
		{
			salida = true;
			//StartCoroutine(espera());
			if(salida)
			{
				alp += 5;
				negro.color = new Color32(0,0,0,alp);
				if(alp >= 255)
				{
					Application.LoadLevel("menu");
				}
			}
		}else
		{
			if(entrada)
			{
				alp -= 5;
				negro.color = new Color32(0,0,0,alp);
				if(alp <= 0)
				{
					entrada = false;
					StartCoroutine(espera2());
				}
			}
		
			if(salida)
			{
				alp += 5;
				negro.color = new Color32(0,0,0,alp);
				if(alp >= 255)
				{
					Application.LoadLevel("menu");
				}
			}
		}
	}

	IEnumerator espera()
	{
		yield return new WaitForSeconds(10);
		Application.LoadLevel("Mensaje");
	}

	IEnumerator espera2()
	{
		yield return new WaitForSeconds(10);
		salida = true;
	}
}
