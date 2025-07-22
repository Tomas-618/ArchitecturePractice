using UnityEngine;

namespace Source.Components.Checkers
{
    public class SphereCastChecker : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _radius;
        [SerializeField, Min(0f)] private float _distance;

        [SerializeField] private Vector3 _direction;
        [SerializeField] private LayerMask _layerMask;

        private Transform _transform;

        private void Awake() =>
            _transform = transform;

        public bool Check(RaycastHit[] hits)
        {
            return Physics.SphereCastNonAlloc(_transform.position, _radius,
                _direction, hits, _distance, _layerMask.value) > 0;
        }
    }
}
