using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class UIItemSlotParent : MonoBehaviour
{
    private UIItemSlot[] _uiItemSlots;

    public void UpdateSlots(List<Inventory_Item> itemList) {
        if (_uiItemSlots == null)
            _uiItemSlots = GetComponentsInChildren<UIItemSlot>();

        for(int i = 0; i < _uiItemSlots.Length; i++) {
            if (i < itemList.Count)
                _uiItemSlots[i].UpdateSlot(itemList[i]);
            else
                _uiItemSlots[i].UpdateSlot(null);
        }

    }
}
