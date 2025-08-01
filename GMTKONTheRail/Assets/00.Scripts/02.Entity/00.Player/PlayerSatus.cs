using System.Collections;
using UnityEngine;

public class PlayerSatus : MonoBehaviour, IGetCompoable,IAfterInitable,IGetDamageable
{
    public PlayerBash Mom;

    public float MaxMaxStemina = 10f, MaxStemina = 10f, CurrentStemina = 10;

    public float MaxHp = 10f, CurrentHP = 10;

    private bool _steminaRegen = true;

    public void Init(Entity agent)
    {
        Mom = agent as PlayerBash;
    }
    public void AfterInit()
    {
        
    }

    private void Start()
    {
        StartCoroutine(SteminaCockrootin());
    }

    private IEnumerator SteminaCockrootin()
    {
        while (true)
        {
            if (_steminaRegen)
            {
                if (CurrentStemina < MaxStemina)
                {
                    AddMaxStemina(Mathf.Min(Random.Range(-0.8f, 0.1f), 0));
                    AddStemina(Random.Range(0.5f, 2));
                }
                else
                {
                    AddMaxStemina(Mathf.Min(Random.Range(-0.15f, 0.6f), 0));
                }

            }
            else
            {
                AddMaxStemina(Mathf.Min(Random.Range(-0.2f, 0.6f), 0));
            }
            yield return new WaitForSeconds(Random.Range(0.8f, 1.5f));
        }
    }
    public void AddStemina(float stemina)
    {
        CurrentStemina = Mathf.Clamp(CurrentStemina + stemina,0,MaxStemina);
    }

    public void AddMaxStemina(float stemina)
    {
        MaxStemina = Mathf.Clamp(MaxStemina + stemina, 0, MaxMaxStemina);
        AddStemina(0);
    }
    public void SetSteminaRegen(bool enable)
    {
        _steminaRegen = enable;
    }
    public void Damage(float damage, float cooldown)
    {
        
    }

    public void Damage(float damage)
    {
        
    }
}
