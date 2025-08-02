using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public enum InputType
{
    Movement,MouseX,MouseY,Click,AltClick
}

public class PlayerBash : Entity
{

    public Dictionary<InputType, UnityAction<InputAction.CallbackContext>> Inputs;

    public PlayerInputSO PlayerInput;
    public PlayerMoveCompo playerMovement;


    protected override void Awake()
    {
        

        //playerInput = GetComponent<PlayerInputSO>();
        base.Awake();
        playerMovement = GetComponent<PlayerMoveCompo>();
    }

    private void Start()
    {
        if(GameManager.Instance != null) 
        GameManager.Instance.PlayerInstance = this;
    }

}
