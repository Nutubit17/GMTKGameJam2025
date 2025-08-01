using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Light Data")]
public class LightDataSO : ScriptableObject
{
    [System.Serializable]
    public struct LightDataStruct
    {
        [SerializeField, Range(0, 1)] public float lightActivePercentage;
        [SerializeField, Range(0f, 0.5f)] public float transitionSmoothness;
        [SerializeField, Range(0f, 10f)] public float frequency;
    }

    public LightDataStruct[] dataArr;
    public int DataCount => dataArr.Length;
}
