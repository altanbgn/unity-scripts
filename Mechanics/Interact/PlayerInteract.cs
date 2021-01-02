using UnityEngine;
using CovertPath.Core;
using CovertPath.Interfaces;
using CovertPath.Interactables;

namespace CovertPath.Mechanics {
	public class PlayerInteract : MonoBehaviour, IAction {
		private Interactable _target;
		private CharacterMovement _movement;
		private ActionScheduler _scheduler;
		private PlayerAttributes _playerAttributes;

		private void Start() {
			_playerAttributes = GetComponent<PlayerAttributes>();
			_movement = GetComponent<CharacterMovement>();
			_scheduler = GetComponent<ActionScheduler>();
		}

		private void Update() {
			if (_target == null)
				return;
			if (InInteractRange()) {
				transform.LookAt(_target.transform.position);
				_movement.Cancel();
				_target.Interact();
				_target = null;
			} else {
				_movement.MoveTo(_target.transform.position);
			}
		}

		private bool InInteractRange() {
			return Vector3.Distance(transform.position, _target.transform.position) < _playerAttributes.attackRange.GetValue();
		}

		public bool IsInteractable(GameObject target) {
			if (target == null)
				return false;
			return target.GetComponent<Interactable>() != null;
		}

		public void StartInteract(GameObject target) {
			if (target.GetComponent<Interactable>() == null)
				return;
			_scheduler.StartAction(this);
			_target = target.GetComponent<Interactable>();
		}

		public void Cancel() {
			_target = null;
			_movement.Cancel();
		}
	}
}