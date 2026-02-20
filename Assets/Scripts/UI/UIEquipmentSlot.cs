using UnityEngine;
using UnityEngine.EventSystems;

public class UIEquipmentSlot : UIItemSlot
{
    public E_ItemType slotType;

    private void OnValidate() {
        this.gameObject.name = "UIEquipmentSlot - " + slotType.ToString();
    }

    public override void OnPointerDown(PointerEventData eventData) {
        if (ItemInSlot == null)
            return;

        inventory.UnequipItem(ItemInSlot);
    }
}
