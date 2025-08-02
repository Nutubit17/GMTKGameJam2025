using UnityEngine;

public class KeyItem : ItemUseableObject
{
    [SerializeField]
    private LayerMask _whatIsInteractive;
    [SerializeField]
    private string _code = "a";
    public void UseKey()
    {
        if (Physics.Raycast(transform.position, transform.forward, out var hit, 2.2f, _whatIsInteractive))
        {
            if (hit.transform.gameObject.TryGetComponent<IKeyInteractiveable>(out IKeyInteractiveable a))
            {
                if(a.UseKeyInteractive(Mom, _code))
                Mom.EraseItem();
            }


        }
    }
}
