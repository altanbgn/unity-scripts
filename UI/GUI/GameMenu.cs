using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace CovertPath.UI {
	public class GameMenu : MonoBehaviour {
		[SerializeField] private GameObject _menuWindow;
		[SerializeField] private GameObject _settingsWindow;
		[SerializeField] private GameObject _profileWindow;
		[SerializeField] private GameObject _statsWindow;
		
		private void Update() {
			ToggleWindow();
		}

		private void ToggleWindow() {
			if (Input.GetKeyDown(KeyCode.Escape)) {
				if (_profileWindow.activeSelf == true || _statsWindow.activeSelf == true) {
					_profileWindow.SetActive(false);
					_statsWindow.SetActive(false);
					return;
				} else {
					if (_menuWindow.activeSelf == true || _settingsWindow.activeSelf == true) {
						_menuWindow.SetActive(false);
						_settingsWindow.SetActive(false);
					} else
						_menuWindow.SetActive(true);
				}
			}
		}

		public void ReturnToGame() {
			_menuWindow.SetActive(false);
		}

		public void OpenSettingsMenu() {
			_menuWindow.SetActive(false);
			_settingsWindow.SetActive(true);
		}

		public void QuitToTitle() {
			SceneManager.LoadScene(0);
		}
	}
}