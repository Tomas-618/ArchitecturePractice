using System;
using Source.Components.Player.Constants;
using Source.Data;
using Source.Data.Contracts;
using Source.Data.Surrogates;
using Source.Services.Input.Contracts;
using Source.Services.Progress.Contracts;
using UnityEngine;
using VContainer;

namespace Source.Components.Player
{
    public class PlayerMover : MonoBehaviour, IProgressSaver, IProgressLoader
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private PlayerSpeed _playerSpeed;
        [SerializeField] private PlayerStamina _playerStamina;

        private Transform _transform;
        private IInputService _inputService;

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService ?? throw new ArgumentNullException(nameof(inputService));
            _transform = transform;
        }

        private void Update()
        {
            var direction = _inputService.GetMoveDirection();

            float speed = _playerSpeed.GetCurrent();

            var movement = speed * _transform.TransformDirection(direction);

            if (movement.sqrMagnitude > _playerSpeed.SqrSpeedToReduceStamina)
                _playerStamina.Reduce(movement.sqrMagnitude * PlayerConstants.StaminaReduceFactor);
            else
                _playerStamina.StartRestoring();

            _characterController.SimpleMove(movement);
        }

        public void UpdateProgress(PlayerProgress playerProgress) =>
            playerProgress.Position = new Vector3Surrogate(_transform.position);

        public void LoadProgress(IReadOnlyPlayerProgress playerProgress)
        {
            var position = playerProgress.Position;

            _characterController.enabled = false;
            _transform.position = position.ConvertToVector3();
            _characterController.enabled = true;
        }
    }
}
