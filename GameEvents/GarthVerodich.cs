using System.Collections;
using UnityEngine;
using CovertPath.Core;

namespace CovertPath.GameEvents {
	public class GarthVerodich : MonoBehaviour {
		public GameObject entranceBlocker;
		public Vector3 entranceLocation;
		public AudioClip bossMusicIntro;
		public AudioClip bossMusicLoop;
		private Vector3 _velocity = Vector3.right;

		private void OnTriggerEnter(Collider other) {
			if (!other.gameObject.CompareTag("Player"))
				return;
			entranceBlocker.transform.position = entranceLocation;
			GameAudio gameAudio = GameObject.FindWithTag("Audio/Manager").GetComponent<GameAudio>();
			gameAudio.ChangeBGM(bossMusicIntro, bossMusicLoop);
			Destroy(this.gameObject);
		}
	}
}