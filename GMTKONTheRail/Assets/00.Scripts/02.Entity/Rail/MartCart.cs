using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class MartCart : MonoBehaviour
{
    [SerializeField] private RailManagement _railManagement;
    [Header("Physics")]
    [Range(0, 1), SerializeField] private float _friction = 0.5f;
    private Transform _visual;
    private IEnumerator _forceRoutine;

    [Header("Shake Decoration")]
    [Range(0, 1), SerializeField] private float _positionShakeness = 0.1f;
    [Range(0.1f, 10), SerializeField] private float _positionFrequency = 2f;
    [Space]
    [Range(0, 20), SerializeField] private float _rotationShakeness = 5f;
    [Range(0.1f, 10), SerializeField] private float _rotationFrequency = 3f;

    private void Awake()
    {
        _visual = transform.Find("Visual");
    }

    // test
    void Start()
    {
        AddForce(10);
    }

    public void AddForce(float force)
    {
        if (_forceRoutine != null)
            StopCoroutine(_forceRoutine);

        _forceRoutine = ForceMoveRoutine(force);
        StartCoroutine(_forceRoutine);
    }

    private IEnumerator ForceMoveRoutine(float force)
    {
        float frictionDelta = Mathf.Pow(1 - _friction, Time.fixedDeltaTime);

        while (Mathf.Abs(force) > 0.1f)
        {
            MoveUpdate(force * Time.fixedDeltaTime);
            force *= frictionDelta;

            yield return new WaitForFixedUpdate();
        }
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
            transform.forward = direction;
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
