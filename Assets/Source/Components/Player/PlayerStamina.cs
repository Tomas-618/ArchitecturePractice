using R3;
using UnityEngine;

namespace Source.Components.Player
{
    public class PlayerStamina : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _increaseValue;
        [SerializeField, Min(0f)] private float _increaseDelay;

        private ReactiveProperty<float> _currentValue;
        private float _increaseTimer;
        private bool _isRestoring;

        [field: SerializeField, Min(0f)] public float MaxValue { get; private set; }

        public Observable<float> CurrentValue => _currentValue;

        public bool HasRunOut => _currentValue.Value <= 0;

        private void Awake()
        {
            _currentValue = new ReactiveProperty<float>(MaxValue);
            _isRestoring = true;
        }

        private void Update() =>
            Restore();

        private void Restore()
        {
            if (_isRestoring == false)
                return;

            if (HasRunOut && CheckRestoreCooldown() == false)
                return;

            _currentValue.Value = Mathf.MoveTowards(_currentValue.Value,
                MaxValue, _increaseValue * Time.deltaTime);
        }

        public void Reduce(float amount)
        {
            if (amount < 0)
                return;

            _isRestoring = false;
            _currentValue.Value = Mathf.MoveTowards(_currentValue.Value,
                0, amount * Time.deltaTime);
        }

        public void StartRestoring() =>
            _isRestoring = true;

        private bool CheckRestoreCooldown()
        {
            float time = Time.time;

            if (time - _increaseTimer < _increaseDelay)
                return false;

            _increaseTimer = time;

            return true;
        }
    }
}
