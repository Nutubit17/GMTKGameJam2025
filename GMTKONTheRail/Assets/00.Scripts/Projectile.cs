using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    protected float _speed = 100f, _damage = 1, _time = 10f;
    [SerializeField]
    protected LayerMask _whatIsTarget;
    [SerializeField] protected ParticleSystem _particleSystem;

    private void FixedUpdate()
    {
        //Debug.Log("Shit");
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _speed * Time.fixedDeltaTime, _whatIsTarget,QueryTriggerInteraction.Ignore))
        {
            if (hit.transform.gameObject.TryGetComponent<IGetDamageable>(out IGetDamageable damgeable))
            {
                damgeable.Damage(_damage);
                
            }

            if(_particleSystem != null)
            Instantiate(_particleSystem, hit.point, Quaternion.LookRotation(hit.normal)).Play();

            //Debug.Log(hit.collider.gameObject.name);

            Destroy(gameObject);
        }

        transform.position = transform.position + transform.forward * _speed * Time.fixedDeltaTime;

        _time -= Time.fixedDeltaTime;

        if (_time < 0)
        { 
        Destroy(gameObject);
        }
    }
}
