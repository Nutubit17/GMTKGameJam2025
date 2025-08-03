using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour,IGetCompoable,IAfterInitable
{
    [SerializeField]
    private RawImage[] _image;

    [SerializeField]
    private PlayerBash _playerBash;
    [SerializeField]
    private PlayerArm _pLInventory;


    public void Init(Entity agent)
    {
        _playerBash = agent.GetComponent<PlayerBash>();
        
    }
    public void AfterInit()
    {


    }
    void Start()
    {
        _pLInventory = _playerBash.GetCompo<PlayerArm>();
        _pLInventory.ItemSet += Updater;
        Updater();
    }
    private void Updater()
    {
        for (int i = 0; i < _image.Length; i++)
        {
            _image[i].texture = _pLInventory.Inventory[i].ItemSO1.ItemIcon;
            
        }
    }


}
