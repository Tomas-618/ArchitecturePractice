using Source.Components.Player.Constants;
using UnityEngine;

namespace Source.Components.Player
{
    public class RigidbodyPusher : MonoBehaviour
    {
        [SerializeField, Min(0)] private float _strength;

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            var targetRigidbody = hit.rigidbody;

            if (targetRigidbody == null || targetRigidbody.isKinematic)
                return;

            if (hit.moveDirection.y < PlayerConstants.MinMoveDirectionY)
                return;

            var pushDirection = new Vector3(hit.moveDirection.x, 0f, hit.moveDirection.z);

            targetRigidbody.AddForce(pushDirection * _strength, ForceMode.Impulse);
        }
    }
}
