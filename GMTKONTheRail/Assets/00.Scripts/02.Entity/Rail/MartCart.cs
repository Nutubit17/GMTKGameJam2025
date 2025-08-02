using System;
using System.Collections;
using UnityEngine;

public class MartCart : MonoBehaviour
{
    private RailManagement _railManagement;

    [Header("Physics")]
    [Range(0, 1), SerializeField] private float _friction = 0.5f;
    [SerializeField] private float _downHillMoveMultiplier = 1.5f;
    [SerializeField] private float _maxForce = 50;
    [SerializeField] private float _totalForce = 0;
    public float TotalForce => _totalForce;

    [Header("Shake Decoration")]
    [Range(0, 1), SerializeField] private float _positionShakeness = 0.1f;
    [Range(0.1f, 10), SerializeField] private float _positionFrequency = 2f;
    [Space]
    [Range(0, 20), SerializeField] private float _rotationShakeness = 5f;
    [Range(0.1f, 10), SerializeField] private float _rotationFrequency = 3f;

    private Transform _visual;


    public void Init(RailManagement management)
    {
        _railManagement = management;
        _visual = transform.Find("Visual");
    }

    // test
    void Start()
    {
        if(gameObject.activeSelf)
            AddForce(Vector3.right * 20);
    }

    public void FixedUpdate()
    {
        float frictionDelta = Mathf.Pow(1 - _friction, Time.fixedDeltaTime);
        _totalForce -= Mathf.Sign(_totalForce) * _railManagement.GetCurrentDirection().y * _downHillMoveMultiplier;

        MoveUpdate(_totalForce * Time.fixedDeltaTime);
        _totalForce *= frictionDelta;
    }

    public void ResetForce()
    {
        _totalForce = 0;
    }

    public void AddForce(Vector3 direction)
    {
        Vector3 railDir = _railManagement.GetCurrentDirection();
        float force = Vector3.Project(direction, railDir).magnitude;

        _totalForce = Mathf.Clamp(
            _totalForce + force,
            -_maxForce,
            _maxForce);
    }

    public void AddForce(float force)
    {
        _totalForce = Mathf.Clamp(
            _totalForce + force,
            -_maxForce,
            _maxForce);
    }

    private void MoveUpdate(float step)
    {
        _railManagement.AddDistance(step);
        _railManagement.GetPositionAndDirection(out var position, out var direction);

        float shakeRotation = 0;
        ShakeDecoration(ref position, ref shakeRotation);

        transform.position = position;

        if (direction != Vector3.zero)
        {
            transform.forward = direction * Mathf.Sign(step);
            transform.rotation *= Quaternion.AngleAxis(shakeRotation, transform.forward);
        }
    }

    private void ShakeDecoration(ref Vector3 position, ref float shakeRotation)
    {
        float currentDist = _railManagement.GetCurrentDistance();

        position += _visual.right * Mathf.PerlinNoise1D(currentDist * _positionFrequency) * _positionShakeness;
        shakeRotation = (Mathf.PerlinNoise1D((currentDist + 777.7f) * _rotationFrequency) - 0.5f) * 2 * _rotationShakeness;
    }
}
