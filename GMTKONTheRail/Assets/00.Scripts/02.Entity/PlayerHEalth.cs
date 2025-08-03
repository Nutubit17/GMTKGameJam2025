using UnityEngine;
using UnityEngine.Events;

public class PlayerHEalth : HealthCompo
{
    [SerializeField]
    private Animator _animator;

    public UnityEvent _hertEvent, _healEvent;

    public override void Damage(float damage)
    {
        if(damage > 0 )
        _hertEvent.Invoke();
        if(damage < 0 )
        _healEvent.Invoke();
        base.Damage(damage);
    }

    private void Update()
    {
        _animator.SetFloat("Hp",_hp/_maxHp);
    }

    public void Hit()
    {
        _animator.SetTrigger("Hit");
    }

    public void Heal()
    {
        _animator.SetTrigger("Heal");
    }
}
