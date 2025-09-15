using UnityEngine;

public class ItemObject : MonoBehaviour,IInteractiveable
{
    public ItemDataAndSO ItemSO;
    //public  ItemDataAndSO;

    private Rigidbody _ri;

    [SerializeField]
    private string _collidewithTag = "Cart";
    public void Init(ItemDataAndSO itemSO)
    {
        ItemSO = itemSO;
    }

    public ItemDataAndSO Intreractive()
    {
        Destroy(gameObject,0.01f);
        return ItemSO;
    }

    private void OnEnable()
    {
        _ri = GetComponent<Rigidbody>();
        _ri.isKinematic = false;
    }

    private void OnTriggerEnter(Collider collision)
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
