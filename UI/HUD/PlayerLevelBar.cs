using UnityEngine;
using UnityEngine.UI;
using CovertPath.Mechanics;
using TMPro;

namespace CovertPath.UI {
	public class PlayerLevelBar : MonoBehaviour {
		[SerializeField] private Image _image;
		private PlayerAttributes _player;

		private void Start() {
			_player = GameObject.FindWithTag("Player").GetComponent<PlayerAttributes>();
		}

		private void Update() {
			UpdateProgressBar();
		}

		private void UpdateProgressBar() {
			float fillAmount = (float) _player.experiencePoints / (float) _player.experienceToLevelUp;
			_image.fillAmount = fillAmount;
		}
	}
}