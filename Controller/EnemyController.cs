using UnityEngine;
using CovertPath.Mechanics;

namespace CovertPath.Mechanics {
	[RequireComponent(typeof(EnemyCombat))]
	[RequireComponent(typeof(EnemyAttributes))]
	public class EnemyController : MonoBehaviour {
		private EnemyCombat _combat;
		private EnemyAttributes _enemyAttributes;
		private GameObject _player;	

		private void Start() {
			_combat = GetComponent<EnemyCombat>();
			_enemyAttributes = GetComponent<EnemyAttributes>();
			_player = GameObject.FindWithTag("Player");
		}

		private void Update() {
			if (_enemyAttributes.isDead)
				return;
			if (_player.GetComponent<PlayerAttributes>().isDead)
				return;
			if (InChaseDistance() && _combat.IsAttackable(_player))
				_combat.StartAttack(_player);
			else
				_combat.Cancel();
		}

		private bool InChaseDistance() {
			float distanceToPlayer = Vector3.Distance(_player.transform.position, transform.position);
			return distanceToPlayer < _enemyAttributes.chaseDistance;
		}
	}
}
