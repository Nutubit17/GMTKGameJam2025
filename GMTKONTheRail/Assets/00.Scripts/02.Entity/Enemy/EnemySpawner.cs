using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyBase[] _enemyPrefabs;
    [SerializeField] private Vector2 _generateDelayMinMax = new Vector2(15f, 30f);
    [SerializeField] private Vector2Int _generateCountMinMax = new Vector2Int(2, 5);
    [SerializeField] private float _generateDistance = 20;
    [SerializeField] private Collider[] _colliderArr = new Collider[10];
    [SerializeField] private LayerMask _layer;
    [SerializeField] private PlayerBash _playerBash;

    private void Awake()
    {
        _playerBash = FindAnyObjectByType<PlayerBash>();
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {

        while (true)
        {
            TrySpawn();
            yield return new WaitForSeconds(Random.Range(_generateDelayMinMax.x, _generateDelayMinMax.y));
        }
    }

    private void TrySpawn()
    {
        int generateCount = Random.Range(_generateCountMinMax.x, _generateCountMinMax.y);

        for (int i = 0; i < generateCount; ++i)
        {
            EnemyBase ebase = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Length)];
            int trycount = 0;
            while (!TrySpawnEnemy(ebase) && trycount < 20)
            {
                ++trycount;
            }
        }
    }

    private bool TrySpawnEnemy(EnemyBase ebase)
    {
        Vector3 point = Random.insideUnitCircle.normalized * _generateDistance;
        point = _playerBash.transform.position + new Vector3(point.x, 3, point.y);

        float checkingRange = 1f;

        _colliderArr = new Collider[10];
        var hit = Physics.OverlapSphereNonAlloc
        (transform.position, checkingRange, _colliderArr, _layer);

        if (hit <= 1)
        {
            var enemy = Instantiate(ebase, point, Quaternion.identity);
            enemy.Init();
            return true;
        }

        return false;
    }
}
