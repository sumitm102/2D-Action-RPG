using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class PlayerStats : EntityStats
{
    private List<string> _activeBuffsList = new List<string>();
    private Inventory_Player _inventory;

    protected override void Awake() {
        base.Awake();
        _inventory = GetComponent<Inventory_Player>();
    }

    public bool CanApplyBuffs(string source) {
        return !_activeBuffsList.Contains(source);
    }


    public void ApplyBuffs(BuffEffectData[] buffsToApply, float duration, string source) {
        StartCoroutine(BuffCo(buffsToApply, duration, source));
    }
    private IEnumerator BuffCo(BuffEffectData[] buffsToApply, float duration, string source) {
        _activeBuffsList.Add(source);

        foreach(var buff in buffsToApply) 
            GetStatByType(buff.type).AddModifier(buff.value, source);

        yield return new WaitForSeconds(duration);

        foreach (var buff in buffsToApply)
            GetStatByType(buff.type).RemoveModifier(source);

        _inventory.TriggerUIUpdate();

        _activeBuffsList.Remove(source);
        
    }
}
