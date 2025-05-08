using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Source.Components.Curtain;
using Source.Infrastructure.StateMachine.Contracts;
using Source.Infrastructure.StateMachine.States;
using Source.Infrastructure.StateMachine.States.Contracts;
using Source.Services.Factories.Contracts;
using Source.Services.Progress.Contracts;
using Source.Services.Scenes.Contracts;
using VContainer;

namespace Source.Infrastructure.StateMachine
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
                    container.Resolve<CurtainLoader>()),
                [typeof(GameLoopState)] = new GameLoopState()
            };
        }

        public async UniTask EnterAsync<TState>(CancellationToken token) where TState : class, IAsyncState
        {
            IAsyncState nextAsyncState = GetState<TState>();

            ChangeState(nextAsyncState);
            await nextAsyncState.EnterAsync(token);
        }

        public async UniTask EnterAsync<TPayLoadedState, TPayload>(TPayload payload, CancellationToken token)
            where TPayLoadedState : class, IPayloadedAsyncState<TPayload>
        {
            IPayloadedAsyncState<TPayload> nextState = GetState<TPayLoadedState>();

            ChangeState(nextState);
            await nextState.EnterAsync(payload, token);
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
