using System;
using UnityEngine;

[Serializable]
public struct SellingItem
{
    public SellingItem(int price1)
    {
        Price = 1;
        SummonObj = null;
    }
    public int Price;
    public GameObject SummonObj;
}

public class VendingMachine : MonoBehaviour,ICoinUseable
{
    [SerializeField]
    protected Transform _iteSpawnPoint;

    [SerializeField]
    protected int _insertedCoin =0;

    public Action InsertCoinAction;

    public ItemSO[] aaa;


    public int GetInsertedCoin()
    {
        return _insertedCoin;
    }
    private void Start()
    {
        foreach(VendingButten item in GetComponentsInChildren<VendingButten>())
        {
            item.Init(this);
        }
    }


    public void UseCoin()
    {
        _insertedCoin++;
        InsertCoinAction?.Invoke();
    }
    public void SellItem(SellingItem sellingItem)
    {
        _insertedCoin -= sellingItem.Price;
        InsertCoinAction?.Invoke();
        Instantiate(sellingItem.SummonObj, _iteSpawnPoint.position,_iteSpawnPoint.rotation);
    }
}
