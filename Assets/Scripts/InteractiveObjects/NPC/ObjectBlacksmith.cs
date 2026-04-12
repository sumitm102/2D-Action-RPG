using UnityEngine;

public class ObjectBlacksmith : ObjectNPC, IInteractable {
    private Animator _anim;
    private static readonly int _blacksmithHash = Animator.StringToHash("IsBlacksmith");

    protected override void Awake() {
        base.Awake();
        _anim = GetComponentInChildren<Animator>();
        if(_anim != null )
            _anim.SetBool(_blacksmithHash, true);
    }
    public void Interact() {
        Debug.Log("Open Craft or Storage");
    }
}
