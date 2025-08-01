using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public ItemSO ItemSO;

    public void Init(ItemSO itemSO)
    {
        ItemSO = itemSO;
    }
}
