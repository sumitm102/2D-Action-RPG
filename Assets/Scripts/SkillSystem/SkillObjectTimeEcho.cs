using UnityEngine;

public class SkillObjectTimeEcho : SkillObjectBase
{
    [SerializeField] private GameObject _onDeathVFX;
    [SerializeField] private LayerMask _groundLayer;
    private SkillTimeEcho _skillTimeEcho;

    private static readonly int _yVelocityHash = Animator.StringToHash("yVelocity");

    public void SetupTimeEcho(SkillTimeEcho skillTimeEcho) {
        _skillTimeEcho = skillTimeEcho;

        Invoke(nameof(HandleDeath), _skillTimeEcho.GetEchoDuration());
    }

    private void Update() {
        anim.SetFloat(_yVelocityHash, rb.linearVelocityY);
        StopHorizontalMovementWhenGrounded();
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
