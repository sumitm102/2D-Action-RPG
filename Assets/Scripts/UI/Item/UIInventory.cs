using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    private Inventory_Player _playerInventory;

    [SerializeField] private UIItemSlotParent _uiInventorySlotParent;
    [SerializeField] private UIEquipSlotParent _equipSlotParent;

    private void Awake() {

        _playerInventory = FindFirstObjectByType<Inventory_Player>();
        _playerInventory.OnInventoryChange += UpdateUI;

        UpdateUI();
    }

    private void UpdateUI() {
        _uiInventorySlotParent.UpdateSlots(_playerInventory.itemList);
        _equipSlotParent.UpdateEquipmentSlots(_playerInventory.equipList);
    }

    // Not using on disable method here since it unsubsribes everytime player equips items when inventory ui is set off
    private void OnDestroy() {
        _playerInventory.OnInventoryChange -= UpdateUI;
    }
}
