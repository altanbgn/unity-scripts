using UnityEngine;
using CovertPath.UI;
using CovertPath.Mechanics;

namespace CovertPath.Items {
	[CreateAssetMenu(fileName = "Item", menuName = "CovertPath/Items")]
	public class Item : ScriptableObject {
		[Header("Item Information")]
		public new string name = "New Item";
		[TextArea]
		public string description = "";
		public ItemType type;
		public ItemRarity rarity;
		public Sprite icon = null;
		
		[Header("Equipment Modifiers")]
		public float healthModifier;
		public float manaModifier;
		public float regenModifier;
		public float attackDamageModifier;
		public float magicDamageModifier;
		public float attackRangeModifier;
		public float attackSpeedModifier;
		public float physicalArmorModifier;
		public float magicArmorModifier;
		
		[Header("Consumable Modifiers")]
		public float healthHealAmount;
		public float manaHealAmount;

		public string GetNameWithRarity() {
			string result = "";
			if (rarity == null)
				return name;
			if (rarity == ItemRarity.Common)
				result += "<#7f8c8d>";
			if (rarity == ItemRarity.Rare)
				result += "<#27ae60>";
			if (rarity == ItemRarity.Unique)
				result += "<#8e44ad>";
			if (rarity == ItemRarity.Legendary)
				result += "<#d35400>";
			result += name + "</color>";
			return result;
		}

		public string GetModifiersText() {
			string result = "<style=MODIFIER>";
			if (healthModifier != 0f)
				result += "+" + healthModifier + " Health\n";
			if (manaModifier != 0f)
				result += "+" + manaModifier + " Mana\n";
			if (regenModifier != 0f)
				result += "+" + regenModifier + " Regen Amount\n";
			if (attackDamageModifier != 0f)
				result += "+" + attackDamageModifier + " Attack Damage\n";
			if (magicDamageModifier != 0f)
				result += "+" + magicDamageModifier + " Magic Damage\n";
			if (attackRangeModifier != 0f)
				result += "+" + attackRangeModifier + " Attack Range\n";
			if (attackSpeedModifier != 0f)
				result += "+" + attackSpeedModifier + " Attack Speed\n";
			if (physicalArmorModifier != 0f)
				result += "+" + physicalArmorModifier + " Physical Armor\n";
			if (magicArmorModifier != 0f)
				result += "+" + magicArmorModifier + " Magic Armor\n";
			result += "</style>";
			return result;
		}
		
		public void Equip() {
			PlayerAttributes playerAttributes = GameObject.FindWithTag("Player").GetComponent<PlayerAttributes>();
			playerAttributes.health.AddModifier(healthModifier);
			playerAttributes.mana.AddModifier(manaModifier);
			playerAttributes.regenAmount.AddModifier(regenModifier);
			playerAttributes.attackDamage.AddModifier(attackDamageModifier);
			playerAttributes.magicDamage.AddModifier(magicDamageModifier);
			playerAttributes.attackRange.AddModifier(attackRangeModifier);
			playerAttributes.attackSpeed.AddModifier(0f - attackSpeedModifier);
			playerAttributes.physicalArmor.AddModifier(physicalArmorModifier);
			playerAttributes.magicArmor.AddModifier(magicArmorModifier);
		}

		public void Unequip() {
			PlayerAttributes playerAttributes = GameObject.FindWithTag("Player").GetComponent<PlayerAttributes>();
			playerAttributes.health.RemoveModifier(healthModifier);
			playerAttributes.mana.RemoveModifier(manaModifier);
			playerAttributes.regenAmount.RemoveModifier(regenModifier);
			playerAttributes.attackDamage.RemoveModifier(attackDamageModifier);
			playerAttributes.magicDamage.RemoveModifier(magicDamageModifier);
			playerAttributes.attackRange.RemoveModifier(attackRangeModifier);
			playerAttributes.attackSpeed.RemoveModifier(0f - attackSpeedModifier);
			playerAttributes.physicalArmor.RemoveModifier(physicalArmorModifier);
			playerAttributes.magicArmor.RemoveModifier(magicArmorModifier);
		}

		public void Use() {
			PlayerAttributes playerAttributes = GameObject.FindWithTag("Player").GetComponent<PlayerAttributes>();
			playerAttributes.Heal(healthHealAmount, manaHealAmount);
		}
	}
}