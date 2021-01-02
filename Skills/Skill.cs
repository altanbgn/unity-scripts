using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CovertPath.Interfaces;
using CovertPath.Mechanics;

namespace CovertPath.Skills {
	public class Skill : MonoBehaviour, ISkill {
		[Header("Skill Information")]
		public new string name = "Demonic Rage";
		[TextArea]
		public string description;
		public Sprite icon = null;
		public AudioSource skillAudio;
		public ParticleSystem skillParticle;

		[Header("Skill Modifiers")]
		public float skillCooldown;
		public float skillDuration;
		public float manaCost;

		private protected PlayerAttributes _playerAttributes;
		private float _cooldownCounter = Mathf.Infinity;

		private void Start() {
			_playerAttributes = GameObject.FindWithTag("Player").GetComponent<PlayerAttributes>();
			skillParticle.Stop();
		}

		private void Update() {
			_cooldownCounter += Time.deltaTime;
		}

		public virtual IEnumerator Initiate(GameObject player) {
			yield break;
		}

		public float GetCooldownCounter() {
			return _cooldownCounter;
		}

		public void Activate(GameObject player, EnemyAttributes target) {
			if (_cooldownCounter > skillCooldown && _playerAttributes.currentMana >= manaCost) {
				StartCoroutine(Initiate(player));
				_cooldownCounter = 0;
			}
		}
	}
}