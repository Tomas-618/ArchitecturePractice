using Assets.Sources.Initialization.StateMachine.States.Contracts;
using UnityEngine;

namespace Assets.Sources.Initialization.StateMachine.States
{
    public class GameLoopState : IState
    {
        public void Enter() =>
            Cursor.lockState = CursorLockMode.Locked;

        public void Exit() { }
    }
}
