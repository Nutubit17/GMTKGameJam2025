using UnityEngine;

public class DistanceLight : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve _curve;
    [SerializeField]
    private Light _light;
    [SerializeField]
    private float _intensity,_maxDistance = 25;
    void Update()
    {
        _light.intensity = _intensity * _curve.Evaluate(Vector3.Distance(transform.position, Camera.main.transform.position)/_maxDistance);
    }
}
