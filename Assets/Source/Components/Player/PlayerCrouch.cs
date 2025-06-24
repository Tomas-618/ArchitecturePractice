using System;
using Source.Components.Checkers;
using Source.Components.Player.Data;
using Source.Services.Input.Contracts;
using UnityEngine;
using VContainer;

namespace Source.Components.Player
{
    public class PlayerCrouch : MonoBehaviour
    {
        private readonly RaycastHit[] _hits = new RaycastHit[1];

        [SerializeField] private PlayerAnimator _animator;
        [SerializeField] private SphereCastChecker _overhangChecker;
        [SerializeField] private PlayerRun _playerRun;

        private IInputService _inputService;

        public bool IsStanding => _animator.CurrentState == PlayerAnimatorState.Idle;

        private bool IsSitting => _animator.CurrentState == PlayerAnimatorState.Sitting;

        [Inject]
        private void Construct(IInputService inputService) =>
            _inputService = inputService ?? throw new ArgumentNullException(nameof(inputService));

        private void OnEnable() =>
            _inputService.CrouchButtonPressed += OnCrouchButtonPressed;

        private void OnDisable() =>
            _inputService.CrouchButtonPressed -= OnCrouchButtonPressed;

        public bool CheckCrouching() =>
            IsStanding == false;

        public void StandUp()
        {
            if (_overhangChecker.Check(_hits) == false)
                _animator.PlayStandAnimation();
        }

        public void SitDown()
        {
            if (_playerRun.CheckRunning() == false)
                _animator.PlaySitAnimation();
        }

        private void OnCrouchButtonPressed()
        {
            if (IsSitting)
                StandUp();

            if (IsStanding)
                SitDown();
        }
    }
}
