using UnityEngine;

namespace CovertPath.Core {
	public class GameAudio : MonoBehaviour {
		private AudioSource _bgmAudio;
		private bool _newAudio = false;
		private AudioClip _audioIntro;
		private AudioClip _audioLoop;

		private void Start() {
			_bgmAudio = GameObject.FindWithTag("Audio/BGM").GetComponent<AudioSource>();
		}

		private void Update() {
			if (_newAudio == true) {
				_bgmAudio.clip = _audioIntro;
				_bgmAudio.loop = false;
				_bgmAudio.Stop();
				_bgmAudio.Play();
				_newAudio = false;
			}
			if (_bgmAudio.loop == false && _bgmAudio.isPlaying == false) {
				_bgmAudio.clip = _audioLoop;
				_bgmAudio.loop = true;
				_bgmAudio.Stop();
				_bgmAudio.Play();
			}
		}

		public void ChangeBGM(AudioClip p_intro, AudioClip p_loop) {
			_audioIntro = p_intro;
			_audioLoop = p_loop;
			_newAudio = true;
		}
	}
}