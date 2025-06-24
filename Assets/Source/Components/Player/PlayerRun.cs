using System;
using Source.Services.Input.Contracts;
using UnityEngine;
using VContainer;

namespace Source.Components.Player
{
    public class PlayerRun : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private PlayerCrouch _playerCrouch;

        private IInputService _inputService;

        [Inject]
        private void Construct(IInputService inputService) =>
            _inputService = inputService ?? throw new ArgumentNullException(nameof(inputService));

        public bool CheckRunning()
        {
            bool isRunning = _inputService.CheckSprintButton()
                             && _characterController.velocity.sqrMagnitude > 0f;

            if (isRunning)
                _playerCrouch.StandUp();

            return isRunning && _playerCrouch.IsStanding;
        }
    }
}
