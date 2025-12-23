using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Item Data/Material Item", fileName = "Material data - ")]
public class SO_ItemData : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public E_ItemType itemType;
    public int maxStackSize = 1;
}
