using System;
using R3;
using Source.Components.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Components.Hud
{
    public class PlayerStaminaBar : MonoBehaviour
    {
        [SerializeField] private Image _bar;

        private PlayerStamina _playerStamina;
        private IDisposable _disposable;

        public void Init(PlayerStamina playerStamina)
        {
            _playerStamina = playerStamina ?? throw new ArgumentNullException(nameof(playerStamina));
            AddListener();
        }

        private void OnEnable() =>
            AddListener();

        private void OnDisable() =>
            RemoveListener();

        private void OnStaminaValueChanged(float value) =>
            _bar.fillAmount = value / _playerStamina.MaxValue;

        private void AddListener()
        {
            if (_playerStamina == null || _disposable != null)
                return;

            _disposable = _playerStamina.CurrentValue
                .Subscribe(OnStaminaValueChanged);
        }

        private void RemoveListener()
        {
            _disposable?.Dispose();
            _disposable = null;
        }
    }
}
