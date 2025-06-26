using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Source.Infrastructure.StateMachine;
using Source.Infrastructure.StateMachine.Global;
using Source.Infrastructure.StateMachine.Global.Contracts;
using Source.Infrastructure.StateMachine.Global.States;
using Source.Services.Scenes.Constants;
using Source.Services.Scenes.Contracts;
using VContainer;
using VContainer.Unity;

namespace Source.Infrastructure
{
    public class Bootstrapper : IAsyncStartable, IDisposable
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;

        [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
        public Bootstrapper(IObjectResolver container)
        {
            if (container == null)
                throw new ArgumentNullException(nameof(container));

            _stateMachine = new GameStateMachine(container);
            _sceneLoader = container.Resolve<ISceneLoader>();
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            try
            {
                await _sceneLoader.LoadAsync(ScenesNames.InitialScene, cancellation);
            }
            catch (OperationCanceledException)
            {
            }

            if (cancellation.IsCancellationRequested == false)
                _stateMachine.Enter<LoadProgressState>();
        }

        public void Dispose() =>
            _stateMachine.Dispose();
    }
}
