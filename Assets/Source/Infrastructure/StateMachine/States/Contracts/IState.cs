namespace Source.Infrastructure.StateMachine.States.Contracts
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}
