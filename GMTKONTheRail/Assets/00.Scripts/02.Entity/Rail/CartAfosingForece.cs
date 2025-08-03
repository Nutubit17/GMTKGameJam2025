using UnityEngine;

public class CartAfosingForece : MonoBehaviour
{
    [SerializeField]
    private MartCart _martCart;
    [SerializeField]
    private float _forceMultipier =1;

    private void Start()
    {
        _martCart = GetComponent<MartCart>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Interactive"))
            return;
        _martCart.AddForce(-collision.impulse * _forceMultipier);
        //_martCart.AddForce((transform.position - collision.collider.transform.position).normalized *_forceMultipier);
    }
}
