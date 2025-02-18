using Assets.Sources.Components.Player.Constants;
using Assets.Sources.Initialization.Di;
using Assets.Sources.Services;
using UnityEngine;

namespace Assets.Sources.Components.Player
{
    public class PlayerRotator : MonoBehaviour
    {
        [SerializeField, Min(0)] private float _sensitivity;

        [SerializeField] private Transform _player;
        [SerializeField] private Transform _cameraTarget;
        [SerializeField] private float _minAngle;
        [SerializeField] private float _maxAngle;

        private InputService _inputService;
        private Vector2 _currentRotation;
        private float _pitch;

        private void OnValidate()
        {
            if (_minAngle > _maxAngle)
                _minAngle = _maxAngle;
        }

        private void Awake() =>
            _inputService = DiContainer.GetInstance().GetSingle<InputService>();

        private void LateUpdate()
        {
            CalculateRotation(_inputService.GetRotation());

            _cameraTarget.localRotation = Quaternion.Euler(Vector3.right * _currentRotation.y);
            _player.Rotate(Vector3.up * _currentRotation.x);
        }

        private void CalculateRotation(Vector3 input)
        {
            _currentRotation.Set(0f, _pitch);

            if (input.sqrMagnitude < PlayerConstants.RotationThreshold)
                return;

            float yaw = input.x * _sensitivity * Time.deltaTime;

            _pitch += input.y * _sensitivity * Time.deltaTime;
            _pitch = Mathf.Clamp(_pitch, _minAngle, _maxAngle);

            _currentRotation.Set(yaw, _pitch);
        }
    }
}
