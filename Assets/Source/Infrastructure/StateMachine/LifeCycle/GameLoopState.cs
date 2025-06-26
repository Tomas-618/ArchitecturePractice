using System;
using System.Collections.Generic;

namespace Source.Infrastructure.StateMachine.LifeCycle
{
    public abstract class GameLoopState
    {
        private readonly List<GameLoopTransition> _transitions;

        protected GameLoopState(int transitionsCapacity)
        {
            if (transitionsCapacity < 0)
                throw new ArgumentOutOfRangeException(nameof(transitionsCapacity));

            _transitions = new List<GameLoopTransition>(transitionsCapacity);
        }

        public void AddTransition(GameLoopTransition transition) =>
            _transitions.Add(transition);

        public bool TryGetNext(out GameLoopState nextState)
        {
            nextState = null;

            for (int i = 0; i < _transitions.Count; i++)
            {
                if (_transitions[i].TryGetNextState(out nextState))
                    return true;
            }

            return false;
        }

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }

        public virtual void Update()
        {
        }
    }
}
