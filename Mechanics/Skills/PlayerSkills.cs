using UnityEngine;
using CovertPath.Core;
using CovertPath.Interfaces;
using CovertPath.Skills;

namespace CovertPath.Mechanics {
	[RequireComponent(typeof(ActionScheduler))]
	public class PlayerSkills : MonoBehaviour {
		public Skill _skillQ;
		public Skill _skillW;
		public Skill _skillE;
		public Skill _skillR;

		private ActionScheduler _scheduler;
		private PlayerAttributes _player;
		private EnemyAttributes _target;

		private void Start() {
			_scheduler = GetComponent<ActionScheduler>();
			_player = GetComponent<PlayerAttributes>();
		}

		private void Update() {
			if (_player.isDead)
				return;
			if (Input.GetKeyDown(KeyCode.Q) && _skillQ != null)
				_skillQ.Activate(gameObject, _target);
			if (Input.GetKeyDown(KeyCode.W) && _skillW != null)
				_skillW.Activate(gameObject, _target);
			if (Input.GetKeyDown(KeyCode.E) && _skillE != null)
				_skillE.Activate(gameObject, _target);
			if (Input.GetKeyDown(KeyCode.R) && _skillR != null)
				_skillR.Activate(gameObject, _target);
		}

		public void SetTarget(GameObject combatTarget) {
			_target = combatTarget.GetComponent<EnemyAttributes>();
		}
	}
}