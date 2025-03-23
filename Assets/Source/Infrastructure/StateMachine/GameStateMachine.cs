using System;
using System.Collections.Generic;
using Source.Components.Curtain;
using Source.Infrastructure.Di;
using Source.Infrastructure.StateMachine.Contracts;
using Source.Infrastructure.StateMachine.States;
using Source.Infrastructure.StateMachine.States.Contracts;
using Source.Services.Factories;
using Source.Services.Progress.Contracts;
using Source.Services.Scenes.Contracts;

namespace Source.Infrastructure.StateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;

        private IExitableState _current;

        public GameStateMachine(DiContainer diContainer, ISceneLoader sceneLoader,
            CurtainLoader curtainLoader)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, diContainer),
                [typeof(LoadProgressState)] = new LoadProgressState(this,
                    diContainer.GetSingle<IPersistentProgressService>(),
                    diContainer.GetSingle<ISaveLoadService>()),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader,
                    diContainer.GetSingle<IPersistentProgressService>(),
                    diContainer.GetSingle<IProgressRegisterService>(), curtainLoader,
                    diContainer.GetSingle<PlayerFactory>()),
                [typeof(GameLoopState)] = new GameLoopState()
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState nextState = GetState<TState>();

            ChangeState(nextState);
            nextState.Enter();
        }

        public void Enter<TPayLoadedState, TPayload>(TPayload payload)
            where TPayLoadedState : class, IPayloadedState<TPayload>
        {
            IPayloadedState<TPayload> nextState = GetState<TPayLoadedState>();

            ChangeState(nextState);
            nextState.Enter(payload);
        }

        private void ChangeState(IExitableState state)
        {
            _current?.Exit();
            _current = state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
}
