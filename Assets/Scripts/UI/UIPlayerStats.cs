using UnityEngine;

public class UIPlayerStats : MonoBehaviour
{
    private UIStatSlot[] _uiStatSlots;
    private Inventory_Player _inventory;

    private void Awake() {
        _uiStatSlots = GetComponentsInChildren<UIStatSlot>();
        _inventory = FindFirstObjectByType<Inventory_Player>();
        _inventory.OnInventoryChange += UpdateStatsUI;
    }

    private void Start() {
        // Calling in start to get default values
        UpdateStatsUI();
    }

    private void UpdateStatsUI() {
        foreach (var statSlot in _uiStatSlots) {
            statSlot.UpdateStatValue();
        }
    }

    private void OnDisable() {
        _inventory.OnInventoryChange -= UpdateStatsUI;
    }
}
