using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReaderSO", menuName = "Scriptable Objects/InputReaderSO")]
public class InputReaderSO : ScriptableObject, InputPlayer.IMovementActions, InputPlayer.IWModeActions
{
    private InputPlayer inputPlayer;
    public event Action OnJumpStarted, OnJumpCanceled, OnWMoving, OffWMoving;
    public event Action<Vector2> Moving, WMoving;

    
    private void OnEnable()
    {
        inputPlayer = new InputPlayer();
        inputPlayer.Movement.Enable();
        inputPlayer.WMode.Enable();
        inputPlayer.Movement.AddCallbacks(this);
        inputPlayer.WMode.AddCallbacks(this);
    }

    private void OnDisable()
    {
        inputPlayer.Movement.Disable();
        inputPlayer.WMode.Disable();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        Moving?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnJumpStarted?.Invoke();
        }
        else if (context.canceled)
        {
            OnJumpCanceled?.Invoke();
        }
    }

    public void OnSwim(InputAction.CallbackContext context)
    {
        WMoving?.Invoke(context.ReadValue<Vector2>());
        if (context.started){OnWMoving?.Invoke();}
        if (context.canceled){OffWMoving?.Invoke();}
        
    }
}
