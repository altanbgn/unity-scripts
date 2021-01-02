using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

namespace CovertPath.Core {
	public class GameSystem : MonoBehaviour {
		[SerializeField] private AudioMixer _audioMixer;
		[SerializeField] private TMP_Dropdown _resolutionDropdown;
		private Resolution[] resolutions;

		private void Start() {
			SetResolutionOptionsToDropdown();
		}

		public void NewGame() {
			SceneManager.LoadScene(1);
		}

		public void SetResolutionOptionsToDropdown() {
			if (_resolutionDropdown == null)
				return;
			resolutions = Screen.resolutions;
			_resolutionDropdown.ClearOptions();
			List<string> options = new List<string>();
			int currentResolutionIndex = 0;
			for (int i = 0; i < resolutions.Length; i++)
			{
				string option = resolutions[i].width + " x " + resolutions[i].height;
				options.Add(option);

				if (
					resolutions[i].width == Screen.currentResolution.width &&
					resolutions[i].height == Screen.currentResolution.height
				)
					currentResolutionIndex = i;
			}

			_resolutionDropdown.AddOptions(options);
			_resolutionDropdown.value = currentResolutionIndex;
			_resolutionDropdown.RefreshShownValue();
		}

		public void QuitToMenu() {
			SceneManager.LoadScene(0);
		}

		public void QuitGame() {
			Application.Quit();
		}

		public void SetMasterVolume(float volume) {
			_audioMixer.SetFloat("master", volume);
		}

		public void SetMusicVolume(float volume) {
			_audioMixer.SetFloat("music", volume);
		}

		public void SetSFXVolume(float volume) {
			_audioMixer.SetFloat("sfx", volume);
		}

		public void SetQuality(int qualityIndex) {
			QualitySettings.SetQualityLevel(qualityIndex);
		}

		public void SetFullscreen(bool isFullscreen) {
			Screen.fullScreen = isFullscreen;
		}

		public void SetResolution(int resolutionIndex) {
			Resolution resolution = resolutions[resolutionIndex];
			Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
		}
	}
}