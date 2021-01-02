using UnityEngine;
using CovertPath.Mechanics;
using CovertPath.Utilities;

namespace CovertPath.Core {
	public class GameCamera : MonoBehaviour {
		public Transform target = null;
		public Vector3 offset = new Vector3(0,0,0);
		private float _distanceToPlayer;

		private void Start() {
			_distanceToPlayer = Vector3.Distance(transform.position, target.position);
		}

		private void LateUpdate() {
			LockInTarget();
			// ViewObstructed();
		}

		private void LockInTarget() {
			transform.position = target.position + offset;
			transform.LookAt(target.position);
		}

		private void ViewObstructed() {
			RaycastHit[] hits;
			hits = Physics.RaycastAll(transform.position, transform.forward, _distanceToPlayer - 15f);
			foreach (RaycastHit hit in hits) {
				if (hit.collider.GetComponent<PlayerAttributes>() != null)
					break;
				Renderer R = hit.collider.GetComponent<Renderer>();
				if (R == null)
					continue;
				AutoTransparent AT = R.GetComponent<AutoTransparent>();
				if (AT == null)
					AT = R.gameObject.AddComponent<AutoTransparent>();
				AT.BeTransparent();
			}
		}
	}
}