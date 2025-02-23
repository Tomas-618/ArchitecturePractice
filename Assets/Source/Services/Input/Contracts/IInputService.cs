using System;
using UnityEngine;

namespace Source.Services.Input.Contracts
{
    public interface IInputService : IDisposable
    {
        event Action SavedButtonPressed;

        Vector3 GetMoveDirection();

        Vector2 GetRotation();
    }
}
