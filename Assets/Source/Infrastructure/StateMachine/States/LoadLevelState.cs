using System;
using Source.Components.Camera;
using Source.Components.Curtain;
using Source.Components.Player;
using Source.Infrastructure.StateMachine.Contracts;
using Source.Infrastructure.StateMachine.States.Contracts;
using Source.Services.Factories;
using Source.Services.Progress.Contracts;
using Source.Services.Scenes.Contracts;
using UnityEngine;

namespace Source.Infrastructure.StateMachine.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";

        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IProgressRegisterService _progressRegisterService;
        private readonly CurtainLoader _curtainLoader;
        private readonly PlayerFactory _factory;

        public LoadLevelState(IGameStateMachine stateMachine, ISceneLoader sceneLoader,
            IPersistentProgressService persistentProgressService,
            IProgressRegisterService progressRegisterService, CurtainLoader curtainLoader,
            PlayerFactory factory)
        {
            _stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
            _sceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));
            _persistentProgressService = persistentProgressService ??
                throw new ArgumentNullException(nameof(persistentProgressService));
            _progressRegisterService = progressRegisterService ??
                throw new ArgumentNullException(nameof(progressRegisterService));
            _curtainLoader = curtainLoader != null ?
                curtainLoader : throw new ArgumentNullException(nameof(curtainLoader));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public void Enter(string sceneName)
        {
            _curtainLoader.Show();
            _progressRegisterService.Clear();
            _sceneLoader.LoadAsync(sceneName, OnLoaded);
        }

        public void Exit() =>
            _curtainLoader.Hide();

        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressLoaders();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InformProgressLoaders() =>
            _progressRegisterService.Load(_persistentProgressService.Progress);

        private void InitGameWorld()
        {
            Transform initialPoint = GameObject.FindWithTag(InitialPointTag).transform;

            PlayerRotator player = _factory.Create(initialPoint.position, initialPoint.rotation);

            SetCameraTarget(player.CameraTarget);
        }

        private void SetCameraTarget(Transform target)
        {
            if (Camera.main.TryGetComponent(out CameraFollower cameraFollower) == false)
                return;

            cameraFollower.Follow(target);
        }
    }
}
