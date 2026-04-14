using System.Collections.Generic;
using UnityEngine;

public class Inventory_Storage : Inventory_Base
{
    private Inventory_Player _playerInventory;

    public List<Inventory_Item> materialStashList; // For storing extra materials in the material stash

    public void SetStorageInventory(Inventory_Player inventoryPlayer) => this._playerInventory = inventoryPlayer;

    public void AddMaterialToStash(Inventory_Item itemToAdd) {
        var stackableItem = FindStackableInStash(itemToAdd);

        if (stackableItem != null)
            stackableItem.AddStack();
        else
            materialStashList.Add(itemToAdd);

        TriggerUIUpdate();
    }

    public Inventory_Item FindStackableInStash(Inventory_Item itemToAdd) {
        List<Inventory_Item> stackableItems = materialStashList.FindAll(item => item.itemData == itemToAdd.itemData);

        foreach(var stackableItem in stackableItems) 
            if(stackableItem.CanAddStack())
                return stackableItem;
        
        return null;
    }

    public void FromPlayerToStorage(Inventory_Item item, bool shouldTransferFullStack) {
        int transferAmount = shouldTransferFullStack ? item.currentStackSize : 1;

        for (int i = 0; i < transferAmount; i++) {
            if (CanAddItem(item)) {

                // This is created so that the storage and player inventory don't have reference to the same item that was 
                // removed and added since previously they displayed that same information on both slots
                var itemToTransfer = new Inventory_Item(item.itemData);

                _playerInventory.RemoveOneItem(item); // Remove the intended item from the player's inventory

                AddItem(itemToTransfer); // Add the newly created item to the storage's inventory
            }
        }

        TriggerUIUpdate();
    }

    public void FromStorageToPlayer(Inventory_Item item, bool shouldTransferFullStack) {
        int transferAmount = shouldTransferFullStack ? item.currentStackSize : 1;

        for (int i = 0; i < transferAmount; i++) {
            if (_playerInventory.CanAddItem(item)) {
                var itemToTransfer = new Inventory_Item(item.itemData);
                RemoveOneItem(item);
                _playerInventory.AddItem(itemToTransfer);
            }
        }

        TriggerUIUpdate();
    }
}
