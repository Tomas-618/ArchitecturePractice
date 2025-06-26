namespace Source.Infrastructure.StateMachine.Global.States.Contracts
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}
