using UnityEngine;

public class HpCheckWithLight : MonoBehaviour
{
    [SerializeField] private float _lightCheckRange = 5;
    [SerializeField] private LayerMask _lightLayer;
    [SerializeField] private PlayerBash _playerBash;

    void Awake()
    {
        _playerBash = GetComponent<PlayerBash>();
    }

    private Collider[] _lightobjarr = new Collider[10];

    private void FixedUpdate()
    {
        var hit = Physics.OverlapSphereNonAlloc
        (_playerBash.transform.position, _lightCheckRange, _lightobjarr, _lightLayer);

        if (hit == 0)
        {
            
        }
        else
        {
            
        }
    }

}
