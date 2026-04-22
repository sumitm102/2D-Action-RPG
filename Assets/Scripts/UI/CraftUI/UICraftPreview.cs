using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICraftPreview : MonoBehaviour
{
    [Header("Item preview setup")]
    [SerializeField] private Image _itemIcon;
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private TextMeshProUGUI _itemInfo;
    [SerializeField] private TextMeshProUGUI _buttonText;

    private Inventory_Item _itemToCraft;
    private Inventory_Storage _storage;
    private UICraftPreviewSlot[] _craftPreviewSlots;


    public void SetupCraftPreview(Inventory_Storage storage) {
        _storage = storage;

        _craftPreviewSlots = GetComponentsInChildren<UICraftPreviewSlot>();

        foreach(var  slot in _craftPreviewSlots)
            slot.gameObject.SetActive(false);
    }
    public void UpdateCraftPreview(SO_ItemData itemData) {
        _itemToCraft = new Inventory_Item(itemData);

        _itemIcon.sprite = itemData.itemIcon;
        _itemName.text = itemData.itemName;
        _itemInfo.text = _itemToCraft.GetItemInfo();
        UpdateCraftPreviewSlots();
    }


    // Gets called when Craft button is clicked
    public void ConfirmCraft() {
        if(_itemToCraft == null) {
            _buttonText.text = "Pick an item";
            return;
        }

        if(_storage.HasEnoughMaterials(_itemToCraft) && _storage.PlayerInventory.CanAddItem(_itemToCraft)) {
            _storage.ConsumeItems(_itemToCraft);
            _storage.PlayerInventory.AddItem(_itemToCraft);
        }

        UpdateCraftPreviewSlots();
    }

    private void UpdateCraftPreviewSlots() {
        foreach (var slot in _craftPreviewSlots)
            slot.gameObject.SetActive(false);

        for (int i = 0; i < _itemToCraft.itemData.craftRecipe.Length; i++) {
            Inventory_Item requiredItem = _itemToCraft.itemData.craftRecipe[i];
            int availableAmount = _storage.GetAvailableAmountOfItem(requiredItem.itemData);
            int requiredAmount = requiredItem.currentStackSize;

            _craftPreviewSlots[i].gameObject.SetActive(true);
            _craftPreviewSlots[i].SetupPreviewSlot(requiredItem.itemData, availableAmount, requiredAmount);
        }
    }
}
