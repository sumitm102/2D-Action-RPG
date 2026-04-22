using UnityEngine;

public class UICraft : MonoBehaviour
{
    private UICraftSlot[] _craftSlots;
    private UICraftListButton[] _craftListButtons;
    private UICraftPreview _uiCraftPreview;

    [SerializeField] private UIItemSlotParent _itemSlotParent;
    private Inventory_Player _playerInventory;


    public void SetupCraftUI(Inventory_Storage storage) {
        _playerInventory = storage.PlayerInventory;
        _playerInventory.OnInventoryChange += UpdateUI;
        UpdateUI();

        _uiCraftPreview = GetComponentInChildren<UICraftPreview>();
        _uiCraftPreview.SetupCraftPreview(storage);
        
        SetupCraftListButton();
    }

    private void SetupCraftListButton() {
        _craftSlots = GetComponentsInChildren<UICraftSlot>();
        _craftListButtons = GetComponentsInChildren<UICraftListButton>();

        foreach(var craftSlot in  _craftSlots)
            craftSlot.gameObject.SetActive(false);

        foreach(var button in _craftListButtons)
            button.SetCraftSlots(_craftSlots);
    }

    private void UpdateUI() => _itemSlotParent.UpdateSlots(_playerInventory.itemList);
}
