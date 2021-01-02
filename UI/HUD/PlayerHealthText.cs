using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CovertPath.Mechanics;

namespace CovertPath.UI {
    public class PlayerHealthText : MonoBehaviour {
        private PlayerAttributes _player;
        private string _text;
        private TextMeshProUGUI _textObject;

		private void Start() {
			_player = GameObject.FindWithTag("Player").GetComponent<PlayerAttributes>();
            _textObject = GetComponent<TextMeshProUGUI>();
		}

        private void Update() {
            _text = "Health : " + Mathf.Round(_player.currentHealth) + "/" + Mathf.Round(_player.health.GetValue());
            _textObject.text = _text;
        }
    }
}