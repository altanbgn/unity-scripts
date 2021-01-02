using UnityEngine;
using UnityEngine.AI;
using CovertPath.Interfaces;
using CovertPath.UI;
using CovertPath.Items;
using TMPro;

namespace CovertPath.Interactables {
	public class TeleportInteractable : Interactable, IInteractable {
		public Vector3 destination;
		public GameObject hoverText;
		private NavMeshAgent _agent;
		private MeshRenderer _mesh;

		private void Start() {
			_agent = GameObject.FindWithTag("Player").GetComponent<NavMeshAgent>();
			_mesh = GetComponent<MeshRenderer>();
			hoverText.SetActive(false);
			_mesh.enabled = false;
		}

		private void OnMouseEnter() {
			_mesh.enabled = true;
			hoverText.SetActive(true);
		}

		private void OnMouseExit() {
			_mesh.enabled = false;
			hoverText.SetActive(false);
		}

		public override void Interact() {
			base.Interact();
			_agent.Warp(destination);
		}
	}
}