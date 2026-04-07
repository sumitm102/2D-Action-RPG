using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Item Data/Item Effect/Heal Effect", fileName = "Item effect data - heal")]
public class ItemEffect_Heal : SO_ItemEffectData
{
    [SerializeField] private float _healPercent = 0.1f;
    public override void ExecuteEffect() {

        // Heals the player by the heal percentage stored in the field
        Player player = FindFirstObjectByType<Player>();

        if (player == null)
            return;

        float healAmount = player.Stats.GetMaxHealth() * _healPercent;
        player.Health.IncreaseHealth(healAmount);
    }
}
