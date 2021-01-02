using UnityEngine;
using CovertPath.Items;
using CovertPath.Mechanics;

namespace CovertPath.UI {
	public class EquipmentUpdater : MonoBehaviour {
		public GameObject equipmentSlotsUI;
		private EquipmentSlot[] _equipmentSlots;
		private EquipmentCore _equipmentCore;

		private void Start() {
			_equipmentCore = EquipmentCore.instance;
			_equipmentCore.onItemChangedCallback += UpdateUI;

			_equipmentSlots = equipmentSlotsUI.GetComponentsInChildren<EquipmentSlot>();
			for (int i = 0; i < _equipmentSlots.Length; i++)
				_equipmentSlots[i].index = i;
		}

		public void UpdateUI() {
			for (int i = 0; i < _equipmentSlots.Length; i++)
				_equipmentSlots[i].AddItem(_equipmentCore.items[i]);
		}
	}
}