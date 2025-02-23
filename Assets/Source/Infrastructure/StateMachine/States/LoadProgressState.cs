using System;
using Source.Data;
using Source.Infrastructure.StateMachine.Contracts;
using Source.Infrastructure.StateMachine.States.Contracts;
using Source.Services.Progress.Contracts;
using Source.Services.Scenes.Constants;

namespace Source.Infrastructure.StateMachine.States
{
    public class LoadProgressState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(IGameStateMachine gameStateMachine,
            IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine ?? throw new ArgumentNullException(nameof(gameStateMachine));
            _progressService = progressService ?? throw new ArgumentNullException(nameof(progressService));
            _saveLoadService = saveLoadService ?? throw new ArgumentNullException(nameof(saveLoadService));
        }

        public void Enter()
        {
            LoadOrCreateProgress();
            _gameStateMachine.Enter<LoadLevelState, string>(_progressService
                .Progress.WorldData.LevelData.SceneName);
        }

        public void Exit() { }

        private void LoadOrCreateProgress()
        {
            _progressService.Progress = _saveLoadService.TryLoad(out PlayerProgress progress)
                ? progress : CreateProgress();
        }

        private PlayerProgress CreateProgress() =>
            new(ScenesNames.LaboratoryScene);
    }
}