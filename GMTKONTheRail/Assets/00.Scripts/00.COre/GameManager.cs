using UnityEngine;

public class GameManager : Entity
{
    static readonly int ID_MyGlobalFloat = Shader.PropertyToID("_Height");
    [SerializeField]
    private float _height =500;

    public PlayerBash PlayerInstance;

    private static GameManager _instance;

    public static GameManager Instance
    {
        get {
            if(_instance == null)
            {
                _instance = new GameObject("GameManager").AddComponent<GameManager>();
                return _instance;
            }


            return _instance; }
    }   


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Awake()
    {
        Screen.SetResolution(640 * 2, 480 * 2, false);
        Screen.fullScreen = true;
        
        base.Awake();
        _instance = this;
        SetHightPower();
    }
    [ContextMenu("SetFloat")]
    public void SetHightPower()
    {
        Shader.SetGlobalFloat(ID_MyGlobalFloat, _height);
    }

}
