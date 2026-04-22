    using UnityEngine;

public class UIStorage : MonoBehaviour
{
    private Inventory_Storage _storage;
    private Inventory_Player _playerInventory;

    [SerializeField] private UIItemSlotParent _playerInventoryParent;
    [SerializeField] private UIItemSlotParent _storageParent;
    [SerializeField] private UIItemSlotParent _materialStashParent;

    public void SetupStorageUI(Inventory_Storage storage) {
        _storage = storage;
        _playerInventory = storage.PlayerInventory;
        _storage.OnInventoryChange += UpdateUI;
        UpdateUI();

        UIStorageSlot[] storageSlot = GetComponentsInChildren<UIStorageSlot>();

        foreach(var slot in storageSlot)
            slot.SetStorage(_storage);
    }


    private void UpdateUI() {
        if(_storage == null)
            return;

        _playerInventoryParent.UpdateSlots(_playerInventory.itemList);
        _storageParent.UpdateSlots(_storage.itemList);
        _materialStashParent.UpdateSlots(_storage.materialStashList);
    }

    private void OnEnable() {
        UpdateUI();
    }
}
