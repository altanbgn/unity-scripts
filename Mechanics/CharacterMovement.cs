using UnityEngine;
using UnityEngine.AI;
using CovertPath.Core;
using CovertPath.Interfaces;

namespace CovertPath.Mechanics {
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(ActionScheduler))]
    [RequireComponent(typeof(Animator))]
    public class CharacterMovement : MonoBehaviour, IAction {
        private NavMeshAgent _agent;
        private ActionScheduler _scheduler;
        private Animator _animator;
        public AudioSource footstepAudio;

        private void Start() {
            _agent = GetComponent<NavMeshAgent>();
            _agent.enabled = true;
            _scheduler = GetComponent<ActionScheduler>();
            _animator = GetComponent<Animator>();
        }

        private void Update() {
            if (_agent.enabled == false)
                return;
			// if entity is dead. Skip
			if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Death"))
				return;
			// if player tries to move while using channeling animation, Cancels movement
			if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("ChannelSkill")) {
				_agent.isStopped = true;
				return;
			}
			// If enemy is not playing movement animation. Cancels movement
			if (!_animator.GetCurrentAnimatorStateInfo(0).IsTag("Movement") && !CompareTag("Player")) {
				_agent.isStopped = true;
				return;
			}

            Vector3 velocity = _agent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            _animator.SetFloat("forwardSpeed", speed);
        }

        private void AudioPlayer(string type) {
            switch(type) {
                case "footstep": {
                    if (footstepAudio == null)
                        return;
                    footstepAudio.pitch = Random.Range(0.8f, 1.2f);
                    footstepAudio.Play();
                    return;
                }
                default:
                    return;
            }
        }

        private void AnimationEventFootstep() {
			AudioPlayer("footstep");
        }

        public void StartMovement(Vector3 target) {
			if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && !CompareTag("Player"))
				return;
			_scheduler.StartAction(this);
            _agent.SetDestination(target);
            _agent.isStopped = false;
        }

		public void MoveTo(Vector3 target) {
			_agent.SetDestination(target);
			_agent.isStopped = false;
		}

        public void Cancel() {
            _agent.isStopped = true;
        }
    }
}