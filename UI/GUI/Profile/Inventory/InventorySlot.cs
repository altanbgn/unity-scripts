using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using CovertPath.Items;
using CovertPath.Mechanics;
using TMPro;

namespace CovertPath.UI {
	public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {
		public Image icon;
		public Item item;
		public int index;
		private InventoryCore _inventoryCore;
		private DragHolderCore _dragHolderCore;
		private GameObject _itemDragHolder;
		private GameObject _descriptionObject;
		private bool _hovered = false;
		private float _scaleFactor;

		private void Start() {
			_scaleFactor = GameObject.FindWithTag("Canvas").GetComponent<Canvas>().scaleFactor;
			_descriptionObject = GameObject.FindWithTag("Item/DescriptionUI");
			_dragHolderCore = DragHolderCore.instance;
			_inventoryCore = InventoryCore.instance;
		}

		private bool IsEmpty() {
			if (item == null)
				return true;
			return false;
		}

		public void OnBeginDrag(PointerEventData eventData) {
			Image _dragHolderImage = _dragHolderCore.gameObject.GetComponent<Image>();
			var i_color = icon.color;

			i_color.a = 0.3f;
			icon.color = i_color;

			_dragHolderCore.gameObject.GetComponent<RectTransform>().transform.position = Input.mousePosition;
			_dragHolderCore.ClearHolder();
			_dragHolderCore.StartDrag(item);
		}

		public void OnDrag(PointerEventData eventData) {
			_dragHolderCore.gameObject.GetComponent<RectTransform>().transform.position = Input.mousePosition;
		}

		public void OnDrop(PointerEventData eventData) {
			if (_inventoryCore.items[index] == _dragHolderCore.itemFirst)
				return;

			_dragHolderCore.DropDrag(item);
			_inventoryCore.items[index] = _dragHolderCore.itemFirst;
			AddItem(_dragHolderCore.itemFirst);

			_inventoryCore.onItemChangedCallback.Invoke();
		}

		public void OnEndDrag(PointerEventData eventData) {
			Image _dragHolderImage = _dragHolderCore.gameObject.GetComponent<Image>();

			var i_color = icon.color;
			i_color.a = 1f;
			icon.color = i_color;

			if (_dragHolderCore.dropped == true) {
				_inventoryCore.items[index] = _dragHolderCore.itemSecond;
				AddItem(_dragHolderCore.itemSecond);
				_inventoryCore.onItemChangedCallback.Invoke();
			} else
				_dragHolderCore.DropDrag(item);

			_inventoryCore.onItemChangedCallback.Invoke();
		}

		public void OnPointerEnter(PointerEventData eventData) {
			_hovered = true;
			if (item != null && _descriptionObject != null) {
				string fullDescription = "";
				fullDescription += item.GetNameWithRarity() + "\n\n";
				fullDescription += item.description + "\n\n";
				fullDescription += item.GetModifiersText();

				_descriptionObject.GetComponent<TextMeshProUGUI>().text = fullDescription;
			}
		}

		public void OnPointerExit(PointerEventData eventData) {
			_hovered = false;
			if (_descriptionObject != null) {
				_descriptionObject.GetComponent<TextMeshProUGUI>().text = "";
			}
		}

		public void AddItem(Item p_item) {
			if (p_item == null) {
				ClearItem();
				return;
			}
			item = p_item;
			icon.sprite = item.icon;
			icon.enabled = true;
		}

		public void ClearItem() {
			item = null;
			icon.sprite = null;
			icon.enabled = false;
		}
	}
}