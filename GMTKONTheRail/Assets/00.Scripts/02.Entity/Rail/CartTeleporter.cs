using UnityEngine;

public class CartTeleporter : MonoBehaviour
{
     [SerializeField]
    private Transform _tpPoint;
    [SerializeField]
    private bool _isCartOnly;
    public void TeleportCart()
    {

        GameManager.Instance.GetCompo<RailManagement>().GetMartCart().TP2Pos(_tpPoint.position,_isCartOnly);
        
    }
}
