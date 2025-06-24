using Source.Components.Player;
using UnityEngine;

namespace Source.Components.Hud
{
    public class HudPrefab : MonoBehaviour
    {
        [SerializeField] private PlayerStaminaBar _staminaBar;

        public void Init(PlayerStamina playerStamina) =>
            _staminaBar.Init(playerStamina);
    }
}
