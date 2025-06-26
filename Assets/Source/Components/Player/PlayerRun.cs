using System;
using Source.Services.Input.Contracts;
using UnityEngine;
using VContainer;

namespace Source.Components.Player
{
    public class PlayerRun : MonoBehaviour
    {
        private IInputService _inputService;

        [Inject]
        private void Construct(IInputService inputService) =>
            _inputService = inputService ?? throw new ArgumentNullException(nameof(inputService));

        public bool CheckRunning() =>
            _inputService.CheckSprintButton()
            && _inputService.GetMoveDirection().sqrMagnitude > 0f;
    }
}
