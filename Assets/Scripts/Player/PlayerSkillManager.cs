using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    public SkillDash DashSkill { get; private set; }
    public SkillShard ShardSkill { get; private set; }
    public SkillSwordThrow SwordThrowSkill { get; private set; }
    public SkillTimeEcho TimeEchoSkill { get; private set; }

    private SkillBase[] _allSkills;

    private void Awake() {
        DashSkill = GetComponentInChildren<SkillDash>();
        ShardSkill = GetComponentInChildren<SkillShard>();
        SwordThrowSkill = GetComponentInChildren<SkillSwordThrow>();
        TimeEchoSkill = GetComponentInChildren<SkillTimeEcho>();

        _allSkills = GetComponentsInChildren<SkillBase>();
    }


    public SkillBase GetSkillByType(E_SkillType skillType) {
        switch (skillType) {
            case E_SkillType.Dash: return DashSkill;
            case E_SkillType.TimeShard: return ShardSkill;
            case E_SkillType.SwordThrow: return SwordThrowSkill;
            case E_SkillType.TimeEcho: return TimeEchoSkill;

            default:
                Debug.Log($"Skill type {skillType} is not implemented");
                return null;
        }
    }

    public void ReduceAllSkillCooldownBy(float cooldownToReduce) {
        foreach (var skill in _allSkills)
            skill.ReduceCooldownBy(cooldownToReduce);
    }
}
