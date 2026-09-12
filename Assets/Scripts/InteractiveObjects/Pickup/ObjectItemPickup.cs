using UnityEngine;

public class ObjectItemPickup : MonoBehaviour
{
    [SerializeField] private Vector2 _dropForce = new Vector2(3f, 10f);
    [SerializeField] private SO_ItemData _itemData;
    [Header("Components")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Collider2D _collider;

    private void OnValidate() {

        if (_itemData == null)
            return;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        SetupVisuals();
        
    }

    public void SetupItem(SO_ItemData itemData) {
        _itemData = itemData;
        SetupVisuals();

        float horizontalDropForce = Random.Range(-_dropForce.x, _dropForce.x);
        _rb.linearVelocity = new Vector2(horizontalDropForce, _dropForce.y);
        _collider.isTrigger = false;
    }

    private void SetupVisuals() {
        _spriteRenderer.sprite = _itemData.itemIcon;
        gameObject.name = "ObjectItemPickup_" + _itemData.itemName;

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && !_collider.isTrigger) {
            _collider.isTrigger = true;
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Inventory_Player playerInventory = collision.GetComponent<Inventory_Player>();

        // Checing if the layer is of player or not
        if(playerInventory == null)
            return;

        Inventory_Item itemToAdd = new Inventory_Item(_itemData);
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
