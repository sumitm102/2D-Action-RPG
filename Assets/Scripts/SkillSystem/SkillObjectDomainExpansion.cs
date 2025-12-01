using UnityEngine;

public class SkillObjectDomainExpansion : SkillObjectBase
{
    private SkillDomainExpansion _skillDomainExpansion;

    private float _expandSpeed;
    private float _duration;
    private float _targetSlowdownPercent;

    private Vector3 _targetScale;
    private bool _isShrinking;


    public void SetupDomain(SkillDomainExpansion skillDomainExpansion) {
        _skillDomainExpansion = skillDomainExpansion;
        
        float maxSize = _skillDomainExpansion.maxDomainSize;
        _duration = _skillDomainExpansion.GetDomainDuration();
        _targetSlowdownPercent = _skillDomainExpansion.GetSlowPercentage();
        _expandSpeed = _skillDomainExpansion.expandSpeed;

        _targetScale = Vector3.one * maxSize;
        Invoke(nameof(ShrinkDomain), _duration);
    }

    private void Update() {
        HandleScaling();
    }

    private void HandleScaling() {
        float sizeDifference = Mathf.Abs(transform.localScale.x - _targetScale.x);
        bool shouldChangeScale = sizeDifference > 0.1f;

        if (shouldChangeScale)
            transform.localScale = Vector3.Lerp(transform.localScale, _targetScale, _expandSpeed * Time.deltaTime);

        if (_isShrinking && sizeDifference < 0.1f)
            Destroy(this.gameObject);

    }

    private void ShrinkDomain() {
        _targetScale = Vector3.zero;
        _isShrinking = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy == null)
            return;

        enemy.SlowDownEntity(_duration, _targetSlowdownPercent, true);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy == null)
            return;

        enemy.StopSlowDownEntity();
    }

}
