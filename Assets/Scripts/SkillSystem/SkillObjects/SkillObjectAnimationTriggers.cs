using UnityEngine;

public class SkillObjectAnimationTriggers : MonoBehaviour
{
    private SkillObjectTimeEcho _timeEchoObject;

    private void Awake() {
        _timeEchoObject = GetComponentInParent<SkillObjectTimeEcho>();
    }

    private void AttackTrigger() {
        _timeEchoObject?.PerformAttack();
    }

    private void TryTerminate(int currentAttackIndex) {
        if(currentAttackIndex == _timeEchoObject.MaxAttacks)
            _timeEchoObject?.HandleDeath();
    }
}
