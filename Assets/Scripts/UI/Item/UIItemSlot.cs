using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemSlot : MonoBehaviour
{
    public Inventory_Item ItemInSlot { get; private set; }

    [Header("UI item slot step")]
    [SerializeField] private Image _itemIcon;
    [SerializeField] private TextMeshProUGUI _itemStackSize;

    public void UpdateSlot(Inventory_Item item) {
        ItemInSlot = item;

        if (ItemInSlot == null) {
            _itemIcon.color = Color.clear;
            _itemStackSize.text = "";

            return;
        }

        Color color = Color.white;
        color.a = 0.9f;

        _itemIcon.color = color;
        _itemIcon.sprite = ItemInSlot.itemData.itemIcon;

        _itemStackSize.text = ItemInSlot.currentStackSize > 1 ? ItemInSlot.currentStackSize.ToString() : "";
    }

}
