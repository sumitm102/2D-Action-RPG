using System;
using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Item Data/Item Effect/Buff Effect", fileName = "Item effect data - buff")]
public class ItemEffect_Buff : SO_ItemEffectData
{
    [SerializeField] private BuffEffectData[] _buffsToApply;
    [SerializeField] private float _duration;
    [SerializeField] private string _source = Guid.NewGuid().ToString();

    private PlayerStats _playerStats;

    public override bool CanBeUsed() {
        if(_playerStats == null)
            _playerStats = FindFirstObjectByType<PlayerStats>();

        if (_playerStats.CanApplyBuffs(_source))
            return true;

        Debug.Log("Same buff effect cannot be applied more than once");
        return false;
        
    }

    public override void ExecuteEffect() {
        _playerStats.ApplyBuffs(_buffsToApply, _duration, _source);
    }
}
