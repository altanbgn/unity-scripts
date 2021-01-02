using UnityEngine;
using UnityEngine.EventSystems;
using CovertPath.Core;
using CovertPath.Mechanics;
using CovertPath.Items;
using CovertPath.Interactables;

namespace CovertPath.Controller
{
	[RequireComponent(typeof(CharacterMovement))]
	[RequireComponent(typeof(PlayerInteract))]
	[RequireComponent(typeof(PlayerCombat))]
	[RequireComponent(typeof(PlayerAttributes))]
	public class PlayerController : MonoBehaviour
	{
		private CharacterMovement _movement;
		private PlayerInteract _interact;
		private PlayerCombat _combat;
		private PlayerAttributes _attributes;
		private GameCursor _gameCursor;
		// private PlayerSkills _skills;

		private void Start() {
			_movement = GetComponent<CharacterMovement>();
			_interact = GetComponent<PlayerInteract>();
			_combat = GetComponent<PlayerCombat>();
			_attributes = GetComponent<PlayerAttributes>();
			_gameCursor = GameObject.FindWithTag("GameSystem").GetComponent<GameCursor>();
			// _playerSkills = GetComponent<PlayerSkills>();
		}

		private void Update() {
			if (EventSystem.current.IsPointerOverGameObject())
				return;
			// Gets all hits with raycast
			RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
			if (_attributes.isDead)
				return;
			if (InteractWithCombat(hits))
				return;
			if (InteractWithOther(hits))
				return;
			if (InteractWithMovement(hits))
				return;
		}

		private bool InteractWithCombat(RaycastHit[] hits) {
			foreach(RaycastHit hit in hits) {
				if (hit.transform == null)
					continue;
				// Checks if the object has Enemy Attributes
				EnemyAttributes target = hit.transform.GetComponent<EnemyAttributes>();
				// If target not found, Skips this single iteration
				if (target == null)
					continue;
				// If target exists get the game object and test if it is attackable
				GameObject targetGameObject = target.gameObject;
				if (!_combat.IsAttackable(targetGameObject))
					continue;
				_gameCursor.SetCursorCombat();
				// If you press RMB, Attack starts
				if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))
					_combat.StartAttack(targetGameObject);
				return true;
			}
			return false;
		}

		// private bool InteractWithSkill(RaycastHit[] hits) {
		// 	foreach(RaycastHit hit in hits) {
		// 		if (hit.transform == null)
		// 			continue;
		// 		EnemyAttributes target = hit.transform.GetComponent<EnemyAttributes>();
		// 		if (target == null)
		// 			continue;
		// 		GameObject targetGameObject = target.gameObject;
		// 		_playerSkills.SetTarget(targetGameObject);
		// 		return true;
		// 	}
		// 	return false;
		// }

		private bool InteractWithOther(RaycastHit[] hits) {
			foreach (RaycastHit hit in hits) {
				if (hit.transform == null)
					continue;
				Interactable target = hit.transform.gameObject.GetComponent<Interactable>();
				if (target == null)
					continue;
				GameObject targetGameObject = target.gameObject;
				if (!_interact.IsInteractable(targetGameObject))
					continue;
				_gameCursor.SetCursorInteract();
				if (Input.GetMouseButtonDown(1) || Input.GetMouseButton(1) || Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
					_interact.StartInteract(targetGameObject);
				return true;
			}
			return false;
		}

		private bool InteractWithMovement(RaycastHit[] hits) {
			foreach (RaycastHit hit in hits) {
				if (hit.transform == null)
					continue;
				// Checks if hit object has layer "Ground". If not skips the iteration
				if (hit.transform.gameObject.layer != LayerMask.NameToLayer("Ground"))
					continue;
				_gameCursor.SetCursorDefault();
				// Moves the location 
				if (Input.GetMouseButtonDown(1) || Input.GetMouseButton(1) || Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
					_movement.StartMovement(hit.point);
				return true;
			}
			return false;
		}

		private static Ray GetMouseRay() {
			return Camera.main.ScreenPointToRay(Input.mousePosition);
		}
	}
}