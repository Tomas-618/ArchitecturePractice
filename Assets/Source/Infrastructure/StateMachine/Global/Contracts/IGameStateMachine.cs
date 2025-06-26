using System;
using Source.Infrastructure.StateMachine.Global.States.Contracts;

namespace Source.Infrastructure.StateMachine.Global.Contracts
{
    public interface IGameStateMachine : IDisposable
    {
        void Enter<TState>() where TState : class, IState;

        void Enter<TPayLoadedState, TPayload>(TPayload payload)
            where TPayLoadedState : class, IPayloadedState<TPayload>;
    }
}
