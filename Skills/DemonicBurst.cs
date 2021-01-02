using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using CovertPath.Mechanics;
using CovertPath.Interfaces;

namespace CovertPath.Skills {
	public class DemonicBurst : Skill {
		public float skillRange;
		public override IEnumerator Initiate(GameObject player) {
			Animator animator = player.GetComponent<Animator>();
			// Consumes mana
			_playerAttributes.ConsumeMana(manaCost);
			animator.SetTrigger("demonicRage");
			yield return new WaitForSeconds(0.5f);
			if (skillParticle != null) {
				skillParticle.Clear();
				skillParticle.Play();
			}

			Collider[] hitColliders = Physics.OverlapSphere(transform.position, skillRange);
			foreach (Collider hitCollider in hitColliders) {
				EnemyAttributes enemyAttributes = hitCollider.gameObject.GetComponent<EnemyAttributes>();
				if (enemyAttributes == null)
					continue;
				enemyAttributes.TakePhysicalDamage(_playerAttributes.attackDamage.GetValue() * 1.5f);
			}
			yield return 0;
		}
	}
}