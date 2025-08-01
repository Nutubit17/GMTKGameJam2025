using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/EnemyInfo")]
public class EnemyInfoSO : ScriptableObject
{
    public float hp = 5;
    public float speed = 2.5f;
    public float runSpeed = 5f;

    [Space]

    public float PlayerDetectRange = 10f;
    public float LightDetectRange = 5f;
    public float RunRange = 2f;
    public float AttackRange = 1.5f;
}

