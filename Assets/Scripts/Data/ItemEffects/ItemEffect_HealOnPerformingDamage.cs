using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Item Data/Item Effect/Heal on Performing Damage Effect",
    fileName = "Item effect data - heal on performing damage")]
public class ItemEffect_HealOnPerformingDamage : SO_ItemEffectData
{
    [SerializeField] private float _hpPercentHealedOnAttack = 0.2f;


    private void HealOnPerformingDamage(float damageAmount) {
        player.Health.IncreaseHealth(damageAmount * _hpPercentHealedOnAttack);
    }

    public override void SubscribeToEvent(Player player) {
        base.SubscribeToEvent(player);
        player.Combat.OnPerformingPhysicalDamage += HealOnPerformingDamage;
    }

    public override void UnsubscribeToEvent() {
        base.UnsubscribeToEvent();
        player.Combat.OnPerformingPhysicalDamage -= HealOnPerformingDamage;
        player = null;
    }
}
