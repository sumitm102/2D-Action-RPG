using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIItemSlot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [field: SerializeField]
    public Inventory_Item ItemInSlot { get; private set; }
    protected Inventory_Player inventory;
    protected UI ui;
    protected RectTransform rect;

    [Header("UI item slot step")]
    [SerializeField] private Image _itemIcon;
    [SerializeField] private TextMeshProUGUI _itemStackSize;

    protected void Awake() {
        inventory = FindAnyObjectByType<Inventory_Player>();
        ui = GetComponentInParent<UI>();
        rect = GetComponent<RectTransform>();
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

    public virtual void OnPointerDown(PointerEventData eventData) {
        if (ItemInSlot == null || ItemInSlot.itemData.itemType == E_ItemType.Material)
            return;


        if (ItemInSlot.itemData.itemType == E_ItemType.Consumable) {
            if (!ItemInSlot.itemEffect.CanBeUsed())
                return;

            inventory.TryUseItem(ItemInSlot);
        }
        else
            inventory.TryEquipItem(ItemInSlot);


        if(ItemInSlot == null)
            ui.ItemTooltip.ShowTooltip(false, null);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if(ItemInSlot == null) 
            return;

        ui.ItemTooltip.ShowTooltip(true, rect, ItemInSlot);
    }

    public void OnPointerExit(PointerEventData eventData) {
        ui.ItemTooltip.ShowTooltip(false, null);
    }
}
