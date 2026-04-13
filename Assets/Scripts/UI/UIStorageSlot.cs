using UnityEngine;
using UnityEngine.EventSystems;

public class UIStorageSlot : UIItemSlot
{
    public enum E_StorageSlotType { StorageSlot, PlayerInventorySlot }
    public E_StorageSlotType slotType;
    private Inventory_Storage _storage;


    public void SetStorage(Inventory_Storage storage) => _storage = storage;

    public override void OnPointerDown(PointerEventData eventData) {
        if (ItemInSlot == null)
            return;

        if (slotType == E_StorageSlotType.StorageSlot)
            _storage.FromStorageToPlayer(ItemInSlot);

        else if (slotType == E_StorageSlotType.PlayerInventorySlot)
            _storage.FromPlayerToStorage(ItemInSlot);

        ui.ItemTooltip.ShowTooltip(false, null);
    }
}
