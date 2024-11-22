using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public Action OnJump;
    public Action OnGrappling;
    public Action<Vector2> OnMove;
    public Action OnPause;
    public Action OnStartGame;
    public Action OnExitGame;
    public Action<Vector2> OnMoveCamera;
    public Action OnNavigation;
    public static bool isUsingController = false;

    public void HandleUIGamepadNavigation(InputAction.CallbackContext context)
    {
         OnNavigation?.Invoke();
    }

    public void HandleInputSourceChange(PlayerInput context)
    {
        if (context.devices[0] is Mouse || context.devices[0] is Keyboard)
        {
            isUsingController = false;
        }
        else if (context.devices[0] is Gamepad)
        {
            isUsingController = true;
        }
    }

    public void HandleStartGameInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnStartGame?.Invoke();
        }
    }

    public void HandleStartGameESCInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnExitGame?.Invoke();
        }
    }

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
