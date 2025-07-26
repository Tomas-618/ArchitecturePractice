using UnityEngine;

namespace Source.Components.Player
{
    public class PlayerOverhangChecker : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _radius;
        [SerializeField, Min(0f)] private float _height;

        [SerializeField] private LayerMask _layerMask;

        private Transform _transform;

        private void Awake() =>
            _transform = transform;

        public bool Check()
        {
            var start = _transform.position;

            var end = new Vector3(start.x,
                start.y + _height,
                start.z);

            return Physics.CheckCapsule(start, end, _radius, _layerMask.value);
        }
    }
}
