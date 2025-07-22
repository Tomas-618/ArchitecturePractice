using System;
using Source.Infrastructure.StateMachine.LifeCycle.Contracts;
using Source.Services.Factories.Contracts;
using UnityEngine;
using VContainer;

namespace Source.Components.Player
{
    public class PlayerMovementLifeCycle : MonoBehaviour
    {
        [SerializeField] private PlayerSpeed _playerSpeed;
        [SerializeField] private PlayerCrouch _playerCrouch;
        [SerializeField] private PlayerRun _playerRun;
        [SerializeField] private PlayerStamina _playerStamina;

        private IGameLoopStateMachine _stateMachine;

        [Inject]
        public void Construct(IPlayerMovementStateMachineFactory stateMachineFactory)
        {
            if (stateMachineFactory == null)
                throw new ArgumentNullException(nameof(stateMachineFactory));

            _stateMachine = stateMachineFactory.Create(_playerSpeed, _playerCrouch,
                _playerRun, _playerStamina);
        }

        private void Update() =>
            _stateMachine.Update();
    }
}
