using UnityEngine;
using UnityEngine.UI;
using CovertPath.Items;

namespace CovertPath.Mechanics {
	public class DragHolderCore : MonoBehaviour {
		#region Singleton
			public static DragHolderCore instance;
			private void Awake() {
				if (instance != null) {
					Debug.LogWarning("More than one instance of Item Drag Holder found!");
					return;
				}
				instance = this;
			}
		#endregion

		public Item itemFirst = null;
		public Item itemSecond = null;
		public bool dropped = false;
		private Image _image;

		private void Start() {
			_image = GetComponent<Image>();
		}

		public void StartDrag(Item p_itemFirst) {
			if (p_itemFirst != null) {
				_image.sprite = p_itemFirst.icon;
				_image.enabled = true;
			}

			itemFirst = p_itemFirst;
		}

		public void DropDrag(Item p_itemSecond) {
			_image.sprite = null;
			_image.enabled = false;

			itemSecond = p_itemSecond;
			dropped = true;
		}

		public void ClearHolder() {
			itemFirst = null;
			itemSecond = null;
			dropped = false;
		}
	}
}