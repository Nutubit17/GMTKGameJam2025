using UnityEngine;
using static UnityEditor.Progress;

public class ItemObject : MonoBehaviour
{
    public ItemSO ItemSO;

    private Rigidbody _ri;

    [SerializeField]
    private string _collidewithTag = "Cart";
    public void Init(ItemSO itemSO)
    {
        ItemSO = itemSO;
    }
    private void OnEnable()
    {
        _ri = GetComponent<Rigidbody>();
        _ri.isKinematic = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //foreach (string item in _noCollision)
        //    if (collision.gameObject.CompareTag(item))
        //        return;
        if (collision.gameObject.CompareTag(_collidewithTag))
        {
            _ri.isKinematic = true;
            transform.parent = collision.transform;
        }

       
    }
}
