using Unity.Cinemachine;
using UnityEngine;

namespace Source.Components.Camera
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera _camera;

        public void SetFollowTarget(Transform target) =>
            _camera.Follow = target;
    }
}
