using UnityEngine;
using UnityEngine.AI;
using CovertPath.Core;
using CovertPath.Interfaces;
using CovertPath.Mechanics;

namespace CovertPath.Mechanics {
    [RequireComponent(typeof(CharacterMovement))]
    [RequireComponent(typeof(EnemyAttributes))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(ActionScheduler))]    
    public class EnemyCombat : MonoBehaviour, IAction, ICombat {
        private float _attackCooldown = Mathf.Infinity;
        private CharacterMovement _movement;
        private EnemyAttributes _enemyAttributes;
        private Animator _animator;
        private ActionScheduler _scheduler;
        private PlayerAttributes _target;
        public bool isBoss;
		public bool isFriendly;

        private void Start() {
            _movement = GetComponent<CharacterMovement>();
            _enemyAttributes = GetComponent<EnemyAttributes>();
            _animator = GetComponent<Animator>();
            _scheduler = GetComponent<ActionScheduler>();
        }

        private void Update() {
            _attackCooldown += Time.deltaTime;
			// If target is not found, Abort this function.
			if (_target == null)
                return;
			// If target is dead, Abort this function.
			if (_target.isDead)
                return;
			// Checks if target is not in range
			if (InAttackRange()) {
				// Stops moving
                _movement.Cancel();
				// Triggers attack to target
				StartAttackAnimation();
			} else {
				// If not move closer to target
				_movement.StartMovement(_target.transform.position);
			}
        }

        private bool InAttackRange() {
            return Vector3.Distance(transform.position, _target.transform.position) < _enemyAttributes.attackRange.GetValue();
        }

        private void StartAttackAnimation() {
            if (_attackCooldown > _enemyAttributes.attackSpeed.GetValue() && _target != null) {
                _animator.ResetTrigger("stopAttack");                    // Stops "stopAttack" animation trigger
                transform.LookAt(_target.transform.position);            // Look to target
                _animator.SetTrigger("attack");                          // Triggers attack animation
                _attackCooldown = 0;                                     // Resets the attack cooldown
            }
        }

        // Trigger in animation by the time when target is hit by the weapon
        private void AnimationEventPhysicalAttack() {
            if (_target == null || !InAttackRange())                                         // If target not found, Abort the function
                return;
            _target.TakePhysicalDamage(_enemyAttributes.attackDamage.GetValue()); // Target takes damage from enemy
        }

        private void AnimationEventMagicAttack() {
            if (_target == null || !InAttackRange())
                return;
            _target.TakeMagicDamage(_enemyAttributes.magicDamage.GetValue());
        }

        // Gives new target to enemy combat
        public void StartAttack(GameObject combatTarget) {
            if (isFriendly == true)
                return;
            // Starts Combat Action (Activating Combat Mode) - You get the refernce :)
            _scheduler.StartAction(this);
            // Come on you know this line
            _target = combatTarget.GetComponent<PlayerAttributes>();
        }

        // Checks if target is attackable. returns bool value
        public bool IsAttackable(GameObject combatTarget) {
            if (combatTarget == null)                                   // If target not found, returns false. Simple af
                return false;
            // Get EnemyAttribute component of the target (Long Line AF) \/
            PlayerAttributes targetAttributes = combatTarget.GetComponent<PlayerAttributes>();
            return targetAttributes != null && !targetAttributes.isDead;// As long as target has enemyAttribute and not dead. GTG
        }

        public void ClearTarget() {
            _target = null;
        }

        // Cancels everything connected with attack.
        public void Cancel() {
            _target = null;
            _animator.ResetTrigger("attack");
            _animator.SetTrigger("stopAttack");
            _movement.Cancel();
        }
    }
}