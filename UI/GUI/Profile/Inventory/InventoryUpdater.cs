using UnityEngine;
using CovertPath.Items;
using CovertPath.Mechanics;

namespace CovertPath.UI {
	public class InventoryUpdater : MonoBehaviour {
		public GameObject inventorySlotsUI;
		private InventorySlot[] _inventorySlots;
		private InventoryCore _inventoryCore;

		private void Start() {
			_inventoryCore = InventoryCore.instance;
			_inventoryCore.onItemChangedCallback += UpdateUI;

			_inventorySlots = inventorySlotsUI.GetComponentsInChildren<InventorySlot>();
			for(int i = 0; i < _inventorySlots.Length; i++)
				_inventorySlots[i].index = i;
		}

		public void UpdateUI() {
			for (int i = 0; i < _inventorySlots.Length; i++)
				_inventorySlots[i].AddItem(_inventoryCore.items[i]);
		}
	}
}