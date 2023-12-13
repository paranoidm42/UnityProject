using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.EnhancedTouch;

public class PlayerInputManager : MonoBehaviour, Controller.IPlayerControllerActions
{
    public Vector2 move;
    public static event Action bostCarHandler;
    public bool jumpCar;
    private Controller controls;

    private void Start()
    {
        controls = new Controller();
        controls.PlayerController.SetCallbacks(this);
        controls.PlayerController.Enable();
    }
    private void OnDestroy()
    {
        controls.PlayerController.Disable();
    }

    public void OnBostCar(InputAction.CallbackContext context)
    {
        if(!context.performed) { return; }
        bostCarHandler?.Invoke();

    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnJumpCar(InputAction.CallbackContext context)
    {
        jumpCar = context.ReadValueAsButton();
    }
}
