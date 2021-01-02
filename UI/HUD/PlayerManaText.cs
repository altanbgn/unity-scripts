using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CovertPath.Mechanics;

namespace CovertPath.UI {
    public class PlayerManaText : MonoBehaviour {
        private PlayerAttributes _player;
        private string _text;
        private TextMeshProUGUI _textObject;

		private void Start() {
			_player = GameObject.FindWithTag("Player").GetComponent<PlayerAttributes>();
            _textObject = GetComponent<TextMeshProUGUI>();
		}

        private void Update() {
            _text = "Mana : " + Mathf.Round(_player.currentMana) + "/" + Mathf.Round(_player.mana.GetValue());
            _textObject.text = _text;
        }
    }
}