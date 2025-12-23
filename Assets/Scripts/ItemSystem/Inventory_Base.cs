using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Base : MonoBehaviour
{
    public int maxInventorySize = 10;
    public List<Inventory_Item> itemList = new List<Inventory_Item>();

    public event Action OnInventoryChange;

    public bool CanAddItem() => itemList.Count < maxInventorySize;

    public void AddItem(Inventory_Item itemToAdd) {

        Inventory_Item itemInInventory = FindItemInList(itemToAdd.itemData);
        if(itemInInventory != null)
            itemInInventory.AddStack();
        else
            itemList.Add(itemToAdd);


        OnInventoryChange?.Invoke();
    }

    public Inventory_Item FindItemInList(SO_ItemData itemData) {
        return itemList.Find(item => item.itemData == itemData && item.CanAddStack());
    }
}
