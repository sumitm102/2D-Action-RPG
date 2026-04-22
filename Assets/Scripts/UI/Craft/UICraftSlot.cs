using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICraftSlot : MonoBehaviour
{
    private SO_ItemData _itemToCraft;

    [SerializeField] private Image _craftItemIcon;
    [SerializeField] private TextMeshProUGUI _craftItemName;
    [Space]
    [SerializeField] private UICraftPreview _craftPreview;

    public void SetupButton(SO_ItemData craftData) {
        _itemToCraft = craftData;
        _craftItemIcon.sprite = craftData.itemIcon;
        _craftItemName.text = craftData.itemName;
    }

    public void UpdateCraftPreview() => _craftPreview.UpdateCraftPreview(_itemToCraft);
}
