using System.Collections;
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

    [SerializeField]
    private GameObject _burnningObject;

    private bool _isBurnning() => (_itemType.Ammo == 1);

    [SerializeField]
    private bool _isFirst = true;
    public void Burn()
    {
        _isBurning = true;
        _itemType.Ammo = 1;
        StartCoroutine(Burnning());
        _burnningObject?.SetActive(true);
    }


    private IEnumerator Burnning()
    {
        while (true)
        {
            if (!_isBurnning())
                break;



            _itemType.Durability -= 1;

            if(_itemType.Durability <=0)
            {
                _isBurning = false;
                Mom.EraseItem();
                
                break;
            }
            yield return new WaitForSeconds(1);
        }
    }

    public void Throw()
    {
        if (_isFirst)
        {

        }
        GameObject instance = Instantiate(_isBurning ? _summonObj : _itemType.ItemSO1.Prefab.gameObject, transform.position, transform.rotation);

        instance.GetComponent<Rigidbody>()?.AddForce(transform.forward * _power, ForceMode.Impulse);
        instance.GetComponent<Rigidbody>()?.AddTorque(transform.right * 3, ForceMode.Impulse);

        Mom.EraseItem();
       
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        //GameObject instance = Instantiate(_isBurning ? _summonObj : _itemType.ItemSO1.Prefab.gameObject, transform.position, transform.rotation);

        _burnningObject?.SetActive(false);

        StopAllCoroutines();
        //if (instance)
        //{
        //    instance.GetComponent<Rigidbody>()?.AddForce(transform.forward * _power, ForceMode.Impulse);
        //    instance.GetComponent<Rigidbody>()?.AddTorque(transform.right * 3, ForceMode.Impulse);
        //    Mom.EraseItem();
        //}
        //Throw();
    }
}
