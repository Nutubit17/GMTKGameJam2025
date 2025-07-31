
using UnityEngine;

public class ItemUseableObject : MonoBehaviour
{
    [SerializeField]
    protected ItemSO _itemType;

    public PlayerArm Mom;

    public virtual void Init(PlayerArm arm)
    {
        Mom = arm;

    }

    public ItemSO GetItemType()
    {
        return _itemType;
    }

    public virtual void UseItem()
    {

    }

    public virtual void AltUseItem()
    {

    }

    public virtual void AltAltUseItem()
    {

    }
}
