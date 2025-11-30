using UnityEngine;

public class SkillTimeEcho : SkillBase {
    [SerializeField] private GameObject _timeEchoPrefab;
    [SerializeField] private float _timeEchoDuration;

    [Header("Attack upgrades")]
    [SerializeField] private int _maxAttacks = 3;
    [SerializeField] private float _duplicateChance = 0.3f;

    [Header("Heal upgrades")]
    [SerializeField] private float _damagePercentHealed = 0.3f;
    [SerializeField] private float _cooldownReducedInSeconds;

    public void CreateTimeEcho(Vector3? targetPosition = null) {
        Vector3 position = targetPosition ?? transform.position;

        GameObject timeEcho = Instantiate(_timeEchoPrefab, position, Quaternion.identity);

        if (timeEcho.TryGetComponent<SkillObjectTimeEcho>(out var skillObjectTimeEcho))
            skillObjectTimeEcho.SetupTimeEcho(this);

    }

    public bool ShouldBeWisp() {
        return upgradeType == E_SkillUpgradeType.TimeEcho_HealWisp
            || upgradeType == E_SkillUpgradeType.TimeEcho_CleanseWisp
            || upgradeType == E_SkillUpgradeType.TimeEcho_CooldownWisp;
    }

    public float GetDuplicateChance() {
        if (upgradeType != E_SkillUpgradeType.TimeEcho_ChanceToDuplicate)
            return 0;

        return _duplicateChance;
    }

    public int GetMaxAttacks() {
        if (upgradeType == E_SkillUpgradeType.TimeEcho_SingleAttack
            || upgradeType == E_SkillUpgradeType.TimeEcho_ChanceToDuplicate)
            return 1;

        if (upgradeType == E_SkillUpgradeType.TimeEcho_MultiAttack)
            return _maxAttacks;

        return 0;
            
    }

    public float GetPercentOfDamageHealed() => !ShouldBeWisp() ? 0 : _damagePercentHealed;

    public float GetCooldownReduceInSeconds() => 
        upgradeType != E_SkillUpgradeType.TimeEcho_CooldownWisp ? 0 : _cooldownReducedInSeconds;

    public bool CanRemoveNegativeEffects() => upgradeType == E_SkillUpgradeType.TimeEcho_CleanseWisp;


    public float GetEchoDuration() => _timeEchoDuration;

    public override void TryUseSkill() {
        if (!CanUseSkill())
            return;

        CreateTimeEcho();
    }
}
