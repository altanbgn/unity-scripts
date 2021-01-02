using UnityEngine;
using CovertPath.Items;
using CovertPath.Mechanics;

namespace CovertPath.UI {
	public class ConsumableUpdater : MonoBehaviour {
		public GameObject consumableSlotsUI;
		private ConsumableSlot[] _consumableSlots;
		private ConsumableCore _consumableCore;

		private void Start() {
			_consumableCore = ConsumableCore.instance;
			_consumableCore.onItemChangedCallback += UpdateUI;

			_consumableSlots = consumableSlotsUI.GetComponentsInChildren<ConsumableSlot>();
			for(int i = 0; i < _consumableSlots.Length; i++)
				_consumableSlots[i].index = i;
		}

		public void UpdateUI() {
			for (int i = 0; i < _consumableSlots.Length; i++)
				_consumableSlots[i].AddItem(_consumableCore.items[i]);
		}
	}
}