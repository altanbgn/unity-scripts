using UnityEngine;
using CovertPath.Interfaces;
using CovertPath.Mechanics;
using CovertPath.Items;
using TMPro;

namespace CovertPath.Interactables {
	public class ItemInteractable : Interactable, IInteractable {
		public Item item;
		public GameObject itemNameObject;
		public TextMeshProUGUI itemNameText;

		private void Start() {
			itemNameObject.SetActive(false);
			itemNameText.GetComponent<TextMeshProUGUI>().text = item.name;
		}

		private void OnMouseEnter() {
			itemNameObject.SetActive(true);
		}

		private void OnMouseExit() {
			itemNameObject.SetActive(false);	
		}

		public override void Interact() {
			base.Interact();
			bool wasPickedUp = InventoryCore.instance.Add(item);
			if (wasPickedUp) {
				Destroy(gameObject);
			}
		}
	}
}