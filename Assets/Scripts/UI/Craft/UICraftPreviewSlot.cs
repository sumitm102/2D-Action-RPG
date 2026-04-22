using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICraftPreviewSlot : MonoBehaviour
{
    [SerializeField] private Image _materialIcon;
    [SerializeField] private TextMeshProUGUI _materialNameAndValue;

    public void SetupPreviewSlot(SO_ItemData itemData, int availableAmount, int requiredAmount) {
        _materialIcon.sprite = itemData.itemIcon;
        _materialNameAndValue.text = itemData.itemName + " - " + availableAmount + "/" + requiredAmount;
    }
}
