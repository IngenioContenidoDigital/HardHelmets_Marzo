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
	public class DialogosCartas : MonoBehaviour
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

		public GameObject master;
		public GameObject tut;
		public GameObject baul;

		public GameObject baulregalo;

		public void Start()
		{
			idioma = PlayerPrefs.GetString("idioma");

			sonido.GetComponent<AudioSource>().Play();
			animacion.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "entrada", false);
			habla = 3;

			this.testTextTyper.PrintCompleted.AddListener(this.HandlePrintCompleted); 
			this.testTextTyper.CharacterPrinted.AddListener(this.HandleCharacterPrinted);

			this.printNextButton.onClick.AddListener(this.HandlePrintNextClicked);
			this.printNoSkipButton.onClick.AddListener(this.HandlePrintNoSkipClicked);

			if(idioma == "ENGLISH")
			{
				dialogueLines.Enqueue("Welcome Soldier.");
				dialogueLines.Enqueue("Enter the tutorial option in the menu, if you want to learn movements and game mechanics ");
				dialogueLines.Enqueue("I will give you three gift chests for new players, open them to unlock new cards");
				dialogueLines.Enqueue("Once you have opened the chests go to the option <color=#EC9B00FF>war bonds</color> to equip the cards that you have unlocked");
			}
			if(idioma == "SPANISH")
			{
				dialogueLines.Enqueue("Bienvenido Soldado.");
				dialogueLines.Enqueue("Ingrese a la opción tutorial en el menú, si quiere aprender movimientos y las mecánicas del juego");
				dialogueLines.Enqueue("Te obsequiare tres cofres de regalo para nuevos jugadores, ábrelos para desbloquear nuevas cartas");
				dialogueLines.Enqueue("Una vez hayas abierto los cofres ve a la opción <color=#EC9B00FF>war bonds</color> para equipar las cartas que hayas desbloqueado ");
			}
			if(idioma == "CHINESE")
			{
				dialogueLines.Enqueue("欢迎士兵。");
				dialogueLines.Enqueue("如果您想学习动作和游戏机制，请在菜单中输入教程选项");
				dialogueLines.Enqueue("我会给你三个新玩家的礼物箱，打开他们解锁新卡");
				dialogueLines.Enqueue("一旦你打开箱子，去选择战争债券来装备你已经解锁的卡片");
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
				if(dialogos == 1)
				{
					tut.SetActive(true);
				}
				if(dialogos == 2)
				{
					tut.SetActive(false);
					//baul.SetActive(true);
				}
				if(dialogos == 3)
				{
					baulregalo.SetActive(true);
					PlayerPrefs.SetInt("caja1", 3);
					PlayerPrefs.SetInt("regaloBaul", 1);
				}

				if(dialogos >= max)
				{
					animacion.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "entrada2", false);
					ventana.SetActive(false);
					master.GetComponent<Menu>().capitan = true;
					PlayerPrefs.SetInt("FirstTimeCartas",1);
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
