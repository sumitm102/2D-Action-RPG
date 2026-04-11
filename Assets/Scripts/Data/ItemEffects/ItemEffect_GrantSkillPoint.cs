using UnityEngine;


[CreateAssetMenu(menuName = "RPG Setup/Item Data/Item Effect/Grant Skill Point Effect", 
    fileName = "Item effect data - grant skill point")]
public class ItemEffect_GrantSkillPoint : SO_ItemEffectData
{
    [SerializeField] private int _pointsToAdd;


    public override void ExecuteEffect() {
        UI ui = FindFirstObjectByType<UI>();
        ui.SkillTreeUI.AddSkillPoints(_pointsToAdd);
    }
}
