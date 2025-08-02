using UnityEngine;

public interface IGetDamageable
{
    public HealthCompo GetParentHealth();
    public void Damage(float damage, float cooldown);
    public void Damage(float damage);
}
