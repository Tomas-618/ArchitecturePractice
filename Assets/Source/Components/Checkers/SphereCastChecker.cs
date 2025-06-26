using UnityEngine;

namespace Source.Components.Checkers
{
    public class SphereCastChecker : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _radius;
        [SerializeField, Min(0f)] private float _distance;

        [SerializeField] private Transform _transform;
        [SerializeField] private Vector3 _direction;
        [SerializeField] private LayerMask _layerMask;

        public bool Check(RaycastHit[] hits)
        {
            return Physics.SphereCastNonAlloc(_transform.position, _radius,
                _direction, hits, _distance, _layerMask.value) > 0;
        }
    }
}
