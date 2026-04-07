using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Item Data/Item Effect/Refund All Skills Effect", 
    fileName = "Item effect data - refund all skills")]
public class ItemEffect_RefundAllSkills : SO_ItemEffectData
{
    public override void ExecuteEffect() {
        UI ui = FindFirstObjectByType<UI>();
        ui.skillTree.RefundAllSkills();

        #region Alternate way to perform the same functionality
        // Find game object with ui skill tree component even if it's inactive
        //UISkillTree skillTree = FindFirstObjectByType<UISkillTree>(FindObjectsInactive.Include);
        //if (skillTree == null)
        //    return;
        //skillTree.RefundAllSkills();
        #endregion
    }
}
