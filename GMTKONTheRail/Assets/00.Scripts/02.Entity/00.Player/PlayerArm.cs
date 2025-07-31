using UnityEngine;

public class PlayerArm : MonoBehaviour,IGetCompoable,IAfterInitable
{
    [SerializeField]
    private LayerMask _whatIsInteractive;

    private Entity _agent;

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
    }

    void Interative()
    {

    }
    void AltInterative()
    {

    }
    void AltAltInterative()
    {

    }

}
