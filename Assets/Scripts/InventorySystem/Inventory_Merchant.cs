using System.Collections.Generic;
using UnityEngine;

public class Inventory_Merchant : Inventory_Base
{
    private Inventory_Player _playerInventory;

    [Header("Shop item details")]
    [SerializeField] private SO_ItemListData _shopItemListData;
    [SerializeField] private int _minItemAmount = 4;

    public void SetInventory(Inventory_Player playerInventory) => _playerInventory = playerInventory;

    protected override void Awake() {
        base.Awake();
        FillShopList();
    }

    #region Purchasing and Selling methods

    public void TryPurchasingItem(Inventory_Item itemToPurchase, bool purchaseFullStack) {
        int amountToPurchase = purchaseFullStack ? itemToPurchase.currentStackSize : 1;

        for (int i = 0; i < amountToPurchase; i++) {
            if(_playerInventory.GoldCurrency < itemToPurchase.currentStackSize) {
                Debug.Log("Not enough currency!");
                return;
            }

            // If purchasing item is of type material, add it to material stash
            // Otherwise add it to player inventory
            if(itemToPurchase.itemData.itemType == E_ItemType.Material)
                _playerInventory.StorageInventory.AddMaterialToStash(itemToPurchase);
            
            else {
                if (_playerInventory.CanAddItem(itemToPurchase)) {

                    // Creating a new instance of the same item so they don't have same reference
                    var itemToAdd = new Inventory_Item(itemToPurchase.itemData);
                    _playerInventory.AddItem(itemToAdd); // Adds the item to the player inventory
                }
            }

            _playerInventory.SubtractFromCurrency(itemToPurchase.PurchasingPrice);
            RemoveOneItem(itemToPurchase); // Removes the item from the merchant inventory
        }

        TriggerUIUpdate();
    }

    public void TrySellingItem(Inventory_Item itemToSell, bool sellFullStack) {
        int amountToSell = sellFullStack ? itemToSell.currentStackSize : 1;

        for (int i = 0; i < amountToSell; i++) {
            int sellingPrice = Mathf.FloorToInt(itemToSell.SellingPrice);

            _playerInventory.AddToCurrency(sellingPrice);
            _playerInventory.RemoveOneItem(itemToSell);
        }

        TriggerUIUpdate();
    }

    #endregion


    public void FillShopList() {
        itemList.Clear(); // Refreshes shop list

        List<Inventory_Item> possibleItemsList = new List<Inventory_Item>();

        foreach(var itemData in _shopItemListData.itemList) {
            int randomizedStack = Random.Range(itemData.minStackSizeAtShop, itemData.maxStackSizeAtShop + 1);
            int finalStack = Mathf.Clamp(randomizedStack, 1, itemData.maxStackSize);

            Inventory_Item itemToAdd = new Inventory_Item(itemData);
            itemToAdd.currentStackSize = finalStack;

            possibleItemsList.Add(itemToAdd);
        }

        int randomItemAmount = Random.Range(_minItemAmount, maxInventorySize + 1);
        int finalAmount = Mathf.Clamp(randomItemAmount, 1, possibleItemsList.Count);

        for (int i = 0; i < finalAmount; i++) {
            int randomIndex = Random.Range(0, possibleItemsList.Count);
            var item = possibleItemsList[randomIndex];

            if(CanAddItem(item)) {
                possibleItemsList.Remove(item);
                AddItem(item);
            }
        }

        TriggerUIUpdate();
    }


    
}
