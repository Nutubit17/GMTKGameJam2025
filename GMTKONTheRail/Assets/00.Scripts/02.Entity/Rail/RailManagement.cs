using System;
using SplineMeshTools.Core;
using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(SplineContainer))]
public class RailManagement : MonoBehaviour
{
    /* Component */
    [SerializeField] private SplineContainer _splineContainer;
    [SerializeField] private MartCart _martCart;
    
    /* Temp */
    private int _currentPoint = 0;
    private float _currentAmount = 0;
    private float _totalAmount = 0;
    private bool _isNeedPositionUpdate = true;
    private Vector3 _lastDirection = Vector3.zero;
    private Vector3 _previousPosition = Vector3.zero;
    private Vector3 _position = Vector3.zero;

    public Action<float> OnCartReachedStart;
    public Action<float> OnCartReachedEnd;

    private void Awake()
    {
        _splineContainer = GetComponent<SplineContainer>();
        _martCart = transform.Find("MartCart").GetComponent<MartCart>();
        _martCart.Init(this);
    }

    public float GetFullDistance() => _splineContainer.CalculateLength(0);
    public float GetCurrentDistance() => _totalAmount;
    public float GetCurrentPercent() => _totalAmount / GetFullDistance();

    public Vector3 GetCurrentPosition()
    {
        _previousPosition = _position;

        if (_isNeedPositionUpdate)
        {
            var position = _splineContainer.EvaluatePosition(0, GetCurrentPercent());

            _position = position;
            _isNeedPositionUpdate = false;
        }

        return _position;
    }

    public Vector3 GetCurrentDirection()
    {
        _lastDirection = (_position - _previousPosition).normalized;

        if (_lastDirection == Vector3.zero)
        {
            _lastDirection = _splineContainer.EvaluateTangent(0, GetCurrentPercent());
        }

        return _lastDirection;
        
    }
    
    public void GetPositionAndDirection(out Vector3 position, out Vector3 direction)
    {
        position = GetCurrentPosition();
        direction = GetCurrentDirection();
    }

    public void WakeUp(float amount)
    {
        _martCart.AddForce(amount);
        _martCart.gameObject.SetActive(true);
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
                if (_currentPoint == 0 && _currentAmount <= 0)
                {
                    OnCartReachedStart?.Invoke(_martCart.TotalForce);
                    
                    _martCart.ResetForce();
                    _martCart.gameObject.SetActive(false);
                    break;
                }

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
                if (_currentPoint == splineCount - 2 && _currentAmount >= currentIdxDistance)
                {
                    OnCartReachedEnd?.Invoke(_martCart.TotalForce);

                    _martCart.ResetForce();
                    _martCart.gameObject.SetActive(false);
                    break;
                }

                _currentPoint = Mathf.Min(splineCount - 2, _currentPoint + 1);
                amount = _currentAmount - currentIdxDistance;
                _currentAmount = 0;
            }
        }
    }

}
