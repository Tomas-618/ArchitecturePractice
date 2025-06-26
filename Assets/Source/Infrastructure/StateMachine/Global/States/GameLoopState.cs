using System;
using Source.Infrastructure.StateMachine.Global.States.Contracts;
using Source.Services.Input.Contracts;
using UnityEngine;

namespace Source.Infrastructure.StateMachine.Global.States
{
    public class GameLoopState : IState
    {
        private readonly IInputService _inputService;

        public GameLoopState(IInputService inputService) =>
            _inputService = inputService ?? throw new ArgumentNullException(nameof(inputService));

        public void Enter()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _inputService.Enable();
        }

        public void Exit() =>
            Dispose();

        public void Dispose()
        {
        }
    }
}
