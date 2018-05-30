namespace RedBlueGames.Tools.TextTyper
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
	public class DialogosTutorial12 : MonoBehaviour
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

		public string idioma;

		public void Start()
		{
			idioma = PlayerPrefs.GetString("idioma");

			sonido.GetComponent<AudioSource>().Play();
			ventana.SetActive(true);

			animacion.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "entrada", false);
			habla = 3;

			Hero.GetComponent<Hero>().pausado = true;
			Hero.GetComponent<Hero>().ready = false;

			this.testTextTyper.PrintCompleted.AddListener(this.HandlePrintCompleted); 
			this.testTextTyper.CharacterPrinted.AddListener(this.HandleCharacterPrinted);

			this.printNextButton.onClick.AddListener(this.HandlePrintNextClicked);
			this.printNoSkipButton.onClick.AddListener(this.HandlePrintNoSkipClicked);

			if(idioma == "ENGLISH")
			{
				dialogueLines.Enqueue("Well done soldier. Now you know all basics to be able to defend yourself and your men in the field.");
			}
			if(idioma == "SPANISH")
			{
				dialogueLines.Enqueue("Bien hecho soldado. ahora sabe todo lo básico para poder defenderse en el campo de batalla.");
			}
			if(idioma == "CHINESE")
			{
				dialogueLines.Enqueue("做得好的士兵。 现在你知道所有的基本知识，能够在野外为自己和你的男人辩护。");
			}
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
					Hero.GetComponent<Hero>().pausado = false;
					Hero.GetComponent<Hero>().ready = true;
					ventana.SetActive(false);
					Destroy(gameObject);
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


