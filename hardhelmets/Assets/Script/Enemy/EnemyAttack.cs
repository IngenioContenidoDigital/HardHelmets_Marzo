using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

	public int Malo;

	Transform Base;
	public Animator animator;
	string _currentDirection = "right";

	//GROUND CHECHER
	public Transform groundCheck;
	float groundRadius = 0.3f;
	public LayerMask whatIsGround;
	public bool grounded = false;

	public bool vivo = true;
	public int salud = 100;

	//OBJETOS DE DISPARO
	public GameObject bulletPref;
	public GameObject bulletPrefFusil;
	public GameObject bulletPrefEscopeta;
	public GameObject bulletPrefSubmetra;
	public GameObject bulletPrefMetra;

	public Transform bulletSpawn;
	public GameObject casquilloPref;
	public Transform casquilloSpawn;
	public GameObject granadePref;
	public GameObject luz;


	//FUNCIONES
	bool actuando;

	bool caminar;
	bool shoot;
	int disparos;

	// Use this for initialization
	void Start ()
	{
		Base = GameObject.FindWithTag ("base").transform;
		_currentDirection = "right";
	}
	
	// Update is called once per frame
	void Update ()
	{
		grounded = Physics.CheckSphere(groundCheck.position, groundRadius, whatIsGround);
		animator.SetBool("grounded", grounded);

		if(vivo)
		{
			if(grounded)
			{
				//MIRA AL JUGADOR
				Vector3 v3Dir = Base.position - transform.position;
				float angle = Mathf.Atan2(0, v3Dir.x) * Mathf.Rad2Deg;

				if(angle == 180)
				{
					_currentDirection = "left";
					gameObject.transform.localScale = new Vector3(-1,1,1);
				}else
				{
					_currentDirection = "right";
					gameObject.transform.localScale = new Vector3(1,1,1);
				}
			}
			//SI ESTA MUY LEJOS
			if(Mathf.Abs((transform.position - Base.position).x) >= 30 && !caminar)
			{
				//caminar = true;
				animator.SetBool("walk",false);
			}
			if(Mathf.Abs((transform.position - Base.position).x) <= 29.9f && !actuando)
			{
				shoot = true;
			}
			//CAMINAR
			if(caminar)
			{
				animator.SetBool("walk",true);
				GetComponent<Rigidbody>().velocity = (Vector2.left * 8);
			}
			//DISPARO
			if(shoot && !animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
			{
				shoot = false;

				caminar = false;
				animator.SetBool("walk",false);

				if(disparos >= 2)
				{
					disparos = 0;
					animator.SetInteger("recarga", Malo);
				}else
				{
					disparos += 1;
					animator.SetInteger("disparo", Malo);
				}
			}
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("FusilShot") || animator.GetCurrentAnimatorStateInfo(0).IsName("ShotgunShot") || animator.GetCurrentAnimatorStateInfo(0).IsName("SubmetraShot") || animator.GetCurrentAnimatorStateInfo(0).IsName("MetraShot") || animator.GetCurrentAnimatorStateInfo(0).IsName("Granada") ||
			animator.GetCurrentAnimatorStateInfo(0).IsName("FusilRecarga") || animator.GetCurrentAnimatorStateInfo(0).IsName("GunRecarga") || animator.GetCurrentAnimatorStateInfo(0).IsName("ShotgunRecarga") || animator.GetCurrentAnimatorStateInfo(0).IsName("RecargaMetraySub"))
		{
			actuando = true;
		}else
		{
			actuando = false;
		}
	}
	//APAGA LA LUZ
	IEnumerator apaga ()
	{
		yield return new WaitForSeconds(0.1f);
		luz.SetActive(false);
	}
	//EVENTOS SPINE
	void ShotA ()//FUSIL
	{
		luz.SetActive(true);
		StartCoroutine(apaga());
		var bullet = (GameObject)Instantiate(bulletPrefFusil, bulletSpawn.position, bulletSpawn.rotation); 
		if(_currentDirection == "right")
		{
			bullet.GetComponent<Rigidbody>().velocity = bullet.transform.right * 10;
		}else
		{
			bullet.GetComponent<Rigidbody>().velocity = bullet.transform.right * -10;
		}
		Destroy(bullet, 5.0f);
		//CASQUILLOS
		var casquillo = (GameObject)Instantiate(casquilloPref, casquilloSpawn.position, casquilloSpawn.rotation); 
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.right * Random.Range(-10,-50);//-50;
		casquillo.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(-0.0f,-0.2f));
		casquillo.GetComponent<Rigidbody>().velocity = casquillo.transform.up * Random.Range(-1,-6);//-5;

	}
}
