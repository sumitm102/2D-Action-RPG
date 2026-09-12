using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Item Data/Material Item", fileName = "Material data - ")]
public class SO_ItemData : ScriptableObject
{
    [Header("Item details")]
    public string itemName;
    public Sprite itemIcon;
    public E_ItemType itemType;
    public int maxStackSize = 1;

    [Header("Drop details")]
    [Range(0, 1000)] public int itemRarity = 100;
    [Range(0, 100)] public float dropChance;
    [Range(0, 100)] public float maxDropChance = 65f;

    [Header("Item effect")]
    public SO_ItemEffectData itemEffect;

    [Header("Craft details")]
    public Inventory_Item[] craftRecipe;

    [Header("Merchant details")]
    [Range(0, 10000)]
    public int itemPrice = 100;
    public int minStackSizeAtShop = 1;
    public int maxStackSizeAtShop = 1;

    private void OnValidate() {
        dropChance = GetDropChance();
    }

    public float GetDropChance() {
        float maxRarity = 1000f;
        float chance = (maxRarity - itemRarity + 1) / maxRarity * 100f;

        return Mathf.Min(chance, maxDropChance);
    }
}
