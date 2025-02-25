using System;
using Source.Infrastructure.Di;
using Source.Infrastructure.StateMachine.Contracts;
using Source.Infrastructure.StateMachine.States.Contracts;
using Source.Services.AssetManagement;
using Source.Services.AssetManagement.Contracts;
using Source.Services.Factories;
using Source.Services.Input;
using Source.Services.Input.Contracts;
using Source.Services.Progress;
using Source.Services.Progress.Contracts;
using Source.Services.Scenes.Constants;
using Source.Services.Scenes.Contracts;

namespace Source.Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly DiContainer _diContainer;

        public BootstrapState(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader, DiContainer diContainer)
        {
            _gameStateMachine = gameStateMachine ?? throw new ArgumentNullException(nameof(gameStateMachine));
            _sceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));
            _diContainer = diContainer ?? throw new ArgumentNullException(nameof(diContainer));

            RegisterServices();
        }

        public void Enter() =>
            _sceneLoader.LoadAsync(ScenesNames.InitialScene, EnterLoadProgress);

        public void Exit() { }

        private void RegisterServices()
        {
            _diContainer.RegisterSingle<IInputService>(new InputService());
            _diContainer.RegisterSingle<IAssetProvider>(new AssetProvider());
            _diContainer.RegisterSingle(_sceneLoader);
            _diContainer.RegisterSingle<IProgressRegisterService>(new ProgressRegisterService());
            _diContainer.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _diContainer.RegisterSingle<ISaveLoadService>(new SaveLoadService
                (_diContainer.GetSingle<IPersistentProgressService>(),
                _diContainer.GetSingle<IProgressRegisterService>()));
            _diContainer.RegisterSingle(new PlayerFactory(_diContainer.GetSingle<IAssetProvider>(),
                _diContainer.GetSingle<IProgressRegisterService>()));
        }

        private void EnterLoadProgress() =>
            _gameStateMachine.Enter<LoadProgressState>();
    }
}
