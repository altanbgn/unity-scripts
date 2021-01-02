using UnityEngine;
using UnityEngine.UI;
using CovertPath.Mechanics;

namespace CovertPath.UI {
	public class PlayerHealthOrb : MonoBehaviour {
		private PlayerAttributes _player;

		private void Start() {
			_player = GameObject.FindWithTag("Player").GetComponent<PlayerAttributes>();
		}				

		private void Update() {
			float fillAmount = (float)_player.currentHealth / (float)_player.health.GetValue();
			GetComponent<Image>().fillAmount = fillAmount;
		}
	}
}