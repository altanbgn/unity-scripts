using UnityEngine;
using UnityEngine.SceneManagement;
using CovertPath.Mechanics;

namespace CovertPath.Mechanics {
	[RequireComponent(typeof(GVBeastCombat))]
	[RequireComponent(typeof(EnemyAttributes))]
	public class GVBeastController : MonoBehaviour {
		private GVBeastCombat _combat;
		private EnemyAttributes _enemyAttributes;
		private GameObject _player;	

		private void Start() {
			_combat = GetComponent<GVBeastCombat>();
			_enemyAttributes = GetComponent<EnemyAttributes>();
			_player = GameObject.FindWithTag("Player");
		}

		private void Update() {
			if (_enemyAttributes.isDead) {
				EndScreen();
				return;
			}
			if (_player.GetComponent<PlayerAttributes>().isDead)
				return;
			if (InChaseDistance() && _combat.IsAttackable(_player))
				_combat.StartAttack(_player);
			else
				_combat.Cancel();
		}

		private void EndScreen() {
			SceneManager.LoadScene(0);
		}

		private bool InChaseDistance() {
			float distanceToPlayer = Vector3.Distance(_player.transform.position, transform.position);
			return distanceToPlayer < _enemyAttributes.chaseDistance;
		}
	}
}
