﻿namespace RedBlueGames.Tools.TextTyper
{
	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using RedBlueGames.Tools.TextTyper;
	using UnityEngine.UI;
	using Spine.Unity;

	/// <summary>
	/// Class that tests TextTyper and shows how to interface with it.
	/// </summary>
	public class DialogosTutorial3 : MonoBehaviour
	{
		public GameObject animacion;
		public int habla;

		[SerializeField]
		private AudioClip printSoundEffect;

		[Header("UI References")]

		[SerializeField]
		private Text text;

		[SerializeField]
		private Button printNextButton;

		[SerializeField]
		private Button printNoSkipButton;

		private Queue<string> dialogueLines = new Queue<string>();

		[SerializeField]
		[Tooltip("The text typer element to test typing with")]
		private TextTyper testTextTyper;

		public void Start()
		{
			sonido.GetComponent<AudioSource>().Play();
			ventana.SetActive(true);

			animacion.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "entrada", false);
			habla = 3;

			this.testTextTyper.PrintCompleted.AddListener(this.HandlePrintCompleted); 
			this.testTextTyper.CharacterPrinted.AddListener(this.HandleCharacterPrinted);

			this.printNextButton.onClick.AddListener(this.HandlePrintNextClicked);
			this.printNoSkipButton.onClick.AddListener(this.HandlePrintNoSkipClicked);

			dialogueLines.Enqueue("Muy bien, ahora vamos a practicar la puntería, muévase hacia arriba o hacia abajo hasta que el circulo de mira aparezca sobre el objetivo.");
			/*dialogueLines.Enqueue("Hello! My name is... <delay=0.5>CAPITAN MOSTACHO</delay>. Got it, bub?");
            dialogueLines.Enqueue("You can <b>use</b> <i>uGUI</i> <size=40>text</size> <size=20>tag</size> and <color=#ff0000ff>color</color> tag <color=#00ff00ff>like this</color>.");
            dialogueLines.Enqueue("bold <b>text</b> test <b>bold</b> text <b>test</b>");
            dialogueLines.Enqueue("You can <size=40>size 40</size> and <size=20>size 20</size>");
            dialogueLines.Enqueue("You can <color=#ff0000ff>color</color> tag <color=#00ff00ff>like this</color>.");*/
			ShowScript();
		}

		public void Update()
		{
			if(habla == 1)
			{
				animacion.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "hablarcorto", false);
			}
			if(habla == 2)
			{
				animacion.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "hablarmedio", false);
			}
			if(habla == 3)
			{
				animacion.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "hablarlargo", false);
			}

			if(animacion.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0).Animation.Name == "hablarcorto" || animacion.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0).Animation.Name == "hablarmedio" || animacion.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0).Animation.Name == "hablarlargo")
			{
				habla = 0;
				StartCoroutine(EspHabla());
			}
			if(Input.GetButtonDown("Submit") || Input.GetButtonDown("Jump"))
			{
				if(dialogos >= max)
				{
					animacion.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "entrada2", false);
					enemigo.SetActive(true);
					StartCoroutine(enemi2());
					StartCoroutine(enemi3());
				}else if(animacion.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0).Animation.Name == "idle")
				{
					habla = 3;
				}

				letras.SetActive(false);
				this.printNextButton.onClick.Invoke();
			}
			/*if (Input.GetKeyDown(KeyCode.Space))
            {
                var tag = RichTextTag.ParseNext("blah<color=red>boo</color");
                LogTag(tag);
                tag = RichTextTag.ParseNext("<color=blue>blue</color");
                LogTag(tag);
                tag = RichTextTag.ParseNext("No tag in here");
                LogTag(tag);
                tag = RichTextTag.ParseNext("No <color=blueblue</color tag here either");
                LogTag(tag);
                tag = RichTextTag.ParseNext("This tag is a closing tag </bold>");
                LogTag(tag);
            }*/
		}
		IEnumerator EspHabla()
		{
			yield return new WaitForSpineAnimationComplete(animacion.GetComponent<SkeletonGraphic>().AnimationState.GetCurrent(0));
			animacion.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "idle", false);
		}
		IEnumerator enemi2()
		{
			yield return new WaitForSeconds(0.3f);
			enemigo2.SetActive(true);
		}
		IEnumerator enemi3()
		{
			yield return new WaitForSeconds(0.3f);
			enemigo3.SetActive(true);
			Hero.GetComponent<Hero>().ready = true;
			ventana.SetActive(false);
			Destroy(gameObject);
		}

		private void HandlePrintNextClicked()
		{
			if (this.testTextTyper.IsSkippable() && this.testTextTyper.IsTyping)
			{
				this.testTextTyper.Skip();
			}
			else
			{
				ShowScript();
			}
		}

		private void HandlePrintNoSkipClicked()
		{
			ShowScript();
		}

		private void ShowScript()
		{
			if (dialogueLines.Count <= 0)
			{
				return;
			}

			this.testTextTyper.TypeText(dialogueLines.Dequeue());
		}

		private void LogTag(RichTextTag tag)
		{
			if (tag != null)
			{
				Debug.Log("Tag: " + tag.ToString());
			}
		}

		private void HandleCharacterPrinted(string printedCharacter)
		{
			// Do not play a sound for whitespace
			if (printedCharacter == " " || printedCharacter == "\n")
			{
				return;
			}

			var audioSource = this.GetComponent<AudioSource>();
			if (audioSource == null)
			{
				audioSource = this.gameObject.AddComponent<AudioSource>();
			}

			audioSource.clip = this.printSoundEffect;
			audioSource.Play();
		}
		public GameObject Hero;
		public GameObject enemigo;
		public GameObject enemigo2;
		public GameObject enemigo3;
		public GameObject ventana;
		public GameObject letras;
		public GameObject sonido;
		public int dialogos;
		public int max;
		private void HandlePrintCompleted()
		{
			Debug.Log("TypeText Complete");
			dialogos += 1;
			letras.SetActive(true);
			animacion.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "idle", false);
		}
	}
}
