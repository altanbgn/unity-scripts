using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using CovertPath.Core;
using CovertPath.Interfaces;

namespace CovertPath.Mechanics {
	[RequireComponent(typeof(ActionScheduler))]
	[RequireComponent(typeof(NavMeshAgent))]
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(CapsuleCollider))]
	public class EnemyAttributes : MonoBehaviour, IAttributes {
		[Header("Enemy")]
		public new string name;
		public bool isDead = false;
		public float chaseDistance = 8f;
		public int experienceAward;
		[Header("Audio")]
		public AudioSource hitAudio;
		public AudioSource hitImpactAudio;
		public AudioSource deathAudio;
		[Header("Particle")]
		public ParticleSystem hitParticle;
		[Header("Enemy Attributes")]
		public Attribute health;
		public Attribute mana;
		public float currentHealth;
		public float currentMana;
		[Header("Combat Attributes")]
		public Attribute attackDamage;
		public Attribute magicDamage;
		public Attribute attackRange;
		public Attribute attackSpeed;
		public Attribute physicalArmor;
		public Attribute magicArmor;
		[Header("Drop Item")]
		public GameObject dropItem;
		public float dropChance;

		private void AudioPlayer(string type) {
			switch(type) {
				case "hit": {
					if (hitAudio == null)
						return;
					hitAudio.pitch = Random.Range(0.8f, 1.2f);
					hitAudio.Play();
					return;	
				}
				case "hitImpact": {
					if (hitImpactAudio == null)
						return;
					hitImpactAudio.pitch = Random.Range(0.9f, 1.1f);
					hitImpactAudio.Play();
					return;
				}
				case "death": {
					if (deathAudio == null)
						return;
					deathAudio.pitch = Random.Range(0.8f, 1.2f);
					deathAudio.Play();
					return;
				}
				default: return;
			}
		}

		private void ParticlePlayer(string type) {
			switch(type) {
				case "hit": {
					if (hitParticle == null)
						return;
					hitParticle.Play();
					return;
				}
				default: return;
			}
		}

		public void TakePhysicalDamage(float damage) {
			AudioPlayer("hit");
			ParticlePlayer("hit");
			AudioPlayer("hitImpact");
			currentHealth = Mathf.Max(currentHealth - Mathf.Max(damage - physicalArmor.GetValue(), 0f), 0f);
			if (currentHealth == 0f)
				StartCoroutine(Die());
		}

		public void TakeMagicDamage(float damage) {
			AudioPlayer("hit");
			ParticlePlayer("hit");
			AudioPlayer("hitImpact");
			currentHealth = Mathf.Max(currentHealth - Mathf.Max(damage - magicArmor.GetValue(), 0f), 0f);
			if (currentHealth == 0f)
				StartCoroutine(Die());
		}

		public void DropItem() {
			if (dropItem == null)
				return;
			float chance = Random.Range(0f, 100f);
			if (chance < dropChance)
				Instantiate(dropItem, transform.position, transform.rotation);
		}

		public IEnumerator Die() {
			if (isDead)
				yield break;
			isDead = true;
			AudioPlayer("death");
			GameObject.FindWithTag("Player").GetComponent<PlayerAttributes>().GainExperience(experienceAward);
			GetComponent<Animator>().SetTrigger("die");
			GetComponent<ActionScheduler>().CancelAction();
			GetComponent<NavMeshAgent>().enabled = false;
			GetComponent<CapsuleCollider>().enabled = false;
			DropItem();
			yield return new WaitForSeconds(5);
			Destroy(this.gameObject);
		}
	}
}