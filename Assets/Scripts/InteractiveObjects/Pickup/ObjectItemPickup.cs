using UnityEngine;

public class ObjectItemPickup : MonoBehaviour
{
    [SerializeField] private SO_ItemData _itemData;

    private SpriteRenderer _spriteRenderer;
    //private Inventory_Item _itemToAdd;
    //private Inventory_Base _inventory;

    //private void Awake() {
    //    _itemToAdd = new Inventory_Item(_itemData);
    //}

    private void OnValidate() {

        if (_itemData == null)
            return;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _itemData.itemIcon;
        gameObject.name = "ObjectItemPickup_" + _itemData.itemName;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Inventory_Item itemToAdd = new Inventory_Item(_itemData);
        Inventory_Player playerInventory = collision.GetComponent<Inventory_Player>();
        Inventory_Storage storageInventory = playerInventory.StorageInventory;

        if(_itemData.itemType == E_ItemType.Material) {
            storageInventory.AddMaterialToStash(itemToAdd);
            Destroy(this.gameObject);
            return;
        }

        if (playerInventory.CanAddItem(itemToAdd)) {
            playerInventory.AddItem(itemToAdd);
            Destroy(this.gameObject);
        }

    }
}
