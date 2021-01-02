using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using CovertPath.Items;
using CovertPath.Mechanics;
using TMPro;

namespace CovertPath.UI {
	public class EquipmentSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {
		public Image icon;
		public Item item;
		public int index;
		private EquipmentCore _equipmentCore;
		private DragHolderCore _dragHolderCore;
		private GameObject _itemDragHolder;
		private GameObject _descriptionObject;
		private bool _hovered = false;
		private float _scaleFactor;

		private void Start() {
			_scaleFactor = GameObject.FindWithTag("Canvas").GetComponent<Canvas>().scaleFactor;
			_descriptionObject = GameObject.FindWithTag("Item/DescriptionUI");
			_dragHolderCore = DragHolderCore.instance;
			_equipmentCore = EquipmentCore.instance;
		}

		public void OnBeginDrag(PointerEventData eventData) {
			// Get Component of an drag holder icon
			Image _dragHolderImage = _dragHolderCore.gameObject.GetComponent<Image>();
			// Make icon color transparent in slot
			var i_color = icon.color;
			i_color.a = 0.3f;
			icon.color = i_color;
			
			_dragHolderCore.gameObject.GetComponent<RectTransform>().transform.position = Input.mousePosition;
			_dragHolderCore.ClearHolder();
			_dragHolderCore.StartDrag(item);
			
			if (item != null)
				item.Unequip();
		}

		public void OnDrag(PointerEventData eventData) {
			// Make drag holder icon follow cursor
			_dragHolderCore.gameObject.GetComponent<RectTransform>().transform.position = Input.mousePosition;
		}

		public void OnDrop(PointerEventData eventData) {
			if (_equipmentCore.items[index] == _dragHolderCore.itemFirst)
				return;

			if (item != null)
				item.Unequip();

			// If there is item caught on drop holder and the not type of equipment then quit function
			if (_dragHolderCore.itemFirst != null && _dragHolderCore.itemFirst.type != ItemType.Equipment)
				return;
			// If there is item on the slot. Store it in drop holder
			_dragHolderCore.DropDrag(item);
			// Update equipment core
			_equipmentCore.items[index] = _dragHolderCore.itemFirst;
			// Update slot
			AddItem(_dragHolderCore.itemFirst);
			// Updating whole equipmentcore
			_equipmentCore.onItemChangedCallback.Invoke();

			if (item != null)
				item.Equip();
		}

		public void OnEndDrag(PointerEventData eventData) {
			// Get Component of an drag holder icon
			Image _dragHolderImage = _dragHolderCore.gameObject.GetComponent<Image>();
			
			// Make icon color vivid again in slot
			var i_color = icon.color;
			i_color.a = 1f;
			icon.color = i_color;

			// If drag holder successfully dropped
			if (_dragHolderCore.dropped == true) {
				// Update equipment core with dragholder second caught item
				_equipmentCore.items[index] = _dragHolderCore.itemSecond;
				// Update item to slot
				AddItem(_dragHolderCore.itemSecond);
			} else
				// if drag holder not successfully dropped return initial item
				_dragHolderCore.DropDrag(item);
			// Updating whole equipmentcore
			_equipmentCore.onItemChangedCallback.Invoke();

			if (item != null)
				item.Equip();
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