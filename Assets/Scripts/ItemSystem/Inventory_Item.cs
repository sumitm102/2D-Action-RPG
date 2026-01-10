using System;
using UnityEngine;

[Serializable]
public class Inventory_Item
{
    public SO_ItemData itemData;
    public int currentStackSize = 1;

    public ItemModifier[] Modifiers { get; private set; }

    public Inventory_Item(SO_ItemData itemData) {
        this.itemData = itemData;
        Modifiers = EquipmentData()?.modifiers;
    }

    private SO_EquipmentData EquipmentData() {
        if (itemData is SO_EquipmentData equipment)
            return equipment;

        return null;
    }

    public void AddModifiers(EntityStats playerStats) {
        foreach(var modifier in Modifiers) {
            Stat statToModify = playerStats?.GetStatByType(modifier.statType);
            statToModify.AddModifier(modifier.value, itemData.itemName);
        }
    }

    public void RemoveModifiers(EntityStats playerStats) {
        foreach (var modifier in Modifiers) {
            Stat statToModify = playerStats?.GetStatByType(modifier.statType);
            statToModify.RemoveModifier(itemData.itemName);
        }
    }

    public bool CanAddStack() => currentStackSize < itemData.maxStackSize;
    public void AddStack() => currentStackSize++;
    public void RemoveStack() => currentStackSize--;
}
