using System;
using Source.Infrastructure.StateMachine.LifeCycle.Contracts;
using Source.Services.Factories.Contracts;
using UnityEngine;

namespace Source.Components.Player
{
    public class PlayerMovementLifeCycle : MonoBehaviour
    {
        private IGameLoopStateMachine _stateMachine;

        public void Init(IGameLoopStateMachineFactory stateMachineFactory)
        {
            if (stateMachineFactory == null)
                throw new ArgumentNullException(nameof(stateMachineFactory));

            _stateMachine = stateMachineFactory.Create();
        }

        private void Update() =>
            _stateMachine?.Update();
    }
}
