using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace Source.Components.Player
{
    public class PlayerStamina : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _increaseValue;
        [SerializeField, Min(0f)] private float _increaseDelay;

        private CancellationTokenSource _cancellationTokenSource;
        private ReactiveProperty<float> _currentValue;

        [field: SerializeField, Min(0f)] public float MaxValue { get; private set; }

        public Observable<float> CurrentValue => _currentValue;

        public bool HasRunOut => _currentValue.Value <= 0;

        private void Awake() =>
            _currentValue = new ReactiveProperty<float>(MaxValue);

        private void OnDisable() =>
            DisposeCancellationTokenSource();

        public void Reduce(float amount)
        {
            if (amount < 0)
                return;

            DisposeCancellationTokenSource();
            _currentValue.Value = Mathf.Max(_currentValue.Value - amount * Time.deltaTime, 0);
        }

        public void StartRestoring()
        {
            if (_cancellationTokenSource != null)
                return;

            RestoreAsync().Forget();
        }

        private async UniTaskVoid RestoreAsync()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            await ProcessRestoring(_cancellationTokenSource.Token);
            DisposeCancellationTokenSource();
        }

        private async UniTask ProcessRestoring(CancellationToken token)
        {
            if (HasRunOut)
                await UniTask.WaitForSeconds(_increaseDelay, cancellationToken: token);

            while (_currentValue.Value < MaxValue)
            {
                _currentValue.Value = Mathf.Min(_currentValue.Value + _increaseValue * Time.deltaTime, MaxValue);
                await UniTask.NextFrame(cancellationToken: token);
            }
        }

        private void DisposeCancellationTokenSource()
        {
            if (_cancellationTokenSource == null)
                return;

            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
        }
    }
}
