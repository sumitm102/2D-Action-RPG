using System;
using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Item Data/Equipment Item", fileName = "Equipment data - ")]
public class SO_EquipmentData : SO_ItemData
{
    [Header("Item modifiers")]
    public ItemModifier[] modifiers;
}

[Serializable]
public class ItemModifier {
    public E_StatType statType;
    public float value;
}
