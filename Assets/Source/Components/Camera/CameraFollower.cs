using Unity.Cinemachine;
using UnityEngine;

namespace Source.Components.Camera
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera _camera;

        public void Follow(Transform target) =>
            _camera.Follow = target;
    }
}
