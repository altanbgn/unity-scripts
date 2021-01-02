using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace CovertPath.Dialogues {
	public class DialogueCore : MonoBehaviour {
		#region Singleton
			public static DialogueCore instance;
			private void Awake()
			{
				if (instance != null)
				{
					Debug.LogWarning("More than one instance of dialogue manager found");
					return;
				}
				instance = this;
			}
		#endregion

		public GameObject dialogueObject;
		public TextMeshProUGUI dialogueText;
		private Queue<string> sentences;
		private bool oneTime;

		private void Start() {
			sentences = new Queue<string>();
		}

		public void StartDialogue(Dialogue dialogue, bool oneTimeDialogue) {
			dialogueObject.SetActive(true);
			sentences.Clear();
			foreach(string sentence in dialogue.sentences)
				sentences.Enqueue(sentence);
			DisplayNextSentence();
			oneTime = oneTimeDialogue;
		}

		public void DisplayNextSentence() {
			if (sentences.Count == 0) {
				DeleteDialogue();
				return;
			}
			string sentence = sentences.Dequeue();
			dialogueText.text = sentence;
			StopAllCoroutines();
			StartCoroutine(TypeSentence(sentence));
		}

		private IEnumerator TypeSentence (string sentence) {
			dialogueText.text = "";
			foreach(char letter in sentence.ToCharArray()) {
				dialogueText.text += letter;
				yield return null;
			}
		}

		public void EndDialogue() {
			dialogueObject.SetActive(false);
		}

		public void DeleteDialogue() {
			EndDialogue();
			if (oneTime)
				Destroy(this);
		}
	}
}
