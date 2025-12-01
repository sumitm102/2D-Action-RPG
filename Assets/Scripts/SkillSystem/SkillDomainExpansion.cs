using UnityEngine;

public class SkillDomainExpansion : SkillBase
{
    [SerializeField] private GameObject _domainPrefab;

    [Header("Slowing down upgrade")]
    [SerializeField] private float _targetSlowdownPercent = 0.8f;
    [SerializeField] private float _slowdownDomainDuration = 5f;

    [Header("Spell casting upgrade")]
    [SerializeField] private float _spellCastingDomainSlowdown = 1f;
    [SerializeField] private float _spellCastingDomainDuration = 8f;

    [Header("Domain details")]
    public float maxDomainSize = 10f;
    public float expandSpeed = 3f;
    

    public float GetDomainDuration() => 
        upgradeType == E_SkillUpgradeType.Domain_SlowDown ? _slowdownDomainDuration : _spellCastingDomainDuration;
    

    public float GetSlowPercentage() =>
        upgradeType == E_SkillUpgradeType.Domain_SlowDown ? _targetSlowdownPercent : _spellCastingDomainSlowdown;
    

    public bool InstantDomain() {
        return upgradeType != E_SkillUpgradeType.Domain_EchoSpam 
            && upgradeType != E_SkillUpgradeType.Domain_ShardSpam;
    }
    public void CreateDomain() {
        GameObject domainObject = Instantiate(_domainPrefab, transform.position, Quaternion.identity);
        if (domainObject.TryGetComponent<SkillObjectDomainExpansion>(out var domainExpansion))
            domainExpansion.SetupDomain(this);
    }
}
