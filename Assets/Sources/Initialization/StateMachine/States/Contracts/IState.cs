namespace Assets.Sources.Initialization.StateMachine.States.Contracts
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}
