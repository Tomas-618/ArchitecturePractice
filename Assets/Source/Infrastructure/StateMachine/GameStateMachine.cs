using System;
using Source.Components.Curtain;
using System.Collections.Generic;
using Source.Infrastructure.Di;
using Source.Infrastructure.StateMachine.Contracts;
using Source.Infrastructure.StateMachine.States;
using Source.Infrastructure.StateMachine.States.Contracts;
using Source.Services;
using Source.Services.AssetManagement;
using Source.Services.Factories;

namespace Source.Infrastructure.StateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;

        private IExitableState _current;

        public GameStateMachine(DiContainer diContainer, SceneLoader sceneLoader,
            CurtainLoader curtainLoader)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, diContainer),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, curtainLoader,
                    diContainer.GetSingle<GameObjectsFactory>(), diContainer.GetSingle<AssetProvider>()),
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
