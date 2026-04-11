using UnityEngine;

public class SO_ItemEffectData : ScriptableObject
{
    [TextArea]
    public string effectDescription;
    protected Player player;

    public virtual bool CanBeUsed() {
        return true;
    }

    public virtual void ExecuteEffect() {

    }

    public virtual void SubscribeToEvent(Player player) {
        this.player = player;
    }

    public virtual void UnsubscribeToEvent() {

    }
}
