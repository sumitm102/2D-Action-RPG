using UnityEngine;

public class ObjectBlacksmith : ObjectNPC, IInteractable {
    private Animator _anim;
    private static readonly int _blacksmithHash = Animator.StringToHash("IsBlacksmith");
    private bool _toggleStorageUI;

    private Inventory_Player _playerInventory;
    private Inventory_Storage _storage;

    protected override void Awake() {
        base.Awake();
        _anim = GetComponentInChildren<Animator>();
        if(_anim != null )
            _anim.SetBool(_blacksmithHash, true);

        _toggleStorageUI = false;
        _storage = GetComponent<Inventory_Storage>();
    }
    public void Interact() {
        ui.StorageUI.SetupStorage(_storage, _playerInventory);

        _toggleStorageUI = !_toggleStorageUI;
        ui.StorageUI.gameObject.SetActive(_toggleStorageUI);
    }

    protected override void OnTriggerEnter2D(Collider2D collision) {
        base.OnTriggerEnter2D(collision);

        _playerInventory = player.GetComponent<Inventory_Player>();
        _storage.SetStorageInventory(_playerInventory);
    }

    protected override void OnTriggerExit2D(Collider2D collision) {
        base.OnTriggerExit2D(collision);

        ui.SwitchOffAllTooltips();
        ui.StorageUI.gameObject.SetActive(false);
    }
}
