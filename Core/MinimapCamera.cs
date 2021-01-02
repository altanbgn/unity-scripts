using UnityEngine;
using CovertPath.Mechanics;
using CovertPath.Utilities;

namespace CovertPath.Core {
	public class MinimapCamera : MonoBehaviour {
		public Transform target = null;
		public Vector3 offset = new Vector3(0,0,0);
		private float _distanceToPlayer;

		private void Start() {
			_distanceToPlayer = Vector3.Distance(transform.position, target.position);
		}

		private void LateUpdate() {
			LockInTarget();
		}

		private void LockInTarget() {
			transform.position = target.position + offset;
		}
	}
}