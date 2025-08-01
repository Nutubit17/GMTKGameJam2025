using UnityEngine;

public class EnemySpider : EnemyBase
{
    protected override void Awake()
    {
        base.Awake();
        _animator.SetTrigger(_IdleHash);
        _animator.SetFloat(_blendHash, AnimationBlend.Idle.Spider);
    }

    protected override void OnIdle()
    {
        if (_currentState != _previousState)
            _animator.SetFloat(_blendHash, AnimationBlend.Idle.Spider);
    }

    protected override void OnAttack()
    {
        _animator.applyRootMotion = false;
        _animator.SetTrigger(_attackHash);
        _animator.SetFloat(_blendHash, AnimationBlend.Attack.Spider);
    }

    protected override void HandleAnimationEnd()
    {
        _currentState = EnemyState.Idle;
        
        _animator.SetTrigger(_attackHash);
        _animator.SetFloat(_blendHash, AnimationBlend.Attack.Spider);
    }

    protected override void OnWalk()
    {
        _animator.SetFloat(_blendHash, AnimationBlend.Walk.Spider);
        _animator.speed = 1f;
    }

    protected override void OnRun()
    {
        _animator.SetTrigger(_walkHash);
        _animator.SetFloat(_blendHash, AnimationBlend.Walk.Spider);
        _animator.speed = 1.5f;
    }
}
