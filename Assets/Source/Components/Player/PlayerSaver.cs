using Source.Infrastructure.Di;
using Source.Services.Input.Contracts;
using Source.Services.Progress.Contracts;
using UnityEngine;

namespace Source.Components.Player
{
	public class PlayerSaver : MonoBehaviour
	{
		private IInputService _inputService;
		private ISaveLoadService _saveLoadService;

        private void Awake()
        {
            DiContainer diContainer = DiContainer.GetInstance();

            _inputService = diContainer.GetSingle<IInputService>();
            _saveLoadService = diContainer.GetSingle<ISaveLoadService>();
        }

        private void OnEnable() =>
            _inputService.SavedButtonPressed += OnSaveButtonPressed;

        private void OnDisable() =>
            _inputService.SavedButtonPressed += OnSaveButtonPressed;

        private void OnSaveButtonPressed() =>
            _saveLoadService.Save();
    }
}
