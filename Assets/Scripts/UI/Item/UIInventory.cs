using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    private Inventory_Player _inventoryFromPlayer;
    private UIItemSlot[] _uiItemSlots;
    private UIEquipmentSlot[] _uiEquipmentSlots;

    [SerializeField] private Transform _uiItemSlotParent;
    [SerializeField] private Transform _uiEquipmentSlotParent;

    private void Awake() {
        _uiItemSlots = _uiItemSlotParent.GetComponentsInChildren<UIItemSlot>();
        _uiEquipmentSlots = _uiEquipmentSlotParent.GetComponentsInChildren<UIEquipmentSlot>();

        _inventoryFromPlayer = FindFirstObjectByType<Inventory_Player>();
        _inventoryFromPlayer.OnInventoryChange += UpdateUI;

        UpdateUI();
    }

    private void UpdateUI() {
        UpdateInventorySlots();
        UpdateEquipmentSlots();
    }

    private void UpdateEquipmentSlots() {
        List<Inventory_EquipmentSlot> playerEquipmentList = _inventoryFromPlayer.equipList;

        for (int i = 0; i < _uiEquipmentSlots.Length; i++) {
            var playerEquipmentSlot = playerEquipmentList[i];

            if (!playerEquipmentSlot.HasItem()) 
                _uiEquipmentSlots[i].UpdateSlot(null);
            else
                _uiEquipmentSlots[i].UpdateSlot(playerEquipmentSlot.equippedItem);
        }
    }

    private void UpdateInventorySlots() {
        List<Inventory_Item> itemList = _inventoryFromPlayer.itemList;

        for (int i = 0; i < _uiItemSlots.Length; i++) {

            if (i < itemList.Count) 
                _uiItemSlots[i].UpdateSlot(itemList[i]);
            
            else 
                _uiItemSlots[i].UpdateSlot(null);
            
        }
    }

    // Not using on disable method here since it unsubsribes everytime player equips items when inventory ui is set off
    private void OnDestroy() {
        _inventoryFromPlayer.OnInventoryChange -= UpdateUI;
    }
}
