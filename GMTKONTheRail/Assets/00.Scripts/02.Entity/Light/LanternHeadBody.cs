using System.Collections;
using UnityEngine;

public class LanternHeadBody : LightObject, ICoinUseable
{
    [SerializeField] private Animator _animator;
    private readonly int _beginHash = Animator.StringToHash("begin");

    private float _lastCoinEnterTime = 0;
    [SerializeField] private float _fuelPerCoin = 2f;

    public void UseCoin()
    {
        UseCoinRoutine();
    }

    private IEnumerator UseCoinRoutine()
    {
        _animator.SetBool(_beginHash, false);
        yield return null;

        if (Time.time > _lastCoinEnterTime + 4)
        {
            _animator.SetBool(_beginHash, true);
            _lastCoinEnterTime = Time.time;

            AddFuel(_fuelPerCoin);
        }
    }
}
