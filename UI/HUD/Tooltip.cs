using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace CovertPath.UI {
	public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
		[SerializeField] private GameObject _tooltip = null;
		[SerializeField] private bool _static = true;
		[SerializeField] private EquipmentSlot _slot = null;
		[SerializeField] private TextMeshProUGUI _textmesh = null;

		private void Start() {
			_tooltip.SetActive(false);
		}

		public void OnPointerEnter(PointerEventData eventData) {
			if (_static == false) {
				if (_slot == null)
					return;
				if (_slot.item == null)
					return;
				_textmesh.text =
					"<b>" + _slot.item.GetNameWithRarity() +
					"</b>\n" + _slot.item.description +
					"\n\n" + _slot.item.GetModifiersText() +
					"\n\n<style=WARNING>Right click on item immediately removes it</style>";
			}
			_tooltip.SetActive(true);
		}

		public void OnPointerExit(PointerEventData eventData) {
			_tooltip.SetActive(false);
		}
	}
}