using System;

namespace Source.Infrastructure.StateMachine.Global.States.Contracts
{
    public interface IExitableState : IDisposable
    {
        void Exit();
    }
}
