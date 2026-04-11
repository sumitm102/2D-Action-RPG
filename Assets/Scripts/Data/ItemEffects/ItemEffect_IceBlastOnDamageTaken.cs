using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Item Data/Item Effect/Ice Blast Effect", 
    fileName = "Item effect data - ice blast")]
public class ItemEffect_IceBlastOnDamageTaken : SO_ItemEffectData
{
    [SerializeField] private float _iceDamage;
    [SerializeField] private float _damageRadius = 1.5f;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private ElementalEffectData _elementalEffectData;

    [Space]
    [SerializeField] private float _hpPercentTrigger = .25f;
    [SerializeField] private float _cooldown;
    [SerializeField] private float _lastTimeUsed = -999f;

    [Header("Vfx details")]
    [SerializeField] private GameObject _iceBlastVFX;
    [SerializeField] private GameObject _onHitVFX;
    public override void ExecuteEffect() {
        bool noCooldown = Time.time >= _lastTimeUsed + _cooldown;
        bool hasReachedThreshold = player.Health.GetHPPercent() <= _hpPercentTrigger;

        if(noCooldown && hasReachedThreshold) {
            _lastTimeUsed = Time.time;
            player.VFX.CreateEffectOf(_iceBlastVFX, player.transform);
            DamageEnemiesWithIceBlast();
        }

    }

    private void DamageEnemiesWithIceBlast() {
        Collider2D[] targets = Physics2D.OverlapCircleAll(player.transform.position, _damageRadius, _enemyLayer);
        bool targetTookDamage = false;
        foreach(var target in targets) {
            if (target.TryGetComponent<IDamagable>(out var damagable)) 
                targetTookDamage = damagable.TakeDamage(0f, _iceDamage, E_ElementType.Ice, player.transform);

            if (target.TryGetComponent<EntityStatusHandler>(out var entityStatusHandler))
                entityStatusHandler.ApplyStatusEffect(E_ElementType.Ice, _elementalEffectData);

            if(targetTookDamage)
                player.VFX.CreateEffectOf(_onHitVFX, player.transform);
        }
    }

    public override void SubscribeToEvent(Player player) {
        base.SubscribeToEvent(player);
        player.Health.OnDamageTaken += ExecuteEffect;

    }

    public override void UnsubscribeToEvent() {
        base.UnsubscribeToEvent();
        player.Health.OnDamageTaken -= ExecuteEffect;
        player = null;
    }
}
