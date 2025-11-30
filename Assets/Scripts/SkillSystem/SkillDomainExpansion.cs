using UnityEngine;

public class SkillDomainExpansion : SkillBase
{
    public bool InstantDomain() {
        return upgradeType != E_SkillUpgradeType.Domain_EchoSpam 
            && upgradeType != E_SkillUpgradeType.Domain_ShardSpam;
    }
    public void CreateDomain() {
        Debug.Log("Created Domain Expansion");
    }
}
