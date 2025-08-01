using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/MapGenerateInfo")]
public class MapGenerateInfoSO : ScriptableObject
{
    public Vector2 _mapSize = new Vector2(16, 16);

    [System.Serializable]
    public struct MapLevel
    {
        public List<MapObject> _mapList;
    }

    public List<MapLevel> _levelList;
}
