using UnityEngine;

public class SO_ItemEffectData : ScriptableObject
{
    [TextArea]
    public string effectDescription;

    public virtual void ExecuteEffect() {

    }
}
