using UnityEngine;

public class CartAfosingForece : MonoBehaviour
{
    [SerializeField]
    private MartCart _martCart;
    [SerializeField]
    private float _forceMultipier =1;

    private void Start()
    {
        if(_martCart == null)
        _martCart = GetComponent<MartCart>();
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Interactive"))
    //        return;
    //    _martCart.AddForce(-collision.impulse * _forceMultipier);
    //    //_martCart.AddForce((transform.position - collision.collider.transform.position).normalized *_forceMultipier);
    //}
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Interactive"))
            return;
        _martCart.AddForce(-collision.impulse * _forceMultipier*Time.fixedDeltaTime);
        //_martCart.AddForce((transform.position - collision.collider.transform.position).normalized *_forceMultipier);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Interactive"))
            return;
        _martCart.AddForce(_forceMultipier*Time.fixedDeltaTime);
    }
}
