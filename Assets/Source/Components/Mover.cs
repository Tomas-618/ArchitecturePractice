using Source.Infrastructure.Di;
using Source.Services;
using UnityEngine;

namespace Source.Components
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;

        private IMovementService _movement;

        private void Awake() =>
            _movement = DiContainer.GetInstance().GetSingle<IMovementService>();

        private void FixedUpdate() =>
            _movement.Update(Time.deltaTime);

        private void Update() =>
            _characterController.Move(_movement.Velocity);
    }
}
