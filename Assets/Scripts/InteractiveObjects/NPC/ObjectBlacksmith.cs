using UnityEngine;

public class ObjectBlacksmith : ObjectNPC, IInteractable {
    private Animator _anim;
    private static readonly int _blacksmithHash = Animator.StringToHash("IsBlacksmith");

    private Inventory_Player _playerInventory;
    private Inventory_Storage _storage;

    protected override void Awake() {
        base.Awake();
        _anim = GetComponentInChildren<Animator>();
        if(_anim != null )
            _anim.SetBool(_blacksmithHash, true);

        _storage = GetComponent<Inventory_Storage>();
    }
    public void Interact() {
        ui.StorageUI.SetupStorage(_storage, _playerInventory);
        ui.StorageUI.gameObject.SetActive(true);
    }

    protected override void OnTriggerEnter2D(Collider2D collision) {
        base.OnTriggerEnter2D(collision);

        _playerInventory = player.GetComponent<Inventory_Player>();
        _storage.SetStorageInventory(_playerInventory);
    }
}
