using System;

namespace Source.Infrastructure.StateMachine.States.Contracts
{
    public interface IExitableState : IDisposable
    {
        void Exit();
    }
}
