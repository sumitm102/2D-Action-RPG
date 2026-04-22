using UnityEngine;

public class ObjectMerchant : ObjectNPC, IInteractable {
    private Inventory_Player _playerInventory;
    private Inventory_Merchant _merchantInventory;

    protected override void Awake() {
        base.Awake();
        _merchantInventory = GetComponent<Inventory_Merchant>();
    }

    protected override void Update() {
        base.Update();
        if(Input.GetKeyDown(KeyCode.Z))
            _merchantInventory.FillShopList();
    }

    public void Interact() {
        ui.MerchantUI.SetupMerchantUI(_merchantInventory, _playerInventory);

        ui.MerchantUI.gameObject.SetActive(toggleUI);
        toggleUI = !toggleUI;
    }

    protected override void OnTriggerEnter2D(Collider2D collision) {
        base.OnTriggerEnter2D(collision);
        _playerInventory = player.GetComponent<Inventory_Player>();
        _merchantInventory.SetInventory(_playerInventory);
    }

    protected override void OnTriggerExit2D(Collider2D collision) {
        base.OnTriggerExit2D(collision);
        ui.SwitchOffAllTooltips();
        ui.MerchantUI.gameObject.SetActive(false);
    }
}
