using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CovertPath.Mechanics {
	[System.Serializable]
	public class Attribute {
		private List<float> _modifiers = new List<float>();
		public float baseValue;

		public float GetValue() {
			float finalValue = baseValue;
			foreach(float x in _modifiers)
				finalValue += x;
			return (float)Mathf.Round(finalValue * 100f) / 100f;
		}

		public void AddModifier(float modifier) {
			if (modifier !=	 0)
				_modifiers.Add(modifier);
		}

		public void RemoveModifier(float modifier) {
			if (modifier != 0)
				_modifiers.Remove(modifier);
		}
	}
}