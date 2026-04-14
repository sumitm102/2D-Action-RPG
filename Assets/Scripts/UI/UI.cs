using UnityEngine;

public class UI : MonoBehaviour
{
    #region Tooltips
    public UISkillTooltip SkillTooltip { get; private set; }
    public UIItemTooltip ItemTooltip { get; private set; }
    public UIStatTooltip StatTooltip { get; private set; }
    #endregion

    public UISkillTree SkillTreeUI { get; private set; }
    public UIInventory InventoryUI { get; private set; }
    public UIStorage StorageUI { get; private set; }
    
    
    private bool _isSkillTreeUIEnabled;
    private bool _isInventoryUIEnabled;

    private void Awake() {
        SkillTooltip = GetComponentInChildren<UISkillTooltip>();
        ItemTooltip = GetComponentInChildren<UIItemTooltip>();
        StatTooltip = GetComponentInChildren<UIStatTooltip>();

        // Able to get the components even when the game objects are not active hierarchy
        SkillTreeUI = GetComponentInChildren<UISkillTree>(true);
        InventoryUI = GetComponentInChildren<UIInventory>(true);
        StorageUI = GetComponentInChildren<UIStorage>(true);

        // Sets it according to whether the object is active or not
        _isSkillTreeUIEnabled = SkillTreeUI.gameObject.activeSelf;
        _isInventoryUIEnabled = InventoryUI.gameObject.activeSelf;
    }


    public void SwitchOffAllTooltips() {
        SkillTooltip.ShowTooltip(false, null);
        ItemTooltip.ShowTooltip(false, null);
        StatTooltip.ShowTooltip(false, null);
    }


    public void ToggleSkillTreeUI() {
        _isSkillTreeUIEnabled = !_isSkillTreeUIEnabled;
        SkillTreeUI.gameObject.SetActive(_isSkillTreeUIEnabled);
        SkillTooltip.ShowTooltip(false, null);
    }

    public void ToggleInventoryUI() {
        _isInventoryUIEnabled = !_isInventoryUIEnabled;
        InventoryUI.gameObject.SetActive(_isInventoryUIEnabled);

        StatTooltip.ShowTooltip(false, null);
        ItemTooltip.ShowTooltip(false, null);

    }
}
