using UnityEngine;

public class TPTrigger : MonoBehaviour
{
    [SerializeField]
    private Transform _tpPoint;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Cart"))
        {
            other.GetComponent<MartCart>().TP2Pos(_tpPoint.position);
        }
    }
}
