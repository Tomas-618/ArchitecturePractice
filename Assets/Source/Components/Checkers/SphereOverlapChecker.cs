using UnityEngine;

namespace Source.Components.Checkers
{
    public class SphereOverlapChecker : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _radius;

        [SerializeField] private LayerMask _layerMask;

        private Transform _transform;

        private void Awake() =>
            _transform = transform;

        public bool Check(Collider[] colliders) =>
            Physics.OverlapSphereNonAlloc(_transform.position, _radius, colliders, _layerMask.value) > 0;
    }
}
