using Unity.Cinemachine;
using UnityEngine;

namespace Assets.Sources.Components.Camera
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera _camera;

        public void Follow(Transform target) =>
            _camera.Follow = target;
    }
}
