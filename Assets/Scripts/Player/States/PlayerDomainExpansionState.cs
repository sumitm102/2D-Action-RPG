using Unity.VisualScripting;
using UnityEngine;

public class PlayerDomainExpansionState : PlayerState {

    private Vector2 _originalPosition;
    private float _originalGravityScale;
    private float _finalRiseDistance;

    private bool _isLevitating;
    private bool _createdDomain;

    public PlayerDomainExpansionState(StateMachine sm, int abn, Player p) : base(sm, abn, p) {
    }

    public override void EnterState() {
        base.EnterState();

        _originalPosition = player.transform.position;
        _originalGravityScale = rb.gravityScale;
        _finalRiseDistance = GetAvailableRiseDistance();

        canDash = false;

        player.SetVelocity(0f, player.RiseSpeed);
    }
    public override void UpdateState() {
        base.UpdateState();

        if (Vector2.Distance(_originalPosition, player.transform.position) >= _finalRiseDistance && !_isLevitating)
            Levitate();

        if (_isLevitating) {

            skillManager.DomainExpansionSkill.PerformSpellCasting();

            if(stateTimer < 0f) {

                _isLevitating = false;
                rb.gravityScale = _originalGravityScale;

                stateMachine.ChangeState(player.IdleState);
            }
        }
    }

    public override void ExitState() {
        base.ExitState();

        _createdDomain = false;
        canDash = true;
    }

    private void Levitate() {
        _isLevitating = true;
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0f;

        stateTimer = skillManager.DomainExpansionSkill.GetDomainDuration();

        if (!_createdDomain) {
            _createdDomain = true;
            skillManager.DomainExpansionSkill.CreateDomain();
        }   
    }

    private float GetAvailableRiseDistance() {
        RaycastHit2D hit =
            Physics2D.Raycast(player.transform.position, Vector3.up, player.RiseMaxDistance, player.groundDetectionLayer);

        return hit.collider != null ? hit.distance - 1f : player.RiseMaxDistance;
    }

}
