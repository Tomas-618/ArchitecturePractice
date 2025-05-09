using System;
using Source.Components.Camera;
using Source.Components.Player.Constants;
using Source.Data;
using Source.Data.Contracts;
using Source.Services.Input.Contracts;
using Source.Services.Progress.Contracts;
using UnityEngine;
using VContainer;

namespace Source.Components.Player
{
    public class PlayerRotator : MonoBehaviour, IProgressSaver, IProgressLoader
    {
        [SerializeField, Min(0)] private float _sensitivity;

        [SerializeField] private Transform _player;
        [SerializeField] private float _minAngle;
        [SerializeField] private float _maxAngle;

        private IInputService _inputService;
        private float _pitch;

        [field: SerializeField] public Transform CameraTarget { get; private set; }

        [Inject]
        private void Construct(PlayerCamera playerCamera, IInputService inputService)
        {
            if (playerCamera == null)
                throw new ArgumentNullException(nameof(playerCamera));

            playerCamera.SetFollowTarget(CameraTarget);
            _inputService = inputService ?? throw new ArgumentNullException(nameof(inputService));
        }

        private void OnValidate()
        {
            if (_minAngle > _maxAngle)
                _minAngle = _maxAngle;
        }

        private void LateUpdate()
        {
            var rotation = CalculateRotation(_inputService.GetRotation());

            CameraTarget.localRotation = Quaternion.Euler(Vector3.right * rotation.y);
            _player.Rotate(Vector3.up * rotation.x);
        }

        public void UpdateProgress(PlayerProgress progress) =>
            progress.Yaw = _player.rotation.eulerAngles.y;

        public void LoadProgress(IReadOnlyPlayerProgress progress)
        {
            var playerRotation = _player.rotation.eulerAngles;

            playerRotation.y = progress.Yaw;
            _player.rotation = Quaternion.Euler(playerRotation);
        }

        private Vector2 CalculateRotation(Vector2 input)
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
