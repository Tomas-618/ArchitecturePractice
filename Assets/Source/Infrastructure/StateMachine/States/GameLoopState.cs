using Source.Infrastructure.StateMachine.States.Contracts;
using UnityEngine;

namespace Source.Infrastructure.StateMachine.States
{
    public class GameLoopState : IState
    {
        public void Enter() =>
            Cursor.lockState = CursorLockMode.Locked;

        public void Exit() { }
    }
}
