using UnityEngine;

public class ObjectMerchant : ObjectNPC, IInteractable {
    public void Interact() {
        Debug.Log("Open merchant's shop");
    }
}
