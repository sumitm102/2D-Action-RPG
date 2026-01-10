using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIItemSlot : MonoBehaviour, IPointerDownHandler
{
    [field: SerializeField]
    public Inventory_Item ItemInSlot { get; private set; }
    private Inventory_Player _inventory;

    [Header("UI item slot step")]
    [SerializeField] private Image _itemIcon;
    [SerializeField] private TextMeshProUGUI _itemStackSize;

    private void Awake() {
        _inventory = FindAnyObjectByType<Inventory_Player>();
    }

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

    public void OnPointerDown(PointerEventData eventData) {
        if (ItemInSlot == null)
            return;

        _inventory.TryEquipItem(ItemInSlot);
    }
}
