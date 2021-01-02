using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;
using CovertPath.Interfaces;
using CovertPath.Mechanics;

namespace CovertPath.Skills {
	public class DemonicRage : Skill {	
		public override IEnumerator Initiate(GameObject player) {
			Animator animator = player.GetComponent<Animator>();
			
			float attackDamageModifier = _playerAttributes.attackDamage.GetValue() * 0.2f;
			float magicDamageModifier = _playerAttributes.magicDamage.GetValue() * 0.2f;
			float physicalArmorModifier = _playerAttributes.physicalArmor.GetValue() * 0.2f;
			float magicArmorModifier = _playerAttributes.magicArmor.GetValue() * 0.2f;

			_playerAttributes.ConsumeMana(manaCost);
			animator.SetTrigger("demonicRage");

			if (skillAudio != null)
				skillAudio.Play();

			yield return new WaitForSeconds(1f);

			if (skillParticle != null) {
				skillParticle.Clear();
				skillParticle.Play();
			}

			_playerAttributes.attackDamage.AddModifier(attackDamageModifier);
			_playerAttributes.magicDamage.AddModifier(magicDamageModifier);
			_playerAttributes.physicalArmor.AddModifier(physicalArmorModifier);
			_playerAttributes.magicArmor.AddModifier(magicArmorModifier);

			yield return new WaitForSeconds(skillDuration);

			if (skillParticle != null)
				skillParticle.Stop();

			_playerAttributes.attackDamage.RemoveModifier(attackDamageModifier);
			_playerAttributes.magicDamage.RemoveModifier(magicDamageModifier);
			_playerAttributes.physicalArmor.RemoveModifier(physicalArmorModifier);
			_playerAttributes.magicArmor.RemoveModifier(magicArmorModifier);
		}
	}
}