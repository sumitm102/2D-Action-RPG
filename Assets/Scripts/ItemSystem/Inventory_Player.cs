using System.Collections.Generic;
using UnityEngine;

public class Inventory_Player : Inventory_Base
{
    private EntityStats _playerStats;
    public List<Inventory_EquipmentSlot> equipList;

    protected override void Awake() {
        base.Awake();

        _playerStats = GetComponent<EntityStats>();
    }

    public void TryEquipItem(Inventory_Item item) {
        Inventory_Item inventoryItem = FindItemInList(item.itemData);
        var matchingSlots = equipList.FindAll(slot => slot.slotType == item.itemData.itemType);

        // Step 1: Try to find empty slot and equip item
        foreach(var slot in matchingSlots) {
            if (!slot.HasItem()) {
                EquipItem(inventoryItem, slot);
                return;
            }


        }
    }

    private void EquipItem(Inventory_Item itemToEquip, Inventory_EquipmentSlot slot) {
        slot.equippedItem = itemToEquip;
        slot.equippedItem.AddModifiers(_playerStats);

        RemoveItem(itemToEquip);
    }
}
