using System.Collections.Generic;
using UnityEngine;

public class SkillDomainExpansion : SkillBase
{
    [SerializeField] private GameObject _domainPrefab;

    [Header("Slowing down upgrade")]
    [SerializeField] private float _targetSlowdownPercent = 0.8f;
    [SerializeField] private float _slowdownDomainDuration = 5f;

    [Header("Spell casting upgrade")]
    [SerializeField] private int _spellsToCast = 10;
    [SerializeField] private float _spellCastingDomainSlowdown = 1f;
    [SerializeField] private float _spellCastingDomainDuration = 8f;
    private float _spellCastTimer;
    private float _spellsPerSecond;

    [Header("Domain details")]
    public float maxDomainSize = 10f;
    public float expandSpeed = 3f;

    private List<Enemy> _trappedTargets = new List<Enemy>();
    private Transform _currentTargetTransform;

    public void CreateDomain() {
        _spellsPerSecond = _spellsToCast / GetDomainDuration();

        GameObject domainObject = Instantiate(_domainPrefab, transform.position, Quaternion.identity);
        if (domainObject.TryGetComponent<SkillObjectDomainExpansion>(out var domainExpansion))
            domainExpansion.SetupDomain(this);
    }

    public void PerformSpellCasting() {
        _spellCastTimer -= Time.deltaTime;

        if(_currentTargetTransform == null) 
            _currentTargetTransform = FindTargetInDomain();

        if(_currentTargetTransform != null & _spellCastTimer < 0) {
            CastSpell(_currentTargetTransform);
            _spellCastTimer = 1f / _spellsPerSecond;
            _currentTargetTransform = null;
        }
        
    }

    private void CastSpell(Transform target) {

        if(upgradeType == E_SkillUpgradeType.Domain_EchoSpam) {

            Vector3 offset = Random.value < 0.5f ? new Vector2(1, 0) : new Vector2(-1, 0);
            PlayerSkillManager.TimeEchoSkill.CreateTimeEcho(target.position + offset);
        }
        else if(upgradeType == E_SkillUpgradeType.Domain_ShardSpam) {

            PlayerSkillManager.ShardSkill.CreateRawShard(target, true);
        }
    }

    private Transform FindTargetInDomain() {
        if (_trappedTargets.Count == 0)
            return null;

        int randomIndex = Random.Range(0, _trappedTargets.Count);
        Transform targetTransform = _trappedTargets[randomIndex].transform;

        if(targetTransform == null) {
            _trappedTargets.RemoveAt(randomIndex);
            return null;
        }

        return targetTransform;
    }

    public float GetDomainDuration() => 
        upgradeType == E_SkillUpgradeType.Domain_SlowDown ? _slowdownDomainDuration : _spellCastingDomainDuration;
    

    public float GetSlowPercentage() =>
        upgradeType == E_SkillUpgradeType.Domain_SlowDown ? _targetSlowdownPercent : _spellCastingDomainSlowdown;
    

    public bool InstantDomain() {
        return upgradeType != E_SkillUpgradeType.Domain_EchoSpam 
            && upgradeType != E_SkillUpgradeType.Domain_ShardSpam;
    }

    public void AddToTargetList(Enemy targetToAdd) => _trappedTargets.Add(targetToAdd);
    public void ClearTargetList() {
        foreach(var target in _trappedTargets)
            target.StopSlowDownEntity();

        _trappedTargets.Clear();
    }
    
}
