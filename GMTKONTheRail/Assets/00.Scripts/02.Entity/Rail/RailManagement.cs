using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Splines;

[RequireComponent(typeof(SplineContainer))]
public class RailManagement : MonoBehaviour
{
    /* Component */
    private SplineContainer _splineContainer;
    
    /* Temp */
    private int _currentPoint = 0;
    private float _currentAmount = 0;
    private float _totalAmount = 0;
    private bool _isNeedPositionUpdate = true;
    private Vector3 _previousPosition = Vector3.zero;
    private Vector3 _position = Vector3.zero;

    private void Awake()
    {
        _splineContainer = GetComponent<SplineContainer>();
    }


    public float GetFullDistance() => _splineContainer.CalculateLength(0);
    public float GetCurrentDistance() => _totalAmount;
    public float GetCurrentPercent() => _totalAmount / GetFullDistance();

    public Vector3 GetCurrentPosition()
    {
        _previousPosition = _position;

        if (_isNeedPositionUpdate)
        {
            _splineContainer.Evaluate(0, GetCurrentPercent(), out var position, out var tangent, out var upvector);

            _position = position;
            _isNeedPositionUpdate = false;
        }

        return _position;
    }

    public Vector3 GetCurrentDirection()
    {
        Vector3 vector = (_position - _previousPosition).normalized;

        if (vector == Vector3.zero)
            return _splineContainer.Spline.EvaluateTangent(0);

        return vector;
        
    } 

    public void GetPositionAndDirection(out Vector3 position, out Vector3 direction)
    {
        position = GetCurrentPosition();
        direction = GetCurrentDirection();
    }


    public void AddDistance(float amount)
    {
        _isNeedPositionUpdate = true;
        int splineCount = _splineContainer.Spline.Count;

        while (true)
        {
            float currentIdxDistance = Vector3.Distance(
                _splineContainer.Spline[_currentPoint].Position,
                 _splineContainer.Spline[_currentPoint + 1].Position);

            _currentAmount += amount;
            _totalAmount += amount;
            _totalAmount = Mathf.Clamp(_totalAmount, 0, GetFullDistance());

            if (_currentAmount < 0)
            {
                if (_currentPoint == 0 && _currentAmount <= 0) break;

                _currentPoint = Mathf.Max(0, _currentPoint - 1);
                float previousIdxDistance = Vector3.Distance(
                    _splineContainer.Spline[_currentPoint].Position,
                    _splineContainer.Spline[_currentPoint + 1].Position);

                amount = _currentAmount;
                _currentAmount = previousIdxDistance;
            }
            else if (_currentAmount < currentIdxDistance)
            {
                break;
            }
            else
            {
                if (_currentPoint == splineCount - 2 && _currentAmount >= currentIdxDistance) break;

                _currentPoint = Mathf.Min(splineCount - 2, _currentPoint + 1);
                amount = _currentAmount - currentIdxDistance;
                _currentAmount = 0;
            }
        }
    }
}
