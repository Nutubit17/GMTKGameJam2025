using UnityEngine;

public class GameManager : Entity
{
    static readonly int ID_MyGlobalFloat = Shader.PropertyToID("_Height");
    [SerializeField]
    private float _height =500;

    public PlayerBash PlayerInstance;

    public static GameManager Instance;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Awake()
    {
        Screen.SetResolution(640, 480, false);
        
        base.Awake();
        Instance = this;
        SetHightPower();
    }
    [ContextMenu("SetFloat")]
    public void SetHightPower()
    {
        Shader.SetGlobalFloat(ID_MyGlobalFloat, _height);
    }

}
