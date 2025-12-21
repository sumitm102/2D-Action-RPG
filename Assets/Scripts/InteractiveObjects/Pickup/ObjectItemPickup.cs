using UnityEngine;

public class ObjectItemPickup : MonoBehaviour
{
    [SerializeField] private SO_ItemData _itemData;

    private SpriteRenderer _spriteRenderer;
    private Inventory_Item _itemToAdd;
    private Inventory_Base _inventory;

    private void Awake() {
        _itemToAdd = new Inventory_Item(_itemData);
    }

    private void OnValidate() {

        if (_itemData == null)
            return;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _itemData.itemIcon;
        gameObject.name = "ObjectItemPickup_" + _itemData.itemName;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        _inventory = collision. GetComponent<Inventory_Base>();

        if (_inventory != null & _inventory.CanAddItem()) {
            _inventory.AddItem(_itemToAdd);
            Destroy(this.gameObject);
        }

    }
}
