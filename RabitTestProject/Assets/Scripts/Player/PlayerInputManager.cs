using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

namespace rabbit_game.player {
    public class PlayerInputManager : MonoBehaviour, Controls.IPlayerControllerActions {
        public Vector2 MovementValue { get; private set; }
        public Vector2 LookValue { get; private set; }

        public event Action jumpHandler;
        public bool runPress;
        public bool crouchPress;
        private Controls controls;
        private void Start() {
            controls = new Controls();
            controls.PlayerController.SetCallbacks(this);
            controls.PlayerController.Enable();
        }
        private void OnDestroy() {
            controls.PlayerController.Disable();
        }

        public void OnCamLook(InputAction.CallbackContext context) {
            LookValue = context.ReadValue<Vector2>();
        }

        public void OnCamZoom(InputAction.CallbackContext context) {
        }

        public void OnCrouch(InputAction.CallbackContext context) {

            if(context.started)
                crouchPress = true;
            else if(context.canceled)
                crouchPress = false;
        }

        public void OnJump(InputAction.CallbackContext context) {
            if (!context.performed) { return; }
            jumpHandler?.Invoke();
        }

        public void OnMove(InputAction.CallbackContext context) {
            MovementValue = context.ReadValue<Vector2>();
        }

        public void OnPull(InputAction.CallbackContext context) {
        }

        public void OnRun(InputAction.CallbackContext context) {
            if (context.started)
                runPress = true; 
            else if(context.canceled)
                runPress = false;
        }
    }
}