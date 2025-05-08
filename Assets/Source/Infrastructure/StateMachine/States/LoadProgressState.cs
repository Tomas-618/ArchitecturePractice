using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Source.Data;
using Source.Infrastructure.StateMachine.Contracts;
using Source.Infrastructure.StateMachine.States.Contracts;
using Source.Services.Progress.Contracts;
using Source.Services.Scenes.Constants;

namespace Source.Infrastructure.StateMachine.States
{
    public class LoadProgressState : IAsyncState
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

        public async UniTask EnterAsync(CancellationToken token)
        {
            LoadOrCreateProgress();

            await _gameStateMachine.EnterAsync<LoadLevelState, string>
                (_progressService.Progress.SceneName, token);
        }

        public void Exit()
        {
        }

        private void LoadOrCreateProgress()
        {
            _progressService.Progress = _saveLoadService.TryLoad(out var playerProgress)
                ? playerProgress
                : CreateProgress();
        }

        private PlayerProgress CreateProgress()
        {
            return new PlayerProgress
            {
                SceneName = ScenesNames.LaboratoryScene
            };
        }
    }
}
