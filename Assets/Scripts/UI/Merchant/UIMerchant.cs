using UnityEngine;

public class UIMerchant : MonoBehaviour
{
    private Inventory_Player _playerInventory;
    private Inventory_Merchant _merchantInventory;

    [SerializeField] private UIItemSlotParent _merchantInventorySlots;
    [SerializeField] private UIItemSlotParent _playerInventorySlots;
    [SerializeField] private UIEquipSlotParent _equipSlots;

    public void SetupMerchantUI(Inventory_Merchant merchantInventory, Inventory_Player playerInventory) {
        _merchantInventory = merchantInventory;
        _playerInventory = playerInventory;

        this._playerInventory.OnInventoryChange += UpdateSlotUI;
        UpdateSlotUI();


        UIMerchantSlot[] merchantSlots = GetComponentsInChildren<UIMerchantSlot>();
        foreach (var slot in merchantSlots)
            slot.SetupMerchantUI(_merchantInventory);
    }

    private void UpdateSlotUI() {
        if(_playerInventory ==  null)
            return;

        _playerInventorySlots.UpdateSlots(_playerInventory.itemList);
        _merchantInventorySlots.UpdateSlots(_merchantInventory.itemList);
        _equipSlots.UpdateEquipmentSlots(_playerInventory.equipList);
    }

    private void OnDisable() {
        _merchantInventory.OnInventoryChange -= UpdateSlotUI;
    }
}
