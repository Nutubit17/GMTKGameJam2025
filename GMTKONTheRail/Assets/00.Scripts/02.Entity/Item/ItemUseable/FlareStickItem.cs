using System.Runtime.InteropServices;
using UnityEngine;

public class FlareStickItem : ItemUseableObject
{
    [SerializeField]
    private GameObject _summonObj;

    [SerializeField]
    private float _power = 10f;
    public void Throw()
    {
        GameObject instance = Instantiate(_summonObj, transform.position,transform.rotation);

        instance.GetComponent<Rigidbody>()?.AddForce(transform.forward*_power,ForceMode.Impulse);
        instance.GetComponent<Rigidbody>()?.AddTorque(transform.right*3,ForceMode.Impulse);

    }
}
