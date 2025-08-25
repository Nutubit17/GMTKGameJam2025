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

    [SerializeField] private AudioSource _railSound;
    private float volume = 1;
    private Transform _visual;


    public void Init(RailManagement management)
    {
        _railManagement = management;
        _visual = transform.Find("Visual");

        _railSound = GetComponent<AudioSource>();
        volume = _railSound.volume;
    }

    // test
    void Start()
    {
        //if(gameObject.activeSelf)
        //    AddForce(Vector3.right * 20);
    }

    public void TP2Pos(Vector3 pos)
    {
        if (GameManager.Instance.PlayerInstance is not null)
        {
            //Vector3 plpos = transform.InverseTransformPoint(GameManager.Instance.PlayerInstance.transform.position);
            //Quaternion plrot =  GameManager.Instance.PlayerInstance.transform.rotation * Quaternion.Inverse(transform.rotation);
            //Matrix4x4 plmatrix = GameManager.Instance.PlayerInstance.transform.localToWorldMatrix * transform.worldToLocalMatrix;
            Transform mom = GameManager.Instance.PlayerInstance.transform.parent;
            GameManager.Instance.PlayerInstance.transform.parent = transform;

        transform.position = pos;
        MoveUpdate(0);

            Invoke(nameof(NoMomPL),0.03f);
            //GameManager.Instance.PlayerInstance.transform.parent = mom;

            //plmatrix *= transform.localToWorldMatrix;
            //GameManager.Instance.PlayerInstance.transform.SetPositionAndRotation(transform.TransformPoint(plpos),plrot * transform.rotation);
        }
        else
        {
            transform.position = pos;
            MoveUpdate(0);
        }
    }

    private void NoMomPL()
    {
        GameManager.Instance.PlayerInstance.transform.parent = null;
    }

    public void FixedUpdate()
    {
        float frictionDelta = Mathf.Pow(1 - _friction, Time.fixedDeltaTime);
        _totalForce -= Mathf.Sign(_totalForce) * _railManagement.GetCurrentDirection().y * _downHillMoveMultiplier;

        MoveUpdate(_totalForce * Time.fixedDeltaTime);
        _totalForce *= frictionDelta;

        _railSound.volume = _totalForce > 0.1f ? volume : 0;
    }

    public void ResetForce()
    {
        _totalForce = 0;
    }

    public void AddForce(Vector3 direction)
    {
        Vector3 railDir = _railManagement.GetCurrentDirection();// * (Math.Abs(Vector3.SignedAngle(_railManagement.GetCurrentDirection(), direction, Vector3.Cross(_railManagement.GetCurrentDirection(), direction))) > 90 ? -1 : 1);
        float force = Vector3.Project(direction, railDir).magnitude * Mathf.Sign(transform.InverseTransformDirection(direction).z);



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
        //ShakeDecoration(ref position, ref shakeRotation);

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
