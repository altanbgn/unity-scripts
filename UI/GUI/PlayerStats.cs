using UnityEngine;
using TMPro;
using CovertPath.Mechanics;

namespace CovertPath.UI {
	public class PlayerStats : MonoBehaviour {
		[SerializeField] private GameObject _stats = null;
		[SerializeField] private TextMeshProUGUI _statsName = null;
		[SerializeField] private TextMeshProUGUI _statsValue = null;
		private PlayerAttributes _player = null;

		private void Start() {
			_player = GameObject.FindWithTag("Player").GetComponent<PlayerAttributes>();
		}

		private void Update() {
			ToggleWindow();
			UpdateWindow();
		}

		private void ToggleWindow() {
			if (Input.GetKeyDown(KeyCode.C))
				_stats.SetActive(!_stats.activeSelf);
		}

		private void UpdateWindow() {
			_statsName.text = 
				"Level\n" + 
				"Experience\n" +
				"Required Experience\n\n" + 
				"Health\n" + 
				"Mana\n" + 
				"Regeneration\n" +
				"Attack Damage\n" + 
				"Magic Damage\n" + 
				"Physical Armor\n" + 
				"Magic Armor\n" + 
				"Attack Range\n" + 
				"Attack Speed\n";

			_statsValue.text =
				_player.level + "\n" +
				_player.experiencePoints + "\n" +
				_player.experienceToLevelUp + "\n\n" +
				(int) _player.currentHealth + "/" + _player.health.GetValue() + "\n" + 
				(int) _player.currentMana + "/" + _player.mana.GetValue() + "\n" + 
				_player.regenAmount.GetValue() + "\n" +
				_player.attackDamage.GetValue() + "\n" + 
				_player.magicDamage.GetValue() + "\n" + 
				_player.physicalArmor.GetValue() + "\n" + 
				_player.magicArmor.GetValue() + "\n" + 
				_player.attackRange.GetValue() + "\n" + 
				_player.attackSpeed.GetValue();
		}
	}
}
