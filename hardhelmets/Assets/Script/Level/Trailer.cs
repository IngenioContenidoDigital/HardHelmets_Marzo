using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Trailer : MonoBehaviour {

	public GameObject video;

	public GameObject mensaje;

	public bool mostrar;

	public bool reproduciendo;

	public bool esconder;

	public bool presionado;
	public bool listo;

	public bool salir;

	// Use this for initialization
	void Start ()
	{
		mostrar = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(mostrar && video.GetComponent<VideoPlayer>().isPlaying)
		{
			video.GetComponent<VideoPlayer>().targetCameraAlpha += 0.003f;
			video.GetComponent<AudioSource>().volume += 0.003f;

			if(video.GetComponent<VideoPlayer>().targetCameraAlpha >= 1)
			{
				reproduciendo = true;
				mostrar = false;
			}
		}

		if(reproduciendo && !video.GetComponent<VideoPlayer>().isPlaying)
		{
			esconder = true;
		}

		if(esconder)
		{
			mostrar = false;
			mensaje.SetActive(false);

			video.GetComponent<VideoPlayer>().targetCameraAlpha -= 0.004f;
			video.GetComponent<AudioSource>().volume -= 0.004f;

			if(video.GetComponent<VideoPlayer>().targetCameraAlpha <= 0)
			{
				salir = true;
				esconder = false;
			}
		}

		if(salir)
		{
			Application.LoadLevel("Load");
			loading.nombre = "menu";
			salir = false;
		}

		if(Input.anyKey && !presionado)
		{
			mensaje.SetActive(true);
			listo = true;
			presionado = true;
		}

		if(Input.GetButtonDown("Cancel") && listo)
		{
			esconder = true;
		}
	}
}
