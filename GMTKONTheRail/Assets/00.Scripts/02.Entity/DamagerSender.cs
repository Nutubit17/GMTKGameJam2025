using UnityEngine;

public class DamagerSender : MonoBehaviour, IGetCompoable, IGetDamageable,IAfterInitable
{

    public Entity Mom;

    public HealthCompo MomHealth;

    public void AfterInit()
    {
        MomHealth = Mom.GetComponent<HealthCompo>();
    }

    public void Damage(float damage, float cooldown)
    {
        MomHealth.Damage(damage, cooldown);
    }

    public void Damage(float damage)
    {
        MomHealth.Damage( damage);
    }

    public HealthCompo GetParentHealth()
    {
        return MomHealth;
    }

    public void Init(Entity agent)
    {
        Mom = agent;
        
    }
}
