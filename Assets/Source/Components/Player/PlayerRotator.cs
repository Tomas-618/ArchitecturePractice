using Source.Components.Player.Constants;
using Source.Infrastructure.Di;
using Source.Services.Input.Contracts;
using UnityEngine;

namespace Source.Components.Player
{
    public class PlayerRotator : MonoBehaviour
    {
        [SerializeField, Min(0)] private float _sensitivity;

        [SerializeField] private Transform _player;
        [SerializeField] private float _minAngle;
        [SerializeField] private float _maxAngle;

        private IInputService _inputService;
        private float _pitch;

        [field: SerializeField] public Transform CameraTarget { get; private set; }

        private void OnValidate()
        {
            if (_minAngle > _maxAngle)
                _minAngle = _maxAngle;
        }

        private void Awake() =>
            _inputService = DiContainer.GetInstance().GetSingle<IInputService>();

        private void LateUpdate()
        {
            var rotation = CalculateRotation(_inputService.GetRotation());

            CameraTarget.localRotation = Quaternion.Euler(Vector3.right * rotation.y);
            _player.Rotate(Vector3.up * rotation.x);
        }

        private Vector2 CalculateRotation(Vector3 input)
        {
            Vector2 rotation = new(0f, _pitch);

            if (input.sqrMagnitude < PlayerConstants.RotationThreshold)
                return rotation;

            float yaw = input.x * _sensitivity * Time.deltaTime;

            _pitch += input.y * _sensitivity * Time.deltaTime;
            _pitch = Mathf.Clamp(_pitch, _minAngle, _maxAngle);

            rotation.Set(yaw, _pitch);

            return rotation;
        }
    }
}
