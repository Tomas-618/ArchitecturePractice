using System;
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
        [SerializeField, Min(0)] private float _speed;

        [SerializeField] private CharacterController _characterController;

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

            _characterController.SimpleMove(_speed *
                                            _transform.TransformDirection(direction));
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
