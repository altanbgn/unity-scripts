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
	public class PlayerAttributes : MonoBehaviour, IAttributes {
		public bool isDead = false;
		[Header("Level Progression")]
		public int level = 1;
		public int experienceToLevelUp = 100;
		public int experiencePoints = 0;
		[Header("Audio")]
		public AudioSource hitAudio;
		public AudioSource deathAudio;
		public AudioSource levelUpAudio;
		[Header("Particle")]
		public ParticleSystem hitParticle;
		public ParticleSystem levelUpParticle;
		[Header("Player Attributes")]
		public Attribute health;
		public Attribute mana;
		public float currentHealth { get; private set; }
		public float currentMana { get; private set; }
		[Header("Combat Attributes")]
		public Attribute regenAmount;
		public Attribute attackDamage;
		public Attribute magicDamage;
		public Attribute attackRange;
		public Attribute attackSpeed;
		public Attribute physicalArmor;
		public Attribute magicArmor;

		private bool healing = false;

		private void AudioPlayer(string type) {
			switch (type) {
				case "hit": {
					if (hitAudio == null)
						return;
					hitAudio.pitch = Random.Range(0.8f, 1.2f);
					hitAudio.Play();
					return;
				}
				case "death": {
					if (deathAudio == null)
						return;
					deathAudio.pitch = Random.Range(0.8f, 1.2f);
					deathAudio.Play();
					return;
				}
				case "levelUp": {
					if (levelUpAudio == null)
						return;
					levelUpAudio.Play();
					return;
				}
				default: return;
			}
		}

		private void ParticlePlayer(string type) {
			switch (type) {
				case "hit": {
					if (hitParticle == null)
						return;
					hitParticle.Play();
					return;
				}
				case "levelUp": {
					if (levelUpParticle == null)
						return;
					levelUpParticle.Play();
					return;
				}
			}
		}

		private void Start() {
			currentHealth = health.GetValue();
			currentMana = mana.GetValue();
			StartCoroutine(Regenerate());
		}

		private void Update() {
			if (isDead == true) {
				StopCoroutine(Regenerate());
				return;
			}
		}

		public IEnumerator Regenerate() {
			while(isDead == false) {
				Heal(regenAmount.GetValue(), regenAmount.GetValue());
				yield return new WaitForSeconds(1f);
			}
		}

		public void Heal(float healthAmount, float manaAmount) {
			currentHealth = Mathf.Min(currentHealth + healthAmount, health.GetValue());
			currentMana = Mathf.Min(currentMana + manaAmount, mana.GetValue());
		}

		public void LevelUp() {
			AudioPlayer("levelUp");
			ParticlePlayer("levelUp");

			health.baseValue = Mathf.Round(health.baseValue * 1.05f);
			mana.baseValue = Mathf.Round(mana.baseValue * 1.05f);
			attackDamage.baseValue = Mathf.Round(attackDamage.baseValue * 1.05f);
			physicalArmor.baseValue += 1f;
			magicArmor.baseValue += 1f;
			regenAmount.baseValue += 0.1f;

			level++;
			experiencePoints = experiencePoints - experienceToLevelUp;
			experienceToLevelUp = Mathf.RoundToInt(Mathf.Sqrt(level) * 100);
		}

		public void GainExperience(int amount) {
			experiencePoints += amount;
			while (experiencePoints >= experienceToLevelUp) {
				LevelUp();
			}
		}

		public void TakePhysicalDamage(float damage) {
			AudioPlayer("hit");
			ParticlePlayer("hit");

			currentHealth = Mathf.Max(currentHealth - Mathf.Max(damage - physicalArmor.GetValue(), 0f), 0f);
			if (currentHealth == 0f)
				StartCoroutine(Die());
		}

		public void TakeMagicDamage(float damage) {
			AudioPlayer("hit");
			ParticlePlayer("hit");

			currentHealth = Mathf.Max(currentHealth - Mathf.Max(damage - magicArmor.GetValue(), 0f), 0f);
			if (currentHealth == 0f)
				StartCoroutine(Die());
		}

		public void ConsumeMana(float amount) {
			currentMana = Mathf.Max(currentMana - amount, 0f);
		}

		public IEnumerator Die() {
			if (isDead)
				yield break;
			isDead = true;
			AudioPlayer("death");
			GetComponent<Animator>().SetTrigger("die");
			GetComponent<Animator>().SetTrigger("stopAttack");
			GetComponent<ActionScheduler>().CancelAction();
			yield return new WaitForSeconds(3);
			SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
		}
	}
}