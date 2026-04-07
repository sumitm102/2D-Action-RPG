using System.Linq.Expressions;
using System.Text;
using TMPro;
using UnityEngine;

public class UIItemTooltip : UITooltip
{
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private TextMeshProUGUI _itemType;
    [SerializeField] private TextMeshProUGUI _itemInfo;

    public void ShowTooltip(bool show, RectTransform targetRect, Inventory_Item itemToShow) {
        base.ShowTooltip(show, targetRect);

        _itemName.text = itemToShow.itemData.itemName;
        _itemType.text = itemToShow.itemData.itemType.ToString();
        _itemInfo.text = GetItemInfo(itemToShow);
    }

    public string GetItemInfo(Inventory_Item item) {
        if (item.itemData.itemType == E_ItemType.Material)
            return "Used for Crafting.";

        if (item.itemData.itemType == E_ItemType.Consumable)
            return item.itemData.itemEffect.effectDescription;

        StringBuilder sb = new StringBuilder();

        sb.AppendLine("");

        foreach(var mod in item.Modifiers) {
            string modType = GetStatNameByType(mod.statType);
            string modValue = IsPercentageStat(mod.statType) ? mod.value.ToString() + "%" : mod.value.ToString();
            sb.AppendLine("+ " + modValue + " " + modType);
        }

        return sb.ToString();
    }

    private string GetStatNameByType(E_StatType statType) {
        switch (statType) {
            case E_StatType.MaxHealth: return "Max Health";
            case E_StatType.HealthRegen: return "Health Regeneration";
            case E_StatType.Strength: return "Strength";
            case E_StatType.Agility: return "Agility";
            case E_StatType.Intelligence: return "Intelligence";
            case E_StatType.Vitality: return "Vitality";
            case E_StatType.AttackSpeed: return "Attack Speed";
            case E_StatType.Damage: return "Damage";
            case E_StatType.CritChance: return "Critical Chance";
            case E_StatType.CritPower: return "Critical Power";
            case E_StatType.ArmorReduction: return "Armor Reduction";
            case E_StatType.FireDamage: return "Fire Damage";
            case E_StatType.IceDamage: return "Ice Damage";
            case E_StatType.LightningDamage: return "Lightning Damage";
            case E_StatType.Armor: return "Armor";
            case E_StatType.Evasion: return "Evasion";
            case E_StatType.FireResistance: return "Fire Resistance";
            case E_StatType.IceResistance: return "Ice Resistance";
            case E_StatType.LightningResistance: return "Lightning Resistance";
            default: return "Unknown Stat";
        }
    }

    private bool IsPercentageStat(E_StatType statType) {
        switch (statType) {
            case E_StatType.CritChance:
            case E_StatType.CritPower:
            case E_StatType.ArmorReduction:
            case E_StatType.FireResistance:
            case E_StatType.IceResistance:
            case E_StatType.LightningResistance:
            case E_StatType.Evasion:
            case E_StatType.AttackSpeed:
                return true;
            default:
                return false;
        }
    }
}
