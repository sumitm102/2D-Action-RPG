using UnityEngine;

public class SkillTimeEcho : SkillBase
{
    [SerializeField] private GameObject _timeEchoPrefab;
    [SerializeField] private float _timeEchoDuration;

    public void CreateTimeEcho() {
        GameObject timeEcho = Instantiate(_timeEchoPrefab, transform.position, Quaternion.identity);

        if(timeEcho.TryGetComponent<SkillObjectTimeEcho>(out var skillObjectTimeEcho))
            skillObjectTimeEcho.SetupTimeEcho(this);
        
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
