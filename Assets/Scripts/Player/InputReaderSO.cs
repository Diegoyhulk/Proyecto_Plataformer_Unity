using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReaderSO", menuName = "Scriptable Objects/InputReaderSO")]
public class InputReaderSO : ScriptableObject, InputPlayer.IMovementActions
{
    private InputPlayer inputPlayer;
    public event Action OnJumpStarted, OnJumpCanceled;
    public event Action<Vector2> Moving;
    
    private void OnEnable()
    {
        inputPlayer = new InputPlayer();
        inputPlayer.Movement.Enable();
        inputPlayer.UI.Disable();
        inputPlayer.Movement.AddCallbacks(this);
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
}
