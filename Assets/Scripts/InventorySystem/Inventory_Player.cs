using System.Collections.Generic;
using UnityEngine;

public class Inventory_Player : Inventory_Base
{
    private Player _player;
    public List<Inventory_EquipmentSlot> equipList;

    public Inventory_Storage StorageInventory { get; private set; }

    [field: SerializeField] public int GoldCurrency { get; private set; } = 10000;

    protected override void Awake() {
        base.Awake();

        _player = GetComponent<Player>();
        StorageInventory = FindFirstObjectByType<Inventory_Storage>();
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

        // Step 2: No empty slots? Replace first one
        var slotToReplace = matchingSlots[0];
        var itemToUnequip = slotToReplace.equippedItem;

        UnequipItem(itemToUnequip, slotToReplace != null);
        EquipItem(inventoryItem, slotToReplace);
    }

    private void EquipItem(Inventory_Item itemToEquip, Inventory_EquipmentSlot slot) {
        float savedHPPercent = _player.Health.GetHPPercent();

        slot.equippedItem = itemToEquip;
        slot.equippedItem.AddModifiers(_player.Stats);
        slot.equippedItem.AddItemEffect(_player);

        // This is to make sure hp bar stays the same even when max health changes
        _player.Health.SetHPToPercent(savedHPPercent);

        RemoveOneItem(itemToEquip);
    }

    public void UnequipItem(Inventory_Item itemToUnequip, bool isReplacingItem = false) {
        if (!CanAddItem(itemToUnequip) && !isReplacingItem) {
            Debug.Log("No space left");
            return;
        }

        float savedHPPercent = _player.Health.GetHPPercent();

        var slotToUnequip = equipList.Find(slot => slot.equippedItem == itemToUnequip);
        if (slotToUnequip != null)
            slotToUnequip.equippedItem = null;

        // Performs the same functionality as above
        //foreach (var slot in equipList) {
        //    if (slot.equippedItem == itemToUnequip) {
        //        slot.equippedItem = null;
        //        break;
        //    }
        //}

        itemToUnequip.RemoveModifiers(_player.Stats);
        itemToUnequip.RemoveItemEffect();

        // This is to make sure hp bar stays the same even when max health changes
        _player.Health.SetHPToPercent(savedHPPercent);

        AddItem(itemToUnequip);


    }

    public void AddToCurrency(int priceToAdd) => GoldCurrency += priceToAdd;
    public void SubtractFromCurrency(int priceToSubtract) => GoldCurrency -= priceToSubtract;
}
