using UnityEngine;
using UnityEngine.Events;

public class CoinEventer : MonoBehaviour,ICoinUseable
{
    [SerializeField]
    private UnityEvent CoinInsertEvent;
    [SerializeField]
    private int _needCoin = 0;

    public void UseCoin()
    {
        _needCoin--;
        if (_needCoin > 0)
            return;

        CoinInsertEvent?.Invoke();
    }

}
