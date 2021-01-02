using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingToMainGame : MonoBehaviour {
	private void Start() {
		SceneManager.LoadScene(2);
	}
}