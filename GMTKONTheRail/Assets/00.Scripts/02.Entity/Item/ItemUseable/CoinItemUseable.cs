using UnityEngine;

public class CoinItemUseable : ItemUseableObject
{
    [SerializeField]
    private LayerMask _whatIsInteractive;

    [SerializeField]
    public GameObject UIUI;

    public override void Update()
    {
        base.Update();
        bool sdaf= false;


        if (Physics.Raycast(transform.position, transform.forward, out var hit, 2.2f, _whatIsInteractive))
        {
            if (hit.transform.gameObject.TryGetComponent<ICoinUseable>(out ICoinUseable a))
                sdaf = true;


        }
        UIUI.SetActive(sdaf);
    }

    protected override void OnDisable()
    {
        UIUI.SetActive(false);
    }
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
