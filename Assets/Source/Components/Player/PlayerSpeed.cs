using UnityEngine;

namespace Source.Components.Player
{
    public class PlayerSpeed : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _onMove;
        [SerializeField, Min(0f)] private float _onRunning;
        [SerializeField, Min(0f)] private float _onCrouch;

        [SerializeField] private PlayerRun _playerRun;
        [SerializeField] private PlayerCrouch _playerCrouch;

        [field: SerializeField, Min(0f)] public float SqrSpeedToReduceStamina { get; private set; }

        private void OnValidate()
        {
            _onCrouch = Mathf.Min(_onMove, _onRunning, _onCrouch);
            _onRunning = Mathf.Max(_onMove, _onRunning, _onCrouch);
        }

        public float GetCurrent()
        {
            if (_playerRun.CheckRunning())
                return _onRunning;

            if (_playerCrouch.CheckCrouching())
                return _onCrouch;

            return _onMove;
        }
    }
}
