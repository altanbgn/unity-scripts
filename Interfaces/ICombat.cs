using UnityEngine;

namespace CovertPath.Interfaces {
	public interface ICombat {
		void StartAttack(GameObject combatTarget);
		bool IsAttackable(GameObject combatTarget);
	}
}