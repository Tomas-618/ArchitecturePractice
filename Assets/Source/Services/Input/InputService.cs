using System;
using Source.Services.Input.Contracts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Source.Services.Input
{
    public class InputService : IInputService
    {
        private readonly InputSystemActions _inputActions;

        public InputService()
        {
            _inputActions = new InputSystemActions();

            _inputActions.Enable();
            AddListeners();
        }

        public event Action SavedButtonPressed;

        public void Dispose()
        {
            RemoveListeners();
            _inputActions.Disable();
        }

        public Vector3 GetMoveDirection()
        {
            Vector3 direction = _inputActions.Player.Move.ReadValue<Vector2>();

            direction.z = direction.y;
            direction.y = 0f;

            return direction;
        }

        public Vector2 GetRotation()
        {
            var rotation = _inputActions.Player.Look.ReadValue<Vector2>();

            return rotation;
        }

        private void OnSaveButtonPressed(InputAction.CallbackContext callbackContext) =>
            SavedButtonPressed?.Invoke();

        private void AddListeners() =>
            _inputActions.Player.Save.performed += OnSaveButtonPressed;

        private void RemoveListeners() =>
            _inputActions.Player.Save.performed -= OnSaveButtonPressed;
    }
}
