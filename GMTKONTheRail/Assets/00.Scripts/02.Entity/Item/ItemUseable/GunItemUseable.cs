using UnityEngine;

public class GunItemUseable : ItemUseableObject
{
    [SerializeField]
    protected Transform _muzzlePivot;
    [SerializeField]
    protected Projectile _projectile;

    [SerializeField]
    protected float _seaGrapes = 0, _Bulletcnt = 1;

    public override void Update()
    {
        _animator.SetFloat("Speed", _playerRi.linearVelocity.magnitude / _maxSpeed, 0.1f, 0.05f);
        _animator.SetFloat("RunSpeed", Mathf.Lerp(0.5f, 1.4f, _playerRi.linearVelocity.magnitude / _maxSpeed), 0.1f, 0.05f);
        _animator.SetBool("Attack", Mom.PlayerInput.IsFire & _itemType.Ammo >0);
        _animator.SetBool("AltAttack", Mom.PlayerInput.IsAltFire);
        _animator.SetBool("Reload", Input.GetKeyDown(KeyCode.R));
    }

    public void ShootBullet()
    {
        
        if (_projectile == null)
            return;

        Instantiate(_projectile.transform, _muzzlePivot.position,_muzzlePivot.rotation);
    }
}
