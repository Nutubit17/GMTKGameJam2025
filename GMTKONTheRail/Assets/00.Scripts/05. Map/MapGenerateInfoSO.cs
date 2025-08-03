using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Scriptable Object/MapGenerateInfo")]
public class MapGenerateInfoSO : ScriptableObject
{
    [SerializeField]
    private Vector2 _mapSize = new Vector2(16, 16);

    [System.Serializable]
    public struct MapLevel
    {
        public List<MapObject> mapList;
    }

    public List<MapLevel> levelList;
    private MapCombiner _mapCombiner = new MapCombiner();

    public void Init()
    {
        for (int i = 0; i < levelList.Count; ++i)
        {
            for (int j = 0; j < levelList[i].mapList.Count; ++j)
            {
                var map = levelList[i].mapList[j];
                
                map.CookMesh();
                levelList[i].mapList[j] = _mapCombiner.Execute(map);
            }
        }
    }

    public MapObject InstantiateRandomMap(Vector2Int position)
    {
        var list = levelList[Mathf.Clamp(Mathf.Abs(position.y), 0, levelList.Count - 1)].mapList;
        var map = Instantiate(list[Random.Range(0, list.Count)]);
        map.Init(position, _mapSize);

        map.gameObject.SetActive(true);
        return map;
    }
    public MapObject InstantiateFirstRandomMap(Vector2Int position)
    {
        var list = levelList[0].mapList;
        var map = Instantiate(list[Random.Range(0, list.Count)]);
        map.Init(position, _mapSize);

        map.gameObject.SetActive(true);
        return map;
    }
}
