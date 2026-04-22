using System;
using System.Text;
using UnityEngine;

[Serializable]
public class Inventory_Item
{
    private string _itemId;
    public SO_ItemData itemData;
    public int currentStackSize = 1;

    public ItemModifier[] Modifiers { get; private set; }
    public SO_ItemEffectData itemEffect;

    public int PurchasingPrice {  get; private set; }
    public float SellingPrice { get; private set; }

    // Since selling price is usually lower than purchasing price
    // This will be used to determine the selling price by being a multiplier to the purchasing price
    private float _sellingPriceMul = 0.5f; 

    public Inventory_Item(SO_ItemData itemData) {
        this.itemData = itemData;
        Modifiers = EquipmentData()?.modifiers;
        itemEffect = itemData.itemEffect;
        PurchasingPrice = itemData.itemPrice;
        SellingPrice = itemData.itemPrice * _sellingPriceMul;

        _itemId = itemData.itemName + " - " + Guid.NewGuid().ToString();
    }

    private SO_EquipmentData EquipmentData() {
        if (itemData is SO_EquipmentData equipment)
            return equipment;

        return null;
    }

    public void AddModifiers(EntityStats playerStats) {
        foreach(var modifier in Modifiers) {
            Stat statToModify = playerStats?.GetStatByType(modifier.statType);
            statToModify.AddModifier(modifier.value, _itemId);
        }
    }

    public void RemoveModifiers(EntityStats playerStats) {
        foreach (var modifier in Modifiers) {
            Stat statToModify = playerStats?.GetStatByType(modifier.statType);
            statToModify.RemoveModifier(_itemId);
        }
    }

    // Subscribes and unsubscribes to an event when damage is taken
    // Also applies effects when event is invoke in the EntityHealth
    public void AddItemEffect(Player player) => itemEffect?.SubscribeToEvent(player);
    public void RemoveItemEffect() => itemEffect?.UnsubscribeToEvent();


    public bool CanAddStack() => currentStackSize < itemData.maxStackSize;
    public void AddStack() => currentStackSize++;
    public void RemoveStack() => currentStackSize--;


    #region Previously in the UIItemTooltip Script
    public string GetItemInfo() {
        if (itemData.itemType == E_ItemType.Material)
            return "Used for Crafting.";

        if (itemData.itemType == E_ItemType.Consumable)
            return itemData.itemEffect.effectDescription;

        StringBuilder sb = new StringBuilder();

        sb.AppendLine("");

        foreach (var mod in Modifiers) {
            string modType = GetStatNameByType(mod.statType);
            string modValue = IsPercentageStat(mod.statType) ? mod.value.ToString() + "%" : mod.value.ToString();
            sb.AppendLine("+ " + modValue + " " + modType);
        }

        if (itemEffect != null) {
            sb.AppendLine("");
            sb.AppendLine("Unique effect:");
            sb.AppendLine(itemEffect.effectDescription);
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
    #endregion
}
