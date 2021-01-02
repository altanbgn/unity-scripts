using UnityEngine;
using CovertPath.Mechanics;

namespace CovertPath.Interfaces {
	public interface ISkill {
		void Activate(GameObject player, EnemyAttributes target);
	}
}