using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Item Data/Material Item", fileName = "Material data - ")]
public class SO_ItemData : ScriptableObject
{
    [Header("Item details")]
    public string itemName;
    public Sprite itemIcon;
    public E_ItemType itemType;
    public int maxStackSize = 1;

    [Header("Item effect")]
    public SO_ItemEffectData itemEffect;

    [Header("Craft details")]
    public Inventory_Item[] craftRecipe;

    [Header("Merchant details")]
    [Range(0, 10000)]
    public int itemPrice = 100;
    public int minStackSizeAtShop = 1;
    public int maxStackSizeAtShop = 1;
}
