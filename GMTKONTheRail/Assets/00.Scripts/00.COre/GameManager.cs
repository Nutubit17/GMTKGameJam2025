using UnityEngine;

public class GameManager : MonoBehaviour
{
    static readonly int ID_MyGlobalFloat = Shader.PropertyToID("_Height");
    [SerializeField]
    private float _height =500;

    public static GameManager Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
        SetHightPower();
    }
    [ContextMenu("SetFloat")]
    public void SetHightPower()
    {
        Shader.SetGlobalFloat(ID_MyGlobalFloat, _height);
    }

}
