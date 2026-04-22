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
        ui.StorageUI.SetupStorageUI(_storage);
        ui.CraftUI.SetupCraftUI(_storage);

        ui.StorageUI.gameObject.SetActive(toggleUI);
        toggleUI = !toggleUI;

        if(ui.CraftUI.gameObject.activeInHierarchy)
            ui.CraftUI.gameObject.SetActive(false);
    }

    protected override void OnTriggerEnter2D(Collider2D collision) {
        base.OnTriggerEnter2D(collision);

        _playerInventory = player.GetComponent<Inventory_Player>();
        _storage.SetStorageInventory(_playerInventory);
    }

    protected override void OnTriggerExit2D(Collider2D collision) {
        base.OnTriggerExit2D(collision);

        ui.SwitchOffAllTooltips();

        ui.CraftUI.gameObject.SetActive(false);
        ui.StorageUI.gameObject.SetActive(false);
    }
}
