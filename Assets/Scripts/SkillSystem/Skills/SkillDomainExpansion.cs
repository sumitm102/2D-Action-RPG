using System.Collections.Generic;
using UnityEngine;

public class SkillDomainExpansion : SkillBase
{
    [SerializeField] private GameObject _domainPrefab;

    [Header("Slowing down upgrade")]
    [SerializeField] private float _targetSlowdownPercent = 0.8f;
    [SerializeField] private float _slowdownDomainDuration = 5f;

    [Header("Shard cast upgrade")]
    [SerializeField] private int _shardsToCast = 10;
    [SerializeField] private float _shardCastSlowdownPercent = 1f;
    [SerializeField] private float _shardCastDomainDuration = 8f;
    private float _spellCastTimer;
    private float _spellsPerSecond;

    [Header("Time echo cast upgrade")]
    [SerializeField] private int _echosToCast = 8;
    [SerializeField] private float _echoCastSlowdownPercent = 1f;
    [SerializeField] private float _echoCastDomainDuration = 6f;

    [Header("Domain details")]
    public float maxDomainSize = 10f;
    public float expandSpeed = 3f;

    private List<Enemy> _trappedTargets = new List<Enemy>();
    private Transform _currentTargetTransform;

    public void CreateDomain() {
        _spellsPerSecond = GetSpellsToCast() / GetDomainDuration();

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

        _trappedTargets.RemoveAll(target => target == null || target.EnemyHealth.IsDead);

        if (_trappedTargets.Count == 0)
            return null;

        int randomIndex = Random.Range(0, _trappedTargets.Count);
        return _trappedTargets[randomIndex].transform;

    }

    public float GetDomainDuration() {
        if (upgradeType == E_SkillUpgradeType.Domain_SlowDown)
            return _slowdownDomainDuration;
        else if (upgradeType == E_SkillUpgradeType.Domain_ShardSpam)
            return _shardCastDomainDuration;
        else if (upgradeType == E_SkillUpgradeType.Domain_EchoSpam)
            return _echoCastDomainDuration;

        return 0f;
    }


    public float GetSlowPercentage() {
        if (upgradeType == E_SkillUpgradeType.Domain_SlowDown)
            return _targetSlowdownPercent;
        else if (upgradeType == E_SkillUpgradeType.Domain_ShardSpam)
            return _shardCastSlowdownPercent;
        else if (upgradeType == E_SkillUpgradeType .Domain_EchoSpam)
            return _echoCastSlowdownPercent;

        return 0f;
    }

    private int GetSpellsToCast() {
        if (upgradeType == E_SkillUpgradeType.Domain_ShardSpam)
            return _shardsToCast;
        else if (upgradeType == E_SkillUpgradeType.Domain_EchoSpam)
            return _echosToCast;

        return 0;
    }
    

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
