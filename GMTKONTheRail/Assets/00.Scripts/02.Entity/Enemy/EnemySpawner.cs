using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyBase[] _enemyPrefabs;
    [SerializeField] private Vector2 _generateDelayMinMax = new Vector2(15f, 30f);

    private void Awake()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {

        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(Random.Range(_generateDelayMinMax.x, _generateDelayMinMax.y));
        }
    }

    private void Spawn()
    {

    }
}
