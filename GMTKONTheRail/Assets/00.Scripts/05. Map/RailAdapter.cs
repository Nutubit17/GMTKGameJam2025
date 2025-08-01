using UnityEngine;

public class RailAdapter
{
    private RailManagement _leftRail;
    private RailManagement _rightRail;

    public RailAdapter(RailManagement leftRail, RailManagement rightRail)
    {
        _leftRail = leftRail;
        _rightRail = rightRail;
    }
}
