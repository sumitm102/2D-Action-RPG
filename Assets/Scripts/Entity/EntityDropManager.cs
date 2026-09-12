using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityDropManager : MonoBehaviour
{
    [SerializeField] private GameObject _itemDropPrefab;
    [SerializeField] private SO_ItemListData _dropListData;

    [Header("Drop restrictions")]
    [SerializeField] private int _maxRarityAmount = 1200;
    [SerializeField] private int _maxItemsToDrop = 3;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.X))
            DropItems();
    }


    public virtual void DropItems() {
        List<SO_ItemData> itemsToDropList = RollDrops();
        int amountToDrop = Mathf.Min(itemsToDropList.Count, _maxItemsToDrop);

        for (int i = 0; i < amountToDrop; i++) {
            CreateItemDrop(itemsToDropList[i]);
        }
    }

    protected void CreateItemDrop(SO_ItemData itemToDrop) {
        GameObject newItem = Instantiate(_itemDropPrefab, transform.position, Quaternion.identity);

        if(newItem.TryGetComponent<ObjectItemPickup>(out var itemPickup))
            itemPickup.SetupItem(itemToDrop);
    }

    public List<SO_ItemData> RollDrops() {
        List<SO_ItemData> possibleDropList = new List<SO_ItemData>();
        List<SO_ItemData> finalDropList = new List<SO_ItemData>();
        float maxRarityAmount = _maxRarityAmount;

        // Roll each item based on rarity and max drop chance
        foreach(var item in _dropListData.itemList) {
            float dropChance = item.GetDropChance();

            if(Random.Range(0, 100) <= dropChance )
                possibleDropList.Add(item);
        }

        // Sort by rarity (highest to lowest)
        possibleDropList = possibleDropList.OrderByDescending(item => item.itemRarity).ToList();

        // Add items to final drop list until rarity limit on entity is reached
        foreach(var item in possibleDropList)
            if(maxRarityAmount > item.itemRarity) {
                finalDropList.Add(item);
                maxRarityAmount -= item.itemRarity;
            }

        return finalDropList;
    }
}
