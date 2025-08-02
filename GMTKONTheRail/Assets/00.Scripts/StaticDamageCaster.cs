
using System.Collections.Generic;
using UnityEngine;

public class StaticDamageCaster : MonoBehaviour , IGetCompoable
{
    public GameManager Mom;

    public void Init(Entity agent)
    {
        Mom = agent as GameManager;
    }

    public void OverlapDamageCast(Vector3 pos, Vector3 size, Quaternion rot, LayerMask whatIsTarget,float Damage)
    {
        Collider[] result = new Collider[8];
        if(Physics.OverlapBoxNonAlloc(pos,size,result,rot, whatIsTarget) >0)
        {
            List<HealthCompo> healths = new();
            foreach (Collider col in result)
            {
                if (col == null)
                    break;

                //if(col.gameObject.CompareTag("Hitable"))
                {
                    
                    if(col.gameObject.TryGetComponent<IGetDamageable>(out IGetDamageable damgeable))
                    {
                        if (healths.Contains(damgeable.GetParentHealth()))
                        {
                            break;
                        }
                        damgeable.Damage(Damage);
                        healths.Add(damgeable.GetParentHealth());
                    }
                }
            }
        }
    }
}
