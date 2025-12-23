using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    private UIItemSlot[] _uiItemSlots;
    private Inventory_Base _inventoryFromPlayer;

    private void Awake() {
        _uiItemSlots = GetComponentsInChildren<UIItemSlot>();

        _inventoryFromPlayer = FindFirstObjectByType<Inventory_Base>();
        _inventoryFromPlayer.OnInventoryChange += UpdateInventorySlots;

        UpdateInventorySlots();
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

    private void OnDisable() {
        _inventoryFromPlayer.OnInventoryChange -= UpdateInventorySlots;
    }
}
