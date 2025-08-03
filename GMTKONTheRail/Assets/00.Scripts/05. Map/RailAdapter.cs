using System;
using UnityEngine;

public class RailAdapter
{
    private RailManagement _leftRail;
    private RailManagement _rightRail;

    public RailAdapter(RailManagement leftRail, RailManagement rightRail)
    {
        _leftRail = leftRail;
        _rightRail = rightRail;

        _leftRail.OnCartReachedEnd += HandleLeftReachedEnd;
        _rightRail.OnCartReachedStart += HandleRightReachedStart;
    }

    private void HandleLeftReachedEnd(float amount)
    {
        _rightRail.WakeUp(amount);
    }

    private void HandleRightReachedStart(float amount)
    {
        _leftRail.SetToEnd();
        _leftRail.WakeUp(amount);
    }

    public void Dispose()
    {
        _leftRail.OnCartReachedEnd -= HandleLeftReachedEnd;
        _rightRail.OnCartReachedStart -= HandleRightReachedStart;
    }
}
