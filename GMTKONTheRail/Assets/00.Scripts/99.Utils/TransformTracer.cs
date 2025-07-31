using UnityEngine;

public class TransformTracer : MonoBehaviour
{
    [SerializeField]
    private Transform _targetTransform;
    private void LateUpdate()
    {
        _targetTransform.SetPositionAndRotation(transform.position, transform.rotation);
    }
}
