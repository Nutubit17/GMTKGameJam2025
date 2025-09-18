using System;
using Unity.Collections;
using UnityEngine;

[Serializable]
struct TPSet
{
    public TPTrigger tpTrigger;
    public Transform tpPos;
}

public class RailSwitchLever : MonoBehaviour, IInteractiveable
{
    [SerializeField]
    private GameObject[] _triggers = new GameObject[2];
    [SerializeField]
    private bool _isRight = false;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private string _animparamName = "IsRight";
    [SerializeField,ReadOnly]
    private int _animparamIndex = 0;
    private void OnValidate() // 갑 변경 시 호출.
    {
        _animparamIndex = Animator.StringToHash(_animparamName);
    }

    void Start()
    {
        SetTriggerState();
    }

    public ItemDataAndSO Intreractive()
    {
        _isRight = !_isRight;
        SetTriggerState();

        return null;
    }

    private void SetTriggerState()
    {
        _triggers[_isRight ? 1 : 0].SetActive(false);
        _triggers[_isRight ? 0 : 1].SetActive(true);
        _animator.SetBool(_animparamIndex, _isRight);
    }



}
