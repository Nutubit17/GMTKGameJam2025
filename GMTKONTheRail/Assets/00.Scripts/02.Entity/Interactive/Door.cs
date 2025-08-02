using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour, IAltInteractiveable
{
    private bool isOpen = false;
    [SerializeField] private Collider _collider;
    [SerializeField] private Transform _visual;
    [SerializeField] private float _moveTime = 0.5f;

    private bool _isDoorMoving = false;
    private IEnumerator _doorMoveRoutine;
    

    public void UseAltInteractive(PlayerArm plarm)
    {
        if (!_isDoorMoving)
        {
            _doorMoveRoutine = DoorMoveRoutine(isOpen = !isOpen);
            StartCoroutine(_doorMoveRoutine);
        }
        
    }

    public IEnumerator DoorMoveRoutine(bool isOpen)
    {
        Quaternion rotation = Quaternion.AngleAxis(Time.deltaTime * (isOpen ? 1 : -1), Vector3.up);
        float percent = 0;

        _isDoorMoving = true;
        _collider.enabled = false;

        while (percent <= 1)
        {
            transform.rotation *= rotation;

            percent += _moveTime * Time.deltaTime;
            yield return null;
        }
        _collider.enabled = true;

        _isDoorMoving = false;
    }
}
