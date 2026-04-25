using System.Collections.Generic;
using UnityEngine;

public class UIEquipSlotParent : MonoBehaviour
{
    private UIEquipmentSlot[] _equipSlots;

    public void UpdateEquipmentSlots(List<Inventory_EquipmentSlot> equipmentList) {
        if(_equipSlots == null)
            _equipSlots = GetComponentsInChildren<UIEquipmentSlot>();

        for (int i = 0; i < _equipSlots.Length; i++) {
            var playerEquipmentSlot = equipmentList[i];

            if (!playerEquipmentSlot.HasItem())
                _equipSlots[i].UpdateSlot(null);
            else
                _equipSlots[i].UpdateSlot(playerEquipmentSlot.equippedItem);
        }
    }
}
