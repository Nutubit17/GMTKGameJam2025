using UnityEngine;

public class EnemySnake : EnemyBase
{   
    protected override int _defaultIdleBlend => AnimationBlend.Idle.Snake;
    protected override int _attackBlendStart => AnimationBlend.Attack.Snake;
    protected override int _attackBlendEnd => AnimationBlend.Attack.Snake;

    protected override int _idleBlendStart => AnimationBlend.Idle.Snake;
    protected override int _idleBlendEnd => AnimationBlend.Idle.Snake;

    protected override int _walkBlend => AnimationBlend.Walk.Snake;
    protected override int _hitBlend => AnimationBlend.Hit.Snake;
}
