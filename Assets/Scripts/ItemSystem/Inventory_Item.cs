using System;
using UnityEngine;

[Serializable]
public class Inventory_Item
{
    public SO_ItemData itemData;

    public Inventory_Item(SO_ItemData itemData) {
        this.itemData = itemData;
    }
}
