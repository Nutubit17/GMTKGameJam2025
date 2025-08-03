using System.Runtime.InteropServices;
using UnityEngine;

public class FlareStickItem : ItemUseableObject
{
    [SerializeField]
    private GameObject _summonObj;

    [SerializeField]
    private float _power = 10f;
    [SerializeField]
    private bool _isBurning = false;

    public void Burn()
    {
        _isBurning = true;
    }
    public void Throw()
    {

        GameObject instance = Instantiate(_isBurning?_summonObj:_itemType.ItemSO1.Prefab.gameObject, transform.position,transform.rotation);

        instance.GetComponent<Rigidbody>()?.AddForce(transform.forward*_power,ForceMode.Impulse);
        instance.GetComponent<Rigidbody>()?.AddTorque(transform.right*3,ForceMode.Impulse);

        Mom.EraseItem();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        Throw();
    }
}
