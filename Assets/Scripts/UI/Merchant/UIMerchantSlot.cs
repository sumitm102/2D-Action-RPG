using UnityEngine;
using UnityEngine.EventSystems;

public class UIMerchantSlot : UIItemSlot
{
    private Inventory_Merchant _merchantInventory;
    public enum MerchantSlotType {
        MerchantSlot,
        PlayerSlot
    }

    public MerchantSlotType slotType;

    public override void OnPointerDown(PointerEventData eventData) {
        if (ItemInSlot == null)
            return;

        bool leftMouseClick = eventData.button == PointerEventData.InputButton.Left;
        bool rightMouseClick = eventData.button == PointerEventData.InputButton.Right;

        if (slotType == MerchantSlotType.PlayerSlot) {
            if (rightMouseClick) {
                bool sellFullStack = Input.GetKey(KeyCode.LeftControl);
                _merchantInventory.TrySellingItem(ItemInSlot, sellFullStack);
            }
            else if (leftMouseClick) {
                base.OnPointerDown(eventData);
            }
        }
        else if(slotType == MerchantSlotType.MerchantSlot) {
            if (leftMouseClick)
                return;

            bool purchaseFullStack = Input.GetKey(KeyCode.LeftControl);
            _merchantInventory.TryPurchasingItem(ItemInSlot, purchaseFullStack);


        }

        ui.ItemTooltip.ShowTooltip(false, null);
    }

    public override void OnPointerEnter(PointerEventData eventData) {
        if(ItemInSlot == null) 
            return;

        if (slotType == MerchantSlotType.MerchantSlot)
            ui.ItemTooltip.ShowTooltip(true, rect, ItemInSlot, true, true);
        else 
            ui.ItemTooltip.ShowTooltip(true, rect, ItemInSlot, false, true);
    }

    public void SetupMerchantUI(Inventory_Merchant merchantInventory) => _merchantInventory = merchantInventory;
}
