using System;
using SplineMeshTools.Core;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UIElements;

[RequireComponent(typeof(SplineContainer))]
public class RailManagement : MonoBehaviour,IGetCompoable
{
    /* Component */
    [SerializeField] private SplineContainer _splineContainer;
    [SerializeField] private MartCart _martCart;
    
    public MartCart GetMartCart()
    {
        return _martCart;
    }
    /* Temp */

    private Vector3 _lastDirection = Vector3.zero;
    private Vector3 _previousPosition = Vector3.zero;
    private Vector3 _position = Vector3.zero;
    [SerializeField]
    private int _currentPoint = 0;
    [SerializeField]
    private float _currentAmount = 0;
    private float _totalAmount = 0;
    private bool _isNeedPositionUpdate = true;

    public Action<float> OnCartReachedStart;
    public Action<float> OnCartReachedEnd;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        _splineContainer = GetComponent<SplineContainer>();
        _martCart = transform.Find("MartCart").GetComponent<MartCart>();
        _martCart = GetComponentInChildren<MartCart>();
        _martCart.Init(this);
        //_martCart.gameObject.SetActive(false);
        //_martCart.gameObject.SetActive(false);
    }


    public float GetFullDistance() => _splineContainer.CalculateLength(_currentPoint);
    public float GetCurrentDistance() => _totalAmount;
    public float GetCurrentPercent() => _totalAmount / GetFullDistance();
    public float ProjectToSpline(
      SplineContainer container,
      Vector3 worldPos,
      out Vector3 nearestWorldPos,
      out float t,
      int splineIndex = 0,
      int resolution = 64,
      int iterations = 2)
    {
        var tr = container.transform;
        float3 localPoint = (float3)tr.InverseTransformPoint(worldPos);


        var spline = container.Splines[splineIndex];
        float3 nearestLocal;
        float dist = SplineUtility.GetNearestPoint(
            spline, localPoint, out nearestLocal, out t, resolution, iterations);


        nearestWorldPos = tr.TransformPoint((Vector3)nearestLocal);
        return dist;
    }
    public float ProjectToContainerAll(
        SplineContainer container,
        Vector3 worldPos,
        out int bestSplineIndex,
        out Vector3 nearestWorldPos,
        out float t,
        int resolution = 64,
        int iterations = 2)
    {
        bestSplineIndex = -1;
        nearestWorldPos = default;
        t = 0f;

        float bestDist = float.PositiveInfinity;
        for (int i = 0; i < container.Splines.Count; i++)
        {
            Vector3 p;
            float ti;
            float d = ProjectToSpline(container, worldPos, out p, out ti, i, resolution, iterations);
            if (d < bestDist)
            {
                bestDist = d;
                bestSplineIndex = i;
                nearestWorldPos = p;
                t = ti;
            }
        }
        return bestDist;
    }
    public void UpdatePosition(Vector3 pos)
    {
        //int idx = 0;
        Vector3 nearistPos = Vector3.zero;
        float newdist = 0;
        ProjectToContainerAll(_splineContainer, pos, out _currentPoint, out nearistPos, out newdist);
        _currentAmount = Mathf.Max(newdist, 0);
        //_currentPoint = idx;
    }
    public Vector3 GetCurrentPosition()
    {
        _previousPosition = _position;

        if (_isNeedPositionUpdate)
        {
            var position = _splineContainer.EvaluatePosition(_currentPoint, GetCurrentPercent());

            _position = position;
            _isNeedPositionUpdate = false;
        }

        return _position;
    }

    public Vector3 GetCurrentSplineDirection()
    {
        return GetSplineDirection(_currentPoint,GetCurrentPercent());
    }

    public Vector3 GetSplineDirection(int railidx, float percent)
    {
        Vector3 pos1 = _splineContainer.EvaluatePosition(railidx, percent);
        Vector3 pos2 = _splineContainer.EvaluatePosition(railidx,percent*1.0001f);
        return (pos2 - pos1).normalized;
    }

    public Vector3 GetCurrentDirection()
    {
        _lastDirection = (_position - _previousPosition).normalized;

        if (_lastDirection == Vector3.zero)
        {
            _lastDirection = _splineContainer.EvaluateTangent(_currentPoint, GetCurrentPercent());
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
        _martCart.gameObject.SetActive(true);
        _martCart.AddForce(amount);
    }

    public void SetToEnd()
    {
        //_currentPoint = _splineContainer.Spline.Count - 2;

        //_currentAmount = Vector3.Distance(
        //            _splineContainer.Spline[_currentPoint].Position,
        //            _splineContainer.Spline[_currentPoint + 1].Position) - 0.01f;

        _totalAmount = GetFullDistance() - 0.01f;
    }

    public void Sleep()
    {
        _martCart.gameObject.SetActive(false);
    }



    public void AddDistance(float amount)
    {
        _isNeedPositionUpdate = true;
        int splineCount = _splineContainer.Spline.Count;

        float dist2 = ProjectToContainerAll(_splineContainer, _martCart.transform.position, out int bestidx, out Vector3 bestPos, out float bsetdist);

        _currentAmount = bsetdist*GetFullDistance() + amount;
        _currentPoint = bestidx;

        _totalAmount = _currentAmount;

        //_totalAmount += Mathf.Clamp(_totalAmount, 0, GetFullDistance());



        //while (true)
        //{
        //    float currentIdxDistance = Vector3.Distance(
        //        _splineContainer.Spline[_currentPoint].Position,
        //         _splineContainer.Spline[_currentPoint + 1].Position);

        //    _currentAmount += amount;
        //    _totalAmount += amount;
        //    _totalAmount = Mathf.Clamp(_totalAmount, 0, GetFullDistance());

        //    if (_currentAmount < 0)
        //    {
        //        if (_currentPoint == 0 && _currentAmount <= 0)
        //        {
        //            OnCartReachedStart?.Invoke(_martCart.TotalForce);

        //            _martCart.ResetForce();
        //            //_martCart.gameObject.SetActive(false);
        //            break;
        //        }

        //        //_currentPoint = Mathf.Max(0, _currentPoint - 1);
        //        float previousIdxDistance = Vector3.Distance(
        //            _splineContainer.Spline[_currentPoint].Position,
        //            _splineContainer.Spline[_currentPoint + 1].Position);

        //        amount = _currentAmount;
        //        _currentAmount = previousIdxDistance;
        //    }
        //    else if (_currentAmount < currentIdxDistance)
        //    {
        //        break;
        //    }
        //    else
        //    {
        //        if (_currentPoint == splineCount - 2 && _currentAmount >= currentIdxDistance)
        //        {
        //            OnCartReachedEnd?.Invoke(_martCart.TotalForce);

        //            _martCart.ResetForce();
        //            //_martCart.gameObject.SetActive(false);
        //            break;
        //        }

        //       // _currentPoint = Mathf.Min(splineCount - 2, _currentPoint + 1);
        //        amount = _currentAmount - currentIdxDistance;
        //        _currentAmount = 0;
        //    }
        //}
    }

    public void Init(Entity agent)
    {
        //throw new NotImplementedException();
    }
}
