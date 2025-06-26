using UnityEngine;

namespace Source.Components.Player
{
    public class PlayerSpeed : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _onWalking;
        [SerializeField, Min(0f)] private float _onRunning;
        [SerializeField, Min(0f)] private float _onCrouching;

        public float CurrentSpeed { get; private set; }

        private void OnValidate()
        {
            _onCrouching = Mathf.Min(_onWalking, _onRunning, _onCrouching);
            _onRunning = Mathf.Max(_onWalking, _onRunning, _onCrouching);
        }

        public void SetOnWalking() =>
            CurrentSpeed = _onWalking;

        public void SetOnRunning() =>
            CurrentSpeed = _onRunning;

        public void SetOnCrouching() =>
            CurrentSpeed = _onCrouching;
    }
}
