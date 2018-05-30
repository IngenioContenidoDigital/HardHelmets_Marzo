using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidosOffline : MonoBehaviour {

	//DISPAROS
	public AudioClip pistola;
	public AudioClip escopeta;
	public AudioClip fusil;
	public AudioClip submetra;
	public AudioClip metra;
	public AudioClip panzer;
	public AudioClip mg;
	public AudioClip llamas;
	public AudioClip sniper;
	public bool fuego;

	//AUDIO PLAYER

	public AudioSource audio1;
	public AudioSource audio2;
	public AudioSource audio3;

	//VOLUMEN
	float efectos;

	void Start ()
	{
		efectos = PlayerPrefs.GetFloat("efects");
		gritar();
	}

	//DISPAROS ------------------
	public void gunshot ()
	{
		audio1.volume = efectos;
		audio1.clip = pistola;
		audio1.Play();
	}
	public void metrashot()
	{
		audio1.volume = efectos;
		audio1.clip = metra;
		audio1.Play();
	}
	public void ShotE ()//PANZER
	{
		audio1.volume = efectos;
		audio1.clip = panzer;
		audio1.Play();
	}
	public void shotshotgun()
	{
		audio1.volume = efectos;
		audio1.clip = escopeta;
		audio1.Play();
	}
	public void submetrashot()
	{
		audio1.volume = efectos;
		audio1.clip = submetra;
		audio1.Play();
	}
	public void fusilshot()
	{
		if(GetComponent<Hero>() != null)
		{
			if(GetComponent<Hero>().arma == "sniper")
			{
				print("SONIDO DISPARO SNIPER");
				audio1.volume = efectos;
				audio1.clip = sniper;
				audio1.Play();
			}else
			{
				print("SONIDO DISPARO FUSIL");
				audio1.volume = efectos;
				audio1.clip = fusil;
				audio1.Play();
			}
		}else
		{
			audio1.volume = efectos;
			audio1.clip = fusil;
			audio1.Play();
		}
	}
	public void mgshot()
	{
		audio1.volume = efectos;
		audio1.clip = mg;
		audio1.Play();
	}

	void Update()
	{
		if(fuego)
		{
			if(!audio1.isPlaying)
			{
				audio1.volume = efectos;
				audio1.clip = llamas;
				audio1.Play();
			}
		}else if(audio1.clip == llamas)
		{
			audio1.Stop();
		}

		if(destruir)
		{
			/*if(!temp.isPlaying)
			{
				Destroy(temp);
			}*/
			destruir = false;
		}
	}
	//AIRE
	void tiro()
	{
		if(GetComponent<Hero>().arma2)
		{
			audio1.volume = efectos;
			audio1.clip = pistola;
			audio1.Play();
		}else
		{
			if(GetComponent<Hero>().arma == "escopeta")
			{
				shotshotgun();
			}
			if(GetComponent<Hero>().arma == "fusil")
			{
				fusilshot();
			}
			if(GetComponent<Hero>().arma == "submetra")
			{
				submetrashot();
			}
			if(GetComponent<Hero>().arma == "metra")
			{
				metrashot();
			}
			if(GetComponent<Hero>().arma == "panzer")
			{
				ShotE();
			}
		}
	}

	//RECARGAS --------------------
	public AudioClip rfusil;
	public AudioClip rgun;
	public AudioClip rshotgun;
	public AudioClip rshotgun2;
	public AudioClip rsuelta;
	public AudioClip rmetra;
	public AudioClip rbusmetra;

	void fusilrecarga ()
	{
		audio2.volume = efectos;
		audio2.clip = rfusil;
		audio2.Play();
	}
	void suelta ()
	{
		audio2.volume = efectos;
		audio2.clip = rsuelta;
		audio2.Play();
	}
	void gunreload()
	{
		audio2.volume = efectos;
		audio2.clip = rgun;
		audio2.Play();
	}
	void lodshotgun()
	{
		audio2.volume = efectos;
		audio2.clip = rshotgun;
		audio2.Play();
	}
	void reshotgun()
	{
		audio2.volume = efectos;
		audio2.clip = rshotgun2;
		audio2.Play();
	}
	void metrareload()
	{
		if(GetComponent<Hero>() != null)
		{
			if(GetComponent<Hero>().arma == "metra")
			{
				audio1.volume = efectos;
				audio1.clip = rmetra;
				audio1.Play();
			}else
			{
				audio1.volume = efectos;
				audio1.clip = rbusmetra;
				audio1.Play();
			}
		}else
		{
			audio1.volume = efectos;
			audio1.clip = rmetra;
			audio1.Play();
		}
	}
	public AudioClip cuchillo1;
	public AudioClip cuchillo2;
	public AudioClip reloadmg;
	public AudioClip inmortero;
	public AudioClip outmortero;
	void cuchillo()
	{
		audio1.volume = efectos;
		audio1.clip = cuchillo1;
		audio1.Play();
	}
	void cuchillob()
	{
		audio1.volume = efectos;
		audio1.clip = cuchillo2;
		audio1.Play();
	}
	void mgreload()
	{
		audio2.volume = efectos;
		audio2.clip = reloadmg;
		audio2.Play();
	}
	void morteroin()
	{
		audio1.volume = efectos;
		audio1.clip = inmortero;
		audio1.Play();
	}
	void morteroout()
	{
		audio1.volume = efectos;
		audio1.clip = outmortero;
		audio1.Play();
	}
	//VOZ -----------------------
	public AudioClip[] dead;
	public AudioClip[] attack;
	public AudioClip[] damage;
	public AudioClip[] idle;
	public AudioClip[] muerto;
	public AudioClip[] xx;
	public AudioClip[] quemado;

	void insendio()
	{
		audio1.volume = efectos;
		audio1.clip = quemado[Random.Range(0,quemado.Length)];
		audio1.Play();
	}

	void Smuerto()
	{
		audio3.volume = efectos;
		audio3.clip = dead[Random.Range(0,dead.Length)];
		audio3.Play();
	}
	void Sattack()
	{
		audio3.volume = efectos;
		audio3.clip = attack[Random.Range(0,attack.Length)];
		audio3.Play();
	}
	void Sdamage()
	{
		audio3.volume = efectos;
		audio3.clip = damage[Random.Range(0,damage.Length)];
		audio3.Play();
	}
	void Sidle()
	{
		audio3.volume = efectos;
		audio3.clip = idle[Random.Range(0,idle.Length)];
		audio3.Play();
	}
	void Smuertob()
	{
		audio3.volume = efectos;
		audio3.clip = muerto[Random.Range(0,muerto.Length)];
		audio3.Play();
	}
	void Sxx()
	{
		audio3.volume = efectos;
		audio3.clip = xx[Random.Range(0,xx.Length)];
		audio3.Play();
	}
	public AudioClip[] grito;

	public void gritar ()
	{
		audio3.volume = efectos;
		audio3.clip = grito[Random.Range(0,grito.Length)];
		audio3.Play();
	}
	public AudioClip splat;

	public void cabeza ()
	{
		audio1.volume = efectos;
		audio1.clip = splat;
		audio1.Play();
	}

	public AudioClip sordo;
	int posi;
	AudioSource temp;
	bool destruir;
	public void bomba ()
	{
		if(posi == 1)
		{
			/*var Temporal = gameObject.AddComponent<AudioSource>();
			temp = Temporal;
			temp.volume = efectos;
			temp.clip = sordo;
			temp.Play();*/
			destruir = true;
		}
	}
	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "explo")
		{
			posi = Random.Range(1,3);
			bomba();
		}
	}
	public AudioClip falli;
	void fall()
	{
		audio1.volume = efectos;
		audio1.clip = falli;
		audio1.Play();
	}

	public AudioClip gunmini;
	void minigun()
	{
		audio1.volume = efectos;
		audio1.clip = gunmini;
		audio1.Play();
	} 
}
