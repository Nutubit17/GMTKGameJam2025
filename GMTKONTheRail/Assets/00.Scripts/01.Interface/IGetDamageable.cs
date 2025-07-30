using UnityEngine;

public interface IGetDamageable
{
    public void Damage(float damage, float cooldown);
    public void Damage(float damage);
}
