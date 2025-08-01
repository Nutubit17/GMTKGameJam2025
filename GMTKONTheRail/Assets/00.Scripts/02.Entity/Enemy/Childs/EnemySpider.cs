using UnityEngine;

public class EnemySpider : EnemyBase
{
    protected override int _defaultIdleBlend => AnimationBlend.Idle.Spider;
    
    protected override int _attackBlendStart => AnimationBlend.Attack.Spider;
    protected override int _attackBlendEnd => AnimationBlend.Attack.Spider;

    protected override int _idleBlendStart => AnimationBlend.Idle.Spider;
    protected override int _idleBlendEnd => AnimationBlend.Idle.Spider;

    protected override int _walkBlend => AnimationBlend.Walk.Spider;
}
