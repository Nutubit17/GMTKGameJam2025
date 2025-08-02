using UnityEngine;

public class CoinItemUseable : ItemUseableObject
{
    [SerializeField]
    private LayerMask _whatIsInteractive;
    public void UseCoin()
    {
        if (Physics.Raycast(transform.position, transform.forward, out var hit, 2.2f, _whatIsInteractive))
        {
            if (hit.transform.gameObject.TryGetComponent<ICoinUseable>(out ICoinUseable a))
            {
                a.UseCoin();

                Mom.EraseItem();
            }


        }
    }
}
