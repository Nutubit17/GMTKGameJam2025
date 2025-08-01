using UnityEngine;

public class ComsumeableItem : ItemUseableObject
{
    [SerializeField]
    private float _steminaAdd = 2;
    public void Comsume()
    {
        Mom.Mom.GetCompo<PlayerSatus>().AddMaxStemina(_steminaAdd);
        Mom.Mom.GetCompo<PlayerSatus>().AddStemina(_steminaAdd);
        Mom.EraseItem();
    }
}
