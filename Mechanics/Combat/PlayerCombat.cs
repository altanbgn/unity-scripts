using UnityEngine;
using CovertPath.Core;
using CovertPath.Interfaces;
using CovertPath.Mechanics;

namespace CovertPath.Mechanics {
	[RequireComponent(typeof(CharacterMovement))]
	[RequireComponent(typeof(PlayerAttributes))]
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(ActionScheduler))]
	public class PlayerCombat : MonoBehaviour, IAction, ICombat {
		public EnemyAttributes target;
		public AudioSource attackAudio;
		private float _cooldownCounter = Mathf.Infinity;
		private CharacterMovement _movement;
		private PlayerAttributes _playerAttributes;
		private Animator _animator;
		private ActionScheduler _scheduler;

		private void Start() {
			_playerAttributes = GetComponent<PlayerAttributes>();
			_movement = GetComponent<CharacterMovement>();
			_animator = GetComponent<Animator>();
			_scheduler = GetComponent<ActionScheduler>();
		}

		private void Update() {
			if (_cooldownCounter < _playerAttributes.attackSpeed.GetValue())
				_cooldownCounter += Time.deltaTime;
			// If target is not found, Abort this function.
			if (target == null)
				return;
			// If target is dead, Abort this function.
			if (target.isDead)
				return;
			// Checks if target is not in range
			if (InAttackRange()) {
				// Stops moving
				_movement.Cancel();
				// Look to target
				transform.LookAt(target.transform.position);
				// Triggers attack to target
				StartAttackAnimation();
			} else {
				// If not move closer to target
				_movement.MoveTo(target.transform.position);
			}
		}

		private void AttackAnimation(int type) {
			switch(type) {
				case 1: {
					_animator.SetTrigger("attack1");
					return;
				}
				case 2: {
					_animator.SetTrigger("attack2");
					return;
				}
				default: return;
			}
		}

		private bool InAttackRange() {
			if (target == null)
				return false;
			return Vector3.Distance(transform.position, target.transform.position) < _playerAttributes.attackRange.GetValue();
		}

		private void StartAttackAnimation() {
			if (_cooldownCounter > _playerAttributes.attackSpeed.GetValue() && target != null) {
				// Resets the attack cooldown
				_cooldownCounter = 0;
				// Triggers attack animation
				AttackAnimation(Random.Range(1, 3));
			}
		}

		// Trigger in animation by the time when target is hit by the blade
		private void AnimationEventPhysicalAttack() {
			// Play attack sound
			if (attackAudio != null)
				attackAudio.Play();
			// If target not found, Abort the function
			if (target == null || !InAttackRange())
				return;
			// Target takes damage from player
			target.TakePhysicalDamage(_playerAttributes.attackDamage.GetValue());
		}

		private void AnimationEventMagicAttack() {
			// If target not found, Abort the function
			if (target == null)
				return;
		}

		// Gives new target to player combat
		public void StartAttack(GameObject combatTarget) {
			// Starts Combat Action (Activating Combat Mode) - You get the refernce :)
			_scheduler.StartAction(this);
			// Come on you know this line
			target = combatTarget.GetComponent<EnemyAttributes>();
		}

		// Checks if target is attackable. returns bool value
		public bool IsAttackable(GameObject combatTarget) {
			// If target not found, returns false. Simple af
			if (combatTarget == null)
				return false;
			// Get EnemyAttribute component of the target
			EnemyAttributes targetAttributes = combatTarget.GetComponent<EnemyAttributes>();
			// As long as target has enemyAttribute and not dead.
			return targetAttributes != null && !targetAttributes.isDead;
		}

		public void ClearTarget() {
			target = null;
		}

		// Cancels everything connected with attack.
		public void Cancel() {
			target = null;
			_animator.ResetTrigger("attack1");
			_animator.ResetTrigger("attack2");
			_animator.SetTrigger("stopAttack");
			_movement.Cancel();
		}
	}
}