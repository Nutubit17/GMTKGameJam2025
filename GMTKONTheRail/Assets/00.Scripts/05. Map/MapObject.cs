using UnityEngine;


public class MapObject : MonoBehaviour
{
    [SerializeField] private RailManagement _railManagement; 
    public RailAdapter RailAdapter { get; private set; }

    public void ConnectCall(MapObject other)
    {
        RailAdapter = new RailAdapter(other._railManagement, _railManagement);
    }
}
