using SplineMeshTools.Core;
using UnityEngine;
using UnityEngine.Splines;


public class MapObject : MonoBehaviour
{
    [SerializeField] private RailManagement _railManagement;
    public RailManagement Rail => _railManagement;
    public RailAdapter RailAdapter { get; private set; }

    private PlayerBash _player;
    private Vector2 _levelBound;
    private Vector2Int _position;
    public Vector2Int Position => _position;

    private void Awake()
    {
        _player = FindAnyObjectByType<PlayerBash>();
    }

    public void CookMesh()
    {
        var resolution = Rail?.GetComponent<SplineMeshResolution>();
        resolution?.GenerateMeshAlongSpline();
    }

    public void Init(Vector2Int position, Vector2 levelBound)
    {
        _position = position;
        _levelBound = levelBound;
        transform.position
         = new Vector3(position.x * levelBound.x, 0, position.y * levelBound.y);
    }

    public void ConnectCall(MapObject other)
    {
        if(RailAdapter == null)
            RailAdapter = new RailAdapter(_railManagement, other._railManagement);
    }

    public void DisconnectCall()
    {
        RailAdapter.Dispose();
        RailAdapter = null;
    }

    public bool CheckPlayerInBound()
    {
        Vector3 playerPosition = _player.transform.position;
        Vector3 halfBound = new Vector3(_levelBound.x / 2, 0, _levelBound.y / 2);
        Vector3 mapMin = transform.position - halfBound,
                mapMax = transform.position + halfBound;

        return  mapMin.x < playerPosition.x && playerPosition.x < mapMax.x
             && mapMin.z < playerPosition.z && playerPosition.z < mapMax.z;
    }
}
