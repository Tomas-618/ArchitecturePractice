using System;
using Source.Infrastructure.StateMachine.LifeCycle.Contracts;

namespace Source.Infrastructure.StateMachine.LifeCycle
{
    public class GameLoopStateMachine : IGameLoopStateMachine
    {
        private GameLoopState _currentState;

        public GameLoopStateMachine(GameLoopState initialState)
        {
            _currentState = initialState ?? throw new ArgumentNullException(nameof(initialState));
            _currentState.Enter();
        }

        public void Update()
        {
            _currentState.Update();
            ChangeState();
        }

        private void ChangeState()
        {
            if (_currentState.TryGetNext(out var nextState))
                ChangeState(nextState);
        }

        private void ChangeState(GameLoopState nextState)
        {
            _currentState.Exit();
            _currentState = nextState;
            _currentState.Enter();
        }
    }
}
