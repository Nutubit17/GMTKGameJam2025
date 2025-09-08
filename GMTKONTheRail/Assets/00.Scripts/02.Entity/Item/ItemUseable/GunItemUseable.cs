using UnityEngine;

public class GunItemUseable : ItemUseableObject
{
    [SerializeField]
    protected Transform _muzzlePivot;
    [SerializeField]
    protected Projectile _projectile;

    [SerializeField]
    protected ItemSO _needAmmo;
    [SerializeField] 
    protected int _maxAmmo = 5;

    [SerializeField]
    protected float _seaGrapes = 0.01f, _Bulletcnt = 1;

    public override void Update()
    {
        _animator.SetFloat("Speed", _playerRi.linearVelocity.magnitude / _maxSpeed, 0.2f, 0.02f);
        _animator.SetFloat("RunSpeed", Mathf.Lerp(0.5f, 1.4f, _playerRi.linearVelocity.magnitude / _maxSpeed), 0.1f, 0.05f);
        _animator.SetBool("Attack", Mom.PlayerInput.IsFire & _itemType.Ammo >0);
        _animator.SetBool("AltAttack", Mom.PlayerInput.IsAltFire);
        if(_itemType.Ammo <= _maxAmmo & Input.GetKeyDown(KeyCode.R) &Mom.ScanAmmo(_needAmmo))
        {
            _animator.SetBool("Reload", Mom.RemoveAmmo(_needAmmo));

        }else
        {
            _animator.SetBool("Reload", false);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _animator.SetTrigger("Drop");
        }
    }

    public void AddAmmo()
    {
        _itemType.Ammo++;
        //Debug.Log(_itemType.Ammo);
    }

    public void ShootBullet()
    {
        
        if (_projectile == null)
            return;

        for(int i =0; i<_seaGrapes;i++)
        {
            Instantiate(_projectile.transform, _muzzlePivot.position, _muzzlePivot.rotation* Quaternion.Euler(Random.Range(-_seaGrapes,_seaGrapes), Random.Range(-_seaGrapes, _seaGrapes), Random.Range(-_seaGrapes, _seaGrapes)));

        }
        _itemType.Ammo--;
        // Debug.Log(_itemType.Ammo);

    }
}
