using System;
using System.Collections;
using UnityEngine;

public class HpCheckWithLight : MonoBehaviour
{
    [SerializeField] private float _lightCheckRange = 5;
    [SerializeField] private LayerMask _lightLayer;
    [SerializeField] private PlayerBash _playerBash;
    [SerializeField] private PlayerHEalth _playerHealth;
    [SerializeField] private float _delay = 6f;
    [SerializeField] private float _damageApplyMultiplier = 0.03f;

    void Awake()
    {
        _playerBash = GetComponent<PlayerBash>();
        _playerHealth = _playerBash.GetComponent<PlayerHEalth>();

        StartCoroutine(DamageRoutine());
    }

    private IEnumerator DamageRoutine()
    {
        while (true)
        {
            DamageFunc();
            yield return new WaitForSeconds(_delay);
        }
    }

    private Collider[] _lightobjarr = new Collider[10];

    private void DamageFunc()
    {
        var hit = Physics.OverlapSphereNonAlloc
        (_playerBash.transform.position, _lightCheckRange, _lightobjarr, _lightLayer);

        if (hit != 0)
        {
            for (int i = 0; i < hit; ++i)
            {
                if (_lightobjarr[i].TryGetComponent<LightObject>(out var light))
                {
                    if (light.IsLightOn)
                    {
                        _playerHealth.Damage(_delay * _damageApplyMultiplier);
                        return;
                    }
                }
            }

        }
        _playerHealth.Damage(_delay * _damageApplyMultiplier);
    }
    

}
