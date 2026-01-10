using System;
using UnityEngine;

[Serializable]
public class Inventory_EquipmentSlot
{
    public E_ItemType slotType;
    public Inventory_Item equippedItem;

    public bool HasItem() => equippedItem != null && equippedItem.itemData != null;
}
