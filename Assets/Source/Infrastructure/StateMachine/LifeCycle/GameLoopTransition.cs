using System;

namespace Source.Infrastructure.StateMachine.LifeCycle
{
    public abstract class GameLoopTransition
    {
        private readonly GameLoopState _nextState;

        protected GameLoopTransition(GameLoopState nextState) =>
            _nextState = nextState ?? throw new ArgumentNullException(nameof(nextState));

        public bool TryGetNextState(out GameLoopState nextState)
        {
            nextState = null;

            if (CheckCondition() == false)
                return false;

            nextState = _nextState;

            return true;
        }

        protected abstract bool CheckCondition();
    }
}
