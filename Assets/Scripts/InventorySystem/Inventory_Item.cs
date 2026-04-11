using System;
using UnityEngine;

[Serializable]
public class Inventory_Item
{
    private string _itemId;
    public SO_ItemData itemData;
    public int currentStackSize = 1;

    public ItemModifier[] Modifiers { get; private set; }
    public SO_ItemEffectData itemEffect;

    public Inventory_Item(SO_ItemData itemData) {
        this.itemData = itemData;
        Modifiers = EquipmentData()?.modifiers;
        itemEffect = itemData.itemEffect;

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
}
