using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour, IAltInteractiveable
{
    private bool isOpen = false;
    [SerializeField] private bool _isLocked = false;
    [SerializeField] private Transform _pivot;
    [SerializeField] private float _moveSpeed = 0.5f;
    

    private bool _isDoorMoving = false;
    private IEnumerator _doorMoveRoutine;


    public void Unlock()
    {
        _isLocked = true;
    }

    public void UseAltInteractive(PlayerArm plarm)
    {
        if (_isLocked)
        {
            // 잠긴 소리
            return;
        }

        if (!_isDoorMoving)
            {
                _doorMoveRoutine = DoorMoveRoutine(isOpen = !isOpen);
                StartCoroutine(_doorMoveRoutine);
            }

    }

    public IEnumerator DoorMoveRoutine(bool isOpen)
    {
        float percent = 0;

        _isDoorMoving = true;

        Vector3 begin = Vector3.zero;
        Vector3 end = Vector3.up * 90;

        if (!isOpen) // swap
            (begin, end) = (end, begin);

        while (percent <= 1)
            {
                _pivot.eulerAngles = Vector3.Lerp(begin, end, percent);
                percent += _moveSpeed * Time.deltaTime;
                yield return null;
            }

        _isDoorMoving = false;
    }
}
