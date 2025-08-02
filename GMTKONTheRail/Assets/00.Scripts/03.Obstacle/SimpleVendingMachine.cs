using UnityEngine;

public class SimpleVendingMachine : MonoBehaviour, ICoinUseable
{
    [SerializeField]
    protected int _price = 1;
    [SerializeField]
    protected GameObject _summonObj;

    [SerializeField]
    protected Transform _summonPivot;

    [SerializeField] protected int _insertedCoin = 0;
    public void UseCoin()
    {
        _insertedCoin++;
        if (_insertedCoin >= _price)
        {
            _insertedCoin -= _price;
            Instantiate(_summonObj, _summonPivot.position, _summonPivot.rotation);
        }
    }
}
