using System;
using System.Collections.Generic;
using Source.Components.Curtain;
using Source.Infrastructure.StateMachine.Global.Contracts;
using Source.Infrastructure.StateMachine.Global.States;
using Source.Infrastructure.StateMachine.Global.States.Contracts;
using Source.Services.Factories.Contracts;
using Source.Services.Input.Contracts;
using Source.Services.Progress.Contracts;
using Source.Services.Scenes.Contracts;
using VContainer;

namespace Source.Infrastructure.StateMachine.Global
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;

        private IExitableState _current;

        public GameStateMachine(IObjectResolver container)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(LoadProgressState)] = new LoadProgressState(this,
                    container.Resolve<IPersistentProgressService>(),
                    container.Resolve<ISaveLoadService>()),
                [typeof(LoadLevelState)] = new LoadLevelState(this,
                    container.Resolve<ISceneLoader>(),
                    container.Resolve<IPersistentProgressService>(),
                    container.Resolve<IProgressRegisterService>(),
                    container.Resolve<IPlayerFactory>(),
                    container.Resolve<IHudFactory>(),
                    container.Resolve<CurtainLoader>()),
                [typeof(GameLoopState)] = new GameLoopState
                    (container.Resolve<IInputService>())
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

        public void Dispose() =>
            _current?.Dispose();

        private void ChangeState(IExitableState state)
        {
            _current?.Exit();
            _current = state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
}
