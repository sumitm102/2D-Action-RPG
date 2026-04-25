using System.Linq.Expressions;
using System.Text;
using TMPro;
using UnityEngine;

public class UIItemTooltip : UITooltip
{
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private TextMeshProUGUI _itemType;
    [SerializeField] private TextMeshProUGUI _itemInfo;

    [SerializeField] private TextMeshProUGUI _itemPrice;
    [SerializeField] private Transform _merchantInfo;


    public void ShowTooltip(bool show, RectTransform targetRect, Inventory_Item itemToShow,
        bool showPurchasingPrice = false, bool showMerchantInfo = false) {
        base.ShowTooltip(show, targetRect);

        _merchantInfo.gameObject.SetActive(showMerchantInfo);
        int price = showPurchasingPrice ? itemToShow.PurchasingPrice: Mathf.FloorToInt(itemToShow.SellingPrice);
        int totalPrice = price * itemToShow.currentStackSize;

        string fullStackPrice = ($"Price: {price}x{itemToShow.currentStackSize} - {totalPrice}g.");
        string singleStackPrice = ($"Price: {price}g.");

        _itemName.text = itemToShow.itemData.itemName;
        _itemType.text = itemToShow.itemData.itemType.ToString();
        _itemInfo.text = itemToShow.GetItemInfo();
        _itemPrice.text = itemToShow.currentStackSize > 1 ? fullStackPrice : singleStackPrice;
    }
}
