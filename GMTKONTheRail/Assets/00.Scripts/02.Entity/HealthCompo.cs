using UnityEngine;
using UnityEngine.Events;

public class HealthCompo : MonoBehaviour,IGetCompoable,IGetDamageable
{
    [SerializeField]
    protected float _maxHp, _hp;

    private bool _isGetDamageAble = true;

    public UnityEvent OnDeadEvent;

    private Entity _agent;

    private bool _isDead = false;
    public void Init(Entity agent)
    {
       _agent = agent;
    }
    public virtual void Awake()
    {
        _hp = _maxHp;
    }
    public virtual void Damage(float damage)
    {
        if( _isGetDamageAble)
        {
            _hp -= damage;
            _hp = Mathf.Clamp(_hp, 0, _maxHp);
            if (_hp <= 0)
            {
                if (!_isDead)
                {
                    OnDeadEvent?.Invoke();
                    _isDead = true;
                }

            }
        }
    }
    public virtual void Damage(float damage,float cooldown)
    {
        if (_isGetDamageAble)
        {
            Damage(damage);
            _isGetDamageAble = false;
            Invoke(nameof(ToggleDamageAble),cooldown);
        }
    }


    void ToggleDamageAble()
    {
        _isGetDamageAble = true;
    }

    public HealthCompo GetParentHealth()
    {
        return this;
    }
}
