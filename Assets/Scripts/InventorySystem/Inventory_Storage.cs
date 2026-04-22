using System.Collections.Generic;
using UnityEngine;

public class Inventory_Storage : Inventory_Base
{
    public Inventory_Player PlayerInventory { get; private set; }

    public List<Inventory_Item> materialStashList; // For storing extra materials in the material stash


    public void ConsumeItems(Inventory_Item itemToCraft) {
        foreach(var requiredItem in itemToCraft.itemData.craftRecipe) {
            int amountToConsume = requiredItem.currentStackSize;
            amountToConsume -= ConsumedItemsAmount(PlayerInventory.itemList, requiredItem);

            if (amountToConsume > 0)
                amountToConsume -= ConsumedItemsAmount(itemList, requiredItem);

            if(amountToConsume > 0)
                amountToConsume -= ConsumedItemsAmount(materialStashList, requiredItem);
        }
    }

    private int ConsumedItemsAmount(List<Inventory_Item> itemList, Inventory_Item requiredItem) {
        int requiredAmount = requiredItem.currentStackSize;
        int consumedAmount = 0;

        foreach(var item in itemList) {
            if (item.itemData != requiredItem.itemData)
                continue;

            int removedAmount = Mathf.Min(item.currentStackSize, requiredAmount - consumedAmount);
            item.currentStackSize -= removedAmount;
            consumedAmount += removedAmount;

            if(item.currentStackSize <= 0)
                itemList.Remove(item);

            if (consumedAmount >= removedAmount)
                break;
        }

        return consumedAmount;
    }

    public bool HasEnoughMaterials(Inventory_Item itemToCraft) {
        foreach(var requiredItem in itemToCraft.itemData.craftRecipe) 
            if (GetAvailableAmountOfItem(requiredItem.itemData) < requiredItem.currentStackSize)
                return false;
        
        return true;
    }

    public void SetStorageInventory(Inventory_Player inventoryPlayer) => this.PlayerInventory = inventoryPlayer;

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

                PlayerInventory.RemoveOneItem(item); // Remove the intended item from the player's inventory

                AddItem(itemToTransfer); // Add the newly created item to the storage's inventory
            }
        }

        TriggerUIUpdate();
    }

    public void FromStorageToPlayer(Inventory_Item item, bool shouldTransferFullStack) {
        int transferAmount = shouldTransferFullStack ? item.currentStackSize : 1;

        for (int i = 0; i < transferAmount; i++) {
            if (PlayerInventory.CanAddItem(item)) {
                var itemToTransfer = new Inventory_Item(item.itemData);
                RemoveOneItem(item);
                PlayerInventory.AddItem(itemToTransfer);
            }
        }

        TriggerUIUpdate();
    }


    // Called in UICraftPreviewMaterialSlot script when looking at the craft screen
    public int GetAvailableAmountOfItem(SO_ItemData requiredItem) {
        int amount = 0;

        // Going Through player inventory, storage, and material stash to find the total available amount of an item
        foreach(var item in PlayerInventory.itemList)
            if (item.itemData == requiredItem)
                amount += item.currentStackSize;

        foreach(var item in itemList)
            if(item.itemData == requiredItem)
                amount += item.currentStackSize;

        foreach(var item in materialStashList)
            if(item.itemData == requiredItem)
                amount += item.currentStackSize;
        
        return amount;
        
    }
}
