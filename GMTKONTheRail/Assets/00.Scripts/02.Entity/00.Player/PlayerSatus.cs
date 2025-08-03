using System.Collections;
using UnityEngine;

public class PlayerSatus : MonoBehaviour, IGetCompoable,IAfterInitable
{
    public PlayerBash Mom;

    public float MaxMaxStemina = 10f, MaxStemina = 10f, CurrentStemina = 10;

    public PlayerHEalth PlHeatlhCOmpo;

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
        if(Random.Range(0,10) > 6)
        PlHeatlhCOmpo?.Damage(-0.5f);
        CurrentStemina = Mathf.Clamp(CurrentStemina + stemina,0,MaxStemina);
    }

    public void AddMaxStemina(float stemina)
    {
        PlHeatlhCOmpo?.Damage(-0.8f);
        MaxStemina = Mathf.Clamp(MaxStemina + stemina, 0, MaxMaxStemina);
        AddStemina(0);
    }
    public void SetSteminaRegen(bool enable)
    {
        _steminaRegen = enable;
    }
}
