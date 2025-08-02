
using UnityEngine;

public class ItemUseableObject : MonoBehaviour
{
    [SerializeField]
    protected ItemSO _itemType;

    public PlayerArm Mom;

    [SerializeField]
    private Animator _animator;

    private Rigidbody _playerRi;

    [SerializeField]
    private float _maxSpeed = 15;

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
        return _itemType;
    }

    public virtual void Update()
    {
        _animator.SetFloat("Speed", _playerRi.linearVelocity.magnitude / _maxSpeed, 0.1f, 0.05f);
        _animator.SetFloat("RunSpeed", Mathf.Lerp(0.5f,1.4f,_playerRi.linearVelocity.magnitude / _maxSpeed), 0.1f, 0.05f);
        _animator.SetBool("Attack",Mom.PlayerInput.IsFire);
        _animator.SetBool("AltAttack",Mom.PlayerInput.IsAltFire);
        _animator.SetBool("Reload", Input.GetKeyDown(KeyCode.R));
    }
    void OnDisable()
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
