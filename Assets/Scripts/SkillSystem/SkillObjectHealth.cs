using UnityEngine;

public class SkillObjectHealth : EntityHealth
{
    protected override void Die() {
        SkillObjectTimeEcho timeEcho = GetComponent<SkillObjectTimeEcho>();
        timeEcho?.HandleDeath();
    }
}
