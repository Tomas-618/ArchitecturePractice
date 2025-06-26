using System;
using Source.Infrastructure.StateMachine.LifeCycle;

namespace Source.Components.Player.StateMachine.States
{
    public class PlayerWalkState : GameLoopState
    {
        private readonly PlayerSpeed _playerSpeed;

        public PlayerWalkState(PlayerSpeed playerSpeed,
            int transitionsCapacity) : base(transitionsCapacity) =>
            _playerSpeed = playerSpeed ?? throw new ArgumentNullException(nameof(playerSpeed));

        public override void Enter() =>
            _playerSpeed.SetOnWalking();
    }
}
