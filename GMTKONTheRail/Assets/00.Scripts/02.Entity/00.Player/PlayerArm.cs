using System.Collections.Generic;
using UnityEngine;

public class PlayerArm : MonoBehaviour,IGetCompoable,IAfterInitable
{
    [SerializeField]
    private LayerMask _whatIsInteractive;
    [SerializeField]
    private float _interactiveDistance = 2.2f;

    private Entity _agent;

    private Dictionary<ItemSO, ItemUseableObject> _itemComponents = new();

    [SerializeField] private ItemSO _nullItem;

    public ItemSO[] Inventory = new ItemSO[3];

    public int CurrentIdx = 0;

    public ItemUseableObject CurrentItem;

    public void Init(Entity agent)
    {
        _agent = agent;

    }

    public void AfterInit()
    {
        PlayerInputSO playerInput = (_agent as PlayerBash).playerInput;
        playerInput.OnClickAction += Interative;
        playerInput.OnAltClickAction += AltInterative;
        playerInput.OnAltAltClickAction += AltAltInterative;

        foreach(ItemUseableObject item in GetComponentsInChildren<ItemUseableObject>())
        {
            _itemComponents.Add(item.GetItemType(),item);
            item.Init(this);
            item.gameObject.SetActive(false);
        }

        SetHoldingItem();
    }

    private void SetHoldingItem()
    {

        if(_itemComponents.TryGetValue(Inventory[CurrentIdx],out ItemUseableObject value))
        {
            CurrentItem?.gameObject.SetActive(false);
            CurrentItem = value;
            CurrentItem.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogAssertion("ItemSO Is Not Vailid!! Fuck Dick Shit!");
        }
    }

    private void Interative()
    {
        CurrentItem?.UseItem();
    }
    private void AltInterative()
    {
        CurrentItem?.AltUseItem();
    }
    private void AltAltInterative()
    {
        if(Physics.Raycast(transform.position,transform.forward,out var hit,2.2f,_whatIsInteractive))
        {
            if(hit.transform.gameObject.TryGetComponent<ItemObject>(out ItemObject item))
            if (Inventory[CurrentIdx] != _nullItem)
            {
                Inventory[CurrentIdx] = item.itemSO;
                Destroy(hit.transform.gameObject);
            }
            else if (Inventory[0] != null)
            {
                Inventory[0] = item.itemSO;
                Destroy(hit.transform.gameObject);
            }
            else if (Inventory[1] != null)
            {   
                Inventory[1] = item.itemSO;
                Destroy(hit.transform.gameObject);
            }
            else if (Inventory[2] != null)
            {
                Inventory[2] = item.itemSO;
                Destroy(hit.transform.gameObject);
            }
            SetHoldingItem();

        }

    }

    private void Update()
    {
        
    }

}
