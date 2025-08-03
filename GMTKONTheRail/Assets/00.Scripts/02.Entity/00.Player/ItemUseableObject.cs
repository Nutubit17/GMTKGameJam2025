
using System;
using UnityEngine;

[Serializable]
public class ItemDataAndSO
{
    public ItemSO ItemSO1;
    public int Ammo;
    public int Durability;
}

public class ItemUseableObject : MonoBehaviour
{
    [SerializeField]
    protected ItemDataAndSO _itemType;

    public PlayerArm Mom;

    [SerializeField]
    protected Animator _animator;

    protected Rigidbody _playerRi;

    [SerializeField]
    protected float _maxSpeed =4.5f;

    public virtual void Init(PlayerArm arm)
    {
        Mom = arm;

    }

    public virtual void Start()
    {
        _playerRi = Mom.Mom.GetComponent<Rigidbody>();
    }
    public ItemSO GetItemType()
    {
        return _itemType.ItemSO1;
    }

    public ItemDataAndSO GetItemData()
    {
        return _itemType;
    }

    public virtual void Update()
    {
        _animator.SetFloat("Speed", _playerRi.linearVelocity.magnitude / _maxSpeed, 0.1f, 0.05f);
        _animator.SetFloat("RunSpeed", Mathf.Lerp(0.5f,1.4f,_playerRi.linearVelocity.magnitude / _maxSpeed), 0.1f, 0.05f);
        _animator.SetBool("Attack",Mom.PlayerInput.IsFire);
        _animator.SetBool("AltAttack",Mom.PlayerInput.IsAltFire);
        //_animator.SetBool("Reload", Input.GetKeyDown(KeyCode.R));
    }

    protected virtual void OnEnable()
    {
        if (!_animator) return;
        _animator.Rebind();
        _animator.Update(0f);
    }
   protected virtual void OnDisable()
    {
        if (!_animator) return;
        _animator.Rebind();
        _animator.Update(0f);
    }
    public virtual void UseItem()
    {
        //_animator.SetBool("Fire")
    }

    public virtual void AltUseItem()
    {

    }

    public virtual void AltAltUseItem()
    {

    }
}
