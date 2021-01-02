using UnityEngine;

namespace CovertPath.Core {
	public class GameCursor : MonoBehaviour {
		enum CursorType {
			Default,
			Combat,
			Interact
		}
		[System.Serializable] struct CursorMapping {
			public CursorType type;
			public Texture2D texture;
		}
		[SerializeField] CursorMapping[] cursorMappings = null;
		private CursorType _currentCursor;

		private void Start() {
			SetCursor(CursorType.Default);
		}

		private void SetCursor(CursorType type) {
			if (_currentCursor == type)
				return;
			CursorMapping mapping = GetCursorMapping(type);
			Vector2 cursorHotspot = new Vector2(20, 6);
			Cursor.SetCursor(mapping.texture, cursorHotspot, CursorMode.Auto);
			_currentCursor = type;
		}

		private CursorMapping GetCursorMapping(CursorType type) {
			foreach (CursorMapping mapping in cursorMappings)
				if (mapping.type == type)
					return mapping;
			return cursorMappings[0];
		}

		public void SetCursorDefault() {
			SetCursor(CursorType.Default);
		}

		public void SetCursorCombat() {
			SetCursor(CursorType.Combat);
		}
		
		public void SetCursorInteract() {
			SetCursor(CursorType.Interact);
		}
	}
}