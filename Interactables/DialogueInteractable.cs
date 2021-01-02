using UnityEngine;
using CovertPath.Interfaces;
using CovertPath.Dialogues;

namespace CovertPath.Interactables {
	public class DialogueInteractable : Interactable, IInteractable {
		public bool oneTime = false;
		public bool lookAtYou = false;
		public Dialogue dialogue;
		private DialogueCore _dialogueManager;
		private bool isTalking = false;

		private void Start() {
			_dialogueManager = DialogueCore.instance;
		}

		private void Update() {
			float distance = Vector3.Distance(transform.position, GameObject.FindWithTag("Player").transform.position);
			if (_dialogueManager == null)
				return;
			if (distance > 3f && isTalking == true) {
				_dialogueManager.EndDialogue();
				isTalking = false;
			}
		}

		public override void Interact() {
			base.Interact();
			if (lookAtYou)
				transform.LookAt(GameObject.FindWithTag("Player").transform.position);
			if (_dialogueManager == null)
				return;
			_dialogueManager.StartDialogue(dialogue, oneTime);
			isTalking = true;
		}
	}
}