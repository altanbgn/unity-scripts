using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CovertPath.Items;

namespace CovertPath.Mechanics {
	public class InventoryCore : MonoBehaviour {
		#region Singleton
			public static InventoryCore instance;
			private void Awake() {
				if (instance != null) {
					Debug.LogWarning("More than one instance of inventory found");
					return;
				}
				instance = this;
			}
		#endregion

		public delegate void OnItemChanged();
		public OnItemChanged onItemChangedCallback;
		public int space;
		public List<Item> items = new List<Item>();

		private void Start() {
			for(int i = 0; i < space; i++)
				items.Add(null);
		}

		public bool Add(Item item) {
			if (items.Contains(null) == false) {
				Debug.Log("Not enough space");
				return false;
			}
			for (int i = 0; i < space; i++) {
				if (items[i] == null) {
					items[i] = item;
					break;
				}
			}
			if (onItemChangedCallback != null)
				onItemChangedCallback.Invoke();
			return true;
		}

		public void Remove(int index) {
			items[index] = null;
			if (onItemChangedCallback != null)
				onItemChangedCallback.Invoke();
		}
	}
}