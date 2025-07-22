using UnityEngine;

namespace Source.Components.Checkers
{
    public class SphereChecker : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _radius;

        [SerializeField] private LayerMask _layerMask;

        private Transform _transform;

        private void Awake() =>
            _transform = transform;

        public bool Check() =>
            Physics.CheckSphere(_transform.position, _radius, _layerMask.value);
    }
}
