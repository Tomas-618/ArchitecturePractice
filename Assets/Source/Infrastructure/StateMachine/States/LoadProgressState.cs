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
    public class LoadProgressState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        private CancellationTokenSource _cancellationTokenSource;

        public LoadProgressState(IGameStateMachine gameStateMachine,
            IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine ?? throw new ArgumentNullException(nameof(gameStateMachine));
            _progressService = progressService ?? throw new ArgumentNullException(nameof(progressService));
            _saveLoadService = saveLoadService ?? throw new ArgumentNullException(nameof(saveLoadService));
        }

        public void Enter() =>
            PrepareProgressAsync().Forget();

        public void Exit() =>
            Dispose();

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }

        private async UniTaskVoid PrepareProgressAsync()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            await LoadOrCreateProgressAsync(_cancellationTokenSource.Token);

            _gameStateMachine.Enter<LoadLevelState, string>
                (_progressService.Progress.SceneName);
        }

        private async UniTask LoadOrCreateProgressAsync(CancellationToken cancellationToken)
        {
            var progress = await _saveLoadService.LoadAsync(cancellationToken);

            _progressService.Progress = progress ?? CreateProgress();
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
