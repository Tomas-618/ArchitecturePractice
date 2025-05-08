using UnityEngine;

namespace Source.Components.Points
{
    public class PlayerInitialPoint : MonoBehaviour
    {
        public Vector3 Position { get; private set; }

        public Quaternion Rotation { get; private set; }

        private void Awake()
        {
            Position = transform.position;
            Rotation = transform.rotation;
        }
    }
}
