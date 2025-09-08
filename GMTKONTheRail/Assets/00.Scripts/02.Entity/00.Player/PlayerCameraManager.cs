using UnityEngine;

public class PlayerCameraManager : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private PlayerMoveCompo PlayerMoveComp;

    [SerializeField] private float tiltAmount = 5f; // �ִ� ���� ����
    [SerializeField] private float tiltSpeed = 5f;  // ���� �ӵ�

    private Quaternion targetRotation;

    void Update()
    {
        Vector3 localVelocity = _camera.transform.InverseTransformDirection(PlayerMoveComp.rigidCompo.linearVelocity);
        float horizontalSpeed = localVelocity.x;
        float tilt = Mathf.Clamp(horizontalSpeed, -1f, 1f) * tiltAmount;

        targetRotation = Quaternion.Euler(0f, 0f, -tilt);
        _camera.transform.localRotation = Quaternion.Lerp(_camera.transform.localRotation, targetRotation, Time.deltaTime * tiltSpeed);
    }
}