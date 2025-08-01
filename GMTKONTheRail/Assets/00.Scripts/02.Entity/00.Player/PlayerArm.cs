using System.Collections.Generic;
using UnityEngine;

public class PlayerArm : MonoBehaviour,IGetCompoable,IAfterInitable
{
    [SerializeField]
    private LayerMask _whatIsInteractive;
    [SerializeField]
    private float _interactiveDistance = 2.2f;

    public Entity Mom;

    private Dictionary<ItemSO, ItemUseableObject> _itemComponents = new();

    [SerializeField] private ItemSO _nullItem;

    public ItemSO[] Inventory = new ItemSO[3];

    public int CurrentIdx = 0;

    public ItemUseableObject CurrentItem;

    public PlayerInputSO PlayerInput;

    public void Init(Entity agent)
    {
        Mom = agent;

    }

    public void AfterInit()
    {
        PlayerInput = (Mom as PlayerBash).PlayerInput;
        PlayerInput.OnClickAction += Interative;
        PlayerInput.OnAltClickAction += AltInterative;
        PlayerInput.OnAltAltClickAction += AltAltInterative;

        foreach(ItemUseableObject item in GetComponentsInChildren<ItemUseableObject>(true))
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
            {
                if (Inventory[CurrentIdx] == _nullItem)
                {
                    Inventory[CurrentIdx] = item.ItemSO;
                    Destroy(hit.transform.gameObject);
                }
                else if (Inventory[0] == _nullItem)
                {
                    Inventory[0] = item.ItemSO;
                    Destroy(hit.transform.gameObject);
                }
                else if (Inventory[1] == _nullItem)
                {
                    Inventory[1] = item.ItemSO;
                    Destroy(hit.transform.gameObject);
                }
                else if (Inventory[2] == _nullItem)
                {
                    Inventory[2] = item.ItemSO;
                    Destroy(hit.transform.gameObject);
                }
                SetHoldingItem();
            }
            

        }

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            CurrentIdx = 0;
            SetHoldingItem();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CurrentIdx = 1;
            SetHoldingItem();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CurrentIdx = 2;
            SetHoldingItem();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(CurrentItem.GetItemType().Prefab is not null)
            {
                ItemObject itemObj = Instantiate(CurrentItem.GetItemType().Prefab,transform.position + transform.forward*1.5f,Quaternion.identity);

                itemObj.Init(CurrentItem.GetItemType());

                Inventory[CurrentIdx] = _nullItem;
                SetHoldingItem();

            }

        }
    }


}
