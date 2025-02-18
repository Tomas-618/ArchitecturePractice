using System;
using UnityEngine;

namespace Source.Services
{
    public class InputService : IDisposable
    {
        private readonly InputSystemActions _inputActions;

        public InputService()
        {
            _inputActions = new InputSystemActions();

            _inputActions.Enable();
        }

        public void Dispose() =>
            _inputActions.Disable();

        public Vector3 GetMoveDirection()
        {
            Vector3 direction = _inputActions.Player.Move.ReadValue<Vector2>();

            direction.z = direction.y;
            direction.y = 0f;

            return direction;
        }

        public Vector2 GetRotation()
        {
            Vector2 rotation = _inputActions.Player.Look.ReadValue<Vector2>();

            return rotation;
        }
    }
}
