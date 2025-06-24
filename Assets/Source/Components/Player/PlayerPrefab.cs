using UnityEngine;

namespace Source.Components.Player
{
    public class PlayerPrefab : MonoBehaviour
    {
        [field: SerializeField] public PlayerStamina Stamina { get; private set; }
    }
}
