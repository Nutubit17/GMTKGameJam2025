using UnityEngine;


public class MapObject : MonoBehaviour
{
    [SerializeField] private RailManagement _railManagement;
    public RailAdapter RailAdapter { get; private set; }

    public void ConnectCall(MapObject other)
    {
        RailAdapter = new RailAdapter(_railManagement, other._railManagement);
    }

    public void DisconnectCall()
    {
        RailAdapter.Dispose();
    }

    public void CheckPlayerInBound()
    {
    }
}
