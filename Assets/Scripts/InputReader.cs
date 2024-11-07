using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public Action OnJump;
    public Action OnGrappling;
    public Action<Vector2> OnMove;
    public Action OnPause;
    public Action<Vector2> OnMoveCamera;

    public void HandleJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnJump?.Invoke();
        }
    }

    public void HandleGrapplingInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnGrappling?.Invoke();
        }
    }

    public void HandleMoveInput(InputAction.CallbackContext context)
    {
        OnMove?.Invoke(context.ReadValue<Vector2>());
    }

    public void HandlePauseInput(InputAction.CallbackContext context)
    {
        if (context.started) 
        { 
            OnPause?.Invoke();
        }
    }

    public void HandleCameraMoveInput(InputAction.CallbackContext context)
    {
        OnMoveCamera?.Invoke(context.ReadValue<Vector2>());
    }
}
