using UnityEngine;

public class OverlapDamageCaster : MonoBehaviour
{
    [SerializeField]
    protected float _damage =1;
    [SerializeField]
    protected LayerMask _whatIsTarget;
    public void DamageCast()
    {
        DamageCast(_damage);
    }

    public void DamageCast(float a)
    {
        GameManager.Instance.GetCompo<StaticDamageCaster>().OverlapDamageCast(transform.position, transform.lossyScale, transform.rotation,_whatIsTarget, a);
    }
}
