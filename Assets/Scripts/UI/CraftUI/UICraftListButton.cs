using UnityEngine;

public class UICraftListButton : MonoBehaviour
{
    [SerializeField] private SO_ItemListData _craftData;
    private UICraftSlot[] _craftSlots;

    public void SetCraftSlots(UICraftSlot[] craftSlots) => _craftSlots = craftSlots;

    // Called in the inspector through button click
    public void UpdateCraftSlots() {
        if(_craftData == null) {
            Debug.Log("Craft list data has not been assigned");
            return;
        }

        foreach(var craftSlot in _craftSlots)
            craftSlot.gameObject.SetActive(false);

        for(int i = 0; i < _craftData.itemList.Length; i++) {
            SO_ItemData itemData = _craftData.itemList[i];

            _craftSlots[i].gameObject.SetActive(true);
            _craftSlots[i].SetupButton(itemData);
        }
    }
}
