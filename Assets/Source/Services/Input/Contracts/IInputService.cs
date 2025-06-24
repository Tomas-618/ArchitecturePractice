using System;
using UnityEngine;

namespace Source.Services.Input.Contracts
{
    public interface IInputService : IDisposable
    {
        event Action SavedButtonPressed;

        event Action CrouchButtonPressed;

        void Enable();

        Vector3 GetMoveDirection();
        
        bool CheckSprintButton();

        Vector2 GetRotation();
    }
}
