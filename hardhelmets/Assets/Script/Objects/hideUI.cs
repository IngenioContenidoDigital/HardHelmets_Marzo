using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideUI : MonoBehaviour {

	private string[] hideCode;
	private int index;

	private string[] showCode;
	private int index2;

	public GameObject arriba;
	public GameObject abajo;

	bool esconder;
	bool mostrar;

	void Start()
	{
		hideCode = new string[] { "h", "i", "d", "e", "q" };
		index = 0;

		showCode = new string[] { "s", "h", "o", "w", "q" };
		index2 = 0;  
	}

	void Update()
	{
		if(arriba == null)
		{
			arriba = GameObject.Find("Arriba");
		}else if (Input.anyKeyDown)
		{
			// Check if the next key in the code is pressed
			if (Input.GetKeyDown(hideCode[index]))
			{
				index ++;
			}else
			{
				index = 0;    
			}

			if (Input.GetKeyDown(showCode[index2]))
			{
				index2 ++;
			}else
			{
				index2 = 0;    
			}
		}

		if (index == hideCode.Length && !esconder)
		{
			arriba.GetComponent<Animator>().SetInteger("mover",1);
			abajo.GetComponent<Animator>().SetInteger("mover",1);

			index = 0;

			mostrar = false;
			esconder = true;
		}

		if (index2 == showCode.Length && !mostrar)
		{
			arriba.GetComponent<Animator>().SetInteger("mover",2);
			abajo.GetComponent<Animator>().SetInteger("mover",2);

			index2 = 0;

			esconder = false;
			mostrar = true;
		}
	}
}
