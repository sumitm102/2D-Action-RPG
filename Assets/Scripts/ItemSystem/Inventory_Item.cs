using System;
using UnityEngine;

[Serializable]
public class Inventory_Item
{
    public SO_ItemData itemData;
    public int currentStackSize = 1;

    public Inventory_Item(SO_ItemData itemData) {
        this.itemData = itemData;
    }

    public bool CanAddStack() => currentStackSize < itemData.maxStackSize;
    public void AddStack() => currentStackSize++;
    public void RemoveStack() => currentStackSize--;
}
