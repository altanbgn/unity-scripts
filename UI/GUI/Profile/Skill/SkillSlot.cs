using UnityEngine;
using UnityEngine.UI;
using CovertPath.Skills;
using TMPro;

namespace CovertPath.UI {
	public class SkillSlot : MonoBehaviour {
		[SerializeField] private Skill _skill = null;
		[SerializeField] private Image _skillIcon;
		[SerializeField] private Image _skillCooldown;
		[SerializeField] private TextMeshProUGUI _skillDescription;

		private void Start() {
			if (_skillIcon == null)
				return;
			if (_skill == null) {
				_skillIcon.enabled = false;
				_skillCooldown.enabled = false;
				return;
			}
			_skillDescription.text = _skill.description;
			_skillIcon.sprite = _skill.icon;
		}

		private void Update() {
			if (_skill == null)
				return;
			float counter = _skill.GetCooldownCounter();
			if (
				_skill.skillCooldown >= counter &&
				counter / _skill.skillCooldown <= 1
			) {
				_skillCooldown.fillAmount = 1 - (counter / _skill.skillCooldown);
			} else  {
				_skillCooldown.fillAmount = 0;
			}
		}
	}
}