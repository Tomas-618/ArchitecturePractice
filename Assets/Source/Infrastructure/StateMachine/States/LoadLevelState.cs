using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Source.Components.Curtain;
using Source.Components.Points;
using Source.Data;
using Source.Infrastructure.LifetimeScopes;
using Source.Infrastructure.StateMachine.Contracts;
using Source.Infrastructure.StateMachine.States.Contracts;
using Source.Services.Factories.Contracts;
using Source.Services.Progress.Contracts;
using Source.Services.Scenes.Contracts;
using VContainer;
using VContainer.Unity;

namespace Source.Infrastructure.StateMachine.States
{
    public class LoadLevelState : IPayloadedAsyncState<string>
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IProgressRegisterService _progressRegisterService;
        private readonly CurtainLoader _curtainLoader;
        private readonly IPlayerFactory _factory;

        public LoadLevelState(IGameStateMachine stateMachine, ISceneLoader sceneLoader,
            IPersistentProgressService persistentProgressService,
            IProgressRegisterService progressRegisterService,
            IPlayerFactory factory, CurtainLoader curtainLoader)
        {
            _stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
            _sceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));
            _persistentProgressService = persistentProgressService ??
                                         throw new ArgumentNullException(nameof(persistentProgressService));
            _progressRegisterService = progressRegisterService ??
                                       throw new ArgumentNullException(nameof(progressRegisterService));
            _curtainLoader = curtainLoader != null
                ? curtainLoader
                : throw new ArgumentNullException(nameof(curtainLoader));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public async UniTask EnterAsync(string sceneName, CancellationToken token)
        {
            _curtainLoader.Show();
            _progressRegisterService.Clear();

            await _sceneLoader.LoadAsync(sceneName, token);
            await OnLoadedAsync(token);
        }

        public void Exit() =>
            _curtainLoader.Hide();

        private async UniTask OnLoadedAsync(CancellationToken token)
        {
            await InitGameWorld(token);
            InformProgressLoaders();

            await _stateMachine.EnterAsync<GameLoopState>(token);
        }

        private void InformProgressLoaders() =>
            _progressRegisterService.Load(_persistentProgressService.Progress);

        private async UniTask InitGameWorld(CancellationToken token)
        {
            var container = LifetimeScope.Find<LevelLifetimeScope>().Container;

            var playerInitialPoint = container.Resolve<PlayerInitialPoint>();

            await _factory.CreateAsync(container, new SpawnData
            {
                Position = playerInitialPoint.Position,
                Rotation = playerInitialPoint.Rotation
            }, token);
        }
    }
}
