using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public readonly Vector2Int[] EightDirection = new Vector2Int[8]
    {
        Vector2Int.right,
        Vector2Int.left,
        Vector2Int.down,
        Vector2Int.up,
        Vector2Int.right + Vector2Int.up,
        Vector2Int.left + Vector2Int.up,
        Vector2Int.right + Vector2Int.down,
        Vector2Int.left + Vector2Int.down,
    };

    [SerializeField] private MapGenerateInfoSO _mapGenerateInfo;
    //[SerializeField] private MapObject mapObject1, mapObject2;
    private Dictionary<Vector2Int, MapObject> _mapDictionary = new Dictionary<Vector2Int, MapObject>();

    private MapObject _currentMap = null;
    private Vector2Int _currentPosition = Vector2Int.zero;

    private void Awake()
    {
        //mapObject1.ConnectCall(mapObject2);
        _mapGenerateInfo = Instantiate(_mapGenerateInfo);
        _mapGenerateInfo.Init();

        _currentPosition = new(0, 0);
        _currentMap = CreateMap(_currentPosition);

        GenerateMapForEightDirection(_currentMap);
        _currentMap.Rail.WakeUp(0.1f);
    }

    public MapObject CreateMap(Vector2Int position)
    {
        if (!_mapDictionary.TryGetValue(position, out var result))
            result = _mapDictionary[position] = _mapGenerateInfo.InstantiateRandomMap(position);

        return result;
    }

    public MapObject GetMap(Vector2Int position)
     => _mapDictionary.TryGetValue(position, out var result) ? result : null;

    private void GenerateMapForEightDirection(MapObject currentMap)
    {
        var currentPosition = currentMap.Position;

        MapObject downMap =null, upMap=null;


        for (int i = 0; i < EightDirection.Length; ++i)
        {
            var sample = CreateMap(currentPosition + EightDirection[i]);
            if (i == 1)
                downMap = sample;
            else if (i == 0)
                upMap = sample;
        }
        
        if (currentPosition.y == 0)
        {
            downMap.ConnectCall(currentMap);
            currentMap.ConnectCall(upMap);
        }
    }

    public void Update()
    {
        if (!_currentMap.CheckPlayerInBound())
        {
            MapObject nextMap = _currentMap;
            Vector2Int nextPosition = _currentPosition;

            foreach (var direction in EightDirection)
            {
                Vector2Int position = _currentPosition + direction;
                if (_mapDictionary[position].CheckPlayerInBound())
                {
                    nextMap = _mapDictionary[position];
                    nextPosition = position;
                    break;
                }
            }

            GenerateMapForEightDirection(nextMap);

            for (int i = 0; i < EightDirection.Length; ++i)
                GetMap(_currentPosition + EightDirection[i]).gameObject.SetActive(false);

            for (int i = 0; i < EightDirection.Length; ++i)
                GetMap(nextPosition + EightDirection[i]).gameObject.SetActive(true);
                
            GetMap(nextPosition).gameObject.SetActive(true);

            _currentMap = nextMap;
            _currentPosition = nextPosition;

        }
    }
}
