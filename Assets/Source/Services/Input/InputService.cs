using System;
using JetBrains.Annotations;
using Source.Services.Input.Contracts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Source.Services.Input
{
    public class InputService : IInputService
    {
        private const float ThresholdDelay = 0.4f;

        private readonly InputSystemActions _inputActions;

        private float _lastStepTime;

        [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
        public InputService() =>
            _inputActions = new InputSystemActions();

        public event Action SavedButtonPressed;

        public event Action CrouchButtonPressed;

        public void Enable()
        {
            _inputActions.Enable();
            AddListeners();
        }

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

        public bool CheckSprintButton() =>
            _inputActions.Player.Sprint.IsPressed();

        public Vector2 GetRotation()
        {
            var rotation = _inputActions.Player.Look.ReadValue<Vector2>();

            return rotation;
        }

        private void OnSaveButtonPressed(InputAction.CallbackContext callbackContext) =>
            SavedButtonPressed?.Invoke();

        private void OnCrouchButtonPressed(InputAction.CallbackContext callbackContext)
        {
            if (CheckCooldown())
                CrouchButtonPressed?.Invoke();
        }

        private bool CheckCooldown()
        {
            float time = Time.time;

            if (time - _lastStepTime < ThresholdDelay)
                return false;

            _lastStepTime = time;

            return true;
        }

        private void AddListeners()
        {
            _inputActions.Player.Save.performed += OnSaveButtonPressed;
            _inputActions.Player.Crouch.performed += OnCrouchButtonPressed;
        }

        private void RemoveListeners()
        {
            _inputActions.Player.Save.performed -= OnSaveButtonPressed;
            _inputActions.Player.Crouch.performed -= OnCrouchButtonPressed;
        }
    }
}
