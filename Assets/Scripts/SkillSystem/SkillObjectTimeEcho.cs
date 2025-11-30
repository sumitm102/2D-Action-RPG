using UnityEngine;

public class SkillObjectTimeEcho : SkillObjectBase
{
    [SerializeField] private GameObject _onDeathVFX;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _wispMoveSpeed = 15f;

    private Transform _playerTransform;
    private SkillTimeEcho _skillTimeEcho;
    private TrailRenderer _wispTrail;
    private EntityHealth _playerHealth;
    private SkillObjectHealth _timeEchoHealth;
    private PlayerSkillManager _playerSkillManager;
    private EntityStatusHandler _playerStatusHandler;
    private bool _shouldMoveToPlayer;

    public int MaxAttacks { get; private set; }

    private static readonly int _yVelocityHash = Animator.StringToHash("yVelocity");
    private static readonly int _canAttackHash = Animator.StringToHash("CanAttack");


    public void SetupTimeEcho(SkillTimeEcho skillTimeEcho) {
        _skillTimeEcho = skillTimeEcho;

        _timeEchoHealth = GetComponent<SkillObjectHealth>();

        // Wisp Trail will be turned off be default and unlocked through upgrade
        _wispTrail = GetComponentInChildren<TrailRenderer>();
        _wispTrail.gameObject.SetActive(false);

        MaxAttacks = _skillTimeEcho.GetMaxAttacks();

        _playerTransform = _skillTimeEcho.Player.transform;
        playerStats = _skillTimeEcho.Player.Stats;
        _playerHealth = _skillTimeEcho.Player.Health;
        _playerSkillManager = _skillTimeEcho.Player.SkillManager;
        _playerStatusHandler = _skillTimeEcho.Player.StatusHandler;
        entityVFX = _skillTimeEcho.Player.VFX;
        damageScaleData = _skillTimeEcho.DamageScaleData;

        FlipToTarget();
        anim.SetBool(_canAttackHash, MaxAttacks > 0);

        Invoke(nameof(HandleDeath), _skillTimeEcho.GetEchoDuration());
    }

    private void Update() {

        if (_shouldMoveToPlayer) 
            HandleWispMovement();
        else {
            anim.SetFloat(_yVelocityHash, rb.linearVelocityY);
            StopHorizontalMovementWhenGrounded();
        }
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

        if (_skillTimeEcho.ShouldBeWisp()) 
            SetupWisp();
        else
            Destroy(this.gameObject);
    }

    private void SetupWisp() {
        _shouldMoveToPlayer = true;

        anim.gameObject.SetActive(false);
        rb.simulated = false;

        _wispTrail.gameObject.SetActive(true);
    }

    private void HandleWispMovement() {
        transform.position = Vector2.MoveTowards(transform.position, _playerTransform.position, _wispMoveSpeed * Time.deltaTime);

        // Apply effects and destroy this object on close contact
        if(Vector2.Distance(transform.position, _playerTransform.position) < 0.5f) {
            HandlePlayerTouch();
            Destroy(this.gameObject);
        }
    }

    private void HandlePlayerTouch() {
        float healAmount = _timeEchoHealth.LastDamageTaken * _skillTimeEcho.GetPercentOfDamageHealed();
        _playerHealth.IncreaseHealth(healAmount);

        float cooldownToReduce = _skillTimeEcho.GetCooldownReduceInSeconds();
        _playerSkillManager.ReduceAllSkillCooldownBy(cooldownToReduce);

        if(_skillTimeEcho.CanRemoveNegativeEffects())
            _playerStatusHandler.RemoveAllNegativeEffects();
    }

    private void StopHorizontalMovementWhenGrounded() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, _groundLayer);

        if (hit.collider != null)
            rb.linearVelocity = new Vector2(0f, rb.linearVelocityY);
    }

}
