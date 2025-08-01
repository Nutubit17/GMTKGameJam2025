using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public enum InputType
{
    Movement,MouseX,MouseY,Click,AltClick
}

public class PlayerBash : Entity
{
    public static PlayerBash Instance;
    public Dictionary<InputType, UnityAction<InputAction.CallbackContext>> Inputs;

    public PlayerInputSO PlayerInput;
    public PlayerMoveCompo playerMovement;
    public UnityAction jumpInputAction;
    public UnityAction<bool> onSlidingAction;

    protected override void Awake()
    {
        
        Instance = this;
        //playerInput = GetComponent<PlayerInputSO>();
        base.Awake();
        playerMovement = GetComponent<PlayerMoveCompo>();
    }

    
}
