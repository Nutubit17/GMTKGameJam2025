using UnityEngine;
using UnityEngine.Events;

public abstract class HealthCompo : MonoBehaviour,IGetCompoable
{
    [SerializeField]
    protected float _maxHp, _hp;

    private bool _isGetDamageAble = true;

    public UnityEvent OnDeadEvent;

    private Agent _agent;
    public void Init(Agent agent)
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
                OnDeadEvent?.Invoke();
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
}
