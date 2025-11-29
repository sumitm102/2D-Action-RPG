using UnityEngine;

public class SkillTimeEcho : SkillBase {
    [SerializeField] private GameObject _timeEchoPrefab;
    [SerializeField] private float _timeEchoDuration;

    [Header("Attack upgrades")]
    [SerializeField] private int _maxAttacks = 3;
    [SerializeField] private float _duplicateChance = 0.3f;

    public void CreateTimeEcho(Vector3? targetPosition = null) {
        Vector3 position = targetPosition ?? transform.position;

        GameObject timeEcho = Instantiate(_timeEchoPrefab, position, Quaternion.identity);

        if (timeEcho.TryGetComponent<SkillObjectTimeEcho>(out var skillObjectTimeEcho))
            skillObjectTimeEcho.SetupTimeEcho(this);

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

    public float GetEchoDuration() {
        return _timeEchoDuration;
    }

    public override void TryUseSkill() {
        if (!CanUseSkill())
            return;

        CreateTimeEcho();
    }
}
