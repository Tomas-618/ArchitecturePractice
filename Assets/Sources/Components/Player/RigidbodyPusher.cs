using Assets.Sources.Components.Player.Constants;
using UnityEngine;

namespace Assets.Sources.Components.Player
{
    public class RigidbodyPusher : MonoBehaviour
    {
        [SerializeField, Min(0)] private float _strength;

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody targetRigidbody = hit.rigidbody;

            if (targetRigidbody == null || targetRigidbody.isKinematic)
                return;

            if (hit.moveDirection.y < PlayerConstants.MinMoveDirectionY)
                return;

            Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0f, hit.moveDirection.z);

            targetRigidbody.AddForce(pushDirection * _strength, ForceMode.Impulse);
        }
    }
}
