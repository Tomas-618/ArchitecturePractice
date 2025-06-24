using UnityEngine;

namespace Source.Components.Player
{
    public class PlayerVelocityObserver : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;

        private Transform _transform;

        private void Awake() =>
            _transform = transform;

        public Vector3 CalculateRelative() =>
            _transform.InverseTransformDirection(_characterController.velocity);
    }
}
