using Assets.Sources.Initialization.Di;
using Assets.Sources.Services;
using UnityEngine;

namespace Assets.Sources.Components
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField, Min(0)] private float _speed;

        [SerializeField] private CharacterController _characterController;

        private Transform _transform;
        private InputService _inputService;

        private void Awake()
        {
            _inputService = DiContainer.GetInstance().GetSingle<InputService>();

            _transform = transform;
        }

        private void Update()
        {
            Vector3 direction = _inputService.GetMoveDirection();

            _characterController.SimpleMove(_speed *
                _transform.TransformDirection(direction));
        }
    }
}
