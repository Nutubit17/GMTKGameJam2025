using UnityEngine;

public class EnemySnake : EnemyBase
{
    protected override void OnIdle()
    {
        if (_currentState != _previousState)
            _animator.SetFloat(_blendHash, AnimationBlend.Idle.Snake);
        
        if (EnemyState.Attack == _previousState)
            transform.position += Vector3.up * 1.5f;
    }

    protected override void OnAttack()
    {
        _animator.SetFloat(_blendHash, AnimationBlend.Attack.Snake);

        if(_currentState != _previousState)
            transform.position -= Vector3.up * 1.5f;
    }
    
    protected override void HandleAnimationEnd()
    {
        _animator.SetTrigger(_IdleHash);
        _animator.SetFloat(_blendHash, AnimationBlend.Idle.Snake);
    }

    protected override void OnWalk()
    {
        _animator.SetFloat(_blendHash, AnimationBlend.Walk.Snake);
        _animator.speed = 1f;

        if (EnemyState.Attack == _previousState)
            transform.position += Vector3.up * 1.5f;
    }

    protected override void OnRun()
    {
        _animator.SetTrigger(_walkHash);
        _animator.SetFloat(_blendHash, AnimationBlend.Walk.Snake);
        _animator.speed = 1.5f;
        
        if (EnemyState.Attack == _previousState)
            transform.position += Vector3.up * 1.5f;
    }
}
