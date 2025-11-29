using UnityEngine;

public class SkillObjectTimeEcho : SkillObjectBase
{
    [SerializeField] private GameObject _onDeathVFX;
    [SerializeField] private LayerMask _groundLayer;
    private SkillTimeEcho _skillTimeEcho;

    public int MaxAttacks { get; private set; }

    private static readonly int _yVelocityHash = Animator.StringToHash("yVelocity");
    private static readonly int _canAttackHash = Animator.StringToHash("CanAttack");


    public void SetupTimeEcho(SkillTimeEcho skillTimeEcho) {
        _skillTimeEcho = skillTimeEcho;
        MaxAttacks = _skillTimeEcho.GetMaxAttacks();

        playerStats = _skillTimeEcho.Player.Stats;
        entityVFX = _skillTimeEcho.Player.VFX;
        damageScaleData = _skillTimeEcho.DamageScaleData;

        FlipToTarget();
        anim.SetBool(_canAttackHash, MaxAttacks > 0);

        Invoke(nameof(HandleDeath), _skillTimeEcho.GetEchoDuration());
    }

    private void Update() {
        anim.SetFloat(_yVelocityHash, rb.linearVelocityY);
        StopHorizontalMovementWhenGrounded();
    }

    private void FlipToTarget() {
        Transform target = FindClosestTarget();

        if (target != null && target.position.x < transform.position.x)
            transform.Rotate(0, 180, 0);
    }

    public void PerformAttack() {
        DamageEnemiesInRadius(targetCheck, checkRadius);

        if (!targetTookDamage)
            return;

        bool canDuplicate = Random.value < _skillTimeEcho.GetDuplicateChance();
        float xOffset = transform.position.x < lastTarget.position.x ? 1f : -1f;

        if (canDuplicate)
            _skillTimeEcho.CreateTimeEcho(lastTarget.position + new Vector3(xOffset, 0, 0));
    }

    public void HandleDeath() {
        Instantiate(_onDeathVFX, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    private void StopHorizontalMovementWhenGrounded() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, _groundLayer);

        if (hit.collider != null)
            rb.linearVelocity = new Vector2(0f, rb.linearVelocityY);
    }

}
