    using UnityEngine;

public class UIStorage : MonoBehaviour
{
    private Inventory_Storage _storage;
    private Inventory_Player _playerInventory;

    [SerializeField] private UIItemSlotParent _playerInventoryParent;
    [SerializeField] private UIItemSlotParent _storageParent;
    [SerializeField] private UIItemSlotParent _materialStashParent;

    public void SetupStorage(Inventory_Storage storage, Inventory_Player playerInventory) {
        _storage = storage;
        _playerInventory = playerInventory;
        _storage.OnInventoryChange += UpdateUI;
        UpdateUI();

        UIStorageSlot[] storageSlot = GetComponentsInChildren<UIStorageSlot>();

        foreach(var slot in storageSlot)
            slot.SetStorage(_storage);
    }

    private void UpdateUI() {
        _playerInventoryParent.UpdateSlots(_playerInventory.itemList);
        _storageParent.UpdateSlots(_storage.itemList);
        _materialStashParent.UpdateSlots(_storage.materialStashList);
    }
}
