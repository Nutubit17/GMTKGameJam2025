using UnityEngine;
using UnityEngine.InputSystem;
[CreateAssetMenu(fileName = "InputSO", menuName = "SO/Input/InputSO")]
public class PlayerInputSO : ScriptableObject, Input2.IActionsActions,Input2.IUtilsActions
{
    private Input2 _input;
    public Vector2 mouseMov;
    public Vector2 movement;

    public bool isFire,isSliding;
    private void OnEnable()
    {
        if (_input == null)
        {
            _input = new Input2();
            _input.Actions.SetCallbacks(this);
            _input.Utils.SetCallbacks(this);
        }
        _input.Enable();
        
    }
    private void OnDisable()
    {
        DisableInput();
    }

    public void DisableInput()
    {
        _input.Disable();
        //_input.UI.Disable();
    }
    public void OnChange(InputAction.CallbackContext context)
    {
        
    }

    public void OnEsc(InputAction.CallbackContext context)
    {
        
    }

    public void OnMouseButton(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isFire = true;
        }
        if (context.canceled) 
        {
            isFire = false;
        }
    }

    public void OnMousePos(InputAction.CallbackContext context)
    {
        mouseMov = context.ReadValue<Vector2>();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    public void OnSliding(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isSliding = true;
        }
        if (context.canceled)
        {
            isSliding = false;
        }
        PlayerBash.Instance.onSlidingAction(isSliding);
    }

    public void OnSwap(InputAction.CallbackContext context)
    {
        
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.performed)
        PlayerBash.Instance.jumpInputAction?.Invoke();
    }
}
