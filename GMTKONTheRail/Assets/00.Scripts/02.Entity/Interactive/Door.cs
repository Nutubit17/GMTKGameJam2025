using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour, IAltInteractiveable,IKeyInteractiveable
{
    private bool isOpen = false;
    [SerializeField] private bool _isLocked = false;
    [SerializeField] private Transform _pivot;
    [SerializeField] private float _moveSpeed = 0.5f;

    [SerializeField] private string _code = "a";

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

    public bool UseKeyInteractive(PlayerArm plarm, string code)
    {
        if(_code == code && _isLocked)
        {
            _isLocked = false;
            return true;
        }
        return false;
    }
}
