using UnityEngine;

namespace Source.Components.Camera
{
    public class PlayerCameraTarget : MonoBehaviour
    {
        [field: SerializeField] public Transform Target { get; private set; }
    }
}
