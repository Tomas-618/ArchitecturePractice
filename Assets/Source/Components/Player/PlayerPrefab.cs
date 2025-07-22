using Source.Services.Factories.Contracts;
using UnityEngine;

namespace Source.Components.Player
{
    public class PlayerPrefab : MonoBehaviour
    {
        [field: SerializeField] public PlayerMovementLifeCycle MovementLifeCycle { get; private set; }

        [field: SerializeField] public PlayerSpeed Speed { get; private set; }

        [field: SerializeField] public PlayerCrouch Crouch { get; private set; }

        [field: SerializeField] public PlayerRun Run { get; private set; }

        [field: SerializeField] public PlayerStamina Stamina { get; private set; }

        public void Init(IGameLoopStateMachineFactory stateMachineFactory) =>
            MovementLifeCycle.Init(stateMachineFactory);
    }
}
