using UnityEngine;
using UnityEngine.UI;

public class VendingButten : MonoBehaviour, IAltInteractiveable
{
    public VendingMachine Mom;

    [SerializeField]
    protected SellingItem _item;
    [SerializeField]
    protected int _remainingCnt = 1;
    [SerializeField]
    protected ItemSO _itemSO;
    [SerializeField]
    protected RawImage _icon;
    [SerializeField]
    protected Image _pricePoints, _insertedPricePoints;
    [SerializeField]
    protected Vector2 _priceUISize = new (100f, 100f );

    public void Init(VendingMachine vendingMachineMom)
    {
        Mom = vendingMachineMom;

        if(_icon == null)
        _icon = GetComponent<RawImage>();

        if (_itemSO != null)
        {
            _item.SummonObj = _itemSO.Prefab.gameObject;
            _icon.texture = _itemSO.ItemIcon;
        }

        if (_pricePoints == null || _insertedPricePoints == null)
        {
            Debug.LogAssertion("_pricePoints or _insertedPricePoints Haldang!");
        }

        _pricePoints.rectTransform.sizeDelta = new Vector2 (_priceUISize.x*_item.Price, _pricePoints.rectTransform.sizeDelta.y);

        Mom.InsertCoinAction += AddCoin;
    }

    protected void AddCoin()
    {
        _insertedPricePoints.rectTransform.sizeDelta = new Vector2(Mathf.Clamp(_priceUISize.x * Mom.GetInsertedCoin(),0.01f,_priceUISize.x*_item.Price), _pricePoints.rectTransform.sizeDelta.y); 
    }

    public void UseAltInteractive(PlayerArm plarm)
    {
        if (_remainingCnt <= 0)
            return;

        if (Mom.GetInsertedCoin() >= _item.Price)
        {
            Mom.SellItem(_item);
            _remainingCnt--;
            if (_remainingCnt <= 0)
            {
                _icon.color = new Color(0.1f, 0.1f, 0.1f, 0.9f);
            }
        }
    }
}

