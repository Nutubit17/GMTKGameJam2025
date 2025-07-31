using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : Agent
{
    [SerializeField] private PlayerBash _player; 
    [SerializeField] private float _playerDetectRange = 10f;
    [SerializeField] private float _lightDetectRange = 5f;
    [SerializeField] private LayerMask _lightLayer;

    private Collider[] _lightColliderCheckArr = new Collider[10];

    protected override void Awake()
    {
        base.Awake();
        _player = FindAnyObjectByType<PlayerBash>();
    }

    public void Update()
    {
        bool isPlayerInRange = (_player.transform.position - transform.position).sqrMagnitude
                         < _playerDetectRange * _playerDetectRange;

        int LightCount = Physics.OverlapSphereNonAlloc
            (transform.position, _lightDetectRange, _lightColliderCheckArr, _lightLayer);

        bool isLightExists = false;

        for (int i = 0; i < LightCount; ++i)
        {
            if (_lightColliderCheckArr[i].TryGetComponent<LightObject>(out var light))
            {
                if (light.IsLightOn)
                {
                    isLightExists = true;
                    break;
                }
            }
        }

        if (isPlayerInRange && !isLightExists)
        {
            EnemyUpdate();
        }
        else
        {
            NormalUpdate();
        }
        
    }


    private void EnemyUpdate()
    {

    }

    private void NormalUpdate()
    {
        
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _playerDetectRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _lightDetectRange);
    }
#endif
}
