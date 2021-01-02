using UnityEngine;
using CovertPath.Mechanics;

namespace CovertPath.UI {
	public class Profile : MonoBehaviour {
		[SerializeField] private GameObject _profileObject;

		private void Update() {
			ToggleWindow();
		}

		private void ToggleWindow() {
			if (Input.GetKeyDown(KeyCode.I))
				_profileObject.SetActive(!_profileObject.activeSelf);
		}
	}
}