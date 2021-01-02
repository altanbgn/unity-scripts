using UnityEngine;
using UnityEngine.UI;
using CovertPath.Mechanics;
using TMPro;

namespace CovertPath.UI {
	public class EnemyHealthBar : MonoBehaviour {
		[SerializeField] private GameObject _enemyHealthObject = null;
		[SerializeField] private Image _enemyHealthImage = null;
		[SerializeField] private TextMeshProUGUI _enemyHealthName = null;
		private EnemyAttributes _target = null;

		private void Update() {
			ToggleWindow();
			UpdateWindow();
		}

		private void ToggleWindow() {
			RaycastHit[] hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
			foreach(RaycastHit hit in hits) {
				EnemyAttributes target = hit.transform.GetComponent<EnemyAttributes>();
				if (target == null) {
					_target = null;
				} else {
					_target = target;
					break;
				}
			}
		}

		private void UpdateWindow() {
			if (_target == null || _target.isDead == true) {
				_enemyHealthObject.SetActive(false);
				return;
			} else {
				_enemyHealthObject.SetActive(true);			
			}
			float fillAmount = (float)_target.currentHealth / (float)_target.health.GetValue();
			_enemyHealthImage.fillAmount = fillAmount;
			_enemyHealthName.text = _target.name;
		}
	}
}