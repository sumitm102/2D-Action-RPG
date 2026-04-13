using UnityEngine;

public class Inventory_Storage : Inventory_Base
{
    private Inventory_Player _playerInventory;
    public void SetStorageInventory(Inventory_Player inventoryPlayer) => this._playerInventory = inventoryPlayer;

    public void FromPlayerToStorage(Inventory_Item item) {
        if (CanAddItem(item)) {
            _playerInventory.RemoveItem(item); // Remove it from the player's inventory
            AddItem(item); // Add it to the storage's inventory
        }

        TriggerUIUpdate();
    }

    public void FromStorageToPlayer(Inventory_Item item) {
        if (_playerInventory.CanAddItem(item)) {
            RemoveItem(item);
            _playerInventory.AddItem(item);
        }

        TriggerUIUpdate();
    }
}
