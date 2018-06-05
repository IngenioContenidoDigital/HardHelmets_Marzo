using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voltearFuego : MonoBehaviour {

	public GameObject Player;

	public GameObject hijo1;
	public GameObject hijo2;
	public GameObject hijo3;
	public GameObject hijo4;
	public GameObject hijo5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Player.transform.localScale.x == 1)//(Player.GetComponent<Hero>()._currentDirection == "right")
		{
			transform.localScale = new Vector3(1,1,1);
			hijo1.transform.localScale = new Vector3(1,1,1);
			hijo2.transform.localScale = new Vector3(1,1,1);
			hijo3.transform.localScale = new Vector3(1,1,1);
			hijo4.transform.localScale = new Vector3(1,1,1);
			hijo5.transform.localScale = new Vector3(1,1,1);
		}else
		{
			transform.localScale = new Vector3(-1,1,1);
			hijo1.transform.localScale = new Vector3(-1,1,1);
			hijo2.transform.localScale = new Vector3(-1,1,1);
			hijo3.transform.localScale = new Vector3(-1,1,1);
			hijo4.transform.localScale = new Vector3(-1,1,1);
			hijo5.transform.localScale = new Vector3(-1,1,1);
		}
		/*if(Player.GetComponent<HeroNetwork>())
		{
			if(Player.GetComponent<HeroNetwork>()._currentDirection == "right")
			{
				transform.localScale = new Vector3(1,1,1);
				hijo1.transform.localScale = new Vector3(1,1,1);
				hijo2.transform.localScale = new Vector3(1,1,1);
				hijo3.transform.localScale = new Vector3(1,1,1);
				hijo4.transform.localScale = new Vector3(1,1,1);
				hijo5.transform.localScale = new Vector3(1,1,1);
			}else
			{
				transform.localScale = new Vector3(-1,1,1);
				hijo1.transform.localScale = new Vector3(-1,1,1);
				hijo2.transform.localScale = new Vector3(-1,1,1);
				hijo3.transform.localScale = new Vector3(-1,1,1);
				hijo4.transform.localScale = new Vector3(-1,1,1);
				hijo5.transform.localScale = new Vector3(-1,1,1);
			}
		}
		//GRANDULON
		if(Player.GetComponent<AIVikingo>())
		{
			if(Player.GetComponent<AIVikingo>()._currentDirection == "right")
			{
				transform.localScale = new Vector3(1,1,1);
				hijo1.transform.localScale = new Vector3(1,1,1);
				hijo2.transform.localScale = new Vector3(1,1,1);
				hijo3.transform.localScale = new Vector3(1,1,1);
				hijo4.transform.localScale = new Vector3(1,1,1);
				hijo5.transform.localScale = new Vector3(1,1,1);
			}else
			{
				transform.localScale = new Vector3(-1,1,1);
				hijo1.transform.localScale = new Vector3(-1,1,1);
				hijo2.transform.localScale = new Vector3(-1,1,1);
				hijo3.transform.localScale = new Vector3(-1,1,1);
				hijo4.transform.localScale = new Vector3(-1,1,1);
				hijo5.transform.localScale = new Vector3(-1,1,1);
			}
		}*/
	}
}
