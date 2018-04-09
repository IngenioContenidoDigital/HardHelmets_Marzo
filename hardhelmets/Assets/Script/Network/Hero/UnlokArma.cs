using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class UnlokArma : MonoBehaviour {

	public GameObject Player;

	public GameObject animacion;

	public bool fusil;
	bool FDes;
	public bool escopeta;
	bool EDes;
	public bool submetra;
	bool SDes;
	public bool metra;
	bool MDes;
	public bool sniper;
	bool SNDes;
	public bool llamas;
	bool LDes;
	public bool panzer;
	bool PDes;
	public bool granada;
	bool GDes;

	public UnityEngine.UI.Text balas;
	public UnityEngine.UI.Text granadas;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(fusil && !FDes)
		{
			sonar();
			animacion.GetComponent<skinCarta>().skinsToCombine[0] = "fusil";
			animacion.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "animation", false);
			FDes = true;
		}
		if(escopeta && !EDes)
		{
			sonar();
			animacion.GetComponent<skinCarta>().skinsToCombine[0] = "escopeta";
			animacion.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "animation", false);
			EDes = true;
		}
		if(submetra && !SDes)
		{
			sonar();
			animacion.GetComponent<skinCarta>().skinsToCombine[0] = "submetra";
			animacion.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "animation", false);
			SDes = true;
		}
		if(metra && !MDes)
		{
			sonar();
			animacion.GetComponent<skinCarta>().skinsToCombine[0] = "metra";
			animacion.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "animation", false);
			MDes = true;
		}
		if(sniper && !SNDes)
		{
			sonar();
			animacion.GetComponent<skinCarta>().skinsToCombine[0] = "sniper";
			animacion.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "animation", false);
			SNDes = true;
		}
		if(llamas && !LDes)
		{
			sonar();
			animacion.GetComponent<skinCarta>().skinsToCombine[0] = "lansallamas";
			animacion.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "animation", false);
			LDes = true;
		}
		if(panzer && !PDes)
		{
			sonar();
			animacion.GetComponent<skinCarta>().skinsToCombine[0] = "pistola";
			animacion.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "animation", false);
			PDes = true;
		}
		if(granada && !GDes)
		{
			sonar();
			animacion.GetComponent<skinCarta>().skinsToCombine[0] = "granada";
			animacion.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "animation", false);
			GDes = true;
		}
		if(Player.GetComponent<Hero>() != null)
		{
			if(Player.GetComponent<Hero>().arma2)
			{
				balas.text = "∞"+"/"+Player.GetComponent<Hero>().balas.ToString();
			}else
			{
				balas.text = ""+Player.GetComponent<Hero>().balasTotales.ToString()+"/"+Player.GetComponent<Hero>().balas.ToString();
			}

			granadas.text = ""+Player.GetComponent<Hero>().granadas.ToString();
		}else
		{
			if(Player.GetComponent<HeroNetwork>().arma2)
			{
				balas.text = "∞"+"/"+Player.GetComponent<HeroNetwork>().balas.ToString();
			}else
			{
				balas.text = ""+Player.GetComponent<HeroNetwork>().balasTotales.ToString()+"/"+Player.GetComponent<HeroNetwork>().balas.ToString();
			}

			granadas.text = ""+Player.GetComponent<HeroNetwork>().granadas.ToString();
		}
	}
	public AudioClip sonido;
	public AudioSource audio1;

	public void sonar ()
	{
		audio1.volume = PlayerPrefs.GetFloat("efects");
		audio1.clip = sonido;
		audio1.Play();
	}
}
